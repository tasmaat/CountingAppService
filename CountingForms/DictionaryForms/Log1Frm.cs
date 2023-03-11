using CountingDB;
using Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace CountingForms.DictionaryForms
{
    public partial class Log1Frm : Form
    {


        private MSDataBase countingDB = new MSDataBase();

        
        DataSet dsUsers = null;

        private HashCoding hashCoding = new HashCoding();

        private long idpol = 0;

        public long idpercht = 0;

        public Log1Frm()
        {
            InitializeComponent();
            countingDB.Connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidateUser();
            /*
            DvigsrFrm dv1 = new DvigsrFrm();
            dv1.Show();
            this.Close();
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateUser()
        {
            if (textBox1.Text.ToString() != String.Empty)
            {
                string hashDBPassword = GetDBPassword("t_g_user", "code", textBox1.Text.ToString());

                if (hashDBPassword == null)
                    return;

                string hashUserPassword = hashCoding.ComputeHash(textBox2.Text);
                string hashDBPasswordBytes = BitConverter.ToString(Encoding.UTF8.GetBytes(hashDBPassword));


                if (hashDBPassword != hashUserPassword)
                {
                    MessageBox.Show("Введенные пользователь или пароль неверные.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                else
                {
                    
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

                        }
                    }

                    d1 = countingDB.GetData9("SELECT *  FROM db_owner.t_g_pols_ip where ippols='" + IP.ToString() + "'");

                    if (d1 != null)
                    {

                        if (d1.Tables[0] != null)
                        {
                            if (d1.Tables[0].Rows.Count > 0)
                                countingDB.Zapros("update db_owner.t_g_pols_ip set datevh1=GETDATE(), id_pols =" + dsUsers.Tables[0].Rows[0]["id"].ToString() + "  where ippols='" + IP.ToString() + "'", "");
                            else
                                countingDB.Zapros("insert into db_owner.t_g_pols_ip(id_pols,ippols,datevh1) values(" + dsUsers.Tables[0].Rows[0]["id"].ToString() + ",'" + IP.ToString() + "',GETDATE()) ", "");


                        }
                        else
                            countingDB.Zapros("insert into db_owner.t_g_pols_ip(id_pols,ippols,datevh1) values(" + dsUsers.Tables[0].Rows[0]["id"].ToString() + ",'" + IP.ToString() + "',GETDATE()) ", "");


                    }
                    else
                        countingDB.Zapros("insert into db_owner.t_g_pols_ip(id_pols,ippols,datevh1) values(" + dsUsers.Tables[0].Rows[0]["id"].ToString() + ",'" + IP.ToString() + "',GETDATE()) ", "");

                    ///////14.02.2020



                    DvigsrFrm dv1 = new DvigsrFrm();
                    dv1.idpols = idpol;
                    dv1.idperch = idpercht;
                    dv1.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите пользователя для входа в систему.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void GetCurrentUserDB(string dataTable)
        {
            

            if (dsUsers != null && dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
            {

                idpol = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id"]);
                /*
                DataExchange.CurrentUser.CurrentUserId = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id"]);
                DataExchange.CurrentUser.CurrentUserName = dsUsers.Tables[0].Rows[0]["user_name"].ToString();
                DataExchange.CurrentUser.CurrentUserCode = dsUsers.Tables[0].Rows[0]["code"].ToString();
                DataExchange.CurrentUser.CurrentUserRole = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id_role"]);
                DataExchange.CurrentUser.CurrentUserCentre = Convert.ToInt64(dsUsers.Tables[0].Rows[0]["id_brach"]);
                */

            }
            else
            {
                MessageBox.Show("Введеного пользователя не существует.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }
        }

        private string GetDBPassword(string dataTable, string field, string param)
        {
            
            dsUsers = countingDB.GetData(dataTable, field, param);
            
            if (dsUsers != null && dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
            {
                return dsUsers.Tables[0].Rows[0]["password"].ToString();
            }
            else
            {
                MessageBox.Show("Введеного пользователя не существует.", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        private void Log1Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            countingDB.Disconnect();
        }

        private void Log1Frm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
