using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODBCInfoGatherer;

namespace Debugger
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionManager test = new ConnectionManager();
            List<ODBC> listOfConnections = new List<ODBC>();
            listOfConnections.AddRange(test.GetSystemDSNList64());
            listOfConnections.AddRange(test.GetSystemDSNList32());
            listOfConnections.AddRange(test.GetUserDSNList64());
            listOfConnections.AddRange(test.GetUserDSNList32());
            foreach(ODBC odbc in listOfConnections)
            {
                Console.WriteLine(odbc.Name + " " + odbc.Server + " " + odbc.Driver);
            }
            Console.Read();
        }
    }
}
