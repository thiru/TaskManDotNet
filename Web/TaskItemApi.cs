namespace TaskManDotNet.Web;

using Microsoft.EntityFrameworkCore;
using TaskManDotNet.Core;
using TaskManDotNet.Db;
using TaskManDotNet.Utils;

public static class TaskItemApi
{
  public static async Task<IResult> GetAll(TaskItemDb db)
  {
    return await WrapException(async () => TypedResults.Ok(await db.TaskItems.ToListAsync()));
  }

  public static async Task<IResult> GetById(int id, TaskItemDb db)
  {
    return await WrapException(async () =>
    {
      return await db.TaskItems.FindAsync(id)
        is TaskItem taskItem
          ? TypedResults.Ok(taskItem)
          : TypedResults.NotFound();
    });
  }

  public static async Task<IResult> Create(TaskItem taskItem, TaskItemDb db)
  {
    return await WrapException(async () =>
    {
      var valResult = taskItem.Validate();
      if (valResult.Failed())
        return TypedResults.BadRequest(valResult);

      db.TaskItems.Add(taskItem);
      await db.SaveChangesAsync();

      Console.WriteLine("Created task with id {0} and description: {1}", taskItem.Id, taskItem.Description);

      return TypedResults.Created($"/tasks/{taskItem.Id}", taskItem);
    });
  }

  public static async Task<IResult> Update(int id, TaskItem updatedTaskItem, TaskItemDb db)
  {
    return await WrapException(async () =>
    {
      var taskItemInDb = await db.TaskItems.FindAsync(id);

      if (taskItemInDb is null)
        return TypedResults.NotFound();

      var valResult = updatedTaskItem.Validate();
      if (valResult.Failed())
        return TypedResults.BadRequest(valResult);

      taskItemInDb.Description = updatedTaskItem.Description;
      taskItemInDb.IsDone = updatedTaskItem.IsDone;

      await db.SaveChangesAsync();

      Console.WriteLine("Updated task with id {0}", taskItemInDb.Id);

      return TypedResults.NoContent();
    });
  }

  public static async Task<IResult> Delete(int id, TaskItemDb db)
  {
    return await WrapException(async () =>
    {
      if (await db.TaskItems.FindAsync(id) is TaskItem taskItem)
      {
        db.TaskItems.Remove(taskItem);
        await db.SaveChangesAsync();

        Console.WriteLine("Deleted task with id {0} and description: {1}", taskItem.Id, taskItem.Description);

        return TypedResults.NoContent();
      }

      return TypedResults.NotFound();
    });
  }

  /// <summary>
  /// Utility to ensure all API calls handle and return exceptions as Result types.
  /// </summary>
  /// <remarks>
  /// Perhaps this is better handled by middleware.
  /// </remarks>
  private static async Task<IResult> WrapException(Func<Task<IResult>> func)
  {
    try
    {
      return await func();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(ex);
      return TypedResults.InternalServerError(Result.Error("An unexpected backend error occurred"));
    }
  }
}
