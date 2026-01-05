using System.Security.Claims;
using System.Text;
using FrasesApi.Endpoints;
using FrasesApi.Features.Auth;
using FrasesApi.Features.Auth.Infrastructure.AuthService;
using FrasesApi.Shared;
using FrasesApi.Shared.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
    
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings!.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
    
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Log.Information($"�� JWT Authentication Failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Log.Information($"🔍 JWT Token Validated Successfully");
            Log.Information($"🔍 Claims: {string.Join(", ", context.Principal?.Claims.Select(c => $"{c.Type}={c.Value}") ?? [])}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Log.Information($"🔍 JWT Challenge: {context.Error}, {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => 
    {
        policy.RequireClaim(ClaimTypes.Role, nameof(Roles.User));
        policy.RequireAssertion(context =>
        {
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
            Log.Information($"🔍 [Authorization] User policy check - Role: {role}");
            Log.Information($"🔍 [Authorization] Expected role: {Roles.User}");
            Log.Information($"🔍 [Authorization] Policy result: {role == nameof(Roles.User)}");
            return role == nameof(Roles.User);
        });
    });
    
    options.AddPolicy("Admin", policy => 
    {
        policy.RequireClaim(ClaimTypes.Role, nameof(Roles.Admin));
        policy.RequireAssertion(context =>
        {
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
            Log.Information($"🔍 [Authorization] Admin policy check - Role: {role}");
            Log.Information($"🔍 [Authorization] Expected role: {Roles.Admin}");
            Log.Information($"🔍 [Authorization] Policy result: {role == nameof(Roles.Admin)}");
            return role == nameof(Roles.Admin);
        });
    });
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new FrasesApi.Shared.Application.Common.ResultsHandler.GenericResultJsonConverter());
    options.SerializerOptions.Converters.Add(new FrasesApi.Shared.Application.Common.ResultsHandler.ResultJsonConverter());
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Frases API";
        document.Info.Version = "v1";
        document.Info.Description = "A frases platform API";
        document.Info.Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Frases Team",
            Email = "contact@frases.com"
        };
        
        document.Components ??= new Microsoft.OpenApi.Models.OpenApiComponents();
        document.Components.SecuritySchemes["Bearer"] = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token"
        };
        
        return Task.CompletedTask;
    });
});

builder.Services.AddAuthModule();
builder.Services.AddShared(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Frases API Documentation")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithSearchHotKey("k");
    });
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();
app.MapFrasesEndpoints();

app.Run();