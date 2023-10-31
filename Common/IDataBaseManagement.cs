using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IDataBaseManagement
    {
        [OperationContract]
        void Ispisi(string s);

        [OperationContract]
        void CreateDatabase();
        [OperationContract]
        void ArchiveDatabase();
        [OperationContract]
        void DeleteDatabase();
        [OperationContract]
        void AddEntry(DataBaseEntry entry);
        [OperationContract]
        void ModifyEntry(DataBaseEntry entry);
        [OperationContract]
        double AvgCityConsumption(string city);
        [OperationContract]
        double AvgRegionConsumption(string region);
        [OperationContract]
        DataBaseEntry HighestRegionConsumer(string region);
    }
}
