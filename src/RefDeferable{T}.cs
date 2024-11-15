#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
using System.Runtime.InteropServices;

namespace System.Defer;

public readonly ref struct RefDeferable<T> : IDeferable
{
    private readonly Span<T> _target;
    private readonly T _deferValue;

    public RefDeferable(ref T target, T initValue, T deferValue)
    {
        _deferValue = deferValue;
        _target = MemoryMarshal.CreateSpan(ref target, 1);
        _target[0] = initValue;
    }

    public RefDeferable(ref T target, T deferValue)
    {
        _deferValue = deferValue;
        _target = MemoryMarshal.CreateSpan(ref target, 1);
    }

    public void Dispose()
    {
        _target[0] = _deferValue;
    }

    public static RefDeferable<T> Defer(ref T target, T initValue, T deferValue)
        => new(ref target, initValue, deferValue);

    public static RefDeferable<T> Defer(ref T target, T deferValue)
        => new(ref target, deferValue);
}
#endif
