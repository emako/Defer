namespace System.Defer;

/// <summary>
/// Represents a disposable resource that supports deferred finalization logic.
/// This interface extends <see cref="IDeferable"/> to allow resources to
/// implement custom behavior that occurs during finalization or cleanup.
/// </summary>
public interface IFinalizable : IDeferable;
