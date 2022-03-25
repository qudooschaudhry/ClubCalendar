using Connect.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Connect.Infrastructure.Repositories;
using Connect.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));


ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IApplicationSettings, ApplicationSettingsAdapter>();

builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

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
