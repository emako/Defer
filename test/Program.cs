using System.Defer;

namespace DeferTester;

internal class Program
{
    static void Main()
    {
        {
            Console.WriteLine("Hello, World!");
            using (Deferable.Defer(() => Console.WriteLine("Goodbye, World!")))
            {
                Console.WriteLine("Do something");
            }
        }

        Console.Write(Environment.NewLine);

        {
            int status = -1;

            Console.WriteLine("init status: " + status);
            using (Deferable<int>.Defer(value => status = value, initValue: 1, deferValue: 0))
            {
                Console.WriteLine("doing something status: " + status);
            }
            Console.WriteLine("after defer status: " + status);
        }

        Console.Write(Environment.NewLine);

        {
            bool flag = default;

            Console.WriteLine("init flag: " + flag);
            using (BooleanDeferable.Defer(value => flag = value))
            {
                Console.WriteLine("doing something flag: " + flag);
            }
            Console.WriteLine("after defer flag: " + flag);
        }

        Console.Write(Environment.NewLine);

        {
            double value = -1d;

            Console.WriteLine("init value: " + value);
            using (RefDeferable<double>.Defer(ref value, 0d, 1d))
            {
                Console.WriteLine("doing something value: " + value);
            }
            Console.WriteLine("after defer value: " + value);
        }

        Console.Write(Environment.NewLine);

        Task.Run(async () =>
        {
            {
                Console.WriteLine("Hello, World!");
                await using (AsyncDeferable.DeferAsync(async () => Console.WriteLine(await Task.FromResult("Goodbye, World!"))))
                {
                    Console.WriteLine("Do something");
                }
            }

            Console.Write(Environment.NewLine);

            {
                int status = -1;

                Console.WriteLine("init status: " + status);
                await using (AsyncDeferable<int>.DeferAsync(async value => status = await Task.FromResult(value), initValue: 1, deferValue: 0))
                {
                    Console.WriteLine("doing something status: " + status);
                }
                Console.WriteLine("after defer status: " + status);
            }
        });

        Console.ReadLine();
    }
}
