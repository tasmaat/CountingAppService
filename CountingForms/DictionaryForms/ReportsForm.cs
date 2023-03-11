using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.ParentForms;
using CountingForms.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class ReportsForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private ReportDocument reportDocument1;
        private MSDataBase dataBase = null;
        
        private DataSet clientsDataSet, userDataSet, clientSubFamilyDataSet, cashCentreDataSet, shiftDataSet;
        private DataSet  zoneDataSet,userFromDataSet,userToDataSet, zoneFromDataSet, zoneToDataSet ;

        private DataTable rDT;

        private string[] y;

        public ReportsForm()
        {
            dataBase = new MSDataBase();
            dataBase.Connect();

            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dgReports.AutoGenerateColumns = false;

            dgReports.Columns.Add("id", "№");
            dgReports.Columns["id"].Visible = true;
            dgReports.Columns["id"].Width = 20;
            dgReports.Columns["id"].DataPropertyName = "id";
            dgReports.Columns["id"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgReports.Columns.Add("name1", "name1");
            dgReports.Columns["name1"].Visible = false;
            dgReports.Columns["name1"].DataPropertyName = "name1";


            dgReports.Columns.Add("name2", "Наименование отчета");
            dgReports.Columns["name2"].Visible = true;
            dgReports.Columns["name2"].Width = 200;
            dgReports.Columns["name2"].DataPropertyName = "name2";
            dgReports.Columns["name2"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgReports.RowHeadersWidth = 4;

            dgReports.Width = 227;
        }

        private async void ReportsForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            rDT = new DataTable();
            rDT.Columns.Add("id", typeof(string));
            rDT.Columns.Add("name1", typeof(string));
            rDT.Columns.Add("name2", typeof(string));

            StreamReader stud1 = new StreamReader(@"C:\\report\\RptList.ini", Encoding.Default);
            string x1 = stud1.ReadToEnd();
            string[] y1 = x1.Split('\n');

            foreach (string s in y1)
            {
                rDT.Rows.Add(s.Split('='));
            }

            dgReports.DataSource = rDT;

            clientsDataSet = dataBase.GetData("t_g_client");
            userDataSet = dataBase.GetData("t_g_user");
            userFromDataSet = dataBase.GetData("t_g_user");
            userToDataSet = dataBase.GetData("t_g_user");
            clientSubFamilyDataSet = dataBase.GetData("t_g_clisubfml");
            cashCentreDataSet = dataBase.GetData9("select * from t_g_cashcentre where tipsp1=1");
            zoneDataSet = dataBase.GetData9("select * from t_g_cashcentre where tipsp1=2");
            zoneFromDataSet = dataBase.GetData9("select * from t_g_cashcentre where tipsp1=2");
            zoneToDataSet = dataBase.GetData9("select * from t_g_cashcentre where tipsp1=2");
            shiftDataSet = dataBase.GetData9("SELECT  [id], concat(CAST([startDateTime] AS varchar(6)), ' - ',[name]) as name FROM[t_g_shift] where status=0 order by startDateTime desc");

            cbShift.DisplayMember = "name";
            cbShift.ValueMember = "id";
            cbShift.DataSource = shiftDataSet.Tables[0];
            cbShift.SelectedIndex = -1;

            cbClient.DisplayMember = "name";
            cbClient.ValueMember = "id";
            cbClient.DataSource = clientsDataSet.Tables[0];
            cbClient.SelectedIndex = -1;

            cbUser.DisplayMember = "user_name";
            cbUser.ValueMember = "id";
            cbUser.DataSource = userDataSet.Tables[0];
            cbUser.SelectedIndex = -1;

            cbUserFrom.DisplayMember = "user_name";
            cbUserFrom.ValueMember = "id";
            cbUserFrom.DataSource = userFromDataSet.Tables[0];
            cbUserFrom.SelectedIndex = -1;

            cbUserTo.DisplayMember = "user_name";
            cbUserTo.ValueMember = "id";
            cbUserTo.DataSource = userToDataSet.Tables[0];
            cbUserTo.SelectedIndex = -1;

            cbClientSubFamily.DisplayMember = "name";
            cbClientSubFamily.ValueMember = "id";
            cbClientSubFamily.DataSource = clientSubFamilyDataSet.Tables[0];
            cbClientSubFamily.SelectedIndex = -1;

            cbCashCentre.DisplayMember = "branch_name";
            cbCashCentre.ValueMember = "id";
            cbCashCentre.DataSource = cashCentreDataSet.Tables[0];
            cbCashCentre.SelectedIndex = -1;

            cbZone.DisplayMember = "branch_name";
            cbZone.ValueMember = "id";
            cbZone.DataSource = zoneDataSet.Tables[0];
            cbZone.SelectedIndex = -1;

            cbZoneFrom.DisplayMember = "branch_name";
            cbZoneFrom.ValueMember = "id";
            cbZoneFrom.DataSource = zoneFromDataSet.Tables[0];
            cbZoneFrom.SelectedIndex = -1;

            cbZoneTo.DisplayMember = "branch_name";
            cbZoneTo.ValueMember = "id";
            cbZoneTo.DataSource = zoneToDataSet.Tables[0];
            cbZoneTo.SelectedIndex = -1;







            clear();
        }

        private void cbShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cbShift.Text.Trim();
            if(text.Contains("дневная смена"))

            
            {
                dateTimePicker3.Value = DateTime.Today.AddHours(8);
                dateTimePicker4.Value = DateTime.Today.AddHours(18);
                dateTimePicker2.Value = System.DateTime.Today;
            }
            else
            if (text.Contains("ночная смена"))
                
            {
                dateTimePicker3.Value = DateTime.Today.AddHours(18);
                dateTimePicker4.Value = DateTime.Today.AddHours(8);
                dateTimePicker2.Value = System.DateTime.Today.AddDays(1); 
            }


        }      

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            reportDocument1 = new ReportDocument();
            reportDocument1.Load(@"C:\\report\\"+ rDT.Rows[dgReports.CurrentCell.RowIndex]["name1"].ToString().Trim()+".rpt");
            
             
            #region reportDocument1.SetParameterValue
            for (int i = 0; i < y.Length; i++)
            {
                switch (y[i].Trim())
                {
                        case "PRM_BeginDateTime":
                            string s1 = dateTimePicker1.Text.Trim() + ' ' + dateTimePicker3.Text.Trim();
                            reportDocument1.SetParameterValue(i, s1);
                            break;
                        case "PRM_EndDateTime":
                            string s2 = dateTimePicker2.Text.Trim() + ' ' + dateTimePicker4.Text.Trim();
                            reportDocument1.SetParameterValue(i, s2);
                            break;
                        case "PRM_UserCashCentre":
                            reportDocument1.SetParameterValue(i, DataExchange.CurrentUser.CurrentUserCentre.ToString().Trim());
                            break;
                        case "PRM_ClientSubFamily":
                                reportDocument1.SetParameterValue(i, cbClientSubFamily.Text.Trim());
                            break;
                        case "PRM_User":
                                reportDocument1.SetParameterValue(i, cbUser.Text.Trim());
                            break;
                        case "PRM_CashCentre":
                                reportDocument1.SetParameterValue(i, cbCashCentre.Text.Trim());
                            break;
                        case "PRM_LoggedUser":
                            reportDocument1.SetParameterValue(i, DataExchange.CurrentUser.CurrentUserName.ToString().Trim());
                            break;
                        case "PRM_Client":
                                reportDocument1.SetParameterValue(i, cbClient.Text.Trim());
                            break;
                        case "PRM_Bag":
                                reportDocument1.SetParameterValue(i, tbBag.Text.Trim());
                            break;
                        case "PRM_MBag":
                                reportDocument1.SetParameterValue(i, tbMBag.Text.Trim());
                            break;
                        case "PRM_Marchrut":
                                reportDocument1.SetParameterValue(i, tbMarshrut.Text.Trim());
                            break;
                        case "PRM_Shift":
                        if (cbShift.SelectedIndex != -1)
                            reportDocument1.SetParameterValue(i, cbShift.SelectedValue.ToString().Trim());
                        else
                        {
                            MessageBox.Show("Выберите смену");
                            return;
                        }
                            break;                       
                        case "PRM_Zone":
                                reportDocument1.SetParameterValue(i, cbZone.Text.Trim());
                            break;
                        case "PRM_UserFrom":
                                reportDocument1.SetParameterValue(i, cbUserFrom.Text.Trim());
                            break;
                        case "PRM_UserTo":
                                reportDocument1.SetParameterValue(i, cbUserTo.Text.Trim());
                            break;
                        case "PRM_ZoneFrom":
                                reportDocument1.SetParameterValue(i, cbZoneFrom.Text.Trim());
                            break;
                        case "PRM_ZoneTo":
                                reportDocument1.SetParameterValue(i, cbZoneTo.Text.Trim());
                            break;
                }
            }

            #endregion reportDocument1.SetParameterValue

            
            reportDocument1.SetDatabaseLogon(dataBase.log, dataBase.par);
            //string myReportName = "C:\sales.pdf";
            string date = DateTime.Now.ToShortDateString().ToString().Trim();
            string time = DateTime.Now.ToLongTimeString().ToString().Trim();
            string datetime = " "+date +"-"+ time.Replace(':','.');
            //MessageBox.Show(datetime);

            if (!Directory.Exists(@"D:\\Отчеты\"))
            {
                Directory.CreateDirectory(@"D:\\Отчеты\");
            }

            reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\Отчеты\" + rDT.Rows[dgReports.CurrentCell.RowIndex]["name2"].ToString().Trim() + datetime +/*"."+time+*/".pdf");


            //ExportOptions options = new ExportOptions();
            //options.ExportFormatType = ExportFormatType.PortableDocFormat;
            //options.ExportDestinationType = ExportDestinationType.DiskFile;
            ////options.DestinationOptions = DestinationOptions
            //    reportDocument1.Export(options);
            CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();
            
            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета
            

            ReportShowForm reportShowForm = new ReportShowForm();

            // crystalReportViewer1.Parent = panel2;
            //crystalReportViewer1.Dock = DockStyle.Fill;
            //button1.Visible = false;

            reportShowForm.Text = rDT.Rows[dgReports.CurrentCell.RowIndex]["name2"].ToString().Trim();
            reportShowForm.name= rDT.Rows[dgReports.CurrentCell.RowIndex]["name2"].ToString().Trim();
            reportShowForm.crystalReportViewer1 = crystalReportViewer1;
            reportShowForm.reportDocument = reportDocument1;
            reportShowForm.Show();
           // comboBox1.Enabled = false;

        }

       private void clear()
        {
            panelDate.Visible = false;
            panelDate.Visible = false;
            panelClientSubFamily.Visible = false;
            panelUser.Visible = false;
            panelCashCentre.Visible = false;
            panelClient.Visible = false;
            panelBag.Visible = false;
            panelMBag.Visible = false;
            panelMarshrut.Visible = false;
            panelShift.Visible = false;
            panelZone.Visible = false;
            panelUserFrom.Visible = false;
            panelUserTo.Visible = false;
            panelZoneFrom.Visible = false;
            panelZoneTo.Visible = false;

            cbUser.SelectedIndex = -1;
            cbUserFrom.SelectedIndex = -1;
            cbUserTo.SelectedIndex = -1;
            cbCashCentre.SelectedIndex = -1;
            cbClient.SelectedIndex = -1;
            cbClientSubFamily.SelectedIndex = -1;
            cbShift.SelectedIndex = -1;
            
            cbZone.SelectedIndex = -1;
            cbZoneFrom.SelectedIndex = -1;
            cbZoneTo.SelectedIndex = -1;

            dateTimePicker1.Value = System.DateTime.Today;
            dateTimePicker2.Value = System.DateTime.Today;
            dateTimePicker3.Value = DateTime.Today.AddHours(8);
            dateTimePicker4.Value = DateTime.Today.AddHours(18); 
           
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            panelBag.Visible = false;
            panelCashCentre.Visible = false;
            panelClient.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(pm.VisiblePossibility(perm, panelBag))
                panelBag.Visible = true;
            if (pm.VisiblePossibility(perm, panelCashCentre))
                panelCashCentre.Visible = true;
            if (pm.VisiblePossibility(perm, panelClient))
                panelClient.Visible = true;
        }

        private void dgReports_SelectionChanged(object sender, EventArgs e)
        {
            clear();
            if (dgReports != null)
                if (dgReports.Rows.Count > 0)
                    if (dgReports.CurrentCell.ColumnIndex != -1)
                    {
                        //MessageBox.Show("name ="+ rDT.Rows[dgReports.CurrentCell.RowIndex]["name1"].ToString());

                        StreamReader studIni = new StreamReader(@"C:\\report\\"+ rDT.Rows[dgReports.CurrentCell.RowIndex]["name1"].ToString() + ".ini", UTF8Encoding.Default);
                        string x = studIni.ReadToEnd();
                        //string[] 
                            y = x.Split('\n');
                        

                        foreach (string s in y)
                        {
                            //MessageBox.Show("Parametr ='" + s + "'");
                            
                            //array[i]. = s.Trim();
                            //i++;
                            switch (s.Trim())
                            {
                                case //"PRM_BeginDateTime":
                                     "PRM_BeginDateTime":
                                    if(pm.VisiblePossibility(perm, panelDate))
                                        panelDate.Visible = true;
                                    break;
                                case "PRM_EndDateTime":
                                    if (pm.VisiblePossibility(perm, panelDate))
                                        panelDate.Visible = true;
                                    break;
                                case "PRM_ClientSubFamily":
                                    if (pm.VisiblePossibility(perm, panelClientSubFamily))
                                        panelClientSubFamily.Visible = true;
                                    break;
                                case "PRM_User":
                                    if (pm.VisiblePossibility(perm, panelUser))
                                        panelUser.Visible = true;
                                    break;
                                case "PRM_CashCentre":
                                    if (pm.VisiblePossibility(perm, panelCashCentre))
                                        panelCashCentre.Visible = true;
                                    break;
                                case "PRM_Client":
                                    if (pm.VisiblePossibility(perm, panelClient))
                                        panelClient.Visible = true;
                                    break;
                                case "PRM_Bag":
                                    if (pm.VisiblePossibility(perm, panelBag))
                                        panelBag.Visible = true;
                                    break;
                                case "PRM_MBag":
                                    if (pm.VisiblePossibility(perm, panelMBag))
                                        panelMBag.Visible = true;
                                    break;
                                case "PRM_Marchrut":
                                    if (pm.VisiblePossibility(perm, panelMarshrut))
                                        panelMarshrut.Visible = true;
                                    break;
                                case "PRM_Shift":
                                    if (pm.VisiblePossibility(perm, panelShift))
                                        panelShift.Visible = true;
                                    break;   
                                case "PRM_Zone":
                                    if (pm.VisiblePossibility(perm, panelZone))
                                        panelZone.Visible = true;
                                    break;
                                case "PRM_UserFrom":
                                    if (pm.VisiblePossibility(perm, panelUserFrom))
                                        panelUserFrom.Visible = true;
                                    break;
                                case "PRM_UserTo":
                                    if (pm.VisiblePossibility(perm, panelUserTo))
                                        panelUserTo.Visible = true;
                                    break;
                                case "PRM_ZoneFrom":
                                    if (pm.VisiblePossibility(perm, panelZoneFrom))
                                        panelZoneFrom.Visible = true;
                                    break;
                                case "PRM_ZoneTo":
                                    if (pm.VisiblePossibility(perm, panelZoneTo))
                                        panelZoneTo.Visible = true;
                                    break;
                                //default:
                                //    MessageBox.Show("Вы нажали неизвестную букву");
                                //    break;
                            }

                        }

            }
        }
    }
}
