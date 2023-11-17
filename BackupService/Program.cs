using CertificateManager;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                List<DataBaseEntry> entryList = new List<DataBaseEntry>();
                while (true)
                {
                    Thread.Sleep(30000);
                    entryList = proxy.PullDatabase();
                    SaveDatabase(entryList);
                }

                Console.WriteLine("Backup service je pokrenut.");
                Console.ReadLine();
            }
        }

        private static void SaveDatabase(List<DataBaseEntry> entryList)
        {
            string databasePath = @"..\..\BackupDatabase.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));

            using (var stream = File.Create(databasePath))
            {
                serializer.Serialize(stream, entryList);
                Console.WriteLine("Backup saved.");
            }
        }
    }
}
