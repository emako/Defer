# Defer

This repository provides `IDeferable` to emulate Golang's `defer` keyword in C#.

## Installing

[![NuGet](https://img.shields.io/nuget/v/Defer.svg)](https://nuget.org/packages/Defer)

The package is available [on NuGet](https://www.nuget.org/packages/Defer). To install, run:

```bash
dotnet add package Defer
```

## Build Status

[![Actions](https://github.com/emako/Defer/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/emako/Defer/actions/workflows/library.nuget.yml)

## Usage

1. Usage of `Deferable`

```c#
// Deferable.Defer
Console.WriteLine("Hello, World!");
using (Deferable.Defer(() => Console.WriteLine("Goodbye, World!")))
{
    Console.WriteLine("Do something");
}

// output:
// Hello, World!
// Do something
// Goodbye, World!
```

2. Usage of `Deferable<T>`

```c#
// Deferable<T>
int status = -1;

Console.WriteLine("init status: " + status);
using (Deferable<int>.Defer(value => status = value, initValue: 1, deferValue: 0))
{
    Console.WriteLine("doing something status: " + status);
}
Console.WriteLine("after defer status: " + status);

// output:
// init status: -1
// doing something status: 1
// after defer status: 0
```

3. Usage of `BooleanDeferable`

```c#
// BooleanDeferable
bool flag = default;

Console.WriteLine("init flag: " + flag);
using (BooleanDeferable.Defer(value => flag = value))
{
    Console.WriteLine("doing something flag: " + flag);
}
Console.WriteLine("after defer flag: " + flag);

// output:
// init flag: False
// doing something flag: True
// after defer flag: False
```

4. Usage of `RefDeferable<T>`

```c#
// RefDeferable<T>
// Only applicable for .NET Standard 2.1, .NET Core 3.0, or later versions
double value = -1d;

Console.WriteLine("init value: " + value);
using (RefDeferable<double>.Defer(ref value, 0d, 1d))
{
    Console.WriteLine("doing something value: " + value);
}
Console.WriteLine("after defer value: " + value);

// output:
// init value: -1
// doing something value: 0
// after defer value: 1
```

## Comparison

> Comparison with Go's `defer`

In Go, `defer` is a keyword used to ensure that a function call is performed later in a program's execution, usually for purposes of cleanup.

```go
fmt.Println("Hello, World!")
defer fmt.Println("Goodbye, World!")
fmt.Println("Do something")
```

In C#, using this library, you can achieve similar behavior with `Deferable.Defer` and the `using` statement.

```csharp
Console.WriteLine("Hello, World!");
using IDeferable defer = Deferable.Defer(() => Console.WriteLine("Goodbye, World!"));
Console.WriteLine("Do something");
```

Both snippets output:

```text
Hello, World!
Do something
Goodbye, World!
```

## References

https://blog.coldwind.top/posts/mimic-go-defer-in-csharp/

