using Microsoft.EntityFrameworkCore;
using WebApp.Application.Services;
using WebApp.Application.Services.Impl;
using WebApp.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//person1 person1 = new person1();
//person1 person2 = new person1();
//Console.WriteLine(person1 == person2);



//\

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql("Host=localhost;Port=5432;Database=test_app;Username=postgres;Password=Sherzod3466");
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

app.MapControllers();

app.Run();
