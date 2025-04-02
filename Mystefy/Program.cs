using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Services;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//make sure that we avoid any object loops 
builder.Services.AddControllers()
.AddJsonOptions(options => {
options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IWarehouseIngredients, WarehouseIngredientsRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFinishedProductService, FinishedProductService>();


Env.Load();

var connectionString = Env.GetString("DB_CONNECTION");
builder.Services.AddDbContext<MystefyDbContext>(options => options.UseNpgsql(connectionString));

// var connectionString = builder.Configuration.GetConnectionString("DB_CONNECTION");
// builder.Services.AddDbContext<MystefyDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
