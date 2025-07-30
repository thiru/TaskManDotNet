namespace TaskManDotNet.Web;

using TaskManDotNet.Core;

public static class TaskItemApi
{
  // Test data - to be replaced with real DB
  static TaskItem[] tasks = new[]
  {
    new TaskItem(1, "first", false),
    new TaskItem(2, "second", false),
    new TaskItem(3, "third", false)
  };

  public static TaskItem[] GetAll()
  {
    return tasks;
  }
}
