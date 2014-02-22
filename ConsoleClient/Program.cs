using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleClient.ServiceReference1;

namespace ConsoleClient
{
    class Program : IServiceCallback
    {
        static void Main(string[] args)
        {
            InstanceContext site = new InstanceContext(null, new Program());
            ServiceClient client = new ServiceClient(site);
            //Subscribe.
            Console.WriteLine("Subscribing");
            client.Subscribe();

            Console.WriteLine();
            
            Console.WriteLine("Press ENTER to unsubscribe and shut down client");
            Console.ReadLine();

            Console.WriteLine("Unsubscribing");
            client.Unsubscribe();

            //Closing the client gracefully closes the connection and cleans up resources
            client.Close();
        }



        public void PriceChange(Model[] quotes)
        {
            for (int i = 0; i < quotes.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Symbol : {0}", quotes[i].Symbol);
                Console.WriteLine("Name : {0}", quotes[i].Name);
                Console.WriteLine("LastTradePrice: {0}", quotes[i].LastTradePrice);
                Console.WriteLine("LastTradeDate: {0}", quotes[i].LastTradeDate);
                Console.WriteLine("OpenPrice: {0}", quotes[i].OpenPrice);
                Console.WriteLine("DayHighPrice: {0}", quotes[i].DayHighPrice);
                Console.WriteLine("DayLowPrice : {0}", quotes[i].DayLowPrice);
                Console.WriteLine();
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
