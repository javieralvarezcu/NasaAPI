using MediatR;
using Microsoft.EntityFrameworkCore;
using NasaApi.Controllers;
using NasaApi.Library;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Settings;
using NasaApi.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// TODO: configuración
builder.Services.Configure<NasaSettings>(builder.Configuration.GetSection(nameof(NasaSettings)));
builder.Services.Configure<PaypalSettings>(builder.Configuration.GetSection(nameof(PaypalSettings)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<INearEarthObjectService, NearEarthObjectService>();
builder.Services.AddScoped<IPaypalService, PaypalService>();
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

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = serviceScope?.ServiceProvider.GetRequiredService<MyDbContext>();
    context?.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

