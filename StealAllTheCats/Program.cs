using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StealAllTheCats;
using StealAllTheCats.Repositories;
using StealAllTheCats.Repositories.Interfaces;
using StealAllTheCats.Services;
using StealAllTheCats.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CatsDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ICatsService, CatsService>();
builder.Services.AddScoped<ICatsRepository, CatsRepository>();
builder.Services.AddAutoMapper(typeof(CatMappingProfile));

builder.Services.AddHttpClient<IHttpClientService, HttpClientService>(client =>
{
    client.BaseAddress = new Uri("https://api.thecatapi.com");
    client.DefaultRequestHeaders.Add("x-api-key", "live_XW9oh9nKLkr5LYX9sHS8J5LCg0G5C6Egqg2yMU6aYHM7g5A8GBYg5ZuVAeM7RIno");
});



builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
