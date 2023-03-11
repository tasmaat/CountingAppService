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
using DataExchange;

namespace Login
{
    public partial class ChangePassword : Form
    {

        HashCoding hashCoding = new HashCoding();
        //Поле идентификатора пользователя
        private Int64 user_id = -1;
        //Набор данных пользователей
        private DataSet usersDataSet = null;
        //Переменная для работы с БД
        private MSDataBase dataBase = new MSDataBase();


        /*
        public ChangePassword()
        {
            InitializeComponent();
        }
*/
        #region Конструктор формы изменения нового пароля
        public ChangePassword(Int64 user_id)
        {
            this.user_id = user_id;
            dataBase.Connect();
            usersDataSet = dataBase.GetData("t_g_user", "id", user_id.ToString());
            InitializeComponent();
        }
        #endregion

        #region Кнопка закрыть
        private void BtnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы не ввели пароль, без пароля возможно пользователь не сможет зайти в систему");
            Close();
        }
        #endregion


        #region Обработка кнопки изменения пароля
        protected virtual void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if(tbNewPassword.Text == tbReTypePassword.Text)
            {
                if(tbNewPassword.Text.Trim()=="")
                {
                    MessageBox.Show("Пароль не можеть пустым!");
                    tbNewPassword.Text = "";
                    tbReTypePassword.Text = "";
                    return;
                }
                if (usersDataSet != null && usersDataSet.Tables[0].Rows.Count != 0)
                {
                    AddNewPasswordToDB(tbNewPassword.Text);
                    MessageBox.Show("Пароль изменен");
                    Close();
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
        }
        #endregion

        /// <summary>
        /// Функция шифрования пароля и добьавление в БД
        /// </summary>
        /// <param name="strPassword">строка пароля</param>
        private void AddNewPasswordToDB(string strPassword)
        {
                string hashUserPassword = hashCoding.ComputeHash(strPassword);
                DataRow userRow = usersDataSet.Tables[0].AsEnumerable().First<DataRow>();
                userRow["Password"] = hashUserPassword;
                userRow["lastupdate"] = DateTime.Now;
                userRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataBase.UpdateData(usersDataSet, "t_g_user");
        }

        #region Обработка нажатия клавиш в поле ввода нового пароля
        private void TbNewPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbReTypePassword.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region Обработка нажатия клавиш в поле повтора нового пароля
        private void TbReTypePassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangePassword.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
    }
}
