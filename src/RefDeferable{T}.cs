#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

using System.Runtime.InteropServices;

namespace System.Defer;

/// <summary>
/// Represents a deferred operation for a reference type T, where the target value is modified and then restored
/// to a deferred value when disposed. The struct is a read-only reference struct, allowing for efficient memory
/// usage by manipulating the reference directly without allocations.
/// </summary>
/// <typeparam name="T">The type of the value being deferred.</typeparam>
public readonly ref struct RefDeferable<T> : IDeferable
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
    /// Initializes a new instance of <see cref="RefDeferable{T}"/> with the target value, initial value, and deferred value.
    /// The target value is set to the initial value.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="initValue">The initial value to assign to the target.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    public RefDeferable(ref T target, T initValue, T deferValue)
    {
        _deferValue = deferValue;

        // Creates a span wrapping the target reference.
        _target = MemoryMarshal.CreateSpan(ref target, 1);

        // Set the target value to the initial value.
        _target[0] = initValue;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RefDeferable{T}"/> with the target value and deferred value.
    /// The target value is not modified initially.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    public RefDeferable(ref T target, T deferValue)
    {
        _deferValue = deferValue;

        // Creates a span wrapping the target reference.
        _target = MemoryMarshal.CreateSpan(ref target, 1);
    }

    /// <summary>
    /// Disposes the instance, restoring the target value to the deferred value.
    /// </summary>
    public void Dispose()
    {
        // Restore the target value to the deferred value upon disposal.
        _target[0] = _deferValue;
    }

    /// <summary>
    /// Creates and returns a new <see cref="RefDeferable{T}"/> instance with the target, initial value, and deferred value.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="initValue">The initial value to assign to the target.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    /// <returns>A new <see cref="RefDeferable{T}"/> instance.</returns>
    public static RefDeferable<T> Defer(ref T target, T initValue, T deferValue)
        => new(ref target, initValue, deferValue);

    /// <summary>
    /// Creates and returns a new <see cref="RefDeferable{T}"/> instance with the target and deferred value.
    /// </summary>
    /// <param name="target">The reference to the target value to be deferred.</param>
    /// <param name="deferValue">The value to restore to the target upon disposal.</param>
    /// <returns>A new <see cref="RefDeferable{T}"/> instance.</returns>
    public static RefDeferable<T> Defer(ref T target, T deferValue)
        => new(ref target, deferValue);
}

#endif
