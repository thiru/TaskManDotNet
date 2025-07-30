namespace TaskManDotNet.Web;

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
    builder.Services.AddOpenApi();
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      app.MapOpenApi();
    }

    return app;
  }
}
