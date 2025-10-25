using Microsoft.EntityFrameworkCore;
using Basic_Task_Manager.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskDbContext>(opt =>
    opt.UseInMemoryDatabase("TaskListDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Add CORS services
builder.Services.AddCors(options =>
    options.AddPolicy("AllowReactApp",
        policy =>
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
            )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.MapControllers();
app.MapGet("/health", () => Results.Ok());
app.Run();

