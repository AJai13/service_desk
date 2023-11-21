using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceDesk.Data;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ServiceDeskDbContext>();
builder.Services.AddCors();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceDesk V1");
    });
}

app.UseHttpsRedirection();
app.UseCors(opcoes => opcoes
    .WithOrigins("http://localhost:4200") // endereço do front
    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.UseAuthorization();
app.MapControllers();
app.Run();
