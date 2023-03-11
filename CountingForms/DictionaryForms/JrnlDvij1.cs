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
    public partial class JrnlDvij1 : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = new MSDataBase();

        private DataSet D1 = null;
        private DataSet D2 = null;

        private DataSet D3 = null;
        private DataSet D4 = null;

        private BindingSource bindingSource;

        public JrnlDvij1()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void JrnlDvij1_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            dataBase.Connect();
            bindingSource = new BindingSource();

            D1 = dataBase.GetData9("select * from t_g_user");
            D2 = dataBase.GetData9("select * from t_g_user");

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "user_name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = D1.Tables[0];
            comboBox1.SelectedIndex = -1;


            comboBox2.Text = "";
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "user_name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = D2.Tables[0];
            comboBox2.SelectedIndex = -1;

            dataGridView1.Columns.Clear();


            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("name1", "Наименование");
            dataGridView1.Columns["name1"].Visible = true;
            dataGridView1.Columns["name1"].ReadOnly = true;
            dataGridView1.Columns["name1"].Width = 60;
            dataGridView1.Columns["name1"].DataPropertyName = "name1";
            
            dataGridView1.Columns.Add("Kol", "Количество");
            dataGridView1.Columns["Kol"].Visible = true;
            dataGridView1.Columns["Kol"].ReadOnly = true;
            dataGridView1.Columns["Kol"].Width = 100;
            dataGridView1.Columns["Kol"].DataPropertyName = "Kol";

            dataGridView1.Columns.Add("Sum1", "Сумма");
            dataGridView1.Columns["Sum1"].Visible = true;
            dataGridView1.Columns["Sum1"].ReadOnly = true;
            dataGridView1.Columns["Sum1"].Width = 100;
            dataGridView1.Columns["Sum1"].DataPropertyName = "Sum1";

            dataGridView1.Columns.Add("Usero", "Пользователь от кого");
            dataGridView1.Columns["Usero"].Visible = true;
            dataGridView1.Columns["Usero"].ReadOnly = true;
            dataGridView1.Columns["Usero"].Width = 100;
            dataGridView1.Columns["Usero"].DataPropertyName = "Usero";

            dataGridView1.Columns.Add("Userk", "Пользователь кому");
            dataGridView1.Columns["Userk"].Visible = true;
            dataGridView1.Columns["Userk"].ReadOnly = true;
            dataGridView1.Columns["Userk"].Width = 100;
            dataGridView1.Columns["Userk"].DataPropertyName = "Userk";

            dataGridView1.Columns.Add("Stat1", "Статус");
            dataGridView1.Columns["Stat1"].Visible = true;
            dataGridView1.Columns["Stat1"].ReadOnly = true;
            dataGridView1.Columns["Stat1"].Width = 100;
            dataGridView1.Columns["Stat1"].DataPropertyName = "Stat1";

            dataGridView1.Columns.Add("Dat", "Дата создания");
            dataGridView1.Columns["Dat"].Visible = true;
            dataGridView1.Columns["Dat"].ReadOnly = true;
            dataGridView1.Columns["Dat"].Width = 100;
            dataGridView1.Columns["Dat"].DataPropertyName = "Dat";
      
            dataGridView1.Columns.Add("Dat1", "Дата изменения");
            dataGridView1.Columns["Dat1"].Visible = true;
            dataGridView1.Columns["Dat1"].ReadOnly = true;
            dataGridView1.Columns["Dat1"].Width = 100;
            dataGridView1.Columns["Dat1"].DataPropertyName = "Dat1";

            dataGridView1.Columns.Add("Tip1", "Тип");
            dataGridView1.Columns["Tip1"].Visible = true;
            dataGridView1.Columns["Tip1"].ReadOnly = true;
            dataGridView1.Columns["Tip1"].Width = 100;
            dataGridView1.Columns["Tip1"].DataPropertyName = "Tip1";

            dataGridView1.Columns.Add("Sost1", "Состояние");
            dataGridView1.Columns["Sost1"].Visible = true;
            dataGridView1.Columns["Sost1"].ReadOnly = true;
            dataGridView1.Columns["Sost1"].Width = 100;
            dataGridView1.Columns["Sost1"].DataPropertyName = "Sost1";

            dataGridView1.Columns.Add("Komm1", "Комментарий");
            dataGridView1.Columns["Komm1"].Visible = true;
            dataGridView1.Columns["Komm1"].ReadOnly = true;
            dataGridView1.Columns["Komm1"].Width = 100;
            dataGridView1.Columns["Komm1"].DataPropertyName = "Komm1";


            dataGridView1.AutoResizeColumns();

            dataGridView2.Columns.Clear();
            dataGridView2.AutoGenerateColumns = false;

            dataGridView2.Columns.Add("nomin1", "Номинал");
            dataGridView2.Columns["nomin1"].Visible = true;
            dataGridView2.Columns["nomin1"].ReadOnly = true;
            dataGridView2.Columns["nomin1"].Width = 60;
            dataGridView2.Columns["nomin1"].DataPropertyName = "nomin1";

            dataGridView2.Columns.Add("Kol1", "Количество");
            dataGridView2.Columns["Kol1"].Visible = true;
            dataGridView2.Columns["Kol1"].ReadOnly = true;
            dataGridView2.Columns["Kol1"].Width = 60;
            dataGridView2.Columns["Kol1"].DataPropertyName = "Kol1";

            dataGridView2.Columns.Add("Sum1", "Сумма");
            dataGridView2.Columns["Sum1"].Visible = true;
            dataGridView2.Columns["Sum1"].ReadOnly = true;
            dataGridView2.Columns["Sum1"].Width = 60;
            dataGridView2.Columns["Sum1"].DataPropertyName = "Sum1";

            dataGridView2.Columns.Add("Sost1", "Состояние");
            dataGridView2.Columns["Sost1"].Visible = true;
            dataGridView2.Columns["Sost1"].ReadOnly = true;
            dataGridView2.Columns["Sost1"].Width = 60;
            dataGridView2.Columns["Sost1"].DataPropertyName = "Sost1";

            //////19.02.2020
            
            dataGridView1.Columns["Kol"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Sum1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Kol1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Sum1"].DefaultCellStyle.Format = "### ### ### ###";

            //////19.02.2020






        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker2.Value.ToString("dd.MM.yyyy");
        }

        private void vub()
        {

           // string sql1 = "select t2.name as Perch, t3.name as Nomin, t4.name as Sost, t1.creation as Dat, t1.count as Kol, t5.name as Kart, t7.user_name as Usero, t6.user_name as Userk from t_g_dvdeneg_jrn t1 inner join t_g_counting t2 on (t1.id_counting = t2.id) inner join t_g_denomination t3 on (t1.id_denomination = t3.id) inner join t_g_condition t4 on (t1.id_condition = t4.id) inner join t_g_cards t5 on (t1.id_card = t5.id) inner join t_g_user t6 on (t1.user_komy = t6.id)  inner join t_g_user t7 on (t1.user_otkogo = t7.id) {0}  order by t1.creation";

            string sql1 = "select t1.tipper,t1.user_otkogo,t1.user_komu,t1.id,t1.name as name1,t1.count as Kol,t1.fact_value as Sum1,t2.user_name as Usero, t3.user_name as Userk,(case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as Stat1,t1.createdate as Dat,t1.izmenstatdate as Dat1,(case when t1.tipper=1 then 'Наличность' when t1.tipper=2 then 'Сумки' end) as Tip1,(case when t1.vidsost=1 then 'Используется' else 'Не используется' end) as Sost1,t1.comment as Komm1 from t_g_pered t1 inner join t_g_user t2 on (t1.user_otkogo=t2.id) left join t_g_user t3 on (t1.user_komu=t3.id) {0}  order by t1.createdate";
            string s1 = "";

            if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
               
                s1 = " where t1.user_otkogo =" + comboBox1.SelectedValue.ToString();

            if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
            {
                if (s1.ToString().Trim() == "")
                    
                    s1 = " where t1.user_komu =" + comboBox2.SelectedValue.ToString();
                else
                   
                    s1 = s1.ToString() + " and  t1.user_komu =" + comboBox2.SelectedValue.ToString();

            }


            if ((textBox1.Text.ToString().Trim() != "") & (textBox2.Text.ToString().Trim() != ""))
            {

                
                DateTime dat1;
                DateTime dat2;

                try
                {
                    dat1 = DateTime.Parse(textBox1.Text.ToString().Trim());

                    dat2 = DateTime.Parse(textBox2.Text.ToString().Trim());
                }
                catch
                {
                    MessageBox.Show("Неправильно заданы даты!");
                    return;
                }

                

                if (s1.ToString().Trim() == "")
                     s1 = " where cast(t1.createdate as date)  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";

                else
                     s1 = s1.ToString() + " and cast(t1.createdate as date)  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";



            }


            sql1 = String.Format(sql1, s1);
            D3 = dataBase.GetData9(sql1);
            bindingSource.DataSource = D3.Tables[0];
            dataGridView1.DataSource = bindingSource;
            //dataGridView1.DataSource = D3.Tables[0];
            dataGridView1.AutoResizeColumns();

            if ((DataRowView)bindingSource.Current != null)
            {
                D4 = dataBase.GetData9("select t2.tipper, (case when t2.tipper = 1 then(select max(t4.name) from t_g_denomination t4 where t4.id = t1.id_detal) when t2.tipper = 2 then(select max(t5.name) from t_g_bags t5 where t5.id = t1.id_detal) end) as nomin1,t1.count as Kol1,t1.fact_value as Sum1,(case when t1.id_cond = 0 then 'Не используется' else (select  max(t3.name) from t_g_condition t3 where t3.id = t1.id_cond) end) as Sost1 from t_g_pered_detal t1 inner join t_g_pered t2 on (t1.id_pered = t2.id) where t2.id = " + ((DataRowView)bindingSource.Current)["id"].ToString());

                dataGridView2.DataSource = D4.Tables[0];
            }
            dataGridView2.AutoResizeColumns();
                                                                                                                                                                                                                                                                                                                                                                                                  

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            

            textBox1.Text = "";
            textBox2.Text = "";

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vub();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataRowView)bindingSource.Current)["id"].ToString() != "")
            {
                D4 = dataBase.GetData9("select t2.tipper, (case when t2.tipper = 1 then(select max(t4.name) from t_g_denomination t4 where t4.id = t1.id_detal) when t2.tipper = 2 then(select max(t5.name) from t_g_bags t5 where t5.id = t1.id_detal) end) as nomin1,t1.count as Kol1,t1.fact_value as Sum1,(case when t1.id_cond = 0 then 'Не используется' else (select  max(t3.name) from t_g_condition t3 where t3.id = t1.id_cond) end) as Sost1 from t_g_pered_detal t1 inner join t_g_pered t2 on (t1.id_pered = t2.id) where t2.id = " + ((DataRowView)bindingSource.Current)["id"].ToString());

                dataGridView2.DataSource = D4.Tables[0];
                dataGridView2.AutoResizeColumns();

                if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "1")
                    dataGridView2.Columns["nomin1"].HeaderText = "Номинал";
                else
                    dataGridView2.Columns["nomin1"].HeaderText = "Сумка";
            }
        }
    }
}
