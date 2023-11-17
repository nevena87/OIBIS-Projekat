using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.IO;
using System.Xml.Serialization;
using Manager;
using System.Threading;

namespace Zadatak26
{
    public class DataBaseService : IDataBaseManagement, IBackupService
    {
        private string databasePath = @"..\..\Database.xml";
        public void AddEntry(DataBaseEntry entry)
        {
            throw new NotImplementedException();
        }

        public void ArchiveDatabase()
        {
            throw new NotImplementedException();
        }

        public double AvgCityConsumption(string city)
        {
            throw new NotImplementedException();
        }

        public double AvgRegionConsumption(string region)
        {
            throw new NotImplementedException();
        }

        public void CreateDatabase()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Administrate"))
            {
                if (!File.Exists(databasePath))
                {
                    using (var stream = File.Create(databasePath))
                    {
                        Console.WriteLine("Database created.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to create a Database. Database already exists.");
                }
            }
        }

        public void DeleteDatabase()
        {
            throw new NotImplementedException();
        }

        public DataBaseEntry HighestRegionConsumer(string region)
        {
            throw new NotImplementedException();
        }

        public void Ispisi(string s)
        {
            Console.WriteLine("Primljena poruka: " + s);
        }

        public void ModifyEntry(DataBaseEntry entry)
        {
            throw new NotImplementedException();
        }

        public List<DataBaseEntry> PullDatabase()
        {
            throw new NotImplementedException();
        }
    }
}