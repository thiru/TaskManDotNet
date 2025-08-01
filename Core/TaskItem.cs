namespace TaskManDotNet.Core;

using TaskManDotNet.Utils;

public class TaskItem
{
  public int Id { get; set; }
  public string? Description { get; set; }
  public bool IsDone { get; set; }

  public Result Validate()
  {
    if (Id < 0)
      return Result.Error("Id should be a non-negative integer");
    else if (string.IsNullOrWhiteSpace(Description))
      return Result.Error("Description should not be blank");

    return Result.Success("Task is valid");
  }
}
