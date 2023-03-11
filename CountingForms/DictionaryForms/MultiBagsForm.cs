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

namespace CountingForms.DictionaryForms
{
    public partial class MultiBagsForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private DataSet listDataSet, accountDataSet, dsEncashpoint, clientDataSet, marshrutDataSet, currencyDataSet = null;

        private DataSet dataSet = null;

        private MSDataBase dataBase = new MSDataBase();
        private bool a1 = true;

        public MultiBagsForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase.Connect();

            clientDataSet = dataBase.GetData9("Select * from [t_g_client] where deleted = 0");
            marshrutDataSet = dataBase.GetData9("select * from t_g_marschrut where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
            currencyDataSet = dataBase.GetData("t_g_currency");
            //listDataSet = dataBase.GetData9("Select * from [t_g_multi_bags] where [deleted]=0");
        }

        private async void MultiBagsForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
            // tbPlomb.MaxLength = 10;

            cbBin.DisplayMember = "BIN";
            cbBin.ValueMember = "id";
            cbBin.DataSource = clientDataSet.Tables[0];
            cbBin.SelectedIndex = -1;

            cbClient.DisplayMember = "name";
            cbClient.ValueMember = "id";
            cbClient.DataSource = clientDataSet.Tables[0];
            cbClient.SelectedIndex = -1;

            //cbEncashpoint.DisplayMember = "name";
            //cbEncashpoint.ValueMember = "id";
            ////cbEncashpoint.DataSource = clientDataSet.Tables[0];
            //cbEncashpoint.SelectedIndex = -1;

            cbMarshrut.DisplayMember = "nummarsh";
            cbMarshrut.ValueMember = "id";
            cbMarshrut.DataSource = marshrutDataSet.Tables[0];
            cbMarshrut.SelectedIndex = -1;

            #region List

            dgList.AutoGenerateColumns = false;
            dgList.ColumnHeadersHeight = 10;
            dgList.RowHeadersWidth = 10;

            dgList.DataSource = null;
            dgList.Rows.Clear();
            dgList.Columns.Clear();

            dgList.Columns.Add("name", "№ Мультисумки");
            dgList.Columns["name"].Visible = true;
            dgList.Columns["name"].DataPropertyName = "name";
            dgList.Columns["name"].Width = 120;

            dgList.Columns.Add("id", "");
            dgList.Columns["id"].Visible = false;
            dgList.Columns["id"].DataPropertyName = "id";

            //if (listDataSet != null)
            //dgList.DataSource = listDataSet.Tables[0];


            #endregion

            //12.08.2020

            dgAccountDeclared.AutoGenerateColumns = false;
            dgAccountDeclared.ColumnHeadersHeight = 10;
            dgAccountDeclared.RowHeadersWidth = 10;

            dgAccountDeclared.DataSource = null;
            dgAccountDeclared.Rows.Clear();
            dgAccountDeclared.Columns.Clear();

            DataGridViewCheckBoxColumn dgAccountBoolColumn = new DataGridViewCheckBoxColumn();
            dgAccountBoolColumn.Name = "state";
            dgAccountBoolColumn.HeaderText = "Исп.";
            dgAccountDeclared.Columns.Add(dgAccountBoolColumn);
            dgAccountDeclared.Columns["state"].Visible = true;
            dgAccountDeclared.Columns["state"].Width = 50;

            dgAccountDeclared.Columns.Add("name", "№ счета");
            dgAccountDeclared.Columns["name"].Visible = true;
            dgAccountDeclared.Columns["name"].DataPropertyName = "name";
            dgAccountDeclared.Columns["name"].Width = 180;

            dgAccountDeclared.Columns.Add("id", "");
            dgAccountDeclared.Columns["id"].Visible = false;
            dgAccountDeclared.Columns["id"].DataPropertyName = "id";
            //dgAccountDeclared.Columns["id"].Width = 180;




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
            dgAccountDeclared.Columns.Add(dgAccountCurrency);
            dgAccountDeclared.Columns["id_currency"].Visible = true;
            dgAccountDeclared.Columns["id_currency"].Width = 65;

            dgAccountDeclared.Columns.Add("value", "Сумма");
            dgAccountDeclared.Columns["value"].Visible = true;
            dgAccountDeclared.Columns["value"].ReadOnly = false;


            dgAccountDeclared.Columns["value"].DefaultCellStyle.Format = "### ### ### ###";


            DataGridViewButtonColumn dgAccountDetails = new DataGridViewButtonColumn();

