using Microsoft.EntityFrameworkCore;
using widebot.interfaces;
using widebot.Mappings;
using widebot.Models;
using widebot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database connection
builder.Services.AddDbContext<WidebotContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// إضافة Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
});

// إضافة HttpClient و WeatherService
builder.Services.AddHttpClient<WeatherService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
