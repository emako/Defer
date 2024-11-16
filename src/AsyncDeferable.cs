#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
using System.Threading.Tasks;

namespace System.Defer;

public partial class AsyncDeferable(Func<ValueTask> func) : IAsyncDeferable
{
    protected readonly Func<ValueTask>? func = func;

    public async ValueTask DisposeAsync()
    {
        if (func != null)
        {
            await func.Invoke();
        }
    }
}

public partial class AsyncDeferable
{
    public static IAsyncDeferable DeferAsync(Func<ValueTask> func)
        => new AsyncDeferable(func);
}
#endif
