using AutoMapper;
using InventoryApi.Data;
using InventoryApi.Middleware;
using InventoryApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;  // For Swagger
using System.Text;
using InventoryApi.Profiles;
using InventoryApi.Dtos;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container (Dependency Injection - Fundamental 1)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext (Fundamental 2: Data Access)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // SQL Server setup

// Register custom services (Fundamental 7: Services Layer)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();  // For JWT generation

// AutoMapper for DTO mapping (Fundamental 5) - Updated to scan assemblies
builder.Services.AddAutoMapper(typeof(MapperProfile));


// JWT Authentication (Fundamental 3)
var key = Encoding.ASCII.GetBytes("your-secret-key-at-least-32-chars-long");  // Store in appsettings.json
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// CORS for frontend (Middleware - Fundamental 4)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware Pipeline (Fundamental 4: Order matters - CORS first, then auth, then endpoints)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();  // JWT validation
app.UseAuthorization();

// Custom Middleware for logging (Fundamental 4 & 7: Audit trails)
app.UseMiddleware<AuditLoggingMiddleware>();

// Global Error Handling Middleware (Fundamental 6)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"message\": \"An error occurred.\"}");
    });
});

app.MapControllers();

app.Run();