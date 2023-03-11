using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountingDB;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataExchange;

namespace CountingForms.ParentForms
{
    public partial class Counterfeit : Form
    {
        //private DataSet counterfeitDataSet = null;
        public DataSet counterfeitDataSet { get; set; }
        public DataSet factorNotesDataSet { get; set; }

        private MSDataBase dataBase = null;
        private DataSet denominationDataSet = null;
        private Int64 counting_id = -1;
        private Int64 card_id = -1;
        private long notes_id = -1;
        private bool newvalue = false;
        private Int64 condition_id = -1;
        private DataSet conditionDataSet = null, conditionFactorDataSet = null;
        //private DataSet d1 = null;
        public Counterfeit()
        {
            InitializeComponent();

        }

        public Counterfeit(DataSet denominationDataSet, Int64 counting_id, Int64 card_id)
        {
            dataBase = new MSDataBase();
            dataBase.Connect();
            InitializeComponent();
            counterfeitDataSet = dataBase.GetData9("select t1.id as id_notes, t1.serial, t1.number, t2.* from t_w_notes t1 " +
                    " left join t_g_counting_denom t2 on t1.id_counting_denom = t2.id " +
                    " where t2.counterfeit = 1 and t2.id_card = " + card_id.ToString()+ " order by id_denomination, id_condition");

            factorNotesDataSet = dataBase.GetData9("select t1.* from t_w_factor_notes t1  " +
               " left join t_w_notes t2 on t1.id_notes = t2.id " +
               " left join t_g_counting_denom t3 on t2.id_counting_denom = t3.id " +
               " where t3.id_card = " + card_id.ToString());

            this.denominationDataSet = denominationDataSet;
            this.counting_id = counting_id;
            this.card_id = card_id;
            //this.d1 = dataBase.GetData9("select * from t_g_condition_factor");// where id_condition=" + condition_id);

            PrepareComponents();
            
        }
                
