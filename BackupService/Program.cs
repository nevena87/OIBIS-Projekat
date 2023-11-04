using CertificateManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupService
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:4002/Backup"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (Backup proxy = new Backup(binding, address))
            {
                /*while (true)
                {
                    
                    //Thread.Sleep(30000);
                }*/
                Console.WriteLine("Backup service je pokrenut.");
                Console.ReadLine();
            }
        }
    }
}
