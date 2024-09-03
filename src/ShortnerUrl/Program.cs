using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using ShortnerUrl.Endpoints;
using ShortnerUrl.Infrastructures;
using ShortnerUrl.Observebility;
using ShortnerUrl.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ShortenService>();
builder.Services.AddOpenTelemetry().WithMetrics(builder =>
{
    builder.AddPrometheusExporter();
    var metric = new []
    {
        ShortenDiagnotics.MeterName
    };

    builder.AddMeter(metric);
});

builder.Services.AddSingleton<ShortenDiagnotics>();

builder.Services.AddDbContext<ShortnerUrlDbContext>(configure =>
{
    var host = builder.Configuration["MongoDbConnectionString:Host"];
    var dbName = builder.Configuration["MongoDbConnectionString:DbName"];

    if(host is null)
        throw new ArgumentNullException(nameof(host));
    if(dbName is null)
        throw new ArgumentNullException(nameof(dbName));

    configure.UseMongoDB(host, dbName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapShortenEndpoint();
app.MapRedirectEndpoint();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();


