using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.ParentForms;
using CountingForms.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataExchange;
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
//using CountingForms.ParentForms;


namespace CountingForms.DictionaryForms
{
    public partial class SendCashForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private DataSet d1 = null;
        private DataSet d2 = null;
        private DataSet d3 = null;
        private DataSet d4 = null;
        private DataSet d5 = null;
        private DataSet d6 = null;
        private DataSet d7 = null;
        private DataSet d11,d12,d13 = null;
        private DataSet d22 = null;
        
        protected DataSet dataSet = null;
        //protected DataSet dataSetInfo = null;
        protected DataSet dataSet1 = null;
        protected DataSet dataSet2 = null;
        protected DataSet dataSet3 = null;
        protected DataSet dataSet4 = null;
        protected DataSet currencyDataSet = null;
        private DataSet countDenomDataSet = null;
        private int b=1;
        private MSDataBase dataBase = new MSDataBase();
        //bool bool1 = true;
       

        public SendCashForm()
        {            
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase.Connect();

            dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
           // dataSetInfo = dataBase.GetData9("selectListInfo");

            d1 = dataBase.GetData9("SELECT id, [user_name]  FROM[CountingDB].[dbo].[t_g_user]  where  deleted is null");
            d2 = dataBase.GetData9("SELECT [branch_name], [id] FROM [CountingDB].[dbo].[t_g_cashcentre]   where tipsp1 = 2");
            d3 = dataBase.GetData9("SELECT [branch_name], [id] FROM [CountingDB].[dbo].[t_g_cashcentre]   where tipsp1 = 3");
            d4 = dataBase.GetData9("SELECT [user_name], [id]  FROM[CountingDB].[dbo].[t_g_user]  where deleted is null");
            d5 = dataBase.GetData9("SELECT [branch_name], [id] FROM [CountingDB].[dbo].[t_g_cashcentre]   where tipsp1 = 2");
            d6 = dataBase.GetData9("SELECT [branch_name], [id] FROM [CountingDB].[dbo].[t_g_cashcentre]   where tipsp1 = 3");
            //d7 = dataBase.GetData9("SELECT DISTINCT id_counting FROM t_g_dvdeneg");
            d7 = dataBase.GetData9("SELECT TOP (1000) [id],[description] FROM [CountingDB].[dbo].[t_g_status]");
            d11 = dataBase.GetData9("SELECT id, [user_name]  FROM[CountingDB].[dbo].[t_g_user]  where deleted is null");
            d12 = dataBase.GetData9("SELECT id, [user_name]  FROM[CountingDB].[dbo].[t_g_user]  where  deleted is null");
            d13 = dataBase.GetData9("SELECT id, [user_name]  FROM[CountingDB].[dbo].[t_g_user]  where  deleted is null");
            d22 = dataBase.GetData9("SELECT [branch_name], [id] FROM [CountingDB].[dbo].[t_g_cashcentre]   where tipsp1 = 2");
           
        }

