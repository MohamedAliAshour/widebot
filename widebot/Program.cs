using Interfaces.interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Model;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database connection
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArticlecs, ArticleServices>();
builder.Services.AddScoped<IShortUrl, ShortUrlServices>();


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
