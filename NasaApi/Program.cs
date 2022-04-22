using NasaApi.Library.DataAccess;
using NasaApi.Library.Settings;
using NasaApi.Library;
using NasaApi.Persistence.Context;
using MediatR;
using NasaApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// TODO: configuración
builder.Services.Configure<NasaSettings>(builder.Configuration.GetSection(nameof(NasaSettings)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<INearEarthObjectService, NearEarthObjectService>();
builder.Services.AddMediatR(typeof(LibraryMediatREntrypoint).Assembly);
builder.Services.AddMediatR(typeof(AsteroidsController).Assembly);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); ;

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

