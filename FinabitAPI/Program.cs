using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using AutoBit_WebInvoices.Models;
using FinabitAPI.Multitenancy;
using FinabitAPI.Utilis;
using FinabitAPI.Repository;

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

builder.Services.AddControllers();

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



app.Run();
