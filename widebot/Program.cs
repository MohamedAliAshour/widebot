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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArticlecs, ArticleServices>();
builder.Services.AddScoped<IShortUrl, ShortUrlServices>();


builder.Services.AddAutoMapper(typeof(ObjectMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "shorturl_redirect",
    pattern: "{shortCode}",
    defaults: new { controller = "ShortUrl", action = "RedirectToLongUrl" }
);


app.UseAuthorization();

app.MapControllers();

app.Run();
