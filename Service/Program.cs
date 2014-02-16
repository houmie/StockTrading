using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {            
            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                host.Open();

                Console.WriteLine("The StockTrading Service is ready over" + Environment.NewLine + "{0}", host.BaseAddresses[0]);
                Console.WriteLine();
                Console.WriteLine("Press any key to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
