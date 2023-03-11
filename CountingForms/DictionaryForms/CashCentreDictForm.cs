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
using CountingForms.Services;
using CountingDB.Entities;

namespace CountingForms.DictionaryForms
{
    public partial class CashCentreDictForm : DictionaryForm
    {
        private DataSet cityDataSet = null;

        public CashCentreDictForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public CashCentreDictForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            //Инициализация компонентов управления
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            //Запрос данных по валютам номинала и состоянию
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();
            cityDataSet = dataBase.GetData("t_g_city");         

            //Заполнение городов  
            cbCity.DataSource = cityDataSet.Tables[0];
            cbCity.DisplayMember = "name";
            cbCity.ValueMember = "id";
            cbCity.SelectedIndex = -1;
            cbCity.SelectedIndexChanged += new EventHandler(cbCity_SelectedIndexChanged);

            ///////18.10.2019

            /////21.10.2019
            /*
            dgList.Columns.Add("id_city", "Город");
            dgList.Columns["id_city"].DataPropertyName = "id_city";
            */

            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name= "id_city";
            dg1.HeaderText = "Город";
            //dg1.FlatStyle = FlatStyle.Flat;
            dg1.DataPropertyName = "id_city";
            dg1.DisplayMember = "name";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = cityDataSet.Tables[0]; 
            dgList.Columns.Add(dg1);

            //dgList.Columns["expire_date"].SortMode = DataGridViewColumnSortMode.NotSortable;

            /*
             * DataGridViewComboBoxColumn dgAccountCurrency =  new DataGridViewComboBoxColumn();
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
            */

            /////21.10.2019


            dgList.Columns.Add("time_zone", "Часовой пояс");
            dgList.Columns["time_zone"].DataPropertyName = "time_zone";
            //dgList.ColumnHeadersVisible = true;
            //dgList.Columns[gridFieldName].HeaderText = strCaption;
            // dgList.RowHeadersVisible = true;
            // dgList.Columns[gridFieldName].Visible = true;
            // dgList.RowHeadersWidth = 30;

            ///////18.10.2019

        }

        private async void CashCentreDictForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
            ////11.02.2020
            dataSet = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
            dgList.DataSource = dataSet.Tables[0];
            ////11.02.2020

        }

        protected override void tbName_TextChanged(object sender, EventArgs e)
        {
            base.tbName_TextChanged(sender, e);
        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;

            if (cbCity.SelectedIndex != -1 && dataSet.Tables[0].Rows[rowIndex]["id_city"].ToString() != cbCity.SelectedValue.ToString())
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

        private void nUTC_ValueChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (nUTC.Value >= 0 && Convert.ToDecimal(dataSet.Tables[0].Rows[rowIndex]["time_zone"]) != nUTC.Value)
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

        /// 21.10.2019
        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgList.CurrentCell != null)
            {
                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);

                // Выбор города
                cbCity.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_city"].Value);//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"]);
                // Выбор часового пояса
                nUTC.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["time_zone"].ToString());
            }

        }
        /// 21.10.2019


        protected override void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /// 21.10.2019
            if (e.RowIndex > -1)
            {
                /// 21.10.2019
                int rowIndex = dgList.CurrentCell.RowIndex;
            base.dgList_CellDoubleClick(sender, e);

            // Выбор города
            cbCity.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[e.RowIndex]["id_city"]);
            // Выбор часового пояса
            nUTC.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[e.RowIndex]["time_zone"].ToString());

                /// 21.10.2019
            }
            /// 21.10.2019
        }

        /////11.02.2020
        protected override void btnDelete_Click(object sender, EventArgs e)
        {


            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.Rows.RemoveAt(rowIndex);
            dataSet.Tables[0].Rows[rowIndex].Delete();
            dataBase.UpdateData(dataSet);

            dataSet = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
            dgList.DataSource = dataSet.Tables[0];

            ///11.10.2019
            ///tbName.Clear();
            if (dgList.CurrentCell != null)
            {
                pok1();
               
            }

            dgList.Select();

        }
        /////11.02.2020

        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            if (tbName.Text != String.Empty && result == 0)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow[gridFieldName] = tbName.Text;
                if (cbCity.SelectedIndex != -1)
                {
                    dataRow["id_city"] = cbCity.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Выберите город кассового центра!");
                    return;
                }
                
                dataRow["time_zone"] = nUTC.Value;
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;

                ////11.02.2020
                // dataSet.Tables[0].Rows[rowIndex]["tipsp1"] = 1;
                dataRow["tipsp1"] = 1;
                ////11.02.2020

                dataSet.Tables[0].Rows.Add(dataRow);
                dataBase.UpdateData(dataSet);

                ////11.02.2020
               // dataSet = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
               // dgList.DataSource = dataSet.Tables[0];
                ////11.02.2020


                dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];

                /// 21.10.2019
                // dgList_SelectionChanged(sender, e);
                base.dgList_SelectionChanged(sender, e);
                dgList.Refresh();
                dgList.Update();
                // Выбор города
                cbCity.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"]);
                // Выбор часового пояса
                nUTC.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["time_zone"].ToString());
                /// 21.10.2019

            }
        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;
            dataSet.Tables[0].Rows[rowIndex]["id_city"] = cbCity.SelectedValue;
            dataSet.Tables[0].Rows[rowIndex]["time_zone"] = nUTC.Value;

            

            dataBase.UpdateData(dataSet);

            ////11.02.2020
            //dataSet = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
           // dgList.DataSource = dataSet.Tables[0];
            ////11.02.2020

        }

       
    }
}
