using Contracts;
using LibraryManagement.Database;
using LibraryManagement.EmailUtilities.Service;
using LibraryManagement.EmailUtilities.ServiceImpl;
using LibraryManagement.Extensions;
using LibraryManagement.Services;
using LibraryManagement.Services.ServiceImpl;
using LibraryManagement.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryDbContext>(options => {
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});


builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters { 
      ValidateIssuer = false,
      ValidIssuer = builder.Configuration["jwt:Issuer"],
      ValidateAudience = false,
      ValidAudience = builder.Configuration["jwt:Audience"],
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Signingkey"]!)),
      ClockSkew = TimeSpan.Zero
    };
});

/**
 * Adding Logger service
 */
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


/**
 * Injecting Email configuration class in utilities to extract 
 * Values stored in appsettings.json file.
 */
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig!);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

//inserting services
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<UserService,UserServiceImpl>();


//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
