using System;
using Nancy.Hosting.Self;

namespace CFGarage
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Server started !");
            using (NancyHost host = new NancyHost(new Uri("http://localhost:47114")))
            {
                host.Start();
                while (true)
                {
                    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
                        break;
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
