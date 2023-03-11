using CountingDB;
using CountingDB.Entities;
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
    public partial class AdminForm : DictionaryForm
    {
        public AdminForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();            

            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dgList.Columns.Add("value", "Название поле");
            dgList.Columns["value"].DataPropertyName = "value";
        }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        protected override void btnAdd_Click(object sender, EventArgs e)
        {           
            if (tbName.Text != String.Empty)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow[gridFieldName] = tbName.Text; 
                dataRow["value"] = tbValue.Text.Trim();
                dataRow["last_update_user"] =DataExchange.CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;

                dataSet.Tables[0].Rows.Add(dataRow);
                dataBase.UpdateData(dataSet);               


                dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];

                base.dgList_SelectionChanged(sender, e);
                dgList.Refresh();
                dgList.Update();                

            }

        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] =DataExchange.CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;
            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;

            dataSet.Tables[0].Rows[rowIndex]["value"] = tbValue.Text.Trim();



            dataBase.UpdateData(dataSet);
        }

        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgList.CurrentCell != null)
            {
                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);

                tbValue.Text = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["value"].ToString();
                // Выбор города
                //cbCity.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"]);
                // Выбор часового пояса
                //nUTC.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["time_zone"].ToString());
            }
        }

      
    }
}