        private void PrepareComponents()
        {
            dgCounterfiet.DataSource = null;
            dgCounterfiet.Rows.Clear();
            dgCounterfiet.Columns.Clear();


            dgCounterfiet.AutoGenerateColumns = false;

            DataGridViewComboBoxColumn denomColumn = new DataGridViewComboBoxColumn();
            denomColumn.Name = "denom";
            denomColumn.ValueMember = "id";
            denomColumn.DisplayMember = "name";
            denomColumn.DataPropertyName = "id_denomination";
            denomColumn.HeaderText = "Номинал";
            denomColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            denomColumn.Visible = true;

            denomColumn.DataSource = denominationDataSet.Tables[0];
            dgCounterfiet.Columns.Add(denomColumn);

            dgCounterfiet.Columns.Add("serial", "Серия");
            dgCounterfiet.Columns["serial"].Visible = true;
            dgCounterfiet.Columns["serial"].DataPropertyName = "serial";

            dgCounterfiet.Columns.Add("number", "Номер");
            dgCounterfiet.Columns["number"].Visible = true;
            dgCounterfiet.Columns["number"].Width = 130;
            dgCounterfiet.Columns["number"].DataPropertyName = "number";
            
            dgCounterfiet.Columns.Add("id_notes", "id_notes");
            dgCounterfiet.Columns["id_notes"].Visible = false;
            dgCounterfiet.Columns["id_notes"].DataPropertyName = "id_notes";
            //dgCounterfiet.Columns.Add("description", "Примечания");
            //dgCounterfiet.Columns["description"].Visible = true;
            //dgCounterfiet.Columns["description"].DataPropertyName = "description";

            dgCounterfiet.DataSource = counterfeitDataSet.Tables[0];



            cbDenomination.ValueMember = "id";
            cbDenomination.DisplayMember = "name";
            cbDenomination.DataSource = denominationDataSet.Tables[0];

            dgCondition.AutoGenerateColumns = false;
            dgCondition.ColumnHeadersHeight = 10;
            dgCondition.RowHeadersWidth = 10;

            DataGridViewCheckBoxColumn dgConditionBoolColumn = new DataGridViewCheckBoxColumn();
            dgConditionBoolColumn.Name = "state";
            dgConditionBoolColumn.HeaderText = "Исп.";
            dgCondition.Columns.Add(dgConditionBoolColumn);
            dgCondition.Columns["state"].Visible = true;
            dgCondition.Columns["state"].Width = 50;

            dgCondition.Columns.Add("name", "Наименование");
            dgCondition.Columns["name"].Visible = true;
            dgCondition.Columns["name"].DataPropertyName = "name";
            dgCondition.Columns["name"].Width = 200;
            dgCondition.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCondition.Columns.Add("id", "id");
            dgCondition.Columns["id"].Visible = false;
            dgCondition.Columns["id"].DataPropertyName = "id";

            dgCondition.Columns.Add("id_notes", "id_notes");
            dgCondition.Columns["id_notes"].Visible = false;
            dgCondition.Columns["id_notes"].DataPropertyName = "id_notes";


            conditionDataSet = dataBase.GetData9("select distinct t1.* from [t_g_condition] t1 right join t_g_condition_factor t2 on t1.id=t2.id_condition");
            // Select * from [CountingDB].[dbo].[t_g_condition] where id_stat<>1");
            dgCondition.DataSource = conditionDataSet.Tables[0];




            dgConditionFactor.AutoGenerateColumns = false;
            dgConditionFactor.ColumnHeadersHeight = 10;
            dgConditionFactor.RowHeadersWidth = 10;

            DataGridViewCheckBoxColumn dgConditionFactorBoolColumn = new DataGridViewCheckBoxColumn();
            dgConditionFactorBoolColumn.Name = "state";
            dgConditionFactorBoolColumn.HeaderText = "Исп.";
            dgConditionFactor.Columns.Add(dgConditionFactorBoolColumn);
            dgConditionFactor.Columns["state"].Visible = true;
            dgConditionFactor.Columns["state"].Width = 50;

            dgConditionFactor.Columns.Add("short_evidence", "Признак");
            dgConditionFactor.Columns["short_evidence"].Visible = true;
            dgConditionFactor.Columns["short_evidence"].DataPropertyName = "short_evidence";
            dgConditionFactor.Columns["short_evidence"].Width = 200;
            dgConditionFactor.Columns["short_evidence"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgConditionFactor.Columns.Add("id_condition", "id_condition");
            dgConditionFactor.Columns["id_condition"].Visible = false;
            dgConditionFactor.Columns["id_condition"].DataPropertyName = "id_condition";

            dgConditionFactor.Columns.Add("id", "id");
            dgConditionFactor.Columns["id"].Visible = false;
            dgConditionFactor.Columns["id"].DataPropertyName = "id";


            dgConditionFactor.Columns.Add("evidence", "evidence");
            dgConditionFactor.Columns["evidence"].Visible = false;
            dgConditionFactor.Columns["evidence"].DataPropertyName = "evidence";


            conditionFactorDataSet = dataBase.GetData9("Select * from [CountingDB].[dbo].[t_g_condition_factor]");
            dgConditionFactor.DataSource = conditionFactorDataSet.Tables[0];

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbDenomination.SelectedIndex = -1;
            tbSerial.Text = String.Empty;
            tbNumber.Text = String.Empty;
            newvalue = true;
            for (int i = 0; i < dgConditionFactor.Rows.Count; i++)
            {
                dgConditionFactor[0, i].Value = false;
            }
            for (int i = 0; i < dgCondition.Rows.Count; i++)
            {
                dgCondition[0, i].Value = false;
            }
            dgCondition[0, 0].Value = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataSet d1;
            Console.WriteLine("condition_id 555="+ condition_id);
            //condition_id = Convert.ToInt64(dgCondition.CurrentRow.Cells["id"].Value);
            Console.WriteLine("condition_id 555=="+ condition_id);
            long counting_denom_id;
            
            int denomination_id = Convert.ToInt32(cbDenomination.SelectedValue);
            Console.WriteLine("denomination_id 22 ="+ denomination_id);
            
            if (!counterfeitDataSet.Tables[0].AsEnumerable().Any(x=>x.Field<Int64>("id_denomination")== denomination_id & x.Field<Int64>("id_condition") == condition_id))
            {
                //Создаем запись в t_g_counting_denom и возвращаем condition_denom_id
                counting_denom_id = Convert.ToInt64(
                    dataBase.GetData9("INSERT INTO t_g_counting_denom ([id_counting],[id_card],[id_denomination]" +
                    ",[id_condition],[creation], [lastupdate],[last_user_update], [source],[counterfeit])" +                   
                    " VALUES("+counting_id+","+card_id+","+ denomination_id +
                    ", "+condition_id+", getdate(), getdate(), "+DataExchange.CurrentUser.CurrentUserId+",0,1); " +
                    " SELECT SCOPE_IDENTITY();").Tables[0].Rows[0][0]);
                Console.WriteLine("condition_denom_id="+ counting_denom_id);

            }
            else
            {
                // Из таблицы t_g_counting_denom получаем condition_denom_id
                 d1 = dataBase.GetData9(" select * from t_g_counting_denom  where counterfeit=1" +
                                        " and id_denomination = " + denomination_id +
                                        " and id_card = " + card_id +
                                        " and id_condition = " + condition_id +
                                        " and id_counting = " + counting_id);
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("d1.rows.count ="+d1.Tables[0].Rows.Count);
                Console.WriteLine("------------------------------------------------------");
                counting_denom_id = Convert.ToInt64(d1.Tables[0].Rows[0]["id"]);
            }
            Console.WriteLine("counting_denom_id =" + counting_denom_id);
            //Создаем запись в t_w_notes и возвращаем notes_id
            long notes_id = Convert.ToInt64(
                dataBase.GetData9("INSERT INTO t_w_notes ([id_counting_denom], [number], [serial])" +                
                " Values (" + counting_denom_id + ", '" + tbNumber.Text.Trim() + "','" + tbSerial.Text.Trim() + "'); " +
                " SELECT SCOPE_IDENTITY();").Tables[0].Rows[0][0]);

            //Обновляем кол-во сомнительных номиналов
            d1 = dataBase.GetData9("UPDATE t_g_counting_denom SET  t_g_counting_denom.[count]=(SELECT count(t_w_notes.id_counting_denom) FROM t_w_notes WHERE t_g_counting_denom.id = t_w_notes.id_counting_denom), lastupdate=GETDATE() where counterfeit=1");
            
            
            
            Console.WriteLine("notes_id=" + notes_id);

            //Создаем запись t_w_factor_notes
            for (int j = 0; j < dgConditionFactor.Rows.Count; j++)
                if (Convert.ToBoolean(dgConditionFactor[0, j].Value))
                {
                     d1 = dataBase.GetData9(" INSERT INTO [t_w_factor_notes] ([id_notes],[id_condition_factor])" +
                                                  " VALUES(" + notes_id + "," + dgConditionFactor[3, j].Value + ")");                
                }
            UpdateDataSet();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Console.WriteLine("condition_id 777 ="+ condition_id);
           // condition_id = Convert.ToInt64(dgCondition.CurrentRow.Cells["id"].Value);
            Console.WriteLine("condition_id 777 = "+ condition_id);
            long counting_denom_id;
            if (!counterfeitDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_denomination") == Convert.ToInt32(cbDenomination.SelectedValue) & x.Field<Int64>("id_condition") == condition_id))
            {
                Console.WriteLine("cbDenomination.SelectedValue="+ cbDenomination.SelectedValue);
                //Создаем запись в t_g_counting_denom и возвращаем condition_denom_id
                counting_denom_id = Convert.ToInt64(
                   dataBase.GetData9("INSERT INTO t_g_counting_denom ([id_counting],[id_card],[id_denomination]" +
                   ",[id_condition],[creation], [lastupdate],[last_user_update], [source],[counterfeit])" +
                   " VALUES(" + counting_id + "," + card_id + "," + cbDenomination.SelectedValue +
                   ", " + condition_id + ", getdate(), getdate(), " + DataExchange.CurrentUser.CurrentUserId + ",0,1); " +
                   " SELECT SCOPE_IDENTITY();").Tables[0].Rows[0][0]);
            }
            else
            {
                // Из таблицы t_g_counting_denom получаем condition_denom_id
                counting_denom_id = Convert.ToInt64(
                    dataBase.GetData9(" select * from t_g_counting_denom  where counterfeit=1" +
                                        " and id_denomination = " + cbDenomination.SelectedValue +
                                        " and id_card = " + card_id +
                                        " and id_condition = "+ condition_id+
                                        " and id_counting = " + counting_id).Tables[0].Rows[0]["id"]);
            }

            //Обновляем данные в t_w_notes
            //notes_id = Convert.ToInt64(dgCondition.CurrentRow.Cells["id_notes"].Value);
            Console.WriteLine("notes_id"+notes_id);
            DataSet d1 = dataBase.GetData9("update t_w_notes set id_counting_denom="+ counting_denom_id +
                     ", number = '"+tbNumber.Text.Trim()+
                     "', serial = '"+tbSerial.Text.Trim()+
                     "' where id = "+notes_id);
            //Обновляем кол-во сомнительных номиналов 
            d1 = dataBase.GetData9("UPDATE t_g_counting_denom SET  t_g_counting_denom.[count]=(SELECT count(t_w_notes.id_counting_denom) FROM t_w_notes WHERE t_g_counting_denom.id = t_w_notes.id_counting_denom), lastupdate=GETDATE() where counterfeit=1");
            //Удаляем из t_g_counting_denom не нужные 0 записи
            d1 = dataBase.GetData9("delete from t_g_counting_denom where [count]=0 and counterfeit=1");

            //Обновляем данные в t_w_factor_notes
            d1 = dataBase.GetData9("delete from t_w_factor_notes where id_notes="+notes_id);
            for (int j = 0; j < dgConditionFactor.Rows.Count; j++)
                if (Convert.ToBoolean(dgConditionFactor[0, j].Value))
                {
                    d1 = dataBase.GetData9(" INSERT INTO [t_w_factor_notes] ([id_notes],[id_condition_factor])" +
                                                  " VALUES(" + notes_id + "," + dgConditionFactor[3, j].Value + ")");
                }
            UpdateDataSet();
        }

        private void UpdateDataSet()
        {
            counterfeitDataSet = dataBase.GetData9("select t1.id as id_notes, t1.serial, t1.number, t2.* from t_w_notes t1 " +
                    " left join t_g_counting_denom t2 on t1.id_counting_denom = t2.id " +
                    " where t2.counterfeit = 1 and t2.id_card = " + card_id.ToString()+ " order by id_denomination, id_condition ");

            factorNotesDataSet = dataBase.GetData9("select t1.* from t_w_factor_notes t1  " +
               " left join t_w_notes t2 on t1.id_notes = t2.id " +
               " left join t_g_counting_denom t3 on t2.id_counting_denom = t3.id " +
               " where t3.id_card = " + card_id.ToString());

            dgCounterfiet.DataSource = counterfeitDataSet.Tables[0];
        }

        private void dgCounterfiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgCounterfiet.CurrentCell.Selected = true;            
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DataSet d1 = dataBase.GetData9("delete from t_w_factor_notes where id_notes=" + notes_id);
            d1 = dataBase.GetData9("delete from t_w_notes where id =" + notes_id);

            //Обновляем кол-во сомнительных номиналов
            d1 = dataBase.GetData9("UPDATE t_g_counting_denom SET  t_g_counting_denom.[count]=(SELECT count(t_w_notes.id_counting_denom) FROM t_w_notes WHERE t_g_counting_denom.id = t_w_notes.id_counting_denom), lastupdate=GETDATE() where counterfeit=1");
            //Удаляем из t_g_counting_denom не нужные 0 записи
            d1 = dataBase.GetData9("delete from t_g_counting_denom where [count]=0 and counterfeit=1");
            UpdateDataSet();


            //counterfeitDataSet.Tables[0].Rows[dgCounterfiet.CurrentCell.RowIndex].Delete();
        }

        private void dgCondition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgCondition_Run();
        }

