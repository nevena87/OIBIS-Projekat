using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataBaseEntry
    {
        private int id;
        private string region;
        private string city;
        private string year;
        private double[] consumption = new double[12];

        public DataBaseEntry()
        {
        }

        public DataBaseEntry(int id, string region, string city, string year, double[] consumption)
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

        //public override bool Equals(object obj)
        //{
        //    var entry = obj as DataBaseEntry;
        //    return entry != null &&
        //           Id == entry.Id &&
        //           Region == entry.Region &&
        //           City == entry.City &&
        //           Year == entry.Year &&
        //           EqualityComparer<double[]>.Default.Equals(Consumption, entry.Consumption);
        //}

        public double GetYearlyConsumption()
        {
            double sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += Consumption[i];
            }
            return sum;
        }
    }
}
