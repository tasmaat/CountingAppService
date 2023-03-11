using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
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
    public partial class ShiftCloseForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase;
        private DataSet shiftDataSet, shift2DataSet, countingDataSet, cardsDataSet, dataSet, cashtransferDataSet;

        public ShiftCloseForm()
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

            dgShift.Columns.Add("id", "id");
            dgShift.Columns["id"].Visible = false;
            dgShift.Columns["id"].DataPropertyName = "id";

            dgShift.Columns.Add("startDateTime", "Начало смены");
            dgShift.Columns["startDateTime"].Visible = true;
            dgShift.Columns["startDateTime"].Width = 110;
            dgShift.Columns["startDateTime"].DataPropertyName = "startDateTime";

            dgShift.Columns.Add("endDateTime", "Конец смены");
            dgShift.Columns["endDateTime"].Visible = true; ;
            dgShift.Columns["endDateTime"].Width = 110;
            dgShift.Columns["endDateTime"].DataPropertyName = "endDateTime";

            dgShift.Columns.Add("status1", "Статус смены");
            dgShift.Columns["status1"].Visible = true;
            dgShift.Columns["status1"].Width = 100;
            dgShift.Columns["status1"].DataPropertyName = "status1";

            dgShift.RowHeadersWidth = 20;
            dgShift.Width = 523;

            //--//

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

        private async void ShiftCloseForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            clear();
        }

        private void PrepareData()
        {
            shiftDataSet = dataBase.GetData9("Select *, iif(status=0,'Открыто','Закрыто') status1 from t_g_shift where [id_cashcenter] = " + DataExchange.CurrentUser.CurrentUserCentre + " order by status, startDateTime desc");

            dgShift.DataSource = shiftDataSet.Tables[0];
        }

        private void clear()
        {
         
            tbShift.Text = "";
          
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
           // MessageBox.Show("4");

        }

        private void detal1()
        {
            if (dgShift.CurrentCell != null)
            {

                tbShift.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["name"].ToString();
                dateTimePicker1.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["startDateTime"].ToString();
                dateTimePicker2.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["startDateTime"].ToString();

                if (Convert.ToInt32(shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["status"]) != 0)
                {
                    if(pm.VisiblePossibility(perm, label4))
                        label4.Visible = true;
                    if (pm.VisiblePossibility(perm, dateTimePicker3))
                        dateTimePicker3.Visible = true;
                    if (pm.VisiblePossibility(perm, dateTimePicker4))
                        dateTimePicker4.Visible = true;
                    dateTimePicker3.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["endDateTime"].ToString();
                    dateTimePicker4.Text = shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["endDateTime"].ToString();
                }
                else
                {
                    label4.Visible = false;
                    dateTimePicker3.Visible = false;
                    dateTimePicker4.Visible = false;
                }

            }
        }

        private void dgShift_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["status"].ToString()=="1")
            {
                MessageBox.Show("Не возможно закрыть закрытую смену");
                return;
            }
            Login.LoginForm login = new Login.LoginForm();
            DialogResult result = login.ShowDialog();
            if(Login.LoginForm.Login)
            {
                //MessageBox.Show("User name="+DataExchange.CurrentUser.CurrentUserName);
                countingDataSet = dataBase.GetData9("Select * from t_g_counting  where fl_prov in (0,1) and deleted = 0  " +
                    "and id_shift_current="+ shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());
                cardsDataSet = dataBase.GetData9("select * from t_g_cards t1 left join t_g_counting t2 on t1.id_bag=t2.id_bag where fl_obr = 0 " +
                    " and t2.deleted=0 " +
                    " and t1.id_shift_current = " + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());
                cashtransferDataSet = dataBase.GetData9("select * from t_w_cashtransfer where id_status in(1,2) " +
                    " and id_shift_current=" + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());
                if(countingDataSet.Tables[0].Rows.Count>0 | cardsDataSet.Tables[0].Rows.Count>0 | cashtransferDataSet.Tables[0].Rows.Count > 0)
                {
                    //        DialogResult result1 = MessageBox.Show(
                    //        "Невозможно закрыть смену, есть не завершенные операции. Продолжить?"
                    //        ,
                    //         "Закрытие Смены",
                    //        MessageBoxButtons.YesNo,
                    //        MessageBoxIcon.Information,
                    //        MessageBoxDefaultButton.Button1
                    ////,MessageBoxOptions.DefaultDesktopOnly
                    //        );

                    //        if (result1 == DialogResult.No)
                    //            return;
                    MessageBox.Show("Невозможно закрыть смену, есть не завершенные операции.");
                    return;
                }

                dataSet = dataBase.GetData9("update t_g_shift set status=1, endDateTime=GETDATE(), id_user_close_shift=" +DataExchange.CurrentUser.CurrentUserId.ToString()+
                    " where id=" + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());

                //MessageBox.Show("Смену зарыли!");

                dataSet = dataBase.GetData9("[ShiftCopy] " + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());
                dataSet = dataBase.GetData9("[ShiftCopyM] " + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());

                dataSet = dataBase.GetData9("[ShiftDelete] " + shiftDataSet.Tables[0].Rows[dgShift.CurrentCell.RowIndex]["id"].ToString());


                MessageBox.Show("Смена закрыта, записи скопированы в архив и удалены оперативной базы!");

                PrepareData();

            }
            else
            {
                MessageBox.Show("Вы ввели не правилный логин или пароль!");
            }
        }

        private void dgShift_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            user();
            detal1();
        }

        private void dgShift_Click(object sender, EventArgs e)
        {
            user();
            detal1();
        }

        private void dgShift_SelectionChanged(object sender, EventArgs e)
        {
            user();
            detal1();
        }

       
    }
}
