using Microsoft.EntityFrameworkCore;
using ValtiShop.Application.Interfaces;
using ValtiShop.Persistence.Data;
using ValtiShop.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ValtiShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ValtiShopDb")));

builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
