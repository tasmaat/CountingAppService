using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class CpsMatchingForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = new MSDataBase();

        private DataSet countingDataSet = null;
        private DataSet CPSDataSet = null;
        private DataSet dataSet = null;
        private DataSet cardDataSet, bagsDataSet, clientsDataSet, accountDataSet, encashpointDataSet, grclienDataSet = null;

        public CpsMatchingForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase.Connect();

            //countingDataSet = dataBase.GetData9("select t1.id, t2.id_account, t1.name name_counting,t2.id_currency ,t4.curr_code ,t3.name name_card,t5.id_card,iif(t2.fact_value is null,0,t2.fact_value)-t2.declared_value Razsum,t2.declared_value,t2.fact_value from t_g_counting t1 left join t_g_counting_content t2 on t1.id=t2.id_counting left join t_g_currency t4 on t2.id_currency =t4.id left join (select distinct id_counting,id_card,l2.id_currency from t_g_counting_denom l1 left join t_g_denomination l2 on l1.id_denomination=l2.id) t5 on t1.id=t5.id_counting and t2.id_currency=t5.id_currency left join t_g_cards t3 on t5.id_card=t3.id where t1.deleted='0' and t1.id_client<>30102 order by t1.fl_prov, t1.id");

            //CPSDataSet = dataBase.GetData9("select t1.id, t2.id_account, t1.name name_counting,t2.id_currency ,t4.curr_code ,t3.name name_card,t5.id_card,iif(t2.fact_value is null,0,t2.fact_value)-t2.declared_value Razsum,t2.declared_value,t2.fact_value from t_g_counting t1 left join t_g_counting_content t2 on t1.id=t2.id_counting left join t_g_currency t4 on t2.id_currency =t4.id left join (select distinct id_counting,id_card,l2.id_currency from t_g_counting_denom l1 left join t_g_denomination l2 on l1.id_denomination=l2.id) t5 on t1.id=t5.id_counting and t2.id_currency=t5.id_currency left join t_g_cards t3 on t5.id_card=t3.id where t1.deleted='0' and t1.id_client=30102 order by t1.fl_prov, t1.id");

            dgCounting.AutoGenerateColumns = false;
            
            dgCounting.Columns.Add("name_counting", "Наименование пересчета");
            dgCounting.Columns["name_counting"].Visible = true;
            dgCounting.Columns["name_counting"].DataPropertyName = "name_counting";

            //dgCounting.Columns["name_counting"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCounting.Columns.Add("id", "id");
            dgCounting.Columns["id"].Visible = false;
            dgCounting.Columns["id"].DataPropertyName = "id";

            dgCounting.Columns.Add("id_content", "id_content");
            dgCounting.Columns["id_content"].Visible = false;
            dgCounting.Columns["id_content"].DataPropertyName = "id_content";
            

            dgCounting.Columns.Add("curr_code", "Код валюты");
            dgCounting.Columns["curr_code"].Visible = true;
            dgCounting.Columns["curr_code"].DataPropertyName = "curr_code";
           // dgCounting.Columns["curr_code"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCounting.Columns.Add("id_currency", "id_currency");
            dgCounting.Columns["id_currency"].Visible = false;
            dgCounting.Columns["id_currency"].DataPropertyName = "id_currency";

            dgCounting.Columns.Add("name_card", "КР");
            dgCounting.Columns["name_card"].Visible = true;
            dgCounting.Columns["name_card"].DataPropertyName = "name_card";
            //dgCounting.Columns["name_card"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCounting.Columns.Add("id_card", "id_card");
            dgCounting.Columns["id_card"].Visible = false;
            dgCounting.Columns["id_card"].DataPropertyName = "id_card";

            dgCounting.Columns.Add("Razsum", "Расхождение");
            dgCounting.Columns["Razsum"].Visible = true;
            dgCounting.Columns["Razsum"].DataPropertyName = "Razsum";
           // dgCounting.Columns["Razsum"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCounting.Columns.Add("declared_value", "Заявленная сумма");
            dgCounting.Columns["declared_value"].Visible = true;
            dgCounting.Columns["declared_value"].DataPropertyName = "declared_value";
            //dgCounting.Columns["declared_value"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCounting.Columns.Add("fact_value", "Подсчитанная сумма");
            dgCounting.Columns["fact_value"].Visible = true;
            dgCounting.Columns["fact_value"].DataPropertyName = "fact_value";
            //dgCounting.Columns["fact_value"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dgCounting.DataSource = countingDataSet.Tables[0];
            dgCounting.RowHeadersWidth = 20;


            ///
            dgCPS.AutoGenerateColumns = false;

            dgCPS.Columns.Add("name_counting", "Наименование пересчета");
            dgCPS.Columns["name_counting"].Visible = true;
            dgCPS.Columns["name_counting"].DataPropertyName = "name_counting";
            //dgCPS.Columns["name_counting"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCPS.Columns.Add("id", "id");
            dgCPS.Columns["id"].Visible = false;
            dgCPS.Columns["id"].DataPropertyName = "id";

            dgCPS.Columns.Add("id_content", "id_content");
            dgCPS.Columns["id_content"].Visible = false;
            dgCPS.Columns["id_content"].DataPropertyName = "id_content";

            dgCPS.Columns.Add("curr_code", "Код валюты");
            dgCPS.Columns["curr_code"].Visible = true;
            dgCPS.Columns["curr_code"].DataPropertyName = "curr_code";
            //dgCPS.Columns["curr_code"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCPS.Columns.Add("id_currency", "id_currency");
            dgCPS.Columns["id_currency"].Visible = false;
            dgCPS.Columns["id_currency"].DataPropertyName = "id_currency";

            dgCPS.Columns.Add("name_card", "КР");
            dgCPS.Columns["name_card"].Visible = true;
            dgCPS.Columns["name_card"].DataPropertyName = "name_card";
            //dgCPS.Columns["name_card"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCPS.Columns.Add("id_card", "id_card");
            dgCPS.Columns["id_card"].Visible = false;
            dgCPS.Columns["id_card"].DataPropertyName = "id_card";

           

            dgCPS.Columns.Add("fact_value", "Подсчитанная сумма");
            dgCPS.Columns["fact_value"].Visible = true;
            dgCPS.Columns["fact_value"].DataPropertyName = "fact_value";
            //dgCPS.Columns["fact_value"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCPS.Columns.Add("deposit7", "deposit7");
            dgCPS.Columns["deposit7"].Visible = false;
            dgCPS.Columns["deposit7"].DataPropertyName = "deposit7";


            
            dgCPS.RowHeadersWidth = 20;

            update1();
                        

            dgCounting.Columns["Razsum"].DefaultCellStyle.Format = "### ### ### ###";
            dgCounting.Columns["declared_value"].DefaultCellStyle.Format = "### ### ### ###";
            dgCounting.Columns["fact_value"].DefaultCellStyle.Format = "### ### ### ###";
            dgCPS.Columns["fact_value"].DefaultCellStyle.Format = "### ### ### ###";


        }

        private async void CpsMatchingForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            cardDataSet = dataBase.GetData("t_g_cards");
            bagsDataSet = dataBase.GetData("t_g_bags");
            clientsDataSet = dataBase.GetData("t_g_client");
            accountDataSet = dataBase.GetData("t_g_account");
            encashpointDataSet = dataBase.GetData("t_g_encashpoint");
            grclienDataSet = dataBase.GetData("t_g_clisubfml");

            comboBox9.Text = "";
            comboBox9.DataSource = null;
            comboBox9.Items.Clear();
            comboBox9.DisplayMember = "name";
            comboBox9.ValueMember = "id";
            comboBox9.DataSource = grclienDataSet.Tables[0];
            comboBox9.SelectedIndex = -1;

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = cardDataSet.Tables[0];
            comboBox1.SelectedIndex = -1;

            comboBox2.Text = "";
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = bagsDataSet.Tables[0];
            comboBox2.SelectedIndex = -1;

            //Список клиентов
            comboBox3.Text = "";
            comboBox3.DataSource = null;
            comboBox3.Items.Clear();
            comboBox3.DisplayMember = "name";
            comboBox3.ValueMember = "id";
            comboBox3.DataSource = clientsDataSet.Tables[0];
            comboBox3.SelectedIndex = -1;

            //Список БИН
            comboBox5.Text = "";
            comboBox5.DataSource = null;
            comboBox5.Items.Clear();
            comboBox5.DisplayMember = "BIN";
            comboBox5.ValueMember = "id";
            comboBox5.DataSource = clientsDataSet.Tables[0];
            comboBox5.SelectedIndex = -1;

            //Список счетов
            comboBox4.Text = "";
            comboBox4.DataSource = null;
            comboBox4.Items.Clear();
            comboBox4.DisplayMember = "name";
            comboBox4.ValueMember = "id";
            comboBox4.DataSource = accountDataSet.Tables[0];
            comboBox4.SelectedIndex = -1;

            //Список точек инкассации
            comboBox6.Text = "";
            comboBox6.DataSource = null;
            comboBox6.Items.Clear();
            comboBox6.DisplayMember = "name";
            comboBox6.ValueMember = "id";
            comboBox6.DataSource = encashpointDataSet.Tables[0];
            comboBox6.SelectedIndex = -1;



            PrepareData();
        }

        private void filtr()
        {
            if(panel1.Visible)
            {
                panel1.Visible = false;
                button8.Text = "Показать фильтр";
            }
            else
            {
                panel1.Visible = true;
                button8.Text = "Скрыть фильтр";
            }

        }
        private void button8_Click(object sender, EventArgs e)
        {
            filtr();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox9.Text.Trim() != "") | (comboBox6.Text.Trim() != "") | (comboBox5.Text.Trim() != "") | (comboBox4.Text.Trim() != "") | (comboBox3.Text.Trim() != "") | (comboBox2.Text.Trim() != "")

                ////05.01.2020

                | (comboBox1.Text.Trim() != "")

                ////05.01.2020

                )
            {
                string s1 = "";

                if (comboBox1.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_cards t3 where name = '" + comboBox1.Text.ToString() + "' and t1.id = t3.id_counting) ";
                if (comboBox2.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_bags t3 where name = '" + comboBox2.Text.ToString() + "' and t1.id_bag = t3.id) ";
                if (comboBox3.Text.Trim() != "")
                    s1 = s1 + "and exists(select 1 from t_g_client t3 where name = '" + comboBox3.Text.ToString() + "' and t1.id_client = t3.id) ";
                if (comboBox5.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_client t3 where BIN = '" + comboBox5.Text.ToString() + "' and t1.id_client = t3.id)";
                if (comboBox4.Text.Trim() != "")
                    s1 = s1 + "and exists(select 1 from t_g_client t3 inner join t_g_account t4 on (t3.id = t4.id_client) inner join t_g_counting_content t5 on (t1.id = t5.id_counting) and(t5.id_account = t4.id) where t4.name = '" + comboBox4.Text.ToString() + "'  and t1.id_client = t3.id)";

                if (comboBox6.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_client t3 inner join t_g_account t4 on (t3.id = t4.id_client)  inner join t_g_encashpoint t5 on(t5.id = t4.id_encashpoint) inner join t_g_counting_content t6 on(t1.id = t6.id_counting) and(t6.id_account = t4.id) where t5.name = '" + comboBox6.Text.ToString() + "' and t1.id_client = t3.id)";

                ////05.01.2020
                if (comboBox9.SelectedValue != null)
                    if (comboBox9.Text.Trim() != "")
                        s1 = s1 + " and exists(select 1 from t_g_client t3  where t1.id_client=t3.id and t3.id_subfml ='" + comboBox9.SelectedValue.ToString() + "')";



                ////05.01.2020

                countingDataSet = dataBase.GetData9("select t1.id, t2.id id_content, t1.name name_counting, t2.id_currency ,t4.curr_code ,t3.name name_card, t5.id_card,iif(t2.fact_value is null, 0, t2.fact_value) - t2.declared_value Razsum,t2.declared_value,t2.fact_value from t_g_counting t1 left join t_g_counting_content t2 on t1.id = t2.id_counting left join t_g_currency t4 on t2.id_currency = t4.id left join(select distinct id_counting, id_card, l2.id_currency from t_g_counting_denom l1 left join t_g_denomination l2 on l1.id_denomination= l2.id) t5 on t1.id = t5.id_counting and t2.id_currency = t5.id_currency left join t_g_cards t3 on t5.id_card = t3.id where t1.deleted = '0' "+ s1.ToString() + " and t1.id_client <> 418 order by t1.fl_prov, t1.id ");
                                     //countingDataSet = dataBase.GetData10("select * from t_g_counting where  deleted=0 " + s1.ToString() + " order by fl_prov ");
                dgCounting.DataSource = countingDataSet.Tables[0];

                button6.BackColor = System.Drawing.Color.Yellow;
                if(pm.EnabledPossibility(perm, button6))
                    button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {


            ////09.12.2019

            //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
            countingDataSet = dataBase.GetData9("select t1.id, t2.id id_content, t1.name name_counting,t2.id_currency ,t4.curr_code ,t3.name name_card,t5.id_card,iif(t2.fact_value is null,0,t2.fact_value)-t2.declared_value Razsum,t2.declared_value,t2.fact_value from t_g_counting t1 left join t_g_counting_content t2 on t1.id=t2.id_counting left join t_g_currency t4 on t2.id_currency =t4.id left join (select distinct id_counting,id_card,l2.id_currency from t_g_counting_denom l1 left join t_g_denomination l2 on l1.id_denomination=l2.id) t5 on t1.id=t5.id_counting and t2.id_currency=t5.id_currency left join t_g_cards t3 on t5.id_card=t3.id where t1.deleted='0' and t1.id_client<>418 order by t1.fl_prov, t1.id");

            ////09.12.2019


            dgCounting.DataSource = countingDataSet.Tables[0];

            ////05.01.2020
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox9.Text = "";
            button6.Enabled = false;

            button6.BackColor = SystemColors.Control;
            ////05.01.2020
        }

        private void dgCPS_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //dgCPS.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Beige;

            for (int i = 0; i < CPSDataSet.Tables[0].Rows.Count; i++)
                if (CPSDataSet.Tables[0].Rows[i]["deposit7"].ToString().Trim() == "1")
                    dgCPS.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
            //    dgCPS.Rows[2].DefaultCellStyle.BackColor = Color.Red;
            //else
            //dgCPS.Rows[3].DefaultCellStyle.BackColor = Color.Blue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("id=" + dgCPS.CurrentRow.Cells["id_content"].Value.ToString());
            //MessageBox.Show("name=" + dgCounting.CurrentRow.Cells["id"].Value.ToString());  //.Value.ToString());//.RowIndex.ToString());// ;
        }

        

        private void PrepareData()
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(dgCounting!=null & dgCPS!=null)
                if(dgCounting.Rows.Count>0 & dgCPS.Rows.Count>0)
                    if(dgCounting.CurrentCell.RowIndex > -1 & dgCPS.CurrentCell.RowIndex>-1)
                    {
                       if(dgCounting.CurrentRow.Cells["id_currency"].Value.ToString() != dgCPS.CurrentRow.Cells["id_currency"].Value.ToString()) //  countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_currency"].ToString() != CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id_currency"].ToString())
                       {
                             MessageBox.Show("Код валюты не соответствует для сопоставления");
                            return;
                       }

                        dataSet = dataBase.GetData9("CpsMatching " + dgCPS.CurrentRow.Cells["id"].Value.ToString().Trim() +   //CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id"].ToString().Trim() +
                         ", " + dgCPS.CurrentRow.Cells["id_currency"].Value.ToString().Trim() +   //CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id_currency"].ToString().Trim()+
                         "," + dgCounting.CurrentRow.Cells["id"].Value.ToString().Trim());   //countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString().Trim());

                        dataSet = dataBase.GetData9("CpsMatchingDelete " + dgCPS.CurrentRow.Cells["id"].Value.ToString() +    //CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id"].ToString().Trim() +
                        ", " + dgCPS.CurrentRow.Cells["id_currency"].Value.ToString().Trim());     //CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id_currency"].ToString().Trim());


                        dataSet = dataBase.GetData9("DELETE FROM [dbo].[t_g_counting_content] WHERE id=" + dgCPS.CurrentRow.Cells["id_content"].Value.ToString().Trim());// CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id_content"].ToString().Trim());

                        dataSet = dataBase.GetData9("select count(*) [count] from t_g_counting_content where id_counting=" + dgCPS.CurrentRow.Cells["id"].Value.ToString().Trim()); //CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id"].ToString().Trim());

                        if (dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("count")).First<Int32>() == 0)
                            dataSet = dataBase.GetData9("delete t_g_counting where id=" + dgCPS.CurrentRow.Cells["id"].Value.ToString().Trim());// CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id"].ToString().Trim());
                        dataSet = dataBase.GetData9("Select * from t_g_counting where id=" + dgCounting.CurrentRow.Cells["id"].Value.ToString().Trim());//countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString().Trim());
                        
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            Int64 id_bag = Convert.ToInt64(dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_bag")).First<Int64>());
                            Int64 id_zona = Convert.ToInt64(dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_zona_create")).First<Int64>());
                            Int64 id_shift = Convert.ToInt64(dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_shift_current")).First<Int64>());
                            Int64 id_card = Convert.ToInt64(dgCPS.CurrentRow.Cells["id_card"].Value.ToString());//CPSDataSet.Tables[0].Rows[dgCPS.CurrentCell.RowIndex]["id_card"].ToString().Trim());
                            Int64 id_counting = Convert.ToInt64(dgCounting.CurrentRow.Cells["id"].Value.ToString());// countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString());
                            dataSet = dataBase.GetData9("update t_g_cards set id_counting ="+id_counting.ToString()+", id_user_create=last_user_update, fl_obr=1, id_bag= " + id_bag.ToString() + ", id_zona_create=" + id_zona.ToString() +
                                ", id_shift_create=" + id_shift.ToString() + ", id_shift_current=" + id_shift.ToString() +
                                " where id="+id_card.ToString());

                        }
                            update1();
                    }
                    else
                    {
                        MessageBox.Show("Ны выбраны подгатовки для сопоставления");
                    }
        }

        private void update1()
        {
            countingDataSet = dataBase.GetData9("select t1.id, t2.id id_content, t1.name name_counting,t2.id_currency ,t4.curr_code ,t3.name name_card,t5.id_card,iif(t2.fact_value is null,0,t2.fact_value)-t2.declared_value Razsum,t2.declared_value,t2.fact_value from t_g_counting t1 left join t_g_counting_content t2 on t1.id=t2.id_counting left join t_g_currency t4 on t2.id_currency =t4.id left join (select distinct id_counting,id_card,l2.id_currency from t_g_counting_denom l1 left join t_g_denomination l2 on l1.id_denomination=l2.id) t5 on t1.id=t5.id_counting and t2.id_currency=t5.id_currency left join t_g_cards t3 on t5.id_card=t3.id where t1.deleted='0' and t1.id_client<>418 order by t1.fl_prov, t1.id");

            CPSDataSet = dataBase.GetData9("select t1.id, t2.id id_content, t1.name name_counting,t2.id_currency ,t4.curr_code ,t3.name name_card,t5.id_card,iif(t2.fact_value is null,0,t2.fact_value)-t2.declared_value Razsum,t2.declared_value,t2.fact_value, t1.deposit7 from t_g_counting t1 left join t_g_counting_content t2 on t1.id=t2.id_counting left join t_g_currency t4 on t2.id_currency =t4.id left join (select distinct id_counting,id_card,l2.id_currency from t_g_counting_denom l1 left join t_g_denomination l2 on l1.id_denomination=l2.id) t5 on t1.id=t5.id_counting and t2.id_currency=t5.id_currency left join t_g_cards t3 on t5.id_card=t3.id where t1.deleted='0' and t1.id_client=418 order by t1.fl_prov, t1.id");

            dgCounting.DataSource = countingDataSet.Tables[0];
            dgCPS.DataSource = CPSDataSet.Tables[0];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            panel1.Visible = true;
            filtr();
            
            update1();
        }
    }
}
