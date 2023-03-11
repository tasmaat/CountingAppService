using System.Diagnostics;

namespace EventLogger
{
    public class EventLogger
    {
        private EventLog eventLog = new EventLog();

        public EventLogger()
        {

            //EventLog.CreateEventSource("GLORY_SERVICE", "GLORY_SERVICE");
            //AutoLog = false;
            if (!EventLog.SourceExists("GLORY_SERVICE")) //Если журнал с таким названием не существует
            {

                EventLog.CreateEventSource("GLORY_SERVICE", "GLORY_SERVICE"); // Создаем журнал
            }

            eventLog.Source = "GLORY_SERVICE"; //Помечаем, что будем писать в этот журнал

            //eventLog.MaximumKilobytes = 4194240;

        }

        public void WriteInfo(string message)
        {
            eventLog.WriteEntry(message, EventLogEntryType.Information);
        }

        public void WriteError(string message)
        {
            eventLog.WriteEntry(message, EventLogEntryType.Error);
        }


    }
}
