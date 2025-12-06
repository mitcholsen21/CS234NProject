using Microsoft.EntityFrameworkCore;
using BitsEFClasses;
using BitsEFClasses.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BitsContext>(options =>
{
    options.UseMySql(
        ConfigDB.GetMySqlConnectionString(),
        new MySqlServerVersion(new Version(8, 0, 0)));
});

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
