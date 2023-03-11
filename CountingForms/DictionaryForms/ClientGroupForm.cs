using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;

namespace CountingForms.DictionaryForms
{
    public partial class ClientGroupForm : DictionaryForm
    {

        //protected DataSet dataSet = null;
        //protected String gridFieldName = null;
        //protected String tableName = null;
        //protected MSDataBase dataBase = null;     

        public ClientGroupForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public ClientGroupForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void ClientGroupForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }
    }
}
