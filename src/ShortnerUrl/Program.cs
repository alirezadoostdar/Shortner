using Microsoft.EntityFrameworkCore;
using ShortnerUrl.Endpoints;
using ShortnerUrl.Infrastructures;
using ShortnerUrl.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ShortenService>();
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

app.Run();


