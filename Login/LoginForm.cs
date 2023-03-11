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
using System.Net;
using System.Net.Sockets;

namespace Login
{



    public partial class LoginForm : Form
    {


        private HashCoding hashCoding = new HashCoding();

        /*
        private struct CurrentUser {

            public CurrentUser(CurrentUser user)
            {
                this = user;
            }

            public CurrentUser(int id, string name, string code, int role):this()
            {
                currentUserid = id;
                currentUserName = name;
                currentUserCode = code;
                currentUserRole = role;
            }
            private int currentUserid;
            private string currentUserName;
            private string currentUserCode;
            private int currentUserRole;

            public int CurrentUserId { get; }
            public string CurrentUserName { get; }
            public string CurrentUserCode { get; }
            public int CurrentUserRole { get; }
        } 

        */
        
        //Свойства валидности пароля и пользователя
        public static bool Login { set; get; }
        public static bool admin { set; get; }

        private MSDataBase countingDB = new MSDataBase();


        ////06.01.2020
        DataSet dsUsers = null;
        DataSet dsShift = null;
        ////06.01.2020

        public LoginForm()
        {
            InitializeComponent();
            Login = false;
            admin = false;
            countingDB.Connect();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ValidateUser();
            this.Close();
        }

        /// <summary>
        /// Проверка имени пользователя и пароля
        /// </summary>
        private void ValidateUser()
        {
            if (tbUser.Text.ToString() != String.Empty)
            {
                string hashDBPassword = GetDBPassword("t_g_user", "code", tbUser.Text.ToString());

                /////27.01.2020
                if (hashDBPassword == null)
                    return;
                /////27.01.2020

                string hashUserPassword = hashCoding.ComputeHash(tbPassword.Text);
                string hashDBPasswordBytes = BitConverter.ToString(Encoding.UTF8.GetBytes(hashDBPassword));


                if (hashDBPassword != hashUserPassword)
                {
                    MessageBox.Show("Введенные пользователь или пароль неверные.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Login = false;
                }
                else
                {
                    Login = true;
                    GetCurrentUserDB("t_g_user");

                    ///////14.02.2020
                    string IP = "";
                    DataSet d1 = null;

                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IP = ip.ToString();
                            
                            //MessageBox.Show(IP);
                            break;
                        }
                       // else IP = "localhost";
                    }
                   // MessageBox.Show(IP);
                    d1 = countingDB.GetData9("SELECT *  FROM t_g_pols_ip where ippols='" + IP.ToString() + "'");

                    if (d1 != null)
                    {

                        if (d1.Tables[0] != null)
                        {
                            if (d1.Tables[0].Rows.Count > 0)
                                countingDB.Zapros("update t_g_pols_ip set datevh1=GETDATE(), id_pols =" + dsUsers.Tables[0].Rows[0]["id"].ToString()+"  where ippols='" + IP.ToString() + "'" , "");
                            else
                                countingDB.Zapros("insert into t_g_pols_ip(id_pols,ippols,datevh1) values(" + dsUsers.Tables[0].Rows[0]["id"].ToString()+",'" + IP.ToString() + "',GETDATE()) " , "");


                        }
                        else
                            countingDB.Zapros("insert into t_g_pols_ip(id_pols,ippols,datevh1) values(" + dsUsers.Tables[0].Rows[0]["id"].ToString() + ",'" + IP.ToString() + "',GETDATE()) " , "");


                    }
                    else
                        countingDB.Zapros("insert into t_g_pols_ip(id_pols,ippols,datevh1) values(" + dsUsers.Tables[0].Rows[0]["id"].ToString() + ",'" + IP.ToString() + "',GETDATE()) " , "");

                    ///////14.02.2020

                    //17.09.2020
                    //нужно добавить проверку на смену
                    checkShift();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите пользователя для входа в систему.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkShift()

        {
            if (DataExchange.CurrentUser.CurrentUserRole != 0)
            {
                dsShift = countingDB.GetData9("select * from t_g_shift_user t1 left join t_g_shift t2 on t1.id_shift=t2.id where status=0 and id_user=" + DataExchange.CurrentUser.CurrentUserId.ToString());
                if (dsShift.Tables[0].Rows.Count > 0)
                {
                    //MessageBox.Show("Вас включили список смены");

                    DataExchange.CurrentUser.CurrentUserZona = Convert.ToInt64(dsShift.Tables[0].Rows[0]["id_zona"]);
                    DataExchange.CurrentUser.CurrentUserShift = Convert.ToInt64(dsShift.Tables[0].Rows[0]["id_shift"]);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Вас нету в списке смены");
                    if (DataExchange.CurrentUser.CurrentUserRole == 3)
                    {
                        MessageBox.Show("Вы должны включить себя в смену");
                        admin = true;
                    }
                    else
                    {
                        MessageBox.Show("У вас нет полномочий для открытия смены!");
                        Login = false;
                        this.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Запрос данных о текущем пользователе из БД
        /// </summary>
        /// <param name="dataTable">Таблица в БД</param>
        /// <returns></returns>
        private void GetCurrentUserDB(string dataTable)
        {
            ////06.01.2020
           // DataSet dsUsers = countingDB.GetData(dataTable);
            ////06.01.2020

            if (dsUsers != null && dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
            {
                
                /*int id = Convert.ToInt32(dsUsers.Tables[0].Rows[0]["id"]);
                string name = dsUsers.Tables[0].Rows[0]["user_name"].ToString();
                string code = dsUsers.Tables[0].Rows[0]["code"].ToString();
                int role = Convert.ToInt32(dsUsers.Tables[0].Rows[0]["id_role"]);
                */
                DataExchange.CurrentUser.CurrentUserId = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id"]);
                DataExchange.CurrentUser.CurrentUserName = dsUsers.Tables[0].Rows[0]["user_name"].ToString();
                DataExchange.CurrentUser.CurrentUserCode = dsUsers.Tables[0].Rows[0]["code"].ToString();
                DataExchange.CurrentUser.CurrentUserRole = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id_role"]);
                DataExchange.CurrentUser.CurrentUserCentre = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id_brach"]);
                
            }
            else
            {
                MessageBox.Show("Введеного пользователя не существует.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return new CurrentUser();
            }
        }

        /// <summary>
        /// Запрос hash-пароля из базы данных
        /// </summary>
        /// <param name="dataTable">Имя таблицы в БД</param>
        /// <param name="field">Поле для условия выборки из БД</param>
        /// <param name="param">Значение поля в таблице</param>
        /// <returns></returns>
        private string GetDBPassword(string dataTable, string field, string param)
        {
            ////06.01.2020
            //  DataSet dsUsers = countingDB.GetData(dataTable, field, param);
            dsUsers = countingDB.GetData(dataTable, field, param);
            
            ////06.01.2020
            if (dsUsers != null && dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
            {
                return dsUsers.Tables[0].Rows[0]["password"].ToString();
            }
            else
            {
                ////27.01.2020
                MessageBox.Show("Введеного пользователя не существует.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ////27.01.2020
                return null;
            }
        }


        

        #region Обработка клавиш на форме
        private void tbUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbPassword.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void LoginForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            countingDB.Disconnect();
        }
    }
}
