namespace System.Defer;

/// <summary>
/// Represents a disposable resource with support for deferred finalization and error handling.
/// This class extends <see cref="Deferable"/> and implements <see cref="IFinalizable"/> to
/// add enhanced cleanup logic and exception management during disposal.
/// </summary>
public partial class Finalizable(Action action) : Deferable(action), IFinalizable
{
    /// <summary>
    /// Gets a value indicating whether the resource has already been disposed.
    /// </summary>
    public bool IsDisposed { get; protected set; }

    /// <summary>
    /// Gets the exception, if any, encountered during the disposal process.
    /// </summary>
    public Exception? Error { get; protected set; }

    /// <summary>
    /// Finalizer for the <see cref="Finalizable"/> class.
    /// Ensures unmanaged resources or deferred cleanup are performed if the object is not properly disposed.
    /// </summary>
    ~Finalizable()
    {
        Dispose(false);
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        Dispose(true);

        // Suppress finalization to avoid double cleanup.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Executes the disposal logic, optionally distinguishing between managed and unmanaged cleanup.
    /// </summary>
    /// <param name="disposing">
    /// A value indicating whether the method is being called from <see cref="Dispose"/> (true)
    /// or from the finalizer (false).
    /// </param>
    /// <exception cref="Exception">
    /// If an error occurs during disposal and <paramref name="disposing"/> is true, the exception is propagated.
    /// </exception>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && Error is not null)
        {
            throw Error;
        }

        if (!IsDisposed)
        {
            try
            {
                base.Dispose();
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

                // Ensure finalization is suppressed even if disposal is incomplete.
                GC.SuppressFinalize(this);
            }
        }
    }
}

/// <inheritdoc />
public partial class Finalizable
{
    /// <summary>
    /// Creates a <see cref="Finalizable"/> instance that executes the specified <see cref="Action"/> on disposal.
    /// </summary>
    /// <param name="action">The action to be executed during disposal.</param>
    /// <returns>An instance of <see cref="Finalizable"/> wrapping the provided action.</returns>
    public new static IDeferable Defer(Action action)
        => new Finalizable(action);
}
