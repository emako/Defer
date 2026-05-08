namespace System.Defer;

/// <summary>
/// Extension helpers for converting objects and delegates into other types
/// used by the Defer library.
/// </summary>
public static class AsExtension
{
    /// <summary>
    /// Attempts to cast <paramref name="obj"/> to <typeparamref name="T"/>.
    /// Returns the cast value when successful; otherwise returns the default
    /// value for <typeparamref name="T"/> (which is <c>null</c> for reference
    /// types and nullable value types).
    /// </summary>
    /// <typeparam name="T">The target type to cast to.</typeparam>
    /// <param name="obj">The object to attempt to cast.</param>
    /// <returns>The object cast to <typeparamref name="T"/>, or the default
    /// value of <typeparamref name="T"/> if the cast fails.</returns>
    public static T? As<T>(this object? obj)
    {
        if (obj is T t)
            return t;

        return default;
    }

    /// <summary>
    /// Wraps an <see cref="Action"/> as an <see cref="IDeferable"/> by
    /// deferring its execution using <see cref="Deferable.Defer(Action)"/>.
    /// </summary>
    /// <param name="action">The action to defer.</param>
    /// <returns>An <see cref="IDeferable"/> that represents the deferred action.</returns>
    public static IDeferable As(this Action action)
    {
        return Deferable.Defer(action);
    }

    /// <summary>
    /// Wraps a <see cref="Func{T}"/> as an <see cref="IDeferable"/> by
    /// deferring its invocation. The returned <see cref="IDeferable"/> will
    /// execute the function when run; any return value from the function is
    /// ignored by the defer wrapper.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="func">The function to defer.</param>
    /// <returns>An <see cref="IDeferable"/> that represents the deferred function.</returns>
    public static IDeferable As<T>(this Func<T> func)
    {
        return Deferable.Defer(() => func?.Invoke());
    }
}
