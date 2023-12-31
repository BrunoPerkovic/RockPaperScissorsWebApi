using Microsoft.EntityFrameworkCore;
using RockPaperScissorsAPI.BL.Config;
using RockPaperScissorsAPI.BL.Interfaces;
using RockPaperScissorsAPI.BL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("RpsDatabase");
    options.EnableSensitiveDataLogging();
});
builder.Services.AddScoped<IRpsService, RpsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();