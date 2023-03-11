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
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.ParentForms;
using CountingForms.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class SendBagsForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = null;
        private DataSet userDataSet = null;
        private DataSet zonaDataSeet = null;
        private DataSet d3 = null;
        private DataSet dataSet = null;
        private DataSet dataSet1 = null;
        private DataSet dataSet2 = null;
        private DataSet dataSetList = null;
        private DataSet dataSetR1 = null;
        private DataSet dataSetR2 = null;
        private DataSet marshrDataSet = null;

        public SendBagsForm()
        {
            dataBase = new MSDataBase();
            dataBase.Connect();
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            //dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
            //    " or user_received=" + DataExchange.CurrentUser.CurrentUserId +
            //    " Order by t1.id desc");

            //dgList.AutoGenerateColumns = true;
            //dgList.ColumnHeadersHeight = 600;

            dgList.RowHeadersWidth = 10;
            detal1();
            //dgList.DataSource = dataSetList.Tables[0];
            dgList.Columns[0].Width = 50;
            dgList.Columns[1].Width = 160;
            dgList.Columns[3].Width = 80;
            dgList.Columns[4].Width = 80;
            dgList.Columns[5].Width = 80;
            dgList.Columns[6].Visible = false;
            dgList.Columns[7].Visible = false;
            dgList.Columns[8].Width = 115;

            dgList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;



            dgList.ReadOnly = true;

            dgSend1.AutoGenerateColumns = false;
            dgSend1.RowHeadersWidth = 10;
            dgSend1.Columns.Add("name", "Наименование сумки");
            dgSend1.Columns["name"].Visible = true;
            dgSend1.Columns["name"].Width = 176;
            dgSend1.Columns["name"].DataPropertyName = "name";
            dgSend1.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;



            dgSend1.Columns.Add("id", "id");
            dgSend1.Columns["id"].Visible = false;
            dgSend1.Columns["id"].DataPropertyName = "id";
            send1();

            zonaDataSeet = dataBase.GetData9("SELECT [branch_name], [id] FROM [CountingDB].[dbo].[t_g_cashcentre]   where tipsp1 = 2");
            userDataSet = dataBase.GetData9("SELECT id, [user_name]  FROM[CountingDB].[dbo].[t_g_user]  where id_role in (0,  2,3) and deleted is null");

            dataSet = userDataSet.Clone();
            DataRow row = dataSet.Tables[0].NewRow();
            row["id"] = 0;
            row["user_name"] = "Пустая строка";
            dataSet.Tables[0].Rows.Add(row);
            dataSet.Merge(userDataSet);

            cbUser.DataSource = dataSet.Tables[0];
            cbUser.DisplayMember = "user_name";
            cbUser.ValueMember = "id";
            cbUser.SelectedIndex = 0;

            dataSet = null;
            dataSet = zonaDataSeet.Clone();
            DataRow row1 = dataSet.Tables[0].NewRow();
            row1["id"] = 0;
            row1["branch_name"] = "Пустая строка";
            dataSet.Tables[0].Rows.Add(row1);
            dataSet.Merge(zonaDataSeet);

            cbZona.DataSource = dataSet.Tables[0];
            cbZona.DisplayMember = "branch_name";
            cbZona.ValueMember = "id";
            cbZona.SelectedIndex = 0;







            marshrDataSet = dataBase.GetData("t_g_marschrut");

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "nummarsh";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = marshrDataSet.Tables[0];
            comboBox1.SelectedIndex = -1;

            /////31.07.2020
            dgSend2.AutoGenerateColumns = false;
            dgSend2.RowHeadersWidth = 10;

            dgSend2.Columns.Add("name", "Наименование сумки");
            dgSend2.Columns["name"].Width = 176;

            dgSend2.Columns["name"].Visible = true;
            dgSend2.Columns["name"].DataPropertyName = "name";
            dgSend2.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgSend2.Columns.Add("id", "id");
            dgSend2.Columns["id"].Visible = false;
            dgSend2.Columns["id"].DataPropertyName = "id";

            ///////06.08.2020
            dgReceive1.AutoGenerateColumns = false;
            dgReceive1.RowHeadersWidth = 10;

            dgReceive1.Columns.Add("name", "Наименование сумки");
            dgReceive1.Columns["name"].Visible = true;
            dgReceive1.Columns["name"].Width = 176;
            dgReceive1.Columns["name"].DataPropertyName = "name";


            dgReceive1.Columns.Add("id", "id");
            dgReceive1.Columns["id"].Visible = false;
            dgReceive1.Columns["id"].DataPropertyName = "id";
            pok1();

            dgReceive2.AutoGenerateColumns = false;
            dgReceive2.RowHeadersWidth = 10;
            dgReceive2.Columns.Add("name", "Наименование сумки");
            dgReceive2.Columns["name"].Visible = true;
            dgReceive2.Columns["name"].Width = 176;
            dgReceive2.Columns["name"].DataPropertyName = "name";


            dgReceive2.Columns.Add("id", "id");
            dgReceive2.Columns["id"].Visible = false;
            dgReceive2.Columns["id"].DataPropertyName = "id";

            dataSet = dataBase.GetData9("Select user_name from t_g_user where id = " + DataExchange.CurrentUser.CurrentUserId);
            textBox5.Text = dataSet.Tables[0].AsEnumerable().Select(x => x.Field<string>("user_name")).FirstOrDefault<string>();

            dataSet = dataBase.GetData9("SELECT t1.[id_zona], c1.branch_name,[id_user], t1.[creation], t1.[lastupdate] FROM[t_g_garter] t1 left join t_g_cashcentre c1 on t1.id_zona = c1.id " +
                " where t1.id_user = " + DataExchange.CurrentUser.CurrentUserId +
                " order by t1.lastupdate desc");
            textBox6.Text = dataSet.Tables[0].AsEnumerable().Select(x => x.Field<string>("branch_name")).FirstOrDefault<string>();
        }

        private void send1()
        {
            string s1;//="";
            if (comboBox1.SelectedIndex == -1)
            {
                s1 = "";
            }
            else {

                radioButton1.Checked = false;
                userDataSet = null;
                int number1;// = 0;

                int.TryParse(comboBox1.SelectedValue.ToString(), out number1);

                s1 = " and id_marshr = " + number1.ToString();
                //MessageBox.Show(s1);
            }
            dataSet = null;
            dataSet1 = null;
            //    dataSet = dataBase.GetData9("SELECT t1.id, t1.name name,t3.declared_value FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
            //    " and t1.status = 1 and t2.deleted = 0 "+s1+" order by id desc");

            dataSet = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031,sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() + 
                "and t1.status = 1 and t2.deleted = 0 " + s1 + " group by t1.id,t1.name order by t1.id desc");

            if (dataSet2 != null)
            {
                if (dataSet2.Tables[0].Rows.Count > 0)
                {
                    dataSet1 = dataSet.Clone();
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        if (dataSet2.Tables[0].AsEnumerable().Any(
                            x => x.Field<Int64>("id") == Convert.ToInt64(row["id"])
                            ))
                        {
                           // MessageBox.Show(row["name"].ToString());
                        }
                        else
                        {
                            DataRow row1 = dataSet1.Tables[0].NewRow();
                            row1["id"] = row["id"];
                            row1["name"] = row["name"];
                            row1["declared_51031"] = row["declared_51031"];
                            row1["declared_1002"] = row["declared_1002"];
                            row1["declared_1003"] = row["declared_1003"];
                            row1["declared_1006"] = row["declared_1006"];
                            row1["declared_21026"] = row["declared_21026"];
                            row1["declared_21027"] = row["declared_21027"];
                            row1["declared_21028"] = row["declared_21028"];
                            row1["declared_21029"] = row["declared_21029"];

                            dataSet1.Tables[0].Rows.Add(row1);
                        }
                    }

                }
                else
                {
                    dataSet1 = dataSet;
                }

            }
            else
            {
                dataSet1 = dataSet;
            }
            
            if(dataSet1!=null)
            dgSend1.DataSource = dataSet1.Tables[0];
        }
               
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataSet2 != null)
            {
                if (dataSet2.Tables[0].Rows.Count > 0)
                {
                    if (cbUser.SelectedIndex != 0 || cbZona.SelectedIndex != 0)
                    {
                        dataBase.Connect();
                        dataSet = null;
                        dataSet = dataBase.GetData9("select * from t_g_moving_bags");

                        //Создаем новую строку для t_g_moving_bags
                        DataRow row = dataSet.Tables[0].NewRow();
                        row["createdate"] = DateTime.Now;
                        row["last_update"] = DateTime.Now;
                        row["lats_update_user"] = DataExchange.CurrentUser.CurrentUserId; 
                        row["user_send"] = DataExchange.CurrentUser.CurrentUserId;
                        if (cbUser.SelectedIndex != 0)
                             row["user_received"] = cbUser.SelectedValue;
                        if (cbZona.SelectedIndex != 0)
                            row["zona_received"] = cbZona.SelectedValue;
                        row["status"] = "1";

                        row["id_user_create"] = DataExchange.CurrentUser.CurrentUserId;
                        row["id_zona_create"] = DataExchange.CurrentUser.CurrentUserZona;
                        row["id_shift_create"] = DataExchange.CurrentUser.CurrentUserShift;
                        row["id_shift_current"] = DataExchange.CurrentUser.CurrentUserShift;

                        //Добавление строку в коллекцию
                        dataSet.Tables[0].Rows.Add(row);

                        //Обновление БД
                        dataBase.UpdateData(dataSet, "t_g_moving_bags");

                        // Получаем последнию ид из таблицы t_g_moving_bags  и записываем ее в переменную id
                        dataSet = dataBase.GetData9("SELECT TOP (1) [id]  FROM [t_g_moving_bags] order by id desc");
                        long id = 0;
                        foreach (DataRow denomRow in dataSet.Tables[0].Rows) id = Convert.ToInt64(denomRow["id"]);

                        dataSet = null;
                        dataSet = dataBase.GetData9("select * from t_g_moving_bags_detal");

                        //Создаем новые строки для t_g_cashtransfer_detalization

                        foreach (DataRow row2 in dataSet2.Tables[0].Rows)
                        {
                            DataRow row1 = dataSet.Tables[0].NewRow();
                            /// если введенная кол-во меньше от первого по умольчанию кол-во
                            row1["id_moving_bags"] = id.ToString();
                            row1["id_bags"] = row2["id"];
                            row1["status"] = 1;
                            dataSet.Tables[0].Rows.Add(row1);
                            d3 = dataBase.GetData9("Update [t_g_bags] SET [lastupdate] = GETDATE() ,[status]=2 where id = " + row2["id"].ToString());
                        }
                        dataBase.UpdateData(dataSet, "t_g_moving_bags_detal");
                        dataSet1 = dataSet2 = null;
                        dgSend1.DataSource = null;
                        dgSend2.DataSource = null;
                        //dgSend1.DataSource = dataSet1.Tables[0];
                        //dgSend2.DataSource = dataSet2.Tables[0];
                        send1();
                        sost();
                        MessageBox.Show("Передача сформирована!");
                        //dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                        //        "Order by t1.id desc");

                        //        dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                        //" or user_received=" + DataExchange.CurrentUser.CurrentUserId +
                        //" Order by t1.id desc");

                        //        dgList.DataSource = dataSetList.Tables[0];
                        detal1();
                        cbZona.SelectedIndex = 0;
                        cbUser.SelectedIndex = 0;
                    }
                    else MessageBox.Show("Выберите получателя из списка или зону!");
                    
                }
                else MessageBox.Show("Нет сумок для передачи!");
            }
            else MessageBox.Show("Нет сумок для передачи!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(
           "Отменить передачу?"
            ,
            "Сообщение",
           MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1
           //,MessageBoxOptions.DefaultDesktopOnly
           );

            if (result1 == DialogResult.No)
                return;


            if (dgList != null && dataSetList != null)
                if (dataSetList.Tables[0].Rows.Count > 0)
                {
                    if (dgList[6, dgList.CurrentRow.Index].Value.ToString() == "1")
                    {
                        dataSet = dataBase.GetData9(" update t_g_moving_bags set [status]=3" +
                            ", last_update = GETDATE() " +
                            ", [lats_update_user] = " + DataExchange.CurrentUser.CurrentUserId +
                            " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());

                        dataSet = dataBase.GetData9(" update t_g_moving_bags_detal set [status]=3" +
                            " where id_moving_bags =" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());

                        dataSet = dataBase.GetData9("Update [t_g_bags] SET [lastupdate] = GETDATE() ,[status]=1 where id in(select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + " )");

                        dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                            " Order by t1.id desc");
                        dgList.DataSource = dataSetList.Tables[0];
                        MessageBox.Show("Передача отменена");
                        send1();
                        cbUser.SelectedIndex = 0;
                        cbZona.SelectedIndex = 0;
                    }
                    else 
                    { 
                        
                        MessageBox.Show("Не возможно отменить, поскольку - \n'" + dgList[1, dgList.CurrentRow.Index].Value.ToString()+"'");

                    }

                }
            //    else MessageBox.Show("dataSetList.Tables[0].Rows.Count > 0  =  false");
            //else MessageBox.Show("dgList != null && dataSetList != null  =  false");
        }
        private void SendBagsForm_Load(object sender, EventArgs e)
        { 
        
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                comboBox1.SelectedIndex = -1;           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                radioButton1.Checked = true;
            else radioButton1.Checked = false;

            send1();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataSet1.Tables[0].Rows.Count>0)
            if (dgSend1.CurrentCell.RowIndex > -1)
            {
                if (dataSet2 == null)
                {
                        dataSet2 = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031,sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                            "and t1.status = 1 and t1.id = " + dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["id"] +
                            " group by t1.id,t1.name order by t1.id desc");
                }else
                {
                    DataRow row = dataSet2.Tables[0].NewRow();
                        row["id"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["id"];
                        row["name"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["name"];
                        row["declared_51031"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_51031"];
                        row["declared_1002"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_1002"];
                        row["declared_1003"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_1003"];
                        row["declared_1006"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_1006"];
                        row["declared_21026"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_21026"];
                        row["declared_21027"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_21027"];
                        row["declared_21028"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_21028"];
                        row["declared_21029"] = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["declared_21029"];


                        if (!dataSet2.Tables[0].AsEnumerable().Any(
                            x=>x.Field<Int64>("id")==Convert.ToInt64(dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["id"])
                            ))
                    dataSet2.Tables[0].Rows.Add(row);//[0].Rows.Add(d2);

                }
                DataRow row1 = dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex];

                dataSet1.Tables[0].Rows.Remove(row1);

                dgSend1.DataSource = dataSet1.Tables[0];
                dgSend2.DataSource = dataSet2.Tables[0];
            }
            sost();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            send1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataSet2 != null)
            if (dataSet2.Tables[0].Rows.Count > 0)
                if (dgSend2.CurrentCell.RowIndex > -1)
                {
                        if (dataSet1 == null)
                        {

                            //dataSet1 = dataBase.GetData9("SELECT t1.id, t1.name name ,t3.declared_value FROM t_g_bags t1 inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                            //" and t1.status = 1 and t1.id = " + dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["id"] +                       
                            //" order by id desc");

                            dataSet1 = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031, sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                                "and t1.status = 1 and t1.id = " + dataSet2.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["id"] +
                                " group by t1.id,t1.name order by t1.id desc");

                        }
                        else
                        {
                            DataRow row = dataSet1.Tables[0].NewRow();
                            row["id"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["id"];
                            row["name"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["name"];
                            row["declared_51031"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_51031"];
                            row["declared_1002"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_1002"];
                            row["declared_1003"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_1003"];
                            row["declared_1006"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_1006"];
                            row["declared_21026"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_21026"];
                            row["declared_21027"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_21027"];
                            row["declared_21028"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_21028"];
                            row["declared_21029"] = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex]["declared_21029"];


                            dataSet1.Tables[0].Rows.Add(row);

                        }
                    DataRow row1 = dataSet2.Tables[0].Rows[dgSend2.CurrentCell.RowIndex];
                   
                    dataSet2.Tables[0].Rows.Remove(row1);

                    dgSend1.DataSource = dataSet1.Tables[0];
                    dgSend2.DataSource = dataSet2.Tables[0];
                }
            sost();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataSet2 == null)
                dataSet2 = dataSet1.Clone();
            if(dataSet1!=null)
                if(dataSet1.Tables[0].Rows.Count>0)
                {
                    foreach (DataRow row in dataSet1.Tables[0].Rows)
                    {
                        DataRow row1 = dataSet2.Tables[0].NewRow();
                        row1["id"] = row["id"];
                        row1["name"] = row["name"];
                        row1["declared_51031"] = row["declared_51031"];
                        row1["declared_1002"] = row["declared_1002"];
                        row1["declared_1003"] = row["declared_1003"];
                        row1["declared_1006"] = row["declared_1006"];
                        row1["declared_21026"] = row["declared_21026"];
                        row1["declared_21027"] = row["declared_21027"];
                        row1["declared_21028"] = row["declared_21028"];
                        row1["declared_21029"] = row["declared_21029"];
                        if (!dataSet2.Tables[0].AsEnumerable().Any(
                            x => x.Field<Int64>("id") == Convert.ToInt64(row["id"])
                            ))
                            dataSet2.Tables[0].Rows.Add(row1);
                    
                    }
                    dgSend2.DataSource = dataSet2.Tables[0];

                    dataSet1.Tables[0].Rows.Clear();                    
                }
            sost();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(dataSet2!=null)
                if(dataSet2.Tables[0].Rows.Count>0)
                {
                    foreach(DataRow row in dataSet2.Tables[0].Rows)
                    {
                        DataRow row1 = dataSet1.Tables[0].NewRow();
                        row1["id"] = row["id"];
                        row1["name"] = row["name"];
                        row1["declared_51031"] = row["declared_51031"];
                        row1["declared_1002"] = row["declared_1002"];
                        row1["declared_1003"] = row["declared_1003"];
                        row1["declared_1006"] = row["declared_1006"];
                        row1["declared_21026"] = row["declared_21026"];
                        row1["declared_21027"] = row["declared_21027"];
                        row1["declared_21028"] = row["declared_21028"];
                        row1["declared_21029"] = row["declared_21029"];

                        dataSet1.Tables[0].Rows.Add(row1);

                    }
                    dgSend1.DataSource = dataSet1.Tables[0];

                    dataSet2.Tables[0].Rows.Clear();
                }
            sost();
        }

        private void sost()
        {
            listBox1.Items.Clear();

            if (dataSet2 != null)
            {
                if (dataSet2.Tables[0].Rows.Count > 0)
                {
                    int count = dataSet2.Tables[0].Rows.Count;
                    textBox1.Text = count.ToString().Trim();
                    Double sum51031, sum1002, sum1003, sum1006, sum21026, sum21027, sum21028, sum21029;
                    sum51031= sum1002 = sum1003=sum1006=sum21026=sum21027=sum21028=sum21029=0;
                    
                    foreach (DataRow row2 in dataSet2.Tables[0].Rows)
                    {
                        sum51031 += Convert.ToDouble(row2["declared_51031"]);
                        sum1002 += Convert.ToDouble(row2["declared_1002"]);
                        sum1003 += Convert.ToDouble(row2["declared_1003"]);
                        sum1006 += Convert.ToDouble(row2["declared_1006"]);
                        sum21026 += Convert.ToDouble(row2["declared_21026"]);
                        sum21027 += Convert.ToDouble(row2["declared_21027"]);
                        sum21028 += Convert.ToDouble(row2["declared_21028"]);
                        sum21029 += Convert.ToDouble(row2["declared_21029"]);
                    }
                    listBox1.Items.Add("UZS = " + sum51031.ToString());
                    listBox1.Items.Add("KZT = "+sum1002.ToString());
                    listBox1.Items.Add("USD = " + sum1003.ToString());
                    listBox1.Items.Add("EUR = " + sum1006.ToString());
                    listBox1.Items.Add("RUB = " + sum21026.ToString());
                    listBox1.Items.Add("GBP = " + sum21027.ToString());
                    listBox1.Items.Add("CHF = " + sum21028.ToString());
                    listBox1.Items.Add("CNY = " + sum21029.ToString());
                }
                else
                    textBox1.Text = "0";
            }
            else
                textBox1.Text = "0";
            


        }


        private void button7_Click(object sender, EventArgs e)
        {
            if(textBox3.Text!="")
            {
                
                string name = textBox3.Text.Trim();
                if (dataSet1.Tables[0].AsEnumerable().Any(
                    x => x.Field<string>("name").Trim() == name
                    ))
                {
                    

                    if (dataSet1.Tables[0].Rows.Count > 0)
                        if (dgSend1.CurrentCell.RowIndex > -1)
                        {
                            Int64 id = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
                            Decimal declared_51031 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_51031")).FirstOrDefault<Decimal>();
                            Decimal declared_1002 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_1002")).FirstOrDefault<Decimal>();
                            Decimal declared_1003 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_1003")).FirstOrDefault<Decimal>();
                            Decimal declared_1006 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_1006")).FirstOrDefault<Decimal>();
                            Decimal declared_21026 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21026")).FirstOrDefault<Decimal>();
                            Decimal declared_21027 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21027")).FirstOrDefault<Decimal>();
                            Decimal declared_21028 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21028")).FirstOrDefault<Decimal>();
                            Decimal declared_21029 = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21029")).FirstOrDefault<Decimal>();

                            if (dataSet2 == null)
                            {
                                // dataSet2 = dataBase.GetData9("SELECT t1.id, t1.name name,t3.declared_value FROM t_g_bags t1 inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                                //" and t1.status = 1 and t1.id = " + id +
                                // " order by id desc");

                                dataSet2 = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031,sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                            "and t1.status = 1 and t1.id = " + id +
                            " group by t1.id,t1.name order by t1.id desc");


                            }
                            else
                            {
                                // d2 = dataBase.GetData9("SELECT t1.id, t1.name name FROM t_g_bags t1 where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                                //" and t1.status = 1 and t1.id = " + id +
                                //" order by id desc");
                                DataRow row2 = dataSet2.Tables[0].NewRow();
                                row2["id"] = id;
                                row2["name"] = name;
                                row2["declared_51031"] = declared_51031;
                                row2["declared_1002"] = declared_1002;
                                row2["declared_1003"] = declared_1003;
                                row2["declared_1006"] = declared_1006;
                                row2["declared_21026"] = declared_21026;
                                row2["declared_21027"] = declared_21027;
                                row2["declared_21028"] = declared_21028;
                                row2["declared_21029"] = declared_21029;

                                if (!dataSet2.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id") == id && x.Field<string>("name")==name ))
                                    dataSet2.Tables[0].Rows.Add(row2);

                            }
                            
                            dataSet = null;
                            dataSet = dataSet1.Clone();
                            foreach(DataRow row1 in dataSet1.Tables[0].Rows)
                                if(Convert.ToInt64(row1["id"])!=id)
                                {
                                    DataRow row = dataSet.Tables[0].NewRow();
                                    row["id"] = row1["id"];
                                    row["name"] = row1["name"];
                                    row["declared_51031"] = row1["declared_51031"];
                                    row["declared_1002"] = row1["declared_1002"];
                                    row["declared_1003"] = row1["declared_1003"];
                                    row["declared_1006"] = row1["declared_1006"];
                                    row["declared_21026"] = row1["declared_21026"];
                                    row["declared_21027"] = row1["declared_21027"];
                                    row["declared_21028"] = row1["declared_21028"];
                                    row["declared_21029"] = row1["declared_21029"];

                                    dataSet.Tables[0].Rows.Add(row);
                                }
                            dataSet1 = dataSet;
                            dgSend1.DataSource = dataSet1.Tables[0];
                            dgSend2.DataSource = dataSet2.Tables[0];
                        }

                    sost();
                }
                else MessageBox.Show("No this text");

                
                textBox3.Text = "";
            }
        }

        private void cbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbUser.SelectedIndex != 0)
            //    cbZona.SelectedIndex = -1;
        }

        private void cbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbZona.SelectedIndex != -1)
            //    cbUser.SelectedIndex = 0;
        }

        private void dgList_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        private void pok1()
        {

            if (dgList.CurrentCell != null)
                if (dgList[0, dgList.CurrentRow.Index].Value.ToString() != "0")
                    if(dgList[0, dgList.CurrentRow.Index].Value.ToString() != "")
                    {
                        // MessageBox.Show("1"+dgList[0, dgList.CurrentRow.Index].Value.ToString()+"1");
                        dataSetR1 = dataBase.GetData9(" SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031,sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) left join t_g_moving_bags_detal m1 on t1.id=m1.id_bags " +
                        " where m1.id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString() +
                        " group by t1.id,t1.name order by t1.id desc ");
                        dataSetR2 = null;
                        //MessageBox.Show("dgList[7, dgList.CurrentRow.Index]=" + dgList[7, dgList.CurrentRow.Index].Value.ToString());
                        //MessageBox.Show("DataExchange.CurrentUser.CurrentUserId=" + DataExchange.CurrentUser.CurrentUserId.ToString());

                        if (Convert.ToInt64(dgList[7, dgList.CurrentRow.Index].Value) == DataExchange.CurrentUser.CurrentUserId)
                            if(pm.EnabledPossibility(perm, button13))
                                button13.Enabled = true; 
                        else
                            button13.Enabled = false;
                    }
            if (dataSetR1 !=null)
            dgReceive1.DataSource = dataSetR1.Tables[0];
            dgReceive2.DataSource = null;

        }

        private void dgList_Click(object sender, EventArgs e)
        {
            if (dgList.CurrentCell != null)
            {
                //MessageBox.Show(dgList[0, dgList.CurrentRow.Index].Value.ToString());
                pok1();

            }

        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (dataSetR1.Tables[0].Rows.Count > 0)
                if (dgReceive1.CurrentCell.RowIndex > -1)
                {
                    if (dataSetR2 == null)
                    {
                        
                        dataSetR2 = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031, sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) " +
                            " where t1.id = "+dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["id"]+
                            " group by t1.id,t1.name order by t1.id desc");

                        
                    }
                    else
                    {
                        DataRow row = dataSetR2.Tables[0].NewRow();
                        row["id"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["id"];
                        row["name"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["name"];
                        row["declared_51031"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_51031"];
                        row["declared_1002"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_1002"];
                        row["declared_1003"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_1003"];
                        row["declared_1006"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_1006"];
                        row["declared_21026"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_21026"];
                        row["declared_21027"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_21027"];
                        row["declared_21028"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_21028"];
                        row["declared_21029"] = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["declared_21029"];


                        if (!dataSetR2.Tables[0].AsEnumerable().Any(
                            x => x.Field<Int64>("id") == Convert.ToInt64(dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex]["id"])
                            ))
                            dataSetR2.Tables[0].Rows.Add(row);//[0].Rows.Add(d2);

                    }
                    DataRow row1 = dataSetR1.Tables[0].Rows[dgReceive1.CurrentCell.RowIndex];

                    dataSetR1.Tables[0].Rows.Remove(row1);

                    dgReceive1.DataSource = dataSetR1.Tables[0];
                    dgReceive2.DataSource = dataSetR2.Tables[0];
                }
            sostR();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataSetR2 != null)
                if (dataSetR2.Tables[0].Rows.Count > 0)
                    if (dgReceive2.CurrentCell.RowIndex > -1)
                    {
                        if (dataSetR1 == null)
                        {
                            dataSetR1 = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031, sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) " +
                            " where t1.id = " + dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["id"] +                                
                                " group by t1.id,t1.name order by t1.id desc");
                        }
                        else
                        {
                            DataRow row = dataSetR1.Tables[0].NewRow();
                            row["id"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["id"];
                            row["name"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["name"];
                            row["declared_51031"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_51031"];
                            row["declared_1002"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_1002"];
                            row["declared_1003"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_1003"];
                            row["declared_1006"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_1006"];
                            row["declared_21026"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_21026"];
                            row["declared_21027"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_21027"];
                            row["declared_21028"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_21028"];
                            row["declared_21029"] = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex]["declared_21029"];


                            dataSetR1.Tables[0].Rows.Add(row);

                        }
                        DataRow row1 = dataSetR2.Tables[0].Rows[dgReceive2.CurrentCell.RowIndex];

                        dataSetR2.Tables[0].Rows.Remove(row1);

                        dgReceive1.DataSource = dataSetR1.Tables[0];
                        dgReceive2.DataSource = dataSetR2.Tables[0];
                    }
            sostR();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataSetR2 == null)
                dataSetR2 = dataSetR1.Clone();
            if (dataSetR1 != null)
                if (dataSetR1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSetR1.Tables[0].Rows)
                    {
                        DataRow row1 = dataSetR2.Tables[0].NewRow();
                        row1["id"] = row["id"];
                        row1["name"] = row["name"];
                        row1["declared_51031"] = row["declared_51031"];
                        row1["declared_1002"] = row["declared_1002"];
                        row1["declared_1003"] = row["declared_1003"];
                        row1["declared_1006"] = row["declared_1006"];
                        row1["declared_21026"] = row["declared_21026"];
                        row1["declared_21027"] = row["declared_21027"];
                        row1["declared_21028"] = row["declared_21028"];
                        row1["declared_21029"] = row["declared_21029"];
                        if (!dataSetR2.Tables[0].AsEnumerable().Any(
                            x => x.Field<Int64>("id") == Convert.ToInt64(row["id"])
                            ))
                            dataSetR2.Tables[0].Rows.Add(row1);

                    }
                    dgReceive2.DataSource = dataSetR2.Tables[0];

                    dataSetR1.Tables[0].Rows.Clear();
                }
            sostR();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataSetR2 != null)
                if (dataSetR2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSetR2.Tables[0].Rows)
                    {
                        DataRow row1 = dataSetR1.Tables[0].NewRow();
                        row1["id"] = row["id"];
                        row1["name"] = row["name"];

                        row1["declared_51031"] = row["declared_51031"];
                        row1["declared_1002"] = row["declared_1002"];
                        row1["declared_1003"] = row["declared_1003"];
                        row1["declared_1006"] = row["declared_1006"];
                        row1["declared_21026"] = row["declared_21026"];
                        row1["declared_21027"] = row["declared_21027"];
                        row1["declared_21028"] = row["declared_21028"];
                        row1["declared_21029"] = row["declared_21029"];

                        dataSetR1.Tables[0].Rows.Add(row1);

                    }
                    dgReceive1.DataSource = dataSetR1.Tables[0];

                    dataSetR2.Tables[0].Rows.Clear();
                }
            sostR();
        }

        private void sostR()
        {
            listBox2.Items.Clear();

            if (dataSetR2 != null)
            {
                if (dataSetR2.Tables[0].Rows.Count > 0)
                {
                    int count = dataSetR2.Tables[0].Rows.Count;
                    textBox4.Text = count.ToString().Trim();
                    Double sum51031,sum1002, sum1003, sum1006, sum21026, sum21027, sum21028, sum21029;
                    sum51031 = sum1002 = sum1003 = sum1006 = sum21026 = sum21027 = sum21028 = sum21029 = 0;

                    foreach (DataRow row2 in dataSetR2.Tables[0].Rows)
                    {
                        sum51031 += Convert.ToDouble(row2["declared_51031"]);
                        sum1002 += Convert.ToDouble(row2["declared_1002"]);
                        sum1003 += Convert.ToDouble(row2["declared_1003"]);
                        sum1006 += Convert.ToDouble(row2["declared_1006"]);
                        sum21026 += Convert.ToDouble(row2["declared_21026"]);
                        sum21027 += Convert.ToDouble(row2["declared_21027"]);
                        sum21028 += Convert.ToDouble(row2["declared_21028"]);
                        sum21029 += Convert.ToDouble(row2["declared_21029"]);
                    }
                    listBox2.Items.Add("UZS = " + sum51031.ToString());
                    listBox2.Items.Add("KZT = " + sum1002.ToString());
                    listBox2.Items.Add("USD = " + sum1003.ToString());
                    listBox2.Items.Add("EUR = " + sum1006.ToString());
                    listBox2.Items.Add("RUB = " + sum21026.ToString());
                    listBox2.Items.Add("GBP = " + sum21027.ToString());
                    listBox2.Items.Add("CHF = " + sum21028.ToString());
                    listBox2.Items.Add("CNY = " + sum21029.ToString());
                }
                else
                    textBox4.Text = "0";
            }
            else
                textBox4.Text = "0";



        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {

                string name = textBox2.Text.Trim();
                if (dataSetR1.Tables[0].AsEnumerable().Any(
                    x => x.Field<string>("name").Trim() == name
                    ))
                {


                    if (dataSetR1.Tables[0].Rows.Count > 0)
                        if (dgReceive1.CurrentCell.RowIndex > -1)
                        {
                            Int64 id = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
                            Decimal declared_51031 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_51031")).FirstOrDefault<Decimal>();
                            Decimal declared_1002 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_1002")).FirstOrDefault<Decimal>();
                            Decimal declared_1003 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_1003")).FirstOrDefault<Decimal>();
                            Decimal declared_1006 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_1006")).FirstOrDefault<Decimal>();
                            Decimal declared_21026 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21026")).FirstOrDefault<Decimal>();
                            Decimal declared_21027 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21027")).FirstOrDefault<Decimal>();
                            Decimal declared_21028 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21028")).FirstOrDefault<Decimal>();
                            Decimal declared_21029 = dataSetR1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Decimal>("declared_21029")).FirstOrDefault<Decimal>();

                            if (dataSetR2 == null)
                            {
                                
                                dataSetR2 = dataBase.GetData9("SELECT t1.id, t1.name name ,sum( iif(t3.id_currency=51031, t3.declared_value,0)) as declared_51031,sum( iif(t3.id_currency=1002, t3.declared_value,0)) as declared_1002,sum(iif(t3.id_currency=1003, t3.declared_value,0)) as declared_1003,sum(iif(t3.id_currency=1006, t3.declared_value,0)) as declared_1006,sum(iif(t3.id_currency=21026, t3.declared_value,0)) as declared_21026,sum(iif(t3.id_currency=21027, t3.declared_value,0)) as declared_21027,sum(iif(t3.id_currency=21028, t3.declared_value,0)) as declared_21028,sum(iif(t3.id_currency=21029, t3.declared_value,0)) as declared_21029 FROM t_g_bags t1 inner join t_g_counting t2 on(t2.id_bag = t1.id) inner join t_g_counting_content t3 on (t3.id_bag=t1.id) " +
                                    "where  t1.id = " + id +
                            " group by t1.id,t1.name order by t1.id desc");


                            }
                            else
                            {
                                // d2 = dataBase.GetData9("SELECT t1.id, t1.name name FROM t_g_bags t1 where t1.last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() +
                                //" and t1.status = 1 and t1.id = " + id +
                                //" order by id desc");
                                DataRow row2 = dataSetR2.Tables[0].NewRow();
                                row2["id"] = id;
                                row2["name"] = name;
                                row2["declared_51031"] = declared_51031;
                                row2["declared_1002"] = declared_1002;
                                row2["declared_1003"] = declared_1003;
                                row2["declared_1006"] = declared_1006;
                                row2["declared_21026"] = declared_21026;
                                row2["declared_21027"] = declared_21027;
                                row2["declared_21028"] = declared_21028;
                                row2["declared_21029"] = declared_21029;

                                if (!dataSetR2.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id") == id && x.Field<string>("name") == name))
                                    dataSetR2.Tables[0].Rows.Add(row2);

                            }

                            dataSet = null;
                            dataSet = dataSetR1.Clone();
                            foreach (DataRow row1 in dataSetR1.Tables[0].Rows)
                                if (Convert.ToInt64(row1["id"]) != id)
                                {
                                    DataRow row = dataSet.Tables[0].NewRow();
                                    row["id"] = row1["id"];
                                    row["name"] = row1["name"];
                                    row["declared_51031"] = row1["declared_51031"];
                                    row["declared_1002"] = row1["declared_1002"];
                                    row["declared_1003"] = row1["declared_1003"];
                                    row["declared_1006"] = row1["declared_1006"];
                                    row["declared_21026"] = row1["declared_21026"];
                                    row["declared_21027"] = row1["declared_21027"];
                                    row["declared_21028"] = row1["declared_21028"];
                                    row["declared_21029"] = row1["declared_21029"];

                                    dataSet.Tables[0].Rows.Add(row);
                                }
                            dataSetR1 = dataSet;
                            dgReceive1.DataSource = dataSetR1.Tables[0];
                            dgReceive2.DataSource = dataSetR2.Tables[0];
                        }

                    sostR();
                }
                else MessageBox.Show("No this text");


                textBox2.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
           if ((e.KeyCode == Keys.Escape) | (e.KeyCode == Keys.Enter))
            {
                button8_Click(sender, e);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) | (e.KeyCode == Keys.Enter))
            {
                button7_Click(sender, e);
            }

        }
        //Кнопка Принять
        private void button13_Click(object sender, EventArgs e)
        {
            if (dgList != null && dataSetList != null)
                if (dataSetList.Tables[0].Rows.Count > 0)
                {
                    if (dgList[6, dgList.CurrentRow.Index].Value.ToString() == "1")
                    {
                        if (dgReceive1.Rows.Count > 0)
                        {
                            MessageBox.Show(" Вы приняли не все сумки! ");
                        }
                        else
                        {
                            dataSet = dataBase.GetData9(" update t_g_moving_bags set [status]=2" +
                        ", last_update = GETDATE() " +
                        ", id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift+
                        ", [user_received] = " + DataExchange.CurrentUser.CurrentUserId +
                        ", [lats_update_user] = " + DataExchange.CurrentUser.CurrentUserId +
                        " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());

                            dataSet = dataBase.GetData9(" update t_g_moving_bags_detal set [status]=2" +

                        " where id_moving_bags =" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());

                            //меняем смену bags
                            dataSet = dataBase.GetData9("Update [t_g_bags] SET [lastupdate] = GETDATE(), id_shift_current="+DataExchange.CurrentUser.CurrentUserShift+",[status]=1, [last_user_update] = " + DataExchange.CurrentUser.CurrentUserId +
                                " where id in(select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + " )");

                            //меняем смену cards
                            dataSet = dataBase.GetData9("Update [t_g_cards] SET [lastupdate] = GETDATE(), [id_shift_current]=" + DataExchange.CurrentUser.CurrentUserShift + ", [last_user_update] = " + DataExchange.CurrentUser.CurrentUserId +
                                " where id_bag in(select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + " )");

                            //меняем смену counting
                            dataSet = dataBase.GetData9("Update [t_g_counting] SET [lastupdate] = GETDATE(), [id_shift_current]=" + DataExchange.CurrentUser.CurrentUserShift + ", [last_user_update] = " + DataExchange.CurrentUser.CurrentUserId +
                                " where id_bag in (select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + " )");


                            //меняем смену counting_content
                            dataSet = dataBase.GetData9("Update [t_g_counting_content] SET [lastupdate] = GETDATE(), [id_shift_current]=" + DataExchange.CurrentUser.CurrentUserShift + ", [last_user_update] = " + DataExchange.CurrentUser.CurrentUserId +
                                " where [id_bag] in (select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + " )");

                            //меняем смену t_g_marschrut
                            dataSet = dataBase.GetData9("Update t_g_marschrut SET [lastupdate] = GETDATE(), [id_shift_current]=" + DataExchange.CurrentUser.CurrentUserShift + ", last_update_user ="+ DataExchange.CurrentUser.CurrentUserId +
                                " where id in (select id_marshr from t_g_bags where id in (select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + "))");

                            //меняем смену multi_bags
                            dataSet = dataBase.GetData9("Update t_g_multi_bags SET [lastupdate] = GETDATE(), [id_shift_current]=" + DataExchange.CurrentUser.CurrentUserShift + ", last_update_user =" + DataExchange.CurrentUser.CurrentUserId +
                                " where id in (select id_multi_bag from t_g_counting where id_bag in (select id_bags from t_g_moving_bags_detal where id_moving_bags= " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + "))");


                            //dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(u2.user_name is null, c1.branch_name,c3.branch_name) as[Зона получатель]," +
                            //    "case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем'when status=5 then 'Передача принята частично !!! ' end as [Статус] ," +
                            //    "status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id " +
                            //    " where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                            //        "Order by t1.id desc");
                            //dgList.DataSource = dataSetList.Tables[0];

                            //dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                            //    "Order by t1.id desc");
                //            dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                //" or user_received=" + DataExchange.CurrentUser.CurrentUserId +
                //" Order by t1.id desc");
                //            dgList.DataSource = dataSetList.Tables[0];

                            detal1();
                            MessageBox.Show("Передача принята");
                            send1();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не возможно принять, поскольку  - \n '" + dgList[5, dgList.CurrentRow.Index].Value.ToString() + "'");
                        //MessageBox.Show("dgList[5, dgList.CurrentRow.Index].Value.ToString() == '1'  =  false");
                        //MessageBox.Show("dgList[5, dgList.CurrentRow.Index].Value.ToString() =" + dgList[6, dgList.CurrentRow.Index].Value.ToString());

                    }

                }
            //    else MessageBox.Show("dataSetList.Tables[0].Rows.Count > 0  =  false");
            //else MessageBox.Show("dgList != null && dataSetList != null  =  false");
        }
        //Кнопка Отменить Получателем
        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(
           "Отменить прием сумок?"
            ,
            "Сообщение",
           MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1
           //,MessageBoxOptions.DefaultDesktopOnly
           );

            if (result1 == DialogResult.No)
                return;


            if (dgList != null && dataSetList != null)
                if (dataSetList.Tables[0].Rows.Count > 0)
                {
                    if (dgList[6, dgList.CurrentRow.Index].Value.ToString() == "1")
                    {
                        dataSet = dataBase.GetData9(" update t_g_moving_bags set [status]=4" +
                    ", last_update = GETDATE() " +
                    ", [lats_update_user] = " + DataExchange.CurrentUser.CurrentUserId +
                    " where id=" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());

                        dataSet = dataBase.GetData9(" update t_g_moving_bags_detal set [status]=4" +

                    " where id_moving_bags =" + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());


                        dataSet = dataBase.GetData9("Update [t_g_bags] SET [lastupdate] = GETDATE() ,[status]=1 where id in(select id_bags from t_g_moving_bags_detal where id_moving_bags = " + dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() + " )");

                        //dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],u1.user_name as [Отправитель],c2.branch_name as[Зона oтправитель],u2.user_name as [Получатель],iif(u2.user_name is null, c1.branch_name,c3.branch_name) as[Зона получатель],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем'when status=5 then 'Передача принята частично !!! ' end as [Статус] ,status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id where user_send = " + DataExchange.CurrentUser.CurrentUserId +
                        //        "Order by t1.id desc");
                        //dgList.DataSource = dataSetList.Tables[0];
                        detal1();
                        MessageBox.Show("Передача отменена");
                        send1();
                    }
                    else
                    {
                        //MessageBox.Show("dgList[5, dgList.CurrentRow.Index].Value.ToString() == '1'  =  false");
                        //MessageBox.Show("dgList[5, dgList.CurrentRow.Index].Value.ToString() =" + dgList[6, dgList.CurrentRow.Index].Value.ToString());
                        MessageBox.Show("Не возможно отменить, поскольку  - \n'" + dgList[1, dgList.CurrentRow.Index].Value.ToString() + "'");

                    }

                }
            //    else MessageBox.Show("dataSetList.Tables[0].Rows.Count > 0  =  false");
            //else MessageBox.Show("dgList != null && dataSetList != null  =  false");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            detal1();
        }

        private void detal1()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус], u1.user_name as [Отправитель],u2.user_name as [Получатель],c2.branch_name as[Зона oтправитель],iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id " +
                    " where user_send = " + DataExchange.CurrentUser.CurrentUserId +
               // " or user_received=" + DataExchange.CurrentUser.CurrentUserId +
               " and id_shift_create=" +DataExchange.CurrentUser.CurrentUserShift+
                " Order by t1.id desc");

                //MessageBox.Show("1");
            }
            else
            {
                dataSetList = dataBase.GetData9("SELECT t1.[id] as [Номер передачи],case when status=1 then 'Передача создана' when status=2 then 'Передача принята' when status=3 then 'Передача отменена отправителем' when status=4 then 'Передача отменена получателем' end as [Статус], u1.user_name as [Отправитель], u2.user_name as [Получатель], c2.branch_name as[Зона oтправитель], iif(c1.branch_name is null, c3.branch_name,c1.branch_name) as[Зона получатель],status,user_received,[createdate] as [Дата создания передачи] FROM t_g_moving_bags t1 left join t_g_user u1 on t1.user_send=u1.id left join t_g_user u2 on t1.user_received=u2.id left join t_g_cashcentre c1 on t1.zona_received = c1.id left join t_g_garter g1 on t1.user_send=g1.id_user left join t_g_cashcentre c2 on g1.id_zona=c2.id left join t_g_garter g2 on u2.id=g2.id_user left join t_g_cashcentre c3 on g2.id_zona=c3.id " +
                    " where " +
                    //" user_send = " + DataExchange.CurrentUser.CurrentUserId +
               " user_received=" + DataExchange.CurrentUser.CurrentUserId +
               " and (id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift +
               " or status =1) " +
                " Order by t1.id desc");
               // MessageBox.Show("2");
            } 
            dgList.DataSource = dataSetList.Tables[0];
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgList.Rows.Count == 0 & dgList.CurrentRow.Index < 1)
            {
                MessageBox.Show("Необходимо выбрать передачу");
                return;
            }
            ReportDocument reportDocument1 = new ReportDocument();
            reportDocument1.Load(@"C:\\report\\BagTransfer.rpt");

            StreamReader studIni = new StreamReader(@"C:\\report\\BagTransfer.ini", UTF8Encoding.Default);
            string x = studIni.ReadToEnd();
            //string[] 
            string[] y = x.Split('\n');

            for (int i = 0; i < y.Length; i++)
            {
                switch (y[i].Trim())
                {
                    case "PRM_IdMovingBags":
                        reportDocument1.SetParameterValue(i, dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim());
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

            reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\Отчеты\Перемещения сумок " + datetime +/*"."+time+*/".pdf");


            CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();

            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета


            ReportShowForm reportShowForm = new ReportShowForm();

            reportShowForm.Text = "Перемещения сумок";
            reportShowForm.name = "Перемещения сумок";
            reportShowForm.crystalReportViewer1 = crystalReportViewer1;
            reportShowForm.reportDocument = reportDocument1;
            reportShowForm.Show();
        }
    }
}