        private void dgCondition_Run()
        {
            tbSerial.Text = "";
            tbNumber.Text = "";
            
            condition_id = Convert.ToInt64(dgCondition.CurrentRow.Cells["id"].Value);
            Console.WriteLine("condition_id111="+ condition_id);

            //newvalue = true;
            for (int i = 0; i < dgCondition.Rows.Count; i++)
                dgCondition[0, i].Value = false;

            dgCondition.CurrentRow.Cells["state"].Value = true;
            

            conditionFactorDataSet = dataBase.GetData9("select * from t_g_condition_factor where id_condition=" + condition_id);
            if (conditionFactorDataSet != null)
                if (conditionFactorDataSet.Tables[0] != null)
                    if (conditionFactorDataSet.Tables[0].Rows.Count > 0 & condition_id>0)
                    {
                        dgConditionFactor.DataSource = conditionFactorDataSet.Tables[0];

                        for (int i = 0; i < dgConditionFactor.Rows.Count; i++)
                            dgConditionFactor[0, i].Value = false;
                    }
                    else
                    {
                        dgConditionFactor.DataSource = null;
                    }

            
        }
                
        private void stateDgConditionFactor()
        {
            if (Convert.ToBoolean(dgConditionFactor.CurrentRow.Cells["state"].Value) != true)
            {
                dgConditionFactor.CurrentRow.Cells["state"].Value = true;
            }
            else
                dgConditionFactor.CurrentRow.Cells["state"].Value = false;
        }

