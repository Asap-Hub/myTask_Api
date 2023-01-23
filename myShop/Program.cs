using Microsoft.EntityFrameworkCore;
using myShop.Application.Service;
using myShop.Infrastructure.Persistence.Data;
using myShop.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    option => option.AddDefaultPolicy(
        builder =>
        {   
            builder.AllowAnyHeader().AllowAnyMethod();

        }
    ));
 

//connection string
builder.Services.AddDbContext<MyShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddValidtors();
builder.Services.AddControllers();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
var app = builder.Build();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

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
