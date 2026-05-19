using sk.Core.Interfaces;
using sk.Core.Middlewares;
using sk.Core.Models.EventModels;
using sk.Core.Models.UserModels;
using sk.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDataBase<EventModel>, MockEventDB>();
builder.Services.AddSingleton<IDataBase<User>, MockUserDB>();

//builder.Services.AddScoped<IDataBase<EventModel>, MockEventDB>();
//builder.Services.AddScoped<IDataBase<User>, MockUserDB>();

builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


app.Run();
