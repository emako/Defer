#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Defer;

[Obsolete("Use RefDeferable instead")]
public readonly ref struct RefAsyncDeferable<T> : IAsyncDeferable
{
    private readonly Span<T> _target;
    private readonly T _deferValue;

    public RefAsyncDeferable(ref T target, T initValue, T deferValue)
    {
        _deferValue = deferValue;
        _target = MemoryMarshal.CreateSpan(ref target, 1);
        _target[0] = initValue;
    }

    public RefAsyncDeferable(ref T target, T deferValue)
    {
        _deferValue = deferValue;
        _target = MemoryMarshal.CreateSpan(ref target, 1);
    }

    public ValueTask DisposeAsync()
    {
        _target[0] = _deferValue;

#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
        return default;
#endif
    }

    public static RefAsyncDeferable<T> DeferAsync(ref T target, T initValue, T deferValue)
        => new(ref target, initValue, deferValue);

    public static RefAsyncDeferable<T> DeferAsync(ref T target, T deferValue)
        => new(ref target, deferValue);
}
#endif
