using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    /// <summary>
    /// DatabaseEntry is a class that will be serialized into the database and deserialized out of the database. 
    /// It contains all the fields specified in the requirements. 
    /// </summary>

    [Serializable]
    public class DatabaseEntry
    {
        private int id;
        private string region;
        private string city;
        private string year;
        private double[] consumption = new double[12]; // Consumption for each month

        public DatabaseEntry()
        {
        }

        public DatabaseEntry(int id, string region, string city, string year, double[] consumption)
        {
            this.Id = id;
            this.Region = region;
            this.City = city;
            this.Year = year;
            this.Consumption = consumption;
        }

        public int Id { get => id; set => id = value; }
        public string Region { get => region; set => region = value; }
        public string City { get => city; set => city = value; }
        public string Year { get => year; set => year = value; }
        public double[] Consumption { get => consumption; set => consumption = value; }

    

        public double GetYearlyConsumption()
        {
            double sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += Consumption[i];
            }
            return sum;
        }

       
     

        public override string ToString()
        {
            return $"Id: {Id}, Region: {Region}, City: {City}, Year: {Year}, Consumption: [{string.Join(", ", Consumption)}]";
        }

    }
}
