using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CountingDB;
using DataExchange;
using System.Linq;
using Login;
using CountingForms.Services;
using CountingDB.Entities;

namespace CountingForms.DictionaryForms
{    
    public partial class UsersForm : DictionaryForm
    {
        private DataSet cashCentreDataSet = null;

        /////12.11.2019
        private DataSet tippolz = null;
        /////12.11.2019
        private DataSet roleDataSet = null;

        private DataSet userDataSet = null;
        private DataSet userDataSet2 = null;
        private DataSet userDataSet3 = null;
        public UsersForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public UsersForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            //Инициализация компонентов управления
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
            //Запрос данных по кассовым центрам
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();
            cashCentreDataSet = dataBase.GetData("t_g_cashcentre");
            rb1.Checked = true;

            roleDataSet = dataBase.GetData("t_g_role");

            cbRole.DataSource = roleDataSet.Tables[0];
            cbRole.DisplayMember = "name";
            cbRole.ValueMember = "id";
            cbRole.SelectedIndex = -1;


            //Заполнение кассовых центров  
            cbCashCentre.DataSource = cashCentreDataSet.Tables[0];
            cbCashCentre.DisplayMember = "branch_name";
            cbCashCentre.ValueMember = "id";
            cbCashCentre.SelectedIndex = -1;
            cbCashCentre.SelectedIndexChanged += new EventHandler(cbCashCentre_SelectedIndexChanged);


            /////22.10.2019
            // dgList.Columns.Add("id_brach", "Подразделение");
            //  dgList.Columns["id_brach"].DataPropertyName = "id_brach";


            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name = "id_brach";
            dg1.HeaderText = "Подразделение";
            dg1.DataPropertyName = "id_brach";
            dg1.DisplayMember = "branch_name";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = cashCentreDataSet.Tables[0];
            dgList.Columns.Add(dg1);
            dgList.Columns["id_brach"].SortMode = DataGridViewColumnSortMode.Automatic;

            dgList.Columns.Add("id_role", "Роль");
            dgList.Columns["id_role"].DataPropertyName = "id_role";
            //dgList.Columns["id_role"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns.Add("code", "Логин");
            dgList.Columns["code"].DataPropertyName = "code";
            // dgList.Columns["code"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns.Add("reference", "Должность");
            dgList.Columns["reference"].DataPropertyName = "reference";
            //dgList.Columns["reference"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.Columns.Add("expire_date", "Срок действия пароля");
            dgList.Columns["expire_date"].DataPropertyName = "expire_date";
            //dgList.Columns["expire_date"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("tip_pol", "tip_pol");
            dgList.Columns["tip_pol"].DataPropertyName = "tip_pol";
            dgList.Columns["tip_pol"].Visible = false;
            dgList.Columns.Add("id", "id");
            dgList.Columns["id"].DataPropertyName = "id";
            dgList.Columns["id"].Visible = false;

            dgList.Columns.Add("person_number", "Табельный номер");
            dgList.Columns["person_number"].DataPropertyName = "person_number";

            dgList.Columns.Add("rasrprper1", "rasrprper1");
            dgList.Columns["rasrprper1"].DataPropertyName = "rasrprper1";
            dgList.Columns["rasrprper1"].Visible = false;


            /////22.10.2019


            /////22.01.2020

            dataSet = dataBase.GetData9("select * from t_g_user where deleted is null order by user_name");
            dgList.DataSource = dataSet.Tables[0];

            /////22.01.2020

            /////12.11.2019

            tippolz = dataBase.GetData9("SELECT id,num_form,num_compon,name_compon,value_compon,dopol_pref  FROM t_g_nastr_form where num_form = 1 and num_compon = 1");

            int i1 = 1, i2 = 20;
            if (tippolz != null)
            {

                if (tippolz.Tables[0] != null)
                {
                    foreach (DataRow tip1 in tippolz.Tables[0].Rows)
                    {


                        RadioButton rb = new RadioButton();
                        rb.Text = tip1["name_compon"].ToString();
                        rb.Location = new Point(10, i2);
                        rb.Name = "radiobutton" + i1.ToString();
                        rb.AutoSize = true;
                        rb.Click += new EventHandler(rb_CheckedChanged);
                        //new EventHandler(vstavkpref);



                        groupBox1.Controls.Add(rb);
                        i1 = i1 + 1;
                        i2 = i2 + 30;

                    }
                }

            }

            /////12.11.2019


            ////22.01.2020
            timer1.Enabled = true;
            ////22.01.2020


            //01.10.2020
            userDataSet = dataBase.GetData9("select * from t_g_user where deleted is null and person_number is not null order by user_name");
            userDataSet2 = dataBase.GetData9("select * from t_g_user where deleted is null order by user_name");
            userDataSet3 = dataBase.GetData9("select * from t_g_user where deleted is null and person_number!='' order by user_name");
            //accountDataSet2 = dataBase.GetData9("select t1.*from t_g_account t1 left join t_g_client t2 on t1.id_client=t2.id where deleted=0 order by t2.name");


            //Список имен
            comboBox2.Text = "";
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "user_name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = userDataSet2.Tables[0];
            comboBox2.SelectedIndex = -1;

            //Список логину
            comboBox4.Text = "";
            comboBox4.DataSource = null;
            comboBox4.Items.Clear();
            comboBox4.DisplayMember = "code";
            comboBox4.ValueMember = "id";
            comboBox4.DataSource = userDataSet2.Tables[0];
            comboBox4.SelectedIndex = -1;

            //Список табельный номер
            comboBox5.Text = "";
            comboBox5.DataSource = null;
            comboBox5.Items.Clear();
            comboBox5.DisplayMember = "person_number";
            comboBox5.ValueMember = "id";
            comboBox5.DataSource = userDataSet3.Tables[0];
            comboBox5.SelectedIndex = -1;

        }

        private async void UsersForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            comboBox2.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            rb1.Checked = true;
            cbCashCentre.SelectedIndex = -1;
            cbRole.SelectedIndex = -1;
            tbLogin.Text = "";
            tbPosition.Text = "";
            tbPersonNumber.Text = "";
            btnAdd.Enabled = false;

            //MessageBox.Show("12332132");

