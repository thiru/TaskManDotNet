namespace TaskManDotNet.Db;

using Microsoft.EntityFrameworkCore;
using TaskManDotNet.Core;

public class TaskItemDb : DbContext
{
  public TaskItemDb(DbContextOptions<TaskItemDb> opts) : base(opts) { }

  public DbSet<TaskItem> TaskItems => Set<TaskItem>();
}
