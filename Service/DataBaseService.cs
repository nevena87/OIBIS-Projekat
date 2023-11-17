using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Zadatak26
{
    public class DataBaseService : IDataBaseManagement, IBackupService
    {
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
            throw new NotImplementedException();
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