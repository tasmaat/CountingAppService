using CountingDB;
using Microsoft.VisualBasic;
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
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;

namespace CountingForms.DictionaryForms
{
    public partial class DvigsrFrm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        public long idpols ;

        // public long idperch= 10322;
        //public long idperch = 10327;
        public long idperch = 0;

        public int itip=0;

        private MSDataBase dataBase = new MSDataBase();

        private DataSet D1 = null;
        private DataSet D2 = null;
        private DataSet D3 = null;
        private DataSet D4 = null;
        private DataSet D5 = null;
        private DataSet D6 = null;
        private DataSet D7 = null;

        private DataSet D8 = null;
        private DataSet D9 = null;

        private DataSet D10 = null;
        private DataSet D11 = null;

        private DataSet D12 = null;
        private DataSet D13 = null;

        private DataSet D14 = null;


        private BindingSource bindingSource;

        private int flprosm = 0;

        private string IP = "";

        public DvigsrFrm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void DvigsrFrm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
            //  idpols = 10026;

            //  string Host1 = System.Net.Dns.GetHostName();
            //  string IP = System.Net.Dns.GetHostByName(Host1).AddressList[0].ToString();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP= ip.ToString();
                    //return ip.ToString();
                }
            }

            dataBase.Connect();

            D2 = dataBase.GetData9("select * from t_g_user where id <>"+ idpols.ToString());

            /// 13.02.2020
            // D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString());
            D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM db_owner.t_g_pered t1 where t1.user_komu="+ idpols.ToString()+ " and (t1.zone1=0 or t1.zone1 is null) union all SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM db_owner.t_g_pered t1 where t1.zone1=1 and exists(select 1 from t_g_cashcentre t22 where t22.tipsp1=2 and t22.id=t1.user_komu and exists(select 1 from t_g_cashcentre  t33 where t33.tipsp1=3 and t22.id=t33.id_parent and t1.user_otkogo<>" + idpols.ToString() + " and ltrim(rtrim(t33.name2))='" + IP.ToString()+"'))");
            /// 13.02.2020

            //D4 = dataBase.GetData9("select  t1.id, name,t2.declared_value from t_g_bags t1 inner join t_g_counting_content t2 on (t2.id_bag=t1.id) where  t1.last_user_update =" + idpols.ToString());
            D4 = dataBase.GetData9("select id,name,sum(c1) as declared_value from( select  t1.id, name,t2.declared_value * (select max(t3.rate) from t_g_currency t3 where t3.id=t2.id_currency) as c1  from t_g_bags t1 inner join t_g_counting_content t2 on (t2.id_bag=t1.id) where  t1.last_user_update =" + idpols.ToString() +" )t33 group by id, name");


            D5 = dataBase.GetData9("select id, name from t_g_bags t1 where t1.last_user_update =" + idpols.ToString());

            D6 = dataBase.GetData9("select * from t_g_user where id <>" + idpols.ToString());

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "user_name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = D2.Tables[0];
            comboBox1.SelectedIndex = -1;

            /////07.02.2020

            comboBox3.Text = "";
            comboBox3.DataSource = null;
            comboBox3.Items.Clear();
            comboBox3.DisplayMember = "user_name";
            comboBox3.ValueMember = "id";
            comboBox3.DataSource = D6.Tables[0];
            comboBox3.SelectedIndex = -1;

            comboBox4.Text = "";
            comboBox4.DataSource = null;
            comboBox4.Items.Clear();
            comboBox4.DisplayMember = "name";
            comboBox4.ValueMember = "id";
            comboBox4.DataSource = D4.Tables[0];
            comboBox4.SelectedIndex = -1;


            comboBox5.Text = "";
            comboBox5.DataSource = null;
            comboBox5.Items.Clear();
            comboBox5.DisplayMember = "name";
            comboBox5.ValueMember = "id";
            comboBox5.DataSource = D5.Tables[0];
            comboBox5.SelectedIndex = -1;

            /////07.02.2020


            dataGridView1.Columns.Clear();
           

            dataGridView1.AutoGenerateColumns = false;

            ////
            dataGridView1.Columns.Add("Key", "Ввод");
            dataGridView1.Columns["Key"].Visible = true;
            //dataGridView1.Columns["Mesh"].ReadOnly = true;
            dataGridView1.Columns["Key"].Width = 100;
            dataGridView1.Columns["Key"].DataPropertyName = "Key";

            dataGridView1.Columns.Add("Val", "Валюта");
            dataGridView1.Columns["Val"].Visible = true;
            dataGridView1.Columns["Val"].ReadOnly = true;
            dataGridView1.Columns["Val"].Width = 60;
            dataGridView1.Columns["Val"].DataPropertyName = "Val1";
            dataGridView1.Columns.Add("Nomin", "Номинал");
            dataGridView1.Columns["Nomin"].Visible = true;
            dataGridView1.Columns["Nomin"].ReadOnly = true;
            dataGridView1.Columns["Nomin"].Width = 60;
            dataGridView1.Columns["Nomin"].DataPropertyName = "Nomin1";
            dataGridView1.Columns.Add("Sost", "Состояние");
            dataGridView1.Columns["Sost"].Visible = false;
            dataGridView1.Columns["Sost"].ReadOnly = true;
            dataGridView1.Columns["Sost"].Width = 100;
            dataGridView1.Columns["Sost"].DataPropertyName = "Sost1";
            dataGridView1.Columns.Add("Kol2", "Наличие");
            dataGridView1.Columns["Kol2"].Visible = true;
            dataGridView1.Columns["Kol2"].ReadOnly = true;
            dataGridView1.Columns["Kol2"].Width = 60;
            dataGridView1.Columns["Kol2"].DataPropertyName = "Kol2";
            dataGridView1.Columns.Add("Kol", "Количество");
            dataGridView1.Columns["Kol"].Visible = true;
            dataGridView1.Columns["Kol"].ReadOnly = true;
            dataGridView1.Columns["Kol"].Width = 60;
            dataGridView1.Columns["Kol"].DataPropertyName = "Kol1";
            dataGridView1.Columns.Add("Suma", "Сумма");
            dataGridView1.Columns["Suma"].Visible = true;
            dataGridView1.Columns["Suma"].ReadOnly = true;
            dataGridView1.Columns["Suma"].Width = 100;
            dataGridView1.Columns["Suma"].DataPropertyName = "Suma1";

            
            /////29.01.2020

            dataGridView1.Columns.Add("Den1", "Id Номинала");
            dataGridView1.Columns["Den1"].Visible = false;
            dataGridView1.Columns["Den1"].DataPropertyName = "id_denomination";

            dataGridView1.Columns.Add("S1", "Id Состояния");
            dataGridView1.Columns["S1"].Visible = false;
            dataGridView1.Columns["S1"].DataPropertyName = "id_condition";

            /////29.01.2020

            //////19.02.2020
            dataGridView1.Columns["Kol2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Kol"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Suma"].DefaultCellStyle.Format = "### ### ### ###";
            //////19.02.2020


            //label2.Visible = false;
            //comboBox2.Visible = false;

            /////05.02.2020
            dataGridView2.Columns.Clear();
            dataGridView2.AutoGenerateColumns = false;

            dataGridView2.Columns.Add("name", "Наименование");
            dataGridView2.Columns["name"].Visible = true;
            dataGridView2.Columns["name"].ReadOnly = true;
            dataGridView2.Columns["name"].DataPropertyName = "name";

            dataGridView2.Columns.Add("c1", "Статус");
            dataGridView2.Columns["c1"].Visible = true;
            dataGridView2.Columns["c1"].ReadOnly = true;
            dataGridView2.Columns["c1"].DataPropertyName = "c1";

            bindingSource = new BindingSource();
            bindingSource.DataSource = D3.Tables[0];
            dataGridView2.DataSource = bindingSource;
            dataGridView2.AutoResizeColumns();

            /////07.02.2020

            dataGridView3.Columns.Clear();
            dataGridView3.AutoGenerateColumns = false;

            dataGridView3.Columns.Add("name", "Номер сумки");
            dataGridView3.Columns["name"].Visible = true;
            dataGridView3.Columns["name"].ReadOnly = true;
            dataGridView3.Columns["name"].DataPropertyName = "name";


            dataGridView3.Columns.Add("c1", "ID");
            dataGridView3.Columns["c1"].Visible = false;
            dataGridView3.Columns["c1"].ReadOnly = true;
            dataGridView3.Columns["c1"].DataPropertyName = "c1";

            dataGridView4.Columns.Clear();
            dataGridView4.AutoGenerateColumns = false;

            dataGridView4.Columns.Add("name1", "Номер сумки");
            dataGridView4.Columns["name1"].Visible = true;
            dataGridView4.Columns["name1"].ReadOnly = true;

            /////07.02.2020


            /////19.02.2020

            dataGridView5.Columns.Clear();


            dataGridView5.AutoGenerateColumns = false;

            
            dataGridView5.Columns.Add("Key", "Ввод");
            dataGridView5.Columns["Key"].Visible = false;
            dataGridView5.Columns["Key"].Width = 100;
            dataGridView5.Columns["Key"].DataPropertyName = "Key";

            dataGridView5.Columns.Add("Val", "Валюта");
            dataGridView5.Columns["Val"].Visible = true;
            dataGridView5.Columns["Val"].ReadOnly = true;
            dataGridView5.Columns["Val"].Width = 60;
            dataGridView5.Columns["Val"].DataPropertyName = "Val1";
            dataGridView5.Columns.Add("Nomin", "Номинал");
            dataGridView5.Columns["Nomin"].Visible = true;
            dataGridView5.Columns["Nomin"].ReadOnly = true;
            dataGridView5.Columns["Nomin"].Width = 60;
            dataGridView5.Columns["Nomin"].DataPropertyName = "Nomin1";
            dataGridView5.Columns.Add("Sost", "Состояние");
            dataGridView5.Columns["Sost"].Visible = true;
            dataGridView5.Columns["Sost"].ReadOnly = true;
            dataGridView5.Columns["Sost"].Width = 100;
            dataGridView5.Columns["Sost"].DataPropertyName = "Sost1";
            dataGridView5.Columns.Add("Kol2", "Наличие");
            dataGridView5.Columns["Kol2"].Visible = true;
            dataGridView5.Columns["Kol2"].ReadOnly = true;
            dataGridView5.Columns["Kol2"].Width = 60;
            dataGridView5.Columns["Kol2"].DataPropertyName = "Kol2";
            dataGridView5.Columns.Add("Kol", "Количество");
            dataGridView5.Columns["Kol"].Visible = true;
            dataGridView5.Columns["Kol"].ReadOnly = true;
            dataGridView5.Columns["Kol"].Width = 60;
            dataGridView5.Columns["Kol"].DataPropertyName = "Kol1";
            dataGridView5.Columns.Add("Suma", "Сумма");
            dataGridView5.Columns["Suma"].Visible = true;
            dataGridView5.Columns["Suma"].ReadOnly = true;
            dataGridView5.Columns["Suma"].Width = 100;
            dataGridView5.Columns["Suma"].DataPropertyName = "Suma1";


            
            dataGridView5.Columns.Add("Den1", "Id Номинала");
            dataGridView5.Columns["Den1"].Visible = false;
            dataGridView5.Columns["Den1"].DataPropertyName = "id_denomination";

            dataGridView5.Columns.Add("S1", "Id Состояния");
            dataGridView5.Columns["S1"].Visible = false;
            dataGridView5.Columns["S1"].DataPropertyName = "id_condition";

           
            dataGridView5.Columns["Kol2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView5.Columns["Kol"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView5.Columns["Suma"].DefaultCellStyle.Format = "### ### ### ###";



            /////19.02.2020

            /////20.02.2020

            dataGridView7.Columns.Clear();


            dataGridView7.AutoGenerateColumns = false;


            dataGridView7.Columns.Add("Key", "Ввод");
            dataGridView7.Columns["Key"].Visible = true;
            dataGridView7.Columns["Key"].Width = 100;
            dataGridView7.Columns["Key"].DataPropertyName = "Key";

            dataGridView7.Columns.Add("Val", "Валюта");
            dataGridView7.Columns["Val"].Visible = true;
            dataGridView7.Columns["Val"].ReadOnly = true;
            dataGridView7.Columns["Val"].Width = 60;
            dataGridView7.Columns["Val"].DataPropertyName = "Val1";
            dataGridView7.Columns.Add("Nomin", "Номинал");
            dataGridView7.Columns["Nomin"].Visible = true;
            dataGridView7.Columns["Nomin"].ReadOnly = true;
            dataGridView7.Columns["Nomin"].Width = 60;
            dataGridView7.Columns["Nomin"].DataPropertyName = "Nomin1";
            dataGridView7.Columns.Add("Sost", "Состояние");
            dataGridView7.Columns["Sost"].Visible = true;
            dataGridView7.Columns["Sost"].ReadOnly = true;
            dataGridView7.Columns["Sost"].Width = 100;
            dataGridView7.Columns["Sost"].DataPropertyName = "Sost1";
            dataGridView7.Columns.Add("Kol2", "Наличие");
            dataGridView7.Columns["Kol2"].Visible = true;
            dataGridView7.Columns["Kol2"].ReadOnly = true;
            dataGridView7.Columns["Kol2"].Width = 60;
            dataGridView7.Columns["Kol2"].DataPropertyName = "Kol2";
            dataGridView7.Columns.Add("Kol", "Количество");
            dataGridView7.Columns["Kol"].Visible = true;
            dataGridView7.Columns["Kol"].ReadOnly = true;
            dataGridView7.Columns["Kol"].Width = 60;
            dataGridView7.Columns["Kol"].DataPropertyName = "Kol1";
            dataGridView7.Columns.Add("Suma", "Сумма");
            dataGridView7.Columns["Suma"].Visible = true;
            dataGridView7.Columns["Suma"].ReadOnly = true;
            dataGridView7.Columns["Suma"].Width = 100;
            dataGridView7.Columns["Suma"].DataPropertyName = "Suma1";



            dataGridView7.Columns.Add("Den1", "Id Номинала");
            dataGridView7.Columns["Den1"].Visible = false;
            dataGridView7.Columns["Den1"].DataPropertyName = "id_denomination";

            dataGridView7.Columns.Add("S1", "Id Состояния");
            dataGridView7.Columns["S1"].Visible = false;
            dataGridView7.Columns["S1"].DataPropertyName = "id_condition";

            dataGridView7.Columns.Add("V1", "Id Валюты");
            dataGridView7.Columns["V1"].Visible = false;
            dataGridView7.Columns["V1"].DataPropertyName = "curr_id";

            dataGridView7.Columns.Add("Kol3", "Количество без состояния");
            dataGridView7.Columns["Kol3"].Visible = false;
            dataGridView7.Columns["Kol3"].DataPropertyName = "Kol3";


            dataGridView7.Columns["Kol2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Kol"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Suma"].DefaultCellStyle.Format = "### ### ### ###";

            dataGridView6.Columns.Clear();


            dataGridView6.AutoGenerateColumns = false;


            dataGridView6.Columns.Add("Key", "Ввод");
            dataGridView6.Columns["Key"].Visible = false;
            dataGridView6.Columns["Key"].Width = 100;
            dataGridView6.Columns["Key"].DataPropertyName = "Key";

            dataGridView6.Columns.Add("Val", "Валюта");
            dataGridView6.Columns["Val"].Visible = true;
            dataGridView6.Columns["Val"].ReadOnly = true;
            dataGridView6.Columns["Val"].Width = 60;
            dataGridView6.Columns["Val"].DataPropertyName = "Val1";
            dataGridView6.Columns.Add("Nomin", "Номинал");
            dataGridView6.Columns["Nomin"].Visible = true;
            dataGridView6.Columns["Nomin"].ReadOnly = true;
            dataGridView6.Columns["Nomin"].Width = 60;
            dataGridView6.Columns["Nomin"].DataPropertyName = "Nomin1";
            dataGridView6.Columns.Add("Sost", "Состояние");
            dataGridView6.Columns["Sost"].Visible = true;
            dataGridView6.Columns["Sost"].ReadOnly = true;
            dataGridView6.Columns["Sost"].Width = 100;
            dataGridView6.Columns["Sost"].DataPropertyName = "Sost1";
            dataGridView6.Columns.Add("Kol2", "Наличие");
            dataGridView6.Columns["Kol2"].Visible = true;
            dataGridView6.Columns["Kol2"].ReadOnly = true;
            dataGridView6.Columns["Kol2"].Width = 60;
            dataGridView6.Columns["Kol2"].DataPropertyName = "Kol2";
            dataGridView6.Columns.Add("Kol", "Количество");
            dataGridView6.Columns["Kol"].Visible = true;
            dataGridView6.Columns["Kol"].ReadOnly = true;
            dataGridView6.Columns["Kol"].Width = 60;
            dataGridView6.Columns["Kol"].DataPropertyName = "Kol1";
            dataGridView6.Columns.Add("Suma", "Сумма");
            dataGridView6.Columns["Suma"].Visible = true;
            dataGridView6.Columns["Suma"].ReadOnly = true;
            dataGridView6.Columns["Suma"].Width = 100;
            dataGridView6.Columns["Suma"].DataPropertyName = "Suma1";



            dataGridView6.Columns.Add("Den1", "Id Номинала");
            dataGridView6.Columns["Den1"].Visible = false;
            dataGridView6.Columns["Den1"].DataPropertyName = "id_denomination";

            dataGridView6.Columns.Add("S1", "Id Состояния");
            dataGridView6.Columns["S1"].Visible = false;
            dataGridView6.Columns["S1"].DataPropertyName = "id_condition";


            dataGridView6.Columns["Kol2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView6.Columns["Kol"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView6.Columns["Suma"].DefaultCellStyle.Format = "### ### ### ###";


            dataGridView8.Columns.Clear();

            dataGridView8.AutoGenerateColumns = false;


            dataGridView8.Columns.Add("Key", "Ввод");
            dataGridView8.Columns["Key"].Visible = true;
            dataGridView8.Columns["Key"].Width = 100;
            dataGridView8.Columns["Key"].DataPropertyName = "Key";

            dataGridView8.Columns.Add("Val", "Валюта");
            dataGridView8.Columns["Val"].Visible = true;
            dataGridView8.Columns["Val"].ReadOnly = true;
            dataGridView8.Columns["Val"].Width = 60;
            dataGridView8.Columns["Val"].DataPropertyName = "Val1";
            dataGridView8.Columns.Add("Nomin", "Номинал");
            dataGridView8.Columns["Nomin"].Visible = true;
            dataGridView8.Columns["Nomin"].ReadOnly = true;
            dataGridView8.Columns["Nomin"].Width = 60;
            dataGridView8.Columns["Nomin"].DataPropertyName = "Nomin1";
            dataGridView8.Columns.Add("Sost", "Состояние");
            dataGridView8.Columns["Sost"].Visible = true;
            dataGridView8.Columns["Sost"].ReadOnly = true;
            dataGridView8.Columns["Sost"].Width = 100;
            dataGridView8.Columns["Sost"].DataPropertyName = "Sost1";
            dataGridView8.Columns.Add("Kol2", "Наличие");
            dataGridView8.Columns["Kol2"].Visible = true;
            dataGridView8.Columns["Kol2"].ReadOnly = true;
            dataGridView8.Columns["Kol2"].Width = 60;
            dataGridView8.Columns["Kol2"].DataPropertyName = "Kol2";
            dataGridView8.Columns.Add("Kol", "Количество");
            dataGridView8.Columns["Kol"].Visible = true;
            dataGridView8.Columns["Kol"].ReadOnly = true;
            dataGridView8.Columns["Kol"].Width = 60;
            dataGridView8.Columns["Kol"].DataPropertyName = "Kol1";
            dataGridView8.Columns.Add("Suma", "Сумма");
            dataGridView8.Columns["Suma"].Visible = true;
            dataGridView8.Columns["Suma"].ReadOnly = true;
            dataGridView8.Columns["Suma"].Width = 100;
            dataGridView8.Columns["Suma"].DataPropertyName = "Suma1";



            dataGridView8.Columns.Add("Den1", "Id Номинала");
            dataGridView8.Columns["Den1"].Visible = false;
            dataGridView8.Columns["Den1"].DataPropertyName = "id_denomination";

            dataGridView8.Columns.Add("S1", "Id Состояния");
            dataGridView8.Columns["S1"].Visible = false;
            dataGridView8.Columns["S1"].DataPropertyName = "id_condition";

            dataGridView8.Columns.Add("V1", "Id Валюты");
            dataGridView8.Columns["V1"].Visible = false;
            dataGridView8.Columns["V1"].DataPropertyName = "curr_id";


            dataGridView8.Columns["Kol2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView8.Columns["Kol"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView8.Columns["Suma"].DefaultCellStyle.Format = "### ### ### ###";

            /////20.02.2020

            /////21.02.2020
            tabControl1.TabPages.Remove(tabPage5);
            /////21.02.2020

            /*
            if (D3 != null)
            {

               
                dataGridView2.DataSource = D3.Tables[0];

                dataGridView2.AutoResizeColumns();
            }
            */
            /////05.02.2020


            //  checkBox1.Visible = false;


            /*
            /////30.01.2020
            if (itip == 0)
            {

                D3 = dataBase.GetData9("select * from t_g_counting");
                comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                comboBox2.Text = "";
                comboBox2.DataSource = null;
                comboBox2.Items.Clear();
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                comboBox2.DataSource = D3.Tables[0];
                comboBox2.SelectedIndex = -1;
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            }
            else
            {
                label2.Visible = false;
                comboBox2.Visible = false;


            }
            /////30.01.2020
            */

            // if (idperch>0)
            pokaz();

            //timer1.Enabled = true;

           // dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";

            /*
            //D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update="+ idpols.ToString()+ " and t1.id_counting = " + idperch .ToString()+ " group by t4.curr_code,t2.name order by t4.curr_code,t2.name");

            D1 = dataBase.GetData9("select * from(select t1.id_denomination,t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) group by t4.curr_code,t2.name,t1.id_denomination union all select t1.id_denomination,t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  group by t4.curr_code,t2.name,t1.id_denomination ) t33 order by Val1, Nomin1");


            //D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");

            dataGridView1.DataSource = D1.Tables[0];

            dataGridView1.AutoResizeColumns();

           dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";
            */


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {

                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                {



                    int flper = 0;

                    string strobsh = "<Dvig>";


                    for (int i1 = 0; i1 < dataGridView1.Rows.Count - 1; i1++)
                    //  foreach (DataRow dr in D1.Tables[0].Rows)
                    {


                        //dr["ИмяИлиНомерСтолбца"].ToString()

                        //    if (dr["Kol1"].ToString().Trim() != "")

                        if (dataGridView1.Rows[i1].Cells["Kol"].Value != null)
                            if (dataGridView1.Rows[i1].Cells["Kol"].Value.ToString() != "")
                            {

                                if (checkBox1.Checked)
                                    //     strobsh = strobsh + "<o1 " + "val=\"" + dr["id_denomination"].ToString() + "\"" + "sost=\"" + dr["id_condition"].ToString() + "\"" + "kol=\"" + dr["Kol1"].ToString() + "\"" + "userotkogo=\"" + idpols.ToString() + "\"" + "userkomy=\"" + comboBox1.SelectedValue.ToString() + "\"";
                                    strobsh = strobsh + "<o1 " + "val=\"" + dataGridView1.Rows[i1].Cells["Den1"].Value.ToString() + "\"" + " sost=\"" + dataGridView1.Rows[i1].Cells["S1"].Value.ToString() + "\"" + " kol=\"" + dataGridView1.Rows[i1].Cells["Kol"].Value.ToString() + "\"" + " userotkogo=\"" + idpols.ToString() + "\"" + " userkomy=\"" + comboBox1.SelectedValue.ToString() + "\" per=\"" + idperch.ToString() + "\" />";
                                else
                                    strobsh = strobsh + "<o1 " + "val=\"" + dataGridView1.Rows[i1].Cells["Den1"].Value.ToString() + "\"" + " kol=\"" + dataGridView1.Rows[i1].Cells["Kol"].Value.ToString() + "\"" + " userotkogo=\"" + idpols.ToString() + "\"" + " userkomy=\"" + comboBox1.SelectedValue.ToString() + "\" per=\"" + idperch.ToString() + "\" />";


                                flper = 1;
                            }


                    }

                    strobsh = strobsh + " </Dvig>";

                    int p2 = 0;

                    if (checkBox1.Checked)
                        p2 = 1;

                    if (flper == 1)
                    {
                     
                        ////12.02.2020
                        if (checkBox2.Checked)
                        dataBase.Dvden1(strobsh, p2, 0, 1);
                        else
                        ////12.02.2020
                            
                        dataBase.Dvden1(strobsh, p2, 0, 0);
                    }



                    pokaz();

                    MessageBox.Show("Передача выполнена!");



                }
                else

                    /////12.02.2020
                    //MessageBox.Show("Выберите пользователя кому передать!");
                    MessageBox.Show("Выберите куда передать!");
                /////12.02.2020

            }

            if (tabControl1.SelectedIndex == 1)
            {

                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {



                    int flper = 0;

                    string strobsh = "<Dvig>";

                    
                    for (int i1 = 0; i1 < dataGridView3.Rows.Count ; i1++)
                    
                    {




                       
                            if (dataGridView3.Rows[i1].Cells["c1"].Value != null)
                            if (dataGridView3.Rows[i1].Cells["c1"].Value.ToString() != "")
                            {

                                
                                    strobsh = strobsh + "<o1 " + "sum=\"" + Convert.ToDouble(textBox15.Text.ToString()).ToString() + "\"" + " kol=\"" + textBox11.Text.ToString() + "\"" + " userotkogo=\"" + idpols.ToString() + "\"" + " userkomy=\"" + comboBox3.SelectedValue.ToString() + "\" per=\"" + dataGridView3.Rows[i1].Cells["c1"].Value.ToString() + "\" />";


                                flper = 1;
                            }


                    }

                    strobsh = strobsh + " </Dvig>";

                    int p2 = 0;



                    if (flper == 1)
                    {

                        ////12.02.2020
                        if (checkBox3.Checked)
                            dataBase.Dvden1(strobsh, p2, 4, 1);
                        else
                            ////12.02.2020
                            dataBase.Dvden1(strobsh, p2, 4, 0);

                    }



                    pokaz();

                    MessageBox.Show("Передача выполнена!");
                    



                }
                else
                    

                /////12.02.2020
                //MessageBox.Show("Выберите пользователя кому передать!");
                MessageBox.Show("Выберите куда передать!");
                /////12.02.2020


            }


            ////////20.02.2020

            if (tabControl1.SelectedIndex == 2)
            {


                /////проверка

                for (int i1=0; i1< dataGridView7.Rows.Count-1;i1++)
                {

                    int icol1 = 0;

                    if (!DBNull.Value.Equals(dataGridView7.Rows[i1].Cells["Kol"].Value))
                    {

                     //   icol1= Convert.ToInt32(dataGridView7.Rows[i1].Cells["Kol3"].Value.ToString());

                        DataRow[] searchedRows = ((DataTable)D11.Tables[0]).Select("Nomin1='" + dataGridView7.Rows[i1].Cells["Nomin"].Value.ToString() + "' and curr_id=" + dataGridView7.Rows[i1].Cells["V1"].Value.ToString()//and Kol1>0"
                            );

                       

                        int icol2 = 0;

                        string sost11 = "";

                        string idsost1 = "";

                        foreach (DataRow r in searchedRows)
                        {
                            if (r["Kol1"].ToString().Trim() != "")
                            {
                                icol2 = icol2 + Convert.ToInt32(r["Kol1"]);

                                icol1 = icol1 + Convert.ToInt32(r["Kol2"]);

                                sost11 = sost11 + r["Sost1"].ToString() + ",";

                                /////21.02.2020
                                idsost1 = idsost1 + r["id_condition"].ToString() + ",";
                                /////21.02.2020

                            }



                        }

                        sost11 = sost11.Substring(0, sost11.Length - 1);
                        
                        /////21.02.2020

                        idsost1 = idsost1.Substring(0, idsost1.Length - 1);
                        D14 = dataBase.GetData9("select sum(t1.count) as c1 from t_g_cash t1 where t1.id_denomination="+ dataGridView7.Rows[i1].Cells["Den1"].Value.ToString() + " and t1.id_user="+ idpols.ToString()+ " and t1.id_cond in ("+ idsost1.ToString()+ ")");
                        if (D14 != null)
                            if (D14.Tables[0] != null)
                            {
                                if (D14.Tables[0].Rows.Count > 0)
                                {
                                    if (D14.Tables[0].Rows[0][0].ToString()!="")
                                    icol1 = Convert.ToInt32(D14.Tables[0].Rows[0][0].ToString());

                                }
                            }

                        ////21.02.2020
                        
                        if (icol2 != icol1)
                        {

                            MessageBox.Show("Недостаток по номиналу - " + dataGridView7.Rows[i1].Cells["Nomin"].Value.ToString()+"! Валюты - "+dataGridView7.Rows[i1].Cells["Val"].Value.ToString()+", по состояниям - " + sost11.ToString()+ ". Общее число по номиналу - " + icol1.ToString()+". Сейчас сделано - "+ icol2.ToString());
                            return;
                        }

                    }

                }

                /////проверка

                int flper = 0;

                string strobsh = "<Dvig>";

                for (int i1 = 0; i1 < dataGridView7.Rows.Count - 1; i1++)
                
                {


                   

                    if (dataGridView7.Rows[i1].Cells["Kol"].Value != null)
                        if (dataGridView7.Rows[i1].Cells["Kol"].Value.ToString() != "")
                        {

                           strobsh = strobsh + "<o1 " + "val=\"" + dataGridView7.Rows[i1].Cells["Den1"].Value.ToString() + "\"" + " sost=\"" + dataGridView7.Rows[i1].Cells["S1"].Value.ToString() + "\"" + " kol=\"" + dataGridView7.Rows[i1].Cells["Kol"].Value.ToString() + "\"" + " userotkogo=\"" + idpols.ToString() + "\"" + " userkomy=\"0\" per=\"0\" />";
                            

                            flper = 1;
                        }


                }

                strobsh = strobsh + " </Dvig>";

                int p2 = 0;

               

                if (flper == 1)
                {


                         dataBase.Dvden1(strobsh, 0, 6, 0);

                    pokaz();

                    MessageBox.Show("Передача выполнена!");
                }



                


                /*
                int icol1 = Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Kol3"].Value.ToString());

                int icol2 = 0;

                
                foreach (DataRow r in searchedRows)
                {
                    if (r["Kol1"].ToString().Trim() != "")
                        icol2 = icol2 + Convert.ToInt32(r["Kol1"]);



                }

                */
                /*
                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {

                }
                */
            }

            ////////20.02.2020

        }

        private void pokaz()
        {

            if (flprosm == 1)
            {
                if (bindingSource.Current!=null)
                if (((DataRowView)bindingSource.Current)["id"].ToString() != "")
                {

                        //////
                        if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "1")
                        {
                         //////

                            if (checkBox1.Checked)
                            {
                                // D1 = dataBase.GetData9("select * from(select t1.id_detal as id_denomination,t1.id_cond,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_pered_detal t1 inner join t_g_denomination t2 on (t1.id_detal = t2.id) inner join t_g_condition t3 on (t1.id_cond = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.id = " + ((DataRowView)bindingSource.Current)["id"].ToString() + "   )t33 where Kol2>0 order by Val1, Nomin1");
                                D1 = dataBase.GetData9("select * from(select '' as [Key],t1.id_detal as id_denomination,t1.id_cond as id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol1,t1.fact_value as Suma1 , case when t3.id>0 then  (select sum(t1.count) from t_g_cash t1 where t1.id_user=" + idpols.ToString() + " and t1.id_denomination=t2.id and t1.id_cond=t3.id) else (select sum(t1.count) from t_g_cash t1 where t1.id_user=" + idpols.ToString() + " and t1.id_denomination=t2.id ) end as Kol2 from t_g_pered_detal t1 inner join t_g_denomination t2 on (t1.id_detal = t2.id) left join t_g_condition t3 on (t1.id_cond = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.id_pered = " + ((DataRowView)bindingSource.Current)["id"].ToString() + "  )t33 where Kol1>0 union all select * from (select 'ИТОГО:' as [Key],null as c0,null as c1, null as c2, null as c3, null as c4, sum(t1.count) as Kol1 , sum(t1.fact_value)  as Suma1, null as Kol2 from t_g_pered_detal t1 where t1.id_pered = " + ((DataRowView)bindingSource.Current)["id"].ToString() + " )t33 where t33.Kol1>0");
                                dataGridView1.Columns["Sost"].Visible = true;
                            }
                            else
                            {
                                //  D1 = dataBase.GetData9("select id_detal as id_denomination, Val1, Nomin1, sum(Kol2) as Kol2 from(select t1.id_detal, t4.curr_code as Val1, t2.name as Nomin1, sum(t1.count) as Kol2 from t_g_pered_detal t1 inner join t_g_denomination t2 on(t1.id_detal = t2.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.id = " + ((DataRowView)bindingSource.Current)["id"].ToString() + "   group by t4.curr_code, t2.name, t1.id_detal) t33 where Kol2 > 0 group by id_detal, Val1,Nomin1 order by Val1, Nomin1");
                                D1 = dataBase.GetData9("select '' as [Key],id_detal as id_denomination, Val1, Nomin1, sum(Kol1) as Kol1 , sum(Suma1) as Suma1, (select sum(t1.count) from t_g_cash t1 where t1.id_user =" + idpols.ToString() + " and t1.id_denomination=t33.id_detal) as Kol2 from(select t1.id_detal, t4.curr_code as Val1, t2.name as Nomin1, sum(t1.count) as Kol1, sum(t1.fact_value) as Suma1 from t_g_pered_detal t1 inner join t_g_denomination t2 on (t1.id_detal = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.id_pered = " + ((DataRowView)bindingSource.Current)["id"].ToString() + " group by t4.curr_code, t2.name, t1.id_detal) t33 where Kol1 > 0 group by id_detal, Val1,Nomin1 union all select *from(select 'ИТОГО:' as [Key], null as c1, null as c2, null as c3, sum(t1.count) as Kol1, sum(t1.fact_value) as Suma1, null as Kol2 from t_g_pered_detal t1 where t1.id_pered = " + ((DataRowView)bindingSource.Current)["id"].ToString() + " )t33 where t33.Kol1 > 0");

                                dataGridView1.Columns["Sost"].Visible = false;
                            }

                        }

                        //////
                        if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "2")
                        {

                            D7 = dataBase.GetData9("select id_detal as c1,name,t1.fact_value as declared_value  from t_g_pered_detal t1 inner join t_g_bags t2 on(t2.id = t1.id_detal)  where t1.id_pered =" + ((DataRowView)bindingSource.Current)["id"].ToString() );
                            dataGridView3.DataSource = D7.Tables[0];
                        }
                        //////


                        }
            }
            else
            {

                if (checkBox1.Checked)
                {
                    //D1 = dataBase.GetData9("select * from(select t1.id_denomination,t1.id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_condition = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) union all select t1.id_denomination,t1.id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_condition = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " )t33 where Kol2>0 order by Val1, Nomin1");
                    // D1 = dataBase.GetData9("select * from(select t1.id_denomination,t1.id_cond,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_cash t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_cond = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + "   )t33 where Kol2>0 order by Val1, Nomin1");
                    D1 = dataBase.GetData9("select * from(select t1.id_denomination,t1.id_cond as id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_cash t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_cond = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.id_user = " + idpols.ToString() + "   )t33 where Kol2>0 order by Val1, Nomin1");

                    

                    // D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination=t2.id) inner join t_g_condition t3 on(t1.id_condition = t3.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");
                    // D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination=t2.id) inner join t_g_condition t3 on(t1.id_condition = t3.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");
                    dataGridView1.Columns["Sost"].Visible = true;
                }
                else
                {
                    // D1 = dataBase.GetData9("select id_denomination, Val1,Nomin1,sum(Kol2) as Kol2 from(select t1.id_denomination,t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) group by t4.curr_code,t2.name,t1.id_denomination union all select t1.id_denomination,t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  group by t4.curr_code,t2.name,t1.id_denomination ) t33 where Kol2>0 group by id_denomination, Val1,Nomin1 order by Val1, Nomin1");
                    //D1 = dataBase.GetData9("select id_denomination, Val1, Nomin1, sum(Kol2) as Kol2 from(select t1.id_denomination, t4.curr_code as Val1, t2.name as Nomin1, sum(t1.count) as Kol2 from t_g_cash t1 inner join t_g_denomination t2 on(t1.id_denomination = t2.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + "   group by t4.curr_code, t2.name, t1.id_denomination) t33 where Kol2 > 0 group by id_denomination, Val1,Nomin1 order by Val1, Nomin1");
                    D1 = dataBase.GetData9("select id_denomination, Val1, Nomin1, sum(Kol2) as Kol2 from(select t1.id_denomination, t4.curr_code as Val1, t2.name as Nomin1, sum(t1.count) as Kol2 from t_g_cash t1 inner join t_g_denomination t2 on(t1.id_denomination = t2.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.id_user = " + idpols.ToString() + "   group by t4.curr_code, t2.name, t1.id_denomination) t33 where Kol2 > 0 group by id_denomination, Val1,Nomin1 order by Val1, Nomin1");


                    // D1 = dataBase.GetData9("select * from(select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = "+ idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) group by t4.curr_code,t2.name union all select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  group by t4.curr_code,t2.name ) t33 order by Val1, Nomin1");

                    //D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " group by t4.curr_code,t2.name order by t4.curr_code,t2.name");
                    //D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");
                    dataGridView1.Columns["Sost"].Visible = false;
                }


            }
            dataGridView1.DataSource = D1.Tables[0];



            dataGridView1.AutoResizeColumns();

            ////19.02.2020

            D10 = dataBase.GetData9("select * from(select t1.id_denomination,t1.id_cond as id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_cash t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_cond = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.id_user = " + idpols.ToString() + "   )t33 where Kol2>0 order by Val1, Nomin1");
            dataGridView5.Columns["Sost"].Visible = true;


            dataGridView5.DataSource = D10.Tables[0];
            dataGridView5.AutoResizeColumns();

            D11= dataBase.GetData9("select t2.id as id_denomination,t3.id as id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,(select max(t8.count) from t_g_cash t8 where t8.id_denomination=t2.id and t8.id_cond=t3.id and t8.id_user=" + idpols.ToString() + ")  as Kol2,(select sum(t8.count) from t_g_cash t8 where t8.id_denomination=t2.id  and t8.id_user=" + idpols.ToString() + ")  as Kol3, t4.id curr_id from t_g_denomination t2 inner join t_g_currency t4 on (t2.id_currency = t4.id) ,t_g_condition t3 where t3.visible=0 and exists(select 1 from t_g_cash t5 inner join t_g_denomination t6 on (t5.id_denomination=t6.id) where t6.id_currency=t2.id_currency) order by t4.id,t3.id,t2.id ");
            dataGridView7.DataSource = D11.Tables[0];
            dataGridView7.AutoResizeColumns();


            ////19.02.2020
            
            ////20.02.2020

            D12 = dataBase.GetData9("select * from(select t1.id_denomination,t1.id_cond as id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_cash t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_cond = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.id_user = " + idpols.ToString() + "   )t33 where Kol2>0 order by Val1, Nomin1");
            dataGridView6.Columns["Sost"].Visible = true;


            dataGridView6.DataSource = D12.Tables[0];
            dataGridView6.AutoResizeColumns();

            D13 = dataBase.GetData9("select t2.id as id_denomination,t3.id as id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,(select max(t8.count) from t_g_cash t8 where t8.id_denomination=t2.id and t8.id_cond=t3.id and t8.id_user=" + idpols.ToString() + ")  as Kol2,(select sum(t8.count) from t_g_cash t8 where t8.id_denomination=t2.id  and t8.id_user=" + idpols.ToString() + ")  as Kol3, t4.id curr_id from t_g_denomination t2 inner join t_g_currency t4 on (t2.id_currency = t4.id) ,t_g_condition t3 where t3.visible=0 and exists(select 1 from t_g_cash t5 inner join t_g_denomination t6 on (t5.id_denomination=t6.id) where t6.id_currency=t2.id_currency) order by t4.id,t3.id,t2.id ");
            dataGridView8.DataSource = D13.Tables[0];
            dataGridView8.AutoResizeColumns();


            ////20.02.2020
            
            // dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Key"].Value = "";

            if (flprosm == 0)
            {
               

                D1.Tables[0].Rows.Add();
                dataGridView1.DataSource = D1.Tables[0];
                // dataGridView1.Rows.Add('','', '', '', '', '', '', '', '');
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";

                ////20.02.2020

                if (!D11.Tables[0].Columns.Contains("Key"))
                    D11.Tables[0].Columns.Add("Key");

                if (!D11.Tables[0].Columns.Contains("Kol1"))
                    D11.Tables[0].Columns.Add("Kol1");

                D11.Tables[0].Rows.Add();
                dataGridView7.DataSource = D11.Tables[0];
                D11.Tables[0].Rows[D11.Tables[0].Rows.Count-1]["Key"]= "ИТОГО:";
                // dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";

                //  dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";
                //  dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";

                if (!D13.Tables[0].Columns.Contains("Key"))
                    D13.Tables[0].Columns.Add("Key");

                if (!D13.Tables[0].Columns.Contains("Kol1"))
                    D13.Tables[0].Columns.Add("Kol1");

                D13.Tables[0].Rows.Add();
                dataGridView8.DataSource = D13.Tables[0];
                //dataGridView8.Rows[dataGridView8.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";
                D13.Tables[0].Rows[D13.Tables[0].Rows.Count - 1]["Key"] = "ИТОГО:";

                ////20.02.2020

            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
                            idperch = Convert.ToInt64(comboBox2.SelectedValue);
               
           

          //  if (idperch > 0)
                pokaz();

            //  pokaz();

            /*
            if (checkBox1.Checked)
            {
                D1 = dataBase.GetData9("select * from(select t1.id_denomination,t1.id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_condition = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) union all select t1.id_denomination,t1.id_condition,t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_condition t3 on (t1.id_condition = t3.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " )t33 order by Val1, Nomin1");

                // D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1,t1.count as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination=t2.id) inner join t_g_condition t3 on(t1.id_condition = t3.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");
                // D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,t3.name as Sost1 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination=t2.id) inner join t_g_condition t3 on(t1.id_condition = t3.id) inner join t_g_currency t4 on(t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");
                dataGridView1.Columns["Sost"].Visible = true;
            }
            else
            {
                D1 = dataBase.GetData9("select * from(select t1.id_denomination,t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) group by t4.curr_code,t2.name,t1.id_denomination union all select t1.id_denomination,t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  group by t4.curr_code,t2.name,t1.id_denomination ) t33 order by Val1, Nomin1");

                // D1 = dataBase.GetData9("select * from(select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update = "+ idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t1.id_counting and t77.id_denomination = t1.id_denomination and t77.id_condition = t1.id_condition) group by t4.curr_code,t2.name union all select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_dvdeneg t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.user1 = " + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  group by t4.curr_code,t2.name ) t33 order by Val1, Nomin1");

                //D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1,sum(t1.count) as Kol2 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + " group by t4.curr_code,t2.name order by t4.curr_code,t2.name");
                //D1 = dataBase.GetData9("select t4.curr_code as Val1,t2.name as Nomin1 from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) inner join t_g_currency t4 on (t2.id_currency = t4.id) where t1.last_user_update=" + idpols.ToString() + " and t1.id_counting = " + idperch.ToString() + "  order by t4.curr_code,t2.name");
                dataGridView1.Columns["Sost"].Visible = false;
            }
            
                dataGridView1.DataSource = D1.Tables[0];



            dataGridView1.AutoResizeColumns();
           dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";
            */

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) & (e.RowIndex!= dataGridView1.Rows.Count-1))
            {
                
                

                if (dataGridView1.Rows[e.RowIndex].Cells["Key"].Value != null)
                    if (dataGridView1.Rows[e.RowIndex].Cells["Key"].Value.ToString() != String.Empty)
                {

                    ///////29.01.2020 проверка на наличие общего количества
                    /*
                    if (checkBox1.Checked)
                    {

                        if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol"].Value) + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Key"].Value)> Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol2"].Value))
                        {
                            MessageBox.Show("Нельзя передавать валюты больше чем есть в наличии по состоянию!");
                            return;
                        }

                       


                    }


                    else
                    {

                        int iobch1 = 0;
                        if (dataGridView1.Rows[e.RowIndex].Cells["Val"].Value != null)
                        {
                            string s1 = dataGridView1.Rows[e.RowIndex].Cells["Val"].Value.ToString();
                            int i2 = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol"].Value) + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Key"].Value)) * Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Nomin"].Value);

                            for (int i1 = 0; i1 < dataGridView1.Rows.Count; i1++)
                            {
                                if (dataGridView1.Rows[i1].Cells["Val"].Value != null)
                                    if (dataGridView1.Rows[i1].Cells["Val"].Value.ToString() == s1.ToString())
                                        iobch1 = iobch1 + Convert.ToInt32(dataGridView1.Rows[i1].Cells["Kol2"].Value) * Convert.ToInt32(dataGridView1.Rows[i1].Cells["Nomin"].Value);

                            }

                            if (i2 > iobch1)
                            {
                                MessageBox.Show("Нельзя передавать валюты больше чем есть в наличии!");
                                return;
                            }
                        }

                    }
                    */

                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol"].Value) + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Key"].Value) > Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol2"].Value))
                    {
                        MessageBox.Show("Нельзя передавать валюты больше чем есть в наличии!");
                        return;
                    }

                    ///////29.01.2020

                    dataGridView1.Rows[e.RowIndex].Cells["Kol"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol"].Value) + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Key"].Value);
                    dataGridView1.Rows[e.RowIndex].Cells["Suma"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kol"].Value)  * Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Nomin"].Value);

                    /////

                    int ikol1 = 0;
                    int isum1 = 0;

                    for (int i1 = 0; i1 < dataGridView1.Rows.Count-1; i1++)
                    {

                        ikol1 = ikol1+Convert.ToInt32(dataGridView1.Rows[i1].Cells["Kol"].Value);
                        isum1 = isum1 + Convert.ToInt32(dataGridView1.Rows[i1].Cells["Suma"].Value);

                     //   if (dataGridView1.Rows[i1].Cells["Val"].Value != null)

                    }

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Kol"].Value = ikol1;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Suma"].Value = isum1;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";

                        dataGridView1.Rows[e.RowIndex].Cells["Key"].Value = String.Empty;
                    }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
             idperch = Convert.ToInt64(comboBox2.SelectedValue);
                

           

            if (idperch > 0)
                pokaz();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if ((e.ColumnIndex == 0) & (e.RowIndex == dataGridView1.Rows.Count - 1))
                dataGridView1.Columns[0].ReadOnly = true;
            else
                dataGridView1.Columns[0].ReadOnly = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            //  dataGridView1.Columns["Key"].Visible = true;

            comboBox3.Text = "";
            comboBox3.SelectedIndex = -1;
            comboBox4.Text = "";
            comboBox4.SelectedIndex = -1;
            comboBox5.Text = "";
            comboBox5.SelectedIndex = -1;
            
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";

            D4 = dataBase.GetData9("select id,name,sum(c1) as declared_value from( select  t1.id, name,t2.declared_value * (select max(t3.rate) from t_g_currency t3 where t3.id=t2.id_currency) as c1  from t_g_bags t1 inner join t_g_counting_content t2 on (t2.id_bag=t1.id) where  t1.last_user_update =" + idpols.ToString() + " )t33 group by id, name");


            D5 = dataBase.GetData9("select id, name from t_g_bags t1 where t1.last_user_update =" + idpols.ToString());

            comboBox4.DataSource = D4.Tables[0];
            comboBox5.DataSource = D5.Tables[0];


            dataGridView3.DataSource = null;

            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();

            flprosm = 0;
            
            pokaz();
            //dataGridView1.Columns["Key"].ReadOnly = false;
            dataGridView1.ReadOnly = false;
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (D3 != null)
            {

                //if dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Kol"].Value
                //(DataRowView)bindingSource.Current)["id"].ToString()

                if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "1")
                {
                    textBox1.Text = ((DataRowView)bindingSource.Current)["c3"].ToString();
                    textBox2.Text = ((DataRowView)bindingSource.Current)["c2"].ToString();
                    textBox3.Text = ((DataRowView)bindingSource.Current)["count"].ToString();
                    textBox4.Text = ((DataRowView)bindingSource.Current)["fact_value"].ToString();
                    textBox5.Text = ((DataRowView)bindingSource.Current)["c1"].ToString();
                    textBox6.Text = ((DataRowView)bindingSource.Current)["createdate"].ToString();
                    textBox7.Text = ((DataRowView)bindingSource.Current)["name"].ToString();

                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";
                    textBox14.Text = "";
                    textBox15.Text = "";
                    textBox16.Text = "";

                    tabControl1.SelectedIndex = 0;

                }

                if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "2")
                {
                    textBox14.Text = ((DataRowView)bindingSource.Current)["c3"].ToString();
                    textBox13.Text = ((DataRowView)bindingSource.Current)["c2"].ToString();
                    textBox12.Text = ((DataRowView)bindingSource.Current)["count"].ToString();
                    textBox16.Text = ((DataRowView)bindingSource.Current)["fact_value"].ToString();
                    textBox10.Text = ((DataRowView)bindingSource.Current)["c1"].ToString();
                    textBox9.Text = ((DataRowView)bindingSource.Current)["createdate"].ToString();
                    textBox8.Text = ((DataRowView)bindingSource.Current)["name"].ToString();

                    textBox11.Text = ((DataRowView)bindingSource.Current)["count"].ToString();
                    textBox15.Text = ((DataRowView)bindingSource.Current)["fact_value"].ToString();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    comboBox1.Text = "";

                    tabControl1.SelectedIndex = 1;
                }

                //////13.02.2020

                if (((DataRowView)bindingSource.Current)["zone1"].ToString() == "True")
                {

                    if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "1")
                        checkBox2.Checked = true;
                    else
                        checkBox3.Checked = true;

                   

                }
                else
                {

                    if (((DataRowView)bindingSource.Current)["tipper"].ToString() == "1")
                        checkBox2.Checked = false;
                    else
                        checkBox3.Checked = false;



                }

                checkBox2_CheckedChanged(sender, e);
                checkBox3_CheckedChanged(sender, e);
                //////13.02.2020

                flprosm = 1;

                



                //  if (((DataRowView)bindingSource.Current)["id"].ToString()!="")
                //   dataGridView1.Columns["Key"].Visible = false;
                pokaz();

                dataGridView1.ReadOnly = true;

                /*
                textBox1.Text = D3.Tables[0].Rows[e.RowIndex]["c3"].ToString();
                textBox2.Text = D3.Tables[0].Rows[e.RowIndex]["c2"].ToString();
                textBox3.Text = D3.Tables[0].Rows[e.RowIndex]["count"].ToString();
                textBox4.Text = D3.Tables[0].Rows[e.RowIndex]["fact_value"].ToString();
                textBox5.Text = D3.Tables[0].Rows[e.RowIndex]["c1"].ToString();
                textBox6.Text = D3.Tables[0].Rows[e.RowIndex]["createdate"].ToString();
                textBox7.Text = D3.Tables[0].Rows[e.RowIndex]["name"].ToString();
                */

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current != null)
                if (((DataRowView)bindingSource.Current)["id"].ToString() != "")
                {
                    string s1 = "";

                    if (tabControl1.SelectedIndex == 1)
                    {
                        ////12.02.2020
                        //if (checkBox3.Checked)
                        if (((DataRowView)bindingSource.Current)["zone1"].ToString() == "True")
                            dataBase.Dvden1("", idpols, 5, Convert.ToInt64(((DataRowView)bindingSource.Current)["id"].ToString()));
                        else
                        ////12.02.2020

                        dataBase.Dvden1("", 0, 5, Convert.ToInt64(((DataRowView)bindingSource.Current)["id"].ToString()));

                    }
                    else
                    {
                        ////12.02.2020
                        // if (checkBox2.Checked)
                        if (((DataRowView)bindingSource.Current)["zone1"].ToString() == "True")
                            s1 = dataBase.Dvden1("", idpols, 2, Convert.ToInt64(((DataRowView)bindingSource.Current)["id"].ToString()));
                        else
                        ////12.02.2020  

                        s1 = dataBase.Dvden1("", 0, 2, Convert.ToInt64(((DataRowView)bindingSource.Current)["id"].ToString()));
                    }

                    if (s1.Length > 0)
                    {
                       if  (s1.Substring(0, 1).ToString() == "0")
                        MessageBox.Show(s1.Substring(1).ToString());
                        else
                        {
                            // pokaz();

                            /// 13.02.2020
                            //D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString());
                            D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString() + " and (t1.zone1=0 or t1.zone1 is null) union all SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.zone1=1 and exists(select 1 from t_g_cashcentre t22 where t22.tipsp1=2 and t22.id=t1.user_komu and exists(select 1 from t_g_cashcentre  t33 where t33.tipsp1=3 and t22.id=t33.id_parent and t1.user_otkogo<>" + idpols.ToString() + " and ltrim(rtrim(t33.name2))='" + IP.ToString() + "'))");
                            /// 13.02.2020
                            bindingSource.DataSource = D3.Tables[0];
                            dataGridView2.DataSource = bindingSource;

                            button5_Click(sender, e);

                            MessageBox.Show("Операция выполнена!");
                        }
                    }
                    else
                    {
                        // pokaz();

                        /// 13.02.2020
                        //D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString());
                        D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString() + " and (t1.zone1=0 or t1.zone1 is null) union all SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.zone1=1 and exists(select 1 from t_g_cashcentre t22 where t22.tipsp1=2 and t22.id=t1.user_komu and exists(select 1 from t_g_cashcentre  t33 where t33.tipsp1=3 and t22.id=t33.id_parent and t1.user_otkogo<>" + idpols.ToString() + " and ltrim(rtrim(t33.name2))='" + IP.ToString() + "'))");

                        /// 13.02.2020

                        bindingSource.DataSource = D3.Tables[0];
                        dataGridView2.DataSource = bindingSource;

                        button5_Click(sender, e);

                        MessageBox.Show("Операция выполнена!");
                    }

                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current != null)
                if (((DataRowView)bindingSource.Current)["id"].ToString() != "")
                {

                    ////

                    string value = "";
                    value = Interaction.InputBox("Введите причину отклонения: ", "Причина отклонения");

                    dataBase.Dvden1(value, 0, 3, Convert.ToInt64(((DataRowView)bindingSource.Current)["id"].ToString()));

                    //if (Tmp.InputBox("New document", "New document name:", ref value) == DialogResult.OK)
                    //{
                    //    myDocument.Name = value;
                    //}

                    ////

                    dataBase.Dvden1("", 0, 1, Convert.ToInt64(((DataRowView)bindingSource.Current)["id"].ToString()));
                    // pokaz();

                    /// 13.02.2020
                    //D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString());
                    D3 = dataBase.GetData9("SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.user_komu=" + idpols.ToString() + " and (t1.zone1=0 or t1.zone1 is null) union all SELECT t1.*, (case when status = 1 then 'Передан' when status = 2 then 'Принят' when status = 3 then 'Оклонён' end ) as c1,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_komu) as c2,(select max(t2.user_name) from t_g_user t2 where t2.id=t1.user_otkogo) as c3,tipper  FROM t_g_pered t1 where t1.zone1=1 and exists(select 1 from t_g_cashcentre t22 where t22.tipsp1=2 and t22.id=t1.user_komu and exists(select 1 from t_g_cashcentre  t33 where t33.tipsp1=3 and t22.id=t33.id_parent and t1.user_otkogo<>" + idpols.ToString() + " and ltrim(rtrim(t33.name2))='" + IP.ToString() + "'))");

                    /// 13.02.2020

                    bindingSource.DataSource = D3.Tables[0];
                    dataGridView2.DataSource = bindingSource;

                    button5_Click(sender, e);

                    MessageBox.Show("Операция выполнена!");

                }
        }

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) | (e.KeyCode == Keys.Enter))
            {

                if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                {

                    /////
                    dataGridView3.DataSource = null;
                    /////

                    dataGridView3.Rows.Add(comboBox4.Text, comboBox4.SelectedValue);
                    textBox11.Text = dataGridView3.Rows.Count.ToString();

                    DataRow[] h2 = ((DataTable)D4.Tables[0]).Select("id='" + comboBox4.SelectedValue.ToString() + "'");
                    if (textBox15.Text=="")
                    textBox15.Text = h2[0]["declared_value"].ToString();
                    else
                    textBox15.Text = (Convert.ToDecimal(textBox15.Text) + Convert.ToDecimal(h2[0]["declared_value"].ToString())).ToString();

                    /////17.02.2020

                    comboBox4.SelectedIndex = -1;

                    /////17.02.2020

                }

                dataGridView3.AutoResizeColumns();
            }

           

        }

        private void comboBox5_KeyUp(object sender, KeyEventArgs e)
        {

            
            if ((e.KeyCode == Keys.Escape) | (e.KeyCode == Keys.Enter))
            {

                //if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                  
                dataGridView4.Rows.Add(comboBox5.Text);

                string s1 = "";
                string s2 = comboBox5.Text.Trim().ToString();

                for (int i1= dataGridView3.Rows.Count-1; i1>=0;i1--)
                {
                    s1 = dataGridView3.Rows[i1].Cells["name"].Value.ToString();
                    s1 = s1.Trim();
                    if (s1.ToString()== s2.ToString())
                    {
                        /////
                        if (D7!=null)
                        /////
                        if (D7.Tables[0].Rows.Count>0)
                        {
                            

                            textBox11.Text = dataGridView3.Rows.Count.ToString();
                            DataRow[] h2 = ((DataTable)D7.Tables[0]).Select("name='" + comboBox5.Text.Trim().ToString() + "'");

                            if (textBox15.Text != "")
                                textBox15.Text = (Convert.ToDecimal(textBox15.Text) - Convert.ToDecimal(h2[0]["declared_value"].ToString())).ToString();

                            dataGridView3.Rows.Remove(dataGridView3.Rows[i1]);
                            textBox11.Text = dataGridView3.Rows.Count.ToString();

                        }
                        

                    }


                }

                /////17.02.2020

                comboBox5.SelectedIndex = -1;

                /////17.02.2020
                
                dataGridView4.AutoResizeColumns();

            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            D2 = dataBase.GetData9("SELECT t1.id, t1.branch_name as user_name  FROM t_g_cashcentre t1 where tipsp1=2");
            else
            D2 = dataBase.GetData9("select * from t_g_user where id <>" + idpols.ToString());

            
            comboBox1.DataSource = D2.Tables[0];
            comboBox1.SelectedIndex = -1;


        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                D6 = dataBase.GetData9("SELECT t1.id, t1.branch_name as user_name  FROM t_g_cashcentre t1 where tipsp1=2");
            else
                D6 = dataBase.GetData9("select * from t_g_user where id <>" + idpols.ToString());


            comboBox3.DataSource = D6.Tables[0];
            comboBox3.SelectedIndex = -1;
        }

        private void dataGridView7_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) & (e.RowIndex != dataGridView7.Rows.Count - 1))
            {



                if (dataGridView7.Rows[e.RowIndex].Cells["Key"].Value != null)
                    if (dataGridView7.Rows[e.RowIndex].Cells["Key"].Value.ToString() != String.Empty)
                    {



                        DataRow[] searchedRows = ((DataTable)D11.Tables[0]).Select("Nomin1='" + dataGridView7.Rows[e.RowIndex].Cells["Nomin"].Value.ToString()+ "' and curr_id="+dataGridView7.Rows[e.RowIndex].Cells["V1"].Value.ToString()//and Kol1>0"
                            );

                        int icol1 = Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Kol3"].Value.ToString());

                        int icol2 = 0;


                        foreach (DataRow r in searchedRows)
                        {
                            if (r["Kol1"].ToString().Trim()!="")
                            icol2 = icol2 + Convert.ToInt32(r["Kol1"]);

                          

                        }

                        if (icol2 + Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Key"].Value) > icol1)
                        {

                            MessageBox.Show("Превышено наличие по номиналу! Общее число по номиналу - "+dataGridView7.Rows[e.RowIndex].Cells["Kol3"].Value.ToString());

                        }
                        else
                        {

                            if ( DBNull.Value.Equals(dataGridView7.Rows[e.RowIndex].Cells["Kol"].Value))
                            dataGridView7.Rows[e.RowIndex].Cells["Kol"].Value = Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Key"].Value);
                            else
                            dataGridView7.Rows[e.RowIndex].Cells["Kol"].Value = Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Kol"].Value) + Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Key"].Value);
                            

                            dataGridView7.Rows[e.RowIndex].Cells["Suma"].Value = Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Kol"].Value) * Convert.ToInt32(dataGridView7.Rows[e.RowIndex].Cells["Nomin"].Value);


                            int ikol1 = 0;
                            int isum1 = 0;

                            for (int i1 = 0; i1 < dataGridView7.Rows.Count - 1; i1++)
                            {

                                if (!DBNull.Value.Equals(dataGridView7.Rows[i1].Cells["Kol"].Value))
                                {
                                    ikol1 = ikol1 + Convert.ToInt32(dataGridView7.Rows[i1].Cells["Kol"].Value);
                                    isum1 = isum1 + Convert.ToInt32(dataGridView7.Rows[i1].Cells["Suma"].Value);
                                }



                            }

                            dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Kol"].Value = ikol1;
                            dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Suma"].Value = isum1;
                        }

                       

                        
                        dataGridView7.Rows[e.RowIndex].Cells["Key"].Value = String.Empty;
                    }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          //  timer1.Enabled = false;
          //  dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells["Key"].Value = "ИТОГО:";
        }
    }
}
