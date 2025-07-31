namespace TaskManDotNet.Web;

public static class Routes
{
  public static void Init(WebApplication app)
  {
    var taskItems = app.MapGroup("/api/tasks");

    taskItems.MapGet("/", TaskItemApi.GetAll);
    taskItems.MapGet("/{id}", TaskItemApi.GetById);
    taskItems.MapPost("/", TaskItemApi.Create);
    taskItems.MapPut("/{id}", TaskItemApi.Update);
    taskItems.MapDelete("/{id}", TaskItemApi.Delete);
  }
}
