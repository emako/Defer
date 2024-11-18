#if NETFRAMEWORK4_5_OR_GREATER

using System.Threading.Tasks;

namespace System.Defer;

/// <summary>
/// Provides asynchronous utility methods to execute actions and functions
/// with encapsulated try-catch-finally or try-finally logic.
/// </summary>
public static class AsyncDeferableExecutor
{
    /// <summary>
    /// Executes an asynchronous action with try-catch-finally logic.
    /// </summary>
    /// <param name="tryAction">The asynchronous action to execute.</param>
    /// <param name="catchAction">The asynchronous action to handle exceptions, optional.</param>
    /// <param name="finallyAction">The asynchronous action to execute regardless of success or failure, optional.</param>
    public static async Task ExecuteAsync(Func<Task> tryAction, Func<Exception, Task>? catchAction = null, Func<Task>? finallyAction = null)
    {
        try
        {
            if (tryAction != null)
            {
                await tryAction.Invoke();
            }
        }
        catch (Exception ex)
        {
            if (catchAction != null)
            {
                await catchAction(ex);
            }
            else
            {
                throw; // Re-throw the exception if no catchAction is provided.
            }
        }
        finally
        {
            if (finallyAction != null) await finallyAction();
        }
    }

    /// <summary>
    /// Executes an asynchronous function with try-catch-finally logic and returns a result.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="tryFunc">The asynchronous function to execute, returning a value of type T.</param>
    /// <param name="catchFunc">The asynchronous function to handle exceptions, returns a default value, optional.</param>
    /// <param name="finallyAction">The asynchronous action to execute regardless of success or failure, optional.</param>
    /// <returns>The result of the try block or the catch block if an exception occurs.</returns>
    public static async Task<T> ExecuteAsync<T>(Func<Task<T>> tryFunc, Func<Exception, Task<T>>? catchFunc = null, Func<Task>? finallyAction = null)
    {
        try
        {
            return tryFunc != null ? await tryFunc() : throw new ArgumentNullException(nameof(tryFunc));
        }
        catch (Exception ex)
        {
            if (catchFunc != null)
            {
                return await catchFunc(ex);
            }

            throw; // Re-throw the exception if no catchFunc is provided.
        }
        finally
        {
            if (finallyAction != null)
            {
                await finallyAction();
            }
        }
    }

    /// <summary>
    /// Executes an asynchronous action with try-finally logic (no exception handling).
    /// </summary>
    /// <param name="tryAction">The asynchronous action to execute.</param>
    /// <param name="finallyAction">The asynchronous action to execute regardless of success or failure.</param>
    public static async Task ExecuteWithoutCatchAsync(Func<Task> tryAction, Func<Task> finallyAction)
    {
        try
        {
            if (tryAction != null)
            {
                await tryAction();
            }
        }
        finally
        {
            if (finallyAction != null)
            {
                await finallyAction();
            }
        }
    }

    /// <summary>
    /// Executes an asynchronous function with try-finally logic (no exception handling) and returns a result.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="tryFunc">The asynchronous function to execute, returning a value of type T.</param>
    /// <param name="finallyAction">The asynchronous action to execute regardless of success or failure.</param>
    /// <returns>The result of the try block.</returns>
    public static async Task<T> ExecuteWithoutCatchAsync<T>(Func<Task<T>> tryFunc, Func<Task> finallyAction)
    {
        try
        {
            return tryFunc != null ? await tryFunc() : throw new ArgumentNullException(nameof(tryFunc));
        }
        finally
        {
            if (finallyAction != null)
            {
                await finallyAction();
            }
        }
    }
}

#endif
