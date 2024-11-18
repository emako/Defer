#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

using System.Threading.Tasks;

namespace System.Defer;

/// <summary>
/// Represents an asynchronous operation that can be deferred and disposed asynchronously.
/// Implements the IAsyncDeferable interface.
/// </summary>
public partial class AsyncDeferable(Func<ValueTask> func) : IAsyncDeferable
{
    /// <summary>
    /// A function that returns a ValueTask, representing the deferred asynchronous operation.
    /// </summary>
    protected readonly Func<ValueTask>? _func = func;

    /// <summary>
    /// Asynchronously disposes of the deferred operation by invoking the provided function, if it exists.
    /// </summary>
    public virtual async ValueTask DisposeAsync()
    {
        if (_func != null)
        {
            await _func.Invoke();
        }
    }
}

/// <inheritdoc/>
public partial class AsyncDeferable
{
    /// <summary>
    /// Creates and returns an instance of IAsyncDeferable with the provided asynchronous function.
    /// </summary>
    /// <param name="func">The asynchronous function to be deferred.</param>
    /// <returns>An instance of IAsyncDeferable.</returns>
    public static IAsyncDeferable DeferAsync(Func<ValueTask> func)
        => new AsyncDeferable(func);
}

#endif
