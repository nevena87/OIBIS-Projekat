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
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Write"))
            {
                if (File.Exists(databasePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
                    List<DataBaseEntry> entryList = new List<DataBaseEntry>();

                    using (FileStream stream = File.OpenRead(databasePath))
                    {
                        try
                        {
                            entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                        }
                        catch (Exception e)
                        {
                            //Avoiding the empty database exception.
                        }
                    }
                    entry.Id = entryList.Count;
                    entryList.Add(entry);
                    using (var stream = File.Create(databasePath))
                    {
                        serializer.Serialize(stream, entryList);
                        Console.WriteLine("Entry added to database");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot add Entry to the Database. Database doesn't exist.");
                }
            }
        }

        public void ArchiveDatabase()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Administrate"))
            {
                if (File.Exists(databasePath))
                {
                    string path = @"..\..\ArchivedDatabases";
                    string[] filePaths = Directory.GetFiles(path);
                    int numFiles = filePaths.Length;
                    numFiles += 1;
                    string filename = @"\ArchivedDatabase" + numFiles + ".xml";
                    string archivedDatabasePath = path + filename;

                    XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
                    List<DataBaseEntry> entryList = new List<DataBaseEntry>();

                    using (FileStream stream = File.OpenRead(databasePath))
                    {
                        try
                        {
                            entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                        }
                        catch (Exception e)
                        {
                            //Avoiding the empty database exception.
                        }
                    }
                    using (var stream = File.Create(archivedDatabasePath))
                    {
                        serializer.Serialize(stream, entryList);
                        File.Delete(databasePath);
                        Console.WriteLine("Database archived.");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot archive the Database. Database doesn't exist.");
                }
            }
        }

        public double AvgCityConsumption(string city)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Read"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
                List<DataBaseEntry> entryList = new List<DataBaseEntry>();

                using (FileStream stream = File.OpenRead(databasePath))
                {
                    try
                    {
                        entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                    }
                    catch (Exception e)
                    {
                        //Avoiding the empty database exception.
                    }
                }
                double res = 0;
                int cnt = 0;
                foreach (DataBaseEntry entry in entryList)
                {
                    if (entry.City.Equals(city))
                    {
                        res += entry.GetYearlyConsumption();
                        cnt++;
                    }
                }
                if (res == 0)
                {
                    Console.WriteLine("There are no such Cities in the Database.");
                    return -1;
                }
                return (res / cnt);
            }
            else
            {
                return -1;
            }
        }

        public double AvgRegionConsumption(string region)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Read"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
                List<DataBaseEntry> entryList = new List<DataBaseEntry>();

                using (FileStream stream = File.OpenRead(databasePath))
                {
                    try
                    {
                        entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                    }
                    catch (Exception e)
                    {
                        //Avoiding the empty database exception.
                    }
                }
                double res = 0;
                int cnt = 0;
                foreach (DataBaseEntry entry in entryList)
                {
                    if (entry.Region.Equals(region))
                    {
                        res += entry.GetYearlyConsumption();
                        cnt++;
                    }
                }
                if (res == 0)
                {
                    Console.WriteLine("There are no such Regions in the Database.");
                    return -1;
                }
                return (res / cnt);
            }
            else
            {
                return -1;
            }
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
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Administrate"))
            {
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                    Console.WriteLine("Database deleted.");
                }
                else
                {
                    Console.WriteLine("Cannot delete the Database. It doesn't exist!");
                }
            }
        }

        public DataBaseEntry HighestRegionConsumer(string region)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Read"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
                List<DataBaseEntry> entryList = new List<DataBaseEntry>();
                DataBaseEntry highestConsumer = null;

                using (FileStream stream = File.OpenRead(databasePath))
                {
                    try
                    {
                        entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                    }
                    catch (Exception e)
                    {
                        //Avoiding the empty database exception.
                    }
                }
                double max = 0;
                foreach (DataBaseEntry entry in entryList)
                {
                    if ((entry.Region.Equals(region)) && (entry.GetYearlyConsumption() > max))
                    {
                        max = entry.GetYearlyConsumption();
                        highestConsumer = entry;
                    }
                }
                if (highestConsumer == null)
                {
                    Console.WriteLine("There are no Consumers in that region.");
                }
                return highestConsumer;
            }
            else
            {
                return null;
            }
        }

        public void Ispisi(string s)
        {
            Console.WriteLine("Primljena poruka: " + s);
        }

        public void ModifyEntry(DataBaseEntry entry)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Write"))
            {
                if (File.Exists(databasePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
                    List<DataBaseEntry> entryList = new List<DataBaseEntry>();
                    DataBaseEntry entryToRemove = null;

                    using (FileStream stream = File.OpenRead(databasePath))
                    {
                        try
                        {
                            entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                        }
                        catch (Exception e)
                        {
                            //Avoiding the empty database exception.
                        }
                    }
                    foreach (DataBaseEntry existingEntry in entryList)
                    {
                        if (existingEntry.Id == entry.Id)
                        {
                            entryToRemove = existingEntry;
                            break;
                        }
                    }
                    if (entryToRemove != null)
                    {
                        entryList.Remove(entryToRemove);
                        try
                        {
                            entryList.Insert(entry.Id, entry);
                        }
                        catch
                        {
                            entryList.Add(entry);
                        }
                        using (var stream = File.Create(databasePath))
                        {
                            serializer.Serialize(stream, entryList);
                            Console.WriteLine("Entry modified.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot modify Entry. Database doesn't contain given Entry.");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot add Entry to the Database. Database doesn't exist.");
                }
            }
        }

        public List<DataBaseEntry> PullDatabase()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<DataBaseEntry>));
            List<DataBaseEntry> entryList = new List<DataBaseEntry>();

            using (FileStream stream = File.OpenRead(databasePath))
            {
                try
                {
                    entryList = (List<DataBaseEntry>)serializer.Deserialize(stream);
                }
                catch (Exception e)
                {
                    //Avoiding the empty database exception.
                }
            }
            return entryList;
        }
    }
}