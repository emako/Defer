#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

namespace System.Defer;

/// <summary>
/// Represents an interface for an asynchronous deferred operation that can be disposed asynchronously.
/// This interface extends <see cref="IAsyncDisposable"/>, indicating that implementing classes
/// can be disposed asynchronously, typically to release asynchronous resources.
/// </summary>
public interface IAsyncDeferable : IAsyncDisposable;

#endif
