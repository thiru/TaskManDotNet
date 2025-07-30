using TaskManDotNet.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var tasks = new[]
{
    new TaskItem(1, "first", false),
    new TaskItem(2, "second", false),
    new TaskItem(3, "third", false)
};

app.MapGet("/api/tasks", () =>
{
    return Results.Ok(tasks);
});

app.Run();
