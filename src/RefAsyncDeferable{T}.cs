#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Defer;

/// <summary>
/// Represents an obsolete version of a deferred operation for a reference type T, where the target value is
/// modified and then restored to a deferred value asynchronously when disposed. This class has been superseded
/// by <see cref="RefDeferable{T}"/>. It uses the <see cref="IAsyncDeferable"/> interface to provide an
/// asynchronous dispose operation, but the new <see cref="RefDeferable{T}"/> is recommended for use.
/// </summary>
/// <typeparam name="T">The type of the value being deferred.</typeparam>
[Obsolete("Use RefDeferable instead")]
public readonly ref struct RefAsyncDeferable<T> : IAsyncDeferable
{
    /// <summary>
    /// A span that wraps the reference to the target value.
    /// </summary>
    private readonly Span<T> _target;

    /// <summary>
    /// The value to restore to the target upon disposal.
    /// </summary>
    private readonly T _deferValue;

    /// <summary>
    /// Initializes a new instance of <see cref="RefAsyncDeferable{T}"/> with the target value, initial value, and deferred value.
    /// The target value is set to the initial value.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="initValue">The initial value to assign to the target.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    public RefAsyncDeferable(ref T target, T initValue, T deferValue)
    {
        _deferValue = deferValue;
        _target = MemoryMarshal.CreateSpan(ref target, 1);
        _target[0] = initValue;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RefAsyncDeferable{T}"/> with the target value and deferred value.
    /// The target value is not modified initially.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    public RefAsyncDeferable(ref T target, T deferValue)
    {
        _deferValue = deferValue;

        // Creates a span wrapping the target reference.
        _target = MemoryMarshal.CreateSpan(ref target, 1);
    }

    /// <summary>
    /// Asynchronously disposes the instance, restoring the target value to the deferred value.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/> that completes when disposal is finished.</returns>
    public ValueTask DisposeAsync()
    {
        // Restore the target value to the deferred value upon disposal.
        _target[0] = _deferValue;

#if NET5_0_OR_GREATER
        // Return a completed task in .NET 5.0 or later.
        return ValueTask.CompletedTask;
#else
        // Return the default value for earlier versions of .NET.
        return default;
#endif
    }

    /// <summary>
    /// Creates and returns a new <see cref="RefAsyncDeferable{T}"/> instance with the target, initial value, and deferred value.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="initValue">The initial value to assign to the target.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    /// <returns>A new <see cref="RefAsyncDeferable{T}"/> instance.</returns>
    public static RefAsyncDeferable<T> DeferAsync(ref T target, T initValue, T deferValue)
        => new(ref target, initValue, deferValue);

    /// <summary>
    /// Creates and returns a new <see cref="RefAsyncDeferable{T}"/> instance with the target and deferred value.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    /// <returns>A new <see cref="RefAsyncDeferable{T}"/> instance.</returns>
    public static RefAsyncDeferable<T> DeferAsync(ref T target, T deferValue)
        => new(ref target, deferValue);
}

#endif
