using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProxy : ChannelFactory<IDataBaseManagement>, IDataBaseManagement
    {
        IDataBaseManagement factory;

        public ClientProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void CreateDatabase()
        {
            try
            {
                factory.CreateDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void ArchiveDatabase()
        {
            try
            {
                factory.ArchiveDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void DeleteDatabase()
        {
            try
            {
                factory.DeleteDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void AddEntry(DataBaseEntry entry)
        {
            try
            {
                factory.AddEntry(entry);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void ModifyEntry(DataBaseEntry entry)
        {
            try
            {
                factory.ModifyEntry(entry);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public double AvgCityConsumption(string city)
        {
            double res = -1;
            try
            {
                res = factory.AvgCityConsumption(city);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return res;
        }

        public double AvgRegionConsumption(string region)
        {
            double res = -1;
            try
            {
                res = factory.AvgRegionConsumption(region);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return res;
        }

        public DataBaseEntry HighestRegionConsumer(string region)
        {
            DataBaseEntry res = null;
            try
            {
                res = factory.HighestRegionConsumer(region);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return res;
        }

        public void Ispisi(string s)
        {
            try
            {
                //za ispis
                factory.Ispisi(s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
    }
}
