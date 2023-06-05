using Microsoft.EntityFrameworkCore;
using UniversidadAPI;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// DB CONTEXTs.
builder.Services.AddDbContext<DBConnection>(options =>
     options.UseOracle("DATA SOURCE="+configuration.GetConnectionString("Data Source")+";PASSWORD="+ configuration.GetConnectionString("Password") + ";USER ID="+ configuration.GetConnectionString("User ID") + ";"));
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
