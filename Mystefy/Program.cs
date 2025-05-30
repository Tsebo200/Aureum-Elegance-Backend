using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;
using Mystefy.Services;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//make sure that we avoid any object loops 
builder.Services.AddControllers()
.AddJsonOptions(options => {
options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Creates In local Database but gives issues with Swagger
// builder.Services.AddDbContext<MystefyDbContext>(options =>
// {
//     options.UseInMemoryDatabase("MystefyPerfumes");
// });

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
// Importing the service files and interface
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IWarehouseIngredients, WarehouseIngredientsRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFinishedProductService, FinishedProductService>();
builder.Services.AddScoped<IBatchFinishedProductService, BatchFinishedProductService>();
builder.Services.AddScoped<IWarehouse, WarehouseService>();
builder.Services.AddScoped<IFragranceService, FragranceService>();
builder.Services.AddScoped<IWarehouseStockService, WarehouseStockService>();
builder.Services.AddScoped<IFragranceIngredientService, FragranceIngredientService>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<IPackagingRepository, PackagingRepositoryService>();
builder.Services.AddScoped<IStockRequestRepository, StockRequestRepositoryService>();
builder.Services.AddScoped<IStockRequestIngredientsRepository, StockRequestIngredientsRepositoryService>();
builder.Services.AddScoped<IStockRequestPackagingsRepository, StockRequestPackagingsRepositoryService>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryIngredientsRepository, DeliveryIngredientsRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IWasteLossRecordIngredientsRepository, WasteLossRecordIngredientsRepositoryService>();
builder.Services.AddScoped<IWasteLossRecordPackagingRepository, WasteLossRecordPackagingRepositoryService>();
builder.Services.AddScoped<IWasteLossRecordFragranceRepository, WasteLossRecordFragranceRepositoryService>();
builder.Services.AddScoped<IWasteLossRecordBatchFinishedProductsRepository, WasteLossRecordBatchFinishedProductsRepositoryService>();
builder.Services.AddScoped<IFinishedProductPackaging, FinishedProductPackagingService>();


Env.Load();

var connectionString = Env.GetString("DB_CONNECTION");
builder.Services.AddDbContext<MystefyDbContext>(options => options.UseNpgsql(connectionString));

// var connectionString = builder.Configuration.GetConnectionString("DB_CONNECTION");
// builder.Services.AddDbContext<MystefyDbContext>(options => options.UseNpgsql(connectionString));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5123")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

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