        private async void SendCashForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        protected virtual void pok1()
        {
            textBox1.Text = dgList[1, dgList.CurrentRow.Index].Value.ToString().Trim();
            text1.Text = dgList[1, dgList.CurrentRow.Index].Value.ToString().Trim();
            textBox2.Text = dgList[2, dgList.CurrentRow.Index].Value.ToString().Trim();
            text2.Text = dgList[2, dgList.CurrentRow.Index].Value.ToString().Trim();
            textBox3.Text = dgList[3, dgList.CurrentRow.Index].Value.ToString().Trim();
            text3.Text = dgList[3, dgList.CurrentRow.Index].Value.ToString().Trim();
            comboBox7.Text = dgList[4, dgList.CurrentRow.Index].Value.ToString().Trim();
            cbChengUser.Text = dgList[4, dgList.CurrentRow.Index].Value.ToString().Trim();
            //text4.Text = dgList[4, dgList.CurrentRow.Index].Value.ToString().Trim();
            //text5.Text = dgList[5, dgList.CurrentRow.Index].Value.ToString().Trim();
            //text6.Text = dgList[6, dgList.CurrentRow.Index].Value.ToString().Trim();
            // text7.Text = dgList[9, dgList.CurrentRow.Index].Value.ToString().Trim();
            comboBox8.Text = dgList[5, dgList.CurrentRow.Index].Value.ToString().Trim();
            cbChengZona.Text = dgList[5, dgList.CurrentRow.Index].Value.ToString().Trim();
            comboBox9.Text = dgList[6, dgList.CurrentRow.Index].Value.ToString().Trim();
            cbChengPk.Text = dgList[6, dgList.CurrentRow.Index].Value.ToString().Trim();
            textBox7.Text = text7.Text = dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim();
            
            tbInfo1.Text = dgList[1, dgList.CurrentRow.Index].Value.ToString().Trim();
            tbInfo2.Text = dgList[2, dgList.CurrentRow.Index].Value.ToString().Trim();
            tbInfo3.Text = dgList[3, dgList.CurrentRow.Index].Value.ToString().Trim();
                       
            tbInfo4.Text = dgList[10, dgList.CurrentRow.Index].Value.ToString().Trim();
            tbInfo5.Text = dgList[11, dgList.CurrentRow.Index].Value.ToString().Trim();
            tbInfo6.Text = dgList[12, dgList.CurrentRow.Index].Value.ToString().Trim();

            tbInfo7.Text = dgList[13, dgList.CurrentRow.Index].Value.ToString().Trim();
            tbInfo8.Text = dgList[14, dgList.CurrentRow.Index].Value.ToString().Trim();
            tbInfo9.Text = dgList[15, dgList.CurrentRow.Index].Value.ToString().Trim();

            tbInfo10.Text = dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim();

            comboBox6.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox5.SelectedIndex = -1;

            //btnAdd.Enabled = true;
            if(pm.EnabledPossibility(perm, btnModify))
                btnModify.Enabled = true;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Enabled = false;
            textBox7.Enabled = false;
            label7.Visible = false;
            if(pm.VisiblePossibility(perm, label8))
                label8.Visible = true;
            if (pm.VisiblePossibility(perm, label15))
                label15.Visible = true;
            if (pm.VisiblePossibility(perm, textBox7))
                textBox7.Visible = true;
            number.Visible = false;
            if (pm.VisiblePossibility(perm, text7))
                text7.Visible = true;
            text1.Enabled = text2.Enabled = text3.Enabled = text7.Enabled = false;

            //if (textBox7.Text != "")
            {
                dataSet2 = dataBase.GetData9("select id_cashtransfer as [Номер], t2.[name] as [Номинал], t4.[name] as [Валюта], " +
                    "t3.[name] as [Состояние], [count] as [Кол - во], format(fact_value, 'n0') as [Сумма] from t_g_cashtransfer_detalization t1  " +
                    "left join t_g_denomination t2 on t1.id_denomination = t2.id left join t_g_condition t3 on t3.id = t1.id_condition " +
                    "left join t_g_currency t4 on t2.id_currency = t4.id  where id_cashtransfer = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim()+
                    "order by t2.id_currency");

                dataSet4 = dataBase.GetData9("select t1.id_cashtransfer as [Номер],t2.[name] as [Номинал],t4.[name] as [Валюта], " +
                    "sum([count]) as [Кол - во], format(sum(fact_value), 'n0')   as [Сумма] from t_g_cashtransfer_detalization t1  " +
                    "left join t_g_denomination t2 on t1.id_denomination = t2.id  left join t_g_currency t4 on t2.id_currency = t4.id "+
                     "where id_cashtransfer = "+ dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim()+
                     " group by t1.id_denomination,t2.[name],t4.[name],t1.id_cashtransfer");


                dataSet3 = dataBase.GetData9("SELECT 	t3.[name] as [валюта], format(SUM(fact_value),'n0') AS [Итого] " +
                                    "FROM t_g_cashtransfer_detalization t1 left join t_g_denomination t2 on t1.id_denomination=t2.id " +
                                    "left join t_g_currency t3 on t2.id_currency = t3.id  where t1.id_cashtransfer=  " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() +
                                    "GROUP BY t3.name"); 
                
                List1.AutoGenerateColumns = true;
                List1.ColumnHeadersHeight = 10;
                List1.RowHeadersWidth = 10;
                List1.DataSource = dataSet2.Tables[0];
                List1.AutoResizeColumns();
                List1.ClearSelection();

                List11.AutoGenerateColumns = true;
                List11.ColumnHeadersHeight = 10;
                List11.RowHeadersWidth = 10;
                List11.DataSource = dataSet4.Tables[0];
                List11.AutoResizeColumns();
                List11.ClearSelection();

                List3.AutoGenerateColumns = true;
                List3.ColumnHeadersHeight = 100;
                List3.RowHeadersWidth = 10;
                List3.DataSource = dataSet2.Tables[0];
                List3.AutoResizeColumns();
                List3.ClearSelection();

                List5.AutoGenerateColumns = true;
                List5.ColumnHeadersHeight = 100;
                List5.RowHeadersWidth = 10;
                List5.DataSource = dataSet2.Tables[0];
                List5.AutoResizeColumns();
                List5.ClearSelection();


                List2.AutoGenerateColumns = true;
                List2.ColumnHeadersHeight = 10;
                List2.RowHeadersWidth = 10;
                List2.DataSource = dataSet3.Tables[0];
                List2.AutoResizeColumns();
                List2.ClearSelection();

                List4.AutoGenerateColumns = true;
                List4.ColumnHeadersHeight = 10;
                List4.RowHeadersWidth = 10;
                List4.DataSource = dataSet3.Tables[0];
                List4.AutoResizeColumns();
                List4.ClearSelection();

                List6.AutoGenerateColumns = true;
                List6.ColumnHeadersHeight = 10;
                List6.RowHeadersWidth = 10;
                List6.DataSource = dataSet3.Tables[0];
                List6.AutoResizeColumns();
                List6.ClearSelection();
            }
            //else
            //{
            //    dataSet2 = dataBase.GetData9("SELECT [id_cashtransfer] as [контейнер], t2.[name] as [Номинал],t3.[name] as [валюта],t4.[name] as [состояние],[count] as [кол-во],[fact_value] as [Сумма] FROM t_g_cashtransfer_detalization t1 left join t_g_denomination t2 on t1.id_denomination=t2.id left join t_g_currency t3 on t2.id_currency = t3.id left join t_g_condition t4 on t4.id = t1.id_condition where t1.[id_cashtransfer]=0");// + textBox7.Text.ToString());
            //    List1.DataSource = dataSet2.Tables[0];
            //    List3.DataSource = dataSet2.Tables[0];
            //    dataSet3 = dataBase.GetData9("SELECT t3.[name] as [валюта], SUM(fact_value) AS [Итого] FROM t_g_cashtransfer_detalization t1 left join t_g_denomination t2 on t1.id_denomination=t2.id left join t_g_currency t3 on t2.id_currency = t3.id where t1.id_cashtransfer=0 GROUP BY t3.name"); //" + number.Text + " GROUP BY t3.name");
            //    List2.DataSource = dataSet3.Tables[0];
            //    List4.DataSource = dataSet3.Tables[0];               
            //}
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
                                   
            //MessageBox.Show(dgList[7, dgList.CurrentRow.Index].Value.ToString().Trim());
            if (dgList[7, dgList.CurrentRow.Index].Value.ToString() == "1")
            {
                dataBase.Connect();
                dataSet = null;
                dataSet = dataBase.GetData9(" update t_w_cashtransfer set id_status=2" +
                    ", lastupdate = GETDATE(), receivedate = GETDATE(), receiveuserdate = " + CurrentUser.CurrentUserId +
                    ", lastuserupdate = " + CurrentUser.CurrentUserId +
                //", id_user_change = " + comboBox7.SelectedValue.ToString() +
                //    ", id_zone_change = " + comboBox8.SelectedValue.ToString() +
                //     ", id_pk_change = " + comboBox9.SelectedValue.ToString() +
                " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());
            
           
            

            MessageBox.Show("Передача принята!");
                //Обнавляем форму
                dataSet = null;
                dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
                //dgList.DataSource = 
                dgList.DataSource = dataSet.Tables[0];
                dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgList.Columns[7].Visible = false;

                dgList.Columns[10].Visible = false;
                dgList.Columns[11].Visible = false;
                dgList.Columns[12].Visible = false;
                dgList.Columns[13].Visible = false;
                dgList.Columns[14].Visible = false;
                dgList.Columns[15].Visible = false;
                //dgList.Columns[0].Visible = false;
                //, dgList.CurrentRow.Index]

                //dataSetInfo = null;
                //dataSetInfo = dataBase.GetData9("selectListInfo");
                //dgListInfo.DataSource = dataSetInfo.Tables[0];
            }
            else
                MessageBox.Show("Процес не может быть изменен, поскольку "+ dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim()+"!");


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (countDenomDataSet == null)
            {
                MessageBox.Show("Заполните данные по номиналом и после создайте перевод!!!");
            }
            else
            {
                double sum=0;
                foreach (DataRow denomRow in countDenomDataSet.Tables[0].Rows)
                {
                    sum = sum + Convert.ToDouble(denomRow["declared_value"]);
                }
                   // MessageBox.Show("Введенная вами сумма = " + sum.ToString());

                //if (number.Text != "")
                {

                    if ((cbUser1.Text.Count() == 0) || (comboBox2.Text.Count() == 0)
                        || (comboBox4.Text.Count() == 0) || (comboBox5.Text.Count() == 0))
                    {
                        MessageBox.Show("Заполните все обязательные поля со звездами!");
                        if (cbUser1.Text.Count() == 0) lblName.ForeColor = Color.Red;
                        if (comboBox2.Text.Count() == 0) label6.ForeColor = Color.Red;
                        if (comboBox4.Text.Count() == 0) label5.ForeColor = Color.Red;
                        if (comboBox5.Text.Count() == 0) label4.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataBase.Connect();
                        dataSet = null;
                        dataSet = dataBase.GetData9("select * from t_w_cashtransfer");

                        //Создаем новую строку для t_w_cashtransfer
                        DataRow dataRow = dataSet.Tables[0].NewRow();
                        dataRow["creation"] = DateTime.Now;
                        dataRow["id_user_send"] = cbUser1.SelectedValue.ToString();
                        dataRow["id_zone_send"] = comboBox2.SelectedValue.ToString();
                        if (comboBox3.Text != "") dataRow["id_pk_send"] = comboBox3.SelectedValue.ToString();
                        dataRow["id_user_receive"] = comboBox4.SelectedValue.ToString();
                        dataRow["id_zone_receive"] = comboBox5.SelectedValue.ToString();
                        if (comboBox6.Text != "") dataRow["id_pk_reseive"] = comboBox6.SelectedValue.ToString();
                        dataRow["id_status"] = "1";
                        dataRow["lastupdate"] = DateTime.Now;
                        if (number.Text != "") dataRow["number_container"] = number.Text;
                        dataRow["lastuserupdate"] = CurrentUser.CurrentUserId;
                        dataRow["createuser"] = CurrentUser.CurrentUserId;
                        dataRow["source"] = "2";

                        dataRow["id_user_create"] = CurrentUser.CurrentUserId;
                        dataRow["id_zona_create"] = CurrentUser.CurrentUserZona;
                        dataRow["id_shift_create"] = CurrentUser.CurrentUserShift;
                        dataRow["id_shift_current"] = CurrentUser.CurrentUserShift;

                        //Добавление строку в переменную
                        dataSet.Tables[0].Rows.Add(dataRow);

                        //List<DataSet> dataset = new List<DataSet>();
                        //dataset.Add(dataSet);


                        //Обновление БД
                        dataBase.UpdateData(dataSet, "t_w_cashtransfer");

                        // Получаем последнию ид из таблицы t_w_cashtransfer  и записываем ее в переменную id
                        dataSet = dataBase.GetData9("SELECT TOP (1) [id]  FROM [t_w_cashtransfer] order by id desc");
                        long id=0;
                        foreach (DataRow denomRow in dataSet.Tables[0].Rows) id = Convert.ToInt64(denomRow["id"]);
                       // MessageBox.Show("id = "+id.ToString());

                        // Создаем новую строку для

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        dataSet1 = dataBase.GetData9("PROC_BALANCE_1 " + cbUser1.SelectedValue.ToString());
                        dataSet = null;
                        dataSet = dataBase.GetData9("select * from t_g_cashtransfer_detalization");

                        //Создаем новые строки для t_g_cashtransfer_detalization
                        
                        foreach (DataRow denomRow in countDenomDataSet.Tables[0].Rows)
                        {
                            
                            
                            /////Работаем без состояния!!!!
                            if (denomRow["id_condition"].ToString() == "")
                            {
                                //MessageBox.Show("Работаем без состояния!!!!");
                                long x = Convert.ToInt64(denomRow["denomcount"]); // это в ручную водимое кол-во

                               // MessageBox.Show(" X= "+x.ToString());


                                
                                foreach (DataRow denomRow3 in dataSet1.Tables[0].Rows) /////
                                {
                                    
                                    if (Convert.ToInt64(denomRow["id_denomination"]) == Convert.ToInt64(denomRow3["id"]))
                                    {
                                        //MessageBox.Show("denomRow3[Count22]=" + denomRow3["Count22"].ToString());
                                        //MessageBox.Show("denomRow3[Count]=" + denomRow3["Count22"].ToString());
                                        if (
                                            x <= Convert.ToInt64(denomRow3["Count22"])                                             
                                            && 
                                            x>0
                                            )
                                        {

                                            DataRow dataRow2 = dataSet.Tables[0].NewRow();
                                            /// если введенная кол-во меньше от первого по умольчанию кол-во
                                            dataRow2["id_cashtransfer"] = id.ToString();
                                            dataRow2["id_denomination"] = denomRow["id_denomination"];

                                            dataRow2["id_condition"] = denomRow3["id_condition"];
                                            dataRow2["creation"] = DateTime.Now;
                                            dataRow2["lastupdate"] = DateTime.Now;
                                            dataRow2["lastuserupdate"] = CurrentUser.CurrentUserId;
                                            dataRow2["count"] = x.ToString();
                                            //MessageBox.Show(" 1 " );
                                            dataRow2["fact_value"] = (x * Convert.ToInt64(denomRow3["value"])).ToString();// denomRow["declared_value"];//////
                                            dataSet.Tables[0].Rows.Add(dataRow2);
                                            break;
                                            

                                        }
                                        else if(x>0)
                                        {
                                            DataRow dataRow2 = dataSet.Tables[0].NewRow();
                                            //MessageBox.Show("x > Count");
                                            //MessageBox.Show("denomRow3[Count22]=" + denomRow3["Count22"]);
                                            //MessageBox.Show("denomRow3[value])=" + denomRow3["value"]);
                                            dataRow2["id_cashtransfer"] = id.ToString();
                                            dataRow2["id_denomination"] = denomRow["id_denomination"];                                            
                                            dataRow2["creation"] = DateTime.Now;
                                            dataRow2["lastupdate"] = DateTime.Now;
                                            dataRow2["lastuserupdate"] = CurrentUser.CurrentUserId;
                                            dataRow2["fact_value"] = (Convert.ToInt64(denomRow3["Count22"]) * Convert.ToInt64(denomRow3["value"])).ToString();//denomRow["declared_value"];
                                            dataRow2["id_condition"] = denomRow3["id_condition"];
                                            dataRow2["count"] = denomRow3["Count22"];
                                           //MessageBox.Show(" 2 ");

                                            dataSet.Tables[0].Rows.Add(dataRow2);
                                            
                                            x -= Convert.ToInt64(denomRow3["Count22"]);
                                           

                                        }
                                    }
                                }


                            }
                            
                            /////Работаем c состоянием!!!!
                            else
                            {
                                if (Convert.ToInt64(denomRow["denomcount"]) == 0) 
                                { 
                                    //MessageBox.Show("Пустая строка"); 
                                }
                                else
                                {
                                    DataRow dataRow2 = dataSet.Tables[0].NewRow();
                                    //MessageBox.Show("Работаем c состоянием!!!!");
                                    //MessageBox.Show(" 1 ");
                                    dataRow2["id_cashtransfer"] = id.ToString();
                                    dataRow2["id_denomination"] = denomRow["id_denomination"];
                                    dataRow2["id_condition"] = denomRow["id_condition"];
                                    dataRow2["creation"] = DateTime.Now;
                                    dataRow2["lastupdate"] = DateTime.Now;
                                    dataRow2["lastuserupdate"] = CurrentUser.CurrentUserId;
                                    dataRow2["count"] = denomRow["denomcount"];
                                    dataRow2["fact_value"] = denomRow["declared_value"];

                                    //Добавление строку в переменную
                                    dataSet.Tables[0].Rows.Add(dataRow2);
                                }
                            }
                            
                           
                        }
                        
                        
                        //Обновление БД
                        dataBase.UpdateData(dataSet, "t_g_cashtransfer_detalization");




                        lblName.ForeColor = Color.Black;
                        label6.ForeColor = Color.Black;
                        label5.ForeColor = Color.Black;
                        label4.ForeColor = Color.Black;




                        countDenomDataSet = null;

                        MessageBox.Show("Передача создана!");
                        //Обнавляем форму
                        dataSet = null;
                        dataSet = dataBase.GetData9("selectList "+DataExchange.CurrentUser.CurrentUserShift);
                        //dgList.DataSource = 
                        dgList.DataSource = dataSet.Tables[0];
                        dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

                        dgList.Columns[7].Visible = false;

                        dgList.Columns[10].Visible = false;
                        dgList.Columns[11].Visible = false;
                        dgList.Columns[12].Visible = false;
                        dgList.Columns[13].Visible = false;
                        dgList.Columns[14].Visible = false;
                        dgList.Columns[15].Visible = false;
                        //dgList.Columns[0].Visible = false;

                        //dataSetInfo = null;
                        //dataSetInfo = dataBase.GetData9("selectListInfo");
                        //dgListInfo.DataSource = dataSetInfo.Tables[0];

                        cbUser1.Text = "";
                        comboBox2.Text = "";
                        comboBox3.Text = "";
                        comboBox4.Text = "";
                        comboBox5.Text = "";
                        comboBox6.Text = "";
                        number.Text = "";
                        //dvdenegList.DataSource = null;
                        sumList.DataSource = null;

                    }
                }
            }
        }

        private void number_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void number_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgList.CurrentCell != null)
            {
                pok1();
                
            }
        }

        private void sumList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //btnAdd.Text = sumList.Rows[e.RowIndex].Cells["id_currency"].Value.ToString();

            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) //PROC_BALANCE
            {
               DenominationParentForSC denominationForm = new DenominationParentForSC("PROC_BALANCE2", sumList.Rows[e.RowIndex].Cells["id_currency"].Value.ToString(),// валюта // senderGrid.Rows[e.RowIndex].Cells["id_currency"].Value.ToString(),



                  //  cbAccount.SelectedValue.ToString()
                  //"30066" //id_account - будем подключать  айди юзера
                  cbUser1.SelectedValue.ToString()  //senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()
                                                    ///////30.10.2019


                    , countDenomDataSet, b);
                DialogResult dialogResult = denominationForm.ShowDialog();
                //CountDenomDataSet cntDenomDataSet = new CountDenomDataSet();
                if (dialogResult == DialogResult.OK)
                {
                    //countDenomDataSet.Clear();
                    countDenomDataSet = denominationForm.CountDenomDataSet;
                    b = denominationForm.b_get;
                }
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {                       

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            

            //lblName.Text = "111";
            //cbUser1.SelectedIndex = 1;
            //cbUser1.SelectedValue = 1;
            




            cbUser1.DataSource = d1.Tables[0];
            cbUser1.DisplayMember = "user_name";
            cbUser1.ValueMember = "id";
            cbUser1.SelectedIndex = -1;

            cbUser2.DataSource = d11.Tables[0];
            cbUser2.DisplayMember = "user_name";
            cbUser2.ValueMember = "id";
            cbUser2.SelectedIndex = -1;

            comboBox2.DataSource = d2.Tables[0];
            comboBox2.DisplayMember = "branch_name";
            comboBox2.ValueMember = "id";
            comboBox2.SelectedIndex = -1;

            cbZona.DataSource = d22.Tables[0];
            cbZona.DisplayMember = "branch_name";
            cbZona.ValueMember = "id";
            cbZona.SelectedIndex = -1;

            cbStatus.DataSource = d7.Tables[0];
            cbStatus.DisplayMember = "description";
            cbStatus.ValueMember = "id";
            cbStatus.SelectedIndex = -1;

            comboBox3.DataSource = d3.Tables[0];
            comboBox3.DisplayMember = "branch_name";
            comboBox3.ValueMember = "id";
            comboBox3.SelectedIndex = -1;

            comboBox4.DataSource = d12.Tables[0];
            comboBox4.DisplayMember = "user_name";
            comboBox4.ValueMember = "id";
            comboBox4.SelectedIndex = -1;

            comboBox7.DataSource = d13.Tables[0];
            comboBox7.DisplayMember = "user_name";
            comboBox7.ValueMember = "id";
            comboBox7.SelectedIndex = -1;

            cbChengUser.DataSource = d4.Tables[0];
            cbChengUser.DisplayMember = "user_name";
            cbChengUser.ValueMember = "id";
            cbChengUser.SelectedIndex = -1;


            comboBox5.DataSource = d5.Tables[0];
            comboBox5.DisplayMember = "branch_name";
            comboBox5.ValueMember = "id";
            comboBox5.SelectedIndex = -1;
            comboBox8.DataSource = d5.Tables[0];
            comboBox8.DisplayMember = "branch_name";
            comboBox8.ValueMember = "id";
            comboBox8.SelectedIndex = -1;

            cbChengZona.DataSource = d5.Tables[0];
            cbChengZona.DisplayMember = "branch_name";
            cbChengZona.ValueMember = "id";
            cbChengZona.SelectedIndex = -1;

            comboBox6.DataSource = d6.Tables[0];
            comboBox6.DisplayMember = "branch_name";
            comboBox6.ValueMember = "id";
            comboBox6.SelectedIndex = -1;
            comboBox9.DataSource = d6.Tables[0];
            comboBox9.DisplayMember = "branch_name";
            comboBox9.ValueMember = "id";
            comboBox9.SelectedIndex = -1;

            cbChengPk.DataSource = d6.Tables[0];
            cbChengPk.DisplayMember = "branch_name";
            cbChengPk.ValueMember = "id";
            cbChengPk.SelectedIndex = -1;


            //number.DataSource = d7.Tables[0];
            //number.DisplayMember = "id_counting";
            ////number.ValueMember = "id";
            //number.SelectedIndex = -1;


            dgList.AutoGenerateColumns = true;
            dgList.ColumnHeadersHeight = 10;
            dgList.RowHeadersWidth = 10;
            dgList.DataSource = dataSet.Tables[0];

            dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns[7].Visible = false;

            dgList.Columns[10].Visible = false;
            dgList.Columns[11].Visible = false;
            dgList.Columns[12].Visible = false;
            dgList.Columns[13].Visible = false;
            dgList.Columns[14].Visible = false;
            dgList.Columns[15].Visible = false;



            //dgList.Columns[0].Visible = false;
            dgList.AutoResizeColumns();
            dgList.ClearSelection();

            //List1.AutoGenerateColumns = true;
            //List1.ColumnHeadersHeight = 100;
            //List1.RowHeadersWidth = 10;
            //List1.DataSource = dataSet2.Tables[0];
            //List1.AutoResizeColumns();
            //List1.ClearSelection();
            List1.DataSource = null;
            List11.DataSource = null;
            List2.DataSource = null;
            List3.DataSource = null;
            List4.DataSource = null;

            sumList.AutoGenerateColumns = false;
            sumList.ColumnHeadersHeight = 10;
            sumList.RowHeadersWidth = 10;

            sumList.DataSource = null;
            sumList.Rows.Clear();
            sumList.Columns.Clear();
            sumList.DataSource = null;
            currencyDataSet = dataBase.GetData("t_g_currency");



            DataGridViewComboBoxColumn dgAccountCurrency = new DataGridViewComboBoxColumn();
            dgAccountCurrency.Name = "id_currency";
            dgAccountCurrency.HeaderText = "Валюта";
            dgAccountCurrency.FlatStyle = FlatStyle.Flat;
            dgAccountCurrency.DataPropertyName = "id_currency";
            dgAccountCurrency.DisplayMember = "curr_code";
            dgAccountCurrency.ValueMember = "id";
            dgAccountCurrency.ReadOnly = true;
            dgAccountCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;


            //dgAccountCurrency.DataSource = currencyDataSet.Tables[0];
            //sumList.Columns.Add(dgAccountCurrency);
            //sumList.Columns["id_currency"].Visible = true;
            //sumList.Columns["id_currency"].Width = 65;


            //DataGridViewColumn sum_value = new DataGridViewColumn();
            //sum_value.Name = "sum_value";
            //sum_value.Visible = true;           
            //sumList.Columns.Add(sum_value);
            //sumList.Columns["sum_value"].Width = 65;
            //sumList.Columns["sum_value"].Name = "сумма";

            //sumList.Columns["sum_value"].HeaderText = "Сумма";

            sumList.DataSource = dataSet3.Tables[0];

            DataGridViewButtonColumn dgAD = new DataGridViewButtonColumn();  
            dgAD.Visible = true;
            dgAD.Text = ". . .";
            dgAD.Name = "Denomination";
            //sumList.Columns.Add(dgAD);
            //sumList.Columns["Denomination"].HeaderText = "Номинал";

            //dvdenegList.AutoGenerateColumns = false;
            //dvdenegList.ColumnHeadersHeight = 10;
            //dvdenegList.RowHeadersWidth = 10;
        }

        private void cbUser1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUser1.SelectedIndex != -1)
           
            {
                int number1;
                int.TryParse(cbUser1.SelectedValue.ToString(), out number1);            

                sumList.AutoGenerateColumns = true;
                sumList.DataSource = null;                

                dataSet3 = dataBase.GetData9("sum_1 " + number1+","+DataExchange.CurrentUser.CurrentUserShift);

                currencyDataSet = dataBase.GetData("t_g_currency");

                sumList.Columns.Clear();

                DataGridViewComboBoxColumn dgAccountCurrency = new DataGridViewComboBoxColumn();
                dgAccountCurrency.Name = "id_currency";
                dgAccountCurrency.HeaderText = "Валюта";
                dgAccountCurrency.FlatStyle = FlatStyle.Flat;
                dgAccountCurrency.DataPropertyName = "id_currency";
                dgAccountCurrency.DisplayMember = "curr_code";
                dgAccountCurrency.ValueMember = "id";
                dgAccountCurrency.ReadOnly = true;
                dgAccountCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;


                dgAccountCurrency.DataSource = currencyDataSet.Tables[0];
                sumList.Columns.Add(dgAccountCurrency);
                sumList.Columns["id_currency"].Visible = true;
                sumList.Columns["id_currency"].Width = 65;

                //DataGridViewColumn sum_value = new DataGridViewColumn();
                //sum_value.Name = "sum_value";
                //sum_value.Visible = true;
                //sumList.Columns.Add(sum_value);
                //sumList.Columns["sum_value"].Width = 65;
                //sumList.Columns["sum_value"].Name = "Сумма";

                sumList.DataSource = dataSet3.Tables[0];

                //sumList.Columns.Remove["Denomination"];
                DataGridViewButtonColumn dgAD = new DataGridViewButtonColumn();
                dgAD.Visible = true;
                dgAD.Text = ". . .";
                dgAD.Name = "Denomination";
                sumList.Columns.Add(dgAD);
                sumList.Columns["Denomination"].HeaderText = "Номинал";



            }
            //if (cbUser1.SelectedIndex == 0)
            //{
            //    lblName.Text = cbUser1.SelectedIndex.ToString();
            //    label6.Text = cbUser1.SelectedValue.ToString();

            //}
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        { 
        }
        private void select(string a1)
        {            
            
        }

        private void CBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblName.Text = CBox1.SelectedIndex.ToString();
           


        }

        private void CBox1_TextUpdate(object sender, EventArgs e)
        {

        }

        private void CBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox4.SelectedIndex != -1)
            //{
            //    dataSet3 = dataBase.GetData("t_g_account", "id_client", comboBox3.SelectedValue.ToString());
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgList[7, dgList.CurrentRow.Index].Value.ToString() == "1")
            {
                dataBase.Connect();
                dataSet = null;
                dataSet = dataBase.GetData9(" update t_w_cashtransfer set id_status=4" +
                    ", lastupdate = GETDATE(), receivedate = GETDATE(), receiveuserdate = " + CurrentUser.CurrentUserId +
                    ", lastuserupdate = " + CurrentUser.CurrentUserId +
                //", id_user_change = " + comboBox7.SelectedValue.ToString() +
                //    ", id_zone_change = " + comboBox8.SelectedValue.ToString() +
                //     ", id_pk_change = " + comboBox9.SelectedValue.ToString() +
                " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());




                MessageBox.Show("Принятие отменена!");
                //Обнавляем форму
                dataSet = null;
                dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
                //dgList.DataSource = 
                dgList.DataSource = dataSet.Tables[0];

                dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgList.Columns[7].Visible = false;

                dgList.Columns[10].Visible = false;
                dgList.Columns[11].Visible = false;
                dgList.Columns[12].Visible = false;
                dgList.Columns[13].Visible = false;
                dgList.Columns[14].Visible = false;
                dgList.Columns[15].Visible = false;
                //, dgList.CurrentRow.Index]

                //dataSetInfo = null;
                //dataSetInfo = dataBase.GetData9("selectListInfo");
                ////dgList.DataSource = 
                //dgListInfo.DataSource = dataSetInfo.Tables[0];

            }
            else
                MessageBox.Show("Процес не может быть отменен, поскольку " + dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim() + "!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgList[7, dgList.CurrentRow.Index].Value.ToString() == "2" )
            {
                dataBase.Connect();
                dataSet = null;
                string s1 = " ";
                if (cbChengPk.SelectedIndex != -1)
                    s1 = ", id_pk_change = " + cbChengPk.SelectedValue.ToString();

                    dataSet = dataBase.GetData9(" update t_w_cashtransfer set id_status=3" +
                    ", lastupdate = GETDATE(), changedate = GETDATE(), changeuserdate = " + CurrentUser.CurrentUserId +
                    ", lastuserupdate = " + CurrentUser.CurrentUserId +
                    ", id_user_change = " + cbChengUser.SelectedValue.ToString() +
                    ", id_zone_change = " + cbChengZona.SelectedValue.ToString() +
                      s1 +
                     
                " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());




                MessageBox.Show("Передача принята, завершена успешно!");
                //Обнавляем форму
                dataSet = null;
                dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
                //dgList.DataSource = 
                dgList.DataSource = dataSet.Tables[0];

                dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgList.Columns[7].Visible = false;

                dgList.Columns[10].Visible = false;
                dgList.Columns[11].Visible = false;
                dgList.Columns[12].Visible = false;
                dgList.Columns[13].Visible = false;
                dgList.Columns[14].Visible = false;
                dgList.Columns[15].Visible = false;
                //dgList.Columns[0].Visible = false;
                //, dgList.CurrentRow.Index]



            }
            else
                MessageBox.Show("Процес не может быть завершен, поскольку " + dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim() + "!");

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (dgList[7, dgList.CurrentRow.Index].Value.ToString() == "2" || dgList[7, dgList.CurrentRow.Index].Value.ToString() == "1")
            {
                dataBase.Connect();
                dataSet = null;
                dataSet = dataBase.GetData9(" update t_w_cashtransfer set id_status=6" +
                    ", lastupdate = GETDATE(), receivedate = GETDATE(), receiveuserdate = " + CurrentUser.CurrentUserId +
                    ", lastuserupdate = " + CurrentUser.CurrentUserId +
                //", id_user_change = " + comboBox7.SelectedValue.ToString() +
                //    ", id_zone_change = " + comboBox8.SelectedValue.ToString() +
                //     ", id_pk_change = " + comboBox9.SelectedValue.ToString() +
                " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());




                MessageBox.Show("Передача отменена контроллером!");
                //Обнавляем форму
                dataSet = null;
                dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
                //dgList.DataSource = 
                dgList.DataSource = dataSet.Tables[0];

                dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgList.Columns[7].Visible = false;

                dgList.Columns[10].Visible = false;
                dgList.Columns[11].Visible = false;
                dgList.Columns[12].Visible = false;
                dgList.Columns[13].Visible = false;
                dgList.Columns[14].Visible = false;
                dgList.Columns[15].Visible = false;
                //dgList.Columns[0].Visible = false;
                //, dgList.CurrentRow.Index]

            }
            else
                MessageBox.Show("Процес не может быть отменен, поскольку " + dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim() + "!");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (button3.Text == "Показать фильтр")
            {
                panel5.Visible = true;
                button3.Text = "Скрыть фильтр";
                dgList.Height = 270;
                dgList.Top = 200;
                SendCashForm.ActiveForm.Height = 515;
            }
            else
            {

                panel5.Visible = false;
                button3.Text = "Показать фильтр";
                dgList.Height = 420;
                dgList.Top = 50;
                SendCashForm.ActiveForm.Height = 515;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                if(pm.EnabledPossibility(perm, dt1))
                    dt1.Enabled = true;
                if (pm.EnabledPossibility(perm, dt2))
                    dt2.Enabled = true;
            }
            else
            {
                dt1.Enabled = false;
                dt2.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s1 = "SELECT t1.id as [Номер], u1.[user_name] as [Пользователь], c1.branch_name as [Зона], c2.branch_name as [ПК], " +
                "iif(u3.[user_name] is null, u2.[user_name],u3.[user_name]) as [Пользователь],	iif(c5.branch_name is null, c3.branch_name, c5.branch_name) as [Зона]," +
                "iif(c6.branch_name is null, c4.branch_name, c6.branch_name) as [ПК],	id_status,	s.[description] as [Статус], t1.creation as [Дата создания перевода] " +
                ",u2.[user_name],    c3.branch_name , c4.branch_name ,	u3.[user_name] ,c5.branch_name , c6.branch_name "+
                "FROM[CountingDB].[dbo].[t_w_cashtransfer] t1 left join t_g_user u1 on t1.[id_user_send] = u1.id  left join t_g_cashcentre c1 on t1.[id_zone_send] = c1.id  " +
                "left join t_g_cashcentre c2 on t1.[id_pk_send] = c2.id  left join t_g_user u2 on t1.[id_user_receive] = u2.id  " +
                "left join t_g_cashcentre c3 on t1.[id_zone_receive] = c3.id left join t_g_cashcentre c4 on t1.[id_pk_reseive] = c4.id  " +
                "left join t_g_status s on t1.[id_status] = s.id left join t_g_user u3 on t1.id_user_change = u3.id " +
                "left join t_g_cashcentre c5 on t1.id_zone_change = c5.id left join t_g_cashcentre c6 on t1.[id_pk_change] = c6.id  " +
                "where u1.id is not null ";
            //string s2 = cbUser2.SelectedValue.ToString();
            //string s3 = cbZona.SelectedValue.ToString();
            //string s4 = cbStatus.SelectedValue.ToString();
            //string d1 = dt1.Value.ToString();
            if (cbUser2.SelectedIndex != -1 || cbZona.SelectedIndex != -1 || cbStatus.SelectedIndex !=-1 || checkBox1.Checked)
            {
                if(pm.EnabledPossibility(perm, button6))
                    button6.Enabled = true;
                if (cbUser2.SelectedIndex != -1)
                {
                    string s2 = cbUser2.SelectedValue.ToString();
                    s1 = s1 + " and (u1.id = " + s2 + " or u2.id = " + s2 + " ) ";
                }
                if (cbZona.SelectedIndex != -1)
                {
                    string s3 = cbZona.SelectedValue.ToString();
                    s1 = s1 + " and (c1.id = " + s3 + " or c3.id = " + s3 + " or c5.id=" + s3 + ") ";
                }
                if (cbStatus.SelectedIndex != -1) s1 = s1 + " and id_status = "+ cbStatus.SelectedValue.ToString() + " "; 
                if(checkBox1.Checked)
                {
                    string d1;
                    if (dt1.Value < dt2.Value)
                    {
                        d1 = dt1.Text.ToString().Trim();
                    }
                    else
                    {
                        d1 = dt2.Text.ToString().Trim();
                        dt1.Value = dt2.Value;
                    }
                        //string d2 = dt2.Text.ToString().Trim();
                        DateTime dt = dt2.Value.AddDays(1);
                    //MessageBox.Show(dt.AddDays(1).ToShortDateString() );
                    s1 = s1 + " and t1.creation > CONVERT(datetime, '" + d1 + "') and t1.creation < CONVERT(datetime, '" + dt.ToShortDateString() + " ')";//d2+"') ";
                    //MessageBox.Show("DT1 = " + d1);
                    //MessageBox.Show("DT2 = " + d2);
                }
                
            }
            else
                button6.Enabled = false;
            s1 = s1 + " order by t1.id desc";
            dataSet = dataBase.GetData9(s1);
            dgList.DataSource = dataSet.Tables[0];


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


            ReportDocument reportDocument1 = new ReportDocument();
            reportDocument1.Load(@"C:\\report\\Cash_Transfers.rpt");

            StreamReader studIni = new StreamReader(@"C:\\report\\Cash_Transfers.ini", UTF8Encoding.Default);
            string x = studIni.ReadToEnd();
            //string[] 
            string[] y = x.Split('\n');

            for (int i = 0; i < y.Length; i++)
            {
                switch (y[i].Trim())
                {
                    case "PRM_IdCashTransfers":

                        reportDocument1.SetParameterValue(i, (dgList[0, dgList.CurrentRow.Index].Value.ToString()));
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

            reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\Отчеты\Перемещение наличности" + datetime +/*"."+time+*/".pdf");


            CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();

            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета


            ReportShowForm reportShowForm = new ReportShowForm();

            reportShowForm.Text = "Перемещение наличности";
            reportShowForm.name = "Перемещение наличности";
            reportShowForm.crystalReportViewer1 = crystalReportViewer1;
            reportShowForm.reportDocument = reportDocument1;
            reportShowForm.Show();


        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
            dgList.DataSource = dataSet.Tables[0];
            button6.Enabled = false;
            cbUser2.SelectedIndex = -1;
            cbZona.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            checkBox1.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dgList[7, dgList.CurrentRow.Index].Value.ToString() == "1")
            {
                if(!dataBase.Status())
                dataBase.Connect();
                dataSet = null;
                dataSet = dataBase.GetData9(" update t_w_cashtransfer set id_status=5" +
                    ", lastupdate = GETDATE() " + 
                    ", lastuserupdate = " + CurrentUser.CurrentUserId +
                //", id_user_change = " + comboBox7.SelectedValue.ToString() +
                //    ", id_zone_change = " + comboBox8.SelectedValue.ToString() +
                //     ", id_pk_change = " + comboBox9.SelectedValue.ToString() +
                " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());




                MessageBox.Show("Передача отменена!");
                //Обнавляем форму
                dataSet = null;
                dataSet = dataBase.GetData9("selectList " + DataExchange.CurrentUser.CurrentUserShift);
                //dgList.DataSource = 
                dgList.DataSource = dataSet.Tables[0];

                dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgList.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgList.Columns[7].Visible = false;

                dgList.Columns[10].Visible = false;
                dgList.Columns[11].Visible = false;
                dgList.Columns[12].Visible = false;
                dgList.Columns[13].Visible = false;
                dgList.Columns[14].Visible = false;
                dgList.Columns[15].Visible = false;
                //dgList.Columns[0].Visible = false;
                //, dgList.CurrentRow.Index]

            }
            else
                MessageBox.Show("Процес не может быть отменен, поскольку " + dgList[8, dgList.CurrentRow.Index].Value.ToString().Trim() + "!");
        }
    }


        ////////////////
}

