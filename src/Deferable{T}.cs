namespace System.Defer;

/// <summary>
/// Represents a deferred operation that can be executed later with a specific type parameter.
/// Implements the <see cref="IDeferable"/> interface.
/// </summary>
/// <typeparam name="T">The type parameter that will be used for the deferred operation.</typeparam>
public partial class Deferable<T> : IDeferable
{
    /// <summary>
    /// A function (Action) that will be deferred and invoked later, accepting a parameter of type T.
    /// </summary>
    protected readonly Action<T>? _action;

    /// <summary>
    /// The value that will be passed to the action when it is deferred.
    /// </summary>
    protected readonly T _deferValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="Deferable{T}"/> class with an action, an initial value, and a deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a parameter of type T.</param>
    /// <param name="initValue">The initial value of type T passed to the action when the instance is created.</param>
    /// <param name="deferValue">The value of type T passed to the action when it is deferred (default is the value passed to the constructor).</param>
    public Deferable(Action<T> action, T initValue, T deferValue)
    {
        _action = action;
        _deferValue = deferValue;

        // Invoke the action immediately with the initial value.
        _action?.Invoke(initValue);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Deferable{T}"/> class with an action and a deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a parameter of type T.</param>
    /// <param name="deferValue">The value of type T passed to the action when it is deferred (default is the value passed to the constructor).</param>
    public Deferable(Action<T> action, T deferValue)
    {
        _action = action;
        _deferValue = deferValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Deferable{T}"/> class with an action and a default deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a parameter of type T.</param>
    public Deferable(Action<T> action)
    {
        _action = action;

        // Default value of type T (must be specified by the caller).
        _deferValue = default!;
    }

    /// <summary>
    /// Executes the deferred action when disposed.
    /// This method is part of the IDisposable pattern and invokes the action with the deferred value.
    /// </summary>
    public void Dispose()
    {
        _action?.Invoke(_deferValue);
    }
}

/// <inheritdoc />
public partial class Deferable<T>
{
    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action, initial value, and deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a parameter of type T.</param>
    /// <param name="initValue">The initial value of type T passed to the action.</param>
    /// <param name="deferValue">The value of type T passed to the action when it is deferred.</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public static IDeferable Defer(Action<T> action, T initValue, T deferValue)
        => new Deferable<T>(action, initValue, deferValue);

    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action and a deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a parameter of type T.</param>
    /// <param name="deferValue">The value of type T passed to the action when it is deferred.</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public static IDeferable Defer(Action<T> action, T deferValue)
        => new Deferable<T>(action, deferValue);

    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action and a default deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a parameter of type T.</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public static IDeferable Defer(Action<T> action)
        => new Deferable<T>(action);
}
