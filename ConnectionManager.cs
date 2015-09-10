using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ODBCInfoGatherer
{
    public class ConnectionManager
    {
        //A constant which stores the path to the odbc.ini registry
        public static string SYSTEM_REGISTRY_PATH = @"SOFTWARE\ODBC\ODBC.INI";
        //A constant which stores the path to the root of the Local Machine registry, used for System DSN
        public static string LOCAL_MACHINE_STRING = @"HKEY_LOCAL_MACHINE";
        //A constant which stores the path to the root of the Current User registry, used for User DSN
        public static string CURRENT_USER_STRING = @"HKEY_CURRENT_USER";

        /// <summary>
        /// Constructor for the ConnectionManager object, completely empty,
        /// this class could probably be static.
        /// </summary>
        public ConnectionManager()
        {

        }

      
        /// <summary>
        /// A method that gets a list of all the 64 bit System data sources
        /// configured in the 64 bit ODBC.
        /// </summary>
        /// <returns>A list of ODBC objects</returns>
        public List<ODBC> GetSystemDSNList64()
        {

            return GetSubKeysAndIterateOver(false, false, SYSTEM_REGISTRY_PATH);
            
        }

        /// <summary>
        /// A method that gets a list of all the 32 bit System data sources
        /// configured in the 32 bit ODBC.
        /// </summary>
        /// <returns>A list of ODBC objects</returns>
        public List<ODBC> GetSystemDSNList32()
        {
            return GetSubKeysAndIterateOver(true, false, SYSTEM_REGISTRY_PATH);
            
        }

        /// <summary>
        /// A method that gets a list of all the 64 bit User data sources
        /// configured in the 64 bit ODBC.
        /// </summary>
        /// <returns>A list of ODBC objects</returns>
        public List<ODBC> GetUserDSNList64()
        {
            return GetSubKeysAndIterateOver(false, true, SYSTEM_REGISTRY_PATH);
            
        }

        /// <summary>
        /// A method that gets a list of all the 32 bit User data sources
        /// configured in the 32 bit ODBC.
        /// </summary>
        /// <returns>A list of ODBC objects</returns>
        public List<ODBC> GetUserDSNList32()
        {
            return GetSubKeysAndIterateOver(true, true, SYSTEM_REGISTRY_PATH);
        }
        
        /// <summary>
        /// A method that gets all the sub keys in the registry representing
        /// ODBC connections, and calls GetDataAndConstructODBC() to
        /// construct the ODBC object for each of these, then adds
        /// them to a List and returns this.
        /// </summary>
        /// <param name="use32BitView">Whether to look in the 32 bit registry or 64 bit registry, 
        /// if true then it looks in the 32 bit registry, otherwise 64</param>
        /// <param name="lookInCurrentUser">Whether to look in the Current_User registry,
        /// or the local_machine registry, if true it looks in Current_User, otherwise
        /// Local_Machine, controls whether it's returning user data sources or system data sources
        /// </param>
        /// <param name="path">The registry path to look in.</param>
        /// <returns>A List of ODBC objects which represent the various ODBC connections.</returns>
        private List<ODBC> GetSubKeysAndIterateOver(bool use32BitView, bool lookInCurrentUser, string path)
        {
            List<ODBC> listOfConnections = new List<ODBC>();
            string stringToReplace;
            RegistryHive hiveToUse;
            RegistryView viewToUse;
            viewToUse = use32BitView ? RegistryView.Registry32 : RegistryView.Registry64;
            if(lookInCurrentUser)
            {
                stringToReplace = CURRENT_USER_STRING;
                hiveToUse = RegistryHive.CurrentUser;
            }
            else
            {
                stringToReplace = LOCAL_MACHINE_STRING;
                hiveToUse = RegistryHive.LocalMachine;
            }
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(hiveToUse,  viewToUse);
                key = key.OpenSubKey(path);
                string[] odbckeys = key.GetSubKeyNames();
                foreach(string odbcKey in odbckeys)
                {
                    if(!odbcKey.Equals("ODBC Data Sources"))
                    {
                        ODBC odbcObject = GetDataAndConstructOdbcObject(hiveToUse, viewToUse, stringToReplace, path, odbcKey);
                        listOfConnections.Add(odbcObject);
                    }
                }
            }
            catch(NullReferenceException ex)
            {
                return null;
            }
            return listOfConnections;
        }

        private ODBC GetDataAndConstructOdbcObject(RegistryHive hiveToUse, RegistryView viewToUse, string stringToReplace, string path, string odbcKey)
        {
            RegistryKey odbcReg = RegistryKey.OpenBaseKey(hiveToUse, viewToUse);
            odbcReg = odbcReg.OpenSubKey(path);
            odbcReg = odbcReg.OpenSubKey(odbcKey);
            string server = (string)odbcReg.GetValue("server");
            string driver = (string)odbcReg.GetValue("driver");
            string name = odbcReg.Name.Replace(stringToReplace + "\\" + path + "\\", "");
            string dsn = name;
            ODBC odbcObject = new ODBC(dsn, driver, server);
            return odbcObject;
        }
    }
}
