using CountingDB;
using CountingForms.DictionaryForms;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.ParentForms
{
    public partial class ShiftAddUserForm : Form
    {
        private MSDataBase dataBase;
        private DataSet dataSet, zonaDataSet1, zonaDataSet2 = null;
        private DataSet userDataSet1, userDataSet2, shiftDataSet = null;
        public long id_shift;
        private bool check = true;

        public ShiftAddUserForm()
        {
            dataBase = new MSDataBase();
            dataBase.Connect();

            InitializeComponent();

            dgZona1.AutoGenerateColumns = false;
            dgZona1.Columns.Add("branch_name", "Наименование зоны");
            dgZona1.Columns["branch_name"].Visible = true;
            //dgZona.Columns["branch_name"].Width = 180;
            dgZona1.Columns["branch_name"].DataPropertyName = "branch_name";
            dgZona1.Columns["branch_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgZona1.Columns.Add("id", "id");
            dgZona1.Columns["id"].Visible = false;
            dgZona1.Columns["id"].DataPropertyName = "id";

            dgZona1.RowHeadersWidth = 4;

            dgZona2.AutoGenerateColumns = false;
            dgZona2.Columns.Add("branch_name", "Наименование зоны");
            dgZona2.Columns["branch_name"].Visible = true;
            //dgZona.Columns["branch_name"].Width = 180;
            dgZona2.Columns["branch_name"].DataPropertyName = "branch_name";
            dgZona2.Columns["branch_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgZona2.Columns.Add("id", "id");
            dgZona2.Columns["id"].Visible = false;
            dgZona2.Columns["id"].DataPropertyName = "id";

            dgZona2.RowHeadersWidth = 4;
                        
            dgUser1.AutoGenerateColumns = false;
            dgUser1.Columns.Add("user_name", "Пользователь");
            dgUser1.Columns["user_name"].Visible = true;            
            dgUser1.Columns["user_name"].DataPropertyName = "user_name";
            dgUser1.Columns["user_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgUser1.Columns.Add("id", "id");
            dgUser1.Columns["id"].Visible = false;
            dgUser1.Columns["id"].DataPropertyName = "id";

            dgUser1.RowHeadersWidth = 4;

            dgUser2.AutoGenerateColumns = false;
            dgUser2.Columns.Add("user_name", "Пользователь");
            dgUser2.Columns["user_name"].Visible = true;
            dgUser2.Columns["user_name"].DataPropertyName = "user_name";
            dgUser2.Columns["user_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgUser2.Columns.Add("id", "id");
            dgUser2.Columns["id"].Visible = false;
            dgUser2.Columns["id"].DataPropertyName = "id";

            dgUser2.RowHeadersWidth = 4;

            dgShift.AutoGenerateColumns = false;
            dgShift.Columns.Add("user_name", "Пользователь");
            dgShift.Columns["user_name"].Visible = true;
            dgShift.Columns["user_name"].Width = 100;
            dgShift.Columns["user_name"].DataPropertyName = "user_name";
            dgShift.Columns["user_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgShift.Columns.Add("branch_name", "Зона");
            dgShift.Columns["branch_name"].Visible = true;
            dgShift.Columns["branch_name"].Width = 100;
            dgShift.Columns["branch_name"].DataPropertyName = "branch_name";
            dgShift.Columns["branch_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgShift.Columns.Add("shift_name", "Смена");
            dgShift.Columns["shift_name"].Visible = true;
            dgShift.Columns["shift_name"].Width = 145;
            dgShift.Columns["shift_name"].DataPropertyName = "shift_name";
            dgShift.Columns["shift_name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgShift.RowHeadersWidth = 4;

            
        }

        private void btnAddZona_Click(object sender, EventArgs e)
        {
            if(zonaDataSet1!=null)
                if(zonaDataSet1.Tables[0].Rows.Count>0)
                {
                    if (zonaDataSet2.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_shift") == id_shift
                    && x.Field<Int64>("id_zona") == Convert.ToInt64(zonaDataSet1.Tables[0].Rows[dgZona1.CurrentCell.RowIndex]["id"])))
                    {
                       // MessageBox.Show("такая зона уже существует!");
                        return;
                    }

                    dataSet = dataBase.GetData9("Insert into [t_g_shift_zona] ([id_zona],[id_shift],[id_cashcenter],[cration],[creation_user],[lastupdate],[last_update_user]) " +
                        " values ("+zonaDataSet1.Tables[0].Rows[dgZona1.CurrentCell.RowIndex]["id"]+", "+id_shift.ToString().Trim()+", "+DataExchange.CurrentUser.CurrentUserCentre+"," +
                        " getdate(), "+DataExchange.CurrentUser.CurrentUserId+ ", getdate(), " + DataExchange.CurrentUser.CurrentUserId + ") ");
                    
                    zona();
                }
        }

        private void btnDeleteZona_Click(object sender, EventArgs e)
        {
            if (zonaDataSet2 != null)
                if (zonaDataSet2.Tables[0].Rows.Count > 0)
                {
                    
                    dataSet = dataBase.GetData9("DELETE FROM [dbo].[t_g_shift_zona] WHERE id="+ zonaDataSet2.Tables[0].Rows[dgZona2.CurrentCell.RowIndex]["id"]);

                    dataSet = dataBase.GetData9("DELETE FROM [dbo].[t_g_shift_user] WHERE id_shift="+id_shift.ToString()+ " and id_zona="+ zonaDataSet2.Tables[0].Rows[dgZona2.CurrentCell.RowIndex]["id_zona"]);
                    check = false; 
                    zona();
                }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (zonaDataSet2.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Добавьте бригаду для смены!");
                return;
            }

                if (userDataSet1 != null)
                if (userDataSet1.Tables[0].Rows.Count > 0)
                {
                    if (userDataSet2.Tables[0].AsEnumerable().Any(x => 
                    x.Field<Int64>("id_shift") == id_shift
                    && x.Field<Int64>("id_zona") == Convert.ToInt64(zonaDataSet2.Tables[0].Rows[dgZona2.CurrentCell.RowIndex]["id_zona"])
                    && x.Field<Int64>("id_user") == Convert.ToInt64(userDataSet1.Tables[0].Rows[dgUser1.CurrentCell.RowIndex]["id"])))
                    {
                        //MessageBox.Show("таой пользователь уже существует!");
                        return;
                    }

                    DataSet check = dataBase.GetData9("select t1.* from t_g_shift_user t1 left join t_g_shift t2 on t1.id_shift=t2.id where t2.status=0");

                    if(check.Tables[0].AsEnumerable().Any(x=>
                    x.Field<Int64>("id_user")==Convert.ToInt64(userDataSet1.Tables[0].Rows[dgUser1.CurrentCell.RowIndex]["id"])))
                    {
                        MessageBox.Show("Этого пользователя невозможно добавить, поскольку этот пользователь существует в списке смен");
                        return;
                    }


                    if (zonaDataSet2.Tables[0].Rows.Count > 0)

                    {
                        dataSet = dataBase.GetData9("INSERT INTO [dbo].[t_g_shift_user] ([id_user],[id_zona],[id_shift],[id_cashcenter],[creation],[creation_user],[lastupdate],[last_update_user]) " +
                          " VALUES (" + userDataSet1.Tables[0].Rows[dgUser1.CurrentCell.RowIndex]["id"] + ", " + zonaDataSet2.Tables[0].Rows[dgZona2.CurrentCell.RowIndex]["id_zona"] + ", " + id_shift.ToString().Trim() + ", " + DataExchange.CurrentUser.CurrentUserCentre + ", " +
                          " getdate(), " + DataExchange.CurrentUser.CurrentUserId + ", getdate(), " + DataExchange.CurrentUser.CurrentUserId + ") ");

                    }
                    else
                    {
                        MessageBox.Show("Добавьте бригаду для смены!");
                        return;
                    }

                    user();
                }
            
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            //--
            if (userDataSet2 != null)
                if (userDataSet2.Tables[0].Rows.Count > 0)
                {

                    dataSet = dataBase.GetData9("DELETE FROM [dbo].[t_g_shift_user] WHERE id=" + userDataSet2.Tables[0].Rows[dgUser2.CurrentCell.RowIndex]["id"]);

                    user();
                }

            //--
        }

        private void dgZona2_SelectionChanged(object sender, EventArgs e)
        {
            if(check)
            user();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ShiftOpingForm shiftOpingForm = new ShiftOpingForm();
            //shiftOpingForm.user();
            Close();
        }

        private void dgZona2_Click(object sender, EventArgs e)
        {
            check = true;
            user();
        }

        private void zona()
        {
            zonaDataSet2 = dataBase.GetData9("select t1.*, t2.branch_name from [t_g_shift_zona] t1 left join t_g_cashcentre t2 on t1.[id_zona]=t2.id where id_shift =" + id_shift.ToString() +
                " and t1.id_cashcenter=" + DataExchange.CurrentUser.CurrentUserCentre+ " order by id_zona");
            
            dgZona2.DataSource = zonaDataSet2.Tables[0];
            user();
        }

        private void user()
        {
            if(zonaDataSet2!=null)
                if(zonaDataSet2.Tables[0].Rows.Count>0)
                {
                    userDataSet2 = dataBase.GetData9("Select t1.*, t2.user_name FROM [CountingDB].[dbo].[t_g_shift_user] t1 " +
                        "left join t_g_user t2 on t1.id_user=t2.id " +
                        " where t1.id_shift="+id_shift.ToString()+
                        " and t1.id_cashcenter = "+DataExchange.CurrentUser.CurrentUserCentre+
                        " and t1.id_zona = "+zonaDataSet2.Tables[0].Rows[dgZona2.CurrentCell.RowIndex]["id_zona"] +
                        " order by t2.id ");
                    if (userDataSet2 != null)
                        dgUser2.DataSource = userDataSet2.Tables[0];
                }
            shiftDataSet = dataBase.GetData9("SELECT  t2.user_name, t3.branch_name, CAST(t4.startDateTime AS nvarchar(6)) + ' - ' + t4.name shift_name FROM[CountingDB].[dbo].[t_g_shift_user] t1 " +
                "left join t_g_user t2 on(t1.id_user = t2.id) left join t_g_cashcentre t3 on(t1.id_zona = t3.id) " +
                "left join t_g_shift t4 on(t1.id_shift = t4.id) where t1.id_shift = "+id_shift.ToString());

            dgShift.DataSource = shiftDataSet.Tables[0];
        }
        private void ShiftAddUserForm_Load(object sender, EventArgs e)
        {
            zonaDataSet1 = dataBase.GetData9("select *  FROM [t_g_cashcentre] where id_parent = "+DataExchange.CurrentUser.CurrentUserCentre);
            dgZona1.DataSource = zonaDataSet1.Tables[0];

            userDataSet1 = dataBase.GetData9("SELECT * FROM [t_g_user] where  deleted is null and id_brach="+DataExchange.CurrentUser.CurrentUserCentre);
            dgUser1.DataSource = userDataSet1.Tables[0];
            zona();
            user();
        }
    }
}
