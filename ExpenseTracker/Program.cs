using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Interface;
using ExpenseTracker.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Local",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetPreflightMaxAge(TimeSpan.FromHours(1));
        });
});

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("Port") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Local");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
