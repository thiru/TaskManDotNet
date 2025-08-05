namespace TaskManDotNet.Web.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Web;
using TaskManDotNet.Core;
using TaskManDotNet.Db;

public static class TaskItemView
{
  public static async Task Get(TaskItemDb db, HttpContext context)
  {
    var taskItems = await db.TaskItems.ToListAsync();

    var html = $"""
      <div>
        {AddButton()}
        {TaskItemList(taskItems)}
      </div>
      """;

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(html);
  }

  public static async Task Post(TaskItemDb db, HttpContext context)
  {
    var desc = context.Request.Headers["HX-Prompt"];

    var taskItem = new TaskItem {Description = desc};

    var valResult = taskItem.Validate();
    if (valResult.Failed())
    {
      context.Response.StatusCode = 400;
      context.Response.ContentType = "text/plain";
      await context.Response.WriteAsync(valResult.Message);
    }
    else
    {
      db.TaskItems.Add(taskItem);
      await db.SaveChangesAsync();

      Console.WriteLine("Created task with id {0} and description: {1}", taskItem.Id, taskItem.Description);

      context.Response.ContentType = "text/html";
      await context.Response.WriteAsync(TaskItemRow(taskItem));
    }
  }

  public static async Task PutIsDone(int id, TaskItemDb db, HttpContext context)
  {
    var taskItem = await db.TaskItems.FindAsync(id);

    if (taskItem is null)
    {
      context.Response.StatusCode = 404;
      context.Response.ContentType = "text/plain";
      await context.Response.WriteAsync("Task not found");
    }
    else
    {
      var form = await context.Request.ReadFormAsync();
      var isDone = form.SingleOrDefault(kvp => kvp.Key == "IsDone").Value;
      if (StringValues.IsNullOrEmpty(isDone))
      {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Invalid value");
      }
      else
      {
        taskItem.IsDone = bool.Parse(isDone.FirstOrDefault());
        await db.SaveChangesAsync();

        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync(TaskItemRow(taskItem));
      }
    }
  }

  public static async Task PutDescription(int id, TaskItemDb db, HttpContext context)
  {
    var taskItem = await db.TaskItems.FindAsync(id);

    if (taskItem is null)
    {
      context.Response.StatusCode = 404;
      context.Response.ContentType = "text/plain";
      await context.Response.WriteAsync("Task not found");
    }
    else
    {
      var description = context.Request.Headers["HX-Prompt"];
      taskItem.Description = description;

      var valResult = taskItem.Validate();
      if (valResult.Failed())
      {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(valResult.Message);
      }
      else
      {
        await db.SaveChangesAsync();

        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync(TaskItemRow(taskItem));
      }
    }
  }

  public static async Task Delete(int id, TaskItemDb db, HttpContext context)
  {
      if (await db.TaskItems.FindAsync(id) is TaskItem taskItem)
      {
        db.TaskItems.Remove(taskItem);
        await db.SaveChangesAsync();

        Console.WriteLine("Deleted task with id {0} and description: {1}", taskItem.Id, taskItem.Description);

        context.Response.ContentType = "text/html";
        await context.Response.CompleteAsync();
      }
      else
      {
        context.Response.StatusCode = 404;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Task not found");
      }
  }

  private static string AddButton()
  {
    return """
      <button class="button mb-3"
              hx-post="/tasks"
              hx-prompt="Task Description:"
              hx-swap="beforebegin"
              hx-target="#new-task">
        Add Task
      </button>
      """;
  }

  private static string TaskItemList(List<TaskItem> taskItems)
  {
    return $"""
      <ul>
        {String.Join(" ", taskItems.Select(x => TaskItemRow(x)).ToList())}
        <li id="new-task"></li>
      </ul>
      """;
  }

  private static string TaskItemRow(TaskItem taskItem)
  {
    return $"""
      <li>
        <span id="task{taskItem.Id}" class="has-hover-show" data-task-id="{taskItem.Id}">
          {LabeledCheckbox(taskItem)}
          {EditButton(taskItem)}
          {DeleteButton(taskItem)}
        </span>
      </li>
      """;
  }

  private static string LabeledCheckbox(TaskItem taskItem)
  {
    var isChecked = taskItem.IsDone ? "checked" : "";
    var hxVals = "js:{'IsDone': this.checked}";

    return $"""
      <label class="checkbox">
        <input {isChecked}
               hx-put="/tasks/{taskItem.Id}/is-done"
               hx-target="#task{taskItem.Id}"
               hx-vals="{hxVals}"
               type="checkbox">
        {HttpUtility.HtmlEncode(taskItem.Description)}
      </label>
      """;
  }

  private static string EditButton(TaskItem taskItem)
  {
    return $"""
      <button class="button is-white is-small ml-1 mb-2 is-invisible hover-show"
              hx-put="/tasks/{taskItem.Id}/desc"
              hx-prompt="Task Description:"
              hx-swap="outerhtml"
              hx-target="#task{taskItem.Id}"
              title="Edit description">
        <span class="icon"><i class="fa-solid fa-pen-to-square"></i></span>
      </button>
      """;
  }

  private static string DeleteButton(TaskItem taskItem)
  {
    return $"""
      <button class="button is-warning is-light is-small ml-1 mb-2 is-invisible hover-show"
              hx-confirm="Are you sure you would like to permanently delete the following task: {HttpUtility.HtmlEncode(taskItem.Description)}"
              hx-delete="/tasks/{taskItem.Id}"
              hx-swap="outerhtml swap:1s"
              hx-target="#task{taskItem.Id}"
              title="Permanently delete task">
        <span class="icon"><i class="fa-solid fa-circle-minus"></i></span>
      </button>
      """;
  }
}
