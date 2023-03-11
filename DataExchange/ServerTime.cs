using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExchange
{
    public class ServerTime
    {
        public static string GetServerTime()
        {
            /*
            System.Management. ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = System.Management.ImpersonationLevel.Impersonate;


            ManagementScope scope = new ManagementScope("\\\\FullComputerName\\root\\cimv2", options);
            scope.Connect();

            //Query system for Operating System information
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();
            /*
            foreach (ManagementObject m in queryCollection)
            {
                // Display the remote computer information
                Console.WriteLine("Computer Name     : {0}", m["csname"]);
                Console.WriteLine("Windows Directory : {0}", m["WindowsDirectory"]);
                Console.WriteLine("Operating System  : {0}", m["Caption"]);
                Console.WriteLine("Version           : {0}", m["Version"]);
                Console.WriteLine("Manufacturer      : {0}", m["Manufacturer"]);
            }*/
            //TcpClient tcpClient = new TcpClient("10.10.102.100", 13);
            //StreamReader stream = new StreamReader(tcpClient.GetStream());
            //return stream.ReadToEnd();
            return "";
        }
    }
}
