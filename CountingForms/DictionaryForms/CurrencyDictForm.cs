using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using DataExchange;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CountingForms.Services;
using CountingDB;
using CountingDB.Entities;

namespace CountingForms.DictionaryForms
{
    public partial class CurrencyDictForm : DictionaryForm
    {

        public DataTable blokTable;

        public CurrencyDictForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public CurrencyDictForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            /////21.10.2019
            ///

            dgList.Columns.Add("curr_code", "Код");
            dgList.Columns["curr_code"].DataPropertyName = "curr_code";
            dgList.Columns["curr_code"].SortMode = DataGridViewColumnSortMode.NotSortable ;

            dgList.Columns.Add("rate", "Курс");
            dgList.Columns["rate"].DataPropertyName = "rate";
            dgList.Columns["rate"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("sort", "П.Н.");
            dgList.Columns["sort"].DataPropertyName = "sort";
            dgList.Columns["sort"].SortMode = DataGridViewColumnSortMode.NotSortable;

            blokTable = new DataTable("blok");
            DataColumn p1 = new DataColumn("id", Type.GetType("System.Boolean"));
            DataColumn p2 = new DataColumn("nameblok", Type.GetType("System.String"));
            blokTable.Columns.Add(p1);
            blokTable.Columns.Add(p2);
            blokTable.Rows.Add(new object[] {"False", "Используется" });
            blokTable.Rows.Add(new object[] { "True", "Заблокирована" });

            
            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name = "locked";
            dg1.HeaderText = "Состояние" +
                "";
            //dg1.FlatStyle = FlatStyle.Flat;
            dg1.DataPropertyName = "locked";
            dg1.DisplayMember = "nameblok";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = blokTable;
            dgList.Columns.Add(dg1);
            
            /////21.10.2019

        }

        private async void CurrencyDictForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        protected override void tbName_TextChanged(object sender, EventArgs e)
        {
            base.tbName_TextChanged(sender, e);
        }

        /// 21.10.2019
        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgList.CurrentCell != null)
            {

                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);

