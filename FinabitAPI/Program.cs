using AutoBit_WebInvoices.Models;
using Finabit_API.Models;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;          // +++
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.Mime;                 
using Microsoft.Extensions.Hosting.WindowsServices;// +++
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();

var sidecarPath = Path.Combine(AppContext.BaseDirectory, "instance.settings.json");
if (File.Exists(sidecarPath))
{
    builder.Configuration.AddJsonFile(sidecarPath, optional: true, reloadOnChange: false); 
}

builder.Configuration.AddEnvironmentVariables();              

GlobalRepository.Initialize(builder.Configuration);

builder.Services.AddSingleton<DBAccess>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<EmployeesRepository>();
builder.Services.AddScoped<DepartmentRepository>();

builder.Services.AddControllers();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

// Swagger (unchanged)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Finabit API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHealthChecks();                          

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions           
{
    ResponseWriter = async (ctx, _) =>
    {
        ctx.Response.ContentType = MediaTypeNames.Application.Json;
        await ctx.Response.WriteAsync("{\"status\":\"ok\"}");
    }
});

app.Run();
