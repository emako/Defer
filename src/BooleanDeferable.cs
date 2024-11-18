namespace System.Defer;

/// <summary>
/// Represents an asynchronous operation that can be deferred, specifically designed for boolean values.
/// Inherits from the generic Deferable class with type parameter <c>bool</c>.
/// </summary>
public partial class BooleanDeferable : Deferable<bool>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanDeferable"/> class with an action,
    /// an initial value, and a deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a boolean value.</param>
    /// <param name="initValue">The initial boolean value passed to the action (default is <c>true</c>).</param>
    /// <param name="deferValue">The boolean value passed to the action when it is deferred (default is <c>false</c>).</param>

    public BooleanDeferable(Action<bool> action, bool initValue = true, bool deferValue = false)
        : base(action, initValue, deferValue)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanDeferable"/> class with an action and a deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a boolean value.</param>
    /// <param name="deferValue">The boolean value passed to the action when it is deferred (default is <c>false</c>).</param>
    public BooleanDeferable(Action<bool> action, bool deferValue = false)
        : base(action, deferValue)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanDeferable"/> class with an action and a default deferred value of <c>false</c>.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a boolean value.</param>
    public BooleanDeferable(Action<bool> action)
        : base(action, false)
    {
    }
}

/// <inheritdoc />
public partial class BooleanDeferable
{
    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action, initial value, and deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a boolean value.</param>
    /// <param name="initValue">The initial boolean value passed to the action (default is <c>true</c>).</param>
    /// <param name="deferValue">The boolean value passed to the action when it is deferred (default is <c>false</c>).</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public new static IDeferable Defer(Action<bool> action, bool initValue = true, bool deferValue = false)
        => new BooleanDeferable(action, initValue, deferValue);

    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action and a deferred value.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a boolean value.</param>
    /// <param name="deferValue">The boolean value passed to the action when it is deferred (default is <c>false</c>).</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public new static IDeferable Defer(Action<bool> action, bool deferValue = false)
        => new BooleanDeferable(action, deferValue);

    /// <summary>
    /// Creates and returns an instance of <see cref="IDeferable"/> with the provided action and a default deferred value of <c>false</c>.
    /// </summary>
    /// <param name="action">The action that will be deferred, which accepts a boolean value.</param>
    /// <returns>An instance of <see cref="IDeferable"/>.</returns>
    public new static IDeferable Defer(Action<bool> action)
        => new BooleanDeferable(action);
}
