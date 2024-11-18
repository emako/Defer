#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

using System.Threading.Tasks;

namespace System.Defer;

/// <summary>
/// Represents an asynchronous operation that can be deferred with a specific value of type T.
/// Implements the IAsyncDeferable interface.
/// </summary>
/// <typeparam name="T">The type of the value passed to the deferred function.</typeparam>
public partial class AsyncDeferable<T> : IAsyncDeferable
{
    /// <summary>
    /// A function that accepts a value of type T and returns a ValueTask, representing the deferred asynchronous operation.
    /// </summary>
    protected readonly Func<T, ValueTask>? _func;

    /// <summary>
    /// The value of type T that will be used during the deferred operation.
    /// </summary>
    protected readonly T _deferValue;

    /// <summary>
    /// Initializes a new instance of the AsyncDeferable class, and immediately invokes the deferred function with the provided initValue.
    /// </summary>
    /// <param name="func">The asynchronous function that accepts a value of type T and returns a ValueTask.</param>
    /// <param name="initValue">The initial value passed to the function.</param>
    /// <param name="deferValue">The value that will be passed when the deferred function is invoked later.</param>
    public AsyncDeferable(Func<T, ValueTask> func, T initValue, T deferValue)
    {
        _func = func;
        _deferValue = deferValue;
        _func?.Invoke(initValue).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Initializes a new instance of the AsyncDeferable class, with the specified deferred value.
    /// </summary>
    /// <param name="func">The asynchronous function that accepts a value of type T and returns a ValueTask.</param>
    /// <param name="deferValue">The value that will be passed when the deferred function is invoked later.</param>
    public AsyncDeferable(Func<T, ValueTask> func, T deferValue)
    {
        _func = func;
        _deferValue = deferValue;
    }

    /// <summary>
    /// Initializes a new instance of the AsyncDeferable class with a function that does not require an initial value.
    /// </summary>
    /// <param name="func">The asynchronous function that accepts a value of type T and returns a ValueTask.</param>
    public AsyncDeferable(Func<T, ValueTask> func)
    {
        _func = func;

        // Default value of T is used when no defer value is specified.
        _deferValue = default!;
    }

    /// <summary>
    /// Asynchronously disposes of the deferred operation by invoking the provided function with the deferValue.
    /// </summary>
    public virtual async ValueTask DisposeAsync()
    {
        // Check if the function is not null before invoking it with the deferValue.
        if (_func != null)
        {
            await _func.Invoke(_deferValue);
        }
    }
}

/// <inheritdoc/>
public partial class AsyncDeferable<T>
{
    /// <summary>
    /// Creates and returns an instance of IAsyncDeferable with the provided asynchronous function, initial value, and deferred value.
    /// </summary>
    /// <param name="func">The asynchronous function that accepts a value of type T and returns a ValueTask.</param>
    /// <param name="initValue">The initial value passed to the function.</param>
    /// <param name="deferValue">The value that will be passed when the deferred function is invoked later.</param>
    /// <returns>An instance of IAsyncDeferable.</returns>
    public static IAsyncDeferable DeferAsync(Func<T, ValueTask> func, T initValue, T deferValue)
        => new AsyncDeferable<T>(func, initValue, deferValue);

    /// <summary>
    /// Creates and returns an instance of IAsyncDeferable with the provided asynchronous function and deferred value.
    /// </summary>
    /// <param name="func">The asynchronous function that accepts a value of type T and returns a ValueTask.</param>
    /// <param name="deferValue">The value that will be passed when the deferred function is invoked later.</param>
    /// <returns>An instance of IAsyncDeferable.</returns>
    public static IAsyncDeferable DeferAsync(Func<T, ValueTask> func, T deferValue)
        => new AsyncDeferable<T>(func, deferValue);

    /// <summary>
    /// Creates and returns an instance of IAsyncDeferable with the provided asynchronous function, using the default deferred value.
    /// </summary>
    /// <param name="func">The asynchronous function that accepts a value of type T and returns a ValueTask.</param>
    /// <returns>An instance of IAsyncDeferable.</returns>
    public static IAsyncDeferable DeferAsync(Func<T, ValueTask> func)
        => new AsyncDeferable<T>(func);
}

#endif