            dgAccountDetails.Name = "Denomination";

            //dgAccountDetails.Visible = true;
            dgAccountDetails.Visible = false;
            dgAccountDetails.Text = "...";


            dgAccountDeclared.Columns.Add(dgAccountDetails);
            //////24.10.2019
            dgAccountDeclared.Columns["Denomination"].HeaderText = "Номинал";
            dgAccountDeclared.Columns["Denomination"].Width = 80;

            sost();
            clear();
            clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /////
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (declaredRow.Cells["value"].Value != null)

                {

                    int i2 = 0;
                    if (Int32.TryParse(declaredRow.Cells["value"].Value.ToString(), out i2) != true)
                    {

                        MessageBox.Show("Сумма должна быть целым числом!");
                        return;

                    }
                }
            }

                    ///////
                    int DeclaredCount = 0;
            if (cbBin.SelectedIndex == -1)
            {
                MessageBox.Show("Введите БИН");
                return;
            }
            if (cbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Введите клиента");
                return;
            }
            if (tbNumber.Text == string.Empty)
            {
                MessageBox.Show("Введите номер мультисумки");
                return;
            }
            if (tbCount.Text == string.Empty)
            {
                MessageBox.Show("Введите количество сумок");
                return;
            }
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                //нужное место
                if (declaredRow.Cells["value"].Value != null && declaredRow.Cells["value"].Value != DBNull.Value && declaredRow.Cells["value"].Value.ToString() != String.Empty && Convert.ToDecimal(declaredRow.Cells["value"].Value) != 0)
                {
                    DeclaredCount++;
                }
            }


            if (DeclaredCount==0)
            {
                MessageBox.Show("Введите задекларированную сумму");
                return;
            }
            
            string number = tbNumber.Text.Trim();
            
            dataSet = dataBase.GetData9("Select * from t_g_multi_bags where deleted=0 and name = '"+number.ToString()+"'");
            if (dataSet != null)
                if(dataSet.Tables[0].Rows.Count>0)
                {
                    MessageBox.Show("Введите другой номер сумки, вами введенный номер уже существует в списке мульти сумок");
                    return;
                }
            Int32 count = 0;
            try
            {

                count = Convert.ToInt32(Convert.ToDouble(tbCount.Text.ToString()));
                
            }
            catch (OverflowException)
            {
                MessageBox.Show("Количество должна быть целым числом!");
                return;
            }

            count = Convert.ToInt32(tbCount.Text);
           // MessageBox.Show(count.ToString()+"_"+(count+1).ToString());
            //dataSet = dataBase.GetData9("INSERT INTO t_g_multi_bags ([creation],[creation_user],[lastupdate],[last_update_user],[id_client],[count_bags1],[name],[deleted]) " +
            //    "VALUES (getdate(), " + DataExchange.CurrentUser.CurrentUserId.ToString().Trim() + ", getdate(), " + DataExchange.CurrentUser.CurrentUserId.ToString().Trim() + ", "+cbBin.SelectedValue.ToString().Trim() + ", "+count.ToString().Trim() + ", '"+number.ToString().Trim()+"', 0)");

            //
            dataSet = dataBase.GetData("t_g_multi_bags");
            DataRow row = dataSet.Tables[0].NewRow();
            row["creation"] = DateTime.Now;
            row["creation_user"] = DataExchange.CurrentUser.CurrentUserId;
            row["lastupdate"] = DateTime.Now;
            row["last_update_user"] = DataExchange.CurrentUser.CurrentUserId;
            row["id_client"] = cbBin.SelectedValue;
            row["id_encashpoint"] = cbEncashpoint.SelectedValue;
            row["count_bags1"] = count;
            row["name"] = tbNumber.Text;
            row["deleted"] = 0;
            if (cbMarshrut.SelectedIndex != -1)
                row["id_marschrut"] = cbMarshrut.SelectedValue;
            else
                row["id_marschrut"] = 0;
            if (tbPlomb.Text != string.Empty)
                row["seal"] = tbPlomb.Text;
            row["fl_prov"] = 0;
            row["id_zona_create"] = DataExchange.CurrentUser.CurrentUserZona;
            row["id_shift_create"] = DataExchange.CurrentUser.CurrentUserShift;
            row["id_shift_current"] = DataExchange.CurrentUser.CurrentUserShift;

            dataSet.Tables[0].Rows.Add(row);
            dataBase.UpdateData(dataSet, "t_g_multi_bags");

            //
            dataSet = dataBase.GetData9("select top 1 id from t_g_multi_bags order by id desc");
            Int64 id = dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id")).First<Int64>();

            dataSet = dataBase.GetData("t_g_multi_bags_content");

            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (Convert.ToBoolean(declaredRow.Cells["state"].Value) == true)
                {
                    DataRow dataRow2 = dataSet.Tables[0].NewRow();
                    dataRow2["id_multi_bag"] = id.ToString();
                    dataRow2["id_account"] = declaredRow.Cells["id"].Value;
                    dataRow2["id_currency"] = declaredRow.Cells["id_currency"].Value;
                    dataRow2["declared_value1"] = declaredRow.Cells["value"].Value;
                    dataRow2["declared_value2"] = 0;
                    dataRow2["creation"] = DateTime.Now;
                    dataRow2["creation_user"] = DataExchange.CurrentUser.CurrentUserId;
                    dataRow2["lastupdate"] = DateTime.Now;
                    dataRow2["last_update_user"] = DataExchange.CurrentUser.CurrentUserId;
                    dataSet.Tables[0].Rows.Add(dataRow2);
                }
            }
            dataBase.UpdateData(dataSet, "t_g_multi_bags_content");

            sost();
            clear();
            //MessageBox.Show("END!");
        }

        private void dgAccountDeclared_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (dgAccountDeclared[4, e.RowIndex].Value != null)
            {

                if (dgAccountDeclared[4, e.RowIndex].Value.ToString() != "")
                    dgAccountDeclared[0, e.RowIndex].Value = true;

            }
            else
            {
                dgAccountDeclared[0, e.RowIndex].Value = false;
            }

        }


        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
     
            Encashpoint();
        }
        private void Encashpoint()
        {
            if(cbClient.SelectedIndex!=-1)
            {
                dsEncashpoint = dataBase.GetData9("SELECT distinct t2.id id, t2.name name FROM[CountingDB].[dbo].t_g_account t1 left join t_g_encashpoint t2 on t1.id_encashpoint = t2.id where id_client = " + cbClient.SelectedValue.ToString());

                cbEncashpoint.DisplayMember = "name";
                cbEncashpoint.ValueMember = "id";
                cbEncashpoint.DataSource = dsEncashpoint.Tables[0];
                cbEncashpoint.SelectedIndex = 0;

            }
            
        }
        private void account()
        {
           // cbEncashpoint.DataSource = dsEncashpoint.Tables[0];
            //if (dsEncashpoint != null)
            //    if(dsEncashpoint.Tables[0].Rows.Count>0)
            if (cbEncashpoint.SelectedIndex != -1)
            {
                // cbBin.SelectedIndex = cbClient.SelectedIndex;
               // MessageBox.Show(cbEncashpoint.SelectedValue.ToString());
               // MessageBox.Show(cbClient.SelectedValue.ToString());
                    accountDataSet = dataBase.GetData9("select * from t_g_account where id_encashpoint = "+ cbEncashpoint.SelectedValue.ToString());
                   if(accountDataSet!=null)
                    if (accountDataSet.Tables.Count > 0)
                    {
                        dgAccountDeclared.DataSource = accountDataSet.Tables[0];
                    
                     dgAccountDeclared.AutoResizeColumns();
                    

                    }
                
                

            }
        }
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
            clear();
        }

        private void cbBin_SelectedIndexChanged(object sender, EventArgs e)
        {
           // cbClient.SelectedIndex = cbBin.SelectedIndex;
           // account();
            Encashpoint();
        }

        private void dgList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }      

        private void dgList_SelectionChanged(object sender, EventArgs e)
        {
            if(a1)
                pok1();
           // MessageBox.Show()
        }

        private void dgList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            pok1();
        }

        private void dgList_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Down) | (e.KeyCode == Keys.Up))
                pok1();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (declaredRow.Cells["value"].Value != null)

                {

                    int i2 = 0;
                    if (Int32.TryParse(declaredRow.Cells["value"].Value.ToString(), out i2) != true)
                    {

                        MessageBox.Show("Сумма должна быть целым числом!");
                        return;

                    }
                }
            }

            ///////
            int DeclaredCount = 0;
            if (cbBin.SelectedIndex == -1)
            {
                MessageBox.Show("Введите БИН");
                return;
            }
            if (cbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Введите клиента");
                return;
            }
            if (tbNumber.Text == string.Empty)
            {
                MessageBox.Show("Введите номер мультисумки");
                return;
            }
            if (tbCount.Text == string.Empty)
            {
                MessageBox.Show("Введите количество сумок");
                return;
            }
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {
                                
                if (declaredRow.Cells["value"].Value != null && declaredRow.Cells["value"].Value != DBNull.Value && declaredRow.Cells["value"].Value.ToString() != String.Empty && Convert.ToDecimal(declaredRow.Cells["value"].Value) != 0)
                {
                    DeclaredCount++;
                }
            }


            if (DeclaredCount == 0)
            {
                MessageBox.Show("Введите задекларированную сумму");
                return;
            }

            Int32 count = 0;
            try
            {

                count = Convert.ToInt32(Convert.ToDouble(tbCount.Text.ToString()));

            }
            catch (OverflowException)
            {
                MessageBox.Show("Количество должна быть целым числом!");
                return;
            }
            string s1 = "";
            count = Convert.ToInt32(tbCount.Text);
            if (cbMarshrut.SelectedIndex != -1)
                s1 = ", id_marschrut = "+cbMarshrut.SelectedValue.ToString().Trim();

            dataSet = dataBase.GetData9("UPDATE [t_g_multi_bags] SET lastupdate = getdate(), last_update_user = "+DataExchange.CurrentUser.CurrentUserId+
                ", id_client = " + cbBin.SelectedValue.ToString().Trim() +s1+ ", id_encashpoint = "+cbEncashpoint.SelectedValue.ToString().Trim()+", count_bags1 = " + count.ToString().Trim()+", name = "+tbNumber.Text.Trim()+
                " where id = "+dgList[1,dgList.CurrentRow.Index].Value); //dgList[1, dgList.CurrentRow.Index].Value

            
            Int64 id = Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value);
            string str = "";
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {
                str = str +" "+ declaredRow.Cells["id"].Value.ToString()+",";
            }
            
            str = str.Remove(str.Length - 1);
            
            DataSet d2 = dataBase.GetData9("Delete from t_g_multi_bags_content where id_account not in ("+str+") and [id_multi_bag] = " + id.ToString());
            
            dataSet = dataBase.GetData("t_g_multi_bags_content");
            
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {
                
                if (Convert.ToBoolean(declaredRow.Cells["state"].Value) == true)
                {
                    if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_multi_bag") == id && x.Field<Int64>("id_account") == Convert.ToInt64(declaredRow.Cells["id"].Value)))
                    {
                        DataSet d1 = dataBase.GetData9("Update t_g_multi_bags_content set lastupdate = getdate(), last_update_user = " + DataExchange.CurrentUser.CurrentUserId + ", declared_value1 = " + declaredRow.Cells["value"].Value.ToString() + " where " +
                            "id_multi_bag = " + id.ToString() + " and id_account = " + declaredRow.Cells["id"].Value.ToString());
                    }
                    else
                    {
                        DataRow dataRow2 = dataSet.Tables[0].NewRow();
                        dataRow2["id_multi_bag"] = id.ToString();
                        dataRow2["id_account"] = declaredRow.Cells["id"].Value;
                        dataRow2["id_currency"] = declaredRow.Cells["id_currency"].Value;
                        dataRow2["declared_value1"] = declaredRow.Cells["value"].Value;
                        dataRow2["declared_value2"] = 0;
                        dataRow2["creation"] = DateTime.Now;
                        dataRow2["creation_user"] = DataExchange.CurrentUser.CurrentUserId;
                        dataRow2["lastupdate"] = DateTime.Now;
                        dataRow2["last_update_user"] = DataExchange.CurrentUser.CurrentUserId;
                        dataSet.Tables[0].Rows.Add(dataRow2);
                    }
                }
            }
                dataBase.UpdateData(dataSet, "t_g_multi_bags_content");

                


                sost();
            clear();
            clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (listDataSet != null)
                if (listDataSet.Tables[0].Rows.Count > 0)
                {
                    a1 = false;
                    listDataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["deleted"] = 1;

                    dataSet = dataBase.GetData9("Update t_g_counting set id_multi_bag=0 where id_multi_bag="+ listDataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"]);

                    dataBase.UpdateData(listDataSet, "t_g_multi_bags");

                    listDataSet = dataBase.GetData9("Select * from [t_g_multi_bags] where [deleted]=0 and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
                    if (listDataSet != null)
                        dgList.DataSource = listDataSet.Tables[0];
                    a1 = true;
                    //dataSet = dataBase.GetData9("");
                    clear();
                    clear();

                }

            

        }

        private void dgList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pok1();
        }        

        private void cbEncashpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            account();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pok1();
        }

        private void clear()
        {

            cbClient.SelectedIndex = -1;
            cbBin.SelectedIndex = -1;
            cbEncashpoint.SelectedIndex = -1;
            cbMarshrut.SelectedIndex = -1;
            tbCount.Text = string.Empty;
            tbNumber.Text = string.Empty;
            tbPlomb.Text = string.Empty;
            cbMarshrut.SelectedIndex = -1;
            if(pm.EnabledPossibility(perm, cbBin))
                cbBin.Enabled = true;
            if (pm.EnabledPossibility(perm, cbClient))
                cbClient.Enabled = true;

            dgAccountDeclared.DataSource = null;
        }

        private void sost()
        {
            
            
            listDataSet = dataBase.GetData9("Select * from [t_g_multi_bags] where [deleted]=0 and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
            if (listDataSet != null)
                dgList.DataSource = listDataSet.Tables[0];

            
        }
        private long ConvInt(object x)
        {
            long b = 0;
            if (Convert.IsDBNull(x))
                b = 0;
            else b = Convert.ToInt64(x);

            return b;
        }
        private void pok1()
        {
            //listDataSet = null;
            //dgList.DataSource = null;

            //listDataSet = dataBase.GetData9("Select * from [t_g_multi_bags] where [deleted]=0");
            //if (listDataSet != null)
            //    dgList.DataSource = listDataSet.Tables[0];
            //sost();
            if (dgList.CurrentCell != null & listDataSet != null)
                if (dgList[0, dgList.CurrentRow.Index].Value.ToString() != "0")
                    if (dgList[0, dgList.CurrentRow.Index].Value.ToString() != "")
                    {
                        DataSet contentDataSet = dataBase.GetData9(" Select * from t_g_multi_bags_content where id_multi_bag = "+ dgList[1, dgList.CurrentRow.Index].Value);
                       tbNumber.Text = listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<String>("name")).FirstOrDefault<String>().ToString();

                        tbCount.Text = listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<Int32>("count_bags1")).FirstOrDefault<Int32>().ToString();
                        cbClient.SelectedValue=cbBin.SelectedValue = listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<Int64>("id_client")).FirstOrDefault<Int64>();
                        
                        cbEncashpoint.SelectedValue= listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<Int64>("id_encashpoint")).FirstOrDefault<Int64>();
                        account();

                        if (listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<String>("seal")).FirstOrDefault<String>() != null)
                            tbPlomb.Text = listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<String>("seal")).FirstOrDefault<String>();
                        else tbPlomb.Text = string.Empty;

                        if (listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<Int64>("id_marschrut")).FirstOrDefault<Int64>() != 0)
                            cbMarshrut.SelectedValue = listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<Int64>("id_marschrut")).FirstOrDefault<Int64>();
                        else cbMarshrut.SelectedValue = 0;

                        if (listDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value)).Select(x => x.Field<Int32>("count_bags2")).FirstOrDefault<Int32>() > 0)
                        {
                            cbBin.Enabled = false;
                            cbClient.Enabled = false;
                        }
                        else
                        {
                            if(pm.EnabledPossibility(perm, cbClient))
                                cbClient.Enabled = true;
                            if (pm.EnabledPossibility(perm, cbBin))
                                cbBin.Enabled = true;
                        }
                        if (contentDataSet != null)
                            if (dgList != null)
                                if(dgList.Rows.Count>0)
                        if (contentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(dgList[1, dgList.CurrentRow.Index].Value) && x.Field<Decimal>("fact_value") > Convert.ToDecimal(0)))
                        {
                            btnDelete.Enabled = false;
                           // btnDelete.Visible = false;
                        }
                        else
                        {
                            if(pm.EnabledPossibility(perm, btnDelete))
                                btnDelete.Enabled = true;
                            //btnDelete.Visible = true;
                        }

                        if (dgAccountDeclared != null)
                            if (dgAccountDeclared.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
                                {
                                    if (contentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))

                                    {
                                        accountRow.Cells["state"].Value = CheckState.Checked;

                                        accountRow.Cells["value"].Value = Convert.ToDouble(contentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)).Select(x => x.Field<Decimal>("declared_value1")).First<Decimal>());
                                        
                                    }

                                }
                            }
                            
                        
                    }
        }
    }
}
