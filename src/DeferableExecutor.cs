namespace System.Defer;

/// <summary>
/// Provides utility methods to execute actions and functions
/// with encapsulated try-catch-finally or try-finally logic.
/// </summary>
public static class DeferableExecutor
{
    /// <summary>
    /// Encapsulates a try-catch-finally logic for actions without return values.
    /// </summary>
    /// <param name="tryAction">The main action to execute.</param>
    /// <param name="catchAction">The action to handle exceptions, optional.</param>
    /// <param name="finallyAction">The action to execute regardless of success or failure, optional.</param>
    public static void Execute(Action tryAction, Action<Exception>? catchAction = null, Action? finallyAction = null)
    {
        try
        {
            tryAction?.Invoke();
        }
        catch (Exception ex)
        {
            catchAction?.Invoke(ex);
        }
        finally
        {
            finallyAction?.Invoke();
        }
    }

    /// <summary>
    /// Encapsulates a try-catch-finally logic for functions with return values.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="tryFunc">The main function to execute, returning a value of type T.</param>
    /// <param name="catchFunc">The function to handle exceptions, returns a default value, optional.</param>
    /// <param name="finallyAction">The action to execute regardless of success or failure, optional.</param>
    /// <returns>The result of the try block or the catch block if an exception occurs.</returns>
    public static T Execute<T>(Func<T> tryFunc, Func<Exception, T>? catchFunc = null, Action? finallyAction = null)
    {
        try
        {
            return tryFunc.Invoke();
        }
        catch (Exception ex)
        {
            if (catchFunc != null)
            {
                return catchFunc.Invoke(ex);
            }

            throw; // Re-throw the exception if no catchFunc is provided.
        }
        finally
        {
            finallyAction?.Invoke();
        }
    }

    /// <summary>
    /// Encapsulates a try-finally logic for actions without return values.
    /// </summary>
    /// <param name="tryAction">The main action to execute.</param>
    /// <param name="finallyAction">The action to execute regardless of success or failure.</param>
    public static void ExecuteWithoutCatch(Action tryAction, Action finallyAction)
    {
        try
        {
            tryAction?.Invoke();
        }
        finally
        {
            finallyAction?.Invoke();
        }
    }

    /// <summary>
    /// Encapsulates a try-finally logic for functions with return values.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="tryFunc">The main function to execute, returning a value of type T.</param>
    /// <param name="finallyAction">The action to execute regardless of success or failure.</param>
    /// <returns>The result of the try block.</returns>
    public static T ExecuteWithoutCatch<T>(Func<T> tryFunc, Action finallyAction)
    {
        try
        {
            return tryFunc();
        }
        finally
        {
            finallyAction?.Invoke();
        }
    }
}
