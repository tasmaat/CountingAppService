using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Services;

namespace CountingForms.DictionaryForms
{
    public partial class CityDictForm : DictionaryForm
    {
        public CityDictForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public CityDictForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        protected override void tbName_TextChanged(object sender, EventArgs e)
        {
            
            base.tbName_TextChanged(sender, e);
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private async void CityDictForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }
    }
}
