using FinabitAPI;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Multitenancy;
using FinabitAPI.Core.Security;
using FinabitAPI.Employee;
using FinabitAPI.Finabit.Account;
using FinabitAPI.Finabit.Customization;
using FinabitAPI.Finabit.Items;
using FinabitAPI.Finabit.SystemData;
using FinabitAPI.Finabit.Transaction;
using FinabitAPI.Repository;
using FinabitAPI.User;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.IO;

var baseDir = AppContext.BaseDirectory;        
Directory.SetCurrentDirectory(baseDir);          

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ContentRootPath = baseDir,                
    Args = args
});

builder.Host.UseWindowsService();

builder.Services.AddCors(o =>
{
    o.AddPolicy("OpenAll", p => p
        .SetIsOriginAllowed(_ => true) 
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());          
});

builder.Logging.AddEventLog();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)   // base
    .AddJsonFile("instance.settings.json", optional: true, reloadOnChange: true) // overrides base
    .AddJsonFile("tenants.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args); // CLI still overrides everything

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ITenantStore, MutableFileTenantStore>();
builder.Services.AddScoped<ITenantAccessor, HttpContextTenantAccessor>();
builder.Services.AddScoped<DBAccess>();
builder.Services.AddHostedService<DatabaseBootstrapper>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<EmployeesRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<AccountDetailsRepository>();
builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<PartnerRepository>();
builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<ItemsMasterImportRepository>();
builder.Services.AddScoped<OptionsRepository>();
builder.Services.AddScoped<TransactionsRepository>();
builder.Services.AddScoped<ICustomizationRepository, CustomizationRepository>();

// SystemData services
builder.Services.AddScoped<FinabitAPI.Finabit.SystemData.SystemDataRepository>();
builder.Services.AddScoped<FinabitAPI.Finabit.SystemData.SystemDataService>();

// Server services
builder.Services.AddScoped<FinabitAPI.Core.Server.ServerRepository>();
builder.Services.AddScoped<FinabitAPI.Core.Server.ServerService>();

// VATPercent services
builder.Services.AddScoped<FinabitAPI.Finabit.VATPercent.VATPercentRepository>();
builder.Services.AddScoped<FinabitAPI.Finabit.VATPercent.VATPercentService>();

// HAccomodation services
builder.Services.AddScoped<FinabitAPI.Hotel.HAccomodation.HAccomodationRepository>();
builder.Services.AddScoped<FinabitAPI.Hotel.HAccomodation.HAccomodationService>();

// ExtraCharge services
builder.Services.AddScoped<FinabitAPI.Hotel.ExtraCharge.ExtraChargeRepository>();
builder.Services.AddScoped<FinabitAPI.Hotel.ExtraCharge.ExtraChargeService>();

builder.Services.AddControllers();

const string MasterKeyBase64 = "hZq8fQVt9wqzYQx0c6Pq9r2C7a0rG2q0bqZpH0m1y4g=";

builder.Services.AddSingleton<IPasswordProtector>(
    _ => new HardcodedKeyPasswordProtector(MasterKeyBase64));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Basic";
        options.DefaultChallengeScheme = "Basic";
    })
    .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Finabit API", Version = "v1" });

    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Basic auth. Enter your username and password."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
        }, Array.Empty<string>() }
    });

    options.AddSecurityDefinition("tenant", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-Tenant-Id",
        Description = "Tenant identifier (optional; falls back to default). You can also use ?tenant= in query."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "tenant" }
        }, Array.Empty<string>() }
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["X-Pipeline"] = "TenantFirst";
    await next();
});

app.UseMiddleware<TenantResolutionMiddleware>();

app.UseCors("OpenAll");

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finabit API v1"));

app.MapGet("/", () => Results.Redirect("/swagger")).AllowAnonymous();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (ctx, report) =>
    {
        ctx.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        await ctx.Response.WriteAsync("{\"status\":\"ok\"}");
    }
}).AllowAnonymous();

app.MapHealthChecks("/test", new HealthCheckOptions
{
    ResponseWriter = async (ctx, report) =>
    {
        ctx.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        await ctx.Response.WriteAsync("{\"status\":\"ok\"}");
    }
}).AllowAnonymous();

app.MapGet("/dbping", async (DBAccess db) =>
{
    try { await db.TestOpenAsync(); return Results.Ok("DB OK"); }
    catch (Exception ex) { return Results.Problem("DB ERROR: " + ex.Message); }
}).AllowAnonymous();

// shows effective config with provider precedence (helps catch overrides)
app.MapGet("/config-debug", (IConfiguration cfg) =>
{
    if (cfg is IConfigurationRoot root)
        return Results.Text(root.GetDebugView());
    return Results.Text("DebugView not available (cfg is not IConfigurationRoot).");
}).AllowAnonymous();

GlobalRepository.UseDbAccess(app.Services);

app.Run();
