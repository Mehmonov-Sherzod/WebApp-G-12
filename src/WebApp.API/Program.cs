using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Models;
using WebApp.Application.Services;
using WebApp.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
person1 person1 = new person1();
person1 person2 = new person1();
Console.WriteLine(person1 == person2);



\

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SubjectService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql("Host=localhost;Port=5432;Database=test_app;Username=postgres;Password=Sherzod3466");
});

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
