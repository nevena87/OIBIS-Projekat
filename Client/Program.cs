using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            Console.WriteLine(name);

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:4001/DataBaseService";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            Console.WriteLine("Korisnik koji je pokrenuo klijenta je: " + WindowsIdentity.GetCurrent().Name);

            using (ClientProxy proxy = new ClientProxy(binding, new EndpointAddress(new Uri(address))))
            {
                proxy.Ispisi("Poruka poslata sa klijenta.");
            }

            Console.ReadLine();
        }
    }
}
