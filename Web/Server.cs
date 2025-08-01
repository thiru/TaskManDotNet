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
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<TaskItemDb>(opt => opt.UseInMemoryDatabase("TaskItemList"));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // TODO restrict cors setup for prod
    builder.Services.AddCors(options =>
    {
      options.AddDefaultPolicy(
          policy =>
          {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
          });
    });

    var app = builder.Build();
    app.UseCors();

    return app;
  }
}
