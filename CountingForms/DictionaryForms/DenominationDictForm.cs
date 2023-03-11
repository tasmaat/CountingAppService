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
    public partial class DenominationDictForm : DictionaryForm
    {
        private DataSet currencyDataSet = null;
        private DataSet conditionDataSet = null;

        ///27.11.2019
        private DataSet tipzen = null;
        ///27.11.2019

        public DenominationDictForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public DenominationDictForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {           
            //Инициализация компонентов управления
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
            //Запрос данных по валютам номинала и состоянию
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();
            currencyDataSet = dataBase.GetData("t_g_currency");
            conditionDataSet = dataBase.GetData("t_g_condition");

            ///27.11.2019
             tipzen = dataBase.GetData("t_g_tipzenn");
            ///27.11.2019

            //Заполнение валют  
            cbCurrency.DataSource = currencyDataSet.Tables[0];
            cbCurrency.DisplayMember = "name";
            cbCurrency.ValueMember = "id";
            cbCurrency.SelectedIndex = -1;



            cbCurrency.SelectedIndexChanged += new EventHandler(cbCurrency_SelectedIndexChanged);


            ///27.11.2019
            comboBox1.DataSource = tipzen.Tables[0];
            comboBox1.DisplayMember = "name_zenn";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;
            comboBox1.SelectedIndexChanged += new EventHandler(cb1_SelectedIndexChanged);
            ///27.11.2019


            //Установка минимальных и максимальных значений
            nValue.Maximum = Decimal.MaxValue;
            nValue.Minimum = Decimal.MinValue;


            /////21.10.2019

            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name = "id_currency";
            dg1.HeaderText = "Валюта";
            //dg1.FlatStyle = FlatStyle.Flat;
            dg1.DataPropertyName = "id_currency";
            dg1.DisplayMember = "name";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = currencyDataSet.Tables[0];
            dgList.Columns.Add(dg1);

            dgList.Columns.Add("value", "Значение");
            dgList.Columns["value"].DataPropertyName = "value";
            dgList.Columns["value"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("valid_date", "Действителен");
            dgList.Columns["valid_date"].DataPropertyName = "valid_date";
            dgList.Columns["valid_date"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dgList.Columns.Add("id_tipzen", "id_tipzen");
            dgList.Columns["id_tipzen"].DataPropertyName = "id_tipzen";
            dgList.Columns["id_tipzen"].Visible = false;


            /////21.10.2019


        }

        private async void DenominationDictForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        protected override void tbName_TextChanged(object sender, EventArgs e)
        {
            base.tbName_TextChanged(sender, e);
        }

        ///27.11.2019
        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(pm.EnabledPossibility(perm, btnModify))
                btnModify.Enabled = true;
            if (pm.EnabledPossibility(perm, btnAdd))
                btnAdd.Enabled = true;
        }
        ///27.11.2019

        private void cbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int rowIndex = dgList.CurrentCell.RowIndex;
                        
            if (cbCurrency.SelectedIndex != -1 && dataSet.Tables[0].Rows[rowIndex]["id_currency"] != cbCurrency.SelectedValue)
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

        private void nValue_ValueChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (nValue.Value >= 0 && Convert.ToDecimal(dataSet.Tables[0].Rows[rowIndex]["value"]) != nValue.Value)
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

        private void dtValid_ValueChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (dtValid.Value >= DateTime.Now && Convert.ToDateTime(dataSet.Tables[0].Rows[rowIndex]["valid_date"]) != dtValid.Value)
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

                // Выбор валюты
                cbCurrency.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_currency"].Value);//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_currency"]);
                // Выбор стоимости
                nValue.Value = Convert.ToDecimal(dgList.CurrentRow.Cells["value"].Value.ToString());//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["value"].ToString());
                // Выбор времени действия 
                dtValid.Value = Convert.ToDateTime(dgList.CurrentRow.Cells["valid_date"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["valid_date"]);

                ///27.11.2019
                 comboBox1.SelectedValue = -1;
                comboBox1.Text = "";
                if (dgList.CurrentRow.Cells["id_tipzen"].Value/*base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_tipzen"]*/!= DBNull.Value)
                    comboBox1.SelectedValue = dgList.CurrentRow.Cells["id_tipzen"].Value;// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_tipzen"];
               // comboBox1.Refresh();
               // comboBox1.Update();

                ///27.11.2019


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
            
            // Выбор валюты
            cbCurrency.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[e.RowIndex]["id_currency"]);
            // Выбор стоимости
            nValue.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[e.RowIndex]["value"].ToString());
            // Выбор времени действия 
            dtValid.Value = Convert.ToDateTime(base.dataSet.Tables[0].Rows[e.RowIndex]["valid_date"]);

                ///27.11.2019
                comboBox1.SelectedValue = -1;
                comboBox1.Text = "";
                if (base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_tipzen"] != DBNull.Value)
                    comboBox1.SelectedValue = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_tipzen"];

                ///27.11.2019

                /// 21.10.2019
            }
            /// 21.10.2019


        }



        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            //int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            if (tbName.Text != String.Empty)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow[gridFieldName] = tbName.Text;
                dataRow["id_currency"] = cbCurrency.SelectedValue;
                dataRow["value"] = nValue.Value;
                dataRow["valid_date"] = dtValid.Value;
                //dataRow["sellrate"] = DBNull;
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;

                ///27.11.2019
                if (comboBox1.SelectedValue!=null
                )
                dataRow["id_tipzen"] = comboBox1.SelectedValue;
               // else
               //     dataRow["id_tipzen"] = DBNull.Value;
                ///27.11.2019


                //dataSet.Tables.Add((DataTable) dgList.DataSource);
                dataSet.Tables[0].Rows.Add(dataRow);
                dataBase.UpdateData(dataSet);

                /// 21.10.2019
                dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];
                base.dgList_SelectionChanged(sender, e);
                dgList.Refresh();
                dgList.Update();

                // Выбор валюты
                cbCurrency.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_currency"]);
                // Выбор стоимости
                nValue.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["value"].ToString());
                // Выбор времени действия 
                dtValid.Value = Convert.ToDateTime(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["valid_date"]);

                ///27.11.2019
                comboBox1.SelectedValue = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_tipzen"];
                ///27.11.2019

                /// 21.10.2019


            }
        }
        
        protected override void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.CurrentCell.Value = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;
            dataSet.Tables[0].Rows[rowIndex]["id_currency"] = cbCurrency.SelectedValue;
            dataSet.Tables[0].Rows[rowIndex]["value"] = nValue.Value;
            dataSet.Tables[0].Rows[rowIndex]["valid_date"] = dtValid.Value;


            ///27.11.2019
            if (comboBox1.SelectedValue != null
                )
                dataSet.Tables[0].Rows[rowIndex]["id_tipzen"] = comboBox1.SelectedValue;
            else
                dataSet.Tables[0].Rows[rowIndex]["id_tipzen"] = DBNull.Value;
                            ///27.11.2019

                            //dataRow["sellrate"] = DBNull;

                            dataBase.UpdateData(dataSet);

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

       
    }
}
