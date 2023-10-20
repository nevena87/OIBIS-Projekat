using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "net.tcp://localhost:4001/IDataBaseManagement";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<IDataBaseManagement> channel = new ChannelFactory<IDataBaseManagement>(binding, address);
            IDataBaseManagement proxy = channel.CreateChannel();

            string message = "Ovo je poruka sa klijenta.";
            proxy.Ispisi(message); 

            Console.WriteLine("Poruka poslata na server: " + message);

            Console.ReadKey();
        }
    }
}
