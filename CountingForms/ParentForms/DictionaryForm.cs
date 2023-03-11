using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountingForms;
using DataExchange;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;

namespace CountingForms
{
    public partial class DictionaryForm : ParentForm
    {
        protected DataSet dataSet = null;
        protected String gridFieldName = null;
        protected String tableName = null;
        protected MSDataBase dataBase = null;

        protected IPermisionsManager pm;
        protected MSDataBaseAsync dataBaseAsync;
        protected List<t_g_role_permisions> perm;


        public DictionaryForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конструктор создания форм отчетов
        /// </summary>
        /// <param name="formName">Название справочника</param>
        /// <param name="gridFieldName">Наименование поля</param>
        /// <param name="strCaption">Подпись поля</param>
        /// <param name="tableName">Название таблицы</param>
        public DictionaryForm(String formName, String gridFieldName, string strCaption, string tableName)
        {
            InitializeComponent();
            dataSet = new DataSet();
            dataBase = new MSDataBase();
            dataBase.Connect();
            this.tableName = tableName;
            this.dataSet = dataBase.GetData(tableName);
            this.gridFieldName = gridFieldName;
            this.tableName = tableName;
            
            dgList.AutoGenerateColumns = false;
            dgList.DataSource = null;
            
            dgList.DataSource = dataSet.Tables[0];
            dgList.Columns.Add(gridFieldName, gridFieldName);            
            dgList.Columns[gridFieldName].DataPropertyName = gridFieldName;
            dgList.ColumnHeadersVisible = true;
            dgList.Columns[gridFieldName].HeaderText = strCaption;
            dgList.RowHeadersVisible = true;
            dgList.Columns[gridFieldName].Visible = true;
            dgList.Columns[gridFieldName].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgList.RowHeadersWidth = 30;            
     
            this.Text = formName;
            lblDictName.Text = formName;
            lblName.Text = strCaption;             
        }

        protected virtual void DictionaryForm_Load(object sender, EventArgs e)
        {

            dgList.ClearSelection();
            ///11.10.2019
            

            if (dgList.CurrentCell != null)
            {
                  pok1();
                /*
                tbName.Text = dgList.CurrentCell.Value.ToString().Trim(); ;
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
            }

            ///11.10.2019
        }

        protected virtual void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { 
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                ///21.10.2019
               // tbName.Text = dgList[e.ColumnIndex, e.RowIndex].Value.ToString().Trim();
                tbName.Text = dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim();
                ///21.10.2019
            }

        }

        protected virtual void dgList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                ///21.10.2019
               // tbName.Text = dgList.CurrentCell.Value.ToString().Trim(); ;
                tbName.Text = dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim();
                ///21.10.2019
            }
        }


        protected virtual void dgList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        protected virtual void tbName_TextChanged(object sender, EventArgs e)
        {

            ///21.10.2019
            if (tbName.Text != String.Empty)
            //if (tbName.Text != String.Empty && dgList.CurrentCell.Value.ToString().Trim() != tbName.Text)
            //if (tbName.Text != String.Empty && dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim() != tbName.Text)
            ///21.10.2019
            {
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
            }


        }

        protected virtual void btnAdd_Click(object sender, EventArgs e)
        {
            int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            if (tbName.Text != String.Empty && result == 0)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow[gridFieldName] = tbName.Text;
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;

               
                dataSet.Tables[0].Rows.Add(dataRow);
                dataBase.UpdateData(dataSet);
                dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];

                ///11.10.2019
                if (dgList.CurrentCell != null)
                {
                      pok1();
                    /*
                    tbName.Text = dgList.CurrentCell.Value.ToString().Trim(); ;
                    btnModify.Enabled = true;
                    btnAdd.Enabled = true;
                    */
                }

                dgList.Select();
                ///11.10.2019

            }
        }

        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Удалить?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            
            if (result == DialogResult.No)
                return;

            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.Rows.RemoveAt(rowIndex);
            dataSet.Tables[0].Rows[rowIndex].Delete();
            dataBase.UpdateData(dataSet);
            dataSet = dataBase.GetData(tableName);
            dgList.DataSource = dataSet.Tables[0];


            ///11.10.2019
            ///tbName.Clear();
            if (dgList.CurrentCell != null)
            {
                  pok1();
                /*
                tbName.Text = dgList.CurrentCell.Value.ToString().Trim(); ;
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
            }

            dgList.Select();
            ///11.10.2019


        }

        protected virtual void dgList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgList.ClearSelection();
            ///11.10.2019
           // dgList.Rows[e.RowIndex].Selected = true;
            ///11.10.2019
        }



        protected virtual void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.CurrentCell.Value = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;
            
            dataBase.UpdateData(dataSet);

            ///11.10.2019
            dgList.Select();
            ///11.10.2019

        }

        protected virtual void DictionaryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //dataBase.Disconnect();
        }

        /// <summary>
        /// Очистка данных для грида
        /// </summary>
        /// <param name="dataGridView">экземпляр грирда</param>
        public void ClearDataGrid(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = null;
            dataGridView.ClearSelection();
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.RowHeadersVisible = true;
            dataGridView.RowHeadersWidth = 30;
            dataGridView.AutoSize = true;
        }

        private void dgList_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

        }

        ///11.10.2019
        
        protected virtual void pok1()
        {

            //tbName.Text = dgList.CurrentCell.Value.ToString().Trim(); ;
            tbName.Text = dgList[0, dgList.CurrentRow.Index].Value.ToString().Trim();
            btnModify.Enabled = true;
            btnAdd.Enabled = true;

        }

        // private void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        protected virtual void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          //  pok1();
            
        }

        protected virtual void dgList_SelectionChanged(object sender, EventArgs e)
        {
           if (dgList.CurrentCell!=null)
            {
                pok1();
                /*
                tbName.Text = dgList.CurrentCell.Value.ToString().Trim(); ;
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
            }

        }

        protected virtual void btnOchist_Click(object sender, EventArgs e)
        {
            //
        }

        ///11.10.2019

    }
}
