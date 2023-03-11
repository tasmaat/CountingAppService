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
using CountingForms.Services;
using DataExchange;

namespace CountingForms.DictionaryForms
{
    public partial class CennostForm : DictionaryForm
    {
        public DataTable blokTable;
        public CennostForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }


        public CennostForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {

            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
            /////21.10.2019
            blokTable = new DataTable("blok");
            DataColumn p1 = new DataColumn("id", Type.GetType("System.Boolean"));
            DataColumn p2 = new DataColumn("nameblok", Type.GetType("System.String"));
            blokTable.Columns.Add(p1);
            blokTable.Columns.Add(p2);
            blokTable.Rows.Add(new object[] { "False", "Видимый" });
            blokTable.Rows.Add(new object[] { "True", "Не видимый" });


            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name = "visible";
            dg1.HeaderText = "Видимость";
            //dg1.FlatStyle = FlatStyle.Flat;
            dg1.DataPropertyName = "visible";
            dg1.DisplayMember = "nameblok";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = blokTable;
            dgList.Columns.Add(dg1);

            /////21.10.2019

        }

        private async void CennostForm_Load(object sender, EventArgs e)
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

                if (dgList.CurrentRow.Cells["visible"].Value != DBNull.Value)//base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["visible"] != DBNull.Value)
                    cbVisisble.Checked = Convert.ToBoolean(dgList.CurrentRow.Cells["visible"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["visible"]);
                else
                    cbVisisble.Checked = false;
            }

        }


        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            if (tbName.Text != String.Empty && result == 0)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow["name_zenn"] = tbName.Text;
                dataRow["Visible"] = cbVisisble.CheckState == CheckState.Checked;
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


                cbVisisble.Checked = Convert.ToBoolean(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["visible"]);


                /// 21.10.2019


            }
        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.CurrentCell.Value = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
            dataSet.Tables[0].Rows[rowIndex]["name_zenn"] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["Visible"] = cbVisisble.CheckState == CheckState.Checked;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;

            dataBase.UpdateData(dataSet);

        }

       
    }
    }
