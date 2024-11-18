namespace System.Defer;

/// <summary>
/// Represents a deferred operation that can be executed later with no parameters.
/// Implements the <see cref="IDeferable"/> interface.
/// </summary>
public partial class Deferable(Action action) : IDeferable
{
    /// <summary>
    /// A function (Action) that will be deferred and invoked later.
    /// </summary>
    protected readonly Action? _action = action;

    /// <summary>
    /// Executes the deferred action when disposed.
    /// This method is part of the IDisposable pattern.
    /// </summary>
    public virtual void Dispose()
    {
        _action?.Invoke();
    }
}

/// <inheritdoc />
public partial class Deferable
{
    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action.
    /// The action will be executed when the instance is disposed.
    /// </summary>
    /// <param name="action">The action to be deferred and executed later.</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public static IDeferable Defer(Action action)
        => new Deferable(action);
}
