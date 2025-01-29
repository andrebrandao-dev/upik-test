using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

var builder = WebApplication.CreateBuilder(args);
var  AllowLocalhost = "_allowOriginLocalhost";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ImageContext>(opt => opt.UseInMemoryDatabase("Image"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowLocalhost,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(AllowLocalhost);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
