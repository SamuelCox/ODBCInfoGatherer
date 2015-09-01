using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ODBCInfoGatherer
{
    class ConnectionManager
    {
        public static string SYSTEM_REGISTRY_PATH = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\ODBC\ODBC.INI";
        public static string USER_REGISTRY_PATH = @"HKEY_CURRENT_USER\Software\ODBC\ODBC.INI";
        
        public ConnectionManager()
        {

        }

        public List<ODBC> GetSystemDSNList64()
        {
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            catch (NullReferenceException ex)
            {

            }
            return null;
        }

        public List<ODBC> GetSystemDSNList32()
        {
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            catch (NullReferenceException ex)
            {

            }
            return null;
        }

        public List<ODBC> GetUserDSNList64()
        {
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            catch(NullReferenceException ex)
            {

            }
            return null;
        }

        public List<ODBC> GetUserDSNList32()
        {
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            catch (NullReferenceException ex)
            {

            }
            return null;
        }

    }
}
