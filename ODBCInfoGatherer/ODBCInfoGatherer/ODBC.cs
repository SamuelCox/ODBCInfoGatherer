using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODBCInfoGatherer
{


    public class ODBC
    {
        public string Name {get; private set;}
        public string Driver {get; private set;}
        public string Server {get; private set;}

        public ODBC(string name, string driver, string server)
        {
            Name = name;
            Driver = driver;
            Server = server;
        }




    }
}