                tbCode.Text = dgList.CurrentRow.Cells["curr_code"].Value.ToString().Trim();//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["curr_code"].ToString().Trim();                                                                                           
                tbCurRate.Text = dgList.CurrentRow.Cells["rate"].Value.ToString().Trim();// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["rate"].ToString().Trim();
                tbSort.Text = dgList.CurrentRow.Cells["sort"].Value.ToString().Trim();//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["sort"].ToString().Trim();
                cbBlockCur.Checked = Convert.ToBoolean(dgList.CurrentRow.Cells["locked"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["locked"]);

            }

        }
        /// 21.10.2019


        protected override void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /// 21.10.2019
            if (e.RowIndex >-1)
            {
                /// 21.10.2019

                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_CellDoubleClick(sender, e);
                tbCode.Text = base.dataSet.Tables[0].Rows[e.RowIndex]["curr_code"].ToString().Trim();
                tbCurRate.Text = base.dataSet.Tables[0].Rows[e.RowIndex]["rate"].ToString().Trim();
                tbSort.Text = base.dataSet.Tables[0].Rows[e.RowIndex]["sort"].ToString().Trim();
                cbBlockCur.Checked = Convert.ToBoolean(base.dataSet.Tables[0].Rows[e.RowIndex]["locked"]);


            /// 21.10.2019
            }
            /// 21.10.2019
            }

        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            /// 21.10.2019
            string s1 = tbCurRate.Text;
            float num;
            int i1;
            if (tbCode.Text.Trim().Length > 4)
                MessageBox.Show("Код валюты не должен быть больше 4 символов!");

           else
            if ((tbCurRate.Text.Trim() != "") &(Single.TryParse(s1, out num) != true))
                MessageBox.Show("Курс должен быть числом!");
            else
            {
                if (Int32.TryParse(tbSort.Text, out i1) != true)
                    MessageBox.Show("Порядковый номер должен быть числом!");
                else
                {

                    /// 21.10.2019

                    int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
                    if (tbName.Text != String.Empty && result == 0)
                    {
                        DataRow dataRow = dataSet.Tables[0].NewRow();
                        dataRow["creation"] = DateTime.Now;
                        dataRow[gridFieldName] = tbName.Text;
                        dataRow["curr_code"] = tbCode.Text.Trim();

                        /// 21.10.2019
                        if (tbCurRate.Text.Trim() == "")
                            dataRow["rate"] = DBNull.Value;
                        else
                            /// 21.10.2019
                            
                            dataRow["rate"] = tbCurRate.Text;
                       
                        // 07.12.2020
                        if (tbSort.Text.Trim() == "")
                            dataRow["sort"] = DBNull.Value;
                        else
                            dataRow["sort"] = tbSort.Text.Trim();


                        dataRow["locked"] = cbBlockCur.Checked;
                        //dataRow["sellrate"] = DBNull;
                        dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                        dataRow["lastupdate"] = DateTime.Now;

                        //dataSet.Tables.Add((DataTable) dgList.DataSource);
                        dataSet.Tables[0].Rows.Add(dataRow);
                        dataBase.UpdateData(dataSet);

                        /// 21.10.2019
                        dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];
                        base.dgList_SelectionChanged(sender, e);
                        dgList.Refresh();
                        dgList.Update();

                        tbCode.Text = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["curr_code"].ToString().Trim();
                        tbCurRate.Text = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["rate"].ToString().Trim();
                        cbBlockCur.Checked = Convert.ToBoolean(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["locked"]);


                        btnModify.Enabled = false;
                        btnAdd.Enabled = false;

                        /// 21.10.2019

                    }
                }
            }
        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {
            string s1 = tbCurRate.Text;
            float num;
            int i1;
            if (tbCode.Text.Trim().Length > 4)
                MessageBox.Show("Код валюты не должен быть больше 4 символов!");
            else
            if ((tbCurRate.Text.Trim() != "") & (Single.TryParse(s1, out num) != true))
                MessageBox.Show("Курс должен быть числом!");
            
            else
            {

                if (Int32.TryParse(tbSort.Text, out i1) != true)
                    MessageBox.Show("Порядковый номер должен быть числом!");
                else
                {

                    int rowIndex = dgList.CurrentCell.RowIndex;
                    //dgList.CurrentCell.Value = tbName.Text;
                    dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
                    dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
                    dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
                    dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;
                    dataSet.Tables[0].Rows[rowIndex]["curr_code"] = tbCode.Text.Trim();

                    /// 21.10.2019
                    if (tbCurRate.Text.Trim() == "")
                        dataSet.Tables[0].Rows[rowIndex]["rate"] = DBNull.Value;
                    else
                        /// 21.10.2019

                        dataSet.Tables[0].Rows[rowIndex]["rate"] = tbCurRate.Text;

                    // 07.12.2020
                    if (tbSort.Text.Trim() == "")
                        dataSet.Tables[0].Rows[rowIndex]["sort"] = DBNull.Value;
                    else
                        dataSet.Tables[0].Rows[rowIndex]["sort"] = tbSort.Text.Trim();

                    dataSet.Tables[0].Rows[rowIndex]["locked"] = cbBlockCur.Checked;
                    //dataRow["sellrate"] = DBNull;

                    dataBase.UpdateData(dataSet);

                    btnModify.Enabled = false;
                    btnAdd.Enabled = false;


                }
            }

        }

        private void tbCode_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbCode.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["curr_code"].ToString() != tbCode.Text)
            {
                if(pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;
                if (pm.EnabledPossibility(perm, btnAdd))
                    btnAdd.Enabled = true;

            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;

            }
        }

        private void tbCurRate_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbCurRate.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["rate"].ToString() != tbCurRate.Text)
            {
                if(pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;
                if (pm.EnabledPossibility(perm, btnAdd))
                    btnAdd.Enabled = true;

            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;

            }
           
        }

        private void cbBlockCur_CheckedChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (Convert.ToBoolean(dataSet.Tables[0].Rows[rowIndex]["locked"]) != cbBlockCur.Checked)
            {
                if(pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;
                if (pm.EnabledPossibility(perm, btnAdd))
                    btnAdd.Enabled = true;

            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;

            }
        }

       

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void tbSort_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbSort.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["sort"].ToString() != tbSort.Text)
            {
                if(pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;
                if (pm.EnabledPossibility(perm, btnAdd))
                    btnAdd.Enabled = true;

            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;

            }
        }
    }
    
}
