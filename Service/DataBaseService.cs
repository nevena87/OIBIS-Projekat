using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Zadatak26
{
    class DataBaseService : IDataBaseManagement
    {
        public void Ispisi(string s)
        {
            Console.WriteLine("Primljena poruka: " + s);
        }
    }
}
