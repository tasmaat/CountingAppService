using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Install;
using System.Diagnostics;

namespace GLORY_SERVICE
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new GLORY_Sevice()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                EventLog eventLog = new EventLog();
                if(!EventLog.SourceExists("GLORY Service"))
                {
                    EventLog.CreateEventSource("GLORY Service", "GLORY Service");
                }
                eventLog.Source = "GLORY Service";
                eventLog.WriteEntry(String.Format("Exception: {0} \n\nStack: {1}", ex.Message + " : " + ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
            }
            
        }
    }
}