        private void dgConditionFactor_SelectionChanged(object sender, EventArgs e)
        {          
            tbEvidence.Text = dgConditionFactor.CurrentRow.Cells["evidence"].Value.ToString();
        }

        private void dgConditionFactor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            stateDgConditionFactor();
        }

        private void cbDenomination_MouseEnter(object sender, EventArgs e)
        {
        }

        private void cbDenomination_MouseDown(object sender, MouseEventArgs e)
        {
            tbSerial.Text = "";
            tbNumber.Text = "";            

            for (int i = 0; i < dgCondition.Rows.Count; i++)
                dgCondition[0, i].Value = false;

            dgCondition[0, 0].Value = true;
            dgCondition.Rows[0].Selected = true;
            dgCondition.CurrentRow.Cells["state"].Value = true;
            condition_id = Convert.ToInt64(dgCondition[0, 0].Value);

            DataSet d1 = dataBase.GetData9("select * from t_g_condition_factor where id_condition=" + condition_id);
            if (d1 != null)
                if (d1.Tables[0] != null)
                    if (d1.Tables[0].Rows.Count > 0)
                    {
                        dgConditionFactor.DataSource = d1.Tables[0];
                        for (int i = 0; i < dgConditionFactor.Rows.Count; i++)
                            dgConditionFactor[0, i].Value = false;
                    }
                    else
                    {
                        dgConditionFactor.DataSource = null;
                    }
        }

