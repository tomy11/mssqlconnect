using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using mssqlconnect.Models;
using mssqlconnect.Repository;
using mssqlconnect.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MovieContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext"))
);

builder.Services.AddScoped<IMovieRepository, MovieService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
