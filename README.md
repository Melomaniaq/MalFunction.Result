# Malfunction.Result

# Getting Started
The result type is an interface that contains two records, Pass and Fail
```cs
  public interface IResult<TPass, TFail>
  {
      public sealed record Pass(TPass Value) : IResult<TPass, TFail>;

      public sealed record Fail(TFail Value) : IResult<TPass, TFail>;
  }
```

To define a result type, make a type alias. If using it across mulpiple files, define it globally in a seperate file
```cs
global using ValidationResult = MalFunction.Result.IResult<int, string>;
```

Set the return value of your method to your result and return a new pass or fail state
```cs
public ValidationResult IsGreaterThan5(int number)
{
    if (number > 5)
        return new ValidationResult.Pass(number);
    else
        return new ValidationResult.Fail("Number was not greater than 5");
}
```

```cs
string message = IsGreaterThan5(6) switch
{
    ValidationResult.Pass pass -> $"Number {pass.Value} was validated",
    ValidationResult.Fail fail -> $"Number failed validation with error: {fail.Value}"
};

Console.Writeline(message);
```

# Manipulating Results
For a detailed explenation of the concepts used in this project i recommend you read:
https://fsharpforfunandprofit.com/posts/elevated-world/
