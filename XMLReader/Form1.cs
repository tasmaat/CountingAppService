using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;
using System.ServiceProcess;
using GLORY_SERVICE;
using System.Configuration.Install;
using System.Diagnostics;
using System.Security.Permissions;
using CountingDB;

namespace XMLReader
{
    public partial class Form1 : Form
    {

        private ServiceController serviceController;
        private EventLog eventLog = new EventLog();
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private DataSet dataSetXML = new DataSet();
        private String fileName = String.Empty;
        private String defaultPath = @"C:\образцы файлов с uw-f\UWFFiles";
        private String readyPath = @"C:\образцы файлов с uw-f\UWFFiles\Ready";
        private String errorPath = @"C:\образцы файлов с uw-f\UWFFiles\Error";
        private FolderBrowserDialog folderDialog =  new FolderBrowserDialog();
        private DataSet denomDataSet = null;
        private DataSet conditionDataSet = null;
        private MSDataBase dataBase = new MSDataBase();
        private DataTable denomFile = new DataTable();


        public Form1()
        {
            InitializeComponent();
            dataBase.Connect();
            denomDataSet = dataBase.GetData("t_g_denomination");
            conditionDataSet = dataBase.GetData("t_g_condition");
            denomFile.Columns.Add("seq_number");
            denomFile.Columns.Add("rev_number");
            denomFile.Columns.Add("id", typeof(int));
            denomFile.Columns.Add("denom_value");
            denomFile.Columns.Add("condition");
            denomFile.Columns.Add("rev");
            
            denomFile.Columns.Add("denom_db");
            denomFile.Columns.Add("condition_db");
            CheckService();
            //CheckStatus();
        }


