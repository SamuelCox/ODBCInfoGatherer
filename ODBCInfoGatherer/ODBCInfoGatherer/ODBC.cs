using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODBCInfoGatherer
{


    public class ODBC
    {
        private string Name {get; set;}
        private string Driver {get; set;}
        private string Server {get; set;}

        public ODBC(string name, string driver, string server)
        {
            Name = name;
            Driver = driver;
            Server = server;
        }




    }
}
