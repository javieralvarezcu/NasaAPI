using NasaApi.Services;
using NasaApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// TODO: configuración
builder.Services.Configure<NasaSettings>(builder.Configuration.GetSection(nameof(NasaSettings)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<INearEarthObjectService, NearEarthObjectService>();

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