            //var add = this.Controls.Find("btnAdd", true);
            //add[0].Enabled = false;            

        }

        protected override void tbName_TextChanged(object sender, EventArgs e)
        {

            base.tbName_TextChanged(sender, e);
        }

        private void cbCashCentre_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int rowIndex = dgList.CurrentCell.RowIndex;
            if (cbCashCentre.SelectedIndex != -1 && cbCashCentre.SelectedValue != dgList.CurrentRow.Cells["id_brach"].Value)//dataSet.Tables[0].Rows[rowIndex]["id_brach"])
            {

                /////22.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////22.01.2020

            }
            else
            {
                /////22.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////22.01.2020

            }
        }

        private void tbRole_TextChanged(object sender, EventArgs e)
        {

            //int rowIndex = dgList.CurrentCell.RowIndex;
            //if (tbRole.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["id_role"].ToString() != tbRole.Text)
            //{
            //    /////22.01.2020
            //    /*
            //    btnModify.Enabled = true;
            //    btnAdd.Enabled = true;
            //    */
            //    /////22.01.2020

            //}
            //else
            //{
            //    /////22.01.2020
            //    /*
            //    btnModify.Enabled = false;
            //    btnAdd.Enabled = false;
            //    */
            //    /////22.01.2020

            //}

        }

        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbLogin.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["code"].ToString() != tbLogin.Text)
            {

                /////22.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////22.01.2020


            }
            else
            {
                /////22.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////22.01.2020

            }

        }

        private void tbPosition_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbPosition.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["reference"].ToString() != tbPosition.Text)
            {
                /////22.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////22.01.2020

            }
            else
            {
                /////22.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////22.01.2020

            }

        }

        private void dtPasswordActive_ValueChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (dtPasswordActive.Value >= DateTime.Now && Convert.ToDateTime(dataSet.Tables[0].Rows[rowIndex]["expire_date"]) != dtPasswordActive.Value)
            {
                /////22.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////22.01.2020

            }
            else
            {
                /////22.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////22.01.2020

            }
        }

        //////22.01.2020

        protected override void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vub_cl();
        }

        private void vub_cl()
        {

            if (dgList.CurrentCell != null)
            {
                if (dgList.CurrentCell.RowIndex > -1)
                {
                    //int rowIndex = dgList.CurrentCell.RowIndex;
                    //  base.dgList_SelectionChanged(sender, e);

                    /////22.01.2020
                    tbName.Text = dgList.CurrentRow.Cells["user_name"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["user_name"].ToString().Trim();
                    /////22.01.2020

                    // Выбор кассового центра
                    cbCashCentre.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_brach"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_brach"]);
                    // Выбор роли
                    //tbRole.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"].ToString().Trim();
                    cbRole.SelectedValue = dgList.CurrentRow.Cells["id_role"].Value;//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"];
                    // Выбор логина
                    tbLogin.Text = dgList.CurrentRow.Cells["code"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["code"].ToString().Trim();
                    // Выбор должности
                    tbPosition.Text = dgList.CurrentRow.Cells["reference"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["reference"].ToString().Trim();
                    //Табельный номер
                    tbPersonNumber.Text = dgList.CurrentRow.Cells["person_number"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["reference"].ToString().Trim();
                    // Время действия пароля
                    dtPasswordActive.Value = Convert.ToDateTime(dgList.CurrentRow.Cells["expire_date"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["expire_date"]);
                    //Активация кнопки смены пароля
                    if(pm.EnabledPossibility(perm, btnChangePassword))
                        btnChangePassword.Enabled = true;
                    //int tip_pol = Convert.ToInt16(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["tip_pol"]);
                    ////MessageBox.Show(tip_pol.ToString());
                    //if (tip_pol == 0)
                    //    rb1.Checked = true;
                    //if (tip_pol == 1)
                    //    rb2.Checked = true;
                    //if (tip_pol == 2)
                    //    rb3.Checked = true;
                    /////18.02.2020

                    if (dgList.CurrentRow.Cells["rasrprper1"].Value.ToString() == "1")// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["rasrprper1"].ToString() == "1")
                        comboBox1.SelectedIndex = 0;
                    else
                    {
                        comboBox1.SelectedIndex = -1;
                        comboBox1.Text = "";

                    }
                    /////18.02.2020

                    /////12.11.2019
                    vubrtippols();
                    /////12.11.2019


                    btnAdd.Enabled = false;
                    if(pm.EnabledPossibility(perm, btnModify))
                        btnModify.Enabled = true;
                }
            }

        }

        //////22.01.2020

        /// 22.10.2019
        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {
            vub_cl();
            /////22.01.2020
            /*
            if (dgList.CurrentCell != null)
            {
                if (dgList.CurrentCell.RowIndex > -1)
                {
                    int rowIndex = dgList.CurrentCell.RowIndex;
                    base.dgList_SelectionChanged(sender, e);

                    // Выбор кассового центра
                    cbCashCentre.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_brach"]);
                    // Выбор роли
                    tbRole.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"].ToString().Trim();
                    // Выбор логина
                    tbLogin.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["code"].ToString().Trim();
                    // Выбор должности
                    tbPosition.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["reference"].ToString().Trim();
                    // Время действия пароля
                    dtPasswordActive.Value = Convert.ToDateTime(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["expire_date"]);
                    //Активация кнопки смены пароля
                    btnChangePassword.Enabled = true;

                    /////12.11.2019
                    vubrtippols();
                    /////12.11.2019

                    


                }
            }
            */
            /////22.01.2020

        }
        /// 22.10.2019


        protected override void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIndex = dgList.CurrentCell.RowIndex;
            base.dgList_CellDoubleClick(sender, e);
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                /////22.01.2020
                tbName.Text = dgList.CurrentRow.Cells["user_name"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["user_name"].ToString().Trim();
                                                                                           /////22.01.2020

                // Выбор кассового центра
                cbCashCentre.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_brach"].Value);// base.dataSet.Tables[0].Rows[e.RowIndex]["id_brach"]);
                // Выбор роли
                //tbRole.Text = dataSet.Tables[0].Rows[e.RowIndex]["id_role"].ToString().Trim();
                cbRole.SelectedValue = dgList.CurrentRow.Cells["id_role"].Value;//dataSet.Tables[0].Rows[e.RowIndex]["id_role"];
                // Выбор логина
                tbLogin.Text = dgList.CurrentRow.Cells["code"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[e.RowIndex]["code"].ToString().Trim();
                // Выбор должности
                tbPosition.Text = dgList.CurrentRow.Cells["reference"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[e.RowIndex]["reference"].ToString().Trim();
                //Табельный номер
                tbPersonNumber.Text = dgList.CurrentRow.Cells["person_number"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["reference"].ToString().Trim();
                // Время действия пароля
                dtPasswordActive.Value = Convert.ToDateTime(dgList.CurrentRow.Cells["expire_date"].Value);// base.dataSet.Tables[0].Rows[e.RowIndex]["expire_date"]);
                //Активация кнопки смены пароля
                if(pm.EnabledPossibility(perm, btnChangePassword))
                    btnChangePassword.Enabled = true;

                /////18.02.2020

                if (dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["rasrprper1"].ToString() == "1")
                    comboBox1.SelectedIndex = 0;
                else
                {
                    comboBox1.SelectedIndex = -1;
                    comboBox1.Text = "";

                }

                /////18.02.2020

                /////12.11.2019
                vubrtippols();
                /////12.11.2019


            }

        }

        /////12.11.2019



        private void vubrtippols()
        {


            if (dgList.CurrentCell != null)
            {
                if (dgList.CurrentCell.RowIndex > -1)
                {

                    int tip_pol;
                    if (dgList.CurrentRow.Cells["tip_pol"].Value != DBNull.Value) //base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["tip_pol"] != DBNull.Value)
                        tip_pol = Convert.ToInt16(dgList.CurrentRow.Cells["tip_pol"].Value);  //base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["tip_pol"]);
                    else
                        tip_pol = 0;
                    //MessageBox.Show(tip_pol.ToString());
                    if (tip_pol == 0)
                        rb1.Checked = true;
                    if (tip_pol == 1)
                        rb2.Checked = true;
                    if (tip_pol == 2)
                        rb3.Checked = true;

                    //for (int i = 0; i < groupBox1.Controls.Count; i++)
                    //{
                    //    ((RadioButton)groupBox1.Controls[i]).Checked = false;

                    //}


                    //if (dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["tip_pol"].ToString().Trim() != "")
                    //{

                    //    string selectString = "value_compon='" + dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["tip_pol"].ToString().Trim() + "'";
                    //    DataRow[] searchedRows = ((DataTable)tippolz.Tables[0]).Select(selectString);
                    //    if (searchedRows.Count() > 0)
                    //    {
                    //        string s1 = searchedRows[0]["name_compon"].ToString();
                    //        for (int i = 0; i < groupBox1.Controls.Count; i++)
                    //        {
                    //            string s2 = ((RadioButton)groupBox1.Controls[i]).Text;
                    //            if (s2.ToString() == s1.ToString())
                    //                ((RadioButton)groupBox1.Controls[i]).Checked = true;

                    //        }
                    //    }


                    //}



                }
            }

        }

        /////12.11.2019

        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("Введите сотрудника");
                return;
            }
            if (cbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите роль");
                return;
            }


            //DataSet d1 = dataBase.GetData("t_g_user");

            if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<string>("code").Trim() == tbLogin.Text.Trim()))
            {
                MessageBox.Show("Введите другой 'Логин', такой 'Логин' иммется в базе");
                return;
            }
            if (tbLogin.Text == "")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (tbPersonNumber.Text == "")
            {
                MessageBox.Show("Введите табельный номер");
                return;
            }

            int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            //MessageBox.Show("result = "+result.ToString());
            if (tbName.Text != String.Empty && result == 0)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow[gridFieldName] = tbName.Text;
                if (cbCashCentre.SelectedIndex != -1)
                {
                    dataRow["id_brach"] = cbCashCentre.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Выберите кассовый центр");
                    return;
                }
                dataRow["id_role"] = cbRole.SelectedValue;
                //int role_id;
                //if (Int32.TryParse(tbRole.Text, out role_id))
                //{
                //    dataRow["id_role"] = role_id;
                //}
                //else
                //{
                //    MessageBox.Show("Используйте для указания роли только целочисленные значения!");
                //    return;
                //}


                dataRow["code"] = tbLogin.Text.Trim();
                dataRow["reference"] = tbPosition.Text;
                dataRow["person_number"] = tbPersonNumber.Text;
                dataRow["expire_date"] = dtPasswordActive.Value;
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;
                dataRow["password"] = String.Empty;

                /////18.02.2020
                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text != ""))
                    dataRow["rasrprper1"] = "1";
                else
                    dataRow["rasrprper1"] = "0";
                /////18.02.2020


                /////////////////////////
                int tip_pol = 0;
                /////12.11.2019
                if (rb1.Checked)
                    tip_pol = 0;
                else if (rb2.Checked)
                    tip_pol = 1;
                else if (rb3.Checked)
                    tip_pol = 2;

                dataRow["tip_pol"] = tip_pol.ToString();

                //for (int i = 0; i < groupBox1.Controls.Count; i++)
                //{
                //    if (((RadioButton)groupBox1.Controls[i]).Checked == true)
                //    {

                //        string s2 = ((RadioButton)groupBox1.Controls[i]).Text;

                //        string selectString = "name_compon='" + s2.ToString() + "'";
                //        DataRow[] searchedRows = ((DataTable)tippolz.Tables[0]).Select(selectString);
                //        if (searchedRows.Count() > 0)
                //        {
                //            dataRow["tip_pol"] = searchedRows[0]["value_compon"].ToString();
                //        }

                //    }

                //}

                /////12.11.2019

                ///
                // int i8 = 0;
                // for (int i = 0; i < groupBox1.Controls.Count; i++)
                // {
                //     if (((RadioButton)groupBox1.Controls[i]).Checked == true)
                //     {
                //         i8 = 1;
                //         break;
                //     }
                // }

                // if (i8 == 0)
                // {


                //     DialogResult result1 = MessageBox.Show(
                //"Тип пользователя не выбран. Продолжить операцию?"
                //,
                //"Сообщение",
                //MessageBoxButtons.YesNo,
                //MessageBoxIcon.Information,
                //MessageBoxDefaultButton.Button1,
                //MessageBoxOptions.DefaultDesktopOnly);

                //     if (result1 == DialogResult.No)
                //         return;
                // }

                ///


                /////////////////////////

                dataSet.Tables[0].Rows.Add(dataRow);
                dataBase.UpdateData(dataSet);

                PasswordChange(Convert.ToInt64(dataRow["id"]));
                dgList.CurrentCell = dgList["user_name", dataSet.Tables[0].Rows.Count - 1];



                base.dgList_SelectionChanged(sender, e);


                dgList.Refresh();
                dgList.Update();

                /////12.11.2019
                vubrtippols();
                /////12.11.2019


                // Выбор кассового центра
                cbCashCentre.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_brach"].Value); //base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_brach"]);
                                                                                                         // Выбор роли
                                                                                                         // tbRole.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"].ToString().Trim();
                cbRole.SelectedValue = dgList.CurrentRow.Cells["id_role"].Value;// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"];
                // Выбор логина
                tbLogin.Text = dgList.CurrentRow.Cells["code"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["code"].ToString().Trim();
                // Выбор должности
                tbPosition.Text = dgList.CurrentRow.Cells["reference"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["reference"].ToString().Trim();
                // Время действия пароля
                dtPasswordActive.Value = Convert.ToDateTime(dgList.CurrentRow.Cells["expire_date"].Value);//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["expire_date"]);

                rb1.Checked = true;

                /// 22.10.2019

            }
            else
                MessageBox.Show("Введите другое имя пользователя, такой пользователь существует");
        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.CurrentCell.Value = tbName.Text;
            //dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];

            /// 22.10.2019
            /*
            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;
            dataSet.Tables[0].Rows[rowIndex]["id_brach"] = cbCashCentre.SelectedValue;
            dataSet.Tables[0].Rows[rowIndex]["id_role"] = Convert.ToInt32(tbRole.Text);
            dataSet.Tables[0].Rows[rowIndex]["expire_date"] = dtPasswordActive.Value;
            dataSet.Tables[0].Rows[rowIndex]["code"] = tbLogin.Text;
            dataSet.Tables[0].Rows[rowIndex]["reference"] = tbPosition.Text;
                
            dataBase.UpdateData(dataSet);
            */

            if (cbCashCentre.SelectedIndex != -1)
            {
                rowIndex = rowIndex;
            }
            else
            {
                MessageBox.Show("Выберите кассовый центр");
                return;
            }

            //int role_id;
            //if (Int32.TryParse(tbRole.Text, out role_id))
            //{
            rowIndex = rowIndex;
            //}
            //else
            //{
            //    MessageBox.Show("Используйте для указания роли только целочисленные значения!");
            //    return;
            //}



            /////12.11.2019

            ///
            int i8 = 0;
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (((RadioButton)groupBox1.Controls[i]).Checked == true)
                {
                    i8 = 1;
                    break;
                }
            }

            if (i8 == 0)
            {
                DialogResult result1 = MessageBox.Show("Тип пользователя не выбран. Продолжить операцию?", "Сообщение",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Information,
                                                       MessageBoxDefaultButton.Button1);

                if (result1 == DialogResult.No)
                    return;
            }

            ///

            ////22.01.2020
            DialogResult result = MessageBox.Show("Продолжить операцию изменения?", "Сообщение",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Information,
                                                    MessageBoxDefaultButton.Button1);

            if (result == DialogResult.No)
                return;
            ////22.01.2020

            string s22 = "-1";
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (((RadioButton)groupBox1.Controls[i]).Checked == true)
                {

                    string s2 = ((RadioButton)groupBox1.Controls[i]).Text;

                    string selectString = "name_compon='" + s2.ToString() + "'";
                    DataRow[] searchedRows = ((DataTable)tippolz.Tables[0]).Select(selectString);
                    if (searchedRows.Count() > 0)
                    {
                        s22 = searchedRows[0]["value_compon"].ToString();
                    }

                }

            }
            /////12.11.2019

            /////18.02.2020
            string f4 = "0";
            if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text != ""))
                f4 = "1";
            /////18.02.2020


            dataBase.Zapros("Update t_g_user set user_name='" + tbName.Text +
                "' ,last_update_user='" + CurrentUser.CurrentUserId +
                "', lastupdate='" + DateTime.Now.ToString() +
                "', id_brach='" + cbCashCentre.SelectedValue.ToString() +
                "', id_role='" + cbRole.SelectedValue.ToString() +
                "', expire_date='" + dtPasswordActive.Value.ToString() +
                "', code='" + tbLogin.Text +
                "', reference='" + tbPosition.Text +
                "', person_number='" + tbPersonNumber.Text +

                /////12.11.2019
                "',tip_pol='" + s22.ToString() +
                /////12.11.2019

                /////18.02.2020
                "',rasrprper1='" + f4.ToString() +
                /////18.02.2020

                "' WHERE id = '{0}'", dgList.CurrentRow.Cells["id"].Value.ToString());// dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());

            //dgList.ClearSelection();

            /////22.01.2020
            dataSet = dataBase.GetData9("select * from t_g_user where deleted is null order by user_name");
            //dataSet = dataBase.GetData("t_g_user");
            /////22.01.2020

            dgList.DataSource = dataSet.Tables[0];
            dgList.ClearSelection();

            dgList.CurrentCell = dgList["user_name", rowIndex];



            base.dgList_SelectionChanged(sender, e);

            dgList.Refresh();
            dgList.Update();

            /////12.11.2019
            vubrtippols();
            /////12.11.2019

            // Выбор кассового центра
            cbCashCentre.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_brach"].Value);  //base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_brach"]);
            // Выбор роли
            //tbRole.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"].ToString().Trim();
            cbRole.SelectedValue = dgList.CurrentRow.Cells["id_role"].Value;     //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_role"];
            // Выбор логина
            tbLogin.Text = dgList.CurrentRow.Cells["code"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["code"].ToString().Trim();
            // Выбор должности
            tbPosition.Text = dgList.CurrentRow.Cells["reference"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["reference"].ToString().Trim();
            // Время действия пароля
            dtPasswordActive.Value = Convert.ToDateTime(dgList.CurrentRow.Cells["expire_date"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["expire_date"]);

            /// 22.10.2019

            //////22.01.2020
            btnAdd.Enabled = false;
            //////22.01.2020

        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (dgList.SelectedRows.Count > 0)
            {
                //int rowIndex = dgList.CurrentCell.RowIndex;
                Int64 user_id = Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value);// dataSet.Tables[0].Rows[rowIndex]["id"]);
                if (user_id == DataExchange.CurrentUser.CurrentUserId | DataExchange.CurrentUser.CurrentUserRole == 0 | DataExchange.CurrentUser.CurrentUserRole == 3)
                    PasswordChange(user_id);
                else
                    MessageBox.Show("Вы не владете правами смены пароля");

            }

        }

        private void PasswordChange(Int64 user_id)
        {
            ChangePassword changePassword = new ChangePassword(user_id);
            DialogResult dialogResult = changePassword.ShowDialog();
        }

        protected override void btnDelete_Click(object sender, EventArgs e)
        {

            // MessageBox.Show("1");


            ////
            if ((dgList.CurrentCell != null) & (dgList.CurrentCell.RowIndex > -1))
            {

                ////22.01.2020
                DialogResult result = MessageBox.Show("Продолжить операцию удаления?", "Сообщение",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Information,
                                                        MessageBoxDefaultButton.Button1//,
                                                                                       //MessageBoxOptions.DefaultDesktopOnly
                                                        );

                if (result == DialogResult.No)
                    return;

                ////22.01.2020


                //  MSDataBase dataBase = new MSDataBase();
                //   dataBase.Connect();

                //////22.01.2020
                //dataBase.Zapros("DELETE FROM t_g_user WHERE id = '{0}'", dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());
                dataBase.Zapros("update  t_g_user set deleted=1 WHERE id = '{0}'", dgList.CurrentRow.Cells["id"].Value.ToString());// dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());
                //////22.01.2020

                dgList.ClearSelection();

                /////22.01.2020
                dataSet = dataBase.GetData9("select * from t_g_user where deleted is null order by user_name");
                //  dataSet = dataBase.GetData("t_g_user");
                /////22.01.2020

                dgList.DataSource = dataSet.Tables[0];
                //dgList.ClearSelection();
                // dgList.CurrentCell=null;

                //UsersForm("Пользователи", "user_name", "Сотрудник", "t_g_user");


                /*
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    try
                    {
                        con.Open();
                        string query = String.Format("DELETE FROM t_g_user WHERE id = '{0}'", dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());
                        SqlCommand com = new SqlCommand(query, con);
                        if (com.ExecuteNonQuery() != 0)
                            result = true;
                    }
                    catch { }
                    return result;
                }
                */

            }

            // MessageBox.Show(dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());


            /*
                        int rowIndex = dgList.CurrentCell.RowIndex;

                        dataSet.Tables[0].Rows[rowIndex].Delete();
                        dataBase.UpdateData(dataSet);
                        dataSet = dataBase.GetData(tableName);
                        dgList.DataSource = dataSet.Tables[0];



                        if (dgList.CurrentCell != null)
                        {
                            pok1();

                        }

                        dgList.Select();
                        */

            ////
        }

        /////12.11.2019
        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            /////12.11.2019

            //for (int i = 0; i < groupBox1.Controls.Count; i++)
            // {
            // if (((RadioButton)groupBox1.Controls[i]).Checked == true)
            // {

            if (((RadioButton)sender).Checked == true)
            {

                string s2 = ((RadioButton)sender).Text;
                string s3 = "";
                string s4 = tbLogin.Text.ToString();
                string s5 = "";

                /////

                foreach (DataRow tip1 in tippolz.Tables[0].Rows)
                {
                    s3 = tip1["dopol_pref"].ToString();

                    if (s3.ToString().Trim() != "")
                    {
                        if (s4.Length >= s3.Length)
                        {
                            s5 = s4.Substring(0, s3.Length);

                            if (s5.ToString() == s3.ToString())
                                s4 = s4.Substring(s3.Length);
                        }
                    }
                    // rb.Text = tip1["name_compon"].ToString();
                }

                //////



                string selectString = "name_compon='" + s2.ToString() + "'";
                DataRow[] searchedRows = ((DataTable)tippolz.Tables[0]).Select(selectString);
                if (searchedRows.Count() > 0)
                {
                    s3 = searchedRows[0]["dopol_pref"].ToString();

                    //if (s3.ToString().Trim() != "")
                    // {

                    // if (s4.IndexOf(s3.ToString()) == -1)
                    // if (s4.Substring(0,s3.Length).ToString().ToUpper()!= s3.ToString().ToUpper())
                    // {
                    //   tbLogin.Text = s3.ToString() + tbLogin.Text;
                    tbLogin.Text = s3.ToString() + s4.ToString();
                    // }
                    // }

                }
                //if ()

                // }

            }

            /////12.11.2019
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            tbName.Text = "";

            /////28.01.2020
            if(pm.EnabledPossibility(perm, btnAdd))
                btnAdd.Enabled = true;
            /////28.01.2020


        }
        /////12.11.2019

        ////22.01.2020
        protected override void btnOchist_Click(object sender, EventArgs e)
        {
            tbName.Text = "";

            // Выбор кассового центра
            cbCashCentre.SelectedValue = -1;
            // Выбор роли
            //tbRole.Text = "";
            cbRole.SelectedIndex = -1;
            // Выбор логина
            tbLogin.Text = "";
            // Выбор должности
            tbPosition.Text = "";
            // Выбор Табельный номер
            tbPersonNumber.Text = "";
            // Время действия пароля
            dtPasswordActive.Value = Convert.ToDateTime(dgList.CurrentRow.Cells["expire_date"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["expire_date"]);
            //Активация кнопки смены пароля
            btnChangePassword.Enabled = false;

            comboBox2.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;

            button10.BackColor = SystemColors.Control;
            button10.Enabled = false;

            if(pm.EnabledPossibility(perm, btnAdd))
                btnAdd.Enabled = true;
            btnModify.Enabled = false;


        }

        private void tbName_TextChanged_1(object sender, EventArgs e)
        {
            btnModify.Enabled = false;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void btnModify_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if ((comboBox2.Text.Trim() != "") | (comboBox4.Text.Trim() != "") | (comboBox5.Text.Trim() != ""))
            {
                string s1 = "";
                if (comboBox2.Text.Trim() != "")
                    s1 = " and id=" + comboBox2.SelectedValue.ToString();
                else if (comboBox4.Text.Trim() != "")
                    s1 = " and id=" + comboBox4.SelectedValue.ToString();
                else if (comboBox5.Text.Trim() != "")
                    s1 = " and id=" + comboBox5.SelectedValue.ToString();

                dataSet = dataBase.GetData9("select * from t_g_user where deleted is null " + s1.ToString());
                dgList.DataSource = dataSet.Tables[0];

                //if (dgList.Rows.Count > 0)
                //    dgList.CurrentCell = dgList[0, 0];

                button10.BackColor = System.Drawing.Color.Yellow;
                if(pm.EnabledPossibility(perm, button10))
                    button10.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataSet = dataBase.GetData9("select * from t_g_user where deleted is null  order by user_name");
            dgList.DataSource = dataSet.Tables[0];

            btnOchist_Click(sender, e);
            //comboBox2.SelectedIndex = -1;
            //comboBox4.SelectedIndex = -1;
            //comboBox5.SelectedIndex = -1;

            //button10.BackColor = SystemColors.Control;
            //button10.Enabled = false;

            //tbName.Text = "";
            //rb1.Checked = true;
            //cbCashCentre.SelectedIndex = -1;
            //cbRole.SelectedIndex = -1;
            //tbLogin.Text = "";
            //tbPosition.Text = "";
            //tbPersonNumber.Text = "";
            ////btnAdd.Enabled = false;
            //btnModify.Enabled = false;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1)
                comboBox2.SelectedValue = comboBox5.SelectedValue;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
                comboBox5.SelectedValue = comboBox2.SelectedValue;


        }

      
        ////22.01.2020

    }
}
