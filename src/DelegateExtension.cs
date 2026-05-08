namespace System.Defer;

/// <summary>
/// Extension helpers for converting objects and delegates into other types
/// used by the Defer library.
/// </summary>
public static class DelegateExtension
{
    /// <summary>
    /// Wraps an <see cref="Action"/> as an <see cref="IDeferable"/> by
    /// deferring its execution using <see cref="Deferable.Defer(Action)"/>.
    /// </summary>
    /// <param name="action">The action to defer.</param>
    /// <returns>An <see cref="IDeferable"/> that represents the deferred action.</returns>
    public static IDeferable ToDefer(this Action action)
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
    public static IDeferable ToDefer<T>(this Func<T> func)
    {
        return Deferable.Defer(() => func?.Invoke());
    }
}
