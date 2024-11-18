#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

using System.Threading.Tasks;

namespace System.Defer;

/// <summary>
/// Represents an asynchronous disposable resource with support for deferred finalization and error handling.
/// This class extends <see cref="AsyncDeferable"/> and provides enhanced logic for cleanup,
/// including exception management and finalization.
/// </summary>
public partial class AsyncFinalizable(Func<ValueTask> func) : AsyncDeferable(func)
{
    /// <summary>
    /// Gets a value indicating whether the resource has already been disposed.
    /// </summary>
    public bool IsDisposed { get; protected set; }

    /// <summary>
    /// Gets the exception, if any, encountered during the asynchronous disposal process.
    /// </summary>
    public Exception? Error { get; protected set; }

    /// <summary>
    /// Finalizer for the <see cref="AsyncFinalizable"/> class.
    /// Ensures asynchronous cleanup logic is executed if the object is not properly disposed.
    /// </summary>
    ~AsyncFinalizable()
    {
        DisposeAsync(false).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <inheritdoc/>
    public override async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
    }

    /// <summary>
    /// Executes the asynchronous disposal logic, optionally distinguishing between
    /// managed and unmanaged cleanup.
    /// </summary>
    /// <param name="disposing">
    /// A value indicating whether the method is being called from <see cref="DisposeAsync"/> (true)
    /// or from the finalizer (false).
    /// </param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    /// <exception cref="Exception">
    /// If an error occurs during disposal and <paramref name="disposing"/> is true, the exception is propagated.
    /// </exception>
    protected async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing && Error is not null)
        {
            throw Error;
        }

        if (!IsDisposed)
        {
            try
            {
                await base.DisposeAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Error = e;
                if (disposing)
                {
                    throw;
                }
            }
            finally
            {
                IsDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}

/// <inheritdoc/>
public partial class AsyncFinalizable
{
    /// <summary>
    /// Creates an <see cref="AsyncFinalizable"/> instance that executes the specified <see cref="Func{ValueTask}"/> asynchronously on disposal.
    /// </summary>
    /// <param name="func">The asynchronous function to be executed during disposal.</param>
    /// <returns>An instance of <see cref="AsyncFinalizable"/> wrapping the provided function.</returns>
    public new static IAsyncDeferable DeferAsync(Func<ValueTask> func)
        => new AsyncFinalizable(func);
}

#endif
