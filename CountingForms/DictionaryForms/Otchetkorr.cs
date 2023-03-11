using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;
//using FastReport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class Otchetkorr : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = null;
        private DataSet v1  = null;
        private DataSet clientsDataSet2 = null;

        ReportDocument reportDocument1;
        CrystalReportViewer crystalReportViewer1;

        public int numzapr=0;

        public string strzapros;
        public Otchetkorr()
        {

            dataBase = new MSDataBase();
            dataBase.Connect();
           
            

            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void Otchetkorr_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            clientsDataSet2 = dataBase.GetData("t_g_client");

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = clientsDataSet2.Tables[0];
            comboBox1.SelectedIndex = -1;

            string sql1 = "";
            if (numzapr == 1)
            {

                string s1 = "";
                string s2 = "";

                sql1 = String.Format(strzapros, s1, s2);

            }
            v1 = dataBase.GetData9(sql1);
            // dgList.AutoGenerateColumns = false;
            //  dgList.DataSource = null;
            dgList.DataSource = v1.Tables[0];

            //////19.02.2020

            dgList.Columns[7].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[8].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[9].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[10].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[11].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[12].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[13].DefaultCellStyle.Format = "### ### ### ###";
            dgList.Columns[14].DefaultCellStyle.Format = "### ### ### ###";            

            //////19.02.2020
            


            /*
            if (v1 != null)
            {
                if (v1.Tables[0] != null)
                {

                    if (v1.Tables[0].Rows.Count > 0)
                    {

                        for (int i1 = 0; i1 < v1.Tables[0].Columns.Count; i1++)
                        {

                            dgList.Columns.Add(v1.Tables[0].Columns[i1].ColumnName, v1.Tables[0].Columns[i1].ColumnName);


                        }
                        dgList.DataSource = v1.Tables[0];
                    }

                }
            }

            
            */
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string sql1 = "";
            if (numzapr == 1)
            {

                string s1 = "";
                string s2 = "";

                sql1 = String.Format(strzapros, s1, s2);

            }
            v1 = dataBase.GetData9(sql1);

            dgList.DataSource = v1.Tables[0];
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string sql1 = "";
            if (numzapr == 1)
            {
                string s3 = "";

                if (clientsDataSet2 != null)
                {
                    if (clientsDataSet2.Tables[0] != null)
                    {
                        if (clientsDataSet2.Tables[0].Rows.Count > 0)
                        {

                            if (comboBox1.SelectedValue != null)
                            {
                                if (comboBox1.SelectedIndex > -1)
                                {
                                    s3 = " and t1.id_client='" + comboBox1.SelectedValue.ToString() + "'";
                                }

                            }
                        }
                    }

                }

                string s1 = " and t70.lastupdate between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value.ToString() + "'";
                string s2 = " and t5.lastupdate between '"+ dateTimePicker1.Value.ToString()+ "' and '"+ dateTimePicker2.Value.ToString() + "'"+s3.ToString();

                sql1 = String.Format(strzapros, s1, s2);

            }
            v1 = dataBase.GetData9(sql1);
            
            dgList.DataSource = v1.Tables[0];
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>
            {
                "name",
                "othername",
                "username",
                "nousername"
            };
            string username = lst.Find((x) => x == "username1");
            //Console.WriteLine(username);

            MessageBox.Show(username);
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            // ReportDocument reportDocument1 = new ReportDocument();
             reportDocument1 = new ReportDocument();
            //Загружаем шаблон отчета
            reportDocument1.Load(Application.StartupPath.ToString() + "\\Reportkorr2.rpt");// !!! Должен стоять перед загрузкой данными
            //// Заталкиваем (Push) данные по шаблону 
            reportDocument1.SetDataSource(v1.Tables[0]);// !!! Должен стоять после загрузки отчета
            //reportDocument.Database.Tables["Customers"].SetDataSource(dataSet.Tables["Customers"]);// Вариант

            //reportDocument1.Load(@"D:\report\итоги_пересчета.rpt");
            //DateTime id=DateTime.Now;
            //string id_str = id.ToString().Trim();
            //MessageBox.Show(id.ToString());
            // reportDocument1.SetParameterValue("PRM_BeginDateTime", id_str.ToString());
            // CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();
            crystalReportViewer1 = new CrystalReportViewer();

            //crystalReportViewer1.Dock = DockStyle.Fill;

           // crystalReportViewer1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета

            // Донастраиваем объект отображения отчета
            crystalReportViewer1.ShowGroupTreeButton = false;// Отключить кнопку
                                                             // crystalReportViewer1.DisplayGroupTree = false;   // Отключить панель

            if (pm.VisiblePossibility(perm, crystalReportViewer1))
                crystalReportViewer1.Visible = true;

            crystalReportViewer1.Parent = panel3;
            crystalReportViewer1.Dock = DockStyle.Fill;
            // Вариант. Если передается параметр Test типа DateTime
            //reportDocument1.SetParameterValue("Test", DateTime.Now);
            //crystalReportViewer1.ReportSource = reportDocument;
        }
    }
}
