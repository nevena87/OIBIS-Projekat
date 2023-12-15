using CertificateManager;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading;
using System.Xml.Serialization;

namespace BackupService
{
    class Program
    {
        public static SecretMasks sm = new SecretMasks();
        public static AES aes = new AES();

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
                List<DatabaseEntry> entryList = new List<DatabaseEntry>();
                while (true)
                {
                    Thread.Sleep(10000);

                    (List<byte[]>, byte[], byte[]) results = proxy.PullDatabase();
                    //List<DatabaseEntry> entryList = new List<DatabaseEntry>();
                    List<byte[]> templList = new List<byte[]>();

                    templList = results.Item1;  // Kriptovana lista
                    byte[] yourKey = results.Item2; // Kriptovan kljuc
                    byte[] yourIV = results.Item3;  // Kriptovan IV vektor

                    // Dekriptovanj kljuca i IV
                    (byte[], byte[]) retV = sm.KeyAndIVEncryption(yourKey, yourIV);
                    byte[] decryptedyourKey = retV.Item1;
                    byte[] decrypteyourIV = retV.Item2;

                    DatabaseEntry entryPOM = new DatabaseEntry();

                    // Dekripotavnje podataka
                    try
                    {
                        foreach (var item in templList)
                        {
                            string decryptedString = aes.DecryptBytesToString_Aes(item, decryptedyourKey, decrypteyourIV);


                            string[] pom = decryptedString.Split(' ');


                            entryPOM.Id = int.Parse(pom[0]);
                            entryPOM.Region = pom[1];
                            entryPOM.City = pom[2];
                            entryPOM.Year = pom[3];
                            entryPOM.Consumption = new double[]
                                     {
                                    ParseDouble(pom, 4),
                                    ParseDouble(pom, 5),
                                    ParseDouble(pom, 6),
                                    ParseDouble(pom, 7),
                                    ParseDouble(pom, 8),
                                    ParseDouble(pom, 9),
                                    ParseDouble(pom, 10),
                                    ParseDouble(pom, 11),
                                    ParseDouble(pom, 12),
                                    ParseDouble(pom, 13),
                                    ParseDouble(pom, 14),
                                    ParseDouble(pom, 15)
                                     };
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    List<DatabaseEntry> listOfEntities = new List<DatabaseEntry>();
                    listOfEntities.Add(entryPOM);

                    SaveDatabase(listOfEntities);
                }
            }
        }




        private static double ParseDouble(string[] pom, int index)
        {
            if (index >= 0 && index < pom.Length)
            {
                string value = pom[index].Trim();

                if (!string.IsNullOrEmpty(value))
                {
                    if (double.TryParse(value, out double result))
                    {
                        return result;
                    }
                    else
                    {

                        return 0.0;
                    }
                }
                else
                {

                    return 0.0;
                }
            }
            else
            {

                return 0.0;
            }
        }


        private static void SaveDatabase(List<DatabaseEntry> entryList)
        {
            string databasePath = @"..\..\BackupDatabase.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<DatabaseEntry>));

            using (var stream = File.Create(databasePath))
            {
                serializer.Serialize(stream, entryList);
                Console.WriteLine("Backup saved.");
            }
        }




    }
}
