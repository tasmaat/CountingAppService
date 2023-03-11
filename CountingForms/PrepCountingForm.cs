using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.DictionaryForms;
using CountingForms.Interfaces;
using CountingForms.ParentForms;
using CountingForms.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
//using CountingForms.DictionaryForms;


namespace CountingForms
{
    public partial class PrepCountingForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;
        private string bag_name = "";
        private int id_bag = 0;
        private DataSet clientsDataSet = null;
        private DataSet accountDataSet = null;
        private DataSet encashpointDataSet = null;
        private DataSet dsMulti = null;
        private DataSet curCodeDataSet = null;
        private DataSet detOtchetDataSet = null;
        private DataSet flprovDataSet = null;
        private Int64 id_multi_bag1 = 0;
        //private DataGridView dgAccountDeclaredCopy;
        private DataSet d2,d3 = null;
        private MSDataBase dataBase = new MSDataBase();
        private DataTable dtAutoComplete = null;

        private DataSet cardsDataSet = null;
        private DataSet currencyDataSet = null;
        private DataSet bagdefectfactorDataSet=null;
        private String Num_defect = "";

        private DataSet countingDataSet = null;
        private DataSet cardsDBDataSet = null;
        private DataSet bagsDataSet = null;
        private DataSet bagsDataSet1 = null;
        private DataSet countContentDataSet = null;
        private DataSet countDenomDataSet = null;
        private DataSet correctDataSet =null;

        private string bagsName;
        private string searchString = String.Empty;

        private DataSet accountDataSet1 = null;
        private DataSet clientsDataSet1 = null;

        ////07.11.2019
        private DataSet obchper1 = null;
        private DataSet detper1 = null;
        private DataSet detper3 = null;
        private DataSet currencyDataSet1 = null;
        private DataSet conditionDataSet1 = null;
        private DataSet cardDataSet1 = null;
        private DataSet countContentDataSet1 = null;


        private DataSet clientsDataSet2 = null;
        private DataSet accountDataSet2 = null;
        private DataSet encashpointDataSet2 = null;
        private DataSet bagsDataSet2 = null;
        private DataSet cardDataSet2 = null;
        private DataSet causeDataSet = null;
        private DataSet causeDiscDataSet = null;
        private DataSet dataSet = null;
        private DataSet dataSet2 = null;
        private DataSet userBalansDataSet = null;

        private string sort = "fl_prov";
        ////07.11.2019

        /// 15.11.2019
        private DataSet pechat3 = null;
        /// 15.11.2019

        /// 19.11.2019
        private static System.Timers.Timer aTimer;
        /// 19.11.2019

        /////20.11.2019
        
        private string dnachism = "";
       

        /////20.11.2019

        /// 21.11.2019

        private string qr_bin="";
        private string qr_data="";
        private string qr_nummech="";
        private string qr_numplom="";
        private string qr_otprav="";
        private string qr_kbe="";
        private string qr_poluch="";
        private string qr_kontr="";
        private string qr_kass="";
        private string qr_vidoper="";
        private string qr_knp="";
        private string qr_numgr="";
        private string qr_poslsh="";
        private string qr_poslsum="";
        private string qr_poslval="";

        private string id_marsh ="";
        private string num_marsh = "";

        private DataSet dsmarsh = null;

        private string fileobrabst1 = "";

        private DataSet dsakt = null;
      
        private DataSet obchper2 = null;

        private DataSet obchper3 = null;


        private DataSet grclienDataSet1 = null;

        private Int64 counting_id;

        private System.Int64 counting_vub = -1;
        bool keyShift = false;
        private Int64 count = 0;
        private Double Sum = 0;
        #region Конструкторы класса

        public PrepCountingForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase.Connect();

            ////09.12.2019
            //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
            if (cbsort.SelectedIndex == 0)
                sort = "fl_prov";
            else if (cbsort.SelectedIndex == 1)
                sort = "name";
            else sort = "creation";

            
            countingDataSet = dataBase.GetData9("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);
           
            ////09.12.2019

            dgCounting.AutoGenerateColumns = false;
            dgCounting.Columns.Add("name", "Наименование пересчета");
            dgCounting.Columns["name"].Visible = true;
            dgCounting.Columns["name"].Width = 100;
            dgCounting.Columns["name"].DataPropertyName = "name";

            dgCounting.Columns.Add("fl_prov_name", "Состояние");
            dgCounting.Columns["fl_prov_name"].Visible = true;
            dgCounting.Columns["fl_prov_name"].DataPropertyName = "fl_prov_name";

            /////06.01.2020
            dgCounting.Columns.Add("id", "id");
            dgCounting.Columns["id"].Visible = false;
            dgCounting.Columns["id"].DataPropertyName = "id";
            /////06.01.2020

            dgCounting.DataSource = countingDataSet.Tables[0];
            dgCounting.RowHeadersWidth = 20;

            dgCounting.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            bagsDataSet = dataBase.GetData("t_g_bags");

            /////29.10.2019
           // bagsDataSet1 = bagsDataSet;
            /////29.10.2019

            countContentDataSet = dataBase.GetData("t_g_counting_content");

            countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");

            cardsDBDataSet = dataBase.GetData("t_g_cards");
            dsMulti = dataBase.GetData9("select * from t_g_multi_bags where deleted = 0 and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
        }
        #endregion

        private async void Form1_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
            //Список КР 
            cardsDataSet = new DataSet();
            cardsDataSet.Tables.Add();
            cardsDataSet.Tables[0].Columns.Add("id_begin");
            cardsDataSet.Tables[0].Columns.Add("BeginCard");

            ////24.10.2019
           // cardsDataSet.Tables[0].Columns.Add("EndCard");
            ////24.10.2019

            dgCards.DataSource = cardsDataSet.Tables[0];
            dgCards.Columns["id_begin"].Visible = false;
            dgCards.Columns["BeginCard"].HeaderText = "ОКР";

            ////24.10.2019
          // dgCards.Columns["EndCard"].HeaderText = "ЗКР";
            ////24.10.2019

            //Предварительная подготовка элементов управления
            PrepareData();

            //Установка фокуса на поле ввода номера сумки
            tbNumBag.Select();

            /////05.12.2019

            dataGridView3.AutoGenerateColumns = false;

            dataGridView3.Columns.Add("Val1", "Валюта");
            dataGridView3.Columns["Val1"].Visible = true;
            dataGridView3.Columns["Val1"].ReadOnly = true;
          //  dataGridView3.Columns["Val1"].Width = 120;
            dataGridView3.Columns["Val1"].DataPropertyName = "Val1";
            //dataGridView3.Columns["Val1"].SortMode = DataGridViewColumnSortMode.NotSortable;



            /////30.12.2019
            //dataGridView3.Columns.Add("Nomin1", "Номинал");
            dataGridView3.Columns.Add("Nomin1", "Ном-л");
            /////30.12.2019

            dataGridView3.Columns["Nomin1"].Visible = true;
            dataGridView3.Columns["Nomin1"].ReadOnly = true;
          //  dataGridView3.Columns["Nomin1"].Width = 120;
            dataGridView3.Columns["Nomin1"].DataPropertyName = "Nomin1";
            //dataGridView3.Columns["Nomin1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.Columns.Add("Colzajv1", "Лист");
            dataGridView3.Columns["Colzajv1"].Visible = true;
            dataGridView3.Columns["Colzajv1"].ReadOnly = true;
          //  dataGridView3.Columns["Colzajv1"].Width = 120;
            dataGridView3.Columns["Colzajv1"].DataPropertyName = "Colzajv1";
           // dataGridView3.Columns["Colzajv1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.Columns.Add("Sumzajvn1", "Сумма");
            dataGridView3.Columns["Sumzajvn1"].Visible = true;
            dataGridView3.Columns["Sumzajvn1"].ReadOnly = true;
         //   dataGridView3.Columns["Sumzajvn1"].Width = 120;
            dataGridView3.Columns["Sumzajvn1"].DataPropertyName = "Sumzajvn1";
          //  dataGridView3.Columns["Sumzajvn1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.Columns.Add("Colper1", "Лист");
            dataGridView3.Columns["Colper1"].Visible = true;
            dataGridView3.Columns["Colper1"].ReadOnly = true;
          //  dataGridView3.Columns["Colper1"].Width = 120;
            dataGridView3.Columns["Colper1"].DataPropertyName = "Colper1";
            //dataGridView3.Columns["Colper1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.Columns.Add("Sumpern1", "Сумма");
            dataGridView3.Columns["Sumpern1"].Visible = true;
            dataGridView3.Columns["Sumpern1"].ReadOnly = true;
         //   dataGridView3.Columns["Sumpern1"].Width = 120;
            dataGridView3.Columns["Sumpern1"].DataPropertyName = "Sumpern1";
           // dataGridView3.Columns["Sumpern1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.Columns.Add("Raskol1", "Лист");
            dataGridView3.Columns["Raskol1"].Visible = true;
            dataGridView3.Columns["Raskol1"].ReadOnly = true;
          //  dataGridView3.Columns["Raskol1"].Width = 120;
            dataGridView3.Columns["Raskol1"].DataPropertyName = "Raskol1";
          //  dataGridView3.Columns["Raskol1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.Columns.Add("Rassum1", "Сумма");
            dataGridView3.Columns["Rassum1"].Visible = true;
            dataGridView3.Columns["Rassum1"].ReadOnly = true;
         //   dataGridView3.Columns["Rassum1"].Width = 120;
            dataGridView3.Columns["Rassum1"].DataPropertyName = "Rassum1";
            //dataGridView3.Columns["Rassum1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            #region dataGridView7

            dataGridView7.AutoGenerateColumns = false;

            dataGridView7.Columns.Add("bag", "Номер сумки");
            dataGridView7.Columns["bag"].Visible = true;
            dataGridView7.Columns["bag"].ReadOnly = true;             
            dataGridView7.Columns["bag"].DataPropertyName = "bag";

            dataGridView7.Columns.Add("Val1", "Валюта");
            dataGridView7.Columns["Val1"].Visible = true;
            dataGridView7.Columns["Val1"].ReadOnly = true;
            //  dataGridView7.Columns["Val1"].Width = 120;
            dataGridView7.Columns["Val1"].DataPropertyName = "Val1";



            
            //dataGridView7.Columns.Add("Nomin1", "Номинал");
            dataGridView7.Columns.Add("Nomin1", "Ном-л");
            /////30.12.2019

            dataGridView7.Columns["Nomin1"].Visible = true;
            dataGridView7.Columns["Nomin1"].ReadOnly = true;
            //  dataGridView7.Columns["Nomin1"].Width = 120;
            dataGridView7.Columns["Nomin1"].DataPropertyName = "Nomin1";

            dataGridView7.Columns.Add("Colzajv1", "Лист");
            dataGridView7.Columns["Colzajv1"].Visible = true;
            dataGridView7.Columns["Colzajv1"].ReadOnly = true;
            //  dataGridView7.Columns["Colzajv1"].Width = 120;
            dataGridView7.Columns["Colzajv1"].DataPropertyName = "Colzajv1";

            dataGridView7.Columns.Add("Sumzajvn1", "Сумма");
            dataGridView7.Columns["Sumzajvn1"].Visible = true;
            dataGridView7.Columns["Sumzajvn1"].ReadOnly = true;
            //   dataGridView7.Columns["Sumzajvn1"].Width = 120;
            dataGridView7.Columns["Sumzajvn1"].DataPropertyName = "Sumzajvn1";

            dataGridView7.Columns.Add("Colper1", "Лист");
            dataGridView7.Columns["Colper1"].Visible = true;
            dataGridView7.Columns["Colper1"].ReadOnly = true;
            //  dataGridView7.Columns["Colper1"].Width = 120;
            dataGridView7.Columns["Colper1"].DataPropertyName = "Colper1";

            dataGridView7.Columns.Add("Sumpern1", "Сумма");
            dataGridView7.Columns["Sumpern1"].Visible = true;
            dataGridView7.Columns["Sumpern1"].ReadOnly = true;
            //   dataGridView7.Columns["Sumpern1"].Width = 120;
            dataGridView7.Columns["Sumpern1"].DataPropertyName = "Sumpern1";

            dataGridView7.Columns.Add("Raskol1", "Лист");
            dataGridView7.Columns["Raskol1"].Visible = true;
            dataGridView7.Columns["Raskol1"].ReadOnly = true;
            //  dataGridView7.Columns["Raskol1"].Width = 120;
            dataGridView7.Columns["Raskol1"].DataPropertyName = "Raskol1";

            dataGridView7.Columns.Add("Rassum1", "Сумма");
            dataGridView7.Columns["Rassum1"].Visible = true;
            dataGridView7.Columns["Rassum1"].ReadOnly = true;
            //   dataGridView7.Columns["Rassum1"].Width = 120;
            dataGridView7.Columns["Rassum1"].DataPropertyName = "Rassum1";
            dataGridView7.RowHeadersWidth = 20;
            #endregion dataGridView7

            ////////14.07.2020
            dataGridView4.AutoGenerateColumns = false;

            dataGridView4.Columns.Add("Val1", "Валюта");
            dataGridView4.Columns["Val1"].Visible = true;
            dataGridView4.Columns["Val1"].ReadOnly = true;
            dataGridView4.Columns["Val1"].DataPropertyName = "Val1";
           // dataGridView4.Columns["Val1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView4.Columns.Add("Sumzajvn1", "Заявлено");
            dataGridView4.Columns["Sumzajvn1"].Visible = true;
            dataGridView4.Columns["Sumzajvn1"].ReadOnly = true;
            dataGridView4.Columns["Sumzajvn1"].DataPropertyName = "Sumzajvn1";
           // dataGridView4.Columns["Sumzajvn1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView4.Columns.Add("Sumpern1", "Подсчитано");
            dataGridView4.Columns["Sumpern1"].Visible = true;
            dataGridView4.Columns["Sumpern1"].ReadOnly = true;
            dataGridView4.Columns["Sumpern1"].DataPropertyName = "Sumpern1";
          //  dataGridView4.Columns["Sumpern1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView4.Columns.Add("Rassum1", "Расхождение");
            dataGridView4.Columns["Rassum1"].Visible = true;
            dataGridView4.Columns["Rassum1"].ReadOnly = true;
            dataGridView4.Columns["Rassum1"].DataPropertyName = "Rassum1";
          //  dataGridView4.Columns["Rassum1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView4.Columns.Add("Info", "Состояния расхождения");
            dataGridView4.Columns["Info"].Visible = true;
            dataGridView4.Columns["Info"].ReadOnly = true;
            dataGridView4.Columns["Info"].DataPropertyName = "Info";
           // dataGridView4.Columns["Info"].SortMode = DataGridViewColumnSortMode.NotSortable;

            #region dataGridView6
            dataGridView6.AutoGenerateColumns = false;

            dataGridView6.Columns.Add("bag", "Номер  мультисумки");
            dataGridView6.Columns["bag"].Visible = true;
            dataGridView6.Columns["bag"].ReadOnly = true;
            dataGridView6.Columns["bag"].DataPropertyName = "bag";

            dataGridView6.Columns.Add("Val1", "Валюта");
            dataGridView6.Columns["Val1"].Visible = true;
            dataGridView6.Columns["Val1"].ReadOnly = true;
            dataGridView6.Columns["Val1"].Width = 60;
            dataGridView6.Columns["Val1"].DataPropertyName = "Val1";

            dataGridView6.Columns.Add("Sumzajvn1", "Заявлено");
            dataGridView6.Columns["Sumzajvn1"].Visible = true;
            dataGridView6.Columns["Sumzajvn1"].ReadOnly = true;
            dataGridView6.Columns["Sumzajvn1"].Width = 80;
            dataGridView6.Columns["Sumzajvn1"].DataPropertyName = "Sumzajvn1";

            dataGridView6.Columns.Add("Sumpern1", "Подсчитано");
            dataGridView6.Columns["Sumpern1"].Visible = true;
            dataGridView6.Columns["Sumpern1"].ReadOnly = true;
            dataGridView6.Columns["Sumpern1"].Width = 90;
            dataGridView6.Columns["Sumpern1"].DataPropertyName = "Sumpern1";

            dataGridView6.Columns.Add("Rassum1", "Расхождение");
            dataGridView6.Columns["Rassum1"].Visible = true;
            dataGridView6.Columns["Rassum1"].ReadOnly = true;
            dataGridView6.Columns["Rassum1"].Width = 100;
            dataGridView6.Columns["Rassum1"].DataPropertyName = "Rassum1";

            dataGridView6.Columns.Add("Info", "Состояния расхождения");
            dataGridView6.Columns["Info"].Visible = true;
            dataGridView6.Columns["Info"].ReadOnly = true;
            dataGridView6.Columns["Info"].DataPropertyName = "Info";
            #endregion


            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.ColumnHeadersHeight = 10;
            dataGridView5.RowHeadersWidth = 10;

            dataGridView5.Columns.Add("name", "Причина");
            dataGridView5.Columns["name"].Visible = true;
            dataGridView5.Columns["name"].ReadOnly = true;
            dataGridView5.Columns["name"].Width = 120;
            dataGridView5.Columns["name"].DataPropertyName = "name";

            dataGridView5.Columns.Add("description", "Описание");
            dataGridView5.Columns["description"].Visible = true;
            dataGridView5.Columns["description"].ReadOnly = true;
            dataGridView5.Columns["description"].Width = 314;
            dataGridView5.Columns["description"].DataPropertyName = "description";

            dataGridView8.AutoGenerateColumns = false;
            dataGridView8.ColumnHeadersHeight = 10;
            dataGridView8.RowHeadersWidth = 10;

            dataGridView8.Columns.Add("name", "Причина");
            dataGridView8.Columns["name"].Visible = true;
            dataGridView8.Columns["name"].ReadOnly = true;
            dataGridView8.Columns["name"].Width = 120;
            dataGridView8.Columns["name"].DataPropertyName = "name";

            dataGridView8.Columns.Add("description", "Описание");
            dataGridView8.Columns["description"].Visible = true;
            dataGridView8.Columns["description"].ReadOnly = true;
            dataGridView8.Columns["description"].Width = 314;
            dataGridView8.Columns["description"].DataPropertyName = "description";


            #region cbMulti
            cbMulti.DisplayMember = "name";
            cbMulti.ValueMember = "id";
            cbMulti.DataSource = dsMulti.Tables[0];
            cbMulti.SelectedIndex = -1;

            PrepareData();
            #endregion
            
            

            ////07.11.2019

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("Valut", "Валюта");
            dataGridView1.Columns["Valut"].Visible = true;
            dataGridView1.Columns["Valut"].ReadOnly = true;
            dataGridView1.Columns["Valut"].Width = 100;
            dataGridView1.Columns["Valut"].DataPropertyName = "Valut1";
           // dataGridView1.Columns["Valut"].SortMode = DataGridViewColumnSortMode.NotSortable;


            //  dataGridView1.Columns.Add("Colzajv", "Количество заявленное");
            dataGridView1.Columns.Add("Colzajv", "Листов ");
            dataGridView1.Columns["Colzajv"].Visible = true;
            dataGridView1.Columns["Colzajv"].ReadOnly = true;

            dataGridView1.Columns["Colzajv"].Width = 100;

            dataGridView1.Columns["Colzajv"].DataPropertyName = "Colzajv1";
            //dataGridView1.Columns["Colzajv"].SortMode = DataGridViewColumnSortMode.NotSortable;


            // dataGridView1.Columns.Add("Sumzajv", "Сумма заявленая");
            dataGridView1.Columns.Add("Sumzajv", "Сумма ");
            dataGridView1.Columns["Sumzajv"].Visible = true;
            dataGridView1.Columns["Sumzajv"].ReadOnly = true;

            dataGridView1.Columns["Sumzajv"].Width = 100;

            dataGridView1.Columns["Sumzajv"].DataPropertyName = "Sumzajv1";
           // dataGridView1.Columns["Sumzajv"].SortMode = DataGridViewColumnSortMode.NotSortable;

            
            //dataGridView1.Columns.Add("Sumzajvn", "Сумма заявленая по номиналам");
            //   dataGridView1.Columns.Add("Sumzajvn", "Сумма по номиналам");
            dataGridView1.Columns.Add("Sumzajvn", "Сумма ном-лов");
            dataGridView1.Columns["Sumzajvn"].Visible = true;
            dataGridView1.Columns["Sumzajvn"].ReadOnly = true;

            dataGridView1.Columns["Sumzajvn"].Width = 100;

            dataGridView1.Columns["Sumzajvn"].DataPropertyName = "Sumzajvn1";
           // dataGridView1.Columns["Sumzajvn"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("Colper", "Количество пересчёта");
            //dataGridView1.Columns.Add("Colper", "Листов по номиналу");
            dataGridView1.Columns.Add("Colper", "Листов ");
            dataGridView1.Columns["Colper"].Visible = true;
            dataGridView1.Columns["Colper"].ReadOnly = true;

            dataGridView1.Columns["Colper"].Width = 100;

            dataGridView1.Columns["Colper"].DataPropertyName = "Colper1";
           // dataGridView1.Columns["Colper"].SortMode = DataGridViewColumnSortMode.NotSortable;

            // dataGridView1.Columns.Add("Sumpern", "Сумма пересчёта по номиналам");
            //dataGridView1.Columns.Add("Sumpern", "Сумма по номиналам");
            dataGridView1.Columns.Add("Sumpern", "Сумма ");
            dataGridView1.Columns["Sumpern"].Visible = true;
            dataGridView1.Columns["Sumpern"].ReadOnly = true;

            dataGridView1.Columns["Sumpern"].Width = 100;

            dataGridView1.Columns["Sumpern"].DataPropertyName = "Sumpern1";
           // dataGridView1.Columns["Sumpern"].SortMode = DataGridViewColumnSortMode.NotSortable;


            /////

            //dataGridView1.Columns.Add("col2", "Листов по номиналам без пересчёта");
            dataGridView1.Columns.Add("col2", "Листов");
            dataGridView1.Columns["col2"].Visible = true;
            dataGridView1.Columns["col2"].ReadOnly = true;
            dataGridView1.Columns["col2"].Width = 100;
            dataGridView1.Columns["col2"].DataPropertyName = "col2";
           // dataGridView1.Columns["col2"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("col3", "Сумма по номиналам без пересчёта");
            dataGridView1.Columns.Add("col3", "Сумма ");
            dataGridView1.Columns["col3"].Visible = true;
            dataGridView1.Columns["col3"].ReadOnly = true;
            dataGridView1.Columns["col3"].Width = 100;
            dataGridView1.Columns["col3"].DataPropertyName = "col3";
           // dataGridView1.Columns["col3"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("col4", "Листов по номиналам с пересчётом");
            dataGridView1.Columns.Add("col4", "Листов ");
            dataGridView1.Columns["col4"].Visible = true;
            dataGridView1.Columns["col4"].ReadOnly = true;
            dataGridView1.Columns["col4"].Width = 100;
            dataGridView1.Columns["col4"].DataPropertyName = "col4";
           //dataGridView1.Columns["col4"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("col5", "Сумма по номиналам с пересчётом");
            dataGridView1.Columns.Add("col5", "Сумма ");
            dataGridView1.Columns["col5"].Visible = true;
            dataGridView1.Columns["col5"].ReadOnly = true;
            dataGridView1.Columns["col5"].Width = 100;
            dataGridView1.Columns["col5"].DataPropertyName = "col5";
          //  dataGridView1.Columns["col5"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("col6", "Листов по номиналам с машины");
            dataGridView1.Columns.Add("col6", "Листов ");
            dataGridView1.Columns["col6"].Visible = true;
            dataGridView1.Columns["col6"].ReadOnly = true;
            dataGridView1.Columns["col6"].Width = 100;
            dataGridView1.Columns["col6"].DataPropertyName = "col6";
          //  dataGridView1.Columns["col6"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("col7", "Сумма по номиналам с машины");
            dataGridView1.Columns.Add("col7", "Сумма ");
            dataGridView1.Columns["col7"].Visible = true;
            dataGridView1.Columns["col7"].ReadOnly = true;
            dataGridView1.Columns["col7"].Width = 100;
            dataGridView1.Columns["col7"].DataPropertyName = "col7";
          //  dataGridView1.Columns["col7"].SortMode = DataGridViewColumnSortMode.NotSortable;

            /////
            ///
            #region datagridview10
            dataGridView10.AutoGenerateColumns = false;

            dataGridView10.Columns.Add("bag", "Номер сумки");
            dataGridView10.Columns["bag"].Visible = true;
            dataGridView10.Columns["bag"].ReadOnly = true;
            dataGridView10.Columns["bag"].Width = 100;
            dataGridView10.Columns["bag"].DataPropertyName = "bag1";

            dataGridView10.Columns.Add("Valut", "Валюта");
            dataGridView10.Columns["Valut"].Visible = true;
            dataGridView10.Columns["Valut"].ReadOnly = true;
            dataGridView10.Columns["Valut"].Width = 100;
            dataGridView10.Columns["Valut"].DataPropertyName = "Valut1";

            //  dataGridView10.Columns.Add("Colzajv", "Количество заявленное");
            dataGridView10.Columns.Add("Colzajv", "Листов ");
            dataGridView10.Columns["Colzajv"].Visible = true;
            dataGridView10.Columns["Colzajv"].ReadOnly = true;

            dataGridView10.Columns["Colzajv"].Width = 100;

            dataGridView10.Columns["Colzajv"].DataPropertyName = "Colzajv1";

            // dataGridView10.Columns.Add("Sumzajv", "Сумма заявленая");
            dataGridView10.Columns.Add("Sumzajv", "Сумма ");
            dataGridView10.Columns["Sumzajv"].Visible = true;
            dataGridView10.Columns["Sumzajv"].ReadOnly = true;

            dataGridView10.Columns["Sumzajv"].Width = 100;

            dataGridView10.Columns["Sumzajv"].DataPropertyName = "Sumzajv1";

            //dataGridView10.Columns.Add("Sumzajvn", "Сумма заявленая по номиналам");
            //   dataGridView10.Columns.Add("Sumzajvn", "Сумма по номиналам");
            dataGridView10.Columns.Add("Sumzajvn", "Сумма ном-лов");
            dataGridView10.Columns["Sumzajvn"].Visible = true;
            dataGridView10.Columns["Sumzajvn"].ReadOnly = true;

            dataGridView10.Columns["Sumzajvn"].Width = 100;

            dataGridView10.Columns["Sumzajvn"].DataPropertyName = "Sumzajvn1";

            //dataGridView10.Columns.Add("Colper", "Количество пересчёта");
            //dataGridView10.Columns.Add("Colper", "Листов по номиналу");
            dataGridView10.Columns.Add("Colper", "Листов ");
            dataGridView10.Columns["Colper"].Visible = true;
            dataGridView10.Columns["Colper"].ReadOnly = true;

            dataGridView10.Columns["Colper"].Width = 100;

            dataGridView10.Columns["Colper"].DataPropertyName = "Colper1";

            // dataGridView10.Columns.Add("Sumpern", "Сумма пересчёта по номиналам");
            //dataGridView10.Columns.Add("Sumpern", "Сумма по номиналам");
            dataGridView10.Columns.Add("Sumpern", "Сумма ");
            dataGridView10.Columns["Sumpern"].Visible = true;
            dataGridView10.Columns["Sumpern"].ReadOnly = true;

            dataGridView10.Columns["Sumpern"].Width = 100;

            dataGridView10.Columns["Sumpern"].DataPropertyName = "Sumpern1";


            /////

            //dataGridView10.Columns.Add("col2", "Листов по номиналам без пересчёта");
            dataGridView10.Columns.Add("col2", "Листов");
            dataGridView10.Columns["col2"].Visible = true;
            dataGridView10.Columns["col2"].ReadOnly = true;
            dataGridView10.Columns["col2"].Width = 100;
            dataGridView10.Columns["col2"].DataPropertyName = "col2";

            //dataGridView10.Columns.Add("col3", "Сумма по номиналам без пересчёта");
            dataGridView10.Columns.Add("col3", "Сумма ");
            dataGridView10.Columns["col3"].Visible = true;
            dataGridView10.Columns["col3"].ReadOnly = true;
            dataGridView10.Columns["col3"].Width = 100;
            dataGridView10.Columns["col3"].DataPropertyName = "col3";

            //dataGridView10.Columns.Add("col4", "Листов по номиналам с пересчётом");
            dataGridView10.Columns.Add("col4", "Листов ");
            dataGridView10.Columns["col4"].Visible = true;
            dataGridView10.Columns["col4"].ReadOnly = true;
            dataGridView10.Columns["col4"].Width = 100;
            dataGridView10.Columns["col4"].DataPropertyName = "col4";

            //dataGridView10.Columns.Add("col5", "Сумма по номиналам с пересчётом");
            dataGridView10.Columns.Add("col5", "Сумма ");
            dataGridView10.Columns["col5"].Visible = true;
            dataGridView10.Columns["col5"].ReadOnly = true;
            dataGridView10.Columns["col5"].Width = 100;
            dataGridView10.Columns["col5"].DataPropertyName = "col5";

            //dataGridView10.Columns.Add("col6", "Листов по номиналам с машины");
            dataGridView10.Columns.Add("col6", "Листов ");
            dataGridView10.Columns["col6"].Visible = true;
            dataGridView10.Columns["col6"].ReadOnly = true;
            dataGridView10.Columns["col6"].Width = 100;
            dataGridView10.Columns["col6"].DataPropertyName = "col6";

            //dataGridView10.Columns.Add("col7", "Сумма по номиналам с машины");
            dataGridView10.Columns.Add("col7", "Сумма ");
            dataGridView10.Columns["col7"].Visible = true;
            dataGridView10.Columns["col7"].ReadOnly = true;
            dataGridView10.Columns["col7"].Width = 100;
            dataGridView10.Columns["col7"].DataPropertyName = "col7";

            #endregion



            

            dataGridView2.AutoGenerateColumns = false;

            dataGridView2.Columns.Add("Key-In", "Ввод");
            dataGridView2.Columns["Key-In"].Visible = true;
            dataGridView2.Columns["Key-In"].Width = 65;


            /////23.12.2019
            dataGridView2.Columns["Key-In"].Frozen = true;
            /////23.12.2019

            dataGridView2.Columns.Add("Card", "Карта");
            dataGridView2.Columns["Card"].Visible = true;
            dataGridView2.Columns["Card"].ReadOnly = true;
            dataGridView2.Columns["Card"].DataPropertyName = "Card1";
          //  dataGridView2.Columns["Card"].SortMode = DataGridViewColumnSortMode.NotSortable;

            /////23.12.2019
            dataGridView2.Columns["Card"].Frozen = true;
            /////23.12.2019

            ////30.12.2019
           // dataGridView2.Columns.Add("Nomin", "Номинал");
            dataGridView2.Columns.Add("Nomin", "Ном-л");
            ////30.12.2019


            dataGridView2.Columns["Nomin"].Visible = true;
            dataGridView2.Columns["Nomin"].ReadOnly = true;
            dataGridView2.Columns["Nomin"].DataPropertyName = "Nomin1";

            /////23.12.2019
            dataGridView2.Columns["Nomin"].Frozen = true;
          //  dataGridView2.Columns["Nomin"].SortMode = DataGridViewColumnSortMode.NotSortable;

            /////23.12.2019

            ////30.12.2019
            // dataGridView2.Columns.Add("Sost", "Состояние");
            dataGridView2.Columns.Add("Sost", "Сост");
            ////30.12.2019

          //  dataGridView2.Columns["Sost"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView2.Columns["Sost"].Visible = true;
            dataGridView2.Columns["Sost"].ReadOnly = true;
            dataGridView2.Columns["Sost"].DataPropertyName = "Sost1";

            //  dataGridView2.Columns.Add("Kolbp", "Количество ввода без пересчёта");
            //dataGridView2.Columns.Add("Kolbp", "Листов без пересчёта");
            dataGridView2.Columns.Add("Kolbp", "Листов ");
            dataGridView2.Columns["Kolbp"].Visible = true;
            dataGridView2.Columns["Kolbp"].ReadOnly = true;
            dataGridView2.Columns["Kolbp"].DataPropertyName = "Kolbp1";
           // dataGridView2.Columns["Kolbp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            // dataGridView2.Columns.Add("Sumbp", "Сумма ввода без пересчёта");
            //dataGridView2.Columns.Add("Sumbp", "Сумма без пересчёта");
            dataGridView2.Columns.Add("Sumbp", "Сумма ");
            dataGridView2.Columns["Sumbp"].Visible = true;
            dataGridView2.Columns["Sumbp"].ReadOnly = true;
            dataGridView2.Columns["Sumbp"].DataPropertyName = "Sumbp1";
          //  dataGridView2.Columns["Sumbp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            // dataGridView2.Columns.Add("Kolp", "Количество пересчёта");
            //dataGridView2.Columns.Add("Kolp", "Листов пересчёта");
            dataGridView2.Columns.Add("Kolp", "Листов ");
            dataGridView2.Columns["Kolp"].Visible = false;
            dataGridView2.Columns["Kolp"].ReadOnly = true;
            dataGridView2.Columns["Kolp"].DataPropertyName = "Kolp1";
          //  dataGridView2.Columns["Kolp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            // dataGridView2.Columns.Add("Sump", "Сумма пересчёта");
            dataGridView2.Columns.Add("Sump", "Сумма ");
            dataGridView2.Columns["Sump"].Visible = false;
            dataGridView2.Columns["Sump"].ReadOnly = true;
            dataGridView2.Columns["Sump"].DataPropertyName = "Sump1";
          //  dataGridView2.Columns["Sump"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //  dataGridView2.Columns.Add("Kolm", "Количество машинного пересчёта");
            //dataGridView2.Columns.Add("Kolm", "Листов с машинны");
            dataGridView2.Columns.Add("Kolm", "Листов ");
            dataGridView2.Columns["Kolm"].Visible = false;
            dataGridView2.Columns["Kolm"].ReadOnly = true;
            dataGridView2.Columns["Kolm"].DataPropertyName = "Kolm1";
           // dataGridView2.Columns["Kolm"].SortMode = DataGridViewColumnSortMode.NotSortable;

            // dataGridView2.Columns.Add("Summ", "Сумма машинного пересчёта");
            //dataGridView2.Columns.Add("Summ", "Сумма с машинны");
            dataGridView2.Columns.Add("Summ", "Сумма ");
            dataGridView2.Columns["Summ"].Visible = false;
            dataGridView2.Columns["Summ"].ReadOnly = true;
            dataGridView2.Columns["Summ"].DataPropertyName = "Summ1";

           // dataGridView2.Columns["Summ"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView2.Columns.Add("Sumob", "Общая сумма");
            dataGridView2.Columns["Sumob"].Visible = true;
            dataGridView2.Columns["Sumob"].ReadOnly = true;
            dataGridView2.Columns["Sumob"].DataPropertyName = "Sumob1";

          //  dataGridView2.Columns["Sumob"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //27.07.2020
            dataGridView2.Columns.Add("id_sost", "id_sost");
            dataGridView2.Columns["id_sost"].Visible = false;
            dataGridView2.Columns["id_sost"].ReadOnly = true;
            dataGridView2.Columns["id_sost"].DataPropertyName = "id_sost";

            dataGridView2.Columns.Add("id_denom", "id_denom");
            dataGridView2.Columns["id_denom"].Visible = false;
            dataGridView2.Columns["id_denom"].ReadOnly = true;
            dataGridView2.Columns["id_denom"].DataPropertyName = "id_denom";

            dataGridView2.Columns.Add("id_card", "id_card");
            dataGridView2.Columns["id_card"].Visible = false;
            dataGridView2.Columns["id_card"].ReadOnly = true;
            dataGridView2.Columns["id_card"].DataPropertyName = "id_card";


            #region dataGridView9
            dataGridView9.AutoGenerateColumns = false;

            dataGridView9.Columns.Add("bag", "Номер сумки");
            dataGridView9.Columns["bag"].Visible = true;
            dataGridView9.Columns["bag"].Width = 65;
            dataGridView9.Columns["bag"].ReadOnly = true;
            dataGridView9.Columns["bag"].DataPropertyName = "bag1";

            /////23.12.2019

            dataGridView9.Columns.Add("Card", "Карта");
            dataGridView9.Columns["Card"].Visible = true;
            dataGridView9.Columns["Card"].Width = 65;
            dataGridView9.Columns["Card"].ReadOnly = true;
            dataGridView9.Columns["Card"].DataPropertyName = "Card1";

            /////23.12.2019
            dataGridView9.Columns["Card"].Frozen = true;
            /////23.12.2019
            dataGridView9.Columns.Add("Valut", "Валюта");
            dataGridView9.Columns["Valut"].Visible = true;
            dataGridView9.Columns["Valut"].ReadOnly = true;
            dataGridView9.Columns["Valut"].Width = 65;
            dataGridView9.Columns["Valut"].DataPropertyName = "Valut1";
            ////30.12.2019
            // dataGridView9.Columns.Add("Nomin", "Номинал");
            dataGridView9.Columns.Add("Nomin", "Ном-л");
            ////30.12.2019

            dataGridView9.Columns["Nomin"].Width = 65;
            dataGridView9.Columns["Nomin"].Visible = true;
            dataGridView9.Columns["Nomin"].ReadOnly = true;
            dataGridView9.Columns["Nomin"].DataPropertyName = "Nomin1";

            /////23.12.2019
            dataGridView9.Columns["Nomin"].Frozen = true;
            /////23.12.2019

            ////30.12.2019
            // dataGridView2.Columns.Add("Sost", "Состояние");
            dataGridView9.Columns.Add("Sost", "Сост");
            ////30.12.2019

            dataGridView9.Columns["Sost"].Visible = true;
            dataGridView9.Columns["Sost"].ReadOnly = true;
            dataGridView9.Columns["Sost"].DataPropertyName = "Sost1";

            //  dataGridView9.Columns.Add("Kolbp", "Количество ввода без пересчёта");
            //dataGridView9.Columns.Add("Kolbp", "Листов без пересчёта");
            dataGridView9.Columns.Add("Kolbp", "Листов ");
            dataGridView9.Columns["Kolbp"].Visible = true;
            dataGridView9.Columns["Kolbp"].ReadOnly = true;
            dataGridView9.Columns["Kolbp"].DataPropertyName = "Kolbp1";

            // dataGridView9.Columns.Add("Sumbp", "Сумма ввода без пересчёта");
            //dataGridView9.Columns.Add("Sumbp", "Сумма без пересчёта");
            dataGridView9.Columns.Add("Sumbp", "Сумма ");
            dataGridView9.Columns["Sumbp"].Visible = true;
            dataGridView9.Columns["Sumbp"].ReadOnly = true;
            dataGridView9.Columns["Sumbp"].DataPropertyName = "Sumbp1";

            // dataGridView9.Columns.Add("Kolp", "Количество пересчёта");
            //dataGridView9.Columns.Add("Kolp", "Листов пересчёта");
            dataGridView9.Columns.Add("Kolp", "Листов ");
            dataGridView9.Columns["Kolp"].Visible = true;
            dataGridView9.Columns["Kolp"].ReadOnly = true;
            dataGridView9.Columns["Kolp"].DataPropertyName = "Kolp1";

            // dataGridView9.Columns.Add("Sump", "Сумма пересчёта");
            dataGridView9.Columns.Add("Sump", "Сумма ");
            dataGridView9.Columns["Sump"].Visible = true;
            dataGridView9.Columns["Sump"].ReadOnly = true;
            dataGridView9.Columns["Sump"].DataPropertyName = "Sump1";

            //  dataGridView9.Columns.Add("Kolm", "Количество машинного пересчёта");
            //dataGridView9.Columns.Add("Kolm", "Листов с машинны");
            dataGridView9.Columns.Add("Kolm", "Листов ");
            dataGridView9.Columns["Kolm"].Visible = true;
            dataGridView9.Columns["Kolm"].ReadOnly = true;
            dataGridView9.Columns["Kolm"].DataPropertyName = "Kolm1";

            // dataGridView9.Columns.Add("Summ", "Сумма машинного пересчёта");
            //dataGridView9.Columns.Add("Summ", "Сумма с машинны");
            dataGridView9.Columns.Add("Summ", "Сумма ");
            dataGridView9.Columns["Summ"].Visible = true;
            dataGridView9.Columns["Summ"].ReadOnly = true;
            dataGridView9.Columns["Summ"].DataPropertyName = "Summ1";

            dataGridView9.Columns.Add("Sumob", "Общая сумма");
            dataGridView9.Columns["Sumob"].Visible = true;
            dataGridView9.Columns["Sumob"].ReadOnly = true;
            dataGridView9.Columns["Sumob"].DataPropertyName = "Sumob1";

            //27.07.2020
            dataGridView9.Columns.Add("id_sost", "id_sost");
            dataGridView9.Columns["id_sost"].Visible = false;
            dataGridView9.Columns["id_sost"].ReadOnly = true;
            dataGridView9.Columns["id_sost"].DataPropertyName = "id_sost";

            dataGridView9.Columns.Add("id_denom", "id_denom");
            dataGridView9.Columns["id_denom"].Visible = false;
            dataGridView9.Columns["id_denom"].ReadOnly = true;
            dataGridView9.Columns["id_denom"].DataPropertyName = "id_denom";
            #endregion dataGridView9


            //////19.02.2020
            dataGridView3.Columns["Colzajv1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView3.Columns["Sumzajvn1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView3.Columns["Colper1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView3.Columns["Sumpern1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView3.Columns["Raskol1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView3.Columns["Rassum1"].DefaultCellStyle.Format = "### ### ### ###";

            dataGridView7.Columns["Colzajv1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Sumzajvn1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Colper1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Sumpern1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Raskol1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView7.Columns["Rassum1"].DefaultCellStyle.Format = "### ### ### ###";

            dataGridView4.Columns["Sumzajvn1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView4.Columns["Sumpern1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView4.Columns["Rassum1"].DefaultCellStyle.Format = "### ### ### ###";

            dataGridView6.Columns["Sumzajvn1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView6.Columns["Sumpern1"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView6.Columns["Rassum1"].DefaultCellStyle.Format = "### ### ### ###";

            dataGridView1.Columns["Colzajv"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Sumzajv"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Sumzajvn"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Colper"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["Sumpern"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["col2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["col3"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["col4"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["col5"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["col6"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView1.Columns["col7"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Kolbp"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Sumbp"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Kolp"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Sump"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Kolm"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Summ"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView2.Columns["Sumob"].DefaultCellStyle.Format = "### ### ### ###";

            dataGridView10.Columns["Colzajv"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["Sumzajv"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["Sumzajvn"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["Colper"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["Sumpern"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["col2"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["col3"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["col4"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["col5"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["col6"].DefaultCellStyle.Format = "### ### ### ###";
            dataGridView10.Columns["col7"].DefaultCellStyle.Format = "### ### ### ###";

            

            dataGridView3.AutoResizeColumns();
            dataGridView4.AutoResizeColumns();
            dataGridView2.AutoResizeColumns();
            dataGridView1.AutoResizeColumns();
            dataGridView10.AutoResizeColumns();
            dataGridView7.AutoResizeColumns();
            dataGridView6.AutoResizeColumns();

            ////
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView3.ColumnHeadersHeight = this.dataGridView3.ColumnHeadersHeight * 3;

            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



            dataGridView3.Paint += new PaintEventHandler(dataGridView3_Paint);



            dataGridView3.Scroll += new ScrollEventHandler(dataGridView3_Scroll);

            dataGridView3.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView3_ColumnWidthChanged);

            ////
            dataGridView7.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView7.ColumnHeadersHeight = this.dataGridView7.ColumnHeadersHeight * 3;

            dataGridView7.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



            dataGridView7.Paint += new PaintEventHandler(dataGridView7_Paint);



            dataGridView7.Scroll += new ScrollEventHandler(dataGridView7_Scroll);

            dataGridView7.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView7_ColumnWidthChanged);
            ///


            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView2.ColumnHeadersHeight = this.dataGridView2.ColumnHeadersHeight * 3;

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



            dataGridView2.Paint += new PaintEventHandler(dataGridView2_Paint);



            dataGridView2.Scroll += new ScrollEventHandler(dataGridView2_Scroll);

            dataGridView2.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);

            #region dataGridView9

            dataGridView9.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView9.ColumnHeadersHeight = this.dataGridView9.ColumnHeadersHeight * 3;

            dataGridView9.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



            dataGridView9.Paint += new PaintEventHandler(dataGridView9_Paint);



            dataGridView9.Scroll += new ScrollEventHandler(dataGridView9_Scroll);

            dataGridView9.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView9_ColumnWidthChanged);

            #endregion dataGridView9
            /////
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 3;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            //  dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

            dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);



            dataGridView10.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView10.ColumnHeadersHeight = this.dataGridView10.ColumnHeadersHeight * 3;

            dataGridView10.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            //  dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

            dataGridView10.Paint += new PaintEventHandler(dataGridView10_Paint);



            dataGridView10.Scroll += new ScrollEventHandler(dataGridView10_Scroll);

            dataGridView10.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView10_ColumnWidthChanged);


            rblCurrency.DisplayMember = "curr_code";
            rblCurrency.ValueMember = "id";

            rblCurrency2.DisplayMember = "curr_code";
            rblCurrency2.ValueMember = "id";

            rblCondition.DisplayMember = "name";
            rblCondition.ValueMember = "id";

            rblCondition2.DisplayMember = "name";
            rblCondition2.ValueMember = "id";

            rblcard.DisplayMember = "name";
            rblcard.ValueMember = "id";

            rblcard2.DisplayMember = "name";
            rblcard2.ValueMember = "id";



            ////07.11.2019

            /////08.11.2019
            clientsDataSet2 = dataBase.GetData("t_g_client");
            accountDataSet2 = dataBase.GetData("t_g_account");
            encashpointDataSet2 = dataBase.GetData("t_g_encashpoint");
            bagsDataSet2 = dataBase.GetData("t_g_bags");
            cardDataSet2 = dataBase.GetData9("select * from t_g_cards where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);
            causeDataSet = dataBase.GetData("t_g_cause");

            ////

            //Список счетов
            comboBox4.Text = "";
            comboBox4.DataSource = null;
            comboBox4.Items.Clear();
            comboBox4.DisplayMember = "name";
            comboBox4.ValueMember = "id";
            comboBox4.DataSource = accountDataSet2.Tables[0];
            comboBox4.SelectedIndex = -1;

            //Список БИН
            comboBox5.Text = "";
            comboBox5.DataSource = null;
            comboBox5.Items.Clear();
            comboBox5.DisplayMember = "BIN";
            comboBox5.ValueMember = "id";
            comboBox5.DataSource = clientsDataSet2.Tables[0];
            comboBox5.SelectedIndex = -1;

            //Список клиентов
            comboBox3.Text = "";
            comboBox3.DataSource = null;
            comboBox3.Items.Clear();
            comboBox3.DisplayMember = "name";
            comboBox3.ValueMember = "id";
            comboBox3.DataSource = clientsDataSet2.Tables[0];
            comboBox3.SelectedIndex = -1;

            //Список точек инкассации
            comboBox6.Text = "";
            comboBox6.DataSource = null;
            comboBox6.Items.Clear();
            comboBox6.DisplayMember = "name";
            comboBox6.ValueMember = "id";
            comboBox6.DataSource = encashpointDataSet2.Tables[0];
            comboBox6.SelectedIndex = -1;

            comboBox1.Text = "";
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = cardDataSet2.Tables[0];
            comboBox1.SelectedIndex = -1;


            comboBox2.Text = "";
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = bagsDataSet2.Tables[0];
            comboBox2.SelectedIndex = -1;

            //15.07.2020
            //Список причин
            comboBox10.Text = "";
            comboBox10.DataSource = null;
            comboBox10.Items.Clear();
            comboBox10.DisplayMember = "name";
            comboBox10.ValueMember = "id";
            comboBox10.DataSource = causeDataSet.Tables[0];
            comboBox10.SelectedIndex = -1;
            comboBox10.Enabled = false;

            comboBox11.Text = "";
            comboBox11.DataSource = null;
            comboBox11.Items.Clear();
            comboBox11.DisplayMember = "name";
            comboBox11.ValueMember = "id";
            comboBox11.DataSource = causeDataSet.Tables[0];
            comboBox11.SelectedIndex = -1;

            button13.Enabled = false;
            button14.Enabled = false;
            textBox3.Enabled = false;
            textBox3.Text = "";
            /////08.11.2019

            ////22.11.2019
            dsmarsh = dataBase.GetData9("SELECT  id ,nummarsh ,inkassator,(select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kontrol ) id_kontrol, (select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kassir ) id_kassir,kol_porsum,num_porsum,komment  FROM t_g_marschrut t1 where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);

            comboBox7.Text = "";
            comboBox7.DataSource = null;
            comboBox7.Items.Clear();
            comboBox7.DisplayMember = "nummarsh";
            comboBox7.ValueMember = "id";
            comboBox7.DataSource = dsmarsh.Tables[0];
            comboBox7.SelectedIndex = -1;

            ////22.11.2019

            ////29.11.2019
            dsakt = dataBase.GetData9("SELECT  * from t_g_akt  where id_shift_current =" + DataExchange.CurrentUser.CurrentUserShift);

            comboBox8.Text = "";
            comboBox8.DataSource = null;
            comboBox8.Items.Clear();
            comboBox8.DisplayMember = "numakt";
            comboBox8.ValueMember = "id";
            comboBox8.DataSource = dsakt.Tables[0];
            comboBox8.SelectedIndex = -1;
            ////29.11.2019

            /////05.01.2020
            grclienDataSet1 = dataBase.GetData("t_g_clisubfml");

            comboBox9.Text = "";
            comboBox9.DataSource = null;
            comboBox9.Items.Clear();
            comboBox9.DisplayMember = "name";
            comboBox9.ValueMember = "id";
            comboBox9.DataSource = grclienDataSet1.Tables[0];
            comboBox9.SelectedIndex = -1;
            /////05.01.2020
            ///24.09.2020
            flprovDataSet = dataBase.GetData9("select distinct fl_prov as id, iif(fl_prov=0,'Подготовлено',iif(fl_prov=1,'Расхождение',iif(fl_prov=2,'Сошлось','У.расхождение'))) as [name] from t_g_counting order by fl_prov");

            cbFlprov.Text = "";
            cbFlprov.DataSource = null;
            cbFlprov.Items.Clear();
            cbFlprov.DisplayMember = "name";
            cbFlprov.ValueMember = "id";
            cbFlprov.DataSource = flprovDataSet.Tables[0];
            cbFlprov.SelectedIndex = -1;


            //19.12.2020
            cbsort.SelectedIndex = 0;

        }


        #region Фильтры для выбора по вводу

        private void FillComboBox(DataSet dsSource, ComboBox cbSource, string txtFieldName)
        {
            string filter_param = cbSource.Text;

            var filteredItems = dsSource.Tables[0].AsEnumerable().Where(x => x.Field<string>(txtFieldName).Contains(filter_param));

            try
            {
                cbSource.DataSource = filteredItems.CopyToDataTable<DataRow>().Rows.Count > 0 ? filteredItems.CopyToDataTable<DataRow>() : null;

                if (String.IsNullOrWhiteSpace(filter_param))
                {
                    cbSource.DataSource = dsSource.Tables[0];
                }


                // this will ensure that the drop down is as long as the list
                cbSource.IntegralHeight = false;
                cbSource.IntegralHeight = true;
                cbSource.DroppedDown = true;

                Cursor.Current = Cursors.Default;
                // remove automatically selected first item
                cbSource.SelectedIndex = -1;

                cbSource.Text = filter_param;

                // set the position of the cursor
                cbSource.SelectionStart = filter_param.Length;
                cbSource.SelectionLength = 0;

            }
            catch (Exception) //ex)
            {
                MessageBox.Show("Нет совпадений");
            }

        }

        private void cbClient_TextUpdate(object sender, EventArgs e)
        {
            //FillComboBox(clientsDataSet, cbClient, "name"); 
        }

        private void cbID_TextUpdate(object sender, EventArgs e)
        {
            //FillComboBox(clientsDataSet, cbID, "BIN");
            

        }

        private void cbAccount_TextUpdate(object sender, EventArgs e)
        {
            //FillComboBox(accountDataSet, cbAccount, "name");
        }

        #endregion


        #region Обработка выбора клиента, счета и точки инкассации

        #region Выбор клиента
        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {

            //////25.10.2019
            if (cbClient.SelectedIndex != -1)
            {
            //////25.10.2019
                
                ///11.10.2019
                cbID.SelectedValue = cbClient.SelectedValue;
                ///11.10.2019

                //accountDataSet = dataBase.GetData("t_g_account", "id_client", cbClient.SelectedValue.ToString());
                accountDataSet = dataBase.GetData9("SELECT t1.*   FROM [CountingDB].[dbo].[t_g_account] t1  left join t_g_currency t2 on t1.id_currency=t2.id  where id_client=" + cbID.SelectedValue.ToString() + "  order by t2.sort ");

                //////25.10.2019
                accountDataSet1 = accountDataSet;
                //////25.10.2019

                encashpointDataSet = dataBase.GetData("t_g_encashpoint", "id", 
                accountDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_encashpoint").ToString()).ToList<string>());


                cbAccount.DisplayMember = "name";
                cbAccount.ValueMember = "id";
                if (accountDataSet.Tables.Count > 0)
                {
                    dgAccountDeclared.DataSource = accountDataSet.Tables[0];
                    //cbAccount.DataSource = accountDataSet.Tables[0];
                    if (accountDataSet.Tables[0].Rows.Count < 1)
                        cbAccount.Text = string.Empty;

                    //////31.12.2019
                    dgAccountDeclared.AutoResizeColumns();
                    //////31.12.2019

                }

                cbEncashPoint.DisplayMember = "name";
                cbEncashPoint.ValueMember = "id";
                if (encashpointDataSet.Tables.Count > 0)
                {
                    cbEncashPoint.DataSource = encashpointDataSet.Tables[0];
                }
                else
                {
                    cbEncashPoint.DataSource = null;
                }


                if (dgAccountDeclared.RowCount > 0)
                {
                    dgAccountDeclared.CurrentCell = dgAccountDeclared["value", 0];

                    //////31.12.2019
                    dgAccountDeclared.AutoResizeColumns();
                    //////31.12.2019
                    
                    //dgAccountDeclared.BeginEdit(true);
                }



                //////25.10.2019
            }
            //////25.10.2019




        }

        #endregion

        private void pokazimg()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            // if (dataGridView1.CurrentCell != null)
            if (cbClient.SelectedIndex != -1)
            {
                //if (pechat2 != null)
                //  if (dataGridView1.CurrentRow.Index < pechat2.Rows.Count)
                {

                    // if (pechat2.Rows[dataGridView1.CurrentRow.Index].RowState != DataRowState.Deleted)
                    {
                        pechat3 = dataBase.GetData9("select id, img1,img2  from t_g_pechatclien where  id_client='" + cbClient.SelectedValue.ToString() + "'");

                        if (pechat3 != null)
                        {

                            if (pechat3.Tables[0] != null)
                            {

                                if (pechat3.Tables[0].Rows.Count > 0)
                                {

                                    if ((pechat3.Tables[0].Rows[0]["img1"] != null) & (pechat3.Tables[0].Rows[0]["img1"] != DBNull.Value))
                                    {

                                        MemoryStream memoryStream = new MemoryStream();

                                        memoryStream.Write((byte[])pechat3.Tables[0].Rows[0]["img1"], 0, ((byte[])pechat3.Tables[0].Rows[0]["img1"]).Length);
                                        pictureBox1.Image = Image.FromStream(memoryStream);

                                        ////05.01.2020
                                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                        ////05.01.2020

                                        memoryStream.Dispose();

                                        //    Thread.Sleep(200);




                                    }
                                    else
                                        pictureBox1.Image = null;

                                    if ((pechat3.Tables[0].Rows[0]["img2"] != null) & (pechat3.Tables[0].Rows[0]["img2"] != DBNull.Value))
                                    {

                                        MemoryStream memoryStream1 = new MemoryStream();

                                        memoryStream1.Write((byte[])pechat3.Tables[0].Rows[0]["img2"], 0, ((byte[])pechat3.Tables[0].Rows[0]["img2"]).Length);
                                        pictureBox2.Image = Image.FromStream(memoryStream1);

                                        ////05.01.2020
                                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                        ////05.01.2020

                                        memoryStream1.Dispose();

                                        //    Thread.Sleep(200);


                                    }
                                    else
                                        pictureBox2.Image = null;
                                }

                            }
                        }
                        //  pictureBox1.Refresh();
                        //  pictureBox1.Update();

                        //  pictureBox2.Refresh();
                        //  pictureBox2.Update();
                    }
                }
            }

        }

        #region Выбор БИН
        private void cbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblID.Text = cbID.SelectedIndex.ToString();
            //lblClient.Text = cbID.SelectedValue.ToString();
            ///11.10.2019
            //     cbClient.SelectedValue = cbID.SelectedValue;
            ///11.10.2019

            accountDataSet = dataBase.GetData("t_g_account", "id_client", cbID.SelectedValue.ToString());
            encashpointDataSet = dataBase.GetData("t_g_encashpoint", "id", accountDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_encashpoint").ToString()).ToList<string>());

            //////25.10.2019
            accountDataSet1 = accountDataSet;
            //////25.10.2019

            cbAccount.DisplayMember = "name";
            cbAccount.ValueMember = "id";
            if (accountDataSet.Tables.Count > 0)
                cbAccount.DataSource = accountDataSet.Tables[0];

            cbEncashPoint.DisplayMember = "name";
            cbEncashPoint.ValueMember = "id";
            if (encashpointDataSet.Tables.Count > 0)
                cbEncashPoint.DataSource = encashpointDataSet.Tables[0];


        }

        #endregion


        #region Выбор счета
        private void cbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccount.SelectedIndex != -1)
            {
                accountDataSet = dataBase.GetData("t_g_account", "id", cbAccount.SelectedValue.ToString());

                Int64 clientId = accountDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(cbAccount.SelectedValue)).Select(x => x.Field<Int64>("id_client")).First<Int64>();
                Int64 encashpointId = accountDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(cbAccount.SelectedValue)).Select(x => x.Field<Int64>("id_encashpoint")).First<Int64>();
                Int64 currencyId = accountDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(cbAccount.SelectedValue)).Select(x => x.Field<Int64>("id_currency")).First<Int64>();

                cbID.SelectedValue = clientId;
                cbClient.SelectedValue = clientId;
                cbEncashPoint.SelectedValue = encashpointId;

            }
             else
            {
                cbID.SelectedValue = -1;
                cbClient.SelectedValue = -1;
                cbEncashPoint.SelectedValue = -1;
            }

           
            //cbCurrency.SelectedValue = currencyId;

        }
        #endregion

        
        #region Выбор точки инкассации

        private void cbEncashPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (accountDataSet.Tables.Count > 0 && accountDataSet.Tables[0].Rows.Count > 0 && cbEncashPoint.SelectedValue != null)
            {
                // MessageBox.Show("1");
                ////25.10.2019
                //accountDataSet = accountDataSet1;
                ////////25.10.2019

                //dgAccountDeclared.DataSource = accountDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_encashpoint") == Convert.ToInt64(cbEncashPoint.SelectedValue)).Count<DataRow>() > 0 ? accountDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_encashpoint") == Convert.ToInt64(cbEncashPoint.SelectedValue)).CopyToDataTable<DataRow>() : null;
                //if (dgAccountDeclared.Rows.Count > 0)

                ////////31.12.2019
                //dgAccountDeclared.AutoResizeColumns();
                ////////31.12.2019
                //MessageBox.Show("EncashPoint= " + cbEncashPoint.SelectedValue.ToString());
                //accountDataSet = dataBase.GetData("t_g_account", "id_encashpoint", cbEncashPoint.SelectedValue.ToString());
                accountDataSet = dataBase.GetData9("SELECT t1.*   FROM [CountingDB].[dbo].[t_g_account] t1  left join t_g_currency t2 on t1.id_currency=t2.id  where id_encashpoint=" + cbEncashPoint.SelectedValue.ToString() + "  order by t2.sort ");



                if (accountDataSet.Tables.Count > 0)
                {
                    dgAccountDeclared.DataSource = accountDataSet.Tables[0];
                    //cbAccount.DataSource = accountDataSet.Tables[0];
                    if (accountDataSet.Tables[0].Rows.Count < 1)
                        cbAccount.Text = string.Empty;

                    //////31.12.2019
                    dgAccountDeclared.AutoResizeColumns();
                    //////31.12.2019
                //    MessageBox.Show("3");
                }

                ////25.10.2019
                //dgAccountDeclared.DataSource = accountDataSet.Tables[0].Select("id_encashpoint="+cbEncashPoint.SelectedValue).CopyToDataTable<DataRow>() ;
                ////25.10.2019
                ///
                //cbAccount.SelectedValue

            }

            //cbAccount.SelectedValue = ;
            if (dgAccountDeclared.RowCount > 0 && cbEncashPoint.SelectedValue != null)
            {
               // MessageBox.Show("2");
                dgAccountDeclared.CurrentCell = dgAccountDeclared["value", 0];
                cbAccount.SelectedValue = accountDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_encashpoint") == Convert.ToInt64(cbEncashPoint.SelectedValue)).Select(x => x.Field<Int64>("id")).First();

                //////31.12.2019
                dgAccountDeclared.AutoResizeColumns();
                //////31.12.2019

                //dgAccountDeclared.BeginEdit(true);
            }



        }

        #endregion


        #endregion


        #region Имя сумки
       
        private void tbNumBag_KeyDown(object sender, KeyEventArgs e)
        {
            ///11.10.2019
            
               if (e.KeyCode == Keys.Escape && tbNumBag.Text != String.Empty)
           
            {
                    e.SuppressKeyPress = true;
                    if(pm.EnabledPossibility(perm, cbID))
                        cbID.Enabled = true;
                    if(pm.EnabledPossibility(perm, cbClient))
                        cbClient.Enabled = true;
                    if(pm.EnabledPossibility(perm, cbAccount))
                        cbAccount.Enabled = true;
                    if(pm.EnabledPossibility(perm, cbEncashPoint))
                        cbEncashPoint.Enabled = true;
                    if(pm.EnabledPossibility(perm, dtDeposit))
                        dtDeposit.Enabled = true;
                    if(pm.EnabledPossibility(perm, tbCard))
                        tbCard.Enabled = true;
                    if(pm.EnabledPossibility(perm, cbClosedCard))
                        cbClosedCard.Enabled = true;

                /////31.12.2019
                //  btnAdd.Enabled = true;
                /////31.12.2019
                if (pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;
            }
            
            ///11.10.2019

        }

        #endregion


        #region Очистка всех полей
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            PrepareData();
        }
        #endregion


        #region Подготовка всех компонентов
        private void PrepareData()
        {
            id_multi_bag1 = 0;
            if(pm.EnabledPossibility(perm, comboBox7))
                comboBox7.Enabled = true;
            //comboBox7.SelectedIndex = -1;

            //Заполнение наборов данных
            // clientsDataSet = dataBase.GetData("t_g_client");
            clientsDataSet = dataBase.GetData9("select * from t_g_client where [deleted]=0 order by name");
            //////31.10.2019
            clientsDataSet1 = clientsDataSet;
            //////31.10.2019

            //accountDataSet = dataBase.GetData("t_g_account");
            accountDataSet = dataBase.GetData9("SELECT t1.* FROM [t_g_account] t1  left join t_g_encashpoint t2 on t1.id_encashpoint=t2.id left join t_g_clienttocc t3 on t2.id_clienttocc=t3.id  left join t_g_client t4 on t3.id_client=t4.id  where deleted=0");
            // encashpointDataSet = dataBase.GetData("t_g_encashpoint");
            encashpointDataSet = dataBase.GetData9("select * from t_g_encashpoint t1 left join t_g_clienttocc t2 on t1.id_clienttocc=t2.id left join t_g_client t3 on t2.id_client=t3.id where deleted=0");
            currencyDataSet = dataBase.GetData("t_g_currency");

            //На время заполнения отключаем все обработчики
            cbAccount.SelectedIndexChanged -= cbAccount_SelectedIndexChanged;            
            cbID.SelectedIndexChanged -= cbID_SelectedIndexChanged;            
            cbClient.SelectedIndexChanged -= cbClient_SelectedIndexChanged;
            cbEncashPoint.SelectedIndexChanged -= cbEncashPoint_SelectedIndexChanged;
            if(pm.EnabledPossibility(perm, cbClient))
                cbClient.Enabled = true;
            if (pm.EnabledPossibility(perm, cbID))
                cbID.Enabled = true;
            if (pm.EnabledPossibility(perm, cbEncashPoint))
                cbEncashPoint.Enabled = true;
            if (pm.EnabledPossibility(perm, cbMulti))
                cbMulti.Enabled = true;

            tbNumBag.Text = String.Empty;
            tbCliCashier.Text = String.Empty;
            cbMulti.SelectedIndex = -1;
           

            /////24.10.2019
            /*
            cbID.Enabled = false;
            cbClient.Enabled = false;
            cbAccount.Enabled = false;
            cbEncashPoint.Enabled = false;
            dtDeposit.Enabled = false;
             tbCard.Enabled = false;
            */
            /////24.10.2019


            cbClosedCard.Enabled = false;
            cbClosedCard.Checked = false;
            tbEndCard.Enabled = false;
            

            //Список счетов
            cbAccount.Text = "";
            cbAccount.DataSource = null;
            cbAccount.Items.Clear();
            cbAccount.DisplayMember = "name";
            cbAccount.ValueMember = "id";
            cbAccount.DataSource = accountDataSet.Tables[0];
            cbAccount.SelectedIndex = -1;

            //Список БИН
            cbID.Text = "";
            cbID.DataSource = null;
            cbID.Items.Clear();
            cbID.DisplayMember = "BIN";
            cbID.ValueMember = "id";
            cbID.DataSource = clientsDataSet.Tables[0];
            cbID.SelectedIndex = -1;

            //Список клиентов
            cbClient.Text = "";
            cbClient.DataSource = null;
            cbClient.Items.Clear();
            cbClient.DisplayMember = "name";
            cbClient.ValueMember = "id";
            cbClient.DataSource = clientsDataSet.Tables[0];
            cbClient.SelectedIndex = -1;

            //Список точек инкассации
            cbEncashPoint.Text = "";
            cbEncashPoint.DataSource = null;
            cbEncashPoint.Items.Clear();
            cbEncashPoint.DisplayMember = "name";
            cbEncashPoint.ValueMember = "id";
            cbEncashPoint.DataSource = encashpointDataSet.Tables[0];
            cbEncashPoint.SelectedIndex = -1;

            //Cписок КР
            cardsDataSet.Tables[0].Rows.Clear();

            //Поля ввода КР
            tbCard.Text = String.Empty;
            tbEndCard.Text = String.Empty;
            cbClosedCard.CheckState = CheckState.Unchecked;


            //Данные по счетам
            dgAccountDeclared.AutoGenerateColumns = false;
            dgAccountDeclared.ColumnHeadersHeight = 10;
            dgAccountDeclared.RowHeadersWidth = 10;

            dgAccountDeclared.DataSource = null;
            dgAccountDeclared.Rows.Clear();
            dgAccountDeclared.Columns.Clear();

            DataGridViewCheckBoxColumn dgAccountBoolColumn = new DataGridViewCheckBoxColumn();
            dgAccountBoolColumn.Name = "state";
            dgAccountBoolColumn.HeaderText = "Исп.";
            dgAccountDeclared.Columns.Add(dgAccountBoolColumn);
            dgAccountDeclared.Columns["state"].Visible = true;
            dgAccountDeclared.Columns["state"].Width = 50;

            dgAccountDeclared.Columns.Add("name", "№ счета");
            dgAccountDeclared.Columns["name"].Visible = true;
            dgAccountDeclared.Columns["name"].ReadOnly = true;
            dgAccountDeclared.Columns["name"].DataPropertyName = "name";
            dgAccountDeclared.Columns["name"].Width = 180;
            //dgAccountDeclared.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgAccountDeclared.Columns.Add("id", "");
            dgAccountDeclared.Columns["id"].Visible = false;
            dgAccountDeclared.Columns["id"].DataPropertyName = "id";
            //dgAccountDeclared.Columns["id"].Width = 180;


           

            DataGridViewComboBoxColumn dgAccountCurrency =  new DataGridViewComboBoxColumn();
            dgAccountCurrency.Name = "id_currency";
            dgAccountCurrency.HeaderText = "Валюта";
            dgAccountCurrency.FlatStyle = FlatStyle.Flat;
            dgAccountCurrency.DataPropertyName = "id_currency";
            dgAccountCurrency.DisplayMember = "curr_code";
            dgAccountCurrency.ValueMember = "id";
            dgAccountCurrency.ReadOnly = true;
            dgAccountCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dgAccountCurrency.DataSource = currencyDataSet.Tables[0];
            dgAccountDeclared.Columns.Add(dgAccountCurrency);
            dgAccountDeclared.Columns["id_currency"].Visible = true;
            dgAccountDeclared.Columns["id_currency"].Width = 65;
           // dgAccountDeclared.Columns["id_currency"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dgAccountDeclared.Columns.Add("value", "Сумма");
            dgAccountDeclared.Columns["value"].Visible = true;
            dgAccountDeclared.Columns["value"].ReadOnly = false;
           // dgAccountDeclared.Columns["value"].SortMode = DataGridViewColumnSortMode.NotSortable;


            //////19.02.2020
            dgAccountDeclared.Columns["value"].DefaultCellStyle.Format = "### ### ### ###";
            //////19.02.2020

            DataGridViewButtonColumn dgAccountDetails = new DataGridViewButtonColumn();
            
             dgAccountDetails.Name = "Denomination";
            
            dgAccountDetails.Visible = true;
            dgAccountDetails.Text = "...";

            
            dgAccountDeclared.Columns.Add(dgAccountDetails);
            //////24.10.2019
            dgAccountDeclared.Columns["Denomination"].HeaderText="Номинал";
            dgAccountDeclared.Columns["Denomination"].Width = 80;
            countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");
            //dgAccountDeclared.Columns["Denomination"].Width = 30;
            //////24.10.2019

            ////29.11.2019
            //comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            //comboBox7.Text = "";
            comboBox8.Text = "";
            textBox2.Text = "";
            ////29.11.2019

            //dgAccountDeclared.Columns["value"].
            //
            dgAccountDeclaredCopy.AutoGenerateColumns = false;
            dgAccountDeclaredCopy.ColumnHeadersHeight = 10;
            dgAccountDeclaredCopy.RowHeadersWidth = 10;

            dgAccountDeclaredCopy.DataSource = null;
            dgAccountDeclaredCopy.Rows.Clear();
            dgAccountDeclaredCopy.Columns.Clear();

            DataGridViewCheckBoxColumn dgAccountBoolColumn1 = new DataGridViewCheckBoxColumn();
            dgAccountBoolColumn1.Name = "state";
            dgAccountBoolColumn1.HeaderText = "Исп.";
            dgAccountDeclaredCopy.Columns.Add(dgAccountBoolColumn1);
            dgAccountDeclaredCopy.Columns["state"].Visible = true;
            dgAccountDeclaredCopy.Columns["state"].Width = 50;

            dgAccountDeclaredCopy.Columns.Add("name", "№ счета");
            dgAccountDeclaredCopy.Columns["name"].Visible = true;
            dgAccountDeclaredCopy.Columns["name"].DataPropertyName = "name";
            dgAccountDeclaredCopy.Columns["name"].Width = 180;

            dgAccountDeclaredCopy.Columns.Add("id", "");
            dgAccountDeclaredCopy.Columns["id"].Visible = false;
            dgAccountDeclaredCopy.Columns["id"].DataPropertyName = "id";
            //dgAccountDeclared.Columns["id"].Width = 180;





            DataGridViewComboBoxColumn dgAccountCurrency1 = new DataGridViewComboBoxColumn();
            dgAccountCurrency1.Name = "id_currency";
            dgAccountCurrency1.HeaderText = "Валюта";
            dgAccountCurrency1.FlatStyle = FlatStyle.Flat;
            dgAccountCurrency1.DataPropertyName = "id_currency";
            dgAccountCurrency1.DisplayMember = "curr_code";
            dgAccountCurrency1.ValueMember = "id";
            dgAccountCurrency1.ReadOnly = true;
            dgAccountCurrency1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dgAccountCurrency1.DataSource = currencyDataSet.Tables[0];
            dgAccountDeclaredCopy.Columns.Add(dgAccountCurrency1);
            dgAccountDeclaredCopy.Columns["id_currency"].Visible = true;
            dgAccountDeclaredCopy.Columns["id_currency"].Width = 65;

            dgAccountDeclaredCopy.Columns.Add("value", "Сумма");
            dgAccountDeclaredCopy.Columns["value"].Visible = true;
            dgAccountDeclaredCopy.Columns["value"].ReadOnly = false;

            //////19.02.2020
            dgAccountDeclaredCopy.Columns["value"].DefaultCellStyle.Format = "### ### ### ###";
            //////19.02.2020




            //dgAccountDeclaredCopy.Columns.Add(dgAccountDetails);
            ////////24.10.2019
            //dgAccountDeclaredCopy.Columns["Denomination"].HeaderText = "Номинал";
            //dgAccountDeclaredCopy.Columns["Denomination"].Width = 80;
            //
            

            //События на изменение
            cbAccount.SelectedIndexChanged += cbAccount_SelectedIndexChanged;
            cbID.SelectedIndexChanged += cbID_SelectedIndexChanged;
            cbClient.SelectedIndexChanged += cbClient_SelectedIndexChanged;
            cbEncashPoint.SelectedIndexChanged += cbEncashPoint_SelectedIndexChanged;

            //cbAccount.SelectedIndexChanged += cbAccount_SelectedIndexChanged;
            ////30.12.2019



            pictureBox1.Image = null;
            pictureBox2.Image = null;

            ////30.12.2019


            ///31.12.2019

            counting_vub = -1;

            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = null;
            dataGridView10.DataSource = null;

            rblCurrency.Visible = false;
            rblCondition.Visible = false;
            //rblCondition2.Visible = false;

            rblcard.Visible = false;

            /*
            rblCurrency.DataSource = null;
            rblCondition.DataSource = null;
            rblcard.DataSource = null;
            */

            label19.Text = "";
            label21.Text = "";

            ///31.12.2019

        }
        #endregion

        #region Подготовка всех компонентов
        private void PrepareData1()
        {
            
            //Заполнение наборов данных
            // clientsDataSet = dataBase.GetData("t_g_client");
            clientsDataSet = dataBase.GetData9("select * from t_g_client where [deleted]=0 order by name");
            //////31.10.2019
            clientsDataSet1 = clientsDataSet;
            //////31.10.2019

            //accountDataSet = dataBase.GetData("t_g_account");
            accountDataSet = dataBase.GetData9("SELECT t1.* FROM [t_g_account] t1  left join t_g_encashpoint t2 on t1.id_encashpoint=t2.id left join t_g_clienttocc t3 on t2.id_clienttocc=t3.id  left join t_g_client t4 on t3.id_client=t4.id  where deleted=0");
            // encashpointDataSet = dataBase.GetData("t_g_encashpoint");
            encashpointDataSet = dataBase.GetData9("select * from t_g_encashpoint t1 left join t_g_clienttocc t2 on t1.id_clienttocc=t2.id left join t_g_client t3 on t2.id_client=t3.id where deleted=0");
            currencyDataSet = dataBase.GetData("t_g_currency");

            //На время заполнения отключаем все обработчики
            cbAccount.SelectedIndexChanged -= cbAccount_SelectedIndexChanged;
            cbID.SelectedIndexChanged -= cbID_SelectedIndexChanged;
            cbClient.SelectedIndexChanged -= cbClient_SelectedIndexChanged;
            cbEncashPoint.SelectedIndexChanged -= cbEncashPoint_SelectedIndexChanged;
            if(pm.EnabledPossibility(perm, cbClient))
                cbClient.Enabled = true;
            if (pm.EnabledPossibility(perm, cbID))
                cbID.Enabled = true;
            if (pm.EnabledPossibility(perm, cbEncashPoint))
                cbEncashPoint.Enabled = true;
            //cbMulti.Enabled = true;

            //tbNumBag.Text = String.Empty;
            //tbCliCashier.Text = String.Empty;
            //cbMulti.SelectedIndex = -1;


            /////24.10.2019
            /*
            cbID.Enabled = false;
            cbClient.Enabled = false;
            cbAccount.Enabled = false;
            cbEncashPoint.Enabled = false;
            dtDeposit.Enabled = false;
             tbCard.Enabled = false;
            */
            /////24.10.2019


            //cbClosedCard.Enabled = false;
            //cbClosedCard.Checked = false;
            //tbEndCard.Enabled = false;


            //Список счетов
            cbAccount.Text = "";
            cbAccount.DataSource = null;
            cbAccount.Items.Clear();
            cbAccount.DisplayMember = "name";
            cbAccount.ValueMember = "id";
            cbAccount.DataSource = accountDataSet.Tables[0];
            cbAccount.SelectedIndex = -1;

            //Список БИН
            cbID.Text = "";
            cbID.DataSource = null;
            cbID.Items.Clear();
            cbID.DisplayMember = "BIN";
            cbID.ValueMember = "id";
            cbID.DataSource = clientsDataSet.Tables[0];
            cbID.SelectedIndex = -1;

            //Список клиентов
            cbClient.Text = "";
            cbClient.DataSource = null;
            cbClient.Items.Clear();
            cbClient.DisplayMember = "name";
            cbClient.ValueMember = "id";
            cbClient.DataSource = clientsDataSet.Tables[0];
            cbClient.SelectedIndex = -1;

            //Список точек инкассации
            cbEncashPoint.Text = "";
            cbEncashPoint.DataSource = null;
            cbEncashPoint.Items.Clear();
            cbEncashPoint.DisplayMember = "name";
            cbEncashPoint.ValueMember = "id";
            cbEncashPoint.DataSource = encashpointDataSet.Tables[0];
            cbEncashPoint.SelectedIndex = -1;

            //Cписок КР
            //cardsDataSet.Tables[0].Rows.Clear();

            //Поля ввода КР
            //tbCard.Text = String.Empty;
            //tbEndCard.Text = String.Empty;
            //cbClosedCard.CheckState = CheckState.Unchecked;


            //Данные по счетам
            dgAccountDeclared.AutoGenerateColumns = false;
            dgAccountDeclared.ColumnHeadersHeight = 10;
            dgAccountDeclared.RowHeadersWidth = 10;

            dgAccountDeclared.DataSource = null;
            dgAccountDeclared.Rows.Clear();
            dgAccountDeclared.Columns.Clear();

            DataGridViewCheckBoxColumn dgAccountBoolColumn = new DataGridViewCheckBoxColumn();
            dgAccountBoolColumn.Name = "state";
            dgAccountBoolColumn.HeaderText = "Исп.";
            dgAccountDeclared.Columns.Add(dgAccountBoolColumn);
            dgAccountDeclared.Columns["state"].Visible = true;
            dgAccountDeclared.Columns["state"].Width = 50;

            dgAccountDeclared.Columns.Add("name", "№ счета");
            dgAccountDeclared.Columns["name"].Visible = true;
            dgAccountDeclared.Columns["name"].ReadOnly = true;
            dgAccountDeclared.Columns["name"].DataPropertyName = "name";
            dgAccountDeclared.Columns["name"].Width = 180;

            dgAccountDeclared.Columns.Add("id", "");
            dgAccountDeclared.Columns["id"].Visible = false;
            dgAccountDeclared.Columns["id"].DataPropertyName = "id";
            //dgAccountDeclared.Columns["id"].Width = 180;




            DataGridViewComboBoxColumn dgAccountCurrency = new DataGridViewComboBoxColumn();
            dgAccountCurrency.Name = "id_currency";
            dgAccountCurrency.HeaderText = "Валюта";
            dgAccountCurrency.FlatStyle = FlatStyle.Flat;
            dgAccountCurrency.DataPropertyName = "id_currency";
            dgAccountCurrency.DisplayMember = "curr_code";
            dgAccountCurrency.ValueMember = "id";
            dgAccountCurrency.ReadOnly = true;
            dgAccountCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dgAccountCurrency.DataSource = currencyDataSet.Tables[0];
            dgAccountDeclared.Columns.Add(dgAccountCurrency);
            dgAccountDeclared.Columns["id_currency"].Visible = true;
            dgAccountDeclared.Columns["id_currency"].Width = 65;

            dgAccountDeclared.Columns.Add("value", "Сумма");
            dgAccountDeclared.Columns["value"].Visible = true;
            dgAccountDeclared.Columns["value"].ReadOnly = false;

            //////19.02.2020
            dgAccountDeclared.Columns["value"].DefaultCellStyle.Format = "### ### ### ###";
            //////19.02.2020

            DataGridViewButtonColumn dgAccountDetails = new DataGridViewButtonColumn();

            dgAccountDetails.Name = "Denomination";

            dgAccountDetails.Visible = true;
            dgAccountDetails.Text = "...";


            dgAccountDeclared.Columns.Add(dgAccountDetails);
            //////24.10.2019
            dgAccountDeclared.Columns["Denomination"].HeaderText = "Номинал";
            dgAccountDeclared.Columns["Denomination"].Width = 80;
            //countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");
            //dgAccountDeclared.Columns["Denomination"].Width = 30;
            //////24.10.2019

            ////29.11.2019
            //comboBox7.SelectedIndex = -1;
            //comboBox8.SelectedIndex = -1;
            ////comboBox7.Text = "";
            //comboBox8.Text = "";
            //textBox2.Text = "";
            ////29.11.2019

            //dgAccountDeclared.Columns["value"].
            //
            dgAccountDeclaredCopy.AutoGenerateColumns = false;
            dgAccountDeclaredCopy.ColumnHeadersHeight = 10;
            dgAccountDeclaredCopy.RowHeadersWidth = 10;

            dgAccountDeclaredCopy.DataSource = null;
            dgAccountDeclaredCopy.Rows.Clear();
            dgAccountDeclaredCopy.Columns.Clear();

            DataGridViewCheckBoxColumn dgAccountBoolColumn1 = new DataGridViewCheckBoxColumn();
            dgAccountBoolColumn1.Name = "state";
            dgAccountBoolColumn1.HeaderText = "Исп.";
            dgAccountDeclaredCopy.Columns.Add(dgAccountBoolColumn1);
            dgAccountDeclaredCopy.Columns["state"].Visible = true;
            dgAccountDeclaredCopy.Columns["state"].Width = 50;

            dgAccountDeclaredCopy.Columns.Add("name", "№ счета");
            dgAccountDeclaredCopy.Columns["name"].Visible = true;
            dgAccountDeclaredCopy.Columns["name"].DataPropertyName = "name";
            dgAccountDeclaredCopy.Columns["name"].Width = 180;

            dgAccountDeclaredCopy.Columns.Add("id", "");
            dgAccountDeclaredCopy.Columns["id"].Visible = false;
            dgAccountDeclaredCopy.Columns["id"].DataPropertyName = "id";
            //dgAccountDeclared.Columns["id"].Width = 180;





            //DataGridViewComboBoxColumn dgAccountCurrency1 = new DataGridViewComboBoxColumn();
            //dgAccountCurrency1.Name = "id_currency";
            //dgAccountCurrency1.HeaderText = "Валюта";
            //dgAccountCurrency1.FlatStyle = FlatStyle.Flat;
            //dgAccountCurrency1.DataPropertyName = "id_currency";
            //dgAccountCurrency1.DisplayMember = "curr_code";
            //dgAccountCurrency1.ValueMember = "id";
            //dgAccountCurrency1.ReadOnly = true;
            //dgAccountCurrency1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            //dgAccountCurrency1.DataSource = currencyDataSet.Tables[0];
            //dgAccountDeclaredCopy.Columns.Add(dgAccountCurrency1);
            //dgAccountDeclaredCopy.Columns["id_currency"].Visible = true;
            //dgAccountDeclaredCopy.Columns["id_currency"].Width = 65;

            //dgAccountDeclaredCopy.Columns.Add("value", "Сумма");
            //dgAccountDeclaredCopy.Columns["value"].Visible = true;
            //dgAccountDeclaredCopy.Columns["value"].ReadOnly = false;

            ////////19.02.2020
            //dgAccountDeclaredCopy.Columns["value"].DefaultCellStyle.Format = "### ### ### ###";
            ////////19.02.2020




            //dgAccountDeclaredCopy.Columns.Add(dgAccountDetails);
            ////////24.10.2019
            //dgAccountDeclaredCopy.Columns["Denomination"].HeaderText = "Номинал";
            //dgAccountDeclaredCopy.Columns["Denomination"].Width = 80;
            //


            //События на изменение
            cbAccount.SelectedIndexChanged += cbAccount_SelectedIndexChanged;
            cbID.SelectedIndexChanged += cbID_SelectedIndexChanged;
            cbClient.SelectedIndexChanged += cbClient_SelectedIndexChanged;
            cbEncashPoint.SelectedIndexChanged += cbEncashPoint_SelectedIndexChanged;

            ////30.12.2019



            pictureBox1.Image = null;
            pictureBox2.Image = null;

            ////30.12.2019


            ///31.12.2019

            //counting_vub = -1;

            //dataGridView3.DataSource = null;
            //dataGridView4.DataSource = null;
            //dataGridView2.DataSource = null;
            //dataGridView1.DataSource = null;
            //dataGridView10.DataSource = null;

            //rblCurrency.Visible = false;
            //rblCondition.Visible = false;
            ////rblCondition2.Visible = false;

            //rblcard.Visible = false;

            ///*
            //rblCurrency.DataSource = null;
            //rblCondition.DataSource = null;
            //rblcard.DataSource = null;
            //*/

            //label19.Text = "";
            //label21.Text = "";

            ///31.12.2019

        }
        #endregion

        #region Обработка компонентов КР

        #region Считывание номера ОКР
        private void tbCard_KeyDown(object sender, KeyEventArgs e)
        {
            // MessageBox.Show(e.ToString());
            //tbCard.SelectAll();       
            if (e.KeyCode == Keys.Escape)

            {
                Button1_Click(sender, e);
                Button1_Click(sender, e);
            }
            if  (e.KeyCode == Keys.Enter)
                Button1_Click(sender, e);
            //   MessageBox.Show(e.KeyCode.ToString());


        }
        #endregion

        #region Считывание номера ЗКР
        private void tbEndCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (tbEndCard.Text != String.Empty)
                {
                    if (cardsDataSet.Tables[0].Rows.Count > 0)
                    {
                        //MessageBox.Show(dgCards.SelectedRows.Count.ToString());
                        if (dgCards.SelectedRows.Count > 0)
                        {
                            cardsDataSet.Tables[0].Rows[dgCards.CurrentCell.RowIndex]["EndCard"] = tbEndCard.Text;
                            //MessageBox.Show(dgCards.CurrentCell.RowIndex.ToString());
                        }
                        else
                        {
                            cardsDataSet.Tables[0].Rows[cardsDataSet.Tables[0].Rows.Count - 1]["EndCard"] = tbEndCard.Text;
                        }
                        tbEndCard.Text = String.Empty;
                    }
                    else
                    {
                        DataRow row = cardsDataSet.Tables[0].NewRow();
                        row["EndCard"] = tbEndCard.Text;

                        cardsDataSet.Tables[0].Rows.Add(row);
                        tbEndCard.Text = String.Empty;
                    }

                }
            }
        }
        #endregion
        
        #region Удаление КР
        private void btnDeleteCard_Click(object sender, EventArgs e)
        {
            if (dgCards.CurrentCell != null)
            {

                ///////31.10.2019

                DialogResult result = MessageBox.Show(
           "Удалить карточку?"
           ,
           "Сообщение",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1
           //,
           //MessageBoxOptions.DefaultDesktopOnly
           );

                if (result == DialogResult.No)
                    return;

                ///////31.10.2019
                
                if (cardsDataSet.Tables[0].Rows[dgCards.CurrentRow.Index]["id_begin"] != null)
                {
                    /////25.10.2019
                    if (cardsDataSet.Tables[0].Rows[dgCards.CurrentRow.Index]["id_begin"] != DBNull.Value)
                    {
                        /////25.10.2019

                       
                        ///
                        Int64 card_id = Convert.ToInt64(cardsDataSet.Tables[0].Rows[dgCards.CurrentRow.Index]["id_begin"]);
                        DataSet card = dataBase.GetData9("Select * from t_g_cards where fl_obr<>0 and id="+card_id.ToString());
                        DataSet card2 = dataBase.GetData9("Select * from t_g_counting_denom where id_card=" + card_id.ToString());
                        if ((card.Tables[0].Rows.Count>0) | (card2.Tables[0].Rows.Count>0))
                        {
                            MessageBox.Show("Эту карту не возможно удалить, поскольку по этой карте был пересчет!");
                            return;
                        }
                        ///////05.11.2019
                        if (cardsDBDataSet.Tables[0].Rows[dgCards.CurrentRow.Index].RowState != DataRowState.Deleted)
                        ///////05.11.2019

                            cardsDBDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == card_id).First<DataRow>().Delete();

                        ///////31.10.2019
                       // dataBase.UpdateData(cardsDBDataSet, "t_g_cards");
                        ///////31.10.2019

                        /////25.10.2019
                    }
                    /////25.10.2019

                }
                cardsDataSet.Tables[0].Rows[dgCards.CurrentRow.Index].Delete();

                ///////04.12.2019
               //MessageBox.Show("Операция выполнена! Карточка удалена.");
                ///////04.12.2019

            }

        }
        #endregion

        #region Проверка необходимости ЗКР
        private void cbClosedCard_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbClosedCard.CheckState == CheckState.Checked)
            {
                if(pm.EnabledPossibility(perm, tbEndCard))
                    tbEndCard.Enabled = true;
            }
            else if (cbClosedCard.CheckState == CheckState.Unchecked)
            {
                tbEndCard.Enabled = false;
            }
        }
        #endregion

        #endregion


        #region Обработка только цифровых значений в гриде
        private void dgAccountDeclared_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            /////06.01.2020
           
           // DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)e.Control;

            /////06.01.2020

            //textBox.KeyDown += new KeyEventHandler(dgAccountDeclared_KeyDown);
            //e.Control.KeyDown += new KeyEventHandler(dgAccountDeclared_KeyDown);
            /*Ch comboboxColumn = e.Control as ComboBox;
            if (comboboxColumn != null)
            {
                comboboxColumn.
            }*/
        }

        private void dgAccountDeclared_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        /////06.01.2020
        /*
        #region Обработка в гриде нажатия Enter и Tab для отметки в Checkbox
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
       {

           
           
            if (dgAccountDeclared.CurrentCell != null && dgAccountDeclared.CurrentCell.ColumnIndex == 4 && (keyData == Keys.Enter || keyData == Keys.Tab))
            {
                dgAccountDeclared.EndEdit();
                if (dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex].Value != null && dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex].Value.ToString() != String.Empty)
                    dgAccountDeclared["state", dgAccountDeclared.CurrentCell.RowIndex].Value = CheckState.Checked;
                else
                    if (dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex].Value == null || dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex].Value.ToString() == String.Empty)
                    dgAccountDeclared["state", dgAccountDeclared.CurrentCell.RowIndex].Value = CheckState.Unchecked;
            }
            if (dgAccountDeclared.CurrentCell != null && dgAccountDeclared.CurrentCell.ColumnIndex == 4 && dgAccountDeclared.IsCurrentCellInEditMode)
            {
                
                /////31.12.2019
                //  btnAdd.Enabled = true;
                /////31.12.2019
                
                btnModify.Enabled = true;
                if (!Char.IsNumber((Char)keyData) && (keyData != Keys.Back) && ((Char)keyData != ',') && ((Char)keyData != '.') )
                {
                    return true;
                }
            }
            
            

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        */
        /////06.01.2020

        #region Кнопка детализации по номиналам
        private void dgAccountDeclared_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                //сountDenomDataSet = dataBase.GetData("t_g_count_denom");
                DenominationParent denominationForm = new DenominationParent("t_g_counting_denomination", 
                    senderGrid.Rows[e.RowIndex].Cells["id_currency"].Value.ToString(),
                  senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()
                    , countDenomDataSet);
                DialogResult dialogResult = denominationForm.ShowDialog();
                //CountDenomDataSet cntDenomDataSet = new CountDenomDataSet();
                if (dialogResult == DialogResult.OK)
                {
                    //countDenomDataSet.Clear();
                    countDenomDataSet = denominationForm.CountDenomDataSet;
                }
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                if(Convert.ToBoolean(senderGrid.Rows[e.RowIndex].Cells["state"].EditedFormattedValue) == false)
                {
                    if (countContentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells["id"].Value)))
                    {
                        countContentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells["id"].Value)).First<DataRow>().Delete();
                        dataBase.UpdateData(countContentDataSet, "t_g_counting_content");
                        
                    }
                }
            }
        }
        #endregion


        #region Монипуляции с пересчетом

        #region Выбор пересчета
        private void dgCounting_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            detal1();
            //MessageBox.Show(e.RowIndex.ToString());
            if(e.RowIndex!=-1)
            {
                id_bag = Convert.ToInt32(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"].ToString());
                Console.WriteLine("id_bag1=" + id_bag);

                //if (cbMulti.SelectedIndex != -1)
                //    id_multi_bag1 =Convert.ToInt64(cbMulti.SelectedValue);
                //MessageBox.Show(id_multi_bag1.ToString());
                if(pm.EnabledPossibility(perm, cbClosedCard))
                    cbClosedCard.Enabled = true;
                if (pm.EnabledPossibility(perm, tbCard))
                    tbCard.Enabled = true;
                Int64 bag_id = -1;
                counting_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[e.RowIndex]["id"]);

                ///31.12.2019

                counting_vub = Convert.ToInt64(countingDataSet.Tables[0].Rows[e.RowIndex]["id"]);
                ///31.12.2019

                /////30.12.2019

                dtDeposit.Value = Convert.ToDateTime(countingDataSet.Tables[0].Rows[e.RowIndex]["date_deposit"].ToString());

                /////30.12.2019

                if (countingDataSet.Tables[0].Rows[e.RowIndex]["id_bag"] != DBNull.Value)
                {
                    bag_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[e.RowIndex]["id_bag"]);

                    /////29.10.2019
                    // bagsDataSet = bagsDataSet1;
                    /////29.10.2019

                    tbNumBag.Text = bagsName = bagsDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == bag_id).Select(x => x.Field<string>("name")).First<string>().Trim();
                    tbCliCashier.Text = bagsDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == bag_id).Select(x => x.Field<string>("cli_cashier")).First<string>().Trim();
                    cbMulti.SelectedValue = Convert.ToInt64(countingDataSet.Tables[0].Rows[e.RowIndex]["id_multi_bag"]);

                    /////20.11.2019
                    DataRow[] h44 = ((DataTable)bagsDataSet.Tables[0]).Select("id='" + bag_id.ToString() + "' and seal is not null");
                    if (h44.Count() > 0)
                        textBox2.Text = bagsDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == bag_id).Select(x => x.Field<string>("seal")).First<string>().Trim();
                    else
                        textBox2.Text = String.Empty;
                    /////20.11.2019


                    /////22.11.2019
                    comboBox7.SelectedIndex = -1;
                    DataRow[] h55 = ((DataTable)bagsDataSet.Tables[0]).Select("id='" + bag_id.ToString() + "' and id_marshr is not null");
                    if (h55.Count() > 0)
                    {
                        comboBox7.SelectedValue = h55[0]["id_marshr"].ToString();


                    }
                    else
                        comboBox7.Text = "";

                    /////22.11.2019


                    /////29.11.2019
                    comboBox8.SelectedIndex = -1;
                    DataRow[] h66 = ((DataTable)bagsDataSet.Tables[0]).Select("id='" + bag_id.ToString() + "' ");
                    if (h66.Count() > 0)
                    {
                        if (h66[0]["id_akt"].ToString().Trim() != "")
                            comboBox8.SelectedValue = h66[0]["id_akt"].ToString();
                        else
                            comboBox8.Text = "";

                    }
                    else
                        comboBox8.Text = "";

                    /////29.11.2019

                }
                else
                {
                    tbNumBag.Text = bagsName = String.Empty;
                    tbCliCashier.Text = String.Empty;
                    /////20.11.2019
                    textBox2.Text = String.Empty;
                    /////20.11.2019


                    /////22.11.2019
                    comboBox7.Text = "";
                    /////22.11.2019


                    /////29.11.2019
                    comboBox8.Text = "";
                    /////29.11.2019
                }

                Int64 client_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[e.RowIndex]["id_client"]);

                countContentDataSet = dataBase.GetData("t_g_counting_content", "id_counting", counting_id.ToString());

                countDenomDataSet = dataBase.GetData("t_g_declared_denom", "id_counting", counting_id.ToString());


                cbClient.SelectedValue = client_id;
                cbID.SelectedValue = client_id;

                if (Convert.ToInt32(countingDataSet.Tables[0].Rows[e.RowIndex]["fl_prov"]) != 0)
                {
                    cbClient.Enabled = false;
                    cbID.Enabled = false;
                    cbEncashPoint.Enabled = false;
                    cbMulti.Enabled = false;

                }
                else
                {
                    if(pm.EnabledPossibility(perm, cbClient))
                        cbClient.Enabled = true;
                    if (pm.EnabledPossibility(perm, cbID))
                        cbID.Enabled = true;
                    if (pm.EnabledPossibility(perm, cbEncashPoint))
                        cbEncashPoint.Enabled = true;
                    if (pm.EnabledPossibility(perm, cbMulti))
                        cbMulti.Enabled = true;
                }
                ///29.10.2019
                //accountDataSet = dataBase.GetData("t_g_account", "id_client", client_id.ToString());
                accountDataSet = dataBase.GetData9("SELECT t1.*   FROM [CountingDB].[dbo].[t_g_account] t1  left join t_g_currency t2 on t1.id_currency=t2.id  where id_client="+ client_id.ToString()+ "  order by t2.sort ");


                dgAccountDeclared.DataSource = accountDataSet.Tables[0];
                dgAccountDeclaredCopy.DataSource = accountDataSet.Tables[0];
                ///29.10.2019

                /////30.12.2019
                dgAccountDeclared.AutoResizeColumns();
                dgAccountDeclaredCopy.AutoResizeColumns();
                /////30.12.2019

                foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
                {
                    if (countContentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))

                    {

                        ///30.10.2019
                        cbAccount.SelectedValue = accountRow.Cells["id"].Value;
                        cbAccount_SelectedIndexChanged(sender, e);
                        cbEncashPoint_SelectedIndexChanged(sender, e);
                        ///30.10.2019

                        /*
                        accountRow.Cells["state"].Value = CheckState.Checked;
                        accountRow.Cells["value"].Value = countContentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)).Select(x => x.Field<Decimal>("declared_value")).First<Decimal>();

                        */
                        ///30.10.2019

                    }
                }

                foreach (DataGridViewRow accountRow in dgAccountDeclaredCopy.Rows)
                {
                    if (countContentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))

                    {
                        cbAccount.SelectedValue = accountRow.Cells["id"].Value;
                        cbAccount_SelectedIndexChanged(sender, e);
                        cbEncashPoint_SelectedIndexChanged(sender, e);
                    }
                }

                foreach (DataGridViewRow accountRow in dgAccountDeclaredCopy.Rows)
                {
                    if (countContentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))

                    {
                        accountRow.Cells["state"].Value = CheckState.Checked;
                        accountRow.Cells["value"].Value = Convert.ToDouble(countContentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)).Select(x => x.Field<Decimal>("declared_value")).First<Decimal>());
                    }
                }


                ///30.10.2019

                foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
                {
                    if (countContentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))

                    {



                        accountRow.Cells["state"].Value = CheckState.Checked;

                        /////31.12.2019
                        // accountRow.Cells["value"].Value = countContentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)).Select(x => x.Field<Decimal>("declared_value")).First<Decimal>();
                        accountRow.Cells["value"].Value = Convert.ToDouble(countContentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)).Select(x => x.Field<Decimal>("declared_value")).First<Decimal>());
                        /////31.12.2019


                    }
                }
                ///30.10.2019

                cardsDataSet.Tables[0].Rows.Clear();
                cardsDBDataSet = dataBase.GetData("t_g_cards", "id_counting", counting_id.ToString());


                if (cardsDBDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow card in cardsDBDataSet.Tables[0].Rows)
                    {
                        DataRow cardsRow = cardsDataSet.Tables[0].NewRow();
                        cardsRow["id_begin"] = card["id"];
                        cardsRow["BeginCard"] = card["name"];

                        cardsDataSet.Tables[0].Rows.Add(cardsRow);
                    }

                }
                //07.08.2020
                btnAdd.Enabled = false;

                if (cbMulti.SelectedIndex != -1)
                {
                    id_multi_bag1 = Convert.ToInt64(cbMulti.SelectedValue);
                    ////dgAccountDeclaredCopy = dgAccountDeclared;
                    ////foreach (DataGridViewColumn c in dgAccountDeclared.Columns)
                    ////{
                    ////    dgAccountDeclaredCopy.Columns.Add(c.Clone() as DataGridViewColumn);
                    ////}

                    ////then you can copy the rows values one by one (working on the selectedrows collection)
                    //foreach (DataGridViewRow r in dgAccountDeclared.SelectedRows)
                    //{
                    //    int index = dgAccountDeclaredCopy.Rows.Add(r.Clone() as DataGridViewRow);
                    //    foreach (DataGridViewCell o in r.Cells)
                    //    {
                    //        dgAccountDeclaredCopy.Rows[index].Cells[o.ColumnIndex].Value = o.Value;
                    //    }
                    //}

                }
                //MessageBox.Show(id_multi_bag1.ToString());
            }
        }

        #endregion

        #region Кнопка записи данных по пересчету
        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<DataSet> paramDataSet = new List<DataSet>();
            List<string> paramTables = new List<string>();
            DataSet countCommonDataSet = new DataSet();
            int DeclaredCount = 0;


            ///////31.10.2019

            //DialogResult result1 = MessageBox.Show(
            //"Добавить пересчёт?"
            // ,
            // "Сообщение",
            //MessageBoxButtons.YesNo,
            // MessageBoxIcon.Information,
            //MessageBoxDefaultButton.Button1
            ////,MessageBoxOptions.DefaultDesktopOnly
            //);

            //if (result1 == DialogResult.No)
            //    return;

            id_marsh = "";


            if(comboBox7.SelectedIndex==-1)
            {
                MessageBox.Show("Выберите маршрут!");
                return;
            }
            if ((comboBox7.SelectedValue != null) & (comboBox7.Text.ToString().Trim() != ""))
                id_marsh = comboBox7.SelectedValue.ToString();

            if (id_marsh.ToString().Trim() == "")
            {
                ////30.12.2019
                /*
                DialogResult result2 = MessageBox.Show(
                      "Не задан маршрут. Продолжить операцию?"
                      ,
                      "Сообщение",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);

                if (result2 == DialogResult.No)
                    return;
                */
              
            }
            //14.08.2020
            #region проверка на количество введенных сумок для мултисумки
            if (cbMulti.SelectedIndex != -1)
            {
                dataSet = dataBase.GetData9("select count_bags1, count_bags2 from t_g_multi_bags where id =" + cbMulti.SelectedValue.ToString().Trim());
                int count1 = dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("count_bags1")).FirstOrDefault<Int32>();
                int count2 = dataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("count_bags2")).FirstOrDefault<Int32>();
                if (count1 <= count2)
                {
                    MessageBox.Show("Вы достигли лимита сумок для - " + cbMulti.Text.Trim() + " мульти сумки, \n измените номер сумки или исправте количество сумок в Мультисумке");
                    return;
                }
                dataSet = null;
            }
            #endregion


            string id_akt = "";

            if ((comboBox8.SelectedValue != null) & (comboBox8.Text.ToString().Trim() != ""))
                id_akt = comboBox8.SelectedValue.ToString();

            if (id_akt.ToString().Trim() == "")
            {
                ////30.12.2019
                /*
                DialogResult result3 = MessageBox.Show(
                      "Не задан акт. Продолжить операцию?"
                      ,
                      "Сообщение",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);

                if (result3 == DialogResult.No)
                    return;
                */
                ////30.12.2019
            }
           

            int kolkartdol =0;

            ///Подсчёт карточек по счетам
            
            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (Convert.ToBoolean(declaredRow.Cells["state"].Value) == true)
                {

                 

                string s1=declaredRow.Cells["id_currency"].Value.ToString();

                    string selectString = "id = '" + s1.Trim().ToString() + "'";
                    DataRow[] searchedRows = ((DataTable)currencyDataSet.Tables[0]).Select(selectString);
                    string s2= searchedRows[0].Field<string>("curr_code");
                    if (s2.Trim().ToUpper() == "KZT")
                        kolkartdol = kolkartdol + 2;
                    else
                        kolkartdol = kolkartdol + 1;

                }

            }

            if (kolkartdol != dgCards.Rows.Count)
            {

        //        DialogResult result = MessageBox.Show(
        //" Продолжить операцию. Не совпадает количество карточек - "+ dgCards.Rows.Count.ToString()+ ", с нужным количеством карточек по счетам - "+ kolkartdol.ToString()+ " (2 карточки по KZT счёту и 1 по всем остальным)"
        //,
        //"Сообщение",
        //MessageBoxButtons.YesNo,
        //MessageBoxIcon.Information,
        //MessageBoxDefaultButton.Button1,
        //MessageBoxOptions.DefaultDesktopOnly);

        //        if (result == DialogResult.No)
        //            return;

                // MessageBox.Show("Выберите клиента");

            }

            //////24.10.2019


            /////25.10.2019

            ///Сравнение общей суммы с суммой по номиналам банкнот
            string s3 = "";

            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (declaredRow.Cells["value"].Value != null)

                {

                    declaredRow.Cells["value"].Value = declaredRow.Cells["value"].Value.ToString().Replace(" ","");
                  

                    long i2,i3=0;
                    if(long.TryParse(declaredRow.Cells["value"].Value.ToString(), out i2))
                    { }
                    else
                    {
                        //MessageBox.Show("Сумма должна быть целым числом!");
                        //return;
                    }

                    if (Int64.TryParse(declaredRow.Cells["value"].Value.ToString(), out i2) != true)
                    {

                        MessageBox.Show("Сумма должна быть целым числом!");
                        return;

                    }

                    i2 = Convert.ToInt64(declaredRow.Cells["value"].Value);

                    //////
                    if (countDenomDataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow denom1 in countDenomDataSet.Tables[0].Rows)
                        {
                            if (
                              // ( Convert.ToBoolean(declaredRow.Cells["state"].Value) == true)
                                //  (
                                //cbAccount.SelectedValue.ToString() 
                                //  declaredRow.Cells["state"].Value.ToString()=="1")
                                 //  &
                                (
                                   declaredRow.Cells["id"].Value.ToString()
                                 //  cardsDataSet.Tables[0].Rows[dgCards.CurrentRow.Index]["id_begin"]
                                == denom1["id_account"].ToString()) 
                                & 
                                (declaredRow.Cells["id_currency"].Value.ToString() == denom1["id_currency"].ToString()))
                            if ((denom1["declared_value"] != null) & (denom1["declared_value"] != DBNull.Value))
                                i3 = i3 + Convert.ToInt64(Convert.ToDouble(denom1["declared_value"].ToString()));
                            //Convert.ToInt32(denom1["declared_value"]);



                        }

                      
                       
                    }

                    if (i3 != i2)
                        s3 = s3 + " Есть расхождения общей суммы - "+ declaredRow.Cells["value"].Value.ToString() +", с суммой по номиналу - "+i3.ToString()+" по счёту - "+ declaredRow.Cells["name"].Value.ToString()+ "\r\n";


                    //////


                }

                 
            }

            /*if (s3.ToString() != "")
            {

                DialogResult result = MessageBox.Show(
       " Продолжить операцию. " + s3

       ,
       "Сообщение",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Information,
       MessageBoxDefaultButton.Button1,
       MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.No)
                    return;

            }*/
                //MessageBox.Show(s3);
            /////25.10.2019


            if (cbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }



            foreach(DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

               
               if (declaredRow.Cells["value"].Value != null && declaredRow.Cells["value"].Value != DBNull.Value && declaredRow.Cells["value"].Value.ToString() != String.Empty && Convert.ToDecimal(declaredRow.Cells["value"].Value) != 0)
                 {
                    DeclaredCount++;
                }
            }

            if(DeclaredCount == 0)
            {
                MessageBox.Show("Введите задекларированную сумму");
                return;
            }


            ////28.10.2019
            /*
            DataColumn[] prmk = new DataColumn[1];
            prmk[0] = bagsDataSet.Tables[0].Columns["ID"];
            bagsDataSet.Tables[0].PrimaryKey = prmk;
            bagsDataSet.Tables[0].Columns["ID"].AutoIncrement = true;
            bagsDataSet.Tables[0].Columns["ID"].AutoIncrementSeed = 1;
            bagsDataSet.Tables[0].Columns["ID"].ReadOnly = true;
            */

            ////28.10.2019


            

            //if (dgCards.Rows.Count == 0)
            //{
            //    MessageBox.Show("Добавьте карточки");
            //    return;
            //}

            //if (dgAccountDeclared.)
            //dataBase.InitTransaction();

            //Запись сумки

            ////28.10.2019
            if (tbNumBag.Text == String.Empty)
           
            {
                MessageBox.Show("Введите номер сумки");
                return;
            }

            ////
            // SqlConnection con = new SqlConnection(dataBase.connectionString);
            // con.Open();
            //SqlTransaction transaction = con.BeginTransaction();
            ///

            //Int64 bag_id = -1;
            //Int64 counting_id = -1;

            List<string> zapros1=new List<string>();

            ////29.10.2019
            //  try
            //  {
            ////29.10.2019

            string zap1 = "";




            //////20.11.2019
            
            //////29.11.2019
            

               //   bag_id = Convert.ToInt64(dataBase.Zapros1(zap1,""));

                /*
                int result=-1;
               
                  
                        string query1 = "";
                       
                            query1 = zap1;
                        SqlCommand com = new SqlCommand(query1, con);
                        
                        com.Transaction = transaction;
                       
                        result = Convert.ToInt32(com.ExecuteScalar());

                     
                Int64 bag_id = Convert.ToInt64(result);
                */

                string cardName = (dgCards.RowCount > 0 ? dgCards["BeginCard", 0].Value.ToString() + "_" : String.Empty);
            cardName = cardName + cbID.Text + "_" + DateTime.Now.ToShortDateString().Replace(".", "");
            string bagName = tbNumBag.Text + "_"+cbID.Text.Replace(" ","")+"_"+ DateTime.Now.ToShortDateString().Replace(".", "");

            dataSet = dataBase.GetData9("Select t1.* from [dbo].[t_g_bags] t1 " +
                "left join t_g_counting t2 on t1.id=t2.id_bag " +
                " where t2.deleted=0 and t1.name='" + tbNumBag.Text.Trim()+"'");
            if (dataSet != null)
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show(tbNumBag.Text + " - такая сумка уже существует! Введите другой номер сумки!");
                    return;
                }


            zap1 = " insert into  [dbo].[t_g_bags]([date_reception],[name] ,[seal],[creation],[lastupdate],[last_user_update],[id_marshr],id_akt,status,create_user,[owner_user],[cli_cashier],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) " +
                "values ('" + DateTime.Now.ToString() + "','" + tbNumBag.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + id_marsh.ToString().Trim() + "','" + id_akt.ToString().Trim() + "', 1 ," + DataExchange.CurrentUser.CurrentUserId.ToString() + "," + DataExchange.CurrentUser.CurrentUserId.ToString() + ", '" + tbCliCashier.Text + "', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "' )select @@IDENTITY";

            // zap1 = " insert into  [dbo].[t_g_bags]([name] ,[seal],[creation],[lastupdate],[last_user_update],[id_marshr]) values ('" + tbNumBag.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() +"','"+ id_marsh.ToString().Trim()+ "')select @@IDENTITY";
            //////29.11.2019

            // zap1 = " insert into  [dbo].[t_g_bags]([name] ,[seal],[creation],[lastupdate],[last_user_update]) values ('" + tbNumBag.Text.Trim() + "','" + textBox2.Text.Trim() + "','" +             DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "')select @@IDENTITY";
            /*
            zap1 = " insert into  [dbo].[t_g_bags]([name] ,[creation],[lastupdate],[last_user_update]) values ('" + tbNumBag.Text.Trim() + "','" +
                DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "')select @@IDENTITY";
            */
            //////20.11.2019

            zapros1.Add(zap1);
            /////
            //  zap1 = "INSERT INTO [dbo].[t_g_counting]([id_client],[id_bag],[creation],[last_user_update],[lastupdate],[name],[date_deposit],[deleted],[status]) VALUES('" + cbClient.SelectedValue .ToString()+"','"+ bag_id.ToString()+"','"+ DateTime.Now .ToString()+"','"+ DataExchange.CurrentUser.CurrentUserId.ToString()+"','"+DateTime.Now.ToString() + "','"+ cardName.ToString()+"','"+ dtDeposit.Value .ToString()+ "',0,0)select @@IDENTITY";


            ////20.11.2019
            //  zap1 = "INSERT INTO [dbo].[t_g_counting]([id_client],[id_bag],[creation],[last_user_update],[lastupdate],[name],[date_deposit],[deleted],[status]) VALUES('" + cbClient.SelectedValue.ToString() + "','{0}','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "','" + cardName.ToString() + "','" + dtDeposit.Value.ToString() + "',0,0)select @@IDENTITY";

            ////21.11.2019
            //zap1 = "INSERT INTO [dbo].[t_g_counting]([id_client],[id_bag],[creation],[last_user_update],[lastupdate],[name],[date_deposit],[deleted],[status],[datenachizm]) VALUES('" + cbClient.SelectedValue.ToString() + "','{0}','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "','" + cardName.ToString() + "','" + dtDeposit.Value.ToString() + "',0,0,'" + dnachism.ToString() + "')select @@IDENTITY";

            if (cbMulti.SelectedIndex!=-1)
            zap1 = "INSERT INTO [dbo].[t_g_counting]([id_client],[id_bag],[creation],[last_user_update],[lastupdate],[name],[date_deposit],[deleted],[status],[datenachizm],[qr_bin],[qr_data], [qr_nummech], [qr_numplom], [qr_otprav], [qr_kbe] , [qr_poluch],[qr_kontr] ,[qr_kass],[qr_vidoper],[qr_knp],[qr_numgr],[qr_poslsh],[qr_poslsum],[qr_poslval], [id_multi_bag],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) " +
                    "VALUES('" + cbClient.SelectedValue.ToString() + "','{0}','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "','" + bagName.ToString() + "','" + dtDeposit.Value.ToString() + "',0,0,'" + dnachism.ToString() +  "','"+qr_bin.ToString().Replace("'"," ")+"','"+qr_data.ToString().Replace("'", " ") + "','"+ qr_nummech.ToString().Replace("'", " ") + "','" +qr_numplom.ToString().Replace("'", " ") + "','"+qr_otprav.ToString().Replace("'", " ") + "','" +qr_kbe.ToString().Replace("'", " ") + "' ,'" +qr_poluch.ToString().Replace("'", " ") + "','"+qr_kontr.ToString().Replace("'", " ") + "','"+qr_kass.ToString().Replace("'", " ") + "','"+qr_vidoper.ToString().Replace("'", " ") + "','"+qr_knp.ToString().Replace("'", " ") + "','"+qr_numgr.ToString().Replace("'", " ") + "','"+qr_poslsh.ToString().Replace("'", " ") + "','"+qr_poslsum.ToString().Replace("'", " ") + "','"+qr_poslval.ToString().Replace("'", " ") + "', '"+cbMulti.SelectedValue.ToString().Trim()+"', '"+DataExchange.CurrentUser.CurrentUserId.ToString() + "', '"+DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '"+DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "')select @@IDENTITY";
            else zap1 = "INSERT INTO [dbo].[t_g_counting]([id_client],[id_bag],[creation],[last_user_update],[lastupdate],[name],[date_deposit],[deleted],[status],[datenachizm],[qr_bin],[qr_data], [qr_nummech], [qr_numplom], [qr_otprav], [qr_kbe] , [qr_poluch],[qr_kontr] ,[qr_kass],[qr_vidoper],[qr_knp],[qr_numgr],[qr_poslsh],[qr_poslsum],[qr_poslval],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) VALUES('" + cbClient.SelectedValue.ToString() + "','{0}','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "','" + bagName.ToString() + "','" + dtDeposit.Value.ToString() + "',0,0,'" + dnachism.ToString() + "','" + qr_bin.ToString().Replace("'", " ") + "','" + qr_data.ToString().Replace("'", " ") + "','" + qr_nummech.ToString().Replace("'", " ") + "','" + qr_numplom.ToString().Replace("'", " ") + "','" + qr_otprav.ToString().Replace("'", " ") + "','" + qr_kbe.ToString().Replace("'", " ") + "' ,'" + qr_poluch.ToString().Replace("'", " ") + "','" + qr_kontr.ToString().Replace("'", " ") + "','" + qr_kass.ToString().Replace("'", " ") + "','" + qr_vidoper.ToString().Replace("'", " ") + "','" + qr_knp.ToString().Replace("'", " ") + "','" + qr_numgr.ToString().Replace("'", " ") + "','" + qr_poslsh.ToString().Replace("'", " ") + "','" + qr_poslsum.ToString().Replace("'", " ") + "','" + qr_poslval.ToString().Replace("'", " ") + "', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "')select @@IDENTITY";

            zapros1.Add(zap1);


            //14.08.2020

            if(cbMulti.SelectedIndex!=-1)
                if(comboBox7.SelectedIndex!=-1)
            {
                zap1 = " DECLARE @count INT update t_g_multi_bags set lastupdate =getdate(), last_update_user ="+DataExchange.CurrentUser.CurrentUserId.ToString()+", @count = count_bags2, count_bags2 = @count + 1, id_marschrut = " + id_marsh.ToString().Trim() + " where id = " + cbMulti.SelectedValue.ToString().Trim();
                zapros1.Add(zap1);
            }
                else
                {
                    zap1 = " DECLARE @count INT update t_g_multi_bags set lastupdate =getdate(), last_update_user =" + DataExchange.CurrentUser.CurrentUserId.ToString() + ", @count = count_bags2, count_bags2 = @count + 1 where id = " + cbMulti.SelectedValue.ToString().Trim();
                    zapros1.Add(zap1);

                }



                ////29.10.2019
                //   counting_id = Convert.ToInt64(dataBase.Zapros1(zap1, ""));

                if (cardsDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow card in cardsDataSet.Tables[0].Rows)
                    {

                    zap1 = " INSERT INTO[dbo].[t_g_cards]([name],[id_bag],[type],[creation],[lastupdate],[last_user_update],[id_counting],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) values('" + card["BeginCard"].ToString() + "','{0}','0','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','{1}', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "')";
                    zapros1.Add(zap1);

                    zap1 = "update t_g_bags set date_preparation=getdate() where name = '" + tbNumBag.Text.Trim() + "' ";
                    zapros1.Add(zap1);
                }
                }
            dataSet = dataBase.GetData("t_g_multi_bags_content");
                //Запись счетов 
                foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
                {

                    if (Convert.ToBoolean(accountRow.Cells["state"].Value) == true)
                    {

                    zap1 = "INSERT INTO[dbo].[t_g_counting_content]([id_account],[id_counting],[id_bag],[declared_value],[fact_value],[id_currency],[creation],[lastupdate],[last_user_update],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) VALUES('" + accountRow.Cells["id"].Value.ToString() + "','{1}','{0}','" + accountRow.Cells["value"].Value.ToString().Replace(",",".") + "','0','" + accountRow.Cells["id_currency"].Value.ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString()+ "', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "')";
                    zapros1.Add(zap1);

                    //Запись в контент мултисумки
                    if (cbMulti.SelectedIndex != -1)
                    {
                        if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(cbMulti.SelectedValue) && x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))
                            zap1 = "DECLARE @value INT update t_g_multi_bags_content set lastupdate=getdate(), @value = declared_value2, declared_value2 = @value + " + accountRow.Cells["value"].Value.ToString().Replace(",", ".") + ", last_update_user = " + DataExchange.CurrentUser.CurrentUserId.ToString() + " where id_multi_bag = " + cbMulti.SelectedValue.ToString() + " and id_account = " + accountRow.Cells["id"].Value.ToString();
                        else zap1 = "INSERT INTO [dbo].[t_g_multi_bags_content] ([id_multi_bag],[id_account],[id_currency],[declared_value1],[declared_value2],[fact_value],[creation],[creation_user],[lastupdate],[last_update_user]) VALUES " +
                                "("+cbMulti.SelectedValue.ToString()+", "+ accountRow.Cells["id"].Value.ToString() + ", "+ accountRow.Cells["id_currency"].Value.ToString() + ", 0, " + accountRow.Cells["value"].Value.ToString().Replace(",", ".") + ", 0," +
                                " getdate(), "+DataExchange.CurrentUser.CurrentUserId.ToString()+ ", GETDATE(), " + DataExchange.CurrentUser.CurrentUserId.ToString() + ")";
                        zapros1.Add(zap1);
                    }
                    ////////

                    if (countDenomDataSet.Tables[0].Rows.Count > 0)
                    {
                        countDenomDataSet.Tables[0].TableName = "t_g_declared_denom";
                        foreach (DataRow cntDenomRow in countDenomDataSet.Tables["t_g_declared_denom"].Rows)
                        {

                            if (accountRow.Cells["id"].Value.ToString() == cntDenomRow["id_account"].ToString())
                            {
                                zap1 = "INSERT INTO[dbo].[t_g_declared_denom]([id_bag],[id_denomination],[denomcount],[id_account],[creation],[lastupdate],[last_user_update],[id_currency],[declared_value],[id_counting]) VALUES('{0}','" + cntDenomRow["id_denomination"].ToString() + "','" + cntDenomRow["denomcount"].ToString() + "','" + cntDenomRow["id_account"].ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + cntDenomRow["id_currency"].ToString() + "','" + cntDenomRow["declared_value"].ToString().Replace(",", ".") + "','{1}')";
                                zapros1.Add(zap1);
                            }

                        }


                    }

                    ////////

                }

                }

                //////
                /*
                if (countDenomDataSet.Tables[0].Rows.Count > 0)
                {
                    countDenomDataSet.Tables[0].TableName = "t_g_declared_denom";
                    foreach (DataRow cntDenomRow in countDenomDataSet.Tables["t_g_declared_denom"].Rows)
                    {

                        zap1 = "INSERT INTO[dbo].[t_g_declared_denom]([id_bag],[id_denomination],[denomcount],[id_account],[creation],[lastupdate],[last_user_update],[id_currency],[declared_value],[id_counting]) VALUES('{0}','"+ cntDenomRow["id_denomination"].ToString() +"','" + cntDenomRow["denomcount"].ToString() + "','" + cntDenomRow["id_account"].ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString()+"','" + cntDenomRow["id_currency"].ToString()+ "','" + cntDenomRow["declared_value"].ToString().Replace(",", ".") + "','{1}')";
                        zapros1.Add(zap1);

                    }


                }
                */
                //////

                int ih1=dataBase.Zapros2(zapros1,2);

            if (ih1 == 1)
            {


                ////09.12.2019
                //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
                if (cbsort.SelectedIndex == 0)
                    sort = "fl_prov";
                else if (cbsort.SelectedIndex == 1)
                    sort = "name";
                else sort = "creation";

                countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);

                ////09.12.2019

                dgCounting.DataSource = countingDataSet.Tables[0];
             //   dgCounting.Refresh();
             //   dgCounting.Update();
                dgCounting.CurrentCell = dgCounting["name", dgCounting.RowCount - 1];


                bagsDataSet = dataBase.GetData("t_g_bags");


                countContentDataSet = dataBase.GetData("t_g_counting_content");

               // countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");

                cardsDBDataSet = dataBase.GetData("t_g_cards");

                ///////04.12.2019
               // MessageBox.Show("Операция выполнена! Пересчёт добавлен.");
                ///////04.12.2019
                dgCounting.CurrentCell = dgCounting["name", dgCounting.RowCount - 1];

                //////06.01.2020

                ////нахождение номера строки с максимальным id
                double idmax = 0;
                int numstrmax = 0;
                int ich1 = 0;
                foreach (DataGridViewRow CountRow1 in dgCounting.Rows)
                {

                    if (Convert.ToDouble(CountRow1.Cells["id"].Value) > idmax)
                    {
                        idmax = Convert.ToDouble(CountRow1.Cells["id"].Value);
                        numstrmax = ich1;
                    }
                    ich1 = ich1 + 1;


                }
                dgCounting.CurrentCell = dgCounting["name", numstrmax];
                /////06.01.2020


            }
            else
                MessageBox.Show("Ошибка выполнения операции!");




            ////29.10.2019

            /////20.11.2019
            textBox1.Text = "";
            if (checkBox1.Checked)
                tbNumBag.Focus();
            
            else
                textBox1.Focus();
            /////20.11.2019

            /////26.11.2019

            if (fileobrabst1.ToString().Trim()!="")
            {

                //Если данный файл уже существует
                if (File.Exists(ConfigurationManager.AppSettings["obrabstfilereadyPath"] + "\\" + System.IO.Path.GetFileName(fileobrabst1)))
                {
                    //Удалить файл
                    File.Delete(ConfigurationManager.AppSettings["obrabstfilereadyPath"] + "\\" + System.IO.Path.GetFileName(fileobrabst1));
                }
                
                //Переместить туда файл
                File.Move(fileobrabst1.ToString(), ConfigurationManager.AppSettings["obrabstfilereadyPath"] + "\\" + System.IO.Path.GetFileName(fileobrabst1));

            }

            /////26.11.2019

            /*
             result = -1;


             query1 = "";

            query1 = zap1;
            SqlCommand com1 = new SqlCommand(query1, con);

            com1.Transaction = transaction;

            result = Convert.ToInt32(com1.ExecuteScalar());

            Int64 counting_id = Convert.ToInt64(result);
            */

            /*
              DataRow bagsRow = bagsDataSet.Tables[0].NewRow();
            if (tbNumBag.Text != String.Empty)
            {
                bagsRow["name"] = tbNumBag.Text;
            }
            else
            {
                MessageBox.Show("Введите номер сумки");
                return;
            }




            bagsRow["creation"] = DateTime.Now;
            bagsRow["lastupdate"] = DateTime.Now;
            bagsRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;

            bagsDataSet.Tables[0].TableName = "t_g_bags";
            bagsDataSet.Tables["t_g_bags"].Rows.Add(bagsRow);
            countCommonDataSet.Tables.Add(bagsDataSet.Tables["t_g_bags"].Copy());
            //countCommonDataSet.Tables["t_g_bags"] = bagsDataSet.Tables["t_g_bags"].Copy();


            // dataBase.UpdateData(bagsDataSet, "t_g_bags");




            Int64 bag_id = Convert.ToInt64(bagsRow["id"]);



            //Запись данных по пересчету со сцылками и названия
            DataRow countRow = countingDataSet.Tables[0].NewRow();
            countRow["id_client"] = cbClient.SelectedValue;
            countRow["id_bag"] = bag_id;
            countRow["creation"] = DateTime.Now;
            countRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
            countRow["lastupdate"] = DateTime.Now;


            //Формирование пересчета
            string cardName = (dgCards.RowCount > 0 ? dgCards["BeginCard", 0].Value.ToString() + "_" : String.Empty);
            countRow["name"] = cardName + cbID.Text + "_" + DateTime.Now.ToShortDateString().Replace(".", "");
            countRow["date_deposit"] = dtDeposit.Value;
            countRow["deleted"] = 0;
            countRow["status"] = 0; // 0 - подготовка к пересчету

            countingDataSet.Tables[0].TableName = "t_g_counting";
            countingDataSet.Tables["t_g_counting"].Rows.Add(countRow);

            ////28.10.2019
            countCommonDataSet.Tables.Add(countingDataSet.Tables["t_g_counting"].Copy());
            //countCommonDataSet.Tables.Add(countingDataSet.Tables["t_g_counting"]);
            ////28.10.2019
            //dataBase.UpdateData(countingDataSet, "t_g_counting");

            Int64 counting_id = Convert.ToInt64(countRow["id"]);

            */

            ////28.10.2019


            ////29.10.2019
            /*
            //Запись карточек
            if (cardsDataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow card in cardsDataSet.Tables[0].Rows)
            {
                DataRow cardsRow = cardsDBDataSet.Tables[0].NewRow();
                cardsRow["name"] = card["BeginCard"];
                cardsRow["id_bag"] = bag_id;
                cardsRow["type"] = 0;
                cardsRow["creation"] = DateTime.Now;
                cardsRow["lastupdate"] = DateTime.Now;
                cardsRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                cardsRow["id_counting"] = counting_id;
                cardsDBDataSet.Tables[0].TableName = "t_g_cards";
                cardsDBDataSet.Tables["t_g_cards"].Rows.Add(cardsRow);
            }

            ////28.10.2019
            countCommonDataSet.Tables.Add(cardsDBDataSet.Tables["t_g_cards"].Copy());
            // countCommonDataSet.Tables.Add(cardsDBDataSet.Tables["t_g_cards"]);
            ////28.10.2019

            //dataBase.UpdateData(cardsDBDataSet, "t_g_cards");
        }


        //Запись счетов 
        foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
        {

            if (Convert.ToBoolean(accountRow.Cells["state"].Value) == true)
            {
                DataRow contentRow = countContentDataSet.Tables[0].NewRow();
                contentRow["id_account"] = accountRow.Cells["id"].Value;
                contentRow["id_counting"] = counting_id;
                contentRow["id_bag"] = bag_id;
                contentRow["declared_value"] = accountRow.Cells["value"].Value;
                contentRow["fact_value"] = 0;
                contentRow["id_currency"] = accountRow.Cells["id_currency"].Value;
                contentRow["creation"] = DateTime.Now;
                contentRow["lastupdate"] = DateTime.Now;
                contentRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                countContentDataSet.Tables[0].TableName = "t_g_counting_content";
                countContentDataSet.Tables["t_g_counting_content"].Rows.Add(contentRow);
            }

        }

        ////28.10.2019
        countCommonDataSet.Tables.Add(countContentDataSet.Tables["t_g_counting_content"].Copy());
        //countCommonDataSet.Tables.Add(countContentDataSet.Tables["t_g_counting_content"]);
        ////28.10.2019

        //dataBase.UpdateData(countContentDataSet, "t_g_counting_content");



        if (countDenomDataSet.Tables[0].Rows.Count > 0)
        {
            countDenomDataSet.Tables[0].TableName = "t_g_declared_denom";
            foreach (DataRow cntDenomRow in countDenomDataSet.Tables["t_g_declared_denom"].Rows)
            {
                cntDenomRow["id_bag"] = bag_id;
                //cntDenomRow["id_card"] = cardName;
                cntDenomRow["id_counting"] = counting_id;
            }

            ////28.10.2019
            countCommonDataSet.Tables.Add(countDenomDataSet.Tables["t_g_declared_denom"].Copy());
            //countCommonDataSet.Tables.Add(countDenomDataSet.Tables["t_g_declared_denom"]);
            ////28.10.2019

            //dataBase.UpdateData(countDenomDataSet, "t_g_declared_denom");

        }
            ////28.10.2019
            //   dataBase.UpdateDataTransaction(countCommonDataSet);

            //   int i1 = Convert.ToInt32("rt");
            //   int i2= dataBase.UpdateData2(countCommonDataSet);
            int i2 = 1;
             dataBase.UpdateData3(countCommonDataSet);
            //  dataBase.UpdateData1(countCommonDataSet);

            //  int i1 = Convert.ToInt32("rt");
            if (i2 != 1)
            {

                dataBase.Zapros("delete from t_g_counting where id='" + counting_id.ToString() + "'", "");
                dataBase.Zapros("delete from t_g_bags  where id='" + bag_id.ToString() + "'", "");



            }
            //    dataBase.TransactionCommit();
            // else
            //    dataBase.TransactionRollback();



        }
        catch (Exception ex)
                   {

            dataBase.Zapros("delete from t_g_counting where id='" + counting_id.ToString() + "'", "");
            dataBase.Zapros("delete from t_g_bags  where id='" + bag_id.ToString() + "'", "");


            // Console.WriteLine(ex.Message);
            //     dataBase.TransactionRollback();
        }
            
        */
            int id_bag = 0;
            DataSet d1 = dataBase.GetData9("Select * from t_g_bags where name ='"+ tbNumBag.Text.Trim()+"'");
            if (d1 != null)
                if (d1.Tables[0].Rows.Count > 0)
                    id_bag = Convert.ToInt32(d1.Tables[0].AsEnumerable().Select(x=>x.Field<Int64>("id")).FirstOrDefault<Int64>());
            MessageBox.Show("id_bag="+id_bag.ToString());



            if (bagdefectfactorDataSet != null)
                for (int i = 0; i < bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                {
                    if (bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString() == "1")
                    {
                        String s="INSERT INTO [dbo].[t_w_bagdefect] " +
                                    "([num_defect],        [id_bag],                   [id_bagdefectfactor],                                         [creation],[lastupdate],[last_update_user])" +
                            " VALUES ('" + Num_defect + "'," + id_bag.ToString() + " ," + bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString() + ",getdate(), getdate()," + DataExchange.CurrentUser.CurrentUserId + ")";
                        Console.WriteLine(s);
                        dataBase.GetData9(s);
                    }
                }


            id_bag = 0;
            bagdefectfactorDataSet = null;

            /////05.01.2020
            if (button6.Enabled==true)
            Button5_Click(sender, e);
            /////05.01.2020


            /////08.01.2020
            PrepareData();
            comboBox7_SelectedIndexChanged(sender, e);           


            ///30.07.2020
            btnAdd.Enabled = false;
            ///30.07.2020
            

        }


        #endregion

        #region Кнопка изменения данных по пересчету

        //Изменение данных в пересчете
        private void btnModify_Click(object sender, EventArgs e)
        {

            List<DataSet> paramDataSet = new List<DataSet>();
            List<string> paramTables = new List<string>();

            List<string> zapros1 = new List<string>();

            int DeclaredCount = 0;

            ///////31.10.2019

       //     DialogResult result1 = MessageBox.Show(
       //"Изменить пересчёт?"
       //,
       //"Сообщение",
       //MessageBoxButtons.YesNo,
       //MessageBoxIcon.Information,
       //MessageBoxDefaultButton.Button1);
       ////,
       ////MessageBoxOptions.DefaultDesktopOnly);

       //     if (result1 == DialogResult.No)
       //         return;


            //Измение номера сумки
            if (tbNumBag.Text.Trim() == String.Empty)

            {
                MessageBox.Show("Введите номер сумки");
                return;
            }

            /////22.11.2019
            id_marsh = "";

            if ((comboBox7.SelectedValue != null) & (comboBox7.Text.ToString().Trim() != ""))
                id_marsh = comboBox7.SelectedValue.ToString();

            if (id_marsh.ToString().Trim() == "")
            {
                ////30.12.2019
                /*
                DialogResult result2 = MessageBox.Show(
                      "Не задан маршрут. Продолжить операцию?"
                      ,
                      "Сообщение",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);

                if (result2 == DialogResult.No)
                    return;
                */
                ////30.12.2019
            }
            /////22.11.2019

            /////29.11.2019
            string id_akt = "";

            if ((comboBox8.SelectedValue != null) & (comboBox8.Text.ToString().Trim() != ""))
                id_akt = comboBox8.SelectedValue.ToString();

            if (id_akt.ToString().Trim() == "")
            {

                ////30.12.2019
                /*
                DialogResult result3 = MessageBox.Show(
                      "Не задан акт. Продолжить операцию?"
                      ,
                      "Сообщение",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);

                if (result3 == DialogResult.No)
                    return;
                */
                ////30.12.2019
            }
            /////29.11.2019

            //////11.11.2019

            int ivudstr = dgCounting.CurrentCell.RowIndex;

            //////11.11.2019

            ///////31.10.2019

            Int64 counting_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]);

            /////22.11.2019
            /*
            ////////05.11.2019
            Int64 bag_id = -1;
            if ((countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"] ==DBNull.Value)|
                (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"] == null))
            {
                string zap2 = "";
                zap2 = " insert into  [dbo].[t_g_bags]([name] ,[creation],[lastupdate],[last_user_update]) values ('" + tbNumBag.Text.Trim() + "','" +
                    DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "')select @@IDENTITY";

                bag_id = dataBase.Zapros1(zap2, "");

                if (bag_id == -1)
                    return;

                bagsDataSet = dataBase.GetData("t_g_bags");

            }
            else
                bag_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"]);
            //    Int64 bag_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"]);
            ////////05.11.2019
            
            

            /////29.10.2019
            // bagsDataSet = bagsDataSet1;
            /////29.10.2019

            DataRow bagsRow = bagsDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == bag_id).First<DataRow>();
            */
            /////22.11.2019


            //////////////////29.10.2019


            //////24.10.2019

            int kolkartdol = 0;

            ///Подсчёт карточек по счетам

            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (Convert.ToBoolean(declaredRow.Cells["state"].Value) == true)
                {



                    string s1 = declaredRow.Cells["id_currency"].Value.ToString();

                    string selectString = "id = '" + s1.Trim().ToString() + "'";
                    DataRow[] searchedRows = ((DataTable)currencyDataSet.Tables[0]).Select(selectString);
                    string s2 = searchedRows[0].Field<string>("curr_code");
                    if (s2.Trim().ToUpper() == "KZT")
                        kolkartdol = kolkartdol + 2;
                    else
                        kolkartdol = kolkartdol + 1;

                }

            }

            if (kolkartdol != dgCards.Rows.Count)
            {

        //        DialogResult result = MessageBox.Show(
        //" Продолжить операцию. Не совпадает количество карточек - " + dgCards.Rows.Count.ToString() + ", с нужным количеством карточек по счетам - " + kolkartdol.ToString() + " (2 карточки по KZT счёту и 1 по всем остальным)"
        //,
        //"Сообщение",
        //MessageBoxButtons.YesNo,
        //MessageBoxIcon.Information,
        //MessageBoxDefaultButton.Button1,
        //MessageBoxOptions.DefaultDesktopOnly);

        //        if (result == DialogResult.No)
        //            return;

                // MessageBox.Show("Выберите клиента");

            }

            //////24.10.2019


            /////25.10.2019

            ///Сравнение общей суммы с суммой по номиналам банкнот
            string s3 = "";

            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {

                if (declaredRow.Cells["value"].Value != null)

                {
                    declaredRow.Cells["value"].Value = declaredRow.Cells["value"].Value.ToString().Replace(" ", "");

                    long i2, i3 = 0;
                    //  Convert.ToInt32(Convert.ToDouble("30,000"));
                    // if (Int32.TryParse(Convert.ToDouble(declaredRow.Cells["value"].Value.ToString()), out i2) != true)
                   // try
                   // { 
                        if(long.TryParse(declaredRow.Cells["value"].Value.ToString(), out i2))
                        {
                           
                        }
                        else
                        {
                            MessageBox.Show("Введите корректное число!");
                            PrepareData();
                            return;
                        }

                        //i2= Convert.ToInt64(Convert.ToDouble(declaredRow.Cells["value"].Value.ToString()));
                        /*
                         if (Int32.TryParse(declaredRow.Cells["value"].Value.ToString(), out i2) != true)
                        {

                            MessageBox.Show("Сумма должна быть целым числом!");
                            return;

                        }
                         */
                    //}
                    //catch (OverflowException)
                    //{
                       // MessageBox.Show("Сумма должна быть целым числом!");
                       // return;
                    //}

                    i2 = Convert.ToInt64(declaredRow.Cells["value"].Value);

                    //////
                    if (countDenomDataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow denom1 in countDenomDataSet.Tables[0].Rows)
                        {
                            if (
                            //   (Convert.ToBoolean(declaredRow.Cells["state"].Value) == true)
                                   //  (
                                   //cbAccount.SelectedValue.ToString() 
                                   //  declaredRow.Cells["state"].Value.ToString()=="1")
                            //       &
                                (declaredRow.Cells["id"].Value.ToString()
                                == denom1["id_account"].ToString()) &
                                (declaredRow.Cells["id_currency"].Value.ToString() == denom1["id_currency"].ToString()))

                                if ((denom1["declared_value"] != null) & (denom1["declared_value"] != DBNull.Value))
                                    
                                    i3 = i3 + Convert.ToInt64(Convert.ToDouble(denom1["declared_value"].ToString()));
                            //Convert.ToInt32(denom1["declared_value"]);


                        }



                    }

                    if (i3 != i2)
                        s3 = s3 + " Есть расхождения общей суммы - " + declaredRow.Cells["value"].Value.ToString() + ", с суммой по номиналу - " + i3.ToString() + " по счёту - " + declaredRow.Cells["name"].Value.ToString() + "\r\n";


                    //////


                }


            }
            //MessageBox.Show("1");
            if (s3.ToString() != "")
            {

       //         DialogResult result = MessageBox.Show(
       //" Продолжить операцию. " + s3

       //,
       //"Сообщение",
       //MessageBoxButtons.YesNo,
       //MessageBoxIcon.Information,
       //MessageBoxDefaultButton.Button1);
       ////,
       ////MessageBoxOptions.DefaultDesktopOnly);

       //         if (result == DialogResult.No)
       //             return;

            }

            //MessageBox.Show("2");
            //MessageBox.Show(s3);
            /////25.10.2019

            if (cbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }



            foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
            {


                if (declaredRow.Cells["value"].Value != null && declaredRow.Cells["value"].Value != DBNull.Value && declaredRow.Cells["value"].Value.ToString() != String.Empty && Convert.ToDecimal(declaredRow.Cells["value"].Value) != 0)
                {
                    DeclaredCount++;
                }
            }

            if (DeclaredCount == 0)
            {
                MessageBox.Show("Введите задекларированную сумму");
                return;
            }

           // MessageBox.Show("3");


            //if (dgCards.Rows.Count == 0)
            //{
            //    MessageBox.Show("Добавьте карточки");
            //    return;
            //}


            //////////////////29.10.2019


            ////////22.11.2019
            Int64 bag_id = -1;
            if ((countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"] == DBNull.Value) |
                (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"] == null))
            {
                string zap2 = "";
                zap2 = " insert into  [dbo].[t_g_bags]([name] ,[creation],[lastupdate],[last_user_update],[owner_user],[cli_cashier]) values ('" + tbNumBag.Text.Trim() + "','" +
                    DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '"+tbCliCashier.Text+"')select @@IDENTITY";

                bag_id = dataBase.Zapros1(zap2, "");

                if (bag_id == -1)
                {
                    
                    return;
                }
                bagsDataSet = dataBase.GetData("t_g_bags");
              
            }
            else
                bag_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"]);
            //    Int64 bag_id = Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"]);

            DataRow bagsRow = bagsDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == bag_id).First<DataRow>();

          //  MessageBox.Show("4");
            ////////22.11.2019


            ///30.10.2019




            string zap1 = "";




            ///////

            /////20.11.2019
            //zapros1.Add("update t_g_bags set name='"+ tbNumBag.Text.Trim() + "' where  id='" + bag_id.ToString() + "'");

            ////22.11.2019
            // zapros1.Add("update t_g_bags set name='" + tbNumBag.Text.Trim() + "',seal='" + textBox2.Text.Trim() + "' where  id='" + bag_id.ToString() + "'");

            ////29.11.2019
            zapros1.Add("update t_g_bags set name='" + tbNumBag.Text.Trim() + "',seal='" + textBox2.Text.Trim() + "' ,id_marshr='" + id_marsh.ToString() + "',id_akt='"+ id_akt.ToString() + "',[cli_cashier]='"+tbCliCashier.Text+"' where  id='" + bag_id.ToString() + "'");

            // zapros1.Add("update t_g_bags set name='" + tbNumBag.Text.Trim() + "',seal='" + textBox2.Text.Trim() + "' ,id_marshr='"+ id_marsh.ToString() + "' where  id='" + bag_id.ToString() + "'");
            ////29.11.2019

            ////22.11.2019

            /////20.11.2019

            string cardName = (dgCards.RowCount > 0 ? dgCards["BeginCard", 0].Value.ToString() + "_" : String.Empty);
            cardName = cardName + cbID.Text + "_" + DateTime.Now.ToShortDateString().Replace(".", "");

            //14.08.2020
            //MessageBox.Show("id_multi_bag1 = " + id_multi_bag1.ToString());
            if(id_multi_bag1!=0)
                if(cbMulti.SelectedIndex!=-1)
                if(id_multi_bag1!=Convert.ToInt64(cbMulti.SelectedValue))
                {
                    //MessageBox.Show("id_multi_bag1 (if)= " + id_multi_bag1.ToString());
                   // MessageBox.Show("cbMulti.SelectedValue.ToString() = " + cbMulti.SelectedValue.ToString());
                    dataSet = dataBase.GetData9("DECLARE @count INT UPDATE t_g_multi_bags SET @count = count_bags2, count_bags2 = @count - 1 WHERE id = " + id_multi_bag1.ToString().Trim());
                    dataSet = dataBase.GetData9("DECLARE @count INT UPDATE t_g_multi_bags SET @count = count_bags2, count_bags2 = @count + 1 WHERE id = " + cbMulti.SelectedValue.ToString().Trim());
                }
            if(cbMulti.SelectedIndex!=-1&comboBox7.SelectedIndex!=-1)
                dataSet = dataBase.GetData9("DECLARE @count INT UPDATE t_g_multi_bags SET id_marschrut = "+comboBox7.SelectedValue.ToString().Trim()+" WHERE id = " + cbMulti.SelectedValue.ToString().Trim());


            ///25.11.2019
            ///
            string bagName = tbNumBag.Text + "_" + cbID.Text.ToString().Replace(" ", "").Trim() + "_" + DateTime.Now.ToShortDateString().Replace(".", "");
            if(cbMulti.SelectedIndex!=-1)
                zapros1.Add("update t_g_counting set name='" + bagName.ToString() + "',id_client='"+ cbClient.SelectedValue.ToString() + "', id_bag = '" + bag_id.ToString() + "', lastupdate='"+ DateTime.Now .ToString()+ "', id_multi_bag = "+cbMulti.SelectedValue.ToString().Trim()+" where id='" + counting_id.ToString() + "'");
            else
                zapros1.Add("update t_g_counting set name='" + bagName.ToString() + "',id_client='" + cbClient.SelectedValue.ToString() + "', id_bag = '" + bag_id.ToString() + "', lastupdate='" + DateTime.Now.ToString() + "' where id='" + counting_id.ToString() + "'");

            
                //  zapros1.Add("update t_g_counting set name='" + cardName.ToString() + "',id_client='" + cbClient.SelectedValue.ToString() + "', id_bag = '" + bag_id.ToString() + "' where id='" + counting_id.ToString() + "'");
                ///25.11.2019

                /////07.11.2019

                string factsum1 = "0";

            /////11.11.2019

            Dictionary<int, string> spisfact1 = new Dictionary<int, string>();

            /*
            Dictionary<int, string> countries = new Dictionary<int, string>(5);
            countries.Add(1, "Russia");
            countries.Add(3, "Great Britain");
            countries.Add(2, "USA");
            countries.Add(4, "France");
            countries.Add(5, "China");
            */

            DataSet fdat1 = dataBase.GetData9("select fact_value,id_currency from t_g_counting_content where id_counting='" + counting_id.ToString() + "'");

            foreach (DataRow fdat in fdat1.Tables[0].Rows)
            {
                spisfact1.Add(Convert.ToInt32(fdat["id_currency"]),fdat["fact_value"].ToString());
            }
            

                /*
                DataSet fdat1 = dataBase.GetData9("select top 1 fact_value from t_g_counting_content where id_counting='" + counting_id.ToString() + "'");

                if (fdat1.Tables[0].Rows.Count > 0)
                {

                    factsum1 = fdat1.Tables[0].Rows[0][0].ToString();

                }
                */
                /////11.11.2019
                /////07.11.2019

                zapros1.Add("delete from t_g_counting_content where id_counting='" + counting_id.ToString() + "'");
            zapros1.Add("delete from t_g_declared_denom where id_counting='" + counting_id.ToString() + "'");
            if (dgCards != null)
                if(dgCards.Rows.Count>0)
            {
                //zapros1.Add("delete from t_g_cards where id_counting='" + counting_id.ToString() + "'");
                zapros1.Add("update t_g_cards set name='" + cardsDataSet.Tables[0].Rows[0]["BeginCard"].ToString() + "', id_bag = '" + bag_id.ToString() + "' where id_counting='" + counting_id.ToString() + "' and id in (select min(id) from t_g_cards where id_counting='" + counting_id.ToString() + "')");
                
            }
            
            /*
            zapros1.Add("update t_g_counting set name='" + cardName.ToString() + "'  where id='" + counting_id.ToString() + "'");

            zapros1.Add("delete from t_g_cards where id_counting='"+ counting_id.ToString() + "' and id_bag='" + bag_id.ToString() + "'");
            zapros1.Add("delete from t_g_counting_content where id_counting='" + counting_id.ToString() + "' and id_bag='" + bag_id.ToString() + "'");
            zapros1.Add("delete from t_g_declared_denom where id_counting='" + counting_id.ToString() + "' and id_bag='" + bag_id.ToString() + "'");
            */
            ////05.11.2019

            ///////


            if (cardsDataSet.Tables[0].Rows.Count > 0)
            {

                ////05.11.2019
                // DataSet d22= dataBase.GetData_Cards(counting_id.ToString());
                DataSet d22 = dataBase.GetData_Cards1(counting_id.ToString());

                
                if (d22.Tables[0].Rows.Count >= cardsDataSet.Tables[0].Rows.Count)
                {
                    //MessageBox.Show("1");
                    for (int i = 0; i < d22.Tables[0].Rows.Count ; i++)
                    {
                        if (cardsDataSet.Tables[0].Rows.Count >= i + 1)
                        {

                            zap1="update t_g_cards set name='" + cardsDataSet.Tables[0].Rows[i]["BeginCard"].ToString() + "', id_bag = '" + bag_id.ToString() + "' where id='" + d22.Tables[0].Rows[i]["id"].ToString()+  "'";
                        }
                        else
                        {
                            zap1 = "delete from t_g_cards where id='" + d22.Tables[0].Rows[i]["id"].ToString() + "'";
                        }
                        zapros1.Add(zap1);
                    }
                }
                else
                {

                    for (int i = 0; i < cardsDataSet.Tables[0].Rows.Count ; i++)
                    {
                        if (d22.Tables[0].Rows.Count >= i + 1)
                        {
                            zap1 = "update t_g_cards set name='" + cardsDataSet.Tables[0].Rows[i]["BeginCard"].ToString() + "', id_bag = '" + bag_id.ToString() + "' where id='" + d22.Tables[0].Rows[i]["id"].ToString() + "'";
                        }
                        else
                        {
                            zap1 = " INSERT INTO[dbo].[t_g_cards]([name],[id_bag],[type],[creation],[lastupdate],[last_user_update],[id_counting],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) values('" + cardsDataSet.Tables[0].Rows[i]["BeginCard"].ToString() + "','" + bag_id.ToString() + "','0','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + counting_id.ToString() + "', '"+DataExchange.CurrentUser.CurrentUserId.ToString() + "', '"+DataExchange.CurrentUser.CurrentUserZona.ToString()+"', '"+DataExchange.CurrentUser.CurrentUserShift.ToString()+"', '"+ DataExchange.CurrentUser.CurrentUserShift.ToString() + "')";
                        }
                        zapros1.Add(zap1);

                        zap1 = "update t_g_bags set date_preparation=getdate() where name = '" + tbNumBag.Text.Trim() + "' ";
                        zapros1.Add(zap1);
                    }

                }
                /*
                foreach (DataRow card in cardsDataSet.Tables[0].Rows)
                {

                    zap1 = " INSERT INTO[dbo].[t_g_cards]([name],[id_bag],[type],[creation],[lastupdate],[last_user_update],[id_counting]) values('" + card["BeginCard"].ToString() + "','"+ bag_id .ToString()+ "','0','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','"+ counting_id.ToString()+ "')";
                    zapros1.Add(zap1);


                }
                */
                ////05.11.2019

            }
            else
            {
                DataSet d22 = dataBase.GetData_Cards1(counting_id.ToString());
                for (int i = 0; i < d22.Tables[0].Rows.Count; i++)
                {
                    zap1 = "delete from t_g_cards where id='" + d22.Tables[0].Rows[i]["id"].ToString() + "'";
                    zapros1.Add(zap1);
                }

            }
            //17.08.2020
            //Запись счетов в контент мультисумки
            dataSet = dataBase.GetData("t_g_multi_bags_content");
            if (id_multi_bag1 != 0)
                if(dgAccountDeclaredCopy.Rows.Count>0)
                foreach(DataGridViewRow accountRow1 in dgAccountDeclaredCopy.Rows)
                {
                        


                        if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multi_bag1) && x.Field<Int64>("id_account") == Convert.ToInt64(accountRow1.Cells["id"].Value)))
                    {
                           
                        zap1 = "DECLARE @value INT update t_g_multi_bags_content set lastupdate=getdate(), @value = declared_value2, declared_value2 = @value - " + accountRow1.Cells["value"].Value.ToString().Replace(",", ".") + ", last_update_user = " + DataExchange.CurrentUser.CurrentUserId.ToString() + " where id_multi_bag = " + id_multi_bag1.ToString() + " and id_account = " + accountRow1.Cells["id"].Value.ToString();
                        zapros1.Add(zap1);
                    }
                }


            //Запись счетов 
            foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
            {
                

                if (Convert.ToBoolean(accountRow.Cells["state"].Value) == true)
                {

                    /////11.11.2019

                    factsum1 = "0";

                    foreach (KeyValuePair<int, string> keyValue in spisfact1)
                    {
                        if (keyValue.Key == Convert.ToInt32(accountRow.Cells["id_currency"].Value))
                            factsum1 = keyValue.Value;
                     //   Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
                    }

                    /////11.11.2019

                    //////07.11.2019


                    zap1 = "INSERT INTO[dbo].[t_g_counting_content]([id_account],[id_counting],[id_bag],[declared_value],[fact_value],[id_currency],[creation],[lastupdate],[last_user_update],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current]) VALUES('" + accountRow.Cells["id"].Value.ToString() + "','" + counting_id.ToString() + "','" + bag_id.ToString() + "','" + accountRow.Cells["value"].Value.ToString().Replace(",", ".") + "','"+ factsum1 .ToString().Replace(",", ".") + "','" + accountRow.Cells["id_currency"].Value.ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','"+DataExchange.CurrentUser.CurrentUserZona+"', '"+DataExchange.CurrentUser.CurrentUserShift+ "', '" + DataExchange.CurrentUser.CurrentUserShift + "')";

                    //  zap1 = "INSERT INTO[dbo].[t_g_counting_content]([id_account],[id_counting],[id_bag],[declared_value],[fact_value],[id_currency],[creation],[lastupdate],[last_user_update]) VALUES('" + accountRow.Cells["id"].Value.ToString() + "','" + counting_id.ToString() + "','" + bag_id.ToString() + "','" + accountRow.Cells["value"].Value.ToString().Replace(",", ".") + "','0','" + accountRow.Cells["id_currency"].Value.ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "')";

                    //////07.11.2019

                    zapros1.Add(zap1);

                    ////////
                    ///

                    //Запись в контент мултисумки
                    
                    
                        if (cbMulti.SelectedIndex != -1)
                        {
                            if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(cbMulti.SelectedValue) && x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))
                                zap1 = "DECLARE @value INT update t_g_multi_bags_content set lastupdate=getdate(), @value = declared_value2, declared_value2 = @value + " + accountRow.Cells["value"].Value.ToString().Replace(",", ".") + ", last_update_user = " + DataExchange.CurrentUser.CurrentUserId + " where id_multi_bag = " + cbMulti.SelectedValue.ToString() + " and id_account = " + accountRow.Cells["id"].Value.ToString();
                            else zap1 = "INSERT INTO [dbo].[t_g_multi_bags_content] ([id_multi_bag],[id_account],[id_currency],[declared_value1],[declared_value2],[fact_value],[creation],[creation_user],[lastupdate],[last_update_user]) VALUES " +
                                    "(" + cbMulti.SelectedValue.ToString() + ", " + accountRow.Cells["id"].Value.ToString() + ", " + accountRow.Cells["id_currency"].Value.ToString() + ", 0, " + accountRow.Cells["value"].Value.ToString().Replace(",", ".") + ", 0," +
                                    " getdate(), " + DataExchange.CurrentUser.CurrentUserId.ToString() + ", GETDATE(), " + DataExchange.CurrentUser.CurrentUserId.ToString() + ")";
                            zapros1.Add(zap1);
                        }
                    ////


                    if (countDenomDataSet.Tables[0].Rows.Count > 0)
                    {
                        countDenomDataSet.Tables[0].TableName = "t_g_declared_denom";
                        foreach (DataRow cntDenomRow in countDenomDataSet.Tables["t_g_declared_denom"].Rows)
                        {

                            if (accountRow.Cells["id"].Value.ToString() == cntDenomRow["id_account"].ToString())
                            {
                                zap1 = "INSERT INTO[dbo].[t_g_declared_denom]([id_bag],[id_denomination],[denomcount],[id_account],[creation],[lastupdate],[last_user_update],[id_currency],[declared_value],[id_counting]) VALUES('" + bag_id.ToString() + "','" + cntDenomRow["id_denomination"].ToString() + "','" + cntDenomRow["denomcount"].ToString() + "','" + cntDenomRow["id_account"].ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + cntDenomRow["id_currency"].ToString() + "','" + cntDenomRow["declared_value"].ToString().Replace(",", ".") + "','" + counting_id.ToString() + "')";
                                zapros1.Add(zap1);
                            }

                        }


                    }

                    ////////

                }

            }

            int ih1 = dataBase.Zapros2(zapros1, 1);
            //MessageBox.Show(ih1.ToString());
            if (ih1 == 1)
            {


                ////09.12.2019
                //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
                if (cbsort.SelectedIndex == 0)
                    sort = "fl_prov";
                else if (cbsort.SelectedIndex == 1)
                    sort = "name";
                else sort = "creation";
                countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);

                ////09.12.2019

                dgCounting.DataSource = countingDataSet.Tables[0];
                //   dgCounting.Refresh();
                //   dgCounting.Update();

                ////11.11.2019
                //     dgCounting.CurrentCell = dgCounting["name", dgCounting.RowCount - 1];
                dgCounting.CurrentCell = dgCounting["name", ivudstr];
                
                ////11.11.2019


                bagsDataSet = dataBase.GetData("t_g_bags");


                countContentDataSet = dataBase.GetData("t_g_counting_content");

                // countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");

                cardsDBDataSet = dataBase.GetData("t_g_cards");

                ///////04.12.2019
               // MessageBox.Show("Операция выполнена! Пересчёт изменен.");
                ///////04.12.2019

                //  dgCounting.CurrentCell = dgCounting["name", dgCounting.RowCount - 1];
            }
            else
                MessageBox.Show("Ошибка выполнения операции!");


            /*
            //Измение номера сумки
            if (tbNumBag.Text != String.Empty)
            {
                bagsRow["name"] = tbNumBag.Text;
            }
            else
            {
                MessageBox.Show("Введите номер сумки");
                return;
            }

            
            bagsRow["creation"] = bagsRow["creation"];
            bagsRow["lastupdate"] = DateTime.Now;
            bagsRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
          
            paramDataSet.Add(bagsDataSet);
            paramTables.Add("t_g_bags");
            //dataBase.UpdateData(bagsDataSet, "t_g_bags");
           
            //Изменение данных по пересчету
            DataRow countRow = countingDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == counting_id).First<DataRow>();
            countRow["id_client"] = cbClient.SelectedValue;
            countRow["id_bag"] = bag_id;
            countRow["creation"] = countRow["creation"];
            countRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
            countRow["lastupdate"] = DateTime.Now;
            
            //Изменение названия песчета
            string cardName = (dgCards.RowCount > 0 ? dgCards["BeginCard", 0].Value.ToString() + "_" : String.Empty);
            countRow["name"] = cardName + cbID.Text + "_" + DateTime.Now.ToShortDateString().Replace(".", "");
            countRow["date_deposit"] = dtDeposit.Value;
            countRow["deleted"] = 0;
            //dataBase.UpdateData(countingDataSet, "t_g_counting");

            paramDataSet.Add(countingDataSet);
            paramTables.Add("t_g_counting");


            //Изменение КР?????????????
            if (cardsDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow card in cardsDataSet.Tables[0].Rows)
                {
                    if (card["id_begin"] != DBNull.Value && cardsDBDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id") == Convert.ToInt64(card["id_begin"])))
                    {
                        DataRow cardsRow = cardsDBDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(card["id_begin"])).First<DataRow>();
                        cardsRow["name"] = card["beginCard"];
                    }
                    else
                    {
                        DataRow cardsRow = cardsDBDataSet.Tables[0].NewRow();
                        cardsRow["name"] = card["BeginCard"];
                        cardsRow["id_bag"] = bag_id;
                        cardsRow["type"] = 0;
                        cardsRow["creation"] = DateTime.Now;
                        cardsRow["lastupdate"] = DateTime.Now;
                        cardsRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                        cardsRow["id_counting"] = counting_id;
                        cardsDBDataSet.Tables[0].Rows.Add(cardsRow);
                    }
                }
                //dataBase.UpdateData(cardsDBDataSet, "t_g_cards");
                paramDataSet.Add(cardsDBDataSet);
                paramTables.Add("t_g_cards");
            }



            foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
            {
                
                if (Convert.ToBoolean(accountRow.Cells["state"].Value) == true)
                {
                    if (countContentDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)))
                    {
                        
                        DataRow contentRow = countContentDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_account") == Convert.ToInt64(accountRow.Cells["id"].Value)).First<DataRow>();
                       

                        contentRow["id_account"] = accountRow.Cells["id"].Value;
                        contentRow["id_counting"] = counting_id;
                        contentRow["id_bag"] = bag_id;
                        contentRow["declared_value"] = accountRow.Cells["value"].Value;
                        contentRow["fact_value"] = 0;
                        contentRow["id_currency"] = accountRow.Cells["id_currency"].Value;
                    }
                    else
                    {
                        DataRow contentRow = countContentDataSet.Tables[0].NewRow();
                        contentRow["id_account"] = accountRow.Cells["id"].Value;
                        contentRow["id_counting"] = counting_id;
                        contentRow["id_bag"] = bag_id;
                        contentRow["declared_value"] = accountRow.Cells["value"].Value;
                        contentRow["fact_value"] = 0;
                        contentRow["id_currency"] = accountRow.Cells["id_currency"].Value;
                        countContentDataSet.Tables[0].Rows.Add(contentRow);
                    }
                    
                }

            }
            //dataBase.UpdateData(countContentDataSet, "t_g_counting_content");

            paramDataSet.Add(countContentDataSet);
            paramTables.Add("t_g_counting_content");

            DataSet contentDenomDBDataset = dataBase.GetData("t_g_declared_denom", "id_counting", counting_id.ToString());
            if (countDenomDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow cntDenomRow in countDenomDataSet.Tables[0].Rows)
                {
                    if (cntDenomRow["id"] == null || cntDenomRow["id"] == DBNull.Value)
                    {
                        cntDenomRow["id_bag"] = bag_id;
                        //cntDenomRow["id_card"] = cardName
                        cntDenomRow["id_counting"] = counting_id;
                    }
                    else
                    {
                        ////
                        
                    }
                }
                //dataBase.UpdateData(countDenomDataSet, "t_g_declared_denom");

                paramDataSet.Add(countDenomDataSet);
                paramTables.Add("t_g_declared_denom");
            }

            dataBase.UpdateDataTransaction(paramDataSet, paramTables);

            */
            ///30.10.2019
            
            ////03.07.2021 АКТ
            if(id_bag!=0 & bagdefectfactorDataSet!=null)
                if(bagdefectfactorDataSet.Tables[0].Rows.Count>0)
                {
                    DataSet d2 = dataBase.GetData9("select * from t_w_bagdefect where id_bag="+id_bag);
                    string ss = "";
                    for (int i = 0; i < bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                    {
                        if (bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString() == "1")
                        {
                            if (ss == "")
                                ss += bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString();
                            else ss +=","+ bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString();

                            ///Проверка 
                            if (!d2.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_bagdefectfactor") == Convert.ToInt64(bagdefectfactorDataSet.Tables[0].Rows[i]["id"])))
                            {
                                String s = "INSERT INTO [dbo].[t_w_bagdefect] " +
                                      "([num_defect],        [id_bag],                   [id_bagdefectfactor],                                         [creation],[lastupdate],[last_update_user])" +
                              " VALUES ('" + Num_defect + "'," + id_bag.ToString() + " ," + bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString() + ",getdate(), getdate()," + DataExchange.CurrentUser.CurrentUserId + ")";
                                Console.WriteLine(s);
                                dataBase.GetData9(s);
                            }
                        }
                    }

                    dataBase.GetData9("delete from t_w_bagdefect where id_bagdefectfactor not in("+ss+")");
                }


            bagdefectfactorDataSet = null;
            id_bag=0;
            

            /////05.01.2020
            if (button6.Enabled == true)
                Button5_Click(sender, e);
            /////05.01.2020
            PrepareData();
            comboBox7_SelectedIndexChanged(sender, e);

            //MessageBox.Show("Finish");
        }
        #endregion

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ///////31.10.2019

            DialogResult result = MessageBox.Show(
       "Удалить пересчёт?"
       ,
       "Сообщение",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Information,
       MessageBoxDefaultButton.Button1);
       //,
       //MessageBoxOptions.DefaultDesktopOnly);
       

            if (result == DialogResult.No)
                return;



            ///////31.10.2019
            countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["deleted"] = 1;
            dataBase.UpdateData(countingDataSet, "t_g_counting");


            ////14.08.2020
            if (id_multi_bag1 != 0)
                dataSet = dataBase.GetData9("DECLARE @count INT UPDATE t_g_multi_bags SET @count = count_bags2, count_bags2 = @count - 1 WHERE id = " + id_multi_bag1.ToString().Trim());

            //17.08.2020
            //Запись счетов в контент мультисумки
            dataSet = dataBase.GetData("t_g_multi_bags_content");
            if (id_multi_bag1 != 0)
                if (dgAccountDeclaredCopy.Rows.Count > 0)
                    foreach (DataGridViewRow accountRow1 in dgAccountDeclaredCopy.Rows)
                    {



                        if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multi_bag1) && x.Field<Int64>("id_account") == Convert.ToInt64(accountRow1.Cells["id"].Value)))
                        {

                            dataSet =dataBase.GetData9("DECLARE @value INT update t_g_multi_bags_content set lastupdate=getdate(), @value = declared_value2, declared_value2 = @value - " + accountRow1.Cells["value"].Value.ToString().Replace(",", ".") + ", last_update_user = " + DataExchange.CurrentUser.CurrentUserId.ToString() + " where id_multi_bag = " + id_multi_bag1.ToString() + " and id_account = " + accountRow1.Cells["id"].Value.ToString());
                            
                        }
                    }
            //

            ///

            ////09.12.2019
            //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
            if (cbsort.SelectedIndex == 0)
                sort = "fl_prov";
            else if (cbsort.SelectedIndex == 1)
                sort = "name";
            else sort = "creation";
            countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);

            ////09.12.2019

            dgCounting.DataSource = countingDataSet.Tables[0];

            ///////04.12.2019
            //MessageBox.Show("Операция выполнена! Пересчёт удален.");
            ///////04.12.2019
            

            /////05.01.2020
            if (button6.Enabled == true)
                Button5_Click(sender, e);
            /////05.01.2020
            ///
            MessageBox.Show("Удаление завершено!");
            PrepareData();
            comboBox7_SelectedIndexChanged(sender, e);
        }

        private void TbNumBag_TextChanged(object sender, EventArgs e)
        {
            /////31.12.2019
            prov_add();
            /////31.12.2019

            if (tbNumBag.Text != String.Empty && bagsName != tbNumBag.Text)
            {
                if(pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;

                /////31.12.2019
                //    btnAdd.Enabled = true;
                /////31.12.2019

                /////20.11.2019
                // if (dnachism.ToString().Trim() == "")
                dnachism = DateTime.Now.ToString();
                /////20.11.2019


                



            }
            else
            {
                ////28.10.2019
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                ////28.10.2019

            }

        }

        ///11.10.2019
        private void tbNumBag_KeyUp(object sender, KeyEventArgs e)


        {

            if (tbNumBag.Text != String.Empty)
            {
               
            timer1.Interval = 3000;
            
            timer1.Tick += obrabfile1;
                timer1.Enabled = true;


            }

            /////05.01.2020

            if (e.KeyCode==Keys.Enter)
            textBox2.Focus();

            /////05.01.2020

            /*
                        if ( tbNumBag.Text != String.Empty)
                        {
                            e.SuppressKeyPress = true;
                            cbID.Enabled = true;
                            cbClient.Enabled = true;
                            cbAccount.Enabled = true;
                            cbEncashPoint.Enabled = true;
                            dtDeposit.Enabled = true;
                            tbCard.Enabled = true;
                            cbClosedCard.Enabled = true;
                            btnAdd.Enabled = true;
                            btnModify.Enabled = true;
                        }
            */
        }

        private void DgAccountDeclared_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

           


                /*
                if ((bool)dgAccountDeclared[intNumColumnDeleteAttribute, e.RowIndex].Value == true)
                { dgAccountDeclared.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red; }
                else { dgAccountDeclared.Rows[e.RowIndex].DefaultCellStyle.BackColor = SystemColors.ControlLightLight; }
                */
            }

        private void DgAccountDeclared_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
///
        
    }

        private void DgAccountDeclared_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void DgAccountDeclared_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            long i2 = 0;
            if (dgAccountDeclared[4, e.RowIndex].Value != null)
                if (dgAccountDeclared[4, e.RowIndex].Value.ToString()!="")
                    if (long.TryParse(dgAccountDeclared[4, e.RowIndex].Value.ToString().Replace(" ",""), out i2))
                    {
                        //
                        //dgAccountDeclared[4, e.RowIndex].FormattedValue = "### ### ### ###";
                        dgAccountDeclared[4, e.RowIndex].Style.Format = "### ### ### ###";

                       //MessageBox.Show( dgAccountDeclared[4, e.RowIndex].Style.Format.ToString());
                       // MessageBox.Show(dgAccountDeclared.Columns[4].DefaultCellStyle.Format.ToString());
                            //dgAccountDeclared.Columns[4].DefaultCellStyle.Format = "### ### ### ###";
                    }
            else
                    {
                        MessageBox.Show("Введите корректное число!");
                        dgAccountDeclared[4, e.RowIndex].Value = String.Empty;
                        dgAccountDeclared[0, e.RowIndex].Value = false;
                        return;
                    }

                if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
                // if (dgAccountDeclared.CurrentCell.ColumnIndex < 0 || dgAccountDeclared.CurrentCell.RowIndex < 0) return;
               // int intNumColumnDeleteAttribute = 0;
            //if (dgAccountDeclared.CurrentCell.ColumnIndex == 0)

            /////
            if (dgAccountDeclared[4, e.RowIndex].Value != null)
            {

                if (dgAccountDeclared[4, e.RowIndex].Value.ToString() != "")
                    dgAccountDeclared[0, e.RowIndex].Value = true;

            }
            else
            {
                dgAccountDeclared[0, e.RowIndex].Value = false;
            }

            /////

            //  if (dgAccountDeclared[0, dgAccountDeclared.CurrentCell.RowIndex].Value != null)
            if (dgAccountDeclared[0, e.RowIndex].Value != null)
                    {

                        // Boolean b1 = (bool)dgAccountDeclared[0, dgAccountDeclared.CurrentCell.RowIndex].Value;
                        Boolean b1 = (bool)dgAccountDeclared[0, e.RowIndex].Value;
                        if (b1 == true)

                        {
                            //if (dgAccountDeclared[3, dgAccountDeclared.CurrentCell.RowIndex].Value.ToString() == "")
                            //    dgAccountDeclared.Rows[dgAccountDeclared.CurrentCell.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.Red;
                            if (dgAccountDeclared[4, e.RowIndex].Value == null)
                                dgAccountDeclared.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                            else
                            if (dgAccountDeclared[4, e.RowIndex].Value.ToString() == "")
                                dgAccountDeclared.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                            else
                                dgAccountDeclared.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;

                        }
                        else
                        {
                    ///31.10.2019
                    /*
                            if (dgAccountDeclared[4, e.RowIndex].Value == null)
                        // MessageBox.Show("1");
                        if (countContentDataSet.Tables[0].Rows[e.RowIndex].RowState != DataRowState.Deleted)
                            countContentDataSet.Tables[0].Rows[e.RowIndex]["declared_value"] = DBNull.Value;
                            else
                            if (dgAccountDeclared[4, e.RowIndex].Value.ToString() != "")
                            {
                                // MessageBox.Show("1");
                                //   countContentDataSet.Tables[0].Rows[e.RowIndex]["declared_value"] = DBNull.Value;
                                //   countContentDataSet.Tables[0].Rows[e.RowIndex]["declared_value"] = 0;
                                dgAccountDeclared[4, e.RowIndex].Value = null;
                               // dgAccountDeclared.Refresh();
                               // dgAccountDeclared.Update();




                            }
                            */
                            ///31.10.2019
                            dgAccountDeclared.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                            // dgAccountDeclared[3, e.RowIndex].Value = "";
                            //  countContentDataSet.Tables[0].Rows[dgAccountDeclared.CurrentCell.RowIndex]["declared_value"] = DBNull.Value;
                        }
                    }



            //////31.12.2019
            dgAccountDeclared.AutoResizeColumns();
            
            prov_add();

            

            if ((dgAccountDeclared.Rows.Count - 1) == e.RowIndex)
                tbCard.Focus();
            if (dgAccountDeclared[4, e.RowIndex].Value != null)
                if (dgAccountDeclared[4, e.RowIndex].Value.ToString().Trim() != "")
                    dgAccountDeclared[4, e.RowIndex].Value = razr(Convert.ToInt64(dgAccountDeclared[4, e.RowIndex].Value.ToString().Replace(" ", "")));

            
        }
        
        private string razr(Int64 a)
        {
            
            string b = "";
            for(int i= 0; a!=0; i++)
            {
                if (i == 3 | i == 6 | i == 9 | i == 12)
                {
                    b = (a % 10).ToString() + " " + b.Substring(0, b.Length);
                    a /= 10;
                }
                else
                {
                    b = (a % 10).ToString() + b.Substring(0, b.Length);
                    a /= 10;
                }           
            }          
            return b;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tbCard.Text = tbCard.Text.Replace("+", "").Trim();
            tbCard.Text = tbCard.Text.Replace("-", "").Trim();

            Int64 i1 = 0;

            if (tbCard.Text != String.Empty)
            {
                if (!Int64.TryParse(tbCard.Text.ToString().Trim(), out i1))
                {

                    MessageBox.Show("Номер карты должна быть целым числом!");
                    tbCard.Text = String.Empty;
                    //tbCard.Text = "";

                    return;

                }


                dataSet = dataBase.GetData9("select * from t_g_cards where name = '" + tbCard.Text.Trim() + "'");
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    if(cardsDataSet.Tables[0].AsEnumerable().Any(x=>x.Field<string>("BeginCard")== tbCard.Text))
                    {
                        MessageBox.Show("Такая карта есть в списке, введите другую карту!");
                        tbCard.Text = String.Empty;
                        return;
                    }
                    DataRow row = cardsDataSet.Tables[0].NewRow();
                    row["BeginCard"] = tbCard.Text;

                    cardsDataSet.Tables[0].Rows.Add(row);
                    tbCard.Text = String.Empty;
                    if(pm.EnabledPossibility(perm, btnModify))
                        btnModify.Enabled = true;
                    //  btnAdd.Enabled = true;

                    /////31.12.2019
                    prov_add();
                    /////31.12.2019

                    // MessageBox.Show("Операция выполнена! Карточка добавлена.");
                }
                else
                {
                    MessageBox.Show(tbCard.Text + " - карта уже существует в базе обработок, введите другую карту!");
                    tbCard.Text = "";
                    tbCard.Text = String.Empty;

                } 
                //tbCard.Text = String.Empty;
            }
        }

        private void CbClient_TextChanged(object sender, EventArgs e)
        {
            

            /*
            clientsDataSet = clientsDataSet1;

            // cbClient.DataSource = clientsDataSet.Tables[0];
            string text = cbClient.Text;
            // if (!string.IsNullOrEmpty(cbClient.Text) && clientsDataSet.Tables[0].Count(s => s.Contains(cbClient.Text)) > 0)
            //      cbClient.DataSource = clientsDataSet.Tables[0].Where(s => s.Contains(cbClient.Text)).ToList();
            string selectString = "name like '%" + cbClient.Text.Trim().ToString() + "%'";
            DataRow[] searchedRows = ((DataTable)currencyDataSet.Tables[0]).Select(selectString);
            //int i1 = ((DataTable)clientsDataSet.Tables[0]).Select(selectString)).;

            if (searchedRows.Count() > 0)

                cbClient.DataSource = ((DataTable)clientsDataSet.Tables[0]).Select(selectString).CopyToDataTable();

            // string s2 = searchedRows[0].Field<string>("curr_code");
            if (string.IsNullOrEmpty(cbClient.Text))
                cbClient.DataSource = clientsDataSet.Tables[0];
            cbClient.Text = text;
            */


            /*
          //  cbClient.IsDropDownOpen = true;
            // убрать selection, если dropdown только открылся
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(CB.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            */

            // if (cbClient.Text.Trim() == "")
            //    PrepareData();
            // MessageBox.Show("1");
            /*
            if (cbClient.Text.Trim() != "")
            {
                string selectString = "name like '" + cbClient.Text.Trim().ToString() + "%'";
                clientsDataSet.Tables[0].Select(selectString);

            }
            */
            /*
            string selectString =
      "BIN = '" + tbID.Text.Trim().ToString() + "'";
            DataRow[] searchedRows =
    ((DataTable)dgList.DataSource).
        Select(selectString);

            int result1 = searchedRows.Length;
            if (result1 > 0)
            {
                MessageBox.Show("Уже есть такой БИН!");
                return;
            }
            */
            //  clientsDataSet.Tables[0]

        }

        private void CbID_TextChanged(object sender, EventArgs e)
        {
          //  if (cbID.Text.Trim() == "")
          //     PrepareData();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            dgCounting_CellContentDoubleClick(dgCounting, new DataGridViewCellEventArgs(0, dgCounting.CurrentCell.RowIndex));

            ////31.12.2019
            btnAdd.Enabled = false;
            ////31.12.2019
        }

        private void CbClient_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbClient.Text.Trim() != "")
            {
                string text = cbClient.Text;

                string selectString = "name like '%" + cbClient.Text.Trim().ToString() + "%'";
                DataRow[] searchedRows = ((DataTable)clientsDataSet.Tables[0]).Select(selectString);
                
                if (searchedRows.Count() > 0)
                    cbClient.DataSource = ((DataTable)clientsDataSet.Tables[0]).Select(selectString).CopyToDataTable();
                else
                    cbClient.DataSource = clientsDataSet.Tables[0];

                cbClient.Text = text;
                cbClient.SelectionStart = text.Length;

            }
            else
                cbClient.DataSource = clientsDataSet.Tables[0];

            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                cbAccount.Focus();

            /////05.01.2020

        }

        private void TabControl1_TabIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void filtr1(int i1)
        {
            ///31.12.2019

            if (counting_vub > -1)
            {
                ///31.12.2019
                if (i1 == 2)
                    btnUpdate.Enabled = false;

                if (i1 == 1)
                {
                    if ((rblCurrency.DataSource != null) & (rblCurrency.SelectedValue != null))
                        // detper1 = dataBase.GetData9("select t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,t1.nominal Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,(case when t2.source = 1 then t2.fact_value else null end) Sumbp1,(case when t2.source = 0 then t2.fact_value else null end) Sump1,(case when t2.source = 2 then t2.fact_value else null end) Summ1,sum(t2.fact_value) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 left join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end) order by t1.name_sost,t1.nominal");

                        /////30.12.2019

                        /////09.12.2019
                        // detper1 = dataBase.GetData9("select t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,(case when t2.source = 1 then t2.fact_value else null end) Sumbp1,(case when t2.source = 0 then t2.fact_value else null end) Sump1,(case when t2.source = 2 then t2.fact_value else null end) Summ1,sum(t2.fact_value) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 left join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end) order by t1.name_sost,t1.nominal");

                        //    detper1 = dataBase.GetData9("select t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,(case when t2.source = 1 then t2.fact_value else null end) Sumbp1,(case when t2.source = 0 then t2.fact_value else null end) Sump1,(case when t2.source = 2 then t2.fact_value else null end) Summ1,sum(t2.fact_value) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end) order by t1.name_sost,t1.nominal");

                        /////09.12.2019

                        detper1 = dataBase.GetData9("select cast(source as int) source, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end)  as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end)  as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end)  as float) Summ1,sum(cast(t2.fact_value  as float)) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),source order by t1.name_sost,t1.nominal");

                    /////30.12.2019

                }
                else
                {
                    string s1 = "";

                    if ((rblcard.DataSource != null) & (rblcard.SelectedValue != null))
                        if (rblcard.SelectedValue.ToString() != "0")
                        {
                            s1 = s1 + " and t2.id_card='" + rblcard.SelectedValue.ToString() + "' ";
                        }
                    if ((rblCondition.DataSource != null) & (rblCondition.SelectedValue != null))
                        if (rblCondition.SelectedValue.ToString() != "0")
                        {
                            s1 = s1 + " and t1.id_sost='" + rblCondition.SelectedValue.ToString() + "' ";
                        }

                    if ((rblCurrency.DataSource != null) & (rblCurrency.SelectedValue != null))
                         detper1 = dataBase.GetData9(
                " select cast(source as int) source, tab.id_card,	tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1,sum(tab.Kolbp1) Kolbp1,sum(tab.Kolp1) Kolp1,sum(tab.Kolm1) Kolm1,sum(tab.Sumbp1) Sumbp1,sum(tab.Sump1) Sump1,sum(tab.Summ1) Summ1, sum(tab.Sumob1) Sumob1 from(select t2.source, t2.id_card, t2.id_counting, t1.id_sost, t1.id_denom, t1.id_currency, t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, sum(cast((case when t2.source = 1 then t2.count else 0 end) as bigint)) Kolbp1,sum(cast((case when t2.source = 0 then t2.count else 0 end)as bigint))Kolp1,sum(cast((case when t2.source = 2 then t2.count else 0 end)as bigint)) Kolm1,sum(cast((case when t2.source = 1 then t2.fact_value else 0 end) as decimal)) Sumbp1,sum(cast((case when t2.source = 0 then t2.fact_value else 0 end) as decimal)) Sump1,sum(cast((case when t2.source = 2 then t2.fact_value else 0 end) as decimal)) Summ1, sum(cast(t2.fact_value as decimal)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency = '" + rblCurrency.SelectedValue.ToString() + "'" + s1.ToString() + " group by t2.source, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom, t1.id_currency,t3.name,t1.nominal, t1.name_sost) tab where Sumob1 <> 0 group by  tab.id_card,tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1, source order by id_denom");
                    
                }

                if (detper1 != null)
                    if (detper1.Tables[0] != null)
                    {
                        dataGridView2.DataSource = detper1.Tables[0];

                        detper3 = detper1.Clone();
                        //detper3.Tables[0].Columns.Add("sourse", typeof(Int16));
                    }
                //////30.12.2019


                dataGridView2.AutoResizeColumns();





                dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;


                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                dataGridView2.Paint -= new PaintEventHandler(dataGridView2_Paint);



                dataGridView2.Scroll -= new ScrollEventHandler(dataGridView2_Scroll);

                dataGridView2.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);


                dataGridView2.Paint += new PaintEventHandler(dataGridView2_Paint);



                dataGridView2.Scroll += new ScrollEventHandler(dataGridView2_Scroll);

                dataGridView2.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView2_ColumnWidthChanged);

                #region dataGridView9

                dataGridView9.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;


                dataGridView9.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                dataGridView9.Paint -= new PaintEventHandler(dataGridView9_Paint);



                dataGridView9.Scroll -= new ScrollEventHandler(dataGridView9_Scroll);

                dataGridView9.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView9_ColumnWidthChanged);


                dataGridView9.Paint += new PaintEventHandler(dataGridView9_Paint);



                dataGridView9.Scroll += new ScrollEventHandler(dataGridView9_Scroll);

                dataGridView9.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView9_ColumnWidthChanged);


                #endregion dataGridView9
                //27.07.2020
                //баланс для юзера
                userBalansDataSet = null;
                userBalansDataSet = dataBase.GetData9("PROC_BALANCE_1 "+ DataExchange.CurrentUser.CurrentUserId.ToString()+", 2007");




                //////30.12.2019

                ///31.12.2019

            }
                    ///31.12.2019

        }

        private void filtr2(int i1)
        {
            
            if (counting_vub > -1)
            {
                ///31.12.2019
                if (i1 == 2)
                    btnUpdate.Enabled = false;

                if (i1 == 1)
                {
                    if ((rblCurrency.DataSource != null) & (rblCurrency.SelectedValue != null))
                        // detper1 = dataBase.GetData9("select t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,t1.nominal Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,(case when t2.source = 1 then t2.fact_value else null end) Sumbp1,(case when t2.source = 0 then t2.fact_value else null end) Sump1,(case when t2.source = 2 then t2.fact_value else null end) Summ1,sum(t2.fact_value) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 left join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end) order by t1.name_sost,t1.nominal");

                        /////30.12.2019

                        /////09.12.2019
                        // detper1 = dataBase.GetData9("select t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,(case when t2.source = 1 then t2.fact_value else null end) Sumbp1,(case when t2.source = 0 then t2.fact_value else null end) Sump1,(case when t2.source = 2 then t2.fact_value else null end) Summ1,sum(t2.fact_value) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 left join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end) order by t1.name_sost,t1.nominal");

                        //    detper1 = dataBase.GetData9("select t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,(case when t2.source = 1 then t2.fact_value else null end) Sumbp1,(case when t2.source = 0 then t2.fact_value else null end) Sump1,(case when t2.source = 2 then t2.fact_value else null end) Summ1,sum(t2.fact_value) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end) order by t1.name_sost,t1.nominal");

                        /////09.12.2019

                        detper1 = dataBase.GetData9("select cast(source as int) source, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom,t1.id_currency,t3.name Card1,cast(t1.nominal as int) Nomin1,t1.name_sost sost1,(case when t2.source = 1 then t2.count else null end) Kolbp1,(case when t2.source = 0 then t2.count else null end) Kolp1,(case when t2.source = 2 then t2.count else null end) Kolm1,cast((case when t2.source = 1 then t2.fact_value else null end)  as float) Sumbp1,cast((case when t2.source = 0 then t2.fact_value else null end)  as float) Sump1,cast((case when t2.source = 2 then t2.fact_value else null end)  as float) Summ1,sum(cast(t2.fact_value  as float)) Sumob1 from (select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) where t2.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "' and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' group by t2.id,t2.id_card,t2.id_counting, t1.id_sost,t1.id_denom,t1.id_currency,t3.name,t1.nominal, t1.name_sost,(case when t2.source = 1 then t2.count else null end) ,(case when t2.source = 0 then t2.count else null end) ,(case when t2.source = 2 then t2.count else null end) ,(case when t2.source = 1 then t2.fact_value else null end) ,(case when t2.source = 0 then t2.fact_value else null end) ,(case when t2.source = 2 then t2.fact_value else null end),source order by t1.name_sost,t1.nominal");

                    /////30.12.2019

                }
                if (i1 == 2)
                {
                    string s1 = "";

                    if ((rblcard2.DataSource != null) & (rblcard2.SelectedValue != null))
                        if (rblcard2.SelectedValue.ToString() != "0")
                        {
                            s1 = s1 + " and t2.id_card='" + rblcard2.SelectedValue.ToString() + "' ";
                        }
                    if ((rblCondition2.DataSource != null) & (rblCondition2.SelectedValue != null))
                        if (rblCondition2.SelectedValue.ToString() != "0")
                        {
                            s1 = s1 + " and t1.id_sost='" + rblCondition2.SelectedValue.ToString() + "' ";
                        }

                    if ((rblCurrency2.DataSource != null) & (rblCurrency2.SelectedValue != null))

                        detOtchetDataSet = dataBase.GetData9("select cast(source as int) source, tab.id_card,	tab.bag bag1, tab.valut Valut1, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1, tab.sost1,sum(tab.Kolbp1) Kolbp1,sum(tab.Kolp1) Kolp1,sum(tab.Kolm1) Kolm1,sum(tab.Sumbp1) Sumbp1, sum(tab.Sump1) Sump1,sum(tab.Summ1) Summ1, sum(tab.Sumob1) Sumob1 from(select t2.source, t1.bag, t1.valut, t2.id_card, t2.id_counting, t1.id_sost, t1.id_denom, t1.id_currency, t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, sum(cast((case when t2.source = 1 then t2.count else 0 end) as bigint)) Kolbp1,sum(cast((case when t2.source = 0 then t2.count else 0 end)as bigint))Kolp1,sum(cast((case when t2.source = 2 then t2.count else 0 end)as bigint)) Kolm1,sum(cast((case when t2.source = 1 then t2.fact_value else 0 end) as decimal)) Sumbp1,sum(cast((case when t2.source = 0 then t2.fact_value else 0 end) as decimal)) Sump1,sum(cast((case when t2.source = 2 then t2.fact_value else 0 end) as decimal)) Summ1, sum(cast(t2.fact_value as decimal)) Sumob1 from( select t1.id id_sost, b1.[name] bag,b3.curr_code valut,t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) left join t_g_counting b2 on t2.id_counting=b2.id left join t_g_bags b1 on t2.id_bag=b1.id left join t_g_currency b3 on t4.id_currency=b3.id " +
                            " where b2.id_multi_bag='" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] + "' " +
                            "and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) " +
                            "where t1.id_currency = " + rblCurrency2.SelectedValue.ToString()+ s1.ToString()+
                            " group by t2.source,t1.bag, t1.valut, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom, t1.id_currency,t3.name,t1.nominal, t1.name_sost) tab where Sumob1 <> 0 group by  tab.id_card, tab.bag, tab.valut, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1, source order by tab.bag, id_denom " +
                            "");
                }
                
                if (detOtchetDataSet != null)
                    if (detOtchetDataSet.Tables[0] != null)
                    {
                        dataGridView9.DataSource = detOtchetDataSet.Tables[0];
                    }
                

                dataGridView9.AutoResizeColumns();
                              

                dataGridView9.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView9.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                dataGridView9.Paint -= new PaintEventHandler(dataGridView9_Paint);
                dataGridView9.Scroll -= new ScrollEventHandler(dataGridView9_Scroll);
                dataGridView9.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView9_ColumnWidthChanged);
                dataGridView9.Paint += new PaintEventHandler(dataGridView9_Paint);
                dataGridView9.Scroll += new ScrollEventHandler(dataGridView9_Scroll);
                dataGridView9.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView9_ColumnWidthChanged);

            }
            

        }

        private void detal1()
        {
            //countingDataSet = null;
            //countingDataSet = dataBase.GetData10("select * from t_g_counting where deleted='0' and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by fl_prov");

            dgCounting.DataSource = countingDataSet.Tables[0];

            comboBox10.SelectedIndex = -1;
            ///31.12.2019

            if ( counting_vub >-1)
            {
                

              if (tabControl1.SelectedIndex == 2)
              {
                    if(dgCounting.CurrentCell!=null)
                 if (dgCounting.CurrentCell.RowIndex > -1)
                 {
                    if (dgCounting.CurrentCell.RowIndex <= countingDataSet.Tables[0].Rows.Count)
                    {

                        obchper2 = dataBase.GetData9(
                         "select t1.Val1,cast(t1.Nomin1 as float) as  Nomin1,t1.Colzajv1 as Colzajv1,cast(t1.Sumzajvn1 as float) as Sumzajvn1, sum(t1.Colper1) as Colper1, sum(cast(t1.Sumpern1 as float)) as Sumpern1, isnull(sum(t1.Colper1),0)-isnull(t1.Colzajv1,0) as Raskol1	   ,   cast(isnull(sum(cast(t1.Sumpern1 as float)),0)-isnull(t1.Sumzajvn1,0) as float) as Rassum1  from (select (case  when p1.curr_code is null then p2.curr_code else p1.curr_code end) as Val1,       (case  when p1.value is null then p2.value else p1.value end) as Nomin1,	   p2.Colzajv1, p2.Sumzajvn1, p1.Colper1, p1.Sumpern1 from (select   t5.fact_value as Sumpern1, t5.count as Colper1, t5.id_denomination as id_denomination, t3.value as value, t9.curr_code as curr_code from t_g_counting_denom as t5   left join t_g_denomination as t3 on(t5.id_denomination = t3.id) left join t_g_currency as t9 on(t3.id_currency = t9.id) " +
                        "where t5.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'" +
                        " and t5.count<>0) as p1 full join( select t6.declared_value as Sumzajvn1, t6.denomcount as Colzajv1,t6.id_currency as id_currency,t6.id_denomination as id_denomination,t7.value as value,t9.curr_code as curr_code from t_g_declared_denom as t6 inner join t_g_denomination as t7 on(t6.id_denomination = t7.id) left join t_g_currency as t9 on(t7.id_currency = t9.id) " +
                        "where t6.id_counting = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'" +
                        " and t6.denomcount<>0) as p2 on(p1.id_denomination = p2.id_denomination) )t1 group by Val1,Nomin1,t1.Colzajv1,t1.Sumzajvn1 order by Val1,Nomin1");
                          
                            dataGridView3.DataSource = obchper2.Tables[0];

                                //           obchper3 = dataBase.GetData9(
                                //               "declare @id_counting int =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]+
                                //               "select tab11.Val1 as Val1, sum(tab11.Sumzajvn1) as Sumzajvn1, sum(tab11.Sumpern1) as Sumpern1, iif(SUM(tab11.Rassum1) < 0, SUM(tab11.Rassum1) * (-1), SUM(tab11.Rassum1)) as Rassum1, iif(SUM(tab11.Rassum1) > 0, 'излишек', iif(SUM(tab11.Rassum1) = 0, ' ', 'недостача')) as Info from     (select   t1.Val1, cast(t1.Nomin1 as float) as Nomin1,     t1.Colzajv1 as Colzajv1,     cast(t1.Sumzajvn1 as float) as Sumzajvn1,  sum(t1.Colper1) as Colper1, sum(cast(t1.Sumpern1 as float)) as Sumpern1,     isnull(sum(t1.Colper1), 0) - isnull(t1.Colzajv1, 0) as Raskol1,     cast(isnull(sum(cast(t1.Sumpern1 as float)), 0) - isnull(t1.Sumzajvn1, 0) as float) as Rassum1     from     (       select             (case  when p1.curr_code is null then p2.curr_code else p1.curr_code end) as Val1,       			(case  when p1.value is null then p2.value else p1.value end) as Nomin1,	   			p2.Colzajv1, p2.Sumzajvn1, p1.Colper1, p1.Sumpern1             from            (                select                    t5.fact_value as Sumpern1,                    t5.count as Colper1,                    t5.id_denomination as id_denomination,                    t3.value as value,                    t9.curr_code as curr_code                     from t_g_counting_denom as t5                    left join t_g_denomination as t3 on(t5.id_denomination = t3.id)                     left join t_g_currency as t9 on(t3.id_currency = t9.id)                    where t5.id_counting = @id_counting " +
                                //               "  ) as p1     full join  ( select t6.declared_value as Sumzajvn1, t6.denomcount as Colzajv1, t6.id_currency as id_currency, t6.id_denomination as id_denomination, t7.value as value, t9.curr_code as curr_code from t_g_declared_denom as t6 inner join t_g_denomination as t7 on(t6.id_denomination = t7.id) left join t_g_currency as t9 on(t7.id_currency = t9.id) where t6.id_counting = @id_counting " +// countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'
                                //" ) as p2 on(p1.id_denomination = p2.id_denomination))t1 group by Val1, Nomin1, t1.Colzajv1,t1.Sumzajvn1 ) as tab11 group by tab11.Val1 ");

                                DataSet d1 = dataBase.GetData9("select [name] from t_g_bags where id=" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_bag"]);
                                if (d1 != null & d1.Tables[0] != null & d1.Tables[0].Rows.Count > 0)
                                    this.bag_name = d1.Tables[0].Rows[0][0].ToString();
                                obchper3 = dataBase.GetData9("declare @id_counting int =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]+
                                " select curr_code as Val1, declared_value as Sumzajvn1, fact_value as Sumpern1, (fact_value-declared_value) as Rassum1, iif((fact_value-declared_value) > 0, 'излишек', iif((fact_value-declared_value) = 0, ' ', 'недостача')) as Info  from t_g_counting_content t1 left join t_g_counting t2 on t1.id_counting=t2.id left join t_g_currency t3 on t1.id_currency=t3.id where id_counting=@id_counting order by curr_code");
                            dataGridView4.DataSource = obchper3.Tables[0];

                            
                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "0")
                                label21.Text = "подготовлено";
                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "1")
                                label21.Text = "расхождение";
                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "2")
                                label21.Text = "сошлось";
                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "3")
                                label21.Text = "у. расхождение";

                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "4")
                                label21.Text = "общая сумма сошлась, по номиналам расхождение";

                                if (label21.Text == "у. расхождение")
                                    button8.Enabled = true;
                                else
                                    button8.Enabled = false;

                            if (label21.Text != "расхождение")
                            {
                                button13.Enabled = false;
                                comboBox10.Enabled = false;
                                comboBox10.SelectedIndex = -1;

                            }
                            else
                            {
                                if(pm.EnabledPossibility(perm, comboBox10))
                                    comboBox10.Enabled = true;
                                DataSet dataSet = dataBase.GetData9("select * from t_g_cause_description where id_counting="+ countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString());
                                if (dataSet.Tables[0].Rows.Count > 0)
                                    if(pm.EnabledPossibility(perm, button13))
                                        button13.Enabled = true;
                                
                            }
                               
                            ////cause_Discription
                                ///
                                causeDiscDataSet = null;

                            causeDiscDataSet = dataBase.GetData9("SELECT t1.[id],[id_counting], t2.[name],[description] FROM[CountingDB].[dbo].[t_g_cause_description] as t1 left join t_g_cause as t2 on t1.id_cause = t2.id where id_counting =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]);

                            dataGridView5.DataSource = causeDiscDataSet.Tables[0];

                            if (causeDiscDataSet.Tables[0].Rows.Count > 0 & label21.Text== "расхождение")
                                if(pm.EnabledPossibility(perm, button18))
                                    button18.Enabled = true;
                            else
                                button18.Enabled = false;
                            ///

                    }
                 }
              }
                //14.08.2020
                if (tabControl1.SelectedIndex == 3)
                {
                    if(countingDataSet!=null)
                        if (countingDataSet.Tables[0] != null)
                            if (dgCounting.CurrentCell != null)
                    if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"].ToString() == "0")
                    {
                        if(pm.VisiblePossibility(perm, label24))
                            label24.Visible = true;
                        tabControl2.Visible = false;
                    }
                    else
                        if (pm.VisiblePossibility(perm, tabControl2))
                            tabControl2.Visible = true;
                    


                }
                //////
                
                if(tabControl2.Visible)
                {
                    if (tabControl2.SelectedIndex == 1)
                    {

                        if (dgCounting != null)
                            if (dgCounting.RowCount > 0)
                                if (dgCounting.CurrentCell.RowIndex > -1)
                                    if (countingDataSet != null)
                                        if (countingDataSet.Tables[0].Rows.Count > 0)
                                        {
                                            //20.08.2020 Update t_g_multi_bag set fl_prov = [0 или 1 или 2] where id = 
                                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] != null)
                                                if (Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"]) != 0)
                                                {
                                                    dataSet = null;
                                                    string id_multibag = countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"].ToString().Trim();

                                                    dataSet = dataBase.GetData9("select fl_prov from t_g_counting where deleted=0 and id_multi_bag = " + id_multibag);
                                                    int i = 4;

                                                    foreach (DataRow row in dataSet.Tables[0].Rows)
                                                    {   
                                                        if (Convert.ToInt64(row["fl_prov"]) == 0)
                                                        {
                                                            i = 0;
                                                            break;
                                                        }
                                                    }

                                                    dataSet = null;
                                                    if (i != 0)
                                                    {
                                                        dataSet = dataBase.GetData9("select iif(declared_value1=fact_value,2,1) n1 from t_g_multi_bags_content where id_multi_bag =" + id_multibag);
                                                       foreach (DataRow row in dataSet.Tables[0].Rows)
                                                        { 
                                                            //MessageBox.Show("."+row["n1"].ToString().Trim() + ".");

                                                            if (row["n1"].ToString().Trim() == "1")
                                                            {
                                                                i = 1;
                                                                break;
                                                            }
                                                            if (row["n1"].ToString().Trim() == "2")                                                           
                                                                i = 2;
                                                                
                                                            
                                                        }
                                                    }

                                                    dataSet = dataBase.GetData9("select * from t_g_multi_bags where id ="+id_multibag);

                                                    if(dataSet.Tables[0].Rows[0]["fl_prov"].ToString().Trim()!="3")
                                                    {
                                                        dataSet = dataBase.GetData9("Update t_g_multi_bags set fl_prov = " + i.ToString().Trim() + " where id =" + id_multibag);
                                                    }
                                                    //MessageBox.Show(i.ToString());
                                                    if(i==2)
                                                    {
                                                        dataSet = dataBase.GetData9("Update t_g_multi_bags set fl_prov =2 where id =" + id_multibag);
                                                    }

                                                }
                                            #region Состояние мулти сумки по пересчету
                                            
                                            dataSet = null;
                                            string id_multibag1 = countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"].ToString();
                                            dataSet = dataBase.GetData9("Select * from [t_g_multi_bags_content] t1 left join t_g_multi_bags t2 on t1.id_multi_bag=t2.id where t2.[deleted]=0");

                                            if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("status_d")).First<Int32>() == 1)
                                                label25.Text = "сошлось";
                                            if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("status_d")).First<Int32>() == 3)
                                                label25.Text = "Заявленная сумма денег мультисумки больше чем в сумок";
                                            if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("status_d")).First<Int32>() == 2)
                                                label25.Text = "Заявленная сумма денег мультисумки меньше чем в сумок";

                                            if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("fl_prov")).First<Int32>() == 0)
                                            {
                                                label25.Text = "подготовлено";
                                                label35.Text = "подготовлено";
                                            }
                                            else
                                            {
                                                if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("fl_prov")).First<Int32>() == 1)
                                                    label35.Text = "расхождение";
                                                if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("fl_prov")).First<Int32>() == 2)
                                                    label35.Text = "сошлось";
                                                if (dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_multi_bag") == Convert.ToInt64(id_multibag1)).Select(x => x.Field<Int32>("fl_prov")).First<Int32>() == 3)
                                                    label35.Text = "у.расхождение";
                                            }
                                            if (label35.Text == "расхождение")
                                            { 
                                                if(pm.EnabledPossibility(perm, button16))
                                                    button16.Enabled = true;
                                                if (pm.EnabledPossibility(perm, comboBox11))
                                                    comboBox11.Enabled = true;
                                            }
                                            else
                                            {
                                                button16.Enabled = false;
                                                comboBox11.Enabled = false;
                                            }
                                            
                                            #endregion


                                            d2 = dataBase.GetData9("declare @id_multi_bag int = " + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] +
                                                                   " select iif(tab1.bag is null,tab2.bag,tab1.bag) bag, iif(tab1.curr_code is null,tab2.curr_code,tab1.curr_code) Val1, cast(iif(tab1.value is null,  tab2.value,  tab1.value) as float) Nomin1, tab2.Colzajv1, tab2.Sumzajvn1, tab1.Colper1, tab1.Sumpern1, isnull(tab1.Colper1,0)-isnull(tab2.Colzajv1,0) as Raskol1, isnull(tab1.Sumpern1,0)-isnull(tab2.Sumzajvn1,0) as Rassum1	from (select * from ( select  b2.name bag, sum(t5.fact_value) as Sumpern1, sum(t5.count) as Colper1, t5.id_denomination as id_denomination, t3.value as value, t9.curr_code as curr_code from t_g_counting_denom as t5 left join t_g_denomination as t3 on(t5.id_denomination = t3.id) left join t_g_currency as t9 on(t3.id_currency = t9.id) left join t_g_counting b1 on t5.id_counting=b1.id left join t_g_bags b2 on b1.id_bag=b2.id where b1.id_multi_bag=@id_multi_bag and b1.deleted=0 group by b2.name, id_denomination, t3.value,t9.curr_code ) p1 where p1.Colper1<>0 )tab1 full join (select b2.name bag, t6.id_denomination as id_denomination,t7.value as value,t9.curr_code as curr_code, t6.declared_value as Sumzajvn1, t6.denomcount as Colzajv1  from t_g_declared_denom as t6 inner join t_g_denomination as t7 on(t6.id_denomination = t7.id) left join t_g_currency as t9 on(t7.id_currency = t9.id) left join t_g_counting b1 on t6.id_counting=b1.id left join t_g_bags b2 on b1.id_bag=b2.id where b1.id_multi_bag=@id_multi_bag and t6.denomcount>0 and b1.deleted=0) tab2 on tab1.id_denomination=tab2.id_denomination order by bag");
                                            if (d2 != null)
                                                if (d2.Tables[0].Rows.Count > 0)
                                                    dataGridView7.DataSource = d2.Tables[0];

                                            dataGridView7.AutoResizeColumns();

                                            dataGridView7.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                                            //dataGridView7.ColumnHeadersHeight = this.dataGridView7.ColumnHeadersHeight * 3;

                                            dataGridView7.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                                            dataGridView7.Paint -= new PaintEventHandler(dataGridView7_Paint);



                                            dataGridView7.Scroll -= new ScrollEventHandler(dataGridView7_Scroll);

                                            dataGridView7.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView7_ColumnWidthChanged);


                                            dataGridView7.Paint += new PaintEventHandler(dataGridView7_Paint);



                                            dataGridView7.Scroll += new ScrollEventHandler(dataGridView7_Scroll);

                                            dataGridView7.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView7_ColumnWidthChanged);
                                            d3 = null;
                                            if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] != null)
                                                if (Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"]) != 0)
                                                {
                                                    d3 = dataBase.GetData9(
                                  "declare @id_multi_bag int = " + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] +
                                  // " select tab3.bag, tab3.Val1, SUM(cast(tab3.Sumzajvn1 as float))Sumzajvn1, SUM(cast(tab3.Sumpern1 as float))Sumpern1, SUM(cast(tab3.Rassum1 as float))Rassum1, iif(SUM(tab3.Rassum1) > 0, 'излишек', iif(SUM(tab3.Rassum1) = 0, ' ', 'недостача')) as Info from ( select iif(tab1.bag is null,tab2.bag,tab1.bag) bag, iif(tab1.curr_code is null, tab2.curr_code,tab1.curr_code) Val1, cast(tab1.value as float) Nomin1,tab2.Colzajv1, tab2.Sumzajvn1, tab1.Colper1, tab1.Sumpern1, isnull(tab1.Colper1,0)-isnull(tab2.Colzajv1,0) as Raskol1, isnull(tab1.Sumpern1,0)-isnull(tab2.Sumzajvn1,0) as Rassum1	from(select * from (select  b1.id_multi_bag bag, sum(t5.fact_value) as Sumpern1, sum(t5.count) as Colper1, t5.id_denomination as id_denomination, t3.value as value, t9.curr_code as curr_code from t_g_counting_denom as t5  left join t_g_denomination as t3 on(t5.id_denomination = t3.id) left join t_g_currency as t9 on(t3.id_currency = t9.id) left join t_g_counting b1 on t5.id_counting=b1.id left join t_g_bags b2 on b1.id_bag=b2.id where b1.id_multi_bag=@id_multi_bag  group by b1.id_multi_bag, id_denomination, t3.value,t9.curr_code ) p1 where p1.Colper1<>0 )tab1 full join(select	b1.id_multi_bag bag, t6.id_denomination as id_denomination,t7.value as value,t9.curr_code as curr_code, t6.declared_value as Sumzajvn1, t6.denomcount as Colzajv1 from t_g_declared_denom as t6 inner join t_g_denomination as t7 on(t6.id_denomination = t7.id) left join t_g_currency as t9 on(t7.id_currency = t9.id) left join t_g_counting b1 on t6.id_counting=b1.id left join t_g_bags b2 on b1.id_bag=b2.id where b1.id_multi_bag=@id_multi_bag and t6.denomcount>0) tab2 on tab1.id_denomination=tab2.id_denomination) tab3 group by 	bag, tab3.Val1");
                                  " select id_multi_bag bag, t2.curr_code Val1, declared_value1 Sumzajvn1, fact_value Sumpern1, fact_value-declared_value1 Rassum1, iif(fact_value-declared_value1>0,'излишек',iif(fact_value-declared_value1=0,'','недостача')) Info  from t_g_multi_bags_content t1 left join t_g_currency t2 on t1.id_currency=t2.id where id_multi_bag=@id_multi_bag"); 
                                  if (d3 != null)
                                                        dataGridView6.DataSource = d3.Tables[0];

                                                    causeDiscDataSet = null;
                                                    causeDiscDataSet = dataBase.GetData9("SELECT t1.[id],[id_multi_bag], t2.[name],[description] FROM[CountingDB].[dbo].[t_g_cause_description] as t1 left join t_g_cause as t2 on t1.id_cause = t2.id where id_multi_bag =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"]);

                                                    dataGridView8.DataSource = causeDiscDataSet.Tables[0];

                                                    dataSet = null;
                                                    //dataSet = dataBase.GetData("");
                                                }
                                        }
                    }
                    if(dgCounting.CurrentCell!=null)
                    if( tabControl2.SelectedIndex==0)
                    {
                        DataSet d1 = dataBase.GetData9("declare @id_multi_bag int = "+ countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] +
                            " select b1.name bag1, t2.id_currency id_currency, t3.curr_code Valut1,  cast(t2.declared_value as float) Sumzajv1, cast(t2.fact_value as float) Sumper1, cast(t8.Sumzajvn1 as float) as Sumzajvn1, sum( cast(t5.fact_value as float)) Sumpern1, t8.Colzajv1, sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2, Sum( cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4, Sum( cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5, Sum(case when t5.source = 2 then t5.count else null end) col6, Sum( cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1 left join t_g_bags b1 on t1.id_bag =b1.id left join t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  left join (select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1, t6.id_currency from t_g_declared_denom t6 group by t6.id_counting,t6.id_currency)t8 on(t1.id = t8.id_counting) and (t2.id_currency = t8.id_currency)where t1.id_multi_bag = @id_multi_bag  group by b1.name, t2.id_currency, t3.curr_code, t2.declared_value, t2.fact_value,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov");

                        if (d1 != null)
                            if (d1.Tables[0].Rows.Count > 0)
                                dataGridView10.DataSource = d1.Tables[0];

                        conditionDataSet1 = dataBase.GetData("t_g_condition");

                        if (conditionDataSet1.Tables[0].Rows[conditionDataSet1.Tables[0].Rows.Count - 1]["name"].ToString() != "Все")
                        {
                            DataRow conditionAllRow = conditionDataSet1.Tables[0].NewRow();
                            conditionAllRow["id"] = 0;
                            conditionAllRow["name"] = "Все";
                            conditionAllRow["visible"] = false;
                            //conditionAllRow["curr_code"] = "Все";
                            conditionDataSet1.Tables[0].Rows.Add(conditionAllRow);

                        }

                        rblCondition2.DataSource = conditionDataSet1.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == false).CopyToDataTable<DataRow>();

                        cardDataSet1 = dataBase.GetData9("select distinct t1.id,case when t1.fl_obr=1 then 'V' +t1.name else t1.name end name from t_g_cards t1 left join t_g_counting t2 on t1.id_counting=t2.id where t2.id_multi_bag= '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] + "'");


                        if (cardDataSet1.Tables[0].Rows.Count > 0)
                        {

                            if (cardDataSet1.Tables[0].Rows[cardDataSet1.Tables[0].Rows.Count - 1]["name"].ToString() != "Все")
                            {
                                DataRow conditionAllRow = cardDataSet1.Tables[0].NewRow();
                                conditionAllRow["id"] = 0;
                                conditionAllRow["name"] = "Все";


                                cardDataSet1.Tables[0].Rows.Add(conditionAllRow);

                            }
                            if(pm.VisiblePossibility(perm, rblcard2))
                                rblcard2.Visible = true;
                            rblcard2.DataSource = cardDataSet1.Tables[0];
                            //rblcard2.SelectedIndex=
                        }
                        else rblcard2.Visible = false;

                        //DataSet d9;
                        //d9 = dataBase.GetData9("select cast(source as int) source, tab.id_card,	tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1,sum(tab.Kolbp1) Kolbp1,sum(tab.Kolp1) Kolp1,sum(tab.Kolm1) Kolm1,sum(tab.Sumbp1) Sumbp1,sum(tab.Sump1) Sump1,sum(tab.Summ1) Summ1, sum(tab.Sumob1) Sumob1 from(select t2.source, t2.id_card, t2.id_counting, t1.id_sost, t1.id_denom, t1.id_currency, t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, sum(cast((case when t2.source = 1 then t2.count else 0 end) as bigint)) Kolbp1,sum(cast((case when t2.source = 0 then t2.count else 0 end)as bigint))Kolp1,sum(cast((case when t2.source = 2 then t2.count else 0 end)as bigint)) Kolm1,sum(cast((case when t2.source = 1 then t2.fact_value else 0 end) as decimal)) Sumbp1,sum(cast((case when t2.source = 0 then t2.fact_value else 0 end) as decimal)) Sump1,sum(cast((case when t2.source = 2 then t2.fact_value else 0 end) as decimal)) Summ1, sum(cast(t2.fact_value as decimal)) Sumob1 from(select t1.id id_sost, t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom  from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) left join t_g_counting b2 on t2.id_counting=b2.id " +
                        //    "where b2.id_multi_bag='" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] + "' " +
                        //    "and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) group by t2.source, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom, t1.id_currency,t3.name,t1.nominal, t1.name_sost) tab where Sumob1 <> 0 group by  tab.id_card,tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1, source order by id_denom");

                        detOtchetDataSet = dataBase.GetData9("select cast(source as int) source, tab.id_card,	tab.bag bag1, tab.valut Valut1, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1, tab.sost1,sum(tab.Kolbp1) Kolbp1,sum(tab.Kolp1) Kolp1,sum(tab.Kolm1) Kolm1,sum(tab.Sumbp1) Sumbp1, sum(tab.Sump1) Sump1,sum(tab.Summ1) Summ1, sum(tab.Sumob1) Sumob1 from(select t2.source, t1.bag, t1.valut, t2.id_card, t2.id_counting, t1.id_sost, t1.id_denom, t1.id_currency, t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, sum(cast((case when t2.source = 1 then t2.count else 0 end) as bigint)) Kolbp1,sum(cast((case when t2.source = 0 then t2.count else 0 end)as bigint))Kolp1,sum(cast((case when t2.source = 2 then t2.count else 0 end)as bigint)) Kolm1,sum(cast((case when t2.source = 1 then t2.fact_value else 0 end) as decimal)) Sumbp1,sum(cast((case when t2.source = 0 then t2.fact_value else 0 end) as decimal)) Sump1,sum(cast((case when t2.source = 2 then t2.fact_value else 0 end) as decimal)) Summ1, sum(cast(t2.fact_value as decimal)) Sumob1 from( select t1.id id_sost, b1.[name] bag,b3.curr_code valut,t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) left join t_g_counting b2 on t2.id_counting=b2.id left join t_g_bags b1 on t2.id_bag=b1.id left join t_g_currency b3 on t4.id_currency=b3.id " +
                            //"select cast(source as int) source, tab.id_card,	tab.bag bag1, tab.valut Valut1, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1, tab.sost1,sum(tab.Kolbp1) Kolbp1,sum(tab.Kolp1) Kolp1,sum(tab.Kolm1) Kolm1,sum(tab.Sumbp1) Sumbp1, sum(tab.Sump1) Sump1,sum(tab.Summ1) Summ1, sum(tab.Sumob1) Sumob1 from(select t2.source, t1.bag, t1.valut, t2.id_card, t2.id_counting, t1.id_sost, t1.id_denom, t1.id_currency, t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, sum(cast((case when t2.source = 1 then t2.count else 0 end) as bigint)) Kolbp1,sum(cast((case when t2.source = 0 then t2.count else 0 end)as bigint))Kolp1,sum(cast((case when t2.source = 2 then t2.count else 0 end)as bigint)) Kolm1,sum(cast((case when t2.source = 1 then t2.fact_value else 0 end) as decimal)) Sumbp1,sum(cast((case when t2.source = 0 then t2.fact_value else 0 end) as decimal)) Sump1,sum(cast((case when t2.source = 2 then t2.fact_value else 0 end) as decimal)) Summ1, sum(cast(t2.fact_value as decimal)) Sumob1 from( select t1.id id_sost, b1.[name] bag,b3.curr_code valut,t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) left join t_g_counting b2 on t2.id_counting=b2.id left join t_g_bags b1 on t2.id_bag=b1.id left join t_g_currency b3 on t4.id_currency=b3.id" +
//                          "select cast(source as int) source, tab.id_card,	tab.bag bag1, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1,sum(tab.Kolbp1) Kolbp1,sum(tab.Kolp1) Kolp1,sum(tab.Kolm1) Kolm1,sum(tab.Sumbp1) Sumbp1,sum(tab.Sump1) Sump1,sum(tab.Summ1) Summ1, sum(tab.Sumob1) Sumob1 from(select t2.source, t1.bag, t2.id_card, t2.id_counting, t1.id_sost, t1.id_denom, t1.id_currency, t3.name Card1, cast(t1.nominal as int) Nomin1,t1.name_sost sost1, sum(cast((case when t2.source = 1 then t2.count else 0 end) as bigint)) Kolbp1,sum(cast((case when t2.source = 0 then t2.count else 0 end)as bigint))Kolp1,sum(cast((case when t2.source = 2 then t2.count else 0 end)as bigint)) Kolm1,sum(cast((case when t2.source = 1 then t2.fact_value else 0 end) as decimal)) Sumbp1, sum(cast((case when t2.source = 0 then t2.fact_value else 0 end) as decimal)) Sump1,sum(cast((case when t2.source = 2 then t2.fact_value else 0 end) as decimal)) Summ1, sum(cast(t2.fact_value as decimal)) Sumob1 from( select t1.id id_sost, b1.[name] bag ,t4.id_currency, t4.value nominal, t1.name name_sost, t2.id_counting, t2.declared_value, t2.fact_value, t4.id id_denom from t_g_condition t1 , t_g_counting_content t2 inner join t_g_account t3 on(t2.id_account = t3.id) inner join t_g_denomination t4 on(t3.id_currency = t4.id_currency) left join t_g_counting b2 on t2.id_counting=b2.id left join t_g_bags b1 on t2.id_bag=b1.id " +
                            " where b2.id_multi_bag='" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"] + "' " +
                            //"and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) group by t2.source,t1.bag, t1.valut, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom, t1.id_currency,t3.name,t1.nominal, t1.name_sost) tab where Sumob1 <> 0 group by  tab.id_card, tab.bag, tab.valut, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1, source order by id_denom" 
                            //"and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) group by t2.source,t1.bag, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom, t1.id_currency,t3.name,t1.nominal, t1.name_sost) tab where Sumob1 <> 0 group by  tab.id_card, tab.bag, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1, source order by id_denom"
                            "and t1.visible = 0) t1 inner join t_g_counting_denom t2 on(t1.id_sost= t2.id_condition) and(t1.id_counting = t2.id_counting) and(t1.id_denom = t2.id_denomination) left join t_g_cards t3 on(t2.id_card = t3.id) group by t2.source,t1.bag, t1.valut, t2.id,t2.id_card,t2.id_counting,t1.id_sost,t1.id_denom, t1.id_currency,t3.name,t1.nominal, t1.name_sost) tab where Sumob1 <> 0 group by  tab.id_card, tab.bag, tab.valut, tab.id_counting,tab.id_sost,tab.id_denom,tab.id_currency,tab.Card1,tab.Nomin1,tab.sost1, source order by tab.bag, id_denom " +
                            "");


                        if (detOtchetDataSet != null)
                            if (detOtchetDataSet.Tables[0].Rows.Count > 0)
                            {                                
                                dataGridView9.DataSource = detOtchetDataSet.Tables[0];

                                rblCurrency2.DataSource = (from cur in detOtchetDataSet.Tables[0].AsEnumerable()
                                                          //join count in countContentDataSet1.Tables[0].AsEnumerable() on curr.Field<Int64>("id") equals count.Field<Int64>("id_currency")
                                                          select new
                                                          {
                                                              id = cur.Field<Int64>("id_currency"),
                                                              //name = curr.Field<String>("name"),
                                                              curr_code = cur.Field<String>("Valut1")
                                                          }
                                                                    ).Distinct().ToList();

                            }
                    }

                    rblCondition2.SelectedValue = 0;
                    rblcard2.SelectedValue = 0;
                }
                if (tabControl1.SelectedIndex == 1)
                {

               


                // private DataSet obchper1 = null;
                //private DataSet detper1 = null;
                if(dgCounting.RowCount>0)
                if (dgCounting.CurrentCell.RowIndex > -1)
                {

                    //if (rblcard.Items.Count>0) dataGridView2.RowCount > 0
                    //     rblcard.DataSource = null;
                    // rblcard.Items.Clear();

                    /*
                    rblCurrency.DisplayMember = "curr_code";
                    rblCurrency.ValueMember = "id";

                    rblCondition.DisplayMember = "name";
                    rblCondition.ValueMember = "id";

                    rblcard.DisplayMember = "name";
                    rblcard.ValueMember = "id";

                    rblCurrency.DataSource = null;
                    rblCurrency.Items.Clear();
                    rblCondition.DataSource = null;
                    rblCondition.Items.Clear();
                    rblcard.DataSource = null;
                    rblcard.Items.Clear();
                    */

                        if (dgCounting.CurrentCell.RowIndex <= countingDataSet.Tables[0].Rows.Count)
                        {

                                obchper1 = dataBase.GetData9(
                            //     "select t7.name Card1, t3.curr_code Valut1, t2.declared_value Sumzajv1, t2.fact_value Sumper1, sum(t6.declared_value) Sumzajvn1, sum(t5.fact_value) Sumpern1, sum(t6.denomcount) Colzajv1, sum(t5.count) Colper1,Sum(case when t5.source=0 then t5.count else null end) col2,Sum(case when t5.source=0 then t5.fact_value else null end) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(case when t5.source = 1 then t5.fact_value else null end) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(case when t5.source = 2 then t5.fact_value else null end) col7                           from t_g_counting t1   left join t_g_counting_content t2 on (t1.id = t2.id_counting) left join t_g_currency t3 on (t2.id_currency = t3.id) left join t_g_denomination t4 on (t3.id = t4.id_currency) left join t_g_counting_denom t5  on (t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  left join t_g_declared_denom t6 on(t1.id = t6.id_counting) and(t4.id = t6.id_denomination) and(t6.id_account = t2.id_account) left join t_g_cards t7 on(t1.id = t7.id_counting) and(t5.id_card = t7.id) where t1.id ='" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'  and t7.id is not null group by t7.name, t3.curr_code, t2.declared_value, t2.fact_value"

                            //////04.12.2019
                            //   "select t7.name Card1, t3.curr_code Valut1, t2.declared_value Sumzajv1, t2.fact_value Sumper1, t8.Sumzajvn1, sum(t5.fact_value) Sumpern1, t8.Colzajv1, sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(case when t5.source = 0 then t5.fact_value else null end) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(case when t5.source = 1 then t5.fact_value else null end) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(case when t5.source = 2 then t5.fact_value else null end) col7 from t_g_counting t1   left join t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  left join t_g_cards t7 on(t1.id = t7.id_counting) and(t5.id_card = t7.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1 from t_g_declared_denom t6 where t6.id_counting= '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'  group by t6.id_counting)t8 on(t1.id = t8.id_counting) where t1.id = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'   and t7.id is not null group by t7.name, t3.curr_code, t2.declared_value, t2.fact_value,t8.Sumzajvn1,t8.Colzajv1"

                            /////30.12.2019
                            /*
                            ///////09.12.2019

                            // "select case when t1.fl_prov=3 then 'у. расхождение'  when t1.fl_prov=2 then 'сошлось' when t1.fl_prov=1 then 'расхождение' else 'подготовлено'  end as status1,t7.name Card1, t3.curr_code Valut1, t2.declared_value Sumzajv1, t2.fact_value Sumper1, t8.Sumzajvn1, sum(t5.fact_value) Sumpern1, t8.Colzajv1, sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(case when t5.source = 0 then t5.fact_value else null end) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(case when t5.source = 1 then t5.fact_value else null end) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(case when t5.source = 2 then t5.fact_value else null end) col7 from t_g_counting t1   left join t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  left join t_g_cards t7 on(t1.id = t7.id_counting) and(t5.id_card = t7.id) left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1,t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'  group by t6.id_counting,t6.id_currency)t8 on(t1.id = t8.id_counting) and (t2.id_currency = t8.id_currency) where t1.id = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'   and t7.id is not null group by t7.name, t3.curr_code, t2.declared_value, t2.fact_value,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov"

                            "select" +
                           // " case when t1.fl_prov=3 then 'у. расхождение'  when t1.fl_prov=2 then 'сошлось' when t1.fl_prov=1 then 'расхождение' else 'подготовлено'  end as status1,t7.name Card1," +
                            " t3.curr_code Valut1, t2.declared_value Sumzajv1, t2.fact_value Sumper1, t8.Sumzajvn1, sum(t5.fact_value) Sumpern1, t8.Colzajv1, sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum(case when t5.source = 0 then t5.fact_value else null end) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum(case when t5.source = 1 then t5.fact_value else null end) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum(case when t5.source = 2 then t5.fact_value else null end) col7 from t_g_counting t1   left join t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  " +
                           // "left join t_g_cards t7 on(t1.id = t7.id_counting) and(t5.id_card = t7.id) " +
                            "left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1,t6.id_currency from t_g_declared_denom t6 where t6.id_counting= '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'  group by t6.id_counting,t6.id_currency)t8 on(t1.id = t8.id_counting) and (t2.id_currency = t8.id_currency) where t1.id = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'   " +
                            //"and t7.id is not null " +
                            "group by " +
                           // "t7.name, " +
                            "t3.curr_code, t2.declared_value, t2.fact_value,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov"

                            ///////09.12.2019
                            */

                            "select t2.id_currency id_currency," +
                            // " case when t1.fl_prov=3 then 'у. расхождение'  when t1.fl_prov=2 then 'сошлось' when t1.fl_prov=1 then 'расхождение' else 'подготовлено'  end as status1,t7.name Card1," +
                            " t3.curr_code Valut1,  cast(t2.declared_value as float) Sumzajv1,  cast(t2.fact_value as float) Sumper1,  cast(t8.Sumzajvn1 as float) as Sumzajvn1, sum( cast(t5.fact_value as float)) Sumpern1, t8.Colzajv1, sum(t5.count) Colper1, Sum(case when t5.source = 0 then t5.count else null end) col2,Sum( cast(case when t5.source = 0 then t5.fact_value else null end as float)) col3, Sum(case when t5.source = 1 then t5.count else null end) col4,Sum( cast(case when t5.source = 1 then t5.fact_value else null end as float)) col5,Sum(case when t5.source = 2 then t5.count else null end) col6,Sum( cast(case when t5.source = 2 then t5.fact_value else null end as float)) col7 from t_g_counting t1   left join t_g_counting_content t2 on(t1.id = t2.id_counting) left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination)  " +
                            // "left join t_g_cards t7 on(t1.id = t7.id_counting) and(t5.id_card = t7.id) " +
                            "left join(select t6.id_counting, sum(t6.declared_value) Sumzajvn1, sum(t6.denomcount) Colzajv1,t6.id_currency from t_g_declared_denom t6 " +
                            //"where t6.id_counting= '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] +"'"+ 
                            "  group by t6.id_counting,t6.id_currency)t8 on(t1.id = t8.id_counting) and (t2.id_currency = t8.id_currency) where t1.id = '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'   " +
                            //"and t7.id is not null " +
                            "group by " +
                            // "t7.name, " +
                            "t2.id_currency, t3.curr_code, t2.declared_value, t2.fact_value,t8.Sumzajvn1,t8.Colzajv1,t1.fl_prov"

                            /////30.12.2019

                            //////04.12.2019

                            );


                        dataGridView1.DataSource = obchper1.Tables[0];
                               // MessageBox.Show("id_multi_bag = "+ countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"].ToString());

                        ///////09.12.2019

                        if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "0")
                            label19.Text = "подготовлено";
                        if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "1")
                            label19.Text = "расхождение";
                        if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "2")
                            label19.Text = "сошлось";
                        if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "3")
                            label19.Text = "у. расхождение";

                            
                        ///////09.12.2019

                        ////30.12.2019

                        if (countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["fl_prov"].ToString() == "4")
                            label19.Text = "общая сумма сошлась, по номиналам расхождение";

                            
                            button10.Enabled = false;
                            button13.Enabled = false;
                            button14.Enabled = false;


                            ///////09.12.2019
                            if (label19.Text != "расхождение")
                            {
                                button10.Enabled = false;
                                //button13.Enabled = false;
                                //comboBox10.Enabled = false;
                            }
                            else
                            {
                                if(pm.EnabledPossibility(perm, button10))
                                    button10.Enabled = true;
                                //button13.Enabled = true;
                                //comboBox10.Enabled = true;
                            }
                            
                           if(pm.VisiblePossibility(perm, rblCurrency))
                                rblCurrency.Visible = true;
                           if (pm.VisiblePossibility(perm, rblCurrency))
                                rblCondition.Visible = true;
                                //rblCondition2.Visible = true;
                           if (pm.VisiblePossibility(perm, rblCurrency))
                                rblcard.Visible = true;
                                /////08.01.2020

                                ///22.07.2020
                                if (dataGridView2.RowCount > 0)
                                { 
                                    if(pm.EnabledPossibility(perm, rbl1))
                                        rbl1.Enabled = true;
                                    if (pm.EnabledPossibility(perm, btnShow))
                                        btnShow.Enabled = true;
                                }
                                else
                                {
                                    rbl1.Enabled = false;
                                    btnShow.Enabled = false;
                                }                           
                                ///


                            currencyDataSet1 = dataBase.GetData("t_g_currency");
                        conditionDataSet1 = dataBase.GetData("t_g_condition");
                        
                        cardDataSet1 = dataBase.GetData9("select distinct t1.id,case when t1.fl_obr=1 then 'V' +t1.name else t1.name end name from t_g_cards t1 where t1.id_counting= '" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'");
                        
                        countContentDataSet1 = dataBase.GetData("t_g_counting_content", "id_counting", countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString());

                        rblCurrency.DataSource = (from curr in currencyDataSet1.Tables[0].AsEnumerable()
                                                  join count in countContentDataSet1.Tables[0].AsEnumerable() on curr.Field<Int64>("id") equals count.Field<Int64>("id_currency")
                                                  select new
                                                  {
                                                      id = curr.Field<Int64>("id"),
                                                      //name = curr.Field<String>("name"),
                                                      curr_code = curr.Field<String>("curr_code")
                                                  }
                                                                    ).Distinct().ToList();

                        if (conditionDataSet1.Tables[0].Rows[conditionDataSet1.Tables[0].Rows.Count - 1]["name"].ToString() != "Все")
                        {
                            DataRow conditionAllRow = conditionDataSet1.Tables[0].NewRow();
                            conditionAllRow["id"] = 0;
                            conditionAllRow["name"] = "Все";
                            conditionAllRow["visible"] = false;
                            //conditionAllRow["curr_code"] = "Все";
                            conditionDataSet1.Tables[0].Rows.Add(conditionAllRow);

                        }
                        rblCondition.DataSource = conditionDataSet1.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == false).CopyToDataTable<DataRow>();
                                //   rblCondition.SelectedValue = 0;

                              //  rblCondition2.DataSource = conditionDataSet1.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == false).CopyToDataTable<DataRow>();


                                rblcard.Visible = false;

                        if (cardDataSet1.Tables[0].Rows.Count > 0)
                        {

                            if (cardDataSet1.Tables[0].Rows[cardDataSet1.Tables[0].Rows.Count - 1]["name"].ToString() != "Все")
                            {
                                DataRow conditionAllRow = cardDataSet1.Tables[0].NewRow();
                                conditionAllRow["id"] = 0;
                                conditionAllRow["name"] = "Все";


                                cardDataSet1.Tables[0].Rows.Add(conditionAllRow);

                            }
                            if(pm.VisiblePossibility(perm, rblcard))
                                rblcard.Visible = true;

                            rblcard.DataSource = cardDataSet1.Tables[0];

                            /////09.12.2019
                            /*
                            for (int i33 = 0; i33 < rblcard.Items.Count;i33++)
                            {
                                string f1 = rblcard.Items[i33].ToString();
                               if (f1.Substring(0,1)=="V")
                               {
                                //    rblcard.
                               }

                            }
                            */
                            /////09.12.2019


                            //  rblcard.SelectedValue = 0;
                        }

                        //////

                        filtr1(1);

                        rblCondition.SelectedValue = 0;
                               

                                rblcard.SelectedValue = 0;

                    }


                }

                //  MessageBox.Show("1");
            }

            //////30.12.2019

                dataGridView3.AutoResizeColumns();
                dataGridView4.AutoResizeColumns();
                dataGridView1.AutoResizeColumns();

              //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                ////
                dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

          //  dataGridView3.ColumnHeadersHeight = this.dataGridView3.ColumnHeadersHeight * 3;

            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            dataGridView3.Paint -= new PaintEventHandler(dataGridView3_Paint);



            dataGridView3.Scroll -= new ScrollEventHandler(dataGridView3_Scroll);

            dataGridView3.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView3_ColumnWidthChanged);


            dataGridView3.Paint += new PaintEventHandler(dataGridView3_Paint);



            dataGridView3.Scroll += new ScrollEventHandler(dataGridView3_Scroll);

            dataGridView3.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView3_ColumnWidthChanged);

           

            /////
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

          //  dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 3;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            //  dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

            dataGridView1.Paint -= new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll -= new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);


            dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);



            dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);

            dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView1_ColumnWidthChanged);

                //      dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;



                #region dataGridView10
                dataGridView10.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                

                dataGridView10.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

               

                dataGridView10.Paint -= new PaintEventHandler(dataGridView10_Paint);



                dataGridView10.Scroll -= new ScrollEventHandler(dataGridView10_Scroll);

                dataGridView10.ColumnWidthChanged -= new DataGridViewColumnEventHandler(DataGridView10_ColumnWidthChanged);


                dataGridView10.Paint += new PaintEventHandler(dataGridView10_Paint);



                dataGridView10.Scroll += new ScrollEventHandler(dataGridView10_Scroll);

                dataGridView10.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridView10_ColumnWidthChanged);
                #endregion

            }
            //////31.12.2019
            ///
            if (dgCounting.CurrentCell == null)
            {
                //MessageBox.Show("1111111111111");
                btnAdd.Enabled = false;
                btnModify.Enabled = false;
               // btnDelete.Enabled = false;
            }
            else
            {
                //btnAdd.Enabled = false;
                //btnModify.Enabled = false;
               // btnDelete.Enabled = true;
            }

        }


            private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            detal1();
        }

        private void DgCounting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // detal1();
        }

        private void DgCounting_SelectionChanged(object sender, EventArgs e)
        {
             
             detal1();
            //detal1();

        }

        private void Rblcard_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr1(2); 
        }

        private void RblCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr1(2);
        }

        private void RblCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr1(2);
        }

        private void Button3_Click(object sender, EventArgs e)
        {

           

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            if (button3.Text == "Показать фильтр")
            {
                panel5.Visible = true;
                button3.Text = "Скрыть фильтр";

            }
            else
            {

                panel5.Visible = false;
                button3.Text = "Показать фильтр";

            }
            // panel5.Height = 100;
            //  panel6.Location = new Point(8, 250);
            //  panel6.Height = 336;
            //  dgCounting.Location = new Point(8, 397);
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            if (cbsort.SelectedIndex == 0)
                sort = "fl_prov";
            else if (cbsort.SelectedIndex == 1)
                sort = "name";
            else sort = "creation";

            countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift+" order by "+sort);

            dgCounting.DataSource = countingDataSet.Tables[0];

            flprovDataSet = dataBase.GetData9("select distinct fl_prov as id, iif(fl_prov=0,'Подготовлено',iif(fl_prov=1,'Расхождение',iif(fl_prov=2,'Сошлось','У.расхождение'))) as [name] from t_g_counting order by fl_prov");
            cbFlprov.DataSource = flprovDataSet.Tables[0];
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text.Trim()!="")|(comboBox2.Text.Trim() != "") | (comboBox3.Text.Trim() != "") | (comboBox4.Text.Trim() != "") | (comboBox5.Text.Trim() != "") | (comboBox6.Text.Trim() != "")

                ////05.01.2020

                | (comboBox9.Text.Trim() != "") | (cbFlprov.Text.Trim()!="")

                ////05.01.2020

                )
            {
                string s1 = "";

                if (comboBox1.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_cards t1 where name = '"+ comboBox1.Text.ToString()+ "' and t_g_counting.id = t1.id_counting) ";
                if (comboBox2.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_bags t1 where name = '" + comboBox2.Text.ToString() + "' and t_g_counting.id_bag = t1.id) ";
                if (comboBox3.Text.Trim() != "")
                    s1 = s1 + "and exists(select 1 from t_g_client t1 where name = '" + comboBox3.Text.ToString() + "' and t_g_counting.id_client = t1.id) ";
                if (comboBox5.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_client t1 where BIN = '" + comboBox5.Text.ToString() + "' and t_g_counting.id_client = t1.id)";
                if (comboBox4.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_client t1 inner join t_g_account t2 on (t1.id = t2.id_client) inner join t_g_counting_content t3 on (t_g_counting.id = t3.id_counting) and(t3.id_account = t2.id) where t2.name = '" + comboBox4.Text.ToString() + "' and t_g_counting.id_client = t1.id) ";
                if (comboBox6.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_client t1 inner join t_g_account t2 on (t1.id = t2.id_client) inner join t_g_encashpoint t3 on (t3.id = t2.id_encashpoint)  inner join t_g_counting_content t4 on (t_g_counting.id = t4.id_counting) and(t4.id_account = t2.id) where t3.name = '" + comboBox6.Text.ToString() + "' and t_g_counting.id_client = t1.id)";

                if (cbFlprov.Text.Trim() != "")
                    s1 = s1 + " and fl_prov=" + cbFlprov.SelectedValue.ToString();
                ////05.01.2020
                if (comboBox9.SelectedValue!=null)
                if (comboBox9.Text.Trim() != "")
                    s1 = s1 + " and exists(select 1 from t_g_client t1  where t_g_counting.id_client=t1.id and t1.id_subfml = '" + comboBox9.SelectedValue.ToString() + "' )";



                ////05.01.2020

                if (cbsort.SelectedIndex == 0)
                    sort = "fl_prov";
                else if (cbsort.SelectedIndex == 1)
                    sort = "name";
                else sort = "creation";
                countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where  deleted=0 and id_bag is not null " + s1.ToString()+ " and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);
                dgCounting.DataSource = countingDataSet.Tables[0];

                button6.BackColor = System.Drawing.Color.Yellow;
                if(pm.EnabledPossibility(perm, button6))
                    button6.Enabled = true;
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {


            ////09.12.2019

            //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
            if (cbsort.SelectedIndex == 0)
                sort = "fl_prov";
            else if (cbsort.SelectedIndex == 1)
                sort = "name";
            else sort = "creation";
            countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);

            ////09.12.2019


            dgCounting.DataSource = countingDataSet.Tables[0];

            ////05.01.2020
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox9.Text = "";
            cbFlprov.Text = "";
            button6.Enabled = false;

            button6.BackColor = SystemColors.Control;
            ////05.01.2020

        }

        private void BtnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void BtnModify_Click_1(object sender, EventArgs e)
        {

        }

        private void ComboBox3_TextChanged(object sender, EventArgs e)
        {
            
            //  if (comboBox3.Text.Trim() != "")
            //     MessageBox.Show("1");
        }

        private void ComboBox3_KeyUp(object sender, KeyEventArgs e)
        {

            
            if (comboBox3.Text.Trim() != "")
            {
                string text = comboBox3.Text;

                string selectString = "name like '%" + comboBox3.Text.Trim().ToString() + "%'";
                DataRow[] searchedRows = ((DataTable)clientsDataSet2.Tables[0]).Select(selectString);
                //  selectString = "name like '%" + comboBox3.Text.Trim().ToString() + "%'";
                if (searchedRows.Count() > 0)
                    comboBox3.DataSource = ((DataTable)clientsDataSet2.Tables[0]).Select(selectString).CopyToDataTable();
                else
                    comboBox3.DataSource = clientsDataSet2.Tables[0];

                comboBox3.Text = text;
                comboBox3.SelectionStart = text.Length;

            }
            else
                comboBox3.DataSource = clientsDataSet2.Tables[0];

            
            // if (comboBox3.Text.Trim() != "")
            {
                
               // string selectString = "name like '%" + comboBox3.Text.Trim().ToString() + "%'";
             //   DataRow[] searchedRows = ((DataTable)clientsDataSet2.Tables[0]).Select(selectString);
                //  selectString = "name like '%" + comboBox3.Text.Trim().ToString() + "%'";
              //  comboBox3.DataSource = ((DataTable)clientsDataSet2.Tables[0]).Select(selectString).CopyToDataTable();

                // MessageBox.Show("1");

                /*
           clientsDataSet = clientsDataSet1;

           // cbClient.DataSource = clientsDataSet.Tables[0];
           string text = cbClient.Text;
           // if (!string.IsNullOrEmpty(cbClient.Text) && clientsDataSet.Tables[0].Count(s => s.Contains(cbClient.Text)) > 0)
           //      cbClient.DataSource = clientsDataSet.Tables[0].Where(s => s.Contains(cbClient.Text)).ToList();
           string selectString = "name like '%" + cbClient.Text.Trim().ToString() + "%'";
           DataRow[] searchedRows = ((DataTable)currencyDataSet.Tables[0]).Select(selectString);
           //int i1 = ((DataTable)clientsDataSet.Tables[0]).Select(selectString)).;

           if (searchedRows.Count() > 0)

               cbClient.DataSource = ((DataTable)clientsDataSet.Tables[0]).Select(selectString).CopyToDataTable();

           // string s2 = searchedRows[0].Field<string>("curr_code");
           if (string.IsNullOrEmpty(cbClient.Text))
               cbClient.DataSource = clientsDataSet.Tables[0];
           cbClient.Text = text;
           */


                /*
              //  cbClient.IsDropDownOpen = true;
                // убрать selection, если dropdown только открылся
                var tb = (TextBox)e.OriginalSource;
                tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB.ItemsSource);
                cv.Filter = s =>
                    ((string)s).IndexOf(CB.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
                */

                // if (cbClient.Text.Trim() == "")
                //    PrepareData();
                // MessageBox.Show("1");
                /*
                if (cbClient.Text.Trim() != "")
                {
                    string selectString = "name like '" + cbClient.Text.Trim().ToString() + "%'";
                    clientsDataSet.Tables[0].Select(selectString);

                }
                */
                /*
                string selectString =
          "BIN = '" + tbID.Text.Trim().ToString() + "'";
                DataRow[] searchedRows =
        ((DataTable)dgList.DataSource).
            Select(selectString);

                int result1 = searchedRows.Length;
                if (result1 > 0)
                {
                    MessageBox.Show("Уже есть такой БИН!");
                    return;
                }
                */

            }
        }



        ///11.10.2019


        /////05.12.2019

        private void DataGridView3_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView3.DisplayRectangle;

            rtHeader.Height = this.dataGridView3.ColumnHeadersHeight / 2;

            this.dataGridView3.Invalidate(rtHeader);

        }

        void dataGridView3_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView3.DisplayRectangle;

            rtHeader.Height = this.dataGridView3.ColumnHeadersHeight / 2;

            this.dataGridView3.Invalidate(rtHeader);


        }



        void dataGridView3_Paint(object sender, PaintEventArgs e)

        {
            for (int j = 0; j < 8;
                j++
                )
            {
               
                if (j >= 2)
                {
                    Rectangle r1 = this.dataGridView3.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView3.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;

                    
                        r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView3.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    if (j == 2)
                        s1 = "Заявлено";
                    if (j == 4)
                        s1 = "Подсчитано";
                    if (j == 6)
                        s1 = "Расхождение";
                   

                    e.Graphics.DrawString(s1,

                        this.dataGridView3.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);
                    
                        j += 1;


                }
                


            }

          
           
        }

        #region datagridView7


        private void DataGridView7_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView7.DisplayRectangle;

            rtHeader.Height = this.dataGridView7.ColumnHeadersHeight / 2;

            this.dataGridView7.Invalidate(rtHeader);

        }

        void dataGridView7_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView7.DisplayRectangle;

            rtHeader.Height = this.dataGridView7.ColumnHeadersHeight / 2;

            this.dataGridView7.Invalidate(rtHeader);


        }



        void dataGridView7_Paint(object sender, PaintEventArgs e)

        {
            for (int j = 0; j < 9;
                j++
                )
            {

                if (j >= 3)
                {
                    Rectangle r1 = this.dataGridView7.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView7.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;


                    r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView7.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    if (j == 3)
                        s1 = "Заявлено";
                    if (j == 5)
                        s1 = "Подсчитано";
                    if (j == 7)
                        s1 = "Расхождение";


                    e.Graphics.DrawString(s1,

                        this.dataGridView7.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView7.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);

                    j += 1;


                }



            }



        }
        #endregion

        /////05.12.2019


        ////13.11.2019

        private void DataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;

            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;

            this.dataGridView1.Invalidate(rtHeader);

        }

        private void DataGridView10_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView10.DisplayRectangle;

            rtHeader.Height = this.dataGridView10.ColumnHeadersHeight / 2;

            this.dataGridView10.Invalidate(rtHeader);

        }

        void dataGridView1_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;

            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;

            this.dataGridView1.Invalidate(rtHeader);


        }
        void dataGridView10_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView10.DisplayRectangle;

            rtHeader.Height = this.dataGridView10.ColumnHeadersHeight / 2;

            this.dataGridView10.Invalidate(rtHeader);


        }



        void dataGridView1_Paint(object sender, PaintEventArgs e)

        {
           
            for (int j = 0; j < 11;
                  j++
                )
            {
                

                if (j >= 0)                
                {
                    Rectangle r1 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView1.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;

                    if (j == 0)
                        // if (j == 2)
                        r1.Width = r1.Width + 4*w2 - 2;
                    else
                        r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    
                    if (j == 0)
                        s1 = "Заявлено";
                    
                    if (j == 4)
                        s1 = "Общее по пересчёту";
                    
                    if (j == 6)
                        s1 = "По номиналу с пересчётом";
                    
                    if (j == 8)
                        
                        s1 = "По номиналу без пересчёта";
                    
                    if (j == 10)
                        s1 = "По номиналу с машины";

                    e.Graphics.DrawString(s1,

                        this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);
                    
                    if (j == 0)
                        j += 2;
                   
                        j += 1;
                    

                }
               


            }

        }

        #region datagridview10
        void dataGridView10_Paint(object sender, PaintEventArgs e)

        {
            
            for (int j = 1; j < 12;
                  j++
                )
            {
                

                if (j >= 1)
                
                {
                    Rectangle r1 = this.dataGridView10.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView10.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;

                    if (j == 1)
                        
                        r1.Width = r1.Width + 4 * w2 - 2;
                    else
                        r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView10.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    
                    if (j == 1)
                        s1 = "Заявлено";
                    
                    if (j == 5)
                        s1 = "Общее по пересчёту";
                    
                    if (j == 7)
                        s1 = "По номиналу с пересчётом";
                    
                    if (j == 9)
                        
                        s1 = "По номиналу без пересчёта";
                    
                    if (j == 11)
                        s1 = "По номиналу с машины";

                    e.Graphics.DrawString(s1,

                        this.dataGridView10.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView10.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);
                    
                    if (j == 1)
                        j += 2;
                    
                    j += 1;


                }
            }
        }
        #endregion


        ////13.11.2019


        ////14.11.2019

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
            for (int j = 0; j < 10;
                j++
                )
            {
               
                if (j >= 4)
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

                    if (j == 4)
                        s1 = "Без пересчёта";
                    if (j == 6)
                        s1 = "С пересчётом";
                    if (j == 8)
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

        #region dataGridView9

        private void DataGridView9_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dataGridView9.DisplayRectangle;

            rtHeader.Height = this.dataGridView9.ColumnHeadersHeight / 2;

            this.dataGridView9.Invalidate(rtHeader);

        }

        void dataGridView9_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dataGridView9.DisplayRectangle;

            rtHeader.Height = this.dataGridView9.ColumnHeadersHeight / 2;

            this.dataGridView9.Invalidate(rtHeader);


        }



        void dataGridView9_Paint(object sender, PaintEventArgs e)

        {
            for (int j = 5; j < 11;
                j++
                )
            {

                if (j >= 5)
                {
                    Rectangle r1 = this.dataGridView9.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dataGridView9.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;


                    r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dataGridView9.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    if (j == 5)
                        s1 = "Без пересчёта";
                    if (j == 7)
                        s1 = "С пересчётом";
                    if (j == 9)
                        s1 = "С машины";

                    e.Graphics.DrawString(s1,

                        this.dataGridView9.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dataGridView9.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);

                    j += 1;


                }



            }


        }

        #endregion dataGridView9

        //////15.11.2019
        private void Button7_Click(object sender, EventArgs e)
        {
           

            int id_EncashPoint = Convert.ToInt32(cbEncashPoint.SelectedValue);
            if (id_EncashPoint == 0) 
            {
                MessageBox.Show("Выберите точку инкассации!");
            }
            else
            {
                PlombForm1 plombForm1 = new PlombForm1(id_EncashPoint, 2);
                DialogResult dialogResult = plombForm1.ShowDialog();
            }
            
        }

        /// 20.11.2019
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            DataSet denominationDataSet = null; 

            if (textBox1.Text.ToString().Trim() != "")
            {
                string s1 = textBox1.Text.ToString().Trim();

                if ((s1.IndexOf("<TransactionFile") > -1) & (s1.IndexOf("</TransactionFile>") > -1))
                {
                    PrepareData();
                    Stream cardStream;
                    cardStream = new MemoryStream(Encoding.Unicode.GetBytes(s1));
                    DataSet dataSetXML = new DataSet();
                    dataSetXML.ReadXml(cardStream);
                    cardStream.Close();

                    string s2 = dataSetXML.Tables["bin"].Rows[0]["p1"].ToString().Trim();
                    string s3 = dataSetXML.Tables["TransactionFile"].Rows[0]["Created"].ToString().Trim();
                    string s4 = dataSetXML.Tables["Nomermesh"].Rows[0]["p1"].ToString().Trim();
                    string s5 = dataSetXML.Tables["Nomerplom"].Rows[0]["p1"].ToString().Trim();
                    string s6 = dataSetXML.Tables["Otprav"].Rows[0]["p1"].ToString().Trim();

                    string s7 = dataSetXML.Tables["kbe"].Rows[0]["p1"].ToString().Trim();
                    string s8 = dataSetXML.Tables["Poluchat"].Rows[0]["p1"].ToString().Trim();
                    string s9 = dataSetXML.Tables["Kontrol"].Rows[0]["p1"].ToString().Trim();
                    string s10 = dataSetXML.Tables["Kassir"].Rows[0]["p1"].ToString().Trim();
                    string s11 = dataSetXML.Tables["Vidoper"].Rows[0]["p1"].ToString().Trim();
                    string s12 = dataSetXML.Tables["Knp"].Rows[0]["p1"].ToString().Trim();
                    string s13 = dataSetXML.Tables["Numgrbank"].Rows[0]["p1"].ToString().Trim();

                                        
                    string[] byte1=null;
                    string f1 = "";
                    byte1 = s6.Split(',');
                    for (int i = 0; i < byte1.Count(); i++)

                    {
                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                    }
                    s6 = f1;

                    f1 = "";
                    byte1 = s8.Split(',');
                    for (int i = 0; i < byte1.Count(); i++)

                    {
                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                    }
                    s8 = f1;

                    f1 = "";
                    byte1 = s9.Split(',');
                    for (int i = 0; i < byte1.Count(); i++)

                    {
                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                    }
                    s9 = f1;

                    f1 = "";
                    byte1 = s10.Split(',');
                    for (int i = 0; i < byte1.Count(); i++)

                    {
                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                    }
                    s10 = f1;

                    f1 = "";
                    byte1 = s11.Split(',');
                    for (int i = 0; i < byte1.Count(); i++)

                    {
                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                    }
                    s11 = f1;

                    qr_bin = s2;
                    qr_data=s3;
                    qr_nummech= s4;
                    qr_numplom= s5;
                    qr_otprav= s6;
                    qr_kbe= s7;
                    qr_poluch= s8;
                    qr_kontr= s9;
                    qr_kass= s10;
                    qr_vidoper= s11;
                    qr_knp= s12;
                    qr_numgr= s13;


                    if (s2.ToString() != "")
                    {
                        DataSet d1 = dataBase.GetData9("select * from t_g_client where bin='"+s2.ToString()+"'");
                        if (d1 != null && d1.Tables[0] != null && d1.Tables[0].Rows.Count > 0)
                        {
                            cbID.Text = s2.ToString();
                            tbNumBag.Text = s4.ToString();
                            textBox2.Text = s5.ToString();
                            countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");

                            if (cbID.Text != "")
                            {
                                clientsDataSet = dataBase.GetData("t_g_client");
                                if (clientsDataSet != null && clientsDataSet.Tables[0].Rows.Count > 0)
                                {
                                    cbID.SelectedValue = clientsDataSet.Tables[0].AsEnumerable().Where(
                                        x => x.Field<string>("BIN").Trim() == s2.ToString().Trim()
                                        ).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
                                }
                            }

                            if (cbID.SelectedValue != null)
                            {

                                accountDataSet = dataBase.GetData9("SELECT t1.*   FROM [CountingDB].[dbo].[t_g_account] t1  left join t_g_currency t2 on t1.id_currency=t2.id  where id_client=" + cbID.SelectedValue.ToString() + "  order by t2.sort ");

                                dgAccountDeclared.DataSource = accountDataSet.Tables[0];

                                foreach (DataRow h1 in dataSetXML.Tables["shet"].Rows)
                                {

                                    if (h1["p1"].ToString().Trim() != "")
                                    {


                                        //Запись счетов 
                                        foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
                                        {

                                            if (accountRow.Cells["name"].Value.ToString() == h1["p1"].ToString())
                                            {
                                                DataRow[] h2 = ((DataTable)dataSetXML.Tables["Danvalut"]).Select("shet_Id='" + h1["shet_Id"].ToString() + "'");
                                                // string s2= searchedRows[0].Field<string>("curr_code");

                                                if (h2.Count() > 0)
                                                {

                                                    string s14 = h2[0]["summa1"].ToString();

                                                    string s16 = h2[0]["valut"].ToString();

                                                    ///21.11.2019
                                                    qr_poslsh = h1["p1"].ToString().Trim();
                                                    qr_poslsum = s14;
                                                    qr_poslval = s16;
                                                    ///21.11.2019

                                                    accountRow.Cells["value"].Value = s14.ToString();
                                                    accountRow.Cells["state"].Value = true;

                                                    denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", accountRow.Cells["id_currency"].Value.ToString());
                                                    if (dataSetXML != null && dataSetXML.Tables != null && dataSetXML.Tables["DanNomin"] != null && dataSetXML.Tables["DanNomin"].Rows.Count > 0)
                                                        foreach (DataRow h3 in ((DataTable)dataSetXML.Tables["DanNomin"]).Select("shet_Id='" + h1["shet_Id"].ToString() + "'"))
                                                        {

                                                            string s17 = h3["Kol"].ToString();
                                                            string s18 = h3["Nomin"].ToString();



                                                            /////////
                                                            //CountDenomDataSet = dataBase.GetSchema("t_g_declared_denom");
                                                            // countDenomDataSet = denominationForm.CountDenomDataSet;




                                                            DataRow[] h4 = ((DataTable)denominationDataSet.Tables[0]).Select("value='" + h3["Nomin"].ToString() + "'");
                                                            // string s2= searchedRows[0].Field<string>("curr_code");

                                                            if (h4.Count() > 0)
                                                            {
                                                                //  MessageBox.Show(h4[0]["id"].ToString());

                                                                //////

                                                                DataRow cntDenomDataRow = countDenomDataSet.Tables[0].NewRow();
                                                                cntDenomDataRow["id_denomination"] = h4[0]["id"].ToString();
                                                                cntDenomDataRow["denomcount"] = h3["Kol"].ToString();
                                                                cntDenomDataRow["id_account"] = accountRow.Cells["id"].Value.ToString();
                                                                cntDenomDataRow["creation"] = DateTime.Now;
                                                                cntDenomDataRow["lastupdate"] = DateTime.Now;
                                                                cntDenomDataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                                                                cntDenomDataRow["id_currency"] = accountRow.Cells["id_currency"].Value.ToString();
                                                                cntDenomDataRow["declared_value"] = Convert.ToInt64(s17) * Convert.ToInt64(s18);
                                                                countDenomDataSet.Tables[0].Rows.Add(cntDenomDataRow);

                                                                /////


                                                            }
                                                            ////////
                                                            //accountDataSet.Tables[0];
                                                            //     if (countDenomDataSet.Tables[0].Rows.Count > 0)
                                                            //    {
                                                            //         countDenomDataSet.Tables[0].TableName = "t_g_declared_denom";
                                                            //         foreach (DataRow cntDenomRow in countDenomDataSet.Tables["t_g_declared_denom"].Rows)
                                                            {

                                                                //          if (accountRow.Cells["id"].Value.ToString() == cntDenomRow["id_account"].ToString())
                                                                //         {
                                                                /*
                                                                zap1 = "INSERT INTO[dbo].[t_g_declared_denom]([id_bag],[id_denomination],[denomcount],[id_account],[creation],[lastupdate],[last_user_update],[id_currency],[declared_value],[id_counting]) VALUES('{0}','" + cntDenomRow["id_denomination"].ToString() + "','" + cntDenomRow["denomcount"].ToString() + "','" + cntDenomRow["id_account"].ToString() + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + cntDenomRow["id_currency"].ToString() + "','" + cntDenomRow["declared_value"].ToString().Replace(",", ".") + "','{1}')";
                                                                zapros1.Add(zap1);
                                                                */

                                                                //      }

                                                                //  }


                                                            }

                                                            ////////

                                                            /////////


                                                            //  MessageBox.Show(s17.ToString());
                                                            //  MessageBox.Show(s18.ToString());
                                                        }

                                                }

                                            }

                                        }


                                        // MessageBox.Show(h1["shet_Id"].ToString());




                                        //MessageBox.Show(h2[0]["summa1"].ToString());





                                    }
                                }

                            }

                        }
                        else
                        {
                            MessageBox.Show("Данный клиент отсутствует в базе данных клиентов");
                            MessageBox.Show("Данный клиент отсутствует в базе данных клиентов");
                            textBox1.Text = "";
                        }
                    }

                }
                else
                {
                  //  MessageBox.Show("Текст  QR кода неправильного формата!");
                    // return;
                }

            }
            /*
            aTimer = new System.Timers.Timer(100);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            aTimer.Enabled = true;
            */
        }
        /// 20.11.2019

        ///////26.11.2019
        // private void OnTimedEvent(Object source, ElapsedEventArgs e)
        private void obrabfile1(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            fileobrabst1 = "";

          string  defaultPath = ConfigurationManager.AppSettings["obrabstfilePath"];
          string readyPath  = ConfigurationManager.AppSettings["obrabstfilereadyPath"];
          string errorPath = ConfigurationManager.AppSettings["obrabstfileerrorPath"];

           /////
            if (!Directory.Exists(defaultPath))
            {
                //Создать такую директорию
                Directory.CreateDirectory(defaultPath);
            }

            if (!Directory.Exists(errorPath))
            {
                //Создать такую директорию
                Directory.CreateDirectory(errorPath);
            }

            if (!Directory.Exists(readyPath))
            {
                //Создать такую директорию
                Directory.CreateDirectory(readyPath);
            }
            /////

            if (tbNumBag.ToString().Trim()!="")
            {

                string[] files = Directory.GetFiles(defaultPath);
                foreach (string s in files)
                {
                  

                    using (FileStream fstream = File.OpenRead(s))
                    {
                        // преобразуем строку в байты
                        byte[] array = new byte[fstream.Length];
                        // считываем данные
                        fstream.Read(array, 0, array.Length);
                        // декодируем байты в строку
                        string textFromFile = System.Text.Encoding.Default.GetString(array);
                       

                        if (textFromFile.ToString().Trim() == "")
                        {
                            //Если данный файл уже существует
                            if (File.Exists(errorPath + "\\" + System.IO.Path.GetFileName(s)))
                            {
                                //Удалить файл
                                File.Delete(errorPath + "\\" + System.IO.Path.GetFileName(s));
                            }
                            fstream.Close();
                            //Переместить туда файл
                            File.Move(s, errorPath + "\\"+System.IO.Path.GetFileName(s));
                        }
                        else

                        ////проверка на нашу структуру
                        if ((textFromFile.IndexOf("<TransactionFile") > -1) & (textFromFile.IndexOf("</TransactionFile>") > -1))
                        {

                           

                            DataSet denominationDataSet = null;

                            
                                string s1 = textFromFile.ToString().Trim();

                                
                                    
                                    Stream cardStream;
                                    cardStream = new MemoryStream(Encoding.Unicode.GetBytes(s1));
                                    DataSet dataSetXML = new DataSet();
                                    dataSetXML.ReadXml(cardStream);
                                    cardStream.Close();

                                    string s2 = dataSetXML.Tables["bin"].Rows[0]["p1"].ToString().Trim();
                                    string s3 = dataSetXML.Tables["TransactionFile"].Rows[0]["Created"].ToString().Trim();
                                    string s4 = dataSetXML.Tables["Nomermesh"].Rows[0]["p1"].ToString().Trim();
                                    string s5 = dataSetXML.Tables["Nomerplom"].Rows[0]["p1"].ToString().Trim();
                                    string s6 = dataSetXML.Tables["Otprav"].Rows[0]["p1"].ToString().Trim();


                                    if (s4.ToString().Trim()== tbNumBag.Text.ToString().Trim())
                            {
                                fileobrabst1 = s;
                                PrepareData();

                                string s7 = dataSetXML.Tables["kbe"].Rows[0]["p1"].ToString().Trim();
                                    string s8 = dataSetXML.Tables["Poluchat"].Rows[0]["p1"].ToString().Trim();
                                    string s9 = dataSetXML.Tables["Kontrol"].Rows[0]["p1"].ToString().Trim();
                                    string s10 = dataSetXML.Tables["Kassir"].Rows[0]["p1"].ToString().Trim();
                                    string s11 = dataSetXML.Tables["Vidoper"].Rows[0]["p1"].ToString().Trim();
                                    string s12 = dataSetXML.Tables["Knp"].Rows[0]["p1"].ToString().Trim();
                                    string s13 = dataSetXML.Tables["Numgrbank"].Rows[0]["p1"].ToString().Trim();



                                    


                                    string[] byte1 = null;
                                    string f1 = "";
                                    byte1 = s6.Split(',');
                                    for (int i = 0; i < byte1.Count(); i++)

                                    {
                                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                                    }
                                    s6 = f1;

                                    f1 = "";
                                    byte1 = s8.Split(',');
                                    for (int i = 0; i < byte1.Count(); i++)

                                    {
                                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                                    }
                                    s8 = f1;

                                    f1 = "";
                                    byte1 = s9.Split(',');
                                    for (int i = 0; i < byte1.Count(); i++)

                                    {
                                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                                    }
                                    s9 = f1;

                                    f1 = "";
                                    byte1 = s10.Split(',');
                                    for (int i = 0; i < byte1.Count(); i++)

                                    {
                                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                                    }
                                    s10 = f1;

                                    f1 = "";
                                    byte1 = s11.Split(',');
                                    for (int i = 0; i < byte1.Count(); i++)

                                    {
                                        f1 = f1 + Convert.ToChar(int.Parse(byte1[i])).ToString();
                                    }
                                    s11 = f1;

                                    qr_bin = s2;
                                    qr_data = s3;
                                    qr_nummech = s4;
                                    qr_numplom = s5;
                                    qr_otprav = s6;
                                    qr_kbe = s7;
                                    qr_poluch = s8;
                                    qr_kontr = s9;
                                    qr_kass = s10;
                                    qr_vidoper = s11;
                                    qr_knp = s12;
                                    qr_numgr = s13;




                                if (s2.ToString() != "")
                                {

                                    cbID.Text = s2.ToString();
                                    tbNumBag.Text = s4.ToString();
                                    textBox2.Text = s5.ToString();
                                    countDenomDataSet = dataBase.GetSchema("t_g_declared_denom");

                                    if (cbID.SelectedValue != null)
                                    {
                                        //accountDataSet = dataBase.GetData("t_g_account", "id_client", cbID.SelectedValue.ToString());
                                        accountDataSet = dataBase.GetData9("SELECT t1.*   FROM [CountingDB].[dbo].[t_g_account] t1  left join t_g_currency t2 on t1.id_currency=t2.id  where id_client=" + cbID.SelectedValue.ToString() + "  order by t2.sort ");

                                        dgAccountDeclared.DataSource = accountDataSet.Tables[0];



                                        foreach (DataRow h1 in dataSetXML.Tables["shet"].Rows)
                                        {

                                            if (h1["p1"].ToString().Trim() != "")
                                            {


                                                //Запись счетов 
                                                foreach (DataGridViewRow accountRow in dgAccountDeclared.Rows)
                                                {

                                                    if (accountRow.Cells["name"].Value.ToString() == h1["p1"].ToString())
                                                    {
                                                        DataRow[] h2 = ((DataTable)dataSetXML.Tables["Danvalut"]).Select("shet_Id='" + h1["shet_Id"].ToString() + "'");


                                                        if (h2.Count() > 0)
                                                        {

                                                            string s14 = h2[0]["summa1"].ToString();

                                                            string s16 = h2[0]["valut"].ToString();


                                                            qr_poslsh = h1["p1"].ToString().Trim();
                                                            qr_poslsum = s14;
                                                            qr_poslval = s16;


                                                            accountRow.Cells["value"].Value = s14.ToString();
                                                            accountRow.Cells["state"].Value = true;

                                                            denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", accountRow.Cells["id_currency"].Value.ToString());

                                                            foreach (DataRow h3 in ((DataTable)dataSetXML.Tables["DanNomin"]).Select("shet_Id='" + h1["shet_Id"].ToString() + "'"))
                                                            {

                                                                string s17 = h3["Kol"].ToString();
                                                                string s18 = h3["Nomin"].ToString();








                                                                DataRow[] h4 = ((DataTable)denominationDataSet.Tables[0]).Select("value='" + h3["Nomin"].ToString() + "'");


                                                                if (h4.Count() > 0)
                                                                {


                                                                    DataRow cntDenomDataRow = countDenomDataSet.Tables[0].NewRow();
                                                                    cntDenomDataRow["id_denomination"] = h4[0]["id"].ToString();
                                                                    cntDenomDataRow["denomcount"] = h3["Kol"].ToString();
                                                                    cntDenomDataRow["id_account"] = accountRow.Cells["id"].Value.ToString();
                                                                    cntDenomDataRow["creation"] = DateTime.Now;
                                                                    cntDenomDataRow["lastupdate"] = DateTime.Now;
                                                                    cntDenomDataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                                                                    cntDenomDataRow["id_currency"] = accountRow.Cells["id_currency"].Value.ToString();
                                                                    cntDenomDataRow["declared_value"] = Convert.ToInt64(s17) * Convert.ToInt64(s18);
                                                                    countDenomDataSet.Tables[0].Rows.Add(cntDenomDataRow);




                                                                }




                                                            }


                                                        }

                                                    }

                                                }










                                            }
                                        }

                                    }



                                }
                                    

                                
                               

                            }

                            ////

                        }
                        else
                        {
                            //Если данный файл уже существует
                            if (File.Exists(errorPath + "\\" + System.IO.Path.GetFileName(s)))
                            {
                                //Удалить файл
                                File.Delete(errorPath + "\\" + System.IO.Path.GetFileName(s));
                            }
                            fstream.Close();
                            //Переместить туда файл
                            File.Move(s, errorPath + "\\" + System.IO.Path.GetFileName(s));

                        }


                        }



                }

            }


        }
        ///////26.11.2019

        private void Button8_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToChar(int.Parse("51")).ToString());
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            FrmMarschrut frmmarsh1 = new FrmMarschrut();
          //  frmmarsh1.id_marsh = id_marsh.ToString().Trim();
          //  frmmarsh1.num_marsh = num_marsh.ToString().Trim();
            DialogResult dialogResult = frmmarsh1.ShowDialog();

            dsmarsh = dataBase.GetData9("SELECT  id ,nummarsh ,inkassator,(select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kontrol ) id_kontrol, (select top 1 t2.user_name from t_g_user t2 where t2.id=t1.id_kassir ) id_kassir,kol_porsum,num_porsum,komment  FROM t_g_marschrut t1 where id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);

           
            comboBox7.DataSource = dsmarsh.Tables[0];
            if (FrmMarschrut.id_marshrut != 0)
                comboBox7.SelectedValue = FrmMarschrut.id_marshrut;
            //  if (dialogResult == DialogResult.OK)
            //  {

            //     id_marsh = frmmarsh1.id_marsh.ToString();
            //     num_marsh = frmmarsh1.num_marsh.ToString();

            //  if (num_marsh.ToString().Trim()!="")
            //      label17.Text= "Выбран маршрут: " + num_marsh.ToString();

            //countDenomDataSet.Clear();
            // countDenomDataSet = denominationForm.CountDenomDataSet;

            //  }

            /*
            //сountDenomDataSet = dataBase.GetData("t_g_count_denom");
            DenominationParent denominationForm = new DenominationParent("t_g_counting_denomination", senderGrid.Rows[e.RowIndex].Cells["id_currency"].Value.ToString(),


              ///////30.10.2019
              //  cbAccount.SelectedValue.ToString()
              senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()
                ///////30.10.2019


                , countDenomDataSet);
            DialogResult dialogResult = denominationForm.ShowDialog();
            //CountDenomDataSet cntDenomDataSet = new CountDenomDataSet();
            if (dialogResult == DialogResult.OK)
            {
                //countDenomDataSet.Clear();
                countDenomDataSet = denominationForm.CountDenomDataSet;
                */
        }

        private void Button8_Click_2(object sender, EventArgs e)
        {
             

         }

        private void Button10_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("1 step");

            if (dataGridView3.Rows.Count > 0)
            {
               // MessageBox.Show("2 step");
                /////10.12.2019
                // if (dataGridView1[0, 0].Value.ToString() == "расхождение")
                if (label21.Text == "расхождение")

                /////10.12.2019
                {
                   // MessageBox.Show("3 step");

                    //////08.01.2020
                    double idmax = 0;

                    idmax = Convert.ToDouble(dgCounting["id", dgCounting.CurrentCell.RowIndex].Value);
                    // int ivudstr = dgCounting.CurrentCell.RowIndex;
                    //  dgCounting.Cells["id"].Value
                    //////08.01.2020

                    string s3 = "update t_g_counting set fl_prov=3, validate_date= getdate(), validate_user = " + DataExchange.CurrentUser.CurrentUserId + ", lastupdate=getdate(), last_user_update=" + DataExchange.CurrentUser.CurrentUserId+" where id ='" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"] + "'";

                    int i3 = dataBase.Zapros1(s3, "");
                    MessageBox.Show("Расхождение утверждено");
                    button18.Enabled = false;
                    ////09.12.2019
                    //countingDataSet = dataBase.GetData("t_g_counting", "deleted", "0");
                    if (cbsort.SelectedIndex == 0)
                        sort = "fl_prov";
                    else if (cbsort.SelectedIndex == 1)
                        sort = "name";
                    else sort = "creation";
                    countingDataSet = dataBase.GetData10("select iif(fl_prov=1,'расхождение',iif(fl_prov=0,'подготовлено',iif(fl_prov=2,'сошлось','у.расхождение'))) fl_prov_name, * from t_g_counting where deleted='0' and id_bag is not null and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift + " order by "+sort);
                    dgCounting.DataSource = countingDataSet.Tables[0];
                    ////09.12.2019

                   // detal1();

                    //////08.01.2020

                    //dgCounting.CurrentCell = dgCounting["name", ivudstr];


                    if (button6.Enabled == true)
                        Button5_Click(sender, e);



                    // double idmax = 0;
                    int numstrmax = 0;
                    int ich1 = 0;

                    foreach (DataGridViewRow CountRow1 in dgCounting.Rows)
                    {

                        if (Convert.ToDouble(CountRow1.Cells["id"].Value) == idmax)
                        {
                            idmax = Convert.ToDouble(CountRow1.Cells["id"].Value);
                            numstrmax = ich1;
                            break;
                        }
                        ich1 = ich1 + 1;


                    }
                    if (dgCounting.Rows.Count > 0)
                    {
                        //MessageBox.Show("1=" + dgCounting.CurrentCell.ToString());
                        dgCounting.CurrentCell = dgCounting["name", numstrmax];
                        //MessageBox.Show("2=" + dgCounting.CurrentCell.ToString());
                    }
                    //detal1();


                    //////08.01.2020

                }
                //else MessageBox.Show("4 step!");

            }
            //else MessageBox.Show("Для начало просмотрите детали, затем можете подтвердить");

                    

         //  countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]


        }

        /////31.12.2019
        
        private void prov_add()
        {

            ///31.12.2019
            if (counting_vub == -1)
            {
            ///31.12.2019
               
                ////
                int DeclaredCount = 0;

                if (cbClient.SelectedIndex == -1)
                {
                    // MessageBox.Show("Выберите клиента");
                    return;
                }

                foreach (DataGridViewRow declaredRow in dgAccountDeclared.Rows)
                {

                    
                    if (declaredRow.Cells["value"].Value != null && declaredRow.Cells["value"].Value != DBNull.Value && declaredRow.Cells["value"].Value.ToString() != String.Empty && Convert.ToDecimal(declaredRow.Cells["value"].Value) != 0)
                    {
                        DeclaredCount++;
                    }
                }

                if (DeclaredCount == 0)
                {
                    // MessageBox.Show("Введите задекларированную сумму");
                    return;
                }

                //if (dgCards.Rows.Count == 0)
                //{
                //    // MessageBox.Show("Добавьте карточки");
                //    return;
                //}

                if (tbNumBag.Text == String.Empty)
                {
                    // MessageBox.Show("Введите номер сумки");
                    return;
                }

                if(pm.EnabledPossibility(perm, btnAdd))
                    btnAdd.Enabled = true;

            ///31.12.2019
            }
            ///31.12.2019

        }

        private void PrepCountingForm_KeyDown(object sender, KeyEventArgs e)
        {

            
            //keyShift = e.Shift;
            ///*
            //if ((keyShift & e.KeyCode) == Keys.D8)
            //{
            //    MessageBox.Show("Нажата звёздочка!");
            //}
            //*/

            //if (
            // //   ((e.KeyCode == (Keys.ShiftKey & Keys.D8))) | 
            // ((keyShift==true)&(e.KeyCode ==  Keys.D8)) |
            //    (e.KeyCode == Keys.Multiply))
            //{
            //    //  MessageBox.Show("Нажата звёздочка!");

            //    if (counting_vub > -1)
            //    {
            //        btnModify_Click(sender, e);
            //    }
            //    else
            //    {
            //        btnAdd_Click(sender, e);
            //    }

            //}
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                cbID.Focus();

            /////05.01.2020

        }

        private void cbEncashPoint_KeyUp(object sender, KeyEventArgs e)
        {
            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                dtDeposit.Focus();

            /////05.01.2020
        }

        private void cbAccount_KeyUp(object sender, KeyEventArgs e)
        {
            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                cbEncashPoint.Focus();

            /////05.01.2020
        }

        private void dtDeposit_KeyUp(object sender, KeyEventArgs e)
        {
            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                comboBox7.Focus();

            /////05.01.2020
        }

        private void comboBox7_KeyUp(object sender, KeyEventArgs e)
        {
            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                comboBox8.Focus();

            /////05.01.2020
        }

        private void comboBox8_KeyUp(object sender, KeyEventArgs e)
        {

            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                dgAccountDeclared.Focus();

            /////05.01.2020

        }

        private void cbID_KeyUp(object sender, KeyEventArgs e)
        {
            /////05.01.2020

            if (e.KeyCode == Keys.Enter)
                cbClient.Focus();

            /////05.01.2020
        }

        private void dgAccountDeclared_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgAccountDeclared.RowCount > 0)
                {
/*

                //    if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex != dgAccountDeclared.RowCount - 2))
                //    {
               //         dgAccountDeclared.CurrentCell = dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex];
               //     }
               //     else 
                    
                    if ((dgAccountDeclared.CurrentCell.ColumnIndex == 4) & (dgAccountDeclared.CurrentCell.RowIndex == dgAccountDeclared.RowCount - 1))
                    {
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex];
                    }
                    else if (dgAccountDeclared.CurrentCell.ColumnIndex == 4)
                    {
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex - 1];
                    }
                    //dgAccountDeclared.CurrentCell = dgAccountDeclared["value", 0];


                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex == dgAccountDeclared.RowCount - 1))
                    {
                        tbCard.Focus();
                    }
                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex != dgAccountDeclared.RowCount - 1))
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex];
                */
                
                }
            }
        }

        private void dgAccountDeclared_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgAccountDeclared.RowCount > 0)
                {

                    if ((dgAccountDeclared.CurrentCell.ColumnIndex == 4) )
                    {
                         e.Handled = true;
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex];
                    }
                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex == dgAccountDeclared.RowCount - 1))
                    {
                        tbCard.Focus();
                    }
                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex != dgAccountDeclared.RowCount - 1))
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex];
                    /*

                                    //    if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex != dgAccountDeclared.RowCount - 2))
                                    //    {
                                   //         dgAccountDeclared.CurrentCell = dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex];
                                   //     }
                                   //     else 

                                        if ((dgAccountDeclared.CurrentCell.ColumnIndex == 4) & (dgAccountDeclared.CurrentCell.RowIndex == dgAccountDeclared.RowCount - 1))
                                        {
                       // e.Handled = true;
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex];
                                        }
                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 4) & (dgAccountDeclared.CurrentCell.RowIndex == 0))
                    {
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex ];
                  //      dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex - 1];
                    }
                  //  else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 4)&(dgAccountDeclared.CurrentCell.RowIndex==1))
                   //                     {
                   //                         dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex-1 ];
                   //                     }

                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 4) & (dgAccountDeclared.CurrentCell.RowIndex != 0))
                    {
                        dgAccountDeclared.CurrentCell = dgAccountDeclared["Denomination", dgAccountDeclared.CurrentCell.RowIndex-1];
                    }
                    //dgAccountDeclared.CurrentCell = dgAccountDeclared["value", 0];


                    else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex == dgAccountDeclared.RowCount - 1))
                                        {
                                            tbCard.Focus();
                                        }
                                        else if ((dgAccountDeclared.CurrentCell.ColumnIndex == 5) & (dgAccountDeclared.CurrentCell.RowIndex != dgAccountDeclared.RowCount - 1))
                                            dgAccountDeclared.CurrentCell = dgAccountDeclared["value", dgAccountDeclared.CurrentCell.RowIndex];
                      */

                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OtchetPodrobn Otchet1 = new OtchetPodrobn();
            Otchet1.vub1 = 1;
            Otchet1.counting_vub = counting_vub;
            Otchet1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Log1Frm Otchet1 = new Log1Frm();
            Otchet1.idpercht = counting_vub;
            Otchet1.Show();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox10.SelectedIndex != -1)
            {
                if(pm.EnabledPossibility(perm, button14))
                    button14.Enabled = true;
                if (pm.EnabledPossibility(perm, textBox3))
                    textBox3.Enabled = true;
               //textBox3.Text = String.Empty;

            }
            else
            {
                button14.Enabled = false;
                textBox3.Enabled = false;
                //textBox3.Text=
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataBase.Connect();
            dataSet = null;
            dataSet = dataBase.GetData9("select * from t_g_cause_description");

            //Создаем новую строку для t_g_cause_description
            DataRow dataRow = dataSet.Tables[0].NewRow();

            dataRow["id_counting"] = countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"];
            dataRow["id_cause"] = comboBox10.SelectedValue.ToString();
            if (textBox3.Text != "") dataRow["description"] = textBox3.Text;


            dataRow["creation"] = DateTime.Now;
            dataRow["createuser"] = DataExchange.CurrentUser.CurrentUserId;



            //Добавление строку в переменную
            dataSet.Tables[0].Rows.Add(dataRow);

            //List<DataSet> dataset = new List<DataSet>();
            //dataset.Add(dataSet);


            //Обновление БД
            dataBase.UpdateData(dataSet, "t_g_cause_description");
            
            causeDiscDataSet = null;

            causeDiscDataSet = dataBase.GetData9("SELECT t1.[id],[id_counting], t2.[name],[description] FROM[CountingDB].[dbo].[t_g_cause_description] as t1 left join t_g_cause as t2 on t1.id_cause = t2.id where id_counting =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]);

            dataGridView5.DataSource = causeDiscDataSet.Tables[0];


            MessageBox.Show("Причина добавлена!");
            textBox3.Text = "";
            if(pm.EnabledPossibility(perm, button13))
                button13.Enabled = true;
            comboBox10.SelectedIndex = -1;

            if (causeDiscDataSet.Tables[0].Rows.Count > 0)
                if (pm.EnabledPossibility(perm, button18))
                    button18.Enabled = true;
            else
                button18.Enabled = false;

        }
        //22.07.2020
        // Воод коректировок данных
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==0)
            {
                if (dataGridView2.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                {
                    string Str = dataGridView2.Rows[e.RowIndex].Cells["Key-In"].Value.ToString().Trim();

                    int Num;

                    bool isNum = int.TryParse(Str, out Num);

                    if (isNum)//проверка на вводимое значение, если введено число то рабоатет, если нет, то выводить сообщение что введено не число!

                    {
                        //27.07.2020
                        //Баланс юзера
                        
                        Int64 userBalansCount = 
                            userBalansDataSet.Tables[0].AsEnumerable().Where(
                              x => x.Field<string>("name") == dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value.ToString()
                              && x.Field<Int64>("id_condition") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                              ).Select(x => x.Field<Int64>("Count22")).FirstOrDefault<Int64>();
                        // 
                        if(pm.EnabledPossibility(perm, btnUpdate))
                            btnUpdate.Enabled = true;
                        bool all = false;
                        long a, b, sum1, sum2, sum3, k1, k2, k3, k_all;
                        double s1, s2, s3, s_all;
                        b = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Key-In"].Value);

                        bool balans;
                        if (b >= 0 || userBalansCount + b >= 0)
                            balans = true;
                        else
                            balans = false;
                        
                        
                        
                        // работаем с dataGridView1
                        if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["Colper"].Value.ToString() != "")
                            k_all = Convert.ToInt64(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["Colper"].Value);
                        else k_all = 0;

                        if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["Sumpern"].Value.ToString() != "")
                            s_all = Convert.ToDouble(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["Sumpern"].Value);
                        else s_all = 0;



                        //
                        Int64 id_card = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value);//detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_card")).FirstOrDefault<Int64>();
                        Int64 id_counting = detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();
                       // MessageBox.Show("id---card="+id_card.ToString());

                      //  MessageBox.Show("id-card22=" + dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value.ToString());


                        if (rbl1.SelectedIndex.ToString() == "0")
                        {
                            if (dataGridView2.Rows[e.RowIndex].Cells["Kolbp"].Value.ToString() != "")
                                a = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolbp"].Value);
                            else
                                a = 0;
                            
                            if (((a + b) >= 0) & balans)
                            {
                                dataGridView2.Rows[e.RowIndex].Cells["Kolbp"].Value = b + a;
                                dataGridView2.Rows[e.RowIndex].Cells["Sumbp"].Value = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolbp"].Value) * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);


                                // работаем с dataGridView1
                                if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col4"].Value.ToString() != "")
                                    k1 = Convert.ToInt64(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col4"].Value);
                                else k1 = 0;

                                if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col5"].Value.ToString() != "")
                                    s1 = Convert.ToDouble(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col5"].Value);
                                else s1 = 0;


                                //без пересчета сумма и количество
                                s1 = s1 + b * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);

                                dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col4"].Value = k1 + b;
                                dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col5"].Value = s1;

                                //общая сумма и количество
                                all = true;

                                
                                     count = 0;
                                
                                if (
                                    detper3.Tables[0].AsEnumerable().Any
                                    (x=>x.Field<Int64>("id_denom")== Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value)
                                    && x.Field<Int64>("id_sost")== Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                                    && x.Field<Int32>("source") == 1
                                    //&& x.Field<Int64>("id_card")== Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value)
                                    ))
                                {
                                    //MessageBox.Show("есть в коллекции!");
                                    DataRow row = detper3.Tables[0].AsEnumerable().Where
                                        (
                                        x => x.Field<Int64>("id_denom") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value)
                                        && x.Field<Int64>("id_sost") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                                        && x.Field<Int32>("source") == 1
                                       // && x.Field<Int64>("id_card") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value)
                                        )
                                        .First<DataRow>();
                                    
                                    row["Kolbp1"] = /*dataGridView2.Rows[e.RowIndex].Cells["Kolbp1"].Value; */
                                        count = Convert.ToInt64(b) + ConvInt(row["Kolbp1"]);
                                    //count = Convert.ToInt64(b) + ConvInt(row["Kolp1"]);

                                    row["Sumbp1"] = Convert.ToDouble(/*Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolbp1"].Value)*/  count * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value)); //Convert.ToDouble(count * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));
                                        

                                }
                                else 
                                {
                                    // нужно заполнить все поля
                                    //MessageBox.Show("нет в коллекции!");
                                    DataRow row = detper3.Tables[0].NewRow();
                                    row["id_card"] = id_card; //detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_card")).FirstOrDefault<Int64>(); 
                                    row["id_counting"] = id_counting; // detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();
                                    row["id_denom"] = dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value;
                                    row["id_sost"] = dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value;
                                    row["Nomin1"] = dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value;
                                    row["Kolbp1"] = dataGridView2.Rows[e.RowIndex].Cells["Kolbp"].Value;//Convert.ToInt64(b+a);
                                    row["Sumbp1"] = Convert.ToDouble(Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolbp"].Value) *Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));//Convert.ToDouble(Convert.ToInt64(b+a)* Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));
                                    row["source"] = 1;

                                    detper3.Tables[0].Rows.Add(row);                                    
                                }
                                    
                                //22


                            }
                        }
                        if (rbl1.SelectedIndex.ToString() == "1")
                        {

                            //dataGridView2.Columns["Kolbp"].Visible = false;
                            //dataGridView2.Columns["Sumbp"].Visible = false;
                            //dataGridView2.Columns["Kolp"].Visible = true;
                            //dataGridView2.Columns["Sump"].Visible = true;
                            //dataGridView2.Columns["Kolm"].Visible = false;
                            //dataGridView2.Columns["Summ"].Visible = false;

                            if (dataGridView2.Rows[e.RowIndex].Cells["Kolp"].Value.ToString() != "")
                                a = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolp"].Value);
                            else a = 0;
                            //MessageBox.Show("a=" + a.ToString());
                            //MessageBox.Show("b=" + b.ToString());
                            //MessageBox.Show("a + b=" + (a + b).ToString());
                            //MessageBox.Show("balans =" + balans);
                            if ((a + b) >= 0 & balans)
                            {
                                dataGridView2.Rows[e.RowIndex].Cells["Kolp"].Value = b + a;
                                dataGridView2.Rows[e.RowIndex].Cells["Sump"].Value = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolp"].Value) * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);

                                // работаем с dataGridView1
                                if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col2"].Value.ToString() != "")
                                    k2 = Convert.ToInt64(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col2"].Value);
                                else k2 = 0;

                                if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col3"].Value.ToString() != "")
                                    s2 = Convert.ToDouble(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col3"].Value);
                                else s2 = 0;


                                //без пересчета сумма и количество
                                s2 = s2 + b * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);

                                dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col2"].Value = k2 + b;
                                dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col3"].Value = s2;

                                //общая сумма и количество
                                all = true;
                                //28.07.2020
                                //11
                                count = 0;
                                if (
                                    detper3.Tables[0].AsEnumerable().Any
                                    (x => x.Field<Int64>("id_denom") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value)
                                    && x.Field<Int64>("id_sost") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                                    && x.Field<Int32>("source") == 0
                                    && x.Field<Int64>("id_card") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value)
                                    )
                                    )
                                {
                                    //MessageBox.Show("есть в коллекции!");
                                    DataRow row = detper3.Tables[0].AsEnumerable().Where
                                        (
                                        x => x.Field<Int64>("id_denom") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value)
                                        && x.Field<Int64>("id_sost") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                                        && x.Field<Int32>("source") == 0
                                        && x.Field<Int64>("id_card") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value)
                                        )
                                        .First<DataRow>();

                                    row["Kolp1"] = count = Convert.ToInt64(b) + ConvInt(row["Kolp1"]);
                                    row["Sump1"] = Convert.ToDouble(count * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));


                                }
                                else
                                {
                                    // нужно заполнить все поля
                                    //MessageBox.Show("нет в коллекции!");
                                    DataRow row = detper3.Tables[0].NewRow();
                                    row["id_card"] = id_card;// detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_card")).FirstOrDefault<Int64>();
                                    row["id_counting"] = id_counting;// detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();
                                    row["id_denom"] = dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value;
                                    row["id_sost"] = dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value;
                                    row["Nomin1"] = dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value;
                                    row["Kolp1"] = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolp"].Value);
                                    row["Sump1"] = Convert.ToDouble(Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolp"].Value) * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));
                                    row["source"] = 0;

                                    detper3.Tables[0].Rows.Add(row);
                                }

                                //22
                            }
                        }
                        if (rbl1.SelectedIndex.ToString() == "2")
                        {
                            //dataGridView2.Columns["Kolbp"].Visible = false;
                            //dataGridView2.Columns["Sumbp"].Visible = false;
                            //dataGridView2.Columns["Kolp"].Visible = false;
                            //dataGridView2.Columns["Sump"].Visible = false;
                            //dataGridView2.Columns["Kolm"].Visible = true;
                            //dataGridView2.Columns["Summ"].Visible = true;

                           // MessageBox.Show(id_card.ToString());
                            if (dataGridView2.Rows[e.RowIndex].Cells["Kolm"].Value.ToString() != "")
                                a = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolm"].Value);
                            else a = 0;
                            //MessageBox.Show("a=" + a.ToString());
                            //MessageBox.Show("b=" + b.ToString());
                            //MessageBox.Show("a + b=" + (a + b).ToString());
                            //MessageBox.Show("balans =" + balans);

                            if ((a + b) >= 0 & balans)
                            {
                                dataGridView2.Rows[e.RowIndex].Cells["Kolm"].Value = b + a;
                                dataGridView2.Rows[e.RowIndex].Cells["Summ"].Value = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolm"].Value) * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);

                                // работаем с dataGridView1
                                if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col6"].Value.ToString() != "")
                                    k3 = Convert.ToInt64(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col6"].Value);
                                else k3 = 0;

                                if (dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col7"].Value.ToString() != "")
                                    s3 = Convert.ToDouble(dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col7"].Value);
                                else s3 = 0;


                                //без пересчета сумма и количество
                                s3 = s3 + b * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);

                                dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col6"].Value = k3 + b;
                                dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["col7"].Value = s3;

                                //общая сумма и количество
                                all = true;
                                //28.07.2020
                                //11
                                count = 0;
                                if (
                                    detper3.Tables[0].AsEnumerable().Any
                                    (x => x.Field<Int64>("id_denom") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value)
                                    && x.Field<Int64>("id_sost") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                                    && x.Field<Int32>("source") == 2
                                    //&& x.Field<Int64>("id_card") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value)
                                    )
                                    )
                                {
                                    //MessageBox.Show("есть в коллекции!");
                                    DataRow row = detper3.Tables[0].AsEnumerable().Where
                                        (
                                        x => x.Field<Int64>("id_denom") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value)
                                        && x.Field<Int64>("id_sost") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value)
                                        && x.Field<Int32>("source") == 2
                                       // && x.Field<Int64>("id_card") == Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["id_card"].Value)
                                    )
                                        .First<DataRow>();

                                    row["Kolm1"] = count = Convert.ToInt64(b) + ConvInt(row["Kolm1"]);
                                    row["Summ1"] = Convert.ToDouble(count * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));


                                }
                                else
                                {
                                    // нужно заполнить все поля
                                    //MessageBox.Show("нет в коллекции!");
                                    DataRow row = detper3.Tables[0].NewRow();
                                    row["id_card"] = id_card;// detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_card")).FirstOrDefault<Int64>();
                                    row["id_counting"] = id_counting;// detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();
                                    row["id_denom"] = dataGridView2.Rows[e.RowIndex].Cells["id_denom"].Value;
                                    row["id_sost"] = dataGridView2.Rows[e.RowIndex].Cells["id_sost"].Value;
                                    row["Nomin1"] = dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value;
                                    row["Kolm1"] = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolm"].Value);
                                    row["Summ1"] = Convert.ToDouble(Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Kolm"].Value) * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value));
                                    row["source"] = 2;

                                    detper3.Tables[0].Rows.Add(row);
                                }

                                //22
                            }
                        }

                        if (dataGridView2.Rows[e.RowIndex].Cells["Sumbp"].Value.ToString() != "")
                            sum1 = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Sumbp"].Value);
                        else sum1 = 0;
                        if (dataGridView2.Rows[e.RowIndex].Cells["Sump"].Value.ToString() != "")
                            sum2 = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Sump"].Value);
                        else sum2 = 0;
                        if (dataGridView2.Rows[e.RowIndex].Cells["Summ"].Value.ToString() != "")
                            sum3 = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Summ"].Value);
                        else sum3 = 0;

                        dataGridView2.Rows[e.RowIndex].Cells["Sumob"].Value = sum1 + sum2 + sum3;

                        // работаем с dataGridView1
                        //s_all = obchper1.Tables[0].AsEnumerable().Where(
                        //        x => x.Field<Int64>("id_currency") == Convert.ToInt64(rblCurrency.SelectedValue)
                        //    ).Select(x => x.Field<double>("Sumpern1")).First<double>();



                        //MessageBox.Show(rblCurrency.SelectedValue.ToString());
                        //MessageBox.Show(
                        //    obchper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_currency")).First<Int64>().ToString()
                        //    );
                        //MessageBox.Show(
                        //    s_all.ToString()
                        //    );


                        ////obchper1.Tables[0].AsEnumerable().Where(x=>x.Field<Int32>("Valut1") ==Convert.ToInt64(rblCurrency.SelectedValue))
                        ////dataGridView1.DataSource = obchper1.Tables[0];
                        ///rblCurrency.DisplayMember = "curr_code";
                        ///rblCurrency.ValueMember = "id";

                        if (all)
                        {
                            s_all = s_all + b * Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Nomin"].Value);
                            dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["Colper"].Value = k_all + b;
                            dataGridView1.Rows[rblCurrency.SelectedIndex].Cells["Sumpern"].Value = s_all;


                        }
                        //23.07.2020
                        //ввод данных для омбарную книгу

                        // correctDataSet = dataGridView2.DataSource;
                        dataGridView2.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;

                    }

                    else

                    {

                        // действие если строка - не число
                        MessageBox.Show("Вы ввели не коректное значение! = '" + Str + "'. Введите пожалуйста кооректное число");

                        dataGridView2.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;
                    }
                }

            }
        }
        //28.07.2020
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(detper3 != null)
            {
                if (detper3.Tables[0] != null)
                {
                   //MessageBox.Show("1");
                    dataBase.Connect();
                    dataSet = null;
                    long id = 0;
                    // Запись в t_g_counting_denom
                    dataSet = null;
                    dataSet = dataBase.GetData9("select * from t_g_counting_denom");
                    counting_id = detper3.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();

                    foreach (DataRow denomRow in detper3.Tables[0].Rows)
                    {
                        //MessageBox.Show("id_counting = " + Convert.ToInt64(denomRow["id_counting"].ToString()));
                        //MessageBox.Show("id_denomination = " + Convert.ToInt64(denomRow["id_denom"].ToString()));
                        //MessageBox.Show("id_sost = " + Convert.ToInt64(denomRow["id_sost"].ToString()));
                        //MessageBox.Show("source = " + Convert.ToInt32(denomRow["source"].ToString()));

                        //MessageBox.Show("2");
                        //MessageBox.Show("id card =" + denomRow["id_card"].ToString());
                        if (dataSet.Tables[0].AsEnumerable().Any(
                            x=>x.Field<Int64>("id_counting") == Convert.ToInt64(denomRow["id_counting"])
                            && x.Field<Int64>("id_denomination")== Convert.ToInt64(denomRow["id_denom"])
                            && x.Field<Int64>("id_condition") == Convert.ToInt64(denomRow["id_sost"])
                            && x.Field<Int32>("source") == Convert.ToInt64(denomRow["source"])
                            && x.Field<Int64>("id_card")==Convert.ToInt64(denomRow["id_card"])
                            )
                        )
                        {
                           
                            //Обнавления записи
                            id = dataSet.Tables[0].AsEnumerable().Where(
                            x => x.Field<Int64>("id_counting") == Convert.ToInt64(counting_id)
                            && x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id_denom"])
                            && x.Field<Int64>("id_condition") == Convert.ToInt64(denomRow["id_sost"])
                            && x.Field<Int32>("source") == Convert.ToInt32(denomRow["source"])
                            && x.Field<Int64>("id_card") == Convert.ToInt64(denomRow["id_card"])
                            ).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
                            
                            count = 0;
                            if (Convert.ToInt32(denomRow["source"]) == 0)
                                count = ConvInt(denomRow["Kolp1"]);
                            else if(Convert.ToInt32(denomRow["source"]) == 1)
                                count = ConvInt(denomRow["Kolbp1"]);
                            else if (Convert.ToInt32(denomRow["source"]) == 2)
                                count = ConvInt(denomRow["Kolm1"]);

                            Sum = ConvDoub(count * ConvInt(denomRow["Nomin1"]));

                            DataSet d1 = dataBase.GetData9("UPDATE t_g_counting_denom SET count = " + count.ToString().Trim() + ", fact_value = " + Sum.ToString().Trim() +
                                    ", lastupdate = GETDATE(), last_user_update = " + DataExchange.CurrentUser.CurrentUserId.ToString() + " WHERE id = " + id.ToString().Trim());
                        }
                        else
                        {
                           //MessageBox.Show("4");
                            //Новая запись
                            DataRow dataRow = dataSet.Tables[0].NewRow();

                            count = 0;
                            if (Convert.ToInt32(denomRow["source"]) == 0)
                                count = ConvInt(denomRow["Kolp1"]);
                            else if (Convert.ToInt32(denomRow["source"]) == 1)
                                count = ConvInt(denomRow["Kolbp1"]);
                            else if (Convert.ToInt32(denomRow["source"]) == 2)
                                count = ConvInt(denomRow["Kolm1"]);

                            Sum = ConvDoub(count * ConvInt(denomRow["Nomin1"]));

                            dataRow["id_counting"] = counting_id;
                            dataRow["id_card"] = denomRow["id_card"];
                            dataRow["id_denomination"] = denomRow["id_denom"];
                            dataRow["id_condition"] = denomRow["id_sost"];
                            dataRow["creation"] = DateTime.Now;
                            dataRow["lastupdate"] = DateTime.Now;
                            dataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                            dataRow["count"] = count;
                            dataRow["fact_value"] = Sum;
                            dataRow["source"] = denomRow["source"];

                            dataSet.Tables[0].Rows.Add(dataRow);

                            
                        }
                    }
                    dataBase.UpdateData(dataSet, "t_g_counting_denom");

                    #region Запись в омбарную книгу
                   /* {
                        id = 0;
                        dataSet = null;
                        dataSet = dataBase.GetData9("select * from t_w_cashtransfer where source=1");
                        //MessageBox.Show("counting_id = " + counting_id.ToString());
                    
                        if (
                            (dataSet.Tables[0].AsEnumerable().Where
                            (
                            x => x.Field<Int64>("id_user_receive") == Convert.ToInt64(DataExchange.CurrentUser.CurrentUserId)
                            && x.Field<Int64>("id_counting") == Convert.ToInt64(counting_id)
                            ).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>().ToString()
                            == "0"
                            )
                           )
                        {
                            //Создаем новую строку для t_w_cashtransfer
                            DataRow dataRow = dataSet.Tables[0].NewRow();
                            dataRow["creation"] = DateTime.Now;
                            dataRow["id_user_receive"] = DataExchange.CurrentUser.CurrentUserId;
                            dataRow["id_status"] = "3";
                            dataRow["lastupdate"] = DateTime.Now;
                            dataRow["lastuserupdate"] = DataExchange.CurrentUser.CurrentUserId;
                            dataRow["createuser"] = DataExchange.CurrentUser.CurrentUserId;
                            dataRow["source"] = "1";

                            dataRow["id_counting"] = counting_id.ToString();

                            dataRow["id_user_create"] = DataExchange.CurrentUser.CurrentUserId;
                            dataRow["id_zona_create"] = DataExchange.CurrentUser.CurrentUserZona;
                            dataRow["id_shift_create"] = DataExchange.CurrentUser.CurrentUserShift;
                            dataRow["id_shift_current"] = DataExchange.CurrentUser.CurrentUserShift;

                            //Добавление строку в переменную
                            dataSet.Tables[0].Rows.Add(dataRow);

                            //Обновление БД
                            dataBase.UpdateData(dataSet, "t_w_cashtransfer");
                            dataSet = dataBase.GetData9("SELECT *  FROM t_w_cashtransfer where source=1");

                        }
                        // Вытащели id из t_w_cashtransfer
                        id = dataSet.Tables[0].AsEnumerable().Where
                            (
                            x => x.Field<Int64>("id_user_receive") == Convert.ToInt64(DataExchange.CurrentUser.CurrentUserId)
                            && x.Field<Int64>("id_counting") == Convert.ToInt64(counting_id)
                            ).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();

                        dataSet = null;
                        dataSet = dataBase.GetData9("select * from t_g_cashtransfer_detalization");
                        dataSet2 = dataBase.GetData9("select * from t_g_cashtransfer_detalization");

                        //
                        Int64 id_casht_d = 0;


                        foreach (DataRow denomRow in detper3.Tables[0].Rows)
                        {
                            if (
                                dataSet2.Tables[0].AsEnumerable().Any(
                                x => x.Field<Int64>("id_cashtransfer") == Convert.ToInt64(id)
                                && x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id_denom"])
                                //&& x.Field<Int64>("id_condition") == Convert.ToInt64(denomRow["id_condition"])
                                )
                               )
                            {
                                //MessageBox.Show("Обнавления записи!");

                                id_casht_d = dataSet2.Tables[0].AsEnumerable().Where(
                                x => x.Field<Int64>("id_cashtransfer") == Convert.ToInt64(id)
                                && x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id_denom"])
                                && x.Field<Int64>("id_condition") == Convert.ToInt64(denomRow["id_sost"])
                                ).Select(x => x.Field<Int64>("id")).FirstOrDefault();

                                count = ConvInt(denomRow["Kolbp1"]) + ConvInt(denomRow["Kolp1"]) + ConvInt(denomRow["Kolm1"]);
                                Sum = Convert.ToDouble(count * Convert.ToInt64(denomRow["Nomin1"]));

                                DataSet d1 = dataBase.GetData9("UPDATE t_g_cashtransfer_detalization SET count = " + count.ToString() + ", fact_value = " + Sum.ToString() +
                                        ", lastupdate = GETDATE(), lastuserupdate = " + DataExchange.CurrentUser.CurrentUserId.ToString() + " WHERE id = " + id_casht_d.ToString());

                            }
                            else
                            {
                                //MessageBox.Show("Новая запись!");
                                count = ConvInt(denomRow["Kolbp1"]) + ConvInt(denomRow["Kolp1"]) + ConvInt(denomRow["Kolm1"]);
                                Sum = Convert.ToDouble(count * Convert.ToInt64(denomRow["Nomin1"]));

                                DataRow dataRow2 = dataSet.Tables[0].NewRow();

                                dataRow2["id_cashtransfer"] = id.ToString();
                                dataRow2["id_denomination"] = denomRow["id_denom"];
                                dataRow2["id_condition"] = denomRow["id_sost"];
                                dataRow2["creation"] = DateTime.Now;
                                dataRow2["count"] = count.ToString().Trim();
                                dataRow2["fact_value"] = Sum.ToString().Trim();
                                dataRow2["lastupdate"] = DateTime.Now;
                                dataRow2["lastuserupdate"] = DataExchange.CurrentUser.CurrentUserId;
                                //Добавление строку в переменную
                                dataSet.Tables[0].Rows.Add(dataRow2);
                            }

                        }
                    
                        dataBase.UpdateData(dataSet, "t_g_cashtransfer_detalization");
                    }
                    
                    */
                   #endregion
                }
                btnShow.Enabled = false;
                btnUpdate.Enabled = false;
                MessageBox.Show("Запись обновлена!");
                detal1();

                DataSet dsv1 = dataBase.GetData9("select iif( fact_value= declared_value, 2 , 1) as n1 from t_g_counting_content where id_counting= "+ countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString()); //t1 left join t_g_cards t3 on t1.id_bag=t3.id_bag where t3.id=" + card_id.ToString());
                /////30.12.2019

                int fl_prov = 0;
                if (dsv1.Tables[0].Rows.Count > 0)
                {

                   foreach (DataRow row in dsv1.Tables[0].Rows)
                    {
                        fl_prov = Convert.ToInt32(row["n1"]);
                        if (fl_prov == 0)
                            break;
                        if (fl_prov == 1)
                            break;


                    }
                    //string s3 =
                    dataBase.GetData9("update t_g_counting set lastupdate=getdate(),last_user_update=" + DataExchange.CurrentUser.CurrentUserId + ", fl_prov='" + fl_prov.ToString() + "' where id=" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString()); // in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "')";

                }
                Button4_Click(sender,e);

            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {   
            Int64 Card_id = Convert.ToInt64( detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_card")).FirstOrDefault<Int64>());
            counting_id = detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();
            
            DataSet detper2 = detper1.Clone();
           
            DataSet d1 = dataBase.GetData9(


                "select distinct 	t2.id id_denom, cast(t2.value as int) Nomin1, t1.id id_sost, t1.name sost1 from t_g_condition t1, t_g_denomination t2 "+
                 " where t2.id_currency = " + rblCurrency.SelectedValue.ToString()+
                 " /*and id_tipzen = 1*/ "+
                 " /* and t1.id in (1, 4, 5) */ "+
                  " order by id_denom "
                );
            

            foreach (DataRow d1Row in d1.Tables[0].Rows)
            {
                
                
                DataRow row1 = detper2.Tables[0].NewRow();
                row1["id_card"] = detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_card")).FirstOrDefault<Int64>();
                row1["id_counting"] = detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>();
                row1["id_sost"] = d1Row["id_sost"];
                row1["id_denom"] = d1Row["id_denom"];
                row1["id_currency"] = detper1.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_currency")).FirstOrDefault<Int64>(); 
                row1["Card1"] = detper1.Tables[0].AsEnumerable().Select(x => x.Field<string>("Card1")).FirstOrDefault<string>();
                row1["Nomin1"] = d1Row["Nomin1"];
                row1["sost1"] = d1Row["sost1"];

                row1["Kolbp1"] = detper1.Tables[0].AsEnumerable().Where(x=>x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                    && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Int64>("Kolbp1")).FirstOrDefault<Int64>().ToString();
                row1["Kolp1"] = detper1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                    && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Int64>("Kolp1")).FirstOrDefault<Int64>();
                row1["Kolm1"] = detper1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                    && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Int64>("Kolm1")).FirstOrDefault<Int64>();
                row1["Sumbp1"] = detper1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                                 && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Decimal>("Sumbp1")).FirstOrDefault<Decimal>();
                row1["Sump1"] =  detper1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                                 && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Decimal>("Sump1")).FirstOrDefault<Decimal>();
                row1["Summ1"] =  detper1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                                   && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Decimal>("Summ1")).FirstOrDefault<Decimal>();
                row1["Sumob1"] =  detper1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denom") == Convert.ToInt64(d1Row["id_denom"])
                 && x.Field<Int64>("id_sost") == Convert.ToInt64(d1Row["id_sost"])).Select(x => x.Field<Decimal>("Sumob1")).FirstOrDefault<Decimal>();


                
                detper2.Tables[0].Rows.Add(row1);
            }
            
            
            dataGridView2.DataSource = detper2.Tables[0];    
            
        }

        private long ConvInt(object x)
        {
            long b = 0;
            if (Convert.IsDBNull(x))
                b = 0;
            else b = Convert.ToInt64(x);

            return b;
        }

        private double ConvDoub(object x)
        {
            double b = 0;
            if (Convert.IsDBNull(x))
                b = 0;
            else b = Convert.ToInt64(x);

            return b;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MultiBagsForm multiBagsForm = new MultiBagsForm();
            

            DialogResult dialogResult = multiBagsForm.ShowDialog();

            dsMulti = dataBase.GetData9("SELECT  *  FROM t_g_multi_bags where deleted = 0 and id_shift_current=" + DataExchange.CurrentUser.CurrentUserShift);


            cbMulti.DataSource = dsMulti.Tables[0];
        }

        private void multibag()
        {
            if (cbMulti.SelectedIndex != -1)
            {
                dsMulti = dataBase.GetData9("SELECT  *  FROM t_g_multi_bags");// where deleted = 0");
                if (dsMulti != null)
                {
                    //MessageBox.Show(cbMulti.SelectedValue.ToString());
                    //MessageBox.Show(dsMulti.Tables[0].AsEnumerable().Where(x=>x.Field<Int64>("id")==Convert.ToInt64(cbMulti.SelectedValue)).Select(x=>x.Field<Int64>("id_client")).First<Int64>().ToString());
                    //MessageBox.Show("Convert.ToInt32(cbMulti.SelectedValue)=" + cbMulti.SelectedValue.ToString());

                    //MessageBox.Show("id client  = "+dsMulti.Tables[0].Rows[6]["id_client"].ToString());



                    cbID.SelectedValue = dsMulti.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(cbMulti.SelectedValue)).Select(x => x.Field<Int64>("id_client")).First<Int64>();
                       // Convert.ToInt32(dsMulti.Tables[0].Rows[Convert.ToInt32(cbMulti.SelectedValue) ]["id_client"]);

                    comboBox7.SelectedValue = dsMulti.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(cbMulti.SelectedValue)).Select(x => x.Field<Int64>("id_marschrut")).First<Int64>();
                    //Convert.ToInt32(dsMulti.Tables[0].Rows[Convert.ToInt32(cbMulti.SelectedValue) - 1]["id_marschrut"]);
                    cbEncashPoint.SelectedValue = dsMulti.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == Convert.ToInt64(cbMulti.SelectedValue)).Select(x => x.Field<Int64>("id_encashpoint")).First<Int64>();
                    //Convert.ToInt32(dsMulti.Tables[0].Rows[Convert.ToInt32(cbMulti.SelectedValue) - 1]["id_encashpoint"]);
                    if (comboBox7.SelectedIndex != -1)
                        comboBox7.Enabled = false;
                    else
                        if(pm.EnabledPossibility(perm, comboBox7))
                            comboBox7.Enabled = true;

                    //cbMulti.SelectedValue = Convert.ToInt64(countingDataSet.Tables[0].Rows[e.RowIndex]["id_multi_bag"]);
                }
            }
        }


        private void cbMulti_SelectedIndexChanged(object sender, EventArgs e)
        {
            multibag();
        }

        private void rblCurrency2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr2(2);
        }

        private void rblCondition2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr2(2);
        }

        private void rblcard2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr2(2);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataBase.Connect();
            dataSet = null;
            dataSet = dataBase.GetData9("select * from t_g_cause_description");

            //Создаем новую строку для t_g_cause_description
            DataRow dataRow = dataSet.Tables[0].NewRow();
            if(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"]!=null)
            dataRow["id_multi_bag"] = countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"];
            dataRow["id_cause"] = comboBox11.SelectedValue.ToString();
            if (textBox4.Text != "") dataRow["description"] = textBox4.Text;


            dataRow["creation"] = DateTime.Now;
            dataRow["createuser"] = DataExchange.CurrentUser.CurrentUserId;



            //Добавление строку в переменную
            dataSet.Tables[0].Rows.Add(dataRow);

            //List<DataSet> dataset = new List<DataSet>();
            //dataset.Add(dataSet);


            //Обновление БД
            dataBase.UpdateData(dataSet, "t_g_cause_description");

            causeDiscDataSet = null;
            if(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"]!=null)
            causeDiscDataSet = dataBase.GetData9("SELECT t1.[id],[id_multi_bag], t2.[name],[description] FROM[CountingDB].[dbo].[t_g_cause_description] as t1 left join t_g_cause as t2 on t1.id_cause = t2.id where id_multi_bag =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"]);
            if (causeDataSet != null)
                if(causeDataSet.Tables[0].Rows.Count>0)
            dataGridView8.DataSource = causeDiscDataSet.Tables[0];


            MessageBox.Show("Причина добавлена!");
            textBox4.Text = "";
            comboBox11.SelectedIndex = -1;
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox11.SelectedIndex != -1)
            {
                if(pm.EnabledPossibility(perm, button17))
                    button17.Enabled = true;
                if (pm.EnabledPossibility(perm, textBox4))
                    textBox4.Enabled = true;

            }
            else
            {
                button17.Enabled = false;
                textBox4.Enabled = false;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataSet = null;
            if(dgCounting!=null)
                if(dgCounting.Rows.Count>0)
                    if(dgCounting.CurrentCell.RowIndex>-1)
                        if(Convert.ToInt64(countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"])>0)
                        {
                            dataSet = dataBase.GetData9("Update t_g_multi_bags set fl_prov=3 where id = "+ countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id_multi_bag"].ToString());
                            MessageBox.Show("Расхождение утверждено!");
                        }
            detal1();
        }

        private void dgCounting_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            detal1();
        }

        private void cbClient_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) | (e.KeyCode == Keys.Back))
                PrepareData1();
        }

        private void cbID_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) | (e.KeyCode == Keys.Back))
                PrepareData1();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(
            "Желаете удалить причину?"
             ,
             "Сообщение",
            MessageBoxButtons.YesNo,
             MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1
            //,MessageBoxOptions.DefaultDesktopOnly
            );

            if (result1 == DialogResult.No)
                return;

            if (causeDiscDataSet.Tables[0].Rows.Count > 0)
                if (dataGridView5.CurrentCell != null)
                {
                    dataSet = dataBase.GetData9("delete from t_g_cause_description where id=" + causeDiscDataSet.Tables[0].Rows[dataGridView5.CurrentCell.RowIndex]["id"]);
                    dataSet = null;
                    causeDiscDataSet = null;
                    causeDiscDataSet = dataBase.GetData9("SELECT t1.[id],[id_counting], t2.[name],[description] FROM[CountingDB].[dbo].[t_g_cause_description] as t1 left join t_g_cause as t2 on t1.id_cause = t2.id where id_counting =" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"]);

                    dataGridView5.DataSource = causeDiscDataSet.Tables[0];

                    dataSet = dataBase.GetData9("select * from t_g_cause_description where id_counting=" + countingDataSet.Tables[0].Rows[dgCounting.CurrentCell.RowIndex]["id"].ToString());
                    if (dataSet.Tables[0].Rows.Count > 0)
                        if(pm.EnabledPossibility(perm, button13))
                            button13.Enabled = true;
                    else
                        button13.Enabled = false;

                    if (causeDiscDataSet.Tables[0].Rows.Count > 0)
                        if (pm.EnabledPossibility(perm, button18))
                            button18.Enabled = true;
                    else
                        button18.Enabled = false;

                }
        }

        private void cbAccount_Click(object sender, EventArgs e)
        {
            if (cbAccount.Text.Count() < 3)
            {
                cbAccount.Text = "KZ";
            cbAccount.Select(2, 0);

            }
            
            
            
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox7.SelectedIndex!=-1)
            {
                dataSet = dataBase.GetData9("Select t1.* from t_g_bags t1  left join t_g_counting t2 on t1.id=t2.id_bag  where deleted=0 and t1.id_marshr = " + comboBox7.SelectedValue.ToString().Trim());
                if (dataSet.Tables[0].Rows.Count > 0)
                    lbBag.Text = dataSet.Tables[0].Rows.Count.ToString();
                else
                    lbBag.Text = "0";
            }
            else
                lbBag.Text = "0";
        }

        private void rbl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView2 != null)
                if (dataGridView2.Columns != null)
                    if(dataGridView2.Columns["Kolbp"]!=null)
                {
                    if (rbl1.SelectedIndex == 0)
                    {
                        dataGridView2.Columns["Kolbp"].Visible = true;
                        dataGridView2.Columns["Sumbp"].Visible = true;
                        dataGridView2.Columns["Kolp"].Visible = false;
                        dataGridView2.Columns["Sump"].Visible = false;
                        dataGridView2.Columns["Kolm"].Visible = false;
                        dataGridView2.Columns["Summ"].Visible = false;

                    }
                    else if (rbl1.SelectedIndex == 1)
                    {
                        dataGridView2.Columns["Kolbp"].Visible = false;
                        dataGridView2.Columns["Sumbp"].Visible = false;
                        dataGridView2.Columns["Kolp"].Visible = true;
                        dataGridView2.Columns["Sump"].Visible = true;
                        dataGridView2.Columns["Kolm"].Visible = false;
                        dataGridView2.Columns["Summ"].Visible = false;

                    }
                    else if (rbl1.SelectedIndex == 2)
                    {
                        dataGridView2.Columns["Kolbp"].Visible = false;
                        dataGridView2.Columns["Sumbp"].Visible = false;
                        dataGridView2.Columns["Kolp"].Visible = false;
                        dataGridView2.Columns["Sump"].Visible = false;
                        dataGridView2.Columns["Kolm"].Visible = true;
                        dataGridView2.Columns["Summ"].Visible = true;

                    }
                }
        }

        private void cbsort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbsort.SelectedIndex!=-1)
            {
                Button4_Click(sender,e);
            }
        }

        private void btnAkt_Click(object sender, EventArgs e)
        {
            Console.WriteLine("idBag="+id_bag.ToString());
            AktNewForm aktNew = new AktNewForm(id_bag, Num_defect, bagdefectfactorDataSet);
            DialogResult dialogResult = aktNew.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Console.WriteLine("---------------");
                //countDenomDataSet.Clear();
                bagdefectfactorDataSet = aktNew.BagdefectfactorDataSet;
                if (bagdefectfactorDataSet != null)
                {
                    Console.WriteLine("++++++++++++++++++++++");
                    for (int i = 0; i < bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                        Console.WriteLine("id = " + bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString() + "checking[" + i + "]=" + bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString());
                }

            }
            else
            {
                Console.WriteLine("+-+-+-+-+-");
                bagdefectfactorDataSet = aktNew.BagdefectfactorDataSet;
                if (bagdefectfactorDataSet != null)
                {
                    Console.WriteLine("++++++++++++++++++++++");
                    for (int i = 0; i < bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                        Console.WriteLine("id = " + bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString() + "checking[" + i + "]=" + bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString());
                }

            }
            Num_defect = aktNew.Num_defect;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument reportDocument1 = new ReportDocument();
            reportDocument1.Load(@"C:\\report\\bag_acceptance_log.rpt");

            StreamReader studIni = new StreamReader(@"C:\\report\\bag_acceptance_log.ini", UTF8Encoding.Default);
            string x = studIni.ReadToEnd();
            //string[] 
            string[] y = x.Split('\n');

            for (int i = 0; i < y.Length; i++)
            {
                switch (y[i].Trim())
                {
                    case "PRM_Marchrut":
                        reportDocument1.SetParameterValue(i, comboBox7.Text.ToString());
                        break;
                    case "PRM_Shift":
                        reportDocument1.SetParameterValue(i, DataExchange.CurrentUser.CurrentUserShift);
                        break;
                }
            }

            reportDocument1.SetDatabaseLogon(dataBase.log, dataBase.par);
            //string myReportName = "C:\sales.pdf";
            string date = DateTime.Now.ToShortDateString().ToString().Trim();
            string time = DateTime.Now.ToLongTimeString().ToString().Trim();
            string datetime = " " + date + "-" + time.Replace(':', '.');
            //MessageBox.Show(datetime);

            if (!Directory.Exists(@"D:\\Отчеты\"))
            {
                Directory.CreateDirectory(@"D:\\Отчеты\");
            }

            reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\Отчеты\журнал учета принятых сумок"  + datetime +/*"."+time+*/".pdf");


            CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();

            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета


            ReportShowForm reportShowForm = new ReportShowForm();

            reportShowForm.Text = "журнал учета принятых сумок";
            reportShowForm.name = "журнал учета принятых сумок"; 
            reportShowForm.crystalReportViewer1 = crystalReportViewer1;
            reportShowForm.reportDocument = reportDocument1;
            reportShowForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)

        {
            ReportDocument reportDocument1 = new ReportDocument();
            reportDocument1.Load(@"C:\\report\\discrepancy_act.rpt");

            StreamReader studIni = new StreamReader(@"C:\\report\\discrepancy_act.ini", UTF8Encoding.Default);
            string x = studIni.ReadToEnd();
            //string[] 
            string[] y = x.Split('\n');

            for (int i = 0; i < y.Length; i++)
            {
                switch (y[i].Trim())
                {
                    case "PRM_Bag":
                        if (this.bag_name != "")
                            reportDocument1.SetParameterValue(i, this.bag_name);
                        break;
                    case "PRM_Shift":
                        reportDocument1.SetParameterValue(i, DataExchange.CurrentUser.CurrentUserShift);
                        break;
                }
            }

            reportDocument1.SetDatabaseLogon(dataBase.log, dataBase.par);
            //string myReportName = "C:\sales.pdf";
            string date = DateTime.Now.ToShortDateString().ToString().Trim();
            string time = DateTime.Now.ToLongTimeString().ToString().Trim();
            string datetime = " " + date + "-" + time.Replace(':', '.');
            //MessageBox.Show(datetime);

            if (!Directory.Exists(@"D:\\Отчеты\"))
            {
                Directory.CreateDirectory(@"D:\\Отчеты\");
            }

            reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\\Отчеты\Акт о кассовом просчете " + datetime +/*"."+time+*/".pdf");


            CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();

            crystalReportViewer1.ReportSource = reportDocument1;// Привязываем к элементу отображения отчета


            ReportShowForm reportShowForm = new ReportShowForm();

            reportShowForm.Text = "Акт о кассовом просчете";
            reportShowForm.name = "Акт о кассовом просчете";
            reportShowForm.crystalReportViewer1 = crystalReportViewer1;
            reportShowForm.reportDocument = reportDocument1;
            reportShowForm.Show();
        }


        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            detal1();
        }
    }
}
