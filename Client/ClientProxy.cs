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
            try
            {
                factory.Ispisi(s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void ModifyEntry(DataBaseEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
