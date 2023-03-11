using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceInstaller
{
    public partial class ServiceInstaller : Form
    {
        private ServiceController serviceController;
        public ServiceInstaller()
        {
            InitializeComponent();
            CheckService();
        }


        #region Расстановка действий при установленной службе
        private void CheckService()
        {
            // Если служба установлена
            if (ServiceIsExisted("GLORYService"))
            {
                // Проверяем статус службы и выставляем действия
                CheckStatus();
                btnServiceUninstall.Enabled = true;    // Кнокпа "Установить" активна
                btnServiceInstall.Enabled = false;  // Кнокпа "Удалить" заблокирована
            }
            else
            {
                btnServiceUninstall.Enabled = false;   // Кнокпа "Установить" заблокирована
                btnServiceInstall.Enabled = true;   // Кнокпа "Удалить" активна
                btnServiceStop.Enabled = false;  // Кнокпа "Стоп" заблокирована
                btnServiceStart.Enabled = false; // Кнокпа "Старт" заблокирована
            }
        }
        #endregion


        #region Проверить запущена ли служба 
        private void CheckStatus()
        {
            // Cоздаем переменную с указателем на службу
            serviceController = new ServiceController("GLORYService");
            // Усли служба запущена
            if (serviceController.Status == ServiceControllerStatus.Running)
            {
                btnServiceStop.Enabled = true;   // Кнокпа "Стоп" активна
                btnServiceStart.Enabled = false; // Кнокпа "Старт" заблокирована
            }
            else
            {
                btnServiceStop.Enabled = false;  // Кнокпа "Стоп" заблокирована
                btnServiceStart.Enabled = true;  // Кнокпа "Старт" активна
            }

        }
        #endregion

        #region Проверить установлена ли служба 
        private bool ServiceIsExisted(string p)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName == p)
                    return true;
            }
            return false;
        }
        #endregion


        #region Кнопка установки службы
        private void BtnServiceInstall_Click(object sender, EventArgs e)
        {
            string[] args = { "GLORY_SERVICE.exe" };
            if (!ServiceIsExisted("GLORYService"))
            {
                try
                {
                    ManagedInstallerClass.InstallHelper(args);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            CheckService(); // Проверяем установлена ли служба и ее статус
        }

        #endregion

        #region Кнопка удаления службы
        private void BtnServiceUninstall_Click(object sender, EventArgs e)
        {
            string[] args = { "/u", "GLORY_SERVICE.exe" };
            if (ServiceIsExisted("GLORYService"))
            {
                try
                {
                    ManagedInstallerClass.InstallHelper(args);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            CheckService(); // Проверяем установлена ли служба и ее статус
        }
        #endregion

        #region Кнопка запуска службы
        private void BtnServiceStart_Click(object sender, EventArgs e)
        {
            if (ServiceIsExisted("GLORYService"))
            {
                serviceController.Start();
                btnServiceStop.Enabled = true;
                btnServiceStart.Enabled = false;
            }
        }
        #endregion


        #region Кнопка остановки службы
        private void BtnServiceStop_Click(object sender, EventArgs e)
        {
            if (ServiceIsExisted("GLORY.Service"))
            {
                serviceController.Stop();
                btnServiceStop.Enabled = false;
                btnServiceStart.Enabled = true;
            }
        }
        #endregion
    }
}
