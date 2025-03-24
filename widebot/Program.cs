using Microsoft.EntityFrameworkCore;
using widebot.Configurations;
using widebot.interfaces;
using widebot.Mappings;
using widebot.Models;
using widebot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WidebotContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Bind WeatherAPI settings using Options Pattern
builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection("WeatherAPI"));


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
});

builder.Services.AddHttpClient<IWeather, WeatherService>(); 



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWeather, WeatherService>();
builder.Services.AddScoped<IArticlecs, ArticleServices>();
builder.Services.AddScoped<IShortUrl, ShortUrlServices>();
builder.Services.AddScoped<IRedirect, RedirectServices>();


builder.Services.AddAutoMapper(typeof(ObjectMapper));


var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
