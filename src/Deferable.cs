namespace System.Defer;

public partial class Deferable(Action action) : IDeferable
{
    protected readonly Action? _action = action;

    public void Dispose()
    {
        _action?.Invoke();
    }
}

public partial class Deferable
{
    public static IDeferable Defer(Action action)
        => new Deferable(action);
}
