namespace TaskManDotNet.Utils;

public record Result(int Level, string Message)
{
  public static Result Success(string message)
  {
    return new Result(0, message);
  }

  public static Result Warning(string message)
  {
    return new Result(-1, message);
  }

  public static Result Error(string message)
  {
    return new Result(-2, message);
  }

  public static Result Fatal(string message)
  {
    return new Result(-3, message);
  }

  public bool Succeeded()
  {
    return Level >= 0;
  }

  public bool Failed()
  {
    return Level < 0;
  }
}

public record Result<T>(int Level, string Message, T Data) : Result(Level, Message);
