using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login;
using DataExchange;

namespace CountingAppService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //MainForm mainForm = new MainForm();
            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {                
                 MessageBox.Show("Приложение уже запущено");
                
                 return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());

                if (Login.LoginForm.Login)
                {
                    //Application.Run(mainForm);
                    Application.Run(new MainForm());
                }
            }
        }
    }
}
