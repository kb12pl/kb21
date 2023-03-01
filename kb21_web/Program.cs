using kb21_web.Data;
using kb21_web.Servicec;
using Microsoft.EntityFrameworkCore;

using static kb21_tools.KbLog;

LogInit(Console.WriteLine);



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PgSqlContext>(options => options
            .UseNpgsql(@"Host=localhost;Username=aa;Password=aa;Database=aa")
            .UseSnakeCaseNamingConvention());

//builder.Services.AddSingleton<BlogContext>();
//builder.Services.AddSingleton<ITestObject,TestObject>();


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

