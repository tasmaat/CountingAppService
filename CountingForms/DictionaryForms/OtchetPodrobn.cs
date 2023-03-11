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
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;

namespace CountingForms.DictionaryForms
{
    public partial class OtchetPodrobn : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        public long counting_vub;
        public int vub1;

        private MSDataBase dataBase = new MSDataBase();

        private DataSet D1 = null;
        private DataSet D2 = null;

        private DataSet D3 = null;
        private DataSet D4 = null;
        private DataSet D5 = null;
        private DataSet D6 = null;

        private BindingSource bindingSource;

        private int tekvub;


        public OtchetPodrobn()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void OtchetPodrobn_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            dataBase.Connect();

            bindingSource = new BindingSource();


            // DataSet D1 = new DataSet();

            /*
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            */

            /*
            if (vub1.ToString() == "1")
            {
                panel1.Visible = false;

             }
            else
*/
            {

                D3 = dataBase.GetData9("select * from t_g_bags");
                D4 = dataBase.GetData9("select * from t_g_user"); 
                D5 = dataBase.GetData9("select * from t_g_cashcentre"); 
                D6 = dataBase.GetData9("select * from t_g_encashpoint");

                comboBox1.Text = "";
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                comboBox1.DataSource = D3.Tables[0];
                comboBox1.SelectedIndex = -1;

                comboBox2.Text = "";
                comboBox2.DataSource = null;
                comboBox2.Items.Clear();
                comboBox2.DisplayMember = "user_name";
                comboBox2.ValueMember = "id";
                comboBox2.DataSource = D4.Tables[0];
                comboBox2.SelectedIndex = -1;

                comboBox3.Text = "";
                comboBox3.DataSource = null;
                comboBox3.Items.Clear();
                comboBox3.DisplayMember = "branch_name";
                comboBox3.ValueMember = "id";
                comboBox3.DataSource = D5.Tables[0];
                comboBox3.SelectedIndex = -1;

                comboBox4.Text = "";
                comboBox4.DataSource = null;
                comboBox4.Items.Clear();
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "id";
                comboBox4.DataSource = D6.Tables[0];
                comboBox4.SelectedIndex = -1;

            }

        }
        
        private void DataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;

            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;

            this.dataGridView1.Invalidate(rtHeader);

        }

        void dataGridView1_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;

            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;

            this.dataGridView1.Invalidate(rtHeader);


        }



        void dataGridView1_Paint(object sender, PaintEventArgs e)

        {
            
            for (int j = 0; j < 15;
                  j++
                )
            {
               

                if (j >= 4)
              
                {
                    Rectangle r1 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView1.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;

                    if (j == 4)
                        // if (j == 2)
                        r1.Width = r1.Width + 4 * w2 - 2;
                    else
                        r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    
                    if (j == 4)
                        s1 = "Заявлено";
                  
                    if (j == 8)
                        s1 = "Общее по пересчёту";
                   
                    if (j == 10)
                        s1 = "По номиналу с пересчётом";
                   
                    if (j == 12)
                        
                        s1 = "По номиналу без пересчёта";
                   
                    if (j == 14)
                        s1 = "По номиналу с машины";

                    e.Graphics.DrawString(s1,

                        this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);
                   
                    if (j == 4)
                        j += 2;
                   
                    j += 1;


                }
               


            }

            
        }



       

        private void DataGridView2_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView2.DisplayRectangle;

            rtHeader.Height = this.dataGridView2.ColumnHeadersHeight / 2;

            this.dataGridView2.Invalidate(rtHeader);

        }

        void dataGridView2_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView2.DisplayRectangle;

            rtHeader.Height = this.dataGridView2.ColumnHeadersHeight / 2;

            this.dataGridView2.Invalidate(rtHeader);


        }



        void dataGridView2_Paint(object sender, PaintEventArgs e)

        {
            for (int j = 0; j < 13;
                j++
                )
            {

                if (j >= 7)
                {
                    Rectangle r1 = this.dataGridView2.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView2.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;


                    r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView2.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    if (j == 7)
                        s1 = "Без пересчёта";
                    if (j == 9)
                        s1 = "С пересчётом";
                    if (j == 11)
                        s1 = "С машины";

                    e.Graphics.DrawString(s1,

                        this.dataGridView2.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);

                    j += 1;


                }



            }


        }

        private void vubot1(int kl23)
        {

            ////
            tekvub = kl23;
            ////

            /*
            dataGridView2.Paint -= new PaintEventHandler(dataGridView2_Paint);



            dataGridView2.Scroll -= new ScrollEventHandler(dataGridView2_Scroll);

            dataGridView2.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);


            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView2.ColumnHeadersHeight = 18;

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;



            dataGridView1.Paint -= new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll -= new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.ColumnHeadersHeight = 18;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            dataGridView1.AutoGenerateColumns = false;
            */

            ////

            /*
            dataGridView1.Columns.Add("Mesh", "Сумка");
            dataGridView1.Columns["Mesh"].Visible = true;
            dataGridView1.Columns["Mesh"].ReadOnly = true;
            dataGridView1.Columns["Mesh"].Width = 100;
            dataGridView1.Columns["Mesh"].DataPropertyName = "Mesh1";
            dataGridView1.Columns.Add("Kart", "Карта");
            dataGridView1.Columns["Kart"].Visible = true;
            dataGridView1.Columns["Kart"].ReadOnly = true;
            dataGridView1.Columns["Kart"].Width = 100;
            dataGridView1.Columns["Kart"].DataPropertyName = "Kart1";
            */
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("Oper", "Оператор");
            dataGridView1.Columns["Oper"].Visible = true;
            dataGridView1.Columns["Oper"].ReadOnly = true;
            dataGridView1.Columns["Oper"].Width = 100;
            dataGridView1.Columns["Oper"].DataPropertyName = "Oper1";
            dataGridView1.Columns.Add("Dat", "Дата изменения");
            dataGridView1.Columns["Dat"].Visible = true;
            dataGridView1.Columns["Dat"].ReadOnly = true;
            dataGridView1.Columns["Dat"].Width = 100;
            dataGridView1.Columns["Dat"].DataPropertyName = "Dat1";
            ////

            dataGridView1.Columns.Add("Centr1", "Кассовый центр");
            dataGridView1.Columns["Centr1"].Visible = true;
            dataGridView1.Columns["Centr1"].ReadOnly = true;
            dataGridView1.Columns["Centr1"].Width = 100;
            dataGridView1.Columns["Centr1"].DataPropertyName = "Centr1";

            dataGridView1.Columns.Add("Valut", "Валюта");
            dataGridView1.Columns["Valut"].Visible = true;
            dataGridView1.Columns["Valut"].ReadOnly = true;
            dataGridView1.Columns["Valut"].Width = 100;
            dataGridView1.Columns["Valut"].DataPropertyName = "Valut1";


            dataGridView1.Columns.Add("Col1", "Листов");
            dataGridView1.Columns["Col1"].Visible = true;
            dataGridView1.Columns["Col1"].ReadOnly = true;

            dataGridView1.Columns["Col1"].Width = 100;

            dataGridView1.Columns["Col1"].DataPropertyName = "Col1";


            dataGridView1.Columns.Add("Sum1", "Сумма");
            dataGridView1.Columns["Sum1"].Visible = true;
            dataGridView1.Columns["Sum1"].ReadOnly = true;

            dataGridView1.Columns["Sum1"].Width = 100;

            dataGridView1.Columns["Sum1"].DataPropertyName = "Sum1";

            

            /*
            dataGridView1.Columns.Add("Sumzajvn", "Сумма ном-лов");
            dataGridView1.Columns["Sumzajvn"].Visible = true;
            dataGridView1.Columns["Sumzajvn"].ReadOnly = true;

            dataGridView1.Columns["Sumzajvn"].Width = 100;

            dataGridView1.Columns["Sumzajvn"].DataPropertyName = "Sumzajvn1";


            dataGridView1.Columns.Add("Colper", "Листов ");
            dataGridView1.Columns["Colper"].Visible = true;
            dataGridView1.Columns["Colper"].ReadOnly = true;

            dataGridView1.Columns["Colper"].Width = 100;

            dataGridView1.Columns["Colper"].DataPropertyName = "Colper1";


            dataGridView1.Columns.Add("Sumpern", "Сумма ");
            dataGridView1.Columns["Sumpern"].Visible = true;
            dataGridView1.Columns["Sumpern"].ReadOnly = true;

            dataGridView1.Columns["Sumpern"].Width = 100;

            dataGridView1.Columns["Sumpern"].DataPropertyName = "Sumpern1";



            dataGridView1.Columns.Add("col2", "Листов");
            dataGridView1.Columns["col2"].Visible = true;
            dataGridView1.Columns["col2"].ReadOnly = true;
            dataGridView1.Columns["col2"].Width = 100;
            dataGridView1.Columns["col2"].DataPropertyName = "col2";


            dataGridView1.Columns.Add("col3", "Сумма ");
            dataGridView1.Columns["col3"].Visible = true;
            dataGridView1.Columns["col3"].ReadOnly = true;
            dataGridView1.Columns["col3"].Width = 100;
            dataGridView1.Columns["col3"].DataPropertyName = "col3";


            dataGridView1.Columns.Add("col4", "Листов ");
            dataGridView1.Columns["col4"].Visible = true;
            dataGridView1.Columns["col4"].ReadOnly = true;
            dataGridView1.Columns["col4"].Width = 100;
            dataGridView1.Columns["col4"].DataPropertyName = "col4";


            dataGridView1.Columns.Add("col5", "Сумма ");
            dataGridView1.Columns["col5"].Visible = true;
            dataGridView1.Columns["col5"].ReadOnly = true;
            dataGridView1.Columns["col5"].Width = 100;
            dataGridView1.Columns["col5"].DataPropertyName = "col5";


            dataGridView1.Columns.Add("col6", "Листов ");
            dataGridView1.Columns["col6"].Visible = true;
            dataGridView1.Columns["col6"].ReadOnly = true;
            dataGridView1.Columns["col6"].Width = 100;
            dataGridView1.Columns["col6"].DataPropertyName = "col6";


            dataGridView1.Columns.Add("col7", "Сумма ");
            dataGridView1.Columns["col7"].Visible = true;
            dataGridView1.Columns["col7"].ReadOnly = true;
            dataGridView1.Columns["col7"].Width = 100;
            dataGridView1.Columns["col7"].DataPropertyName = "col7";
            */


            dataGridView2.AutoGenerateColumns = false;


            ////
            /*
            dataGridView2.Columns.Add("Mesh", "Сумка");
            dataGridView2.Columns["Mesh"].Visible = true;
            dataGridView2.Columns["Mesh"].ReadOnly = true;
            dataGridView2.Columns["Mesh"].Width = 100;
            dataGridView2.Columns["Mesh"].DataPropertyName = "Mesh1";

            dataGridView2.Columns["Mesh"].Frozen = true;
            */

            dataGridView2.Columns.Add("Oper", "Оператор");
            dataGridView2.Columns["Oper"].Visible = true;
            dataGridView2.Columns["Oper"].ReadOnly = true;
            dataGridView2.Columns["Oper"].Width = 100;
            dataGridView2.Columns["Oper"].DataPropertyName = "Oper1";

            dataGridView2.Columns["Oper"].Frozen = true;

            dataGridView2.Columns.Add("Dat", "Дата изменения");
            dataGridView2.Columns["Dat"].Visible = true;
            dataGridView2.Columns["Dat"].ReadOnly = true;
            dataGridView2.Columns["Dat"].Width = 100;
            dataGridView2.Columns["Dat"].DataPropertyName = "Dat1";

            dataGridView2.Columns["Dat"].Frozen = true;
            ////

            /*
            dataGridView2.Columns.Add("Key-In", "Ввод");
            dataGridView2.Columns["Key-In"].Visible = true;
            dataGridView2.Columns["Key-In"].Width = 65;


            dataGridView2.Columns["Key-In"].Frozen = true;
            */

            /*
            dataGridView2.Columns.Add("Card", "Карта");
            dataGridView2.Columns["Card"].Visible = true;
            dataGridView2.Columns["Card"].ReadOnly = true;
            dataGridView2.Columns["Card"].DataPropertyName = "Card1";


            dataGridView2.Columns["Card"].Frozen = true;
            */

            dataGridView2.Columns.Add("Nomin", "Ном-л");



            dataGridView2.Columns["Nomin"].Visible = true;
            dataGridView2.Columns["Nomin"].ReadOnly = true;
            dataGridView2.Columns["Nomin"].DataPropertyName = "Nomin1";


            dataGridView2.Columns["Nomin"].Frozen = true;

            dataGridView2.Columns.Add("Sost", "Сост");


            dataGridView2.Columns["Sost"].Visible = true;
            dataGridView2.Columns["Sost"].ReadOnly = true;
            dataGridView2.Columns["Sost"].DataPropertyName = "Sost1";

            dataGridView2.Columns["Sost"].Frozen = true;

            dataGridView2.Columns.Add("Valut", "Валюта");
            dataGridView2.Columns["Valut"].Visible = true;
            dataGridView2.Columns["Valut"].ReadOnly = true;
            dataGridView2.Columns["Valut"].Width = 100;
            dataGridView2.Columns["Valut"].DataPropertyName = "Valut1";

            dataGridView2.Columns["Valut"].Frozen = true;

            dataGridView2.Columns.Add("Kolbp", "Листов");
            dataGridView2.Columns["Kolbp"].Visible = true;
            dataGridView2.Columns["Kolbp"].ReadOnly = true;
            dataGridView2.Columns["Kolbp"].DataPropertyName = "Kolbp1";


            dataGridView2.Columns.Add("Sumbp", "Сумма");
            dataGridView2.Columns["Sumbp"].Visible = true;
            dataGridView2.Columns["Sumbp"].ReadOnly = true;
            dataGridView2.Columns["Sumbp"].DataPropertyName = "Sumbp1";


            //////19.02.2020
            dataGridView1.Columns["Col1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Sum1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Kolbp"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Sumbp"].DefaultCellStyle.Format = "### ### ### ###";
            //////19.02.2020



            /*
            dataGridView2.Columns.Add("Kolp", "Листов ");
            dataGridView2.Columns["Kolp"].Visible = true;
            dataGridView2.Columns["Kolp"].ReadOnly = true;
            dataGridView2.Columns["Kolp"].DataPropertyName = "Kolp1";


            dataGridView2.Columns.Add("Sump", "Сумма ");
            dataGridView2.Columns["Sump"].Visible = true;
            dataGridView2.Columns["Sump"].ReadOnly = true;
            dataGridView2.Columns["Sump"].DataPropertyName = "Sump1";


            dataGridView2.Columns.Add("Kolm", "Листов ");
            dataGridView2.Columns["Kolm"].Visible = true;
            dataGridView2.Columns["Kolm"].ReadOnly = true;
            dataGridView2.Columns["Kolm"].DataPropertyName = "Kolm1";


            dataGridView2.Columns.Add("Summ", "Сумма ");
            dataGridView2.Columns["Summ"].Visible = true;
            dataGridView2.Columns["Summ"].ReadOnly = true;
            dataGridView2.Columns["Summ"].DataPropertyName = "Summ1";

            dataGridView2.Columns.Add("Sumob", "Общая сумма");
            dataGridView2.Columns["Sumob"].Visible = true;
            dataGridView2.Columns["Sumob"].ReadOnly = true;
            dataGridView2.Columns["Sumob"].DataPropertyName = "Sumob1";
            */






            /*

            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView2.ColumnHeadersHeight = this.dataGridView2.ColumnHeadersHeight * 3;

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



            dataGridView2.Paint += new PaintEventHandler(dataGridView2_Paint);



            dataGridView2.Scroll += new ScrollEventHandler(dataGridView2_Scroll);

            dataGridView2.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);



            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 3;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;


            dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);
            */

            /*
            if (vub1.ToString() == "1")
            {
                if (kl23 == 1)
                    //  D1 = dataBase.GetData9("select  t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + counting_vub.ToString() + "'  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) where t1.id = '" + counting_vub.ToString() + "'  group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate, t1.id,t3.id order by t1.id,t3.id,t11.user_name");

                    D1 = dataBase.GetData9("select c1,c2,	Mesh1,	Kart1,	Oper1,	Dat1,	Valut1,	sum(Sumzajv1) as Sumzajv1,	sum(Sumper1) as Sumper1,	sum(Sumzajvn1) as Sumzajvn1,	sum(Sumpern1) as Sumpern1,	sum(Colzajv1) as Colzajv1,	sum(Colper1) as Colper1,	sum(col2) as col2,	sum(col3) as col3,	sum(col4) as col4,	sum(col5) as col5,	sum(col6) as col6,	sum(col7) as col7 from (select  t1.id as c1, t3.id as c2, t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1, cast(t2.fact_value as float) Sumper1, cast(t8.Sumzajvn1 as float) as Sumzajvn1, sum(cast(t5.fact_value as float)) Sumpern1, t8.Colzajv1, sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + counting_vub.ToString() + "'  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) where t1.id = '" + counting_vub.ToString() + "' and not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t5.id_counting and t77.id_denomination = t5.id_denomination and t77.id_condition = t5.id_condition) group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate, t1.id,t3.id union all select  t1.id as c1,t3.id as c2,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) inner join t_g_dvdeneg t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination) inner join t_g_cards as t10 on(t10.id = t5.id_card)   left join t_g_user t11 on(t5.user1 = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + counting_vub.ToString() + "'  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) where t1.id = '" + counting_vub.ToString() + "' group by t3.curr_code, t2.declared_value, t2.fact_value,t10.name ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t11.user_name,t1.lastupdate, t1.id,t3.id) t33 group by c1, c2, Mesh1, Kart1, Oper1, Dat1, Valut1 order by c1,c2,Oper1");
                else
                    D1 = dataBase.GetData9("select  t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom_trace t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + counting_vub.ToString() + "'  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) where t1.id = '" + counting_vub.ToString() + "'  group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate, t1.id,t3.id order by t1.id,t3.id,t11.user_name");


            }
            else
            */
            {


                String s1 = "";
                String sql1 = "";

                if (kl23 == 1)
                    // sql1 = "select t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency)  left join t_g_account t18 on (t2.id_account=t18.id) left join t_g_clienttocc t19 on (t18.id_clienttocc=t19.id)  {0} group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id order by t1.id,t3.id,t11.user_name";
                    //   sql1 = "select c3,c4,c5,c6,c1,c2,	Mesh1,	Kart1,	Oper1,	Dat1,	Valut1,	sum(Sumzajv1) as Sumzajv1,	sum(Sumper1) as Sumper1,	sum(Sumzajvn1) as Sumzajvn1,	sum(Sumpern1) as Sumpern1,	sum(Colzajv1) as Colzajv1,	sum(Colper1) as Colper1,	sum(col2) as col2,	sum(col3) as col3,	sum(col4) as col4,	sum(col5) as col5,	sum(col6) as col6,	sum(col7) as col7 from (select t9.id as c3,t11.id as c4,t19.id_cashcentre as c5,t18.id_encashpoint as c6, t1.id c1,t3.id c2,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency)  left join t_g_account t18 on (t2.id_account=t18.id) left join t_g_clienttocc t19 on (t18.id_clienttocc=t19.id) where   not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting=t5.id_counting and t77.id_denomination=t5.id_denomination and t77.id_condition=t5.id_condition) group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id,t9.id ,t11.id ,t19.id_cashcentre ,t18.id_encashpoint union all select t9.id as c3,t11.id as c4,t19.id_cashcentre as c5,t18.id_encashpoint as c6,t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) inner join t_g_dvdeneg t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination) inner join t_g_cards as t10 on(t10.id = t5.id_card)   left join t_g_user t11 on(t5.user1 = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency)  left join t_g_account t18 on (t2.id_account=t18.id) left join t_g_clienttocc t19 on (t18.id_clienttocc=t19.id) group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id,t9.id ,t11.id ,t19.id_cashcentre ,t18.id_encashpoint  ) t33 {0} group by c1,c2,	Mesh1,	Kart1,	Oper1,	Dat1,	Valut1, c3,c4,c5,c6 order by c1,c2,Oper1";
                    sql1 = "Select * from (select t33.user_name as Oper1,t33.name as Valut1,t33.branch_name as Centr1,t33.c1 as Col1,cast(t33.c2 as float) as Sum1, (select max(t2.lastupdate) from t_g_cash t2 inner join t_g_denomination t6 on (t2.id_denomination = t6.id)  where t2.id_user = t33.id_user and t2.id_denomination = t6.id and t2.id_cashcentre = t33.id_cashcentre and t33.id_currency = t6.id_currency) as Dat1,t33.id_user, t33.id_cashcentre from(select t3.id_currency, t1.id_cashcentre,t2.user_name, t1.id_user, t4.name, t5.branch_name, sum(t1.count) as c1, sum(t1.count * t3.value) as c2 from t_g_cash t1 inner join t_g_user t2 on (t1.id_user = t2.id) inner join t_g_denomination t3 on (t1.id_denomination = t3.id) inner join t_g_currency t4 on (t3.id_currency = t4.id) inner join t_g_cashcentre t5 on (t1.id_cashcentre = t5.id) group by t1.id_user, t4.name, t5.branch_name, t3.id_currency, t1.id_cashcentre,t2.user_name) as t33 ) as t44 {0}";


                else
                    //  sql1 = "select t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom_trace t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency)  left join t_g_account t18 on (t2.id_account=t18.id) left join t_g_clienttocc t19 on (t18.id_clienttocc=t19.id)  {0} group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id order by t1.id,t3.id,t11.user_name";
                    sql1 = "select * from (select t3.id_currency, t1.id_cashcentre,t2.user_name as Oper1, t1.id_user, t4.name as Valut1, t5.branch_name as Centr1,t1.count as Col1, cast(t1.count* t3.value as float) as Sum1,t1.lastupdate as Dat1 from t_g_cash_trace t1 inner join t_g_user t2 on (t1.id_user = t2.id) inner join t_g_denomination t3 on (t1.id_denomination = t3.id) inner join t_g_currency t4 on (t3.id_currency = t4.id) inner join t_g_cashcentre t5 on (t1.id_cashcentre = t5.id)) t33 {0} order by Dat1";



              //  if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                    //   s1 = " t9.id =" + comboBox1.SelectedValue.ToString();
               //     s1 = " c3 =" + comboBox1.SelectedValue.ToString();

                if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        // s1 = " t11.id =" + comboBox2.SelectedValue.ToString();
                        s1 = " id_user =" + comboBox2.SelectedValue.ToString();
                    else
                        // s1 = s1.ToString() + " and  t11.id =" + comboBox2.SelectedValue.ToString();
                        s1 = s1.ToString() + " and  id_user =" + comboBox2.SelectedValue.ToString();

                }

                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {

                    if (s1.ToString().Trim() == "")
                        //   s1 = " t19.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                        s1 = " id_cashcentre =" + comboBox3.SelectedValue.ToString();
                    else
                        //s1 = s1.ToString() + " and  t19.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                        s1 = s1.ToString() + " and  id_cashcentre =" + comboBox3.SelectedValue.ToString();

                }

                /*
                if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        // s1 = " t18.id_encashpoint =" + comboBox4.SelectedValue.ToString();
                        s1 = " c6 =" + comboBox4.SelectedValue.ToString();
                    else
                        //s1 = s1.ToString() + " and  t18.id_encashpoint =" + comboBox4.SelectedValue.ToString();
                        s1 = s1.ToString() + " and  c6 =" + comboBox4.SelectedValue.ToString();

                }
                */

                if ((textBox1.Text.ToString().Trim() != "") & (textBox2.Text.ToString().Trim() != ""))
                {

                    /////
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

                    //////

                    if (s1.ToString().Trim() == "")
                        //    s1 = " t1.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";
                        s1 = " cast(Dat1 as date)  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";

                    else
                        // s1 = s1.ToString() + " and t1.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";
                        s1 = s1.ToString() + " and cast(Dat1 as date)  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";



                }


                if (s1.ToString().Trim() != "")
                    s1 = " where " + s1.ToString();


                sql1 = String.Format(sql1, s1);
                D1 = dataBase.GetData9(sql1);




            }

            //D1 = dataBase.GetData9("select t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id order by t1.id,t3.id,t11.user_name");

            bindingSource.DataSource = D1.Tables[0];
            dataGridView1.DataSource = bindingSource;
            // dataGridView1.DataSource = D1.Tables[0];

            /*
            if (vub1.ToString() == "1")
            {
                if (kl23 == 1)
                    //   D2 = dataBase.GetData9("select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,t16.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + counting_vub.ToString() + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag=t14.id) left join t_g_user t15 on (t15.id=t2.last_user_update) left join t_g_currency t16 on (t1.id_currency=t16.id)  group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t16.curr_code  order by t14.name,t1.nominal,t2.lastupdate,t15.user_name");
                    D2 = dataBase.GetData9("select * from(select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, t16.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + counting_vub.ToString() + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_currency t16 on(t1.id_currency = t16.id) where not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t2.id_counting and t77.id_denomination = t2.id_denomination and t77.id_condition = t2.id_condition) group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t16.curr_code union all select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, t16.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + counting_vub.ToString() + "' and t1.visible = 0) t1 inner join t_g_dvdeneg t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.user1) left join t_g_currency t16 on(t1.id_currency = t16.id)  group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t16.curr_code ) t33 order by Mesh1, Nomin1, Dat1, Oper1");


                else
                    D2 = dataBase.GetData9("select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,t16.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + counting_vub.ToString() + "' and t1.visible = 0) t1 inner join t_g_counting_denom_trace t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag=t14.id) left join t_g_user t15 on (t15.id=t2.last_user_update) left join t_g_currency t16 on (t1.id_currency=t16.id)  group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t16.curr_code  order by t14.name,t1.nominal,t2.lastupdate,t15.user_name");


            }
            else
            */
            if ((DataRowView)bindingSource.Current != null)
            {

                String s1 = "";
                String sql1 = "";

                if (kl23 == 1)
                    //   sql1 = "select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1,t26.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc,t3.id_encashpoint from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency)  where  t1.visible = 0 ) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) left join t_g_currency t26 on (t1.id_currency=t26.id) {0} group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t26.curr_code order by t2.id_counting,t1.nominal,t2.lastupdate,t15.user_name";
                    //    sql1 = "select * from(select t3.id_bag as c1, t15.id as c2,t16.id_cashcentre as c3,t1.id_encashpoint as c4, t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, t26.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc, t3.id_encashpoint from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency)  where t1.visible = 0 ) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) left join t_g_currency t26 on(t1.id_currency = t26.id) where not exists(select 1 from t_g_dvdeneg t77 where t77.id_counting = t2.id_counting and t77.id_denomination = t2.id_denomination and t77.id_condition = t2.id_condition) group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t26.curr_code ,t3.id_bag , t15.id ,t16.id_cashcentre  ,t1.id_encashpoint union all select t3.id_bag as c1, t15.id as c2,t16.id_cashcentre as c3,t1.id_encashpoint as c4,t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, t26.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc, t3.id_encashpoint from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency)  where t1.visible = 0 ) t1 inner join t_g_dvdeneg t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.user1) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) left join t_g_currency t26 on(t1.id_currency = t26.id)  group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t26.curr_code ,t3.id_bag , t15.id ,t16.id_cashcentre  ,t1.id_encashpoint ) t33  {0} order by Mesh1, Nomin1, Dat1, Oper1";
                    sql1 = "select t3.id_currency,t1.id_cashcentre, t2.user_name as Oper1,t3.name as Nomin1,t1.id_user,t4.name as Valut1,t5.branch_name, t1.count as Kolbp1 ,cast(t1.count*t3.value as float) as Sumbp1, t1.lastupdate as Dat1,t6.name as Sost1 from t_g_cash t1 inner join t_g_user t2 on (t1.id_user=t2.id) inner join t_g_denomination t3 on (t1.id_denomination=t3.id) inner join t_g_currency t4 on (t3.id_currency=t4.id) inner join t_g_cashcentre t5 on (t1.id_cashcentre=t5.id) inner join t_g_condition t6 on (t1.id_cond=t6.id) {0} order by t3.id_currency,t3.value,t6.name";


                else
                    //sql1 = "select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1,t26.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc,t3.id_encashpoint from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency)  where  t1.visible = 0 ) t1 inner join t_g_counting_denom_trace t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) left join t_g_currency t26 on (t1.id_currency=t26.id) {0} group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t26.curr_code order by t2.id_counting,t1.nominal,t2.lastupdate,t15.user_name";
                    sql1 = "select t3.id_currency,t1.id_cashcentre, t2.user_name as Oper1,t3.name as Nomin1,t1.id_user,t4.name as Valut1,t5.branch_name, t1.count as Kolbp1 ,cast(t1.count* t3.value as float) as Sumbp1, t1.lastupdate as Dat1,t6.name as Sost1 from t_g_cash_trace t1 inner join t_g_user t2 on (t1.id_user = t2.id) inner join t_g_denomination t3 on (t1.id_denomination = t3.id) inner join t_g_currency t4 on (t3.id_currency = t4.id) inner join t_g_cashcentre t5 on (t1.id_cashcentre = t5.id) inner join t_g_condition t6 on (t1.id_cond = t6.id) {0} order by t3.id_currency,t3.value,t6.name ";

                           s1 = s1 + " t1.id_user = " + ((DataRowView)bindingSource.Current)["id_user"].ToString();

               // if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                    // s1 = " t3.id_bag =" + comboBox1.SelectedValue.ToString();
                    //   s1 = " c1 =" + comboBox1.SelectedValue.ToString();
               //     s1 = s1.ToString() + " and c1 =" + comboBox1.SelectedValue.ToString();

                if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        // s1 = " t15.id =" + comboBox2.SelectedValue.ToString();
                        s1 = " t1.id_user =" + comboBox2.SelectedValue.ToString();
                    else
                        //s1 = s1.ToString() + " and  t15.id =" + comboBox2.SelectedValue.ToString();
                        s1 = s1.ToString() + " and  t1.id_user =" + comboBox2.SelectedValue.ToString();

                }

                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {

                    if (s1.ToString().Trim() == "")
                        // s1 = " t16.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                        s1 = " t1.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                    else
                        // s1 = s1.ToString() + " and  t16.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                        s1 = s1.ToString() + " and  t1.id_cashcentre =" + comboBox3.SelectedValue.ToString();

                }

                /*
                if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        //  s1 = " t1.id_encashpoint =" + comboBox4.SelectedValue.ToString();
                        s1 = " c4 =" + comboBox4.SelectedValue.ToString();
                    else
                        //s1 = s1.ToString() + " and  t1.id_encashpoint =" + comboBox4.SelectedValue.ToString();
                        s1 = s1.ToString() + " and  c4 =" + comboBox4.SelectedValue.ToString();

                }
                */

                if ((textBox1.Text.ToString().Trim() != "") & (textBox2.Text.ToString().Trim() != ""))
                {

                    /////
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

                    //////

                    if (s1.ToString().Trim() == "")
                        // s1 = " t2.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";
                        s1 = " cast(t1.lastupdate as date) between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";

                    else
                        // s1 = s1.ToString() + " and t2.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";
                        s1 = s1.ToString() + " and cast(t1.lastupdate as date)  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";



                }


                if (s1.ToString().Trim() != "")
                    s1 = " where " + s1.ToString();

                // sql1 = String.Format(strzapros, s1, s2);

                // D2 = dataBase.GetData9("select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) ) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) {0} group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate order by t2.id_counting,t1.nominal,t2.lastupdate,t15.user_name");

                sql1 = String.Format(sql1, s1);
                D2 = dataBase.GetData9(sql1);

                dataGridView2.DataSource = D2.Tables[0];

            }


           // dataGridView2.DataSource = D2.Tables[0];

            dataGridView2.AutoResizeColumns();
            dataGridView1.AutoResizeColumns();

           


        }

        private void vubop()
        {

            String s1 = "";
            String sql1 = "";

            if ((DataRowView)bindingSource.Current!=null)
            if (((DataRowView)bindingSource.Current)["id_user"].ToString() != "")
            {

                if (tekvub == 1)
                    sql1 = "select t3.id_currency,t1.id_cashcentre, t2.user_name as Oper1,t3.name as Nomin1,t1.id_user,t4.name as Valut1,t5.branch_name, t1.count as Kolbp1 ,cast(t1.count*t3.value as float) as Sumbp1, t1.lastupdate as Dat1,t6.name as Sost1 from t_g_cash t1 inner join t_g_user t2 on (t1.id_user=t2.id) inner join t_g_denomination t3 on (t1.id_denomination=t3.id) inner join t_g_currency t4 on (t3.id_currency=t4.id) inner join t_g_cashcentre t5 on (t1.id_cashcentre=t5.id) inner join t_g_condition t6 on (t1.id_cond=t6.id) {0} order by t3.id_currency,t3.value,t6.name";


                else
                    sql1 = "select t3.id_currency,t1.id_cashcentre, t2.user_name as Oper1,t3.name as Nomin1,t1.id_user,t4.name as Valut1,t5.branch_name, t1.count as Kolbp1 ,cast(t1.count* t3.value as float) as Sumbp1, t1.lastupdate as Dat1,t6.name as Sost1 from t_g_cash_trace t1 inner join t_g_user t2 on (t1.id_user = t2.id) inner join t_g_denomination t3 on (t1.id_denomination = t3.id) inner join t_g_currency t4 on (t3.id_currency = t4.id) inner join t_g_cashcentre t5 on (t1.id_cashcentre = t5.id) inner join t_g_condition t6 on (t1.id_cond = t6.id) {0} order by t3.id_currency,t3.value,t6.name ";

                s1 = s1 + " t1.id_user = " + ((DataRowView)bindingSource.Current)["id_user"].ToString();

              //  if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
              //      s1 = s1.ToString() + " and c1 =" + comboBox1.SelectedValue.ToString();

                if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")

                        s1 = s1.ToString() + " and  t1.id_user =" + comboBox2.SelectedValue.ToString();

                }

                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {

                    if (s1.ToString().Trim() == "")

                        s1 = s1.ToString() + " and  t1.id_cashcentre =" + comboBox3.SelectedValue.ToString();

                }

                /*
                if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")

                        s1 = s1.ToString() + " and  c4 =" + comboBox4.SelectedValue.ToString();

                }
                */

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
                        s1 = " cast(t1.lastupdate as date between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";

                    else
                        s1 = s1.ToString() + " and cast(t1.lastupdate as date) between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";



                }


                if (s1.ToString().Trim() != "")
                    s1 = " where " + s1.ToString();

                sql1 = String.Format(sql1, s1);
                D2 = dataBase.GetData9(sql1);

                dataGridView2.DataSource = D2.Tables[0];

                dataGridView2.AutoResizeColumns();
               

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            vubot1(1);

            /*
            dataGridView2.Paint -= new PaintEventHandler(dataGridView2_Paint);



            dataGridView2.Scroll -= new ScrollEventHandler(dataGridView2_Scroll);

            dataGridView2.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);

            
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView2.ColumnHeadersHeight = 18;

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;



            dataGridView1.Paint -= new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll -= new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.ColumnHeadersHeight = 18;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            dataGridView1.AutoGenerateColumns = false;

            ////
            dataGridView1.Columns.Add("Mesh", "Сумка");
            dataGridView1.Columns["Mesh"].Visible = true;
            dataGridView1.Columns["Mesh"].ReadOnly = true;
            dataGridView1.Columns["Mesh"].Width = 100;
            dataGridView1.Columns["Mesh"].DataPropertyName = "Mesh1";
            dataGridView1.Columns.Add("Kart", "Карта");
            dataGridView1.Columns["Kart"].Visible = true;
            dataGridView1.Columns["Kart"].ReadOnly = true;
            dataGridView1.Columns["Kart"].Width = 100;
            dataGridView1.Columns["Kart"].DataPropertyName = "Kart1";
            dataGridView1.Columns.Add("Oper", "Оператор");
            dataGridView1.Columns["Oper"].Visible = true;
            dataGridView1.Columns["Oper"].ReadOnly = true;
            dataGridView1.Columns["Oper"].Width = 100;
            dataGridView1.Columns["Oper"].DataPropertyName = "Oper1";
            dataGridView1.Columns.Add("Dat", "Дата");
            dataGridView1.Columns["Dat"].Visible = true;
            dataGridView1.Columns["Dat"].ReadOnly = true;
            dataGridView1.Columns["Dat"].Width = 100;
            dataGridView1.Columns["Dat"].DataPropertyName = "Dat1";
            ////

            dataGridView1.Columns.Add("Valut", "Валюта");
            dataGridView1.Columns["Valut"].Visible = true;
            dataGridView1.Columns["Valut"].ReadOnly = true;
            dataGridView1.Columns["Valut"].Width = 100;
            dataGridView1.Columns["Valut"].DataPropertyName = "Valut1";


            dataGridView1.Columns.Add("Colzajv", "Листов ");
            dataGridView1.Columns["Colzajv"].Visible = true;
            dataGridView1.Columns["Colzajv"].ReadOnly = true;

            dataGridView1.Columns["Colzajv"].Width = 100;

            dataGridView1.Columns["Colzajv"].DataPropertyName = "Colzajv1";


            dataGridView1.Columns.Add("Sumzajv", "Сумма ");
            dataGridView1.Columns["Sumzajv"].Visible = true;
            dataGridView1.Columns["Sumzajv"].ReadOnly = true;

            dataGridView1.Columns["Sumzajv"].Width = 100;

            dataGridView1.Columns["Sumzajv"].DataPropertyName = "Sumzajv1";


            dataGridView1.Columns.Add("Sumzajvn", "Сумма ном-лов");
            dataGridView1.Columns["Sumzajvn"].Visible = true;
            dataGridView1.Columns["Sumzajvn"].ReadOnly = true;

            dataGridView1.Columns["Sumzajvn"].Width = 100;

            dataGridView1.Columns["Sumzajvn"].DataPropertyName = "Sumzajvn1";


            dataGridView1.Columns.Add("Colper", "Листов ");
            dataGridView1.Columns["Colper"].Visible = true;
            dataGridView1.Columns["Colper"].ReadOnly = true;

            dataGridView1.Columns["Colper"].Width = 100;

            dataGridView1.Columns["Colper"].DataPropertyName = "Colper1";


            dataGridView1.Columns.Add("Sumpern", "Сумма ");
            dataGridView1.Columns["Sumpern"].Visible = true;
            dataGridView1.Columns["Sumpern"].ReadOnly = true;

            dataGridView1.Columns["Sumpern"].Width = 100;

            dataGridView1.Columns["Sumpern"].DataPropertyName = "Sumpern1";



            dataGridView1.Columns.Add("col2", "Листов");
            dataGridView1.Columns["col2"].Visible = true;
            dataGridView1.Columns["col2"].ReadOnly = true;
            dataGridView1.Columns["col2"].Width = 100;
            dataGridView1.Columns["col2"].DataPropertyName = "col2";


            dataGridView1.Columns.Add("col3", "Сумма ");
            dataGridView1.Columns["col3"].Visible = true;
            dataGridView1.Columns["col3"].ReadOnly = true;
            dataGridView1.Columns["col3"].Width = 100;
            dataGridView1.Columns["col3"].DataPropertyName = "col3";


            dataGridView1.Columns.Add("col4", "Листов ");
            dataGridView1.Columns["col4"].Visible = true;
            dataGridView1.Columns["col4"].ReadOnly = true;
            dataGridView1.Columns["col4"].Width = 100;
            dataGridView1.Columns["col4"].DataPropertyName = "col4";


            dataGridView1.Columns.Add("col5", "Сумма ");
            dataGridView1.Columns["col5"].Visible = true;
            dataGridView1.Columns["col5"].ReadOnly = true;
            dataGridView1.Columns["col5"].Width = 100;
            dataGridView1.Columns["col5"].DataPropertyName = "col5";


            dataGridView1.Columns.Add("col6", "Листов ");
            dataGridView1.Columns["col6"].Visible = true;
            dataGridView1.Columns["col6"].ReadOnly = true;
            dataGridView1.Columns["col6"].Width = 100;
            dataGridView1.Columns["col6"].DataPropertyName = "col6";


            dataGridView1.Columns.Add("col7", "Сумма ");
            dataGridView1.Columns["col7"].Visible = true;
            dataGridView1.Columns["col7"].ReadOnly = true;
            dataGridView1.Columns["col7"].Width = 100;
            dataGridView1.Columns["col7"].DataPropertyName = "col7";



            dataGridView2.AutoGenerateColumns = false;


            ////
            dataGridView2.Columns.Add("Mesh", "Сумка");
            dataGridView2.Columns["Mesh"].Visible = true;
            dataGridView2.Columns["Mesh"].ReadOnly = true;
            dataGridView2.Columns["Mesh"].Width = 100;
            dataGridView2.Columns["Mesh"].DataPropertyName = "Mesh1";

            dataGridView2.Columns["Mesh"].Frozen = true;

            dataGridView2.Columns.Add("Oper", "Оператор");
            dataGridView2.Columns["Oper"].Visible = true;
            dataGridView2.Columns["Oper"].ReadOnly = true;
            dataGridView2.Columns["Oper"].Width = 100;
            dataGridView2.Columns["Oper"].DataPropertyName = "Oper1";

            dataGridView2.Columns["Oper"].Frozen = true;

            dataGridView2.Columns.Add("Dat", "Дата");
            dataGridView2.Columns["Dat"].Visible = true;
            dataGridView2.Columns["Dat"].ReadOnly = true;
            dataGridView2.Columns["Dat"].Width = 100;
            dataGridView2.Columns["Dat"].DataPropertyName = "Dat1";

            dataGridView2.Columns["Dat"].Frozen = true;
            ////

            
            //dataGridView2.Columns.Add("Key-In", "Ввод");
            //dataGridView2.Columns["Key-In"].Visible = true;
            //dataGridView2.Columns["Key-In"].Width = 65;


           // dataGridView2.Columns["Key-In"].Frozen = true;
            

            dataGridView2.Columns.Add("Card", "Карта");
            dataGridView2.Columns["Card"].Visible = true;
            dataGridView2.Columns["Card"].ReadOnly = true;
            dataGridView2.Columns["Card"].DataPropertyName = "Card1";


            dataGridView2.Columns["Card"].Frozen = true;

            dataGridView2.Columns.Add("Nomin", "Ном-л");



            dataGridView2.Columns["Nomin"].Visible = true;
            dataGridView2.Columns["Nomin"].ReadOnly = true;
            dataGridView2.Columns["Nomin"].DataPropertyName = "Nomin1";


            dataGridView2.Columns["Nomin"].Frozen = true;

            dataGridView2.Columns.Add("Sost", "Сост");


            dataGridView2.Columns["Sost"].Visible = true;
            dataGridView2.Columns["Sost"].ReadOnly = true;
            dataGridView2.Columns["Sost"].DataPropertyName = "Sost1";

            dataGridView2.Columns["Sost"].Frozen = true;

            dataGridView2.Columns.Add("Valut", "Валюта");
            dataGridView2.Columns["Valut"].Visible = true;
            dataGridView2.Columns["Valut"].ReadOnly = true;
            dataGridView2.Columns["Valut"].Width = 100;
            dataGridView2.Columns["Valut"].DataPropertyName = "Valut1";

            dataGridView2.Columns["Valut"].Frozen = true;

            dataGridView2.Columns.Add("Kolbp", "Листов ");
            dataGridView2.Columns["Kolbp"].Visible = true;
            dataGridView2.Columns["Kolbp"].ReadOnly = true;
            dataGridView2.Columns["Kolbp"].DataPropertyName = "Kolbp1";


            dataGridView2.Columns.Add("Sumbp", "Сумма ");
            dataGridView2.Columns["Sumbp"].Visible = true;
            dataGridView2.Columns["Sumbp"].ReadOnly = true;
            dataGridView2.Columns["Sumbp"].DataPropertyName = "Sumbp1";


            dataGridView2.Columns.Add("Kolp", "Листов ");
            dataGridView2.Columns["Kolp"].Visible = true;
            dataGridView2.Columns["Kolp"].ReadOnly = true;
            dataGridView2.Columns["Kolp"].DataPropertyName = "Kolp1";


            dataGridView2.Columns.Add("Sump", "Сумма ");
            dataGridView2.Columns["Sump"].Visible = true;
            dataGridView2.Columns["Sump"].ReadOnly = true;
            dataGridView2.Columns["Sump"].DataPropertyName = "Sump1";


            dataGridView2.Columns.Add("Kolm", "Листов ");
            dataGridView2.Columns["Kolm"].Visible = true;
            dataGridView2.Columns["Kolm"].ReadOnly = true;
            dataGridView2.Columns["Kolm"].DataPropertyName = "Kolm1";


            dataGridView2.Columns.Add("Summ", "Сумма ");
            dataGridView2.Columns["Summ"].Visible = true;
            dataGridView2.Columns["Summ"].ReadOnly = true;
            dataGridView2.Columns["Summ"].DataPropertyName = "Summ1";

            dataGridView2.Columns.Add("Sumob", "Общая сумма");
            dataGridView2.Columns["Sumob"].Visible = true;
            dataGridView2.Columns["Sumob"].ReadOnly = true;
            dataGridView2.Columns["Sumob"].DataPropertyName = "Sumob1";






           

            
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView2.ColumnHeadersHeight = this.dataGridView2.ColumnHeadersHeight * 3;

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            


            dataGridView2.Paint += new PaintEventHandler(dataGridView2_Paint);



            dataGridView2.Scroll += new ScrollEventHandler(dataGridView2_Scroll);

            dataGridView2.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);


            
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 3;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            

            dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);


            if (vub1.ToString() == "1")
                D1 = dataBase.GetData9("select  t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + counting_vub.ToString()+"'  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) where t1.id = '"+ counting_vub.ToString() + "'  group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate, t1.id,t3.id order by t1.id,t3.id,t11.user_name");
            else
            {


                String s1 = "";
                String sql1 = "select t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency)  left join t_g_account t18 on (t2.id_account=t18.id) left join t_g_clienttocc t19 on (t18.id_clienttocc=t19.id)  {0} group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id order by t1.id,t3.id,t11.user_name";

                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                    s1 = " t9.id =" + comboBox1.SelectedValue.ToString();

                if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        s1 = " t11.id =" + comboBox2.SelectedValue.ToString();
                    else
                        s1 = s1.ToString() + " and  t11.id =" + comboBox2.SelectedValue.ToString();

                }

                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {

                    if (s1.ToString().Trim() == "")
                        s1 = " t19.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                    else
                        s1 = s1.ToString() + " and  t19.id_cashcentre =" + comboBox3.SelectedValue.ToString();

                }

                if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        s1 = " t18.id_encashpoint =" + comboBox4.SelectedValue.ToString();
                    else
                        s1 = s1.ToString() + " and  t18.id_encashpoint =" + comboBox4.SelectedValue.ToString();

                }

                if ((textBox1.Text.ToString().Trim() != "") & (textBox2.Text.ToString().Trim() != ""))
                {

                    /////
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

                    //////

                    if (s1.ToString().Trim() == "")
                        s1 = " t1.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";
                    else
                        s1 = s1.ToString() + " and t1.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";



                }


                if (s1.ToString().Trim() != "")
                    s1 = " where " + s1.ToString();

              
                sql1 = String.Format(sql1, s1);
                D1 = dataBase.GetData9(sql1);




            }

            //D1 = dataBase.GetData9("select t1.id,t3.id,t9.name as Mesh1, t10.name as Kart1, t11.user_name as Oper1, t1.lastupdate as Dat1, t3.curr_code Valut1, cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1 ,  cast(t8.Sumzajvn1 as float) as Sumzajvn1 , sum(cast(t5.fact_value as float)) Sumpern1 , t8.Colzajv1 , sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_bags t9 on(t1.id_bag = t9.id)  left join  t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  inner join t_g_cards as t10 on(t10.id = t5.id_card) left join t_g_user t11 on(t5.last_user_update = t11.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6  group by t6.id_counting, t6.id_currency)t8 on(t1.id = t8.id_counting) and(t2.id_currency = t8.id_currency) group by t3.curr_code, t2.declared_value, t2.fact_value ,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov,t9.name,t10.name,t11.user_name,t1.lastupdate,t3.id, t1.id order by t1.id,t3.id,t11.user_name");


            dataGridView1.DataSource = D1.Tables[0];

            if (vub1.ToString() == "1")
                D2 = dataBase.GetData9("select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,t16.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + counting_vub.ToString() + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag=t14.id) left join t_g_user t15 on (t15.id=t2.last_user_update) left join t_g_currency t16 on (t1.id_currency=t16.id)  group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t16.curr_code  order by t14.name,t1.nominal,t2.lastupdate,t15.user_name");
            else
            {

                String s1 = "";
                String sql1 = "select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1,t26.curr_code Valut1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc,t3.id_encashpoint from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency)  where  t1.visible = 0 ) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) left join t_g_currency t26 on (t1.id_currency=t26.id) {0} group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate,t26.curr_code order by t2.id_counting,t1.nominal,t2.lastupdate,t15.user_name";

                if ((comboBox1.SelectedIndex>-1)&(comboBox1.Text.ToString().Trim()!=""))
                s1 = " t3.id_bag =" + comboBox1.SelectedValue.ToString();

                if ((comboBox2.SelectedIndex > -1) & (comboBox2.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim()=="")
                    s1 = " t15.id =" + comboBox2.SelectedValue.ToString();
                    else
                    s1 = s1.ToString()+" and  t15.id =" + comboBox2.SelectedValue.ToString();

                }

                if ((comboBox3.SelectedIndex > -1) & (comboBox3.Text.ToString().Trim() != ""))
                {

                    if (s1.ToString().Trim() == "")
                        s1 = " t16.id_cashcentre =" + comboBox3.SelectedValue.ToString();
                    else
                        s1 = s1.ToString() + " and  t16.id_cashcentre =" + comboBox3.SelectedValue.ToString();

                }

                if ((comboBox4.SelectedIndex > -1) & (comboBox4.Text.ToString().Trim() != ""))
                {
                    if (s1.ToString().Trim() == "")
                        s1 = " t1.id_encashpoint =" + comboBox4.SelectedValue.ToString();
                    else
                        s1 = s1.ToString() + " and  t1.id_encashpoint =" + comboBox4.SelectedValue.ToString();

                }

                if ((textBox1.Text.ToString().Trim() != "") & (textBox2.Text.ToString().Trim() != ""))
                {

                    /////
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

                    //////

                    if (s1.ToString().Trim() == "")
                        s1 = " t2.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";
                    else
                        s1 = s1.ToString() + " and t2.lastupdate  between '" + textBox1.Text.ToString().Trim() + "' and '" + textBox2.Text.ToString().Trim() + "'";



                    }


                if (s1.ToString().Trim() != "")
                 s1 = " where " + s1.ToString();

                // sql1 = String.Format(strzapros, s1, s2);

               // D2 = dataBase.GetData9("select t14.name as Mesh1,t15.user_name as Oper1,t2.lastupdate as Dat1, t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end) as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end) as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end) as float) Summ1,sum(cast(t2.fact_value as float)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom, t3.id_clienttocc from t_g_condition t1, t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) ) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) left join t_g_bags t14 on(t3.id_bag = t14.id) left join t_g_user t15 on(t15.id = t2.last_user_update) left join t_g_clienttocc t16 on(t16.id = t1.id_clienttocc) {0} group by t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),t14.name,t15.user_name,t2.lastupdate order by t2.id_counting,t1.nominal,t2.lastupdate,t15.user_name");

                sql1 = String.Format(sql1, s1);
                D2 = dataBase.GetData9(sql1);



            }


            dataGridView2.DataSource = D2.Tables[0];

            dataGridView2.AutoResizeColumns();
            dataGridView1.AutoResizeColumns();
            */

            //    dataGridView2.DataSource = D2.Tables[0];
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            textBox1.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker2.Value.ToString("dd.MM.yyyy");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

            textBox1.Text = "";
            textBox2.Text = "";

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            vubot1(2);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            vubop();

        }
    }
}
