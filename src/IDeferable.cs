namespace System.Defer;

/// <summary>
/// Represents an interface for a deferred operation that can be disposed synchronously.
/// This interface extends <see cref="IDisposable"/>, indicating that implementing classes
/// can be disposed synchronously, typically to release resources.
/// </summary>
public interface IDeferable : IDisposable;
