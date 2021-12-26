using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncExample
{
    class Program
    {
        private static async Task<string> DelayedMethodThatSucceds()
        {
            await Task.Delay(4000);
            Console.WriteLine("Working delayedMethodThatSucceds");
            return "Success delayedMethodThatSucceds";
        }

        private static async Task<string> DelayedMethodThatFails()
        {
            await Task.Delay(2000);
            Console.WriteLine("Working delayedMethodThatFails");
            throw new Exception("This is an exception on delayedMethodThatFails");
            //return "Success delayedMethodThatFails";
        }


        private static async Task MethodHandlingOneTask()
        {
            Console.WriteLine("Starting MethodHandlingOneTask");

            Task<string> DelayedMethodThatFailsTask = DelayedMethodThatFails();

            try
            {
                string data = await DelayedMethodThatFailsTask;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            Console.WriteLine("Ending MethodHandlingOneTask");
        }

        private static void MethodHandlingMultipleTasks()
        {
            Console.WriteLine("Starting MethodHandlingMultipleTasks");

            List<Task<string>> allResults = new List<Task<string>>();
            allResults.Add(DelayedMethodThatSucceds());
            allResults.Add(DelayedMethodThatFails());

            try
            {
                Task.WaitAll(allResults.ToArray());
            }
            catch (AggregateException exceptions)
            {
                foreach (var ex in exceptions.InnerExceptions)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }

            foreach (var result in allResults)
            {
                if (result.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("status: {0}, result: {1}",
                    result.Status, result.Result, result.Exception?.Message);
                }
                else
                {
                    Console.WriteLine("status: {0}, reason: {1}",
                    result.Status, result.Exception?.Message);
                }
            }


            Console.WriteLine("Ending MethodHandlingMultipleTasks");
        }


        static void Main(string[] args)
        {
            //MethodHandlingOneTask().GetAwaiter().GetResult();
            MethodHandlingMultipleTasks();
        }
    }
}
