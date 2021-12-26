using System;
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

        private static async Task MainAsync()
        {
            await MethodHandlingOneTask();
        }

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
    }
}
