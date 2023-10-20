using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak26
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DataBaseService)))
            {
                string address = "net.tcp://localhost:4001/IDataBaseManagement";
                NetTcpBinding binding = new NetTcpBinding();

                host.AddServiceEndpoint(typeof(IDataBaseManagement), binding, address);

                host.Open();
                Console.WriteLine("Servis je uspesno pokrenut");
                Console.ReadKey();
            }
        }
    }
}
