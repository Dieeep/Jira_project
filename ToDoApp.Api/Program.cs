using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Services;
using ToDoApp.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    // Налаштування глибини, якщо потрібно
    options.JsonSerializerOptions.MaxDepth = 64; // Або інше значення, відповідно до ваших потреб
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ToDoAppDb");

builder.Services.AddDbContext<ToDoContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITaskService, TaskService>();


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
