
using System.ServiceProcess;


namespace GLORY_SERVICE
{
    public partial class GLORY_Sevice : ServiceBase
    {
        private EventLogger.EventLogger eventLogger = new EventLogger.EventLogger();     // Переменная для записи в журнал событий
        private FileProcesser fileProcesser = new FileProcesser();
        public GLORY_Sevice()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)  
        {
            eventLogger.WriteInfo("Служба запущена");
            fileProcesser.PathStartMonitor();
            fileProcesser.Watch();

        }

        protected override void OnStop()
        {
            fileProcesser.Dispose();
            eventLogger.WriteInfo("Служба остановлена");
        }
    }
}
