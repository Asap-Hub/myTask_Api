using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 
using myShop.Domain.Model;
using myShop.Infrastructure.Persistence.Data;
using myShop.Infrastructure.Services;
using myShop.Infrastructure.Utility;
using System.Configuration;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    option => option.AddDefaultPolicy(
        builder =>
        {   
            builder.AllowAnyHeader().AllowAnyMethod();

        }
    ));
 

//configuration
builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection(nameof(ConnectionString)));

//connection string
builder.Services.AddDbContext<MyShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("localConnection"));
}, ServiceLifetime.Transient);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {

    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;

    //for confirming email before using
    options.SignIn.RequireConfirmedEmail = true;    


}).AddEntityFrameworkStores<MyShopContext>().AddDefaultTokenProviders();

//builder.Services.AddScoped<UserManager<IdentityUser>>();
//builder.Services.AddScoped<SignInManager<TbIdentityUserlUser>>();

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWT:Issuer"], 
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
    };
});


//for only allowing admins to have access to the account.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",
        policy => policy.RequireClaim("Admin"));
});


//configuring smtp 
builder.Services.Configure<SMTPSettings>(builder.Configuration.GetSection("SMTP"));

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddValidtors();
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



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
