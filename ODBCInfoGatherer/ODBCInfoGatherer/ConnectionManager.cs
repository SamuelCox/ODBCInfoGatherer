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
        public static string SYSTEM_REGISTRY_PATH = @"SOFTWARE\ODBC\ODBC.INI";
        public static string USER_REGISTRY_PATH = @"Software\ODBC\ODBC.INI";
        public static string LOCAL_MACHINE_STRING = @"HKEY_LOCAL_MACHINE";
        public static string CURRENT_USER_STRING = @"HKEY_CURRENT_USER";

        public ConnectionManager()
        {

        }

      

        public List<ODBC> GetSystemDSNList64()
        {

            return GetSubKeysAndIterateOver(false, false, SYSTEM_REGISTRY_PATH);
            
        }

        public List<ODBC> GetSystemDSNList32()
        {
            return GetSubKeysAndIterateOver(true, false, SYSTEM_REGISTRY_PATH);
            
        }

        public List<ODBC> GetUserDSNList64()
        {
            return GetSubKeysAndIterateOver(false, true, SYSTEM_REGISTRY_PATH);
            
        }

        public List<ODBC> GetUserDSNList32()
        {
            return GetSubKeysAndIterateOver(true, true, SYSTEM_REGISTRY_PATH);
        }

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
