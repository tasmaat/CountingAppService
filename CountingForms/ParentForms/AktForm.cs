
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
    public partial class AktForm : Form
    {

        private DataSet dsakt = null;
        private DataSet dsprich1 = null;
        private DataSet dsprich2 = null;
        private DataSet dan1 = null;
        private DataSet dan2 = null;
        private DataSet dan3 = null;
        /*
        private DataSet dsuser1 = null;
        private DataSet dsuser2 = null;
        public string id_marsh = "";
        public string num_marsh = "";
        */
        private MSDataBase dataBase;

        
        private BindingSource bindingSource;
       
        public AktForm()
        {
            InitializeComponent();
        }

        private void AktForm_Load(object sender, EventArgs e)
        {


            dataBase = new MSDataBase();
            dataBase.Connect();

            //dsprich1 = dataBase.GetData9("select * from t_g_nastr_form t1 where num_form=2");
            //dsakt = dataBase.GetData9("select * from (select t2.id,t1.name_compon prich,t2.numakt numakt1,t2.dan1,t2.dan2,t2.dan3,t1.value_compon,t1.num_compon,t1.name_pole,t1.name_grup from t_g_nastr_form t1 inner join t_g_akt t2 on ((cast(t1.value_compon as int)&t2.dan1)=t1.value_compon) where t1.num_form=2 and t1.name_pole='dan1' union all select t2.id, t1.name_compon, t2.numakt,t2.dan1,t2.dan2,t2.dan3,t1.value_compon,t1.num_compon,t1.name_pole,t1.name_grup from t_g_nastr_form t1 inner join t_g_akt t2 on ((cast(t1.value_compon as int) & t2.dan2) = t1.value_compon) where t1.num_form = 2 and t1.name_pole = 'dan2' union all select t2.id, t1.name_compon, t2.numakt,t2.dan1,t2.dan2,t2.dan3,t1.value_compon,t1.num_compon,t1.name_pole,t1.name_grup from t_g_nastr_form t1 inner join t_g_akt t2 on ((cast(t1.value_compon as int) & t2.dan3) = t1.value_compon) where t1.num_form = 2 and t1.name_pole = 'dan3') t order by numakt1,name_grup");
            dsakt = dataBase.GetData9("SELECT t1.[id],[dan1], t2.[name] d1name,[dan2], t3.[name] d2name, [dan3], t4.[name] d3name,[creation],[last_update_user],[lastupdate],[numakt],[notes] " +
                "FROM[CountingDB].[dbo].[t_g_akt] t1 left join t_g_sprav t2 on t1.dan1 = t2.id left join t_g_sprav t3 on t1.dan2 = t3.id left join t_g_sprav t4 on t1.dan3 = t4.id  " +
                "where id_shift_current =" + DataExchange.CurrentUser.CurrentUserShift);
            

            dgakt.AutoGenerateColumns = false;

            dgakt.Columns.Add("numakt", "Номер акта");
            dgakt.Columns["numakt"].DataPropertyName = "numakt";
            dgakt.Columns["numakt"].Width = 60;
            
            dgakt.Columns.Add("d1name", "Вид повреждении");
            dgakt.Columns["d1name"].DataPropertyName = "d1name";
            dgakt.Columns["d1name"].Width = 100;

            dgakt.Columns.Add("d2name", "Четкость оттисков");
            dgakt.Columns["d2name"].DataPropertyName = "d2name";
            dgakt.Columns["d2name"].Width = 100;

            dgakt.Columns.Add("d3name", "Данные о соответствии");
            dgakt.Columns["d3name"].DataPropertyName = "d3name";
            dgakt.Columns["d3name"].Width = 160;

            dgakt.Columns.Add("notes", "Примечание");
            dgakt.Columns["notes"].DataPropertyName = "notes";
            dgakt.Columns["notes"].Width = 200;

            dgakt.Columns.Add("dan1", "dan1");
            dgakt.Columns["dan1"].DataPropertyName = "dan1";
            dgakt.Columns["dan1"].Visible = false;

            dgakt.Columns.Add("dan2", "dan2");
            dgakt.Columns["dan2"].DataPropertyName = "dan2";
            dgakt.Columns["dan2"].Visible = false;

            dgakt.Columns.Add("dan3", "dan3");
            dgakt.Columns["dan3"].DataPropertyName = "dan3";
            dgakt.Columns["dan3"].Visible = false;

            dgakt.Columns.Add("id", "id");
            dgakt.Columns["id"].DataPropertyName = "id";
            dgakt.Columns["id"].Visible = false;



            //bindingSource = new BindingSource();
            //bindingSource.DataSource = dsakt.Tables[0];
            //dgakt.DataSource = bindingSource;
            dgakt.DataSource = dsakt.Tables[0];
            dgakt.RowHeadersWidth = 20;

            //int i1 = 1;
            //int i2 = 50;
            //int i3 = 10-190;
            //string s1 = "";

            //foreach (DataRow row in dsprich1.Tables[0].Rows)
            //{

            //    if (s1.ToString() != row["name_grup"].ToString())
            //    {
            //        i3 = i3 + 200;
            //        i2 = 50;
            //        i2 = i2 + 20;
            //        Label l = new Label();
            //        l.Text = row["name_grup"].ToString();
            //        l.Location = new Point(i3 + 20, i2);
            //        l.Name = "Label" + i1.ToString();
            //        l.AutoSize = true;
            //        panel1.Controls.Add(l);
            //        i2 = i2 + 20;


            //    }

            //    CheckBox rb = new CheckBox();
            //    rb.Text = row["name_compon"].ToString();
            //    rb.Location = new Point(i3, i2);
            //    rb.Name = "CheckBox" + row["num_compon"].ToString();
            //    rb.AutoSize = true;
            //    panel1.Controls.Add(rb);

            //    i1 = i1 + 1;
            //    i2 = i2 + 20;
            //    s1 = row["name_grup"].ToString();

            //    Dgakt_SelectionChanged(sender, e);

                
            //}

            dan1 = dataBase.GetData9("select * from t_g_sprav where id_parent=1");

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = dan1.Tables[0];
            comboBox1.SelectedIndex = -1;

            dan2 = dataBase.GetData9("select * from t_g_sprav where id_parent=2");

            comboBox2.Text = "";
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = dan2.Tables[0];
            comboBox2.SelectedIndex = -1;


            dan3 = dataBase.GetData9("select * from t_g_sprav where id_parent=3");

            comboBox3.Text = "";
            comboBox3.DataSource = null;
            comboBox3.Items.Clear();
            comboBox3.DisplayMember = "name";
            comboBox3.ValueMember = "id";
            comboBox3.DataSource = dan3.Tables[0];
            comboBox3.SelectedIndex = -1;

            btnClear_Click(sender,e);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Введтие номер акта!");
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите вид повреждения!");
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите четкость оттисков!");
                return;
            }
            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите данные о соответствии!");
                return;
            }

            DataSet dataSet = dataBase.GetData9("Select * from [dbo].[t_g_akt] where [numakt]='"+textBox1.Text.Trim()+"'");
            if(dataSet.Tables[0].Rows.Count>0)
            {
                MessageBox.Show("Введите другое номер акта, " + textBox1.Text.Trim() + " номер акта уже используется!");
                return;
            }

            string s1 = "INSERT INTO [dbo].[t_g_akt] " +
             "([dan1],[dan2],[dan3],[creation],[last_update_user],[lastupdate],[numakt],[notes],id_shift_create, id_shift_current)" +
            "VALUES(" +
            ""+comboBox1.SelectedValue+","+comboBox2.SelectedValue+","+comboBox3.SelectedValue+",GETDATE(),"+DataExchange.CurrentUser.CurrentUserId.ToString()+",GETDATE()," +
            ""+textBox1.Text.Trim()+",'"+tbNotes.Text+"',"+DataExchange.CurrentUser.CurrentUserShift+ "," + DataExchange.CurrentUser.CurrentUserShift + ")";
           // DataSet dataSet = dataBase.GetData9(s1);
            int i1 = dataBase.Zapros1(s1, "");

            
            updateList();
            btnClear_Click(sender, e);


        }
        private void vcel1()
        {
            if(dgakt.Rows.Count>0)
            {
                textBox1.Text = dgakt.CurrentRow.Cells["numakt"].Value.ToString().Trim();
                comboBox1.SelectedValue = dgakt.CurrentRow.Cells["dan1"].Value;
                comboBox2.SelectedValue = dgakt.CurrentRow.Cells["dan2"].Value;
                comboBox3.SelectedValue = dgakt.CurrentRow.Cells["dan3"].Value;
                tbNotes.Text = dgakt.CurrentRow.Cells["notes"].Value.ToString().Trim();

            }

        }
        private void Dgakt_SelectionChanged(object sender, EventArgs e)
        {
            vcel1();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int i1=-1;
            if (dgakt.Rows.Count > 0)
            {
                string s1 = "delete from  t_g_akt where id='" + dgakt.CurrentRow.Cells["id"].Value + "'";

               i1 = dataBase.Zapros1(s1, "");

            }
            
            if (i1 > -1)
            {
                updateList();
                btnClear_Click(sender, e);
                MessageBox.Show("Операция выполнена!");
            }
            else
                MessageBox.Show("Ошибка операции!");
            btnClear_Click(sender, e);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Введтие номер акта!");
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите вид повреждения!");
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите четкость оттисков!");
                return;
            }
            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите данные о соответствии!");
                return;
            }

            DataSet dataSet = dataBase.GetData9("Select * from [dbo].[t_g_akt] where [numakt]='" + textBox1.Text.Trim() + "' and id<>"+dgakt.CurrentRow.Cells["id"].Value);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("Введите другое номер акта, " + textBox1.Text.Trim() + " номер акта уже используется!");
                return;
            }

            string s1 = "UPDATE [dbo].[t_g_akt] " +
             "SET [dan1]=" + comboBox1.SelectedValue + ",[dan2]=" + comboBox2.SelectedValue + ",[dan3]=" + comboBox3.SelectedValue + ",[last_update_user]=" + DataExchange.CurrentUser.CurrentUserId.ToString() + "," +
             "[lastupdate]=GETDATE(),[numakt]=" + textBox1.Text.Trim() + ",[notes]='" + tbNotes.Text + "' " +
             "WHERE id="+dgakt.CurrentRow.Cells["id"].Value;
            // DataSet dataSet = dataBase.GetData9(s1);
            int i1 = dataBase.Zapros1(s1, "");
            updateList();
            btnClear_Click(sender,e);
           
        }

        private void updateList() 
        {
            dsakt = dataBase.GetData9("SELECT t1.[id],[dan1], t2.[name] d1name,[dan2], t3.[name] d2name, [dan3], t4.[name] d3name,[creation],[last_update_user],[lastupdate],[numakt],[notes] " +
                "FROM[CountingDB].[dbo].[t_g_akt] t1 left join t_g_sprav t2 on t1.dan1 = t2.id left join t_g_sprav t3 on t1.dan2 = t3.id left join t_g_sprav t4 on t1.dan3 = t4.id " +
                "where id_shift_current ="+DataExchange.CurrentUser.CurrentUserShift);
            dgakt.DataSource = dsakt.Tables[0];


        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            tbNotes.Text = "";
        }

        private void dgakt_Click(object sender, EventArgs e)
        {
            vcel1();
        }
    }
}
