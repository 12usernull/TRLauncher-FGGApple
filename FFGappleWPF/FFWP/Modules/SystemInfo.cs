using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace FFWP.Modules
{
 
    public static class SystemInfo
    {
        public static double GetPhysicalAvailableMemoryInGB()
        {
            ManagementScope scope = new ManagementScope();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject mo in collection)
            {
                double ramBytes = Convert.ToDouble(mo["TotalPhysicalMemory"]);
                double ramGB = ramBytes / (1024 * 1024 * 1024);
                return ramGB;
            }

            return 0;
        }
    }

}