        private void DgCounterfiet_SelectionChanged(object sender, EventArgs e)
        {           
            if (dgCounterfiet.CurrentCell != null)
            {
                tbSerial.Text = counterfeitDataSet.Tables[0].Rows[dgCounterfiet.CurrentCell.RowIndex]["serial"].ToString().Trim();
                tbNumber.Text = counterfeitDataSet.Tables[0].Rows[dgCounterfiet.CurrentCell.RowIndex]["number"].ToString().Trim();
                cbDenomination.SelectedValue = counterfeitDataSet.Tables[0].Rows[dgCounterfiet.CurrentCell.RowIndex]["id_denomination"].ToString();
                int denomination_id = Convert.ToInt32(cbDenomination.SelectedValue);
                Console.WriteLine("denomination_id 11 ="+ denomination_id);
                this.condition_id = Convert.ToInt64(counterfeitDataSet.Tables[0].Rows[dgCounterfiet.CurrentCell.RowIndex]["id_condition"]);
                Console.WriteLine("condition_id 222=" + condition_id);
                this.notes_id = Convert.ToInt64(counterfeitDataSet.Tables[0].Rows[dgCounterfiet.CurrentCell.RowIndex]["id_notes"]);
                for (int i = 0; i < dgCondition.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgCondition[2, i].Value) == condition_id)
                    {
                        dgCondition[0, i].Value = true;
                        dgCondition.Rows[i].Selected = true;
                    }
                    else
                        dgCondition[0, i].Value = false;
                }
                detal();
            }
        }
    private void detal()
        {
            //condition_id = Convert.ToInt64(dgCondition.CurrentRow.Cells["id"].Value);
            conditionFactorDataSet = dataBase.GetData9("select * from t_g_condition_factor where id_condition=" + condition_id);
            if (conditionFactorDataSet != null && conditionFactorDataSet.Tables[0] != null)
                if (conditionFactorDataSet.Tables[0].Rows.Count > 0)
                {

                    dgConditionFactor.DataSource = conditionFactorDataSet.Tables[0];
                    for (int i = 0; i < dgConditionFactor.Rows.Count; i++)
                        dgConditionFactor[0, i].Value = false;
                    if (!newvalue)
                        detal2();
                }
                else
                {
                    dgConditionFactor.DataSource = null;
                }


        }

        private void btnReading_Click(object sender, EventArgs e)
        {
            string data = new StreamReader(@"mr.rt", Encoding.Default).ReadToEnd();
            string[] pdata = data.Split(':');
            tbSerial.Text = pdata[0];
            tbNumber.Text = pdata[1];
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument reportDocument1 = new ReportDocument();
            reportDocument1.Load(@"C:\\report\\act_of_doubtful_banknotes.rpt");

            StreamReader studIni = new StreamReader(@"C:\\report\\act_of_doubtful_banknotes.ini", UTF8Encoding.Default);
            string x = studIni.ReadToEnd();
            //string[] 
            string[] y = x.Split('\n');
            string bag_name = "";
            DataSet d1 = dataBase.GetData9("select t1.[name] from t_g_bags t1 left join t_g_cards t2 on t1.id=t2.id_bag where t2.id="+card_id);
            if (d1 != null & d1.Tables[0] != null & d1.Tables[0].Rows.Count > 0)
                bag_name = d1.Tables[0].Rows[0][0].ToString();

            for (int i = 0; i < y.Length; i++)
            {
                switch (y[i].Trim())
                {
                    case "PRM_Bag":
                        if (bag_name != "")
                            reportDocument1.SetParameterValue(i, bag_name);
                        break;
                    case "PRM_Shift":
                        reportDocument1.SetParameterValue(i, DataExchange.CurrentUser.CurrentUserShift);
                        break;
                }
            }

            reportDocument1.SetDatabaseLogon(dataBase.log, dataBase.par);
            //string myReportName = "C:\sales.pdf";
            string date = DateTime.Now.ToShortDateString().ToString().Trim();
            string time = DateTime.Now.ToLongTimeString().ToString().Trim();
            string datetime = " " + date + "-" + time.Replace(':', '.');
            //MessageBox.Show(datetime);

            if (!Directory.Exists(@"D:\\Отчеты\"))
            {
                Directory.CreateDirectory(@"D:\\Отчеты\");
            }

            reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\Отчеты\Акт по сомнительным банкнотам" + datetime +/*"."+time+*/".pdf");


            CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();

            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета


            ReportShowForm reportShowForm = new ReportShowForm();

            reportShowForm.Text = "Акт по сомнительным банкнотам";
            reportShowForm.name = "Акт по сомнительным банкнотам";
            reportShowForm.crystalReportViewer1 = crystalReportViewer1;
            reportShowForm.reportDocument = reportDocument1;
            reportShowForm.Show();
        }


        private void detal2()
        {
            int condition_factor_id;
            if (notes_id != -1)
            {
                for (int i = 0; i < dgConditionFactor.Rows.Count; i++)
                {
                    condition_factor_id = Convert.ToInt32(dgConditionFactor[3, i].Value);

                    if (factorNotesDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_condition_factor") == condition_factor_id && x.Field<Int64>("id_notes") == notes_id))
                        dgConditionFactor[0, i].Value = true;
                    else
                        dgConditionFactor[0, i].Value = false;


                }
            }
        }
    }
}
