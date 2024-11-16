#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
namespace System.Defer;

public interface IAsyncDeferable : IAsyncDisposable;
#endif
