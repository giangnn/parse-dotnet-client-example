using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParseDotnet
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            ParseClient.Initialize(new ParseClient.Configuration
                {
                    ApplicationId = "myAppId",
                    Server = "http://localhost:1337/parse/", // trailing slash is important!
                WindowsKey = "myDotnetKey"
                });

            CreateOrder(1, 1);
            CreateOrder(2, 2);

            Console.ReadLine();
        }

        static void CreateOrder(int userId, int min)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var order = new ParseObject("Order");
                    order["quantity"] = rand.NextDouble() + min;
                    order["userId"] = userId;
                    await order.SaveAsync();
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
