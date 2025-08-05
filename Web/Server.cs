namespace TaskManDotNet.Web;

using Microsoft.EntityFrameworkCore;
using TaskManDotNet.Db;

public static class Server
{
  public static void Start(string[] args)
  {
    var app = CreateWebApp(args);

    Routes.Init(app);

    app.Run();
  }

  private static WebApplication CreateWebApp(string[] args)
  {
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
      WebRootPath = "frontend"
    });

    builder.Services.AddDbContext<TaskItemDb>(opt => opt.UseInMemoryDatabase("TaskItemList"));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    var app = builder.Build();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    return app;
  }
}
