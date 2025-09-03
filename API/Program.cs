using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(); //To Access Cross platform...

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3001", "https://localhost:3001"));
app.MapControllers();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider;
try
{
    var service = context.GetRequiredService<AppDbContext>();
    await service.Database.MigrateAsync();
    await DbInitializer.SeedData(service);
}
catch (Exception ex)
{
    var logger = context.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error");
}
app.Run();
