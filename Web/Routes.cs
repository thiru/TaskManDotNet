namespace TaskManDotNet.Web;

using TaskManDotNet.Web.Views;

public static class Routes
{
  public static void Init(WebApplication app)
  {
    app.MapGet("/tasks", TaskItemView.Get);
    app.MapPost("/tasks", TaskItemView.Post);
    app.MapPut("/tasks/{id}/is-done", TaskItemView.PutIsDone);
    app.MapPut("/tasks/{id}/desc", TaskItemView.PutDescription);
    app.MapDelete("/tasks/{id}", TaskItemView.Delete);
  }
}
