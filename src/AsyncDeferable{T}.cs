#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
using System.Threading.Tasks;

namespace System.Defer;

public partial class AsyncDeferable<T> : IAsyncDeferable
{
    protected readonly Func<T, ValueTask>? _func;
    protected readonly T _deferValue;

    public AsyncDeferable(Func<T, ValueTask> func, T initValue, T deferValue)
    {
        _func = func;
        _deferValue = deferValue;
        _func?.Invoke(initValue).GetAwaiter().GetResult();
    }

    public AsyncDeferable(Func<T, ValueTask> func, T deferValue)
    {
        _func = func;
        _deferValue = deferValue;
    }

    public async ValueTask DisposeAsync()
    {
        if (_func != null)
        {
            await _func.Invoke(_deferValue);
        }
    }
}

public partial class AsyncDeferable<T>
{
    public static IAsyncDeferable DeferAsync(Func<T, ValueTask> func, T initValue, T deferValue)
        => new AsyncDeferable<T>(func, initValue, deferValue);

    public static IAsyncDeferable DeferAsync(Func<T, ValueTask> func, T deferValue)
        => new AsyncDeferable<T>(func, deferValue);
}
#endif
