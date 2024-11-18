#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER

namespace System.Defer;

/// <summary>
/// Represents an interface for asynchronous resources that support finalization.
/// Extends <see cref="IAsyncDeferable"/> to include a contract for objects requiring asynchronous cleanup
/// with additional handling for finalization scenarios.
/// </summary>
internal interface IAsyncFinalizable : IAsyncDeferable;

#endif
