using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApp.API.Middleware;
using WebApp.Application.Common;
using WebApp.Application.Helpers.GenerateJwt;
using WebApp.Application.Helpers.PasswordHashers;
using WebApp.Application.Services;
using WebApp.Application.Services.Impl;
using WebApp.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var jwtSettings = builder.Configuration.GetSection("JwtOption").Get<JwtOption>();

builder.Services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
    };
});

// Add services to the container.
//person1 person1 = new person1();
//person1 person2 = new person1();
//Console.WriteLine(person1 == person2);
//\

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Swagger + JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your Project Name", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securitySchema, new[] { "Bearer" }
        }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<PasswordHelper>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql("Host=localhost;Port=5432;Database=Test-App;Username=postgres;Password=Sherzod3466");
});


var app = builder.Build();

using var scope = app.Services.CreateScope();
AutoMigration.Migrate(scope.ServiceProvider);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
