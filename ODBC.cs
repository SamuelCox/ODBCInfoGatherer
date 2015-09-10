using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODBCInfoGatherer
{


    public class ODBC
    {
        //The Data source name of the ODBC connection.
        public string Name {get; private set;}
        //The driver of the ODBC connection.
        public string Driver {get; private set;}
        //The server of the ODBC connection.
        public string Server {get; private set;}

        /// <summary>
        /// Constructor for ODBC objects.
        /// </summary>
        /// <param name="name">The Data source name of the ODBC connection.</param>
        /// <param name="driver">The Driver of the ODBC connection.</param>
        /// <param name="server">The server of the ODBC connection.</param>
        public ODBC(string name, string driver, string server)
        {
            Name = name;
            Driver = driver;
            Server = server;
        }




    }
}
