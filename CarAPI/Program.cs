using CarAPI.Entities;
using CarAPI.Payment;
using CarAPI.Repositories;
using CarAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOwnersService, OwnersService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IOwnersRepository, OwnersRepository>();
builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<IPaymentService, CashService>();
builder.Services.AddSingleton<InMemoryContext>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
