namespace TaskManDotNet.Web;

public static class Routes
{
  public static void Init(WebApplication app)
  {
    app.MapGet("/api/tasks", TaskItemApi.GetAll);
  }
}
