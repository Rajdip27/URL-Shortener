using System.Text.Json.Serialization;
using UrlShortener.Api.Endpoints;
using UrlShortener.Api.Extensions;
using UrlShortener.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger & API explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers and JSON options to ignore null values
builder.Services.AddControllers().AddJsonOptions(o =>
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// Register your app services (DbContext, MediatR, Redis, Validation, etc.)
builder.Services.AddShortenerServices(builder.Configuration);

// Add database migration runner service
builder.Services.AddSingleton<DatabaseMigrationService>();

var app = builder.Build();

// Run database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetRequiredService<DatabaseMigrationService>();
    await migrationService.MigrateAsync();
}

// Configure Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Url Shortener API V1");
    });
}

// Map your minimal API endpoints
ShortLinkEndpoints.Map(app);

app.Run();
