namespace System.Defer;

public partial class Deferable<T> : IDeferable
{
    protected readonly Action<T>? _action;
    protected readonly T _deferValue;

    public Deferable(Action<T> action, T initValue, T deferValue)
    {
        _action = action;
        _deferValue = deferValue;
        _action?.Invoke(initValue);
    }

    public Deferable(Action<T> action, T deferValue)
    {
        _action = action;
        _deferValue = deferValue;
    }

    public Deferable(Action<T> action)
    {
        _action = action;
        _deferValue = default!;
    }

    public void Dispose()
    {
        _action?.Invoke(_deferValue);
    }
}

public partial class Deferable<T>
{
    public static IDeferable Defer(Action<T> action, T initValue, T deferValue)
        => new Deferable<T>(action, initValue, deferValue);

    public static IDeferable Defer(Action<T> action, T deferValue)
        => new Deferable<T>(action, deferValue);

    public static IDeferable Defer(Action<T> action)
        => new Deferable<T>(action);
}