        #region Расстановка действий при установленной службе
        private void CheckService()
        {
            // Если служба установлена
            if (ServiceIsExisted("GLORY_Service"))
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
            serviceController = new ServiceController("GLORY_Service");
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

        #endregion

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            
            foreach(string file in Directory.GetFiles(defaultPath))
            {
                //file = Path.Combine(defaultPath, file);
                //try
                //{
                //    dataSetXML.ReadXml(Path.Combine(defaultPath, file));
                //    File.Copy(Path.Combine(defaultPath, file),)
                //w}

            }

            try
            {
                
                using (StreamReader stream = new StreamReader(Path.Combine(defaultPath, "IMP_HC_DENOMI_CONVERSION_KZT.dat")))
                {
                    bool bRead = false;
                    string denomLine;

                    while((denomLine = stream.ReadLine()) != null)
                    {
                        if (denomLine == "*****")
                        {
                            bRead = true;
                            continue;
                        }
                            
                        if (bRead)
                        {
                            denomLine = denomLine.Replace("\"", "");
                            DataRow rowDenom = denomFile.NewRow();
                            rowDenom.ItemArray = denomLine.Split(',');
                            denomFile.Rows.Add(rowDenom);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(denomFile.Rows.Count > 0)
            {
                dataGridView1.AutoGenerateColumns = false;
                //dataGridView1.Columns.Add("seq_number", "№");


                dataGridView1.Columns.Add("rev_number", "Тип");
                dataGridView1.Columns["rev_number"].DataPropertyName = "rev_number";
                dataGridView1.Columns.Add("id", "Индекс");
                dataGridView1.Columns["id"].DataPropertyName = "id";
                dataGridView1.Columns.Add("denom_value", "Номинал");
                dataGridView1.Columns["denom_value"].DataPropertyName = "denom_value";
                dataGridView1.Columns.Add("condition", "Состояние");
                dataGridView1.Columns["condition"].DataPropertyName = "condition";
                dataGridView1.Columns.Add("rev", "Литер");
                dataGridView1.Columns["rev"].DataPropertyName = "rev";

                DataGridViewComboBoxColumn denomColumn = new DataGridViewComboBoxColumn();
                denomColumn.DisplayMember = "name";
                denomColumn.ValueMember = "id";
                denomColumn.DataPropertyName = "name";
                denomColumn.DataSource = denomDataSet.Tables[0];
                denomColumn.Name = "denom_db";
                denomColumn.HeaderText = "Номинал БД";
                dataGridView1.Columns.Add(denomColumn);

                DataGridViewComboBoxColumn conditionColumn = new DataGridViewComboBoxColumn();
                conditionColumn.DisplayMember = "name";
                conditionColumn.ValueMember = "id";
                conditionColumn.DataPropertyName = "name";
                conditionColumn.Name = "condition_db";
                conditionColumn.HeaderText = "Состояние БД";

                conditionColumn.DataSource = conditionDataSet.Tables[0];
                dataGridView1.Columns.Add(conditionColumn);
                dataGridView1.DataSource = denomFile;
            }

            watcher.Path = defaultPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;
 
        }

        private void BtnServiceInstall_Click(object sender, EventArgs e)
        {
            string[] args = { "GLORY_SERVICE.exe" };
            if (!ServiceIsExisted("GLORY_Service"))
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

        private void BtnServiceUninstall_Click(object sender, EventArgs e)
        {
            string[] args = { "/u", "GLORY_SERVICE.exe" };
            if (ServiceIsExisted("GLORY_Service"))
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

        private void BtnServiceStart_Click(object sender, EventArgs e)
        {
            if (ServiceIsExisted("GLORY_Service"))
            {
                serviceController.Start();
                btnServiceStop.Enabled = true;
                btnServiceStart.Enabled = false;
            }
        }

        private void BtnServiceStop_Click(object sender, EventArgs e)
        {
            if (ServiceIsExisted("GLORY_Service"))
            {
                serviceController.Stop();
                btnServiceStop.Enabled = false;
                btnServiceStart.Enabled = true;
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show(e.FullPath + " " + e.ChangeType);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            
            try
            {
                if (fileName != e.FullPath)
                {
                    
                    Stream stream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read);
                    if (stream != null)
                    {

                        dataSetXML.ReadXml(stream);
                        //int cardNum = dataSetXML.Tables["HeaderCardTransaction"]
                        stream.Close();
                        fileName = e.FullPath;
                    }
                }
                
                
            }
            catch(IOException ex)
            {

            }
            finally
            {

            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            MessageBox.Show(e.OldFullPath +" " + e.OldName + " " + e.FullPath);
        }

        private void DataToGrid()
        {
            DataSet ds = dataSetXML;
            //dataGridView1.DataSource = dataSetXML.Tables["TransactionFile"];
       }

        private DataTable GridToTable(DataGridView dataGridView)
        {
            DataTable resultTable = new DataTable();

            foreach(DataGridViewColumn column in dataGridView.Columns)
            {
                resultTable.Columns.Add(column.Name);
            }
                 

            foreach(DataGridViewRow row in dataGridView.Rows)
            {
                resultTable.Rows.Add();
                foreach(DataGridViewCell cell in row.Cells)
                {
                    resultTable.Rows[resultTable.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                }
                //resultTable.ImportRow(row);
            }
            return resultTable;
        }

        private void WriteMap_Click(object sender, EventArgs e)
        {
            //denomFile.AcceptChanges();
            //dataGridView1.Refresh();
            //DataTable XMLMapSetting = dataGridView1.Rows.Cast<DataView>(). .ToTable();
            DataTable XMLMapTable = GridToTable(dataGridView1);
            try
            {
                XMLMapTable.WriteXml(File.OpenWrite("MapDenomination.xml"));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void BtnOpenDialog_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderDialog.ShowDialog();

            if(dialogResult == DialogResult.OK)
            {
                defaultPath = folderDialog.SelectedPath;
                tbPath.Text = folderDialog.SelectedPath;
                tbErrorPath.Text = folderDialog.SelectedPath + @"\Error";
                tbReadyPath.Text = folderDialog.SelectedPath + @"\Ready";
                errorPath = folderDialog.SelectedPath + @"\Error";
                readyPath = folderDialog.SelectedPath + @"\Ready";
            }

        }

      
    }
}
