using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Extensions;
using Aplicatie_Transporturi.Interfaces;
using Aplicatie_Transporturi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transport API", Version = "v1" });
});

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    // Use PostgreSQL if connection string contains "Host=" (Docker/Production)
    if (connectionString != null && connectionString.Contains("Host="))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        // Use SQL Server for local development
        options.UseSqlServer(connectionString);
    }
});

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();

var app = builder.Build();

// Apply migrations automatically in production
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

// Enable Swagger in all environments for Docker
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transport API v1");
});

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();