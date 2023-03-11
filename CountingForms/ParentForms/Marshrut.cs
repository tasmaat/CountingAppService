using CountingDB;
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

   
    public partial class FrmMarschrut : Form
    {
        public static long id_marshrut=0;
        private DataSet dsmarsh = null;

        private DataSet dsuser1 = null;
        private DataSet dsuser2 = null;
        public string id_marsh= "" ;
        public string num_marsh = "";
        private MSDataBase dataBase;

        ///27.11.2019
        private BindingSource bindingSource;
        ///27.11.2019

        public FrmMarschrut()
        {
            InitializeComponent();
        }

        private void FrmMarschrut_Load(object sender, EventArgs e)
        {

            /*
              
            
            bindingSource = new BindingSource();
            bindingSource.DataSource = list;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

           // DataRow currentRow = bindingSource.GetCurrentDataRow();
            //  MessageBox.Show(bindingSource.Current.ToString());
            MessageBox.Show(((CWorker)bindingSource.Current).Family.ToString());
        }
    }

            */

            dataBase = new MSDataBase();
            dataBase.Connect();

            dsuser1 = dataBase.GetData9("select * from t_g_user where [deleted] is null");
            dsuser2 = dataBase.GetData9("select * from t_g_user where [deleted] is null");
            dsmarsh = dataBase.GetData9("SELECT  id ,nummarsh ,inkassator,(select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kontrol ) id_kontrol, (select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kassir ) id_kassir,kol_porsum,num_porsum,komment,kol_oplsumok  FROM t_g_marschrut t1 where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);

            dgmarsh.AutoGenerateColumns = false;

            
            dgmarsh.Columns.Add("nummarsh", "Номер маршрута");
            dgmarsh.Columns["nummarsh"].Width = 100;
            dgmarsh.Columns["nummarsh"].DataPropertyName = "nummarsh";
            dgmarsh.Columns.Add("inkassator", "Инкассатор");
            dgmarsh.Columns["inkassator"].Width = 150;
            dgmarsh.Columns["inkassator"].DataPropertyName = "inkassator";
            dgmarsh.Columns.Add("id_kontrol", "Контроллер");
            dgmarsh.Columns["id_kontrol"].Width = 150;
            dgmarsh.Columns["id_kontrol"].DataPropertyName = "id_kontrol";
            dgmarsh.Columns.Add("id_kassir", "Кассир");
            dgmarsh.Columns["id_kassir"].Width = 150;
            dgmarsh.Columns["id_kassir"].DataPropertyName = "id_kassir";

            ///26.11.2019
            dgmarsh.Columns.Add("kol_oplsumok", "Число опломбированых сумок");
            dgmarsh.Columns["kol_oplsumok"].Width = 100;
            dgmarsh.Columns["kol_oplsumok"].DataPropertyName = "kol_oplsumok";
            ///26.11.2019
            
            dgmarsh.Columns.Add("kol_porsum", "Количество порожних сумок");
            dgmarsh.Columns["kol_porsum"].Width = 100;
            dgmarsh.Columns["kol_porsum"].DataPropertyName = "kol_porsum";
            dgmarsh.Columns.Add("num_porsum", "Номера порожних сумок");
            dgmarsh.Columns["num_porsum"].Width = 100;
            dgmarsh.Columns["num_porsum"].DataPropertyName = "num_porsum";
            dgmarsh.Columns.Add("komment", "Примечание");
            dgmarsh.Columns["komment"].Width = 300;
            dgmarsh.Columns["komment"].DataPropertyName = "komment";

            /*
            /////27.11.2019
            bindingSource = new BindingSource();
            bindingSource.DataSource = dsmarsh.Tables[0];
            dgmarsh.DataSource = bindingSource;
           // dgmarsh.DataSource = dsmarsh.Tables[0];
            /////27.11.2019
            */

            dgmarsh.DataSource = dsmarsh.Tables[0];

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "user_name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = dsuser1.Tables[0];
            comboBox1.SelectedIndex = -1;

            comboBox2.Text = "";
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "user_name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = dsuser2.Tables[0];
            comboBox2.SelectedIndex = -1;

            if (id_marsh.ToString().Trim() == "")
            {
                label8.Text = "Маршрут не выбран";

            }
            else
            {

                //  DataRow[] h2 = ((DataTable)dsmarsh.Tables[0]).Select("id='" + id_marsh.ToString() + "'");
                //  string f1 = "";

                //  if (h2.Count() > 0)
                //      f1=h2[0]["nummarsh"].ToString();
                //  label8.Text = "Выбран маршрут номер: " + f1.ToString();

                label8.Text = "Выбран маршрут номер: " + num_marsh.ToString();
            }
            PrepareData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dsmarsh != null)
            {

                if (dsmarsh.Tables[0] != null)

                {
                    if (dgmarsh.CurrentCell.RowIndex > -1)
                    {
                        if (dgmarsh.CurrentCell.RowIndex< dsmarsh.Tables[0].Rows.Count)
                        id_marsh = dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["id"].ToString();
                        num_marsh = dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["nummarsh"].ToString();
                        label8.Text = "Выбран маршрут номер: " + num_marsh.ToString();

                     }

                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataSet dataSet = dataBase.GetData9("select*from t_g_marschrut where nummarsh='"+ textBox1.Text.Trim()+"'");
            int i = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                i = dataSet.Tables[0].Rows.Count;
                dataSet = dataBase.GetData9("select*from t_g_marschrut where nummarsh like'" + textBox1.Text.Replace(" ", "").Trim() + "(%'");
                i += dataSet.Tables[0].Rows.Count;
            }
                if(i>0)
            {
                //MessageBox.Show("Введите другой номер маршута, номер " + textBox1.Text+" - уже существует в базе");
                //return;
                textBox1.Text = textBox1.Text + "(" + i.ToString() + ")";
            }
            string s3 = "";
            i = 0;
            if ((comboBox1.SelectedValue != null)&(comboBox1.Text.ToString().Trim()!=""))
                s3 = comboBox1.SelectedValue.ToString();

            string s4 = "";

            if ((comboBox2.SelectedValue != null) & (comboBox2.Text.ToString().Trim() != ""))
                s4 = comboBox2.SelectedValue.ToString();
            int i2 = 0;
            if(textBox5.Text!="")
                if(Int32.TryParse(textBox5.Text.Trim(),out i2))
            {

            }
                else
            {
                MessageBox.Show("'Число порожних сумок' - должна быть целым числом!");
                return;
            }
            if(textBox2.Text!="")
            if (Int32.TryParse(textBox2.Text.Trim(), out i2))
            {

            }
            else
            {
                MessageBox.Show("'Число опломбированых сумок' - должна быть целым числом!");
                return;
            }

            ///26.11.2019
            // string s1 = "INSERT INTO t_g_marschrut(nummarsh,inkassator,id_kontrol,id_kassir,kol_porsum,num_porsum,komment)      VALUES ('" + textBox1.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + s3.ToString() + "','" + s4.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + textBox7.Text.ToString() + "')";
            string s1 = "INSERT INTO t_g_marschrut(nummarsh,inkassator,id_kontrol,id_kassir,kol_porsum,num_porsum,komment,kol_oplsumok,creation,last_update_user,lastupdate,[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current])      VALUES ('" + textBox1.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + s3.ToString() + "','" + s4.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + textBox7.Text.ToString() + "','" + textBox2.Text.ToString() + "','"+ DateTime.Now .ToString()+ "','"+ DataExchange.CurrentUser.CurrentUserId .ToString()+ "','" + DateTime.Now.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "')";
            ///26.11.2019

            int i1 = dataBase.Zapros1(s1, "");

            if (i1 > -1)
            {

                dsmarsh = dataBase.GetData9("SELECT  id ,nummarsh ,inkassator,(select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kontrol ) id_kontrol, (select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kassir ) id_kassir,kol_porsum,num_porsum,komment,kol_oplsumok  FROM t_g_marschrut t1 where id_shift_current="+DataExchange.CurrentUser.CurrentUserShift);
                dgmarsh.DataSource = dsmarsh.Tables[0];
                dgmarsh.CurrentCell = dgmarsh["nummarsh", dgmarsh.RowCount - 1];
                Dgmarsh_SelectionChanged(sender, e);
                MessageBox.Show("Операция выполнена!");
               //1111
            }
            else
                MessageBox.Show("Ошибка операции!");


        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dsmarsh != null)
            {

                if (dsmarsh.Tables[0] != null)

                {
                    if (dgmarsh.CurrentCell.RowIndex > -1)
                    {
                        if (dgmarsh.CurrentCell.RowIndex < dsmarsh.Tables[0].Rows.Count)
                        {
                            DataSet dataSet = dataBase.GetData9("  select t1.* from t_g_bags t1 left join t_g_counting t2 on t1.id = t2.id_bag where t2.deleted = 0 and t1.id_marshr = " + dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["id"].ToString());

                            if (dataSet.Tables[0].Rows.Count > 0)
                            {
                                MessageBox.Show("Невозможно удалить, маршут уже используется в подгатовках!");
                            }
                            else
                            {


                                string s1 = "delete from  t_g_marschrut where id='" + dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["id"].ToString() + "'";

                                int i1 = dataBase.Zapros1(s1, "");

                                if (i1 > -1)
                                {

                                    dsmarsh = dataBase.GetData9("SELECT  id ,nummarsh ,inkassator,(select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kontrol ) id_kontrol, (select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kassir ) id_kassir,kol_porsum,num_porsum,komment,kol_oplsumok  FROM t_g_marschrut t1 where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
                                    dgmarsh.DataSource = dsmarsh.Tables[0];

                                    MessageBox.Show("Операция выполнена!");
                                }
                                else
                                    MessageBox.Show("Ошибка операции!");
                            }

                        }
                         //   id_marsh = dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["id"].ToString();


                    }

                }

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {


            if (dsmarsh != null)
            {

                if (dsmarsh.Tables[0] != null)

                {
                    if (dgmarsh.CurrentCell.RowIndex > -1)
                    {
                        if (dgmarsh.CurrentCell.RowIndex < dsmarsh.Tables[0].Rows.Count)
                        {
                            string s3 = "";

                            if ((comboBox1.SelectedValue != null) & (comboBox1.Text.ToString().Trim() != ""))
                                s3 = comboBox1.SelectedValue.ToString();

                            string s4 = "";

                            if ((comboBox2.SelectedValue != null) & (comboBox2.Text.ToString().Trim() != ""))
                                s4 = comboBox2.SelectedValue.ToString();

                            ///26.11.2019
                           // string s1 = "update  t_g_marschrut set nummarsh='" + textBox1.Text.ToString() + "',inkassator='" + textBox4.Text.ToString() + "',id_kontrol='" + s3.ToString() + "',id_kassir='" + s4.ToString() + "',kol_porsum='" + textBox5.Text.ToString() + "',num_porsum='" + textBox6.Text.ToString() + "',komment='" + textBox7.Text.ToString() + "' where id='" + dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["id"].ToString() + "'";

                            string s1 = "update  t_g_marschrut set nummarsh='" + textBox1.Text.ToString() + "',inkassator='" + textBox4.Text.ToString() + "',id_kontrol='" + s3.ToString() + "',id_kassir='" + s4.ToString() + "',kol_porsum='" + textBox5.Text.ToString() + "',num_porsum='" + textBox6.Text.ToString() + "',komment='" + textBox7.Text.ToString() + "',kol_oplsumok='" + textBox2.Text.ToString() + "', last_update_user='"+ DataExchange.CurrentUser.CurrentUserId.ToString() + "'" +
                                ", lastupdate='"+ DateTime.Now.ToString() + "' where id='" + dsmarsh.Tables[0].Rows[dgmarsh.CurrentCell.RowIndex]["id"].ToString() + "'";

                            ///26.11.2019

                            int i1 = dataBase.Zapros1(s1, "");

                            if (i1 > -1)
                            {
                                int rowIndex = dgmarsh.CurrentCell.RowIndex;

                                dsmarsh = dataBase.GetData9("SELECT  id ,nummarsh ,inkassator,(select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kontrol ) id_kontrol, (select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kassir ) id_kassir,kol_porsum,num_porsum,komment,kol_oplsumok  FROM t_g_marschrut t1 where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
                                dgmarsh.DataSource = dsmarsh.Tables[0];

                                dgmarsh.CurrentCell = dgmarsh["nummarsh", rowIndex];
                                Dgmarsh_SelectionChanged(sender, e);
                                MessageBox.Show("Операция выполнена!");
                            }
                            else
                                MessageBox.Show("Ошибка операции!");
                        }

                        }
                        


                    }

                }


            }
        private void PrepareData()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.SelectedValue = DataExchange.CurrentUser.CurrentUserId;
            comboBox2.SelectedValue = DataExchange.CurrentUser.CurrentUserId;

        }
        private void Dgmarsh_SelectionChanged(object sender, EventArgs e)
        {
            if (dgmarsh.CurrentCell != null)
            {
                if (dgmarsh.CurrentRow.Index < dsmarsh.Tables[0].Rows.Count)
                {
                    textBox1.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["nummarsh"].ToString().Trim();
                    ///26.11.2019
                    textBox2.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["kol_oplsumok"].ToString().Trim();
                    ///26.11.2019
                    textBox4.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["inkassator"].ToString().Trim();
                    textBox5.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["kol_porsum"].ToString().Trim();
                    textBox6.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["num_porsum"].ToString().Trim();
                    textBox7.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["komment"].ToString().Trim();
                    // if (dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["id_kontrol"]!=null)
                    comboBox1.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["id_kontrol"].ToString().Trim();
                    //  if (dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["id_kassir"] != null)
                    comboBox2.Text = dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["id_kassir"].ToString().Trim();
                    //MessageBox.Show("id="+dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["id"].ToString());
                    id_marshrut = Convert.ToInt64(dsmarsh.Tables[0].Rows[dgmarsh.CurrentRow.Index]["id"]);
                }

                /*
                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);

                tbCode.Text = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["curr_code"].ToString().Trim();
                tbCurRate.Text = base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["rate"].ToString().Trim();
                cbBlockCur.Checked = Convert.ToBoolean(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["locked"]);
                */
            }
        }

        private void FrmMarschrut_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmMarschrut_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            /*
            if (id_marsh.ToString().Trim() == "")
            {
                if (MessageBox.Show("Не выбран маршрут. Закрыть форму?", "Маршрут",
            MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                    // Call method to save file...
                }
            }
            */
        }

        private void Button5_Click(object sender, EventArgs e)
        {
          //  bindingSource.
            MessageBox.Show(((DataRowView)bindingSource.Current)["id"].ToString());
        }
    }
}
