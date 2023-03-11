using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.ParentForms;
using CountingForms.Services;

namespace CountingForms.DictionaryForms
{
    public partial class ShiftOpingForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase;
        private DataSet dataSet;
        private DataSet shiftDataSet, shift2DataSet;
        private bool del = false;

        public ShiftOpingForm()
        {
            dataBase = new MSDataBase();
            dataBase.Connect();

            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dgShift.AutoGenerateColumns = false;
            dgShift.Columns.Add("name", "Наименование смены");
            dgShift.Columns["name"].Visible = true;
            dgShift.Columns["name"].Width = 180;
            dgShift.Columns["name"].DataPropertyName = "name";
            dgShift.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgShift.Columns.Add("id", "id");
            dgShift.Columns["id"].Visible = false;
            dgShift.Columns["id"].DataPropertyName = "id";

            dgShift.Columns.Add("startDateTime", "Начало смены");
            dgShift.Columns["startDateTime"].Visible = true;
            dgShift.Columns["startDateTime"].Width = 110;
            dgShift.Columns["startDateTime"].DataPropertyName = "startDateTime";
            dgShift.Columns["startDateTime"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgShift.Columns.Add("endDateTime", "Конец смены");
            dgShift.Columns["endDateTime"].Visible = false;
            dgShift.Columns["endDateTime"].Width = 110;
            dgShift.Columns["endDateTime"].DataPropertyName = "endDateTime";

            dgShift.Columns.Add("status1", "Статус смены");
            dgShift.Columns["status1"].Visible = true;
            dgShift.Columns["status1"].Width = 100;
            dgShift.Columns["status1"].DataPropertyName = "status1";
            dgShift.Columns["status1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgShift.RowHeadersWidth = 20;
            dgShift.Width = 413;

            //--
            dgShift2.AutoGenerateColumns = false;
            dgShift2.Columns.Add("user_name", "Пользователь");
            dgShift2.Columns["user_name"].Visible = true;
            dgShift2.Columns["user_name"].Width = 100;
            dgShift2.Columns["user_name"].DataPropertyName = "user_name";

            dgShift2.Columns.Add("branch_name", "Зона");
            dgShift2.Columns["branch_name"].Visible = true;
            dgShift2.Columns["branch_name"].Width = 100;
            dgShift2.Columns["branch_name"].DataPropertyName = "branch_name";

            dgShift2.Columns.Add("shift_name", "Смена");
            dgShift2.Columns["shift_name"].Visible = true;
            dgShift2.Columns["shift_name"].Width = 145;
            dgShift2.Columns["shift_name"].DataPropertyName = "shift_name";

            dgShift2.RowHeadersWidth = 4;

            PrepareData();

            clear();
        }

        private async void ShiftOpingForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            clear();
        }

        private void clear()
        {
            cbShifts.SelectedIndex = -1;
            cbShifts.Text = "";
            //tbName.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            dateTimePicker3.Text = "";
            dateTimePicker4.Text = "";
        }

        public void user()
        {
            if (shiftDataSet != null)
                if (shiftDataSet.Tables[0] != null)
                    if (dgShift.CurrentCell != null)
                    {
                        shift2DataSet = dataBase.GetData9("SELECT t2.user_name, t3.branch_name, CAST(t4.startDateTime AS nvarchar(6)) + ' - ' + t4.name shift_name FROM[CountingDB].[dbo].[t_g_shift_user] t1 " +
                            "left join t_g_user t2 on(t1.id_user = t2.id) left join t_g_cashcentre t3 on(t1.id_zona = t3.id) " +
                            "left join t_g_shift t4 on(t1.id_shift = t4.id) where t1.id_shift = " + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());

                        dgShift2.DataSource = shift2DataSet.Tables[0];
                    }
        }
        private void PrepareData()
        {
            shiftDataSet = dataBase.GetData9("Select *, iif(status=0,'Открыто','Закрыто') status1 from t_g_shift where status=0 and [id_cashcenter] = "+DataExchange.CurrentUser.CurrentUserCentre+" order by startDateTime desc");

            dgShift.DataSource = shiftDataSet.Tables[0];

        }
        private void btnClose_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            del = false;
            if (cbShifts.Text != "")
            {
                
                dataSet = dataBase.GetData9("Insert into t_g_shift ([name],[startDateTime],[status],[creation],[creation_user],[lastupdate],[last_update_user],[id_cashcenter]) " +
                    "values('" + cbShifts.Text.Trim() + "', '" + dateTimePicker1.Text + " " + dateTimePicker2.Text + "', 0, " +
                    " getdate(), " + DataExchange.CurrentUser.CurrentUserId + ", getdate(), " + DataExchange.CurrentUser.CurrentUserId + ", "+ DataExchange.CurrentUser.CurrentUserCentre + ")");
                MessageBox.Show("Смена создана!");
                PrepareData();
                clear();
            }
            else
            {
                MessageBox.Show("Введите смену");
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            del = false;
            if (dgShift != null)
                if (dgShift.Rows.Count > 0)
                    if (dgShift.CurrentCell.ColumnIndex != -1)
                    {
                        dataSet = dataBase.GetData9("UPDATE t_g_shift SET [name] = '" + cbShifts.Text.Trim() + "', [startDateTime] = '" + dateTimePicker1.Text + " " + dateTimePicker2.Text + "', [lastupdate] = getdate(), [last_update_user] = "+DataExchange.CurrentUser.CurrentUserId+
                                                    " where id = "+ shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString() );
                        
                        MessageBox.Show("Смена изменена!");
                        PrepareData();
                        clear();

                    }

        }

        private void dgShift_SelectionChanged(object sender, EventArgs e)
        {
            //PrepareData();
            if (!del)
            {
                detal1();
                user();
            }
        }
        private void detal1()
        {
            if (!del)
                if (dgShift != null)
                if (dgShift.Rows.Count > 0)
                    if (dgShift.CurrentCell.ColumnIndex != -1)
                    {
                        cbShifts.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["name"].ToString();
                        dateTimePicker1.Text= shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["startDateTime"].ToString();
                        dateTimePicker2.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["startDateTime"].ToString();

                        //dateTimePicker3.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["endDateTime"].ToString();
                        //dateTimePicker4.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["endDateTime"].ToString();

                       
                    }
            
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgShift != null)
                if (dgShift.Rows.Count > 0)
                    if (dgShift.CurrentCell.ColumnIndex != -1)
                    {
                        dataSet = dataBase.GetData9("DELETE FROM t_g_shift WHERE id =" + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());
                        dataSet = dataBase.GetData9("DELETE FROM [t_g_shift_user] WHERE [id_shift]=" + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());
                        MessageBox.Show("Смена удалена");
                        del = true;


                        PrepareData();
                        clear();
                        
                    }

        }

        private void dgShift_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            user();
            del = false;
        }

        private void dgShift_Click(object sender, EventArgs e)
        {
            del = false;
            user();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (dgShift.Rows.Count == 0)
                btnAdd_Click(sender, e);
            if (shiftDataSet != null)
                if (shiftDataSet.Tables[0].Rows.Count > 0)
                {
                    del = false;
                    ShiftAddUserForm shiftAddUserForm = new ShiftAddUserForm();
                    shiftAddUserForm.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["name"].ToString().Trim() + " - " + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["startDateTime"].ToString().Substring(0, 5);
                    shiftAddUserForm.id_shift = Convert.ToInt64(shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"]);//.ToString().Trim();

                    //DialogResult dialogResult = denominationForm.ShowDialog();

                    shiftAddUserForm.ShowDialog();
                    user();

                }
                else
                    MessageBox.Show("Добавьте смену! а затем можете добавить сотрудников");
        }
    }
}
