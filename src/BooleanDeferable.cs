namespace System.Defer;

public partial class BooleanDeferable : Deferable<bool>
{
    public BooleanDeferable(Action<bool> action, bool initValue = true, bool deferValue = false)
        : base(action, initValue, deferValue)
    {
    }

    public BooleanDeferable(Action<bool> action, bool deferValue = false)
        : base(action, deferValue)
    {
    }
}

public partial class BooleanDeferable
{
    public new static IDeferable Defer(Action<bool> action, bool initValue = true, bool deferValue = false)
        => new Deferable<bool>(action, initValue, deferValue);
}
