using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CountingDB;
using DataExchange;
using System.Linq;
using System.IO;
using System.Threading;
using System.Timers;
using CountingForms.ParentForms;
using CountingForms.Interfaces;
using CountingDB.Entities;
using CountingForms.Services;

namespace CountingForms.DictionaryForms
{
    public partial class ClientDictForm : ParentForms.DictionaryTabForm
    {
        IPermisionsManager pm;
        MSDataBaseAsync dataBaseAsync;
        List<t_g_role_permisions> perm;

        private DataSet clientGroupDataSet = null;
        private DataSet cityDataSet = null;
        private DataSet cashCentreDataSet = null;
        private DataSet clienttocDataSet = null;
        private DataSet enCashPointDataSet = null;

        //////15.01.2020
        private DataSet enCashPointDataSetobsh = null;
        private DataSet accountDataSetobsh = null;
        //////15.01.2020

        private DataSet accountDataSet = null;
        private DataSet currencyDataSet = null;
        private DataSet allCashCentresDataset = null;

        /// 14.11.2019
        private DataSet pechat1 = null;
        private DataTable pechat2 = null;
        private DataSet pechat3 = null;
        private DataSet clientsDataSet2 = null;
        private DataSet accountDataSet2 = null;
        private DataSet nameColumnDataSet = null;
        /////13.01.2020
        private DataSet pechat4 = null;
        /////13.01.2020

        /////14.01.2020
        //private static System.Timers.Timer aTimer;
        private long flvub = 0;
        private int flizm = 0;
        private int flizmtocc = 0;
        private BindingSource bindingSource;
        /////14.01.2020

        /// 14.11.2019

        /////16.01.2020

        private DataView dataview1;

        /////16.01.2020

        /////21.01.2020

        private int flkn = 0;
        private BindingSource bindingSource1;

        private int y1 = 1;

        /////21.01.2020


        public ClientDictForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
            //comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (comboBox3.SelectedIndex != -1)
            //    comboBox4.Enabled = false;
            //else comboBox4.Enabled = true;
        }


        /// <summary>
        /// Конструктор создания форм отчетов
        /// </summary>
        /// <param name="formName">Название справочника</param>
        /// <param name="gridFieldName">Наименование поля</param>
        /// <param name="strCaption">Подпись поля</param>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="tabNames">подписи ко вкладкам</param>
        public ClientDictForm(String formName, String gridFieldName, String strCaption, String tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            //Подключение к БД
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            //Запрос данных клиентов
            dataSet = dataBase.GetData9("select * from t_g_client where deleted = 0 order by name");
            dgList.DataSource = dataSet.Tables[0];

            //Запрос данных по клиентским группам
            clientGroupDataSet = dataBase.GetData("t_g_clisubfml");

            //Запрос данных по городам
            cityDataSet = dataBase.GetData("t_g_city");

            //clienttocDataSet = dataBase.GetData();            

            InitializeComponent();
            //Заполнение название полей
            nameColumnDataSet = dataBase.GetData("t_g_name_column");
            //INF1
            if (nameColumnDataSet.Tables[0].AsEnumerable().Any(x => x.Field<string>("name") == "INF1"))
                label15.Text = nameColumnDataSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("name") == "INF1").Select(x => x.Field<string>("value")).FirstOrDefault<string>();
            else
                label15.Text = "INF1";

            //INF2
            if (nameColumnDataSet.Tables[0].AsEnumerable().Any(x => x.Field<string>("name") == "INF2"))
                label14.Text = nameColumnDataSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("name") == "INF2").Select(x => x.Field<string>("value")).FirstOrDefault<string>();
            else
                label14.Text = "INF2";


            //Заполнение клиентских групп
            cbClientGroup.DataSource = clientGroupDataSet.Tables[0];
            cbClientGroup.DisplayMember = "name";
            cbClientGroup.ValueMember = "id";
            cbClientGroup.SelectedIndex = -1;
            cbClientGroup.SelectedIndexChanged += new EventHandler(cbClientGroup_SelectedIndexChanged);

            //Заполнение городов
            cbCity.DataSource = cityDataSet.Tables[0];
            cbCity.DisplayMember = "name";
            cbCity.ValueMember = "id";
            cbCity.SelectedIndex = -1;
            cbCity.SelectedIndexChanged += new EventHandler(cbCity_SelectedIndexChanged);

            dgList.KeyUp += dgList_KeyUp;

            /////21.10.2019
            dgList.Columns.Add("BIN", "БИН/Код");
            dgList.Columns["BIN"].DataPropertyName = "BIN";


            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name = "id_subfml";
            dg1.HeaderText = "Группа клиента";
            //dg1.FlatStyle = FlatStyle.Flat;
            dg1.DataPropertyName = "id_subfml";
            dg1.DisplayMember = "name";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = clientGroupDataSet.Tables[0];
            dgList.Columns.Add(dg1);

            DataGridViewComboBoxColumn dg2 = new DataGridViewComboBoxColumn();
            dg2.Name = "id_city";
            dg2.HeaderText = "Город";
            dg2.DataPropertyName = "id_city";
            dg2.DisplayMember = "name";
            dg2.ValueMember = "id";
            dg2.ReadOnly = true;
            dg2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg2.DataSource = cityDataSet.Tables[0];
            dgList.Columns.Add(dg2);

            dgList.Columns.Add("address", "Адрес");
            dgList.Columns["address"].DataPropertyName = "address";
            dgList.Columns.Add("report_group_1", "Отчётная группа 1");
            dgList.Columns["report_group_1"].DataPropertyName = "report_group_1";
            dgList.Columns.Add("report_group_2", "Отчётная группа 2");
            dgList.Columns["report_group_2"].DataPropertyName = "report_group_2";
            dgList.Columns.Add("report_group_3", "Отчётная группа 3");
            dgList.Columns["report_group_3"].DataPropertyName = "report_group_3";
            dgList.Columns.Add("report_group_4", "Отчётная группа 4");
            dgList.Columns["report_group_4"].DataPropertyName = "report_group_4";
            dgList.Columns.Add("postindex", "Почтовый индекс");
            dgList.Columns["postindex"].DataPropertyName = "postindex";

            //17.09.2020
            dgList.Columns.Add("RKO_CODE", "RKO_CODE");
            dgList.Columns["RKO_CODE"].DataPropertyName = "RKO_CODE";
            dgList.Columns.Add("RKO_DEP_CODE", "RKO_DEP_CODEE");
            dgList.Columns["RKO_DEP_CODE"].DataPropertyName = "RKO_DEP_CODE";
            dgList.Columns.Add("KO_CODE", "KO_CODE");
            dgList.Columns["KO_CODE"].DataPropertyName = "KO_CODE";
            dgList.Columns.Add("KO_INF1", "KO_INF1");
            dgList.Columns["KO_INF1"].DataPropertyName = "KO_INF1";
            dgList.Columns.Add("KO_INF2", "KO_INF2");
            dgList.Columns["KO_INF2"].DataPropertyName = "KO_INF2";
            dgList.Columns.Add("KO_INF3", "KO_INF3");
            dgList.Columns["KO_INF3"].DataPropertyName = "KO_INF3";
            dgList.Columns.Add("KO_INF4", "KO_INF4");
            dgList.Columns["KO_INF4"].DataPropertyName = "KO_INF4";
            dgList.Columns.Add("KO_INF5", "KO_INF5");
            dgList.Columns["KO_INF5"].DataPropertyName = "KO_INF5";

            /////21.10.2019

            /// 14.11.2019

            /////13.01.2020
            pechat1 = dataBase.GetData9("select id      ,id_client      ,name_pechat from t_g_pechatclien");
            //    pechat1 = dataBase.GetData9("select id      ,id_client      ,name_pechat,img1,img2 from t_g_pechatclien");
            /////13.01.2020

            //dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.Columns.Add("name_pechat", "Описание пломбы");
            //dataGridView1.Columns["name_pechat"].DataPropertyName = "name_pechat";
            //dataGridView1.Columns["name_pechat"].SortMode = DataGridViewColumnSortMode.NotSortable;

            /// 14.11.2019


            dgList.CurrentCell = null;
            dgList.ReadOnly = true;
            dgList.Columns["BIN"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("id", "id");
            dgList.Columns["id"].DataPropertyName = "id";
            dgList.Columns["id"].Visible = false;


            /////08.01.2020
            timer2.Enabled = true;
            /////08.01.2020


            //01.10.2020
            clientsDataSet2 = dataBase.GetData9("select * from t_g_client where deleted=0 order by name");
            accountDataSet2 = dataBase.GetData9("select t1.*from t_g_account t1 left join t_g_client t2 on t1.id_client=t2.id where deleted=0 order by t2.name");
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
            //Список счетов
            comboBox4.Text = "";
            comboBox4.DataSource = null;
            comboBox4.Items.Clear();
            comboBox4.DisplayMember = "name";
            comboBox4.ValueMember = "id";
            comboBox4.DataSource = accountDataSet2.Tables[0];
            comboBox4.SelectedIndex = -1;



            /////14.01.2020
            oschist1();
            /////14.01.2020



        }

        private async void ClientDictForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        #region Работа с основным гридом

        #region Обработка измениний на БИН
        private void tbID_TextChanged(object sender, EventArgs e)
        {
            // int rowIndex = dgList.CurrentCell.RowIndex;
            // if (tbID.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["BIN"].ToString().Trim() != tbID.Text)
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            // else
            {
                /////21.01.2020
                // btnModify.Enabled = false;
                //  btnAdd.Enabled = false;
                /////21.01.2020

            }
        }
        #endregion

        #region Обработка измениний на группу клиентов
        private void cbClientGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;

            if (cbClientGroup.SelectedIndex != -1 && dgList.CurrentRow.Cells["id_subfml"].Value /*dataSet.Tables[0].Rows[rowIndex]["id_subfml"]*/ != cbClientGroup.SelectedValue)
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////21.01.2020

            }
        }

        #endregion

        #region Обработка изменений на город
        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;

            if (cbCity.SelectedIndex != -1 && dgList.CurrentRow.Cells["id_city"].Value/*dataSet.Tables[0].Rows[rowIndex]["id_city"]*/ != cbCity.SelectedValue)
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////21.01.2020

            }
        }
        #endregion

        #region Обработка измений на поле Адрес
        private void tbAddress_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbAddress.Text != String.Empty && dgList.CurrentRow.Cells["address"].Value.ToString().Trim()/*dataSet.Tables[0].Rows[rowIndex]["address"].ToString().Trim()*/ != tbAddress.Text.Trim())
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////21.01.2020

            }

        }

        #endregion

        #region Обработка изменений на поле Отчетная группа 1
        private void tbGroup1_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbGroup1.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["report_group_1"].ToString().Trim() != tbGroup1.Text.Trim())
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */

            }

        }
        #endregion

        #region Обработка изменений на поле Отчетная группа 2
        private void tbGroup2_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbGroup2.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["report_group_2"].ToString().Trim() != tbGroup2.Text.Trim())
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                //btnModify.Enabled = false;
                //btnAdd.Enabled = false;
                /////21.01.2020

            }
        }
        #endregion

        #region Обработка изменений на поле Отчетная группа 3
        private void tbGroup3_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbGroup3.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["report_group_3"].ToString().Trim() != tbGroup3.Text.Trim())
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////21.01.2020

            }
        }
        #endregion

        #region Обработка изменений на поле Отчетная группа 4
        private void tbGroup4_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbGroup4.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["report_group_4"].ToString().Trim() != tbGroup4.Text.Trim())
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////21.01.2020

            }
        }
        #endregion

        #region Обработка изменений на поле почтовый индекс
        private void tbPostIndex_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (tbPostIndex.Text != String.Empty && dataSet.Tables[0].Rows[rowIndex]["postindex"].ToString().Trim() != tbPostIndex.Text.Trim())
            {
                /////21.01.2020
                /*
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                */
                /////21.01.2020

            }
            else
            {
                /////21.01.2020
                /*
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                */
                /////21.01.2020

            }
        }
        #endregion

        #region Подготовка всех компонентов
        private void AllConntrolsView()
        {
            //Убираем обработчик по добавлению кассоввых центров
            clbCashCentre.ItemCheck -= clbCashCentre_ItemCheck;

            //////14.01.2020

            if (flvub == 0)
                clienttocDataSet = dataBase.GetData("t_g_clienttocc", "id_client", "-1");
            else

                //////14.01.2020


                //Загружаем данные связей выбранного клиента с кассовыми центрами и точками инкассации
                clienttocDataSet = dataBase.GetData("t_g_clienttocc", "id_client", dgList.CurrentRow.Cells["id"].Value.ToString()); //dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());
            //Загружаем кассовые центры для выбранного клиента
            cashCentreDataSet = dataBase.GetData("t_g_cashcentre", "id",
                clienttocDataSet.Tables[0].AsEnumerable().Select(r => r.Field<Int64>("id_cashcentre").ToString()).ToList<string>());
            //Загружаем все кассовые центры из справочника

            /////11.02.2020

            //allCashCentresDataset = dataBase.GetData("t_g_cashcentre");
            allCashCentresDataset = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
            /////11.02.2020

            //Загружаем все валюты 
            currencyDataSet = dataBase.GetData("t_g_currency");

            //Очищаем грид с кассовыми центрами
            ClearDataGrid(dgCashCentre);
            //Очищаем грид с точками инкассации
            ClearDataGrid(dgEnCashPoint);
            //Очищаем грид со счетами
            ClearDataGrid(dgAccounts);

            //Добавляем гриду с кассовыми центрами столбец с назавниями
            dgCashCentre.Columns.Add("branch_name", "Кассовый центр");
            dgCashCentre.Columns["branch_name"].DataPropertyName = "branch_name";
            dgCashCentre.Columns["branch_name"].Visible = true;

            dgCashCentre.Columns.Add("id", "id центр");
            dgCashCentre.Columns["id"].DataPropertyName = "id";
            dgCashCentre.Columns["id"].Visible = false;

            //Если у клиента есть кассовые центры
            if (cashCentreDataSet.Tables.Count > 0)
            {

                /////16.01.2020
                /*
                 *  bindingSource.DataSource = pechat4.Tables[0];

            dataGridView1.DataSource = bindingSource;
                dataview1 = cashCentreDataSet.Tables[0].DefaultView;
                dataview1.Sort = "branch_name ASC, branch_name DESC";
                dgCashCentre.DataSource = dataview1;
                */


                ////21.01.2020
                dgCashCentre.DataSource = cashCentreDataSet.Tables[0];
                /*
                bindingSource1 = new BindingSource();
                bindingSource1.DataSource = cashCentreDataSet.Tables[0];
                dgCashCentre.DataSource = bindingSource1;
                */
                ////21.01.2020

                /////16.01.2020

                //Выводим их в гриде 


                /////08.01.2020
                dgCashCentre.AutoResizeColumns();
                /////08.01.2020

                //Снимаем выделение 
                dgCashCentre.ClearSelection();

            }

            //Добавляем все кассовые центры в компонент выбора кассовых центров
            ((ListBox)clbCashCentre).DataSource = allCashCentresDataset.Tables[0];
            ((ListBox)clbCashCentre).DisplayMember = allCashCentresDataset.Tables[0].Columns["branch_name"].ColumnName;
            ((ListBox)clbCashCentre).ValueMember = allCashCentresDataset.Tables[0].Columns["id"].ColumnName;
            //Убираем выделение
            clbCashCentre.ClearSelected();
            //Делаем все центры неактивными
            UncheckCheckBox(clbCashCentre);
            //Если у клиента есть кассовые центры
            if (cashCentreDataSet.Tables.Count > 0)
            {
                //Отмечаем те, которые есть у клиента
                SetCheckedListBox(cashCentreDataSet, clbCashCentre);
            }
            //Добавляем обработчик 
            clbCashCentre.ItemCheck += clbCashCentre_ItemCheck;
            //Снимаем выбеленение кассовых центров
            clbCashCentre.ClearSelected();

            //Привязываем валюту к счетам
            cbAccountCurrency.DisplayMember = "curr_code";
            cbAccountCurrency.ValueMember = "id";
            cbAccountCurrency.DataSource = currencyDataSet.Tables[0];

            //Выбираем точки инкассации для данного клиента
            enCashPointDataSet = dataBase.GetData("t_g_encashpoint", "id_clienttocc",
              clienttocDataSet.Tables[0].AsEnumerable().
              Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[0]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[0]["id"])).
              Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
              ToList<string>());

            //Выбираем счета для точек инкассации
            accountDataSet = dataBase.GetData("t_g_account", "id_encashpoint",
               (enCashPointDataSet.Tables.Count > 0 && enCashPointDataSet.Tables[0].Rows.Count > 0) ? enCashPointDataSet.Tables[0].Rows[0]["id"].ToString() : "0");
            currencyDataSet = dataBase.GetData("t_g_currency");


            ///////15.01.2020
            /////заполняем всеми записями


            enCashPointDataSetobsh = dataBase.GetData9("select t2.* from t_g_clienttocc as t1 inner join t_g_encashpoint as t2 on(t1.id=t2.id_clienttocc) where t1.id_client=" + dgList.CurrentRow.Cells["id"].Value.ToString());// dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());

            accountDataSetobsh = dataBase.GetData("t_g_account", "id_client", dgList.CurrentRow.Cells["id"].Value.ToString().Trim());// dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());

            ///////15.01.2020


            //Отображаем точки инкассации
            EncashPoint_View();

            //Отображаем счета
            Account_View();
        }
        #endregion

        #endregion

        #region Манипуляции с клиентом

        ////08.01.2020

        private void vubcl1()
        {

            if (dgList.CurrentCell != null)
            {

                int rowIndex = dgList.CurrentCell.RowIndex;


                // base.dgList_SelectionChanged(sender, e);

                /////14.01.2020
                flvub = Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value); // dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id"] );
                /////14.01.2020

                tbName.Text = dgList.CurrentRow.Cells["name"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["name"].ToString().Trim();
                tbID.Text = dgList.CurrentRow.Cells["BIN"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["BIN"].ToString().Trim();
                //Группа клиента
                cbClientGroup.SelectedValue = dgList.CurrentRow.Cells["id_subfml"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_subfml"];
                //Город
                cbCity.SelectedValue = dgList.CurrentRow.Cells["id_city"].Value.ToString().Trim();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"];
                //Адрес
                tbAddress.Text = dgList.CurrentRow.Cells["address"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["address"].ToString().Trim();
                //Группы
                tbGroup1.Text = dgList.CurrentRow.Cells["report_group_1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_1"].ToString().Trim();
                tbGroup2.Text = dgList.CurrentRow.Cells["report_group_2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_2"].ToString().Trim();
                tbGroup3.Text = dgList.CurrentRow.Cells["report_group_3"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_3"].ToString().Trim();
                tbGroup4.Text = dgList.CurrentRow.Cells["report_group_4"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_4"].ToString().Trim();
                //Индекс
                tbPostIndex.Text = dgList.CurrentRow.Cells["postindex"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["postindex"].ToString().Trim();
                //17.09.2020
                tbRKO_CODE.Text = dgList.CurrentRow.Cells["RKO_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["RKO_CODE"].ToString().Trim();
                tbRKO_DEP_CODE.Text = dgList.CurrentRow.Cells["RKO_DEP_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["RKO_DEP_CODE"].ToString().Trim();
                tbKO_CODE.Text = dgList.CurrentRow.Cells["KO_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_CODE"].ToString().Trim();
                tbKO_INF1.Text = dgList.CurrentRow.Cells["KO_INF1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF1"].ToString().Trim();
                tbKO_INF2.Text = dgList.CurrentRow.Cells["KO_INF2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF2"].ToString().Trim();
                tbKO_INF3.Text = dgList.CurrentRow.Cells["KO_INF3"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF3"].ToString().Trim();
                tbKO_INF4.Text = dgList.CurrentRow.Cells["KO_INF4"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF4"].ToString().Trim();
                dtpKO_INF5.Text = dgList.CurrentRow.Cells["KO_INF5"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF5"].ToString().Trim();

                tbEncashPointName.Clear();
                tbPointAdrress.Clear();
                tbAccountName.Clear();
                cbAccountCurrency.Text = "";

                /////
                dgCashCentre.ClearSelection();
                // if (dgCashCentre.Rows.Count>0) 
                dgCashCentre.DataSource = null;
                /////

                //Заполнение всех элементов 
                AllConntrolsView();
                //Фокус на точке инкасации
                tbEncashPointName.Focus();

                /////14.11.2019

                ////20.01.2020
                pechat4 = null;
                ////20.01.2020

                /////13.01.2020
                flizm = 0;
                flizmtocc = 0;
                if (tabControl1.SelectedIndex == 3)
                    pechatpoisk();
                /////13.01.2020




                /////14.11.2019


                textBox1.Text = "Картинки";



                ////21.01.2020
                //btnAdd.Enabled = true;
                //btnModify.Enabled = true;
                flkn = 1;
                btnAdd.Enabled = false;
                btnModify.Enabled = true;
                ////21.01.2020

            }


        }

        ////08.01.2020


        //////06.01.2020

        protected override void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            vubcl1();

            //  pok1();

            /*
            if (dgList.CurrentCell != null)
            {

                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);



                tbID.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["BIN"].ToString().Trim();
                //Группа клиента
                cbClientGroup.SelectedValue = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_subfml"];
                //Город
                cbCity.SelectedValue = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"];
                //Адрес
                tbAddress.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["address"].ToString().Trim();
                //Группы
                tbGroup1.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_1"].ToString().Trim();
                tbGroup2.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_2"].ToString().Trim();
                tbGroup3.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_3"].ToString().Trim();
                tbGroup4.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_4"].ToString().Trim();
                //Индекс
                tbPostIndex.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["postindex"].ToString().Trim();

                tbEncashPointName.Clear();
                tbPointAdrress.Clear();
                tbAccountName.Clear();
                cbAccountCurrency.Text = "";

                /////
                dgCashCentre.ClearSelection();
                // if (dgCashCentre.Rows.Count>0) 
                dgCashCentre.DataSource = null;
                /////

                //Заполнение всех элементов 
                AllConntrolsView();
                //Фокус на точке инкасации
                tbEncashPointName.Focus();

                /////14.11.2019

                pechatpoisk();




                /////14.11.2019

            }
            */
        }

        //////06.01.2020

        /// 21.10.2019
        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {
            //////06.01.2020
            tbName.Text = "";
            /*
            if (dgList.CurrentCell != null)
            {

                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);

               

                tbID.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["BIN"].ToString().Trim();
                //Группа клиента
                cbClientGroup.SelectedValue = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_subfml"];
                //Город
                cbCity.SelectedValue = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"];
                //Адрес
                tbAddress.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["address"].ToString().Trim();
                //Группы
                tbGroup1.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_1"].ToString().Trim();
                tbGroup2.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_2"].ToString().Trim();
                tbGroup3.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_3"].ToString().Trim();
                tbGroup4.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_4"].ToString().Trim();
                //Индекс
                tbPostIndex.Text = dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["postindex"].ToString().Trim();

                 tbEncashPointName.Clear();
                 tbPointAdrress.Clear();
                tbAccountName.Clear();
                cbAccountCurrency.Text = "";

                /////
                dgCashCentre.ClearSelection();
                // if (dgCashCentre.Rows.Count>0) 
                dgCashCentre.DataSource = null;
                /////

                //Заполнение всех элементов 
                AllConntrolsView();
                //Фокус на точке инкасации
                tbEncashPointName.Focus();

                /////14.11.2019

                pechatpoisk();

                


                /////14.11.2019

            }
            */
            //////06.01.2020

        }
        /// 21.10.2019

        /////14.11.2019
        #region
        /*
        private void pokazimg()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            if (dataGridView1.CurrentCell != null)
            {
                if (pechat4 != null)
                    if (pechat4.Tables[0] != null)
                    {
                        if (dataGridView1.CurrentRow.Index < pechat4.Tables[0].Rows.Count)
                        {
                            if ((((DataRowView)bindingSource.Current)["img1"] != null) & (((DataRowView)bindingSource.Current)["img1"] != DBNull.Value))
                            {


                                MemoryStream memoryStream = new MemoryStream();

                                ////20.01.2020
                                // memoryStream.Write((byte[])pechat4.Tables[0].Rows[dataGridView1.CurrentRow.Index]["img1"], 0, ((byte[])pechat4.Tables[0].Rows[dataGridView1.CurrentRow.Index]["img1"]).Length);
                                memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img1"], 0, ((byte[])((DataRowView)bindingSource.Current)["img1"]).Length);
                                ////20.01.2020
                                pictureBox1.Image = Image.FromStream(memoryStream);

                                ////22.01.2020
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                ////22.01.2020

                                memoryStream.Dispose();






                            }
                            else
                                pictureBox1.Image = null;

                            if ((((DataRowView)bindingSource.Current)["img2"] != null) & (((DataRowView)bindingSource.Current)["img2"] != DBNull.Value))
                            {

                                MemoryStream memoryStream = new MemoryStream();

                                ////20.01.2020
                                //memoryStream.Write((byte[])pechat4.Tables[0].Rows[dataGridView1.CurrentRow.Index]["img2"], 0, ((byte[])pechat4.Tables[0].Rows[dataGridView1.CurrentRow.Index]["img2"]).Length);
                                memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img2"], 0, ((byte[])((DataRowView)bindingSource.Current)["img2"]).Length);

                                ////20.01.2020

                                pictureBox2.Image = Image.FromStream(memoryStream);

                                ////22.01.2020
                                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                ////22.01.2020

                                memoryStream.Dispose();






                            }
                            else
                                pictureBox2.Image = null;


                        }

                    }
            }


        }
        */
        #endregion
        private void pechatpoisk()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;

            bindingSource = new BindingSource();
            
            if (flizm == 0)
            {

                if (pechat4 == null)
                {
                    ////20.01.2020

                    if (flvub > 0)
                        pechat4 = dataBase.GetData9("select * from t_g_pechatclien where " + "id_client='" + dgList.CurrentRow.Cells["id"].Value.ToString() /*dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id"].ToString()*/ + "'");
                    else
                        pechat4 = dataBase.GetData9("select * from t_g_pechatclien where  id_client=-1 ");

                    //////20.01.2020
                }
                //  bindingSource.DataSource = pechat4.Tables[0];

                //  dataGridView1.DataSource = bindingSource;
                //////20.01.2020

                // dataGridView1.DataSource = pechat4.Tables[0];
            }

            //////20.01.2020
            #region
            /*
            bindingSource.DataSource = pechat4.Tables[0];

            dataGridView1.DataSource = bindingSource;

            pokazimg();
            */
            #endregion
            //////20.01.2020

            //  pechat2 = dataBase.GetData9("select id      ,id_client      ,name_pechat,img1,img2 from t_g_pechatclien where " +"id_client='" + dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id"].ToString() + "'").Tables[0];
            //  dataGridView1.DataSource = pechat2;



            /*
            pechat1 = dataBase.GetData9("select id      ,id_client      ,name_pechat from t_g_pechatclien");
            
            string sqlselect = "id_client='" + dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id"].ToString() + "'";
            DataRow[] datrow1 = pechat1.Tables[0].Select(sqlselect);
            pechat2 = null;

            if (datrow1.Count() > 0)
            {
                pechat2 = pechat1.Tables[0].Select(sqlselect).CopyToDataTable();
                dataGridView1.DataSource = pechat2;

               // pokazimg();

            }
            else
                dataGridView1.DataSource = null;
            */
            //////13.01.2020

        }


        /////14.11.2019


        #region Выбор клиента
        protected override void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //Убираем обработчик
                clbCashCentre.ItemCheck -= clbCashCentre_ItemCheck;
                //Базовый метод для выбора клиента
                base.dgList_CellDoubleClick(sender, e);

                /////14.01.2020
                
                flvub = Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value);// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id"]);
                /////14.01.2020
                //MessageBox.Show("e.rowindex = "+e.RowIndex.ToString());
                //БИН
                tbID.Text = dataSet.Tables[0].Rows[e.RowIndex]["BIN"].ToString().Trim();
                //Группа клиента
                cbClientGroup.SelectedValue = dataSet.Tables[0].Rows[e.RowIndex]["id_subfml"];
                //Город
                cbCity.SelectedValue = dataSet.Tables[0].Rows[e.RowIndex]["id_city"];
                //Адрес
                tbAddress.Text = dataSet.Tables[0].Rows[e.RowIndex]["address"].ToString().Trim();
                //Группы
                tbGroup1.Text = dataSet.Tables[0].Rows[e.RowIndex]["report_group_1"].ToString().Trim();
                tbGroup2.Text = dataSet.Tables[0].Rows[e.RowIndex]["report_group_2"].ToString().Trim();
                tbGroup3.Text = dataSet.Tables[0].Rows[e.RowIndex]["report_group_3"].ToString().Trim();
                tbGroup4.Text = dataSet.Tables[0].Rows[e.RowIndex]["report_group_4"].ToString().Trim();
                //Индекс
                tbPostIndex.Text = dataSet.Tables[0].Rows[e.RowIndex]["postindex"].ToString().Trim();
                //17.09.2020
                tbRKO_CODE.Text = dataSet.Tables[0].Rows[e.RowIndex]["RKO_CODE"].ToString().Trim();
                tbRKO_DEP_CODE.Text = dataSet.Tables[0].Rows[e.RowIndex]["RKO_DEP_CODE"].ToString().Trim();
                tbKO_CODE.Text = dataSet.Tables[0].Rows[e.RowIndex]["KO_CODE"].ToString().Trim();
                tbKO_INF1.Text = dataSet.Tables[0].Rows[e.RowIndex]["KO_INF1"].ToString().Trim();
                tbKO_INF2.Text = dataSet.Tables[0].Rows[e.RowIndex]["KO_INF2"].ToString().Trim();
                tbKO_INF3.Text = dataSet.Tables[0].Rows[e.RowIndex]["KO_INF3"].ToString().Trim();
                tbKO_INF4.Text = dataSet.Tables[0].Rows[e.RowIndex]["KO_INF4"].ToString().Trim();
                dtpKO_INF5.Text = dataSet.Tables[0].Rows[e.RowIndex]["KO_INF5"].ToString().Trim();


                //Заполнение всех элементов 
                AllConntrolsView();
                //Фокус на точке инкасации
                tbEncashPointName.Focus();

                /////14.11.2019

                /////13.01.2020
                flizm = 0;
                flizmtocc = 0;
                if (tabControl1.SelectedIndex == 3)
                    pechatpoisk();

                /////13.01.2020



                /////14.11.2019
            }
        }
        #endregion

        #region Кнопка добавить клиента
        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            
            /// 21.10.2019
            string s1 = tbGroup1.Text;
            string s2 = tbGroup2.Text;
            string s3 = tbGroup3.Text;
            string s4 = tbGroup4.Text;
            int num;
          


            if (tbID.Text.Trim().Length > 20)
                MessageBox.Show("БИН не должен быть больше 20 знаков!");

            else
            if (tbPostIndex.Text.Trim().Length > 10)
                MessageBox.Show("Почтовый индекс не должен быть больше 10 символов!");

            else
            if (((tbGroup1.Text.Trim() != "") & (Int32.TryParse(s1, out num) != true)) 
               | ((tbGroup2.Text.Trim() != "")&(Int32.TryParse(s2, out num) != true))
               | ((tbGroup3.Text.Trim() != "") & (Int32.TryParse(s3, out num) != true))
               | ((tbGroup4.Text.Trim() != "") & (Int32.TryParse(s4, out num) != true))
                )
                MessageBox.Show("Отчётная группа должна быть числом!");
            else
            {

                //    int result1=              dataSet.Tables[0].AsEnumerable().              Where(r => r.Field<String>("BIN") == tbID.Text.Trim().ToString()).              Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).Count();
                //Поиск клиента с таким же именем
                //    int
                //  result1 = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[1].Value.ToString() == tbID.Text.Trim().ToString()).Count();

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

                //      int        result1 = dataSet.Tables[0].AsEnumerable().Where(x => x.Field<String>("BIN") == tbID.Text.Trim().ToString()).Count();

                //Если имя клиента не пустое и такого имени не существует
                //   if (tbID.Text != String.Empty && result1 == 0)
                {
                    /// 21.10.2019

                    //Поиск клиента с таким же именем
                    int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
                    //Если имя клиента не пустое и такого имени не существует
                    if (tbName.Text != String.Empty && result == 0)
                    {
                        //Создаем новую строку
                        DataRow dataRow = dataSet.Tables[0].NewRow();
                        dataRow["creation"] = DateTime.Now;
                        dataRow[gridFieldName] = tbName.Text;
                        dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                        dataRow["lastupdate"] = DateTime.Now;

                        //Клиентская группа
                        if (cbClientGroup.SelectedValue != null)
                        {
                            dataRow["id_subfml"] = cbClientGroup.SelectedValue;
                        }
                        else
                        {
                            MessageBox.Show("Выберите клиентскую группу");
                            return;
                        }

                        //БИН
                        dataRow["BIN"] = tbID.Text;
                        if (cbCity.SelectedValue != null)
                        {
                            dataRow["id_city"] = cbCity.SelectedValue;

                        }
                        else
                        {
                            MessageBox.Show("Выберите город");
                            return;

                        }

                        //Адрес
                        dataRow["address"] = tbAddress.Text;
                        
                            
                            dataRow["report_group_1"] = tbGroup1.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup1.Text);
                        dataRow["report_group_2"] = tbGroup2.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup2.Text);
                        dataRow["report_group_3"] = tbGroup3.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup3.Text);
                        dataRow["report_group_4"] = tbGroup4.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup3.Text);
                        //Почтовый индекс
                        dataRow["postindex"] = tbPostIndex.Text;
                        //17.09.2020
                        dataRow["RKO_CODE"] = tbRKO_CODE.Text==string.Empty ? (Object)DBNull.Value:(tbRKO_CODE.Text);
                        dataRow["RKO_DEP_CODE"] = tbRKO_DEP_CODE.Text == string.Empty ? (Object)DBNull.Value : (tbRKO_DEP_CODE.Text);
                        dataRow["KO_CODE"] = tbKO_CODE.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_CODE.Text.Trim());
                        dataRow["KO_INF1"] = tbKO_INF1.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF1.Text.Trim());
                        dataRow["KO_INF2"] = tbKO_INF2.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF2.Text.Trim());
                        dataRow["KO_INF3"] = tbKO_INF3.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF3.Text.Trim());
                        dataRow["KO_INF4"] = tbKO_INF4.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF4.Text.Trim());
                        dataRow["KO_INF5"] = dtpKO_INF5.Text.Trim();

                        //Добавление строку в переменную
                        dataSet.Tables[0].Rows.Add(dataRow);

                        //////16.01.2020


                        List<DataSet> dataset1=new List<DataSet>();
                        dataset1.Add(dataSet);
                        dataset1.Add(pechat4);

                        dataset1.Add(clienttocDataSet);
                        dataset1.Add(enCashPointDataSetobsh);
                        dataset1.Add(accountDataSetobsh);

                        dataBase.GetDataset11(dataset1,0,1);
                        dataSet = dataBase.GetData("t_g_client", "deleted", "0");
                        dgList.DataSource = dataSet.Tables[0];
                        //Обновление БД
                        //    dataBase.UpdateData(dataSet, "t_g_client");

                        //////16.01.2020

                        //Подготовка всех компонентов
                        AllConntrolsView();
                        dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];

                        /// 21.10.2019
                        dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];
                        base.dgList_SelectionChanged(sender, e);
                        dgList.Refresh();
                        dgList.Update();

                        tbID.Text = dgList.CurrentRow.Cells["BIN"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["BIN"].ToString().Trim();
                        //Группа клиента
                        cbClientGroup.SelectedValue = dgList.CurrentRow.Cells["id_subfml"].Value;//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_subfml"];
                        //Город
                        cbCity.SelectedValue = dgList.CurrentRow.Cells["id_city"].Value; //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"];
                        //Адрес
                        tbAddress.Text = dgList.CurrentRow.Cells["address"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["address"].ToString().Trim();
                        //Группы
                        tbGroup1.Text = dgList.CurrentRow.Cells["report_group_1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_1"].ToString().Trim();
                        tbGroup2.Text = dgList.CurrentRow.Cells["report_group_2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_2"].ToString().Trim();
                        tbGroup3.Text = dgList.CurrentRow.Cells["report_group_3"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_3"].ToString().Trim();
                        tbGroup4.Text = dgList.CurrentRow.Cells["report_group_4"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_4"].ToString().Trim();
                        //Индекс
                        tbPostIndex.Text = dgList.CurrentRow.Cells["postindex"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["postindex"].ToString().Trim();
                        //17.09.2020
                        tbRKO_CODE.Text = dgList.CurrentRow.Cells["RKO_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["RKO_CODE"].ToString().Trim();
                        tbRKO_DEP_CODE.Text = dgList.CurrentRow.Cells["RKO_DEP_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["RKO_DEP_CODE"].ToString().Trim();
                        tbKO_CODE.Text = dgList.CurrentRow.Cells["KO_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_CODE"].ToString().Trim();
                        tbKO_INF1.Text = dgList.CurrentRow.Cells["KO_INF1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF1"].ToString().Trim();
                        tbKO_INF2.Text = dgList.CurrentRow.Cells["KO_INF2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF2"].ToString().Trim();
                        tbKO_INF3.Text = dgList.CurrentRow.Cells["KO_INF3"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF3"].ToString().Trim();
                        tbKO_INF4.Text = dgList.CurrentRow.Cells["KO_INF4"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF4"].ToString().Trim();
                        dtpKO_INF5.Text = dgList.CurrentRow.Cells["KO_INF5"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF5"].ToString().Trim();


                        /////08.01.2020
                        vubcl1();
                        //  dgList_CellClick(sender, e);
                        /////08.01.2020
                        /// 21.10.2019


                    }

                    /// 21.10.2019
                }
            }
            /// 21.10.2019
        }
        #endregion

        #region Кнопка изменить клиента
        protected override void btnModify_Click(object sender, EventArgs e)
        {


            /////23.10.2019

            if (dgList.CurrentCell == null)
                return;

            if (dgList.CurrentCell.RowIndex == -1)
                return;
            /////23.10.2019


            /// 21.10.2019
            string s1 = tbGroup1.Text;
            string s2 = tbGroup2.Text;
            string s3 = tbGroup3.Text;
            string s4 = tbGroup4.Text;
            int num;

            

                int rowIndex = dgList.CurrentCell.RowIndex;

            if (tbID.Text.Trim().Length > 20)
                MessageBox.Show("БИН не должен быть больше 20 знаков!");

            else
            if (tbPostIndex.Text.Trim().Length > 10)
                MessageBox.Show("Почтовый индекс не должен быть больше 10 символов!");

            else
            if (((tbGroup1.Text.Trim() != "") & (Int32.TryParse(s1, out num) != true))
               | ((tbGroup2.Text.Trim() != "") & (Int32.TryParse(s2, out num) != true))
               | ((tbGroup3.Text.Trim() != "") & (Int32.TryParse(s3, out num) != true))
               | ((tbGroup4.Text.Trim() != "") & (Int32.TryParse(s4, out num) != true))
                )
                MessageBox.Show("Отчётная группа должна быть числом!");
            else
            {
                /*
                //////////25.10.2019
                
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
                                

                    //Поиск клиента с таким же именем
                    int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
                    //Если имя клиента не пустое и такого имени не существует
                    if (tbName.Text != String.Empty && result == 0)
                    */
                //
                //////////25.10.2019
                if (tbName.Text != String.Empty)// && result == 0)


                {
                    /////22.10.2019

                    //Клиентская группа
                    if (cbClientGroup.SelectedValue != null)
                    {
                        num=1;
                    }
                    else
                    {
                        MessageBox.Show("Выберите клиентскую группу");
                        return;
                    }

                    

                    if (cbCity.SelectedValue != null)
                    {
                        num = 1;

                    }
                    else
                    {
                        MessageBox.Show("Выберите город");
                        return;

                    }

                    //////20.01.2020

                    ////22.01.2020
                    DialogResult result = MessageBox.Show(
                    " Продолжить операцию изменения? ",
                    "Сообщение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                    
                    if (result == DialogResult.No)
                        return;
                    ////
                    pechatpoisk();
                    ////

                    //////
                    //MessageBox.Show("111");
                    /*//DataSet cloneDataSet = new DataSet();
                
                    //cloneDataSet.Tables.Add((dgList.DataSource as DataTable).Copy());
                    //MessageBox.Show("count = " + cloneDataSet.Tables[0].Rows.Count);
                    //MessageBox.Show("count = " + dataSet.Tables[0].Rows.Count);
                    //dataSet.Clear();
                    //dataSet = cloneDataSet;
                    ////return;*/

                    //DataRow dataRow = dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex];
                    //dataRow[gridFieldName] = tbName.Text;
                    //dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                    //dataRow["lastupdate"] = DateTime.Now;
                    //dataRow["id_subfml"] = cbClientGroup.SelectedValue;
                    ////БИН
                    //dataRow["BIN"] = tbID.Text;
                    //dataRow["id_city"] = cbCity.SelectedValue;
                    ////Адрес
                    //dataRow["address"] = tbAddress.Text;
                    //dataRow["report_group_1"] = tbGroup1.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup1.Text);
                    //dataRow["report_group_2"] = tbGroup2.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup2.Text);
                    //dataRow["report_group_3"] = tbGroup3.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup3.Text);
                    //dataRow["report_group_4"] = tbGroup4.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup4.Text);
                    ////Почтовый индекс
                    //dataRow["postindex"] = tbPostIndex.Text;
                    ////17.09.2020
                    //dataRow["RKO_CODE"] = tbRKO_CODE.Text == string.Empty ? (Object)DBNull.Value : (tbRKO_CODE.Text);
                    //dataRow["RKO_DEP_CODE"] = tbRKO_DEP_CODE.Text == string.Empty ? (Object)DBNull.Value : (tbRKO_DEP_CODE.Text);
                    //dataRow["KO_CODE"] = tbKO_CODE.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_CODE.Text.Trim());
                    //dataRow["KO_INF1"] = tbKO_INF1.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF1.Text.Trim());
                    //dataRow["KO_INF2"] = tbKO_INF2.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF2.Text.Trim());
                    //dataRow["KO_INF3"] = tbKO_INF3.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF3.Text.Trim());
                    //dataRow["KO_INF4"] = tbKO_INF4.Text.Trim() == string.Empty ? (Object)DBNull.Value : (tbKO_INF4.Text.Trim());
                    //dataRow["KO_INF5"] = dtpKO_INF5.Text.Trim();

                   // DataSet d1 = dataBase.GetData9("");
                    string sql= "UPDATE t_g_client " +                        
                        " SET name = '"+ tbName.Text+
                        "', last_update_user =" + CurrentUser.CurrentUserId+
                        ", lastupdate=getdate(), id_subfml =" + cbClientGroup.SelectedValue+
                        ", BIN ='" + tbID.Text+
                        "', id_city  =" + cbCity.SelectedValue+
                        ", address = '" + tbAddress.Text+"'"+
                        (tbGroup1.Text == string.Empty ? (Object)DBNull.Value : ", report_group_1 =" + Convert.ToInt16(tbGroup1.Text)) +
                        (tbGroup2.Text == string.Empty ? (Object)DBNull.Value : ", report_group_2 =" + Convert.ToInt16(tbGroup2.Text)) +
                        (tbGroup3.Text == string.Empty ? (Object)DBNull.Value : ", report_group_3 =" + Convert.ToInt16(tbGroup3.Text)) +
                        (tbGroup4.Text == string.Empty ? (Object)DBNull.Value : ", report_group_4 =" + Convert.ToInt16(tbGroup4.Text)) +
                        ", postindex ='" + tbPostIndex.Text +
                        "', RKO_CODE ='" +(tbRKO_CODE.Text == string.Empty ? (Object)DBNull.Value : (tbRKO_CODE.Text)) +
                        "', RKO_DEP_CODE ='" + (tbRKO_DEP_CODE.Text == string.Empty ? (Object)DBNull.Value : (tbRKO_DEP_CODE.Text)) +
                        "', KO_CODE ='" + (tbKO_CODE.Text == string.Empty ? (Object)DBNull.Value : (tbKO_CODE.Text)) +
                        "', KO_INF1 ='" + tbKO_INF1.Text.Trim() +
                        "', KO_INF2 ='" + tbKO_INF2.Text.Trim() +
                        "', KO_INF3 ='" + tbKO_INF3.Text.Trim() +
                        "', KO_INF4 ='" + tbKO_INF4.Text.Trim() +
                        "', KO_INF5 ='" + dtpKO_INF5.Text.Trim() +
                       
                        "' WHERE id = "+ dgList.CurrentRow.Cells["id"].Value.ToString();
                    //////
                    System.Diagnostics.Debug.WriteLine("sql =" + sql);
                    //MessageBox.Show("sql = "+sql);
                    dataBase.GetData9(sql);
                    //return;

                    List <DataSet> dataset1 = new List<DataSet>();
                    dataset1.Add(dataSet);
                    dataset1.Add(pechat4);

                    dataset1.Add(clienttocDataSet);
                    dataset1.Add(enCashPointDataSetobsh);
                    dataset1.Add(accountDataSetobsh);
                    //11111
                   // MessageBox.Show(accountDataSetobsh.Tables[0].Columns["id_client"].ToString());

                    int u88 = 0;
                    if (flizm == 1)
                        u88 = 1;
                    //MessageBox.Show("client=" + Convert.ToInt32(dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"]).ToString());
                    dataBase.GetDataset11(dataset1, Convert.ToInt32(dgList.CurrentRow.Cells["id"].Value), u88);// dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"]), u88);

                    //dataBase.GetDataset11(dataset1, Convert.ToInt32(dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"]), u88);


                    //////20.01.2020

                    //dgList.ClearSelection();
                    dataSet = dataBase.GetData9("select * from t_g_client where deleted = 0 order by name");
                    dgList.DataSource = dataSet.Tables[0];
                    dgList.ClearSelection();

                    dgList.CurrentCell = dgList["name", rowIndex];



                    base.dgList_SelectionChanged(sender, e);
                    dgList.Refresh();
                    dgList.Update();

                    tbID.Text = dgList.CurrentRow.Cells["BIN"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["BIN"].ToString().Trim();
                    //Группа клиента
                    cbClientGroup.SelectedValue = dgList.CurrentRow.Cells["id_subfml"].Value; //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_subfml"];
                    //Город
                    cbCity.SelectedValue = dgList.CurrentRow.Cells["id_city"].Value; //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"];
                    //Адрес
                    tbAddress.Text = dgList.CurrentRow.Cells["address"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["address"].ToString().Trim();
                    //Группы
                    tbGroup1.Text = dgList.CurrentRow.Cells["report_group_1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_1"].ToString().Trim();
                    tbGroup2.Text = dgList.CurrentRow.Cells["report_group_2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_2"].ToString().Trim();
                    tbGroup3.Text = dgList.CurrentRow.Cells["report_group_3"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_3"].ToString().Trim();
                    tbGroup4.Text = dgList.CurrentRow.Cells["report_group_4"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["report_group_4"].ToString().Trim();
                    //Индекс
                    tbPostIndex.Text = dgList.CurrentRow.Cells["postindex"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["postindex"].ToString().Trim();
                    
                    //17.09.2020
                    tbRKO_CODE.Text = dgList.CurrentRow.Cells["RKO_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["RKO_CODE"].ToString().Trim();
                    tbRKO_DEP_CODE.Text = dgList.CurrentRow.Cells["RKO_DEP_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["RKO_DEP_CODE"].ToString().Trim();
                    tbKO_CODE.Text = dgList.CurrentRow.Cells["KO_CODE"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_CODE"].ToString().Trim();
                    tbKO_INF1.Text = dgList.CurrentRow.Cells["KO_INF1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF1"].ToString().Trim();
                    tbKO_INF2.Text = dgList.CurrentRow.Cells["KO_INF2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF2"].ToString().Trim();
                    tbKO_INF3.Text = dgList.CurrentRow.Cells["KO_INF3"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF3"].ToString().Trim();
                    tbKO_INF4.Text = dgList.CurrentRow.Cells["KO_INF4"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF4"].ToString().Trim();
                    dtpKO_INF5.Text = dgList.CurrentRow.Cells["KO_INF5"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["KO_INF5"].ToString().Trim();




                    /*
                    //Создаем новую строку
                    DataRow dataRow = dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex];
                    //dataRow["creation"] = DateTime.Now;
                    dataRow[gridFieldName] = tbName.Text;
                    dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                    dataRow["lastupdate"] = DateTime.Now;

                    //Клиентская группа
                    if (cbClientGroup.SelectedValue != null)
                    {
                        dataRow["id_subfml"] = cbClientGroup.SelectedValue;
                    }
                    else
                    {
                        MessageBox.Show("Выберите клиентскую группу");
                        return;
                    }

                    //БИН
                    dataRow["BIN"] = tbID.Text;

                    if (cbCity.SelectedValue != null)
                    {
                        dataRow["id_city"] = cbCity.SelectedValue;

                    }
                    else
                    {
                        MessageBox.Show("Выберите город");
                        return;

                    }

                    
                    //Адрес
                    dataRow["address"] = tbAddress.Text;
                    dataRow["report_group_1"] = tbGroup1.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup1.Text);
                    dataRow["report_group_2"] = tbGroup2.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup2.Text);
                    dataRow["report_group_3"] = tbGroup3.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup3.Text);
                    dataRow["report_group_4"] = tbGroup4.Text == string.Empty ? (Object)DBNull.Value : Convert.ToInt16(tbGroup3.Text);
                    //Почтовый индекс
                    dataRow["postindex"] = tbPostIndex.Text;
                    //Добавление строку в переменную
                    //dataSet.Tables[0].Rows.Add(dataRow);
                    //Обновление БД
                    dataBase.UpdateData(dataSet, "t_g_client");

                    */

                    /////22.10.2019

                    //Подготовка всех компонентов
                    AllConntrolsView();


                    /////08.01.2020
                    vubcl1();
                    
                    /////08.01.2020

                }
            }
        }
        #endregion

        #region Кнопка удаления клиента
        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show(
            //    "Удалить клиента?",
            //    "Сообщение",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Information,
            //    MessageBoxDefaultButton.Button1);

            //if (result == DialogResult.No)
            //    return;

            /////23.10.2019

            if (dgList.CurrentCell == null)
                return;

            if (dgList.CurrentCell.RowIndex == -1)
                return;
            /////23.10.2019


            ////22.01.2020
            DialogResult result = MessageBox.Show(
" Продолжить операцию удаления? "

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
            ////22.01.2020

            //Выборка из БД связей клиента с кассовыми центрами и точками инкассации
            ///clienttocDataSet = dataBase.GetData("t_g_clienttocc");

            //Индекс клиента
            int rowIndex = dgList.CurrentCell.RowIndex;
            
            //Идентификатор клиента
            Int64 clientId = Convert.ToInt64(dataSet.Tables[0].Rows[rowIndex]["id"]);

            DataSet check = dataBase.GetData9("  select* from t_g_counting_content t1 left join t_g_account t2 on t1.id_account=t2.id where id_client="+ clientId.ToString());

            if(check.Tables[0].Rows.Count>0)
            {
                MessageBox.Show("Этого клиента не возможно удалить, поськолку у этого клиента есть еще не завершенные сумки и пересчеты");
                return;
            }

            DataRow clientRow = dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == clientId).First<DataRow>();

            //////22.10.2019

            
            if ((dgList.CurrentCell != null) & (dgList.CurrentCell.RowIndex > -1))
            {
               
                dataBase.Zapros("Update t_g_client set deleted=1, lastupdate='"+ DateTime.Now .ToString()+ "' WHERE id = '{0}'", dgList.CurrentRow.Cells["id"].Value.ToString().Trim());// dataSet.Tables[0].Rows[dgList.CurrentCell.RowIndex]["id"].ToString());

                dgList.ClearSelection();
                dataSet = dataBase.GetData("t_g_client", "deleted", "0");
                dgList.DataSource = dataSet.Tables[0];

                /////08.01.2020
                vubcl1();

                /////08.01.2020
            }


            /*
            clientRow["deleted"] = true;
            clientRow["lastupdate"] = DateTime.Now;
            */
            //////22.10.2019

            //Список связей на удаление для данного клиента
            //List<Int64> deleteRowsList = clienttocDataSet.Tables[0].AsEnumerable().
            //    Where(r => r.Field<Int64>("id_client") == clientId).
            //    Select(r => r.Field<Int64>("id")).ToList<Int64>();

            //Удаление всех записей из списка
            /*foreach (int row in deleteRowsList)
            {
                for (int i = 0; i < clienttocDataSet.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt64(clienttocDataSet.Tables[0].Rows[i]["id"]) == row)
                    {
                        clienttocDataSet.Tables[0].Rows[i].Delete();
                    }
                }
            }*/

            //////22.10.2019
            /*

            //Обновление БД
            //dataBase.UpdateData(clienttocDataSet);

            //Удаление клиента из списка
            //dataSet.Tables[0].Rows[rowIndex].Delete();

            //Обновление в БД
            dataBase.UpdateData(dataSet, "t_g_client");
            //Очистка названия
            tbName.Clear();
            tbID.Clear();
            tbAddress.Clear();
            tbGroup1.Clear();
            tbGroup2.Clear();
            tbGroup3.Clear();
            tbGroup4.Clear();
            tbPostIndex.Clear();
            cbCity.SelectedIndex = -1;
            cbClientGroup.SelectedIndex = -1;
            ClearDataGrid(dgCashCentre);
            ClearDataGrid(dgEnCashPoint);
            ClearDataGrid(dgAccounts);
            clbCashCentre.ClearSelected();
            dataSet = dataBase.GetData("t_g_client", "deleted", "0");
            dgList.DataSource = dataSet.Tables[0];

            */
            //////22.10.2019

        }
        #endregion

        #region Обработка нажатия клавиш на основном гриде
        protected override void dgList_KeyUp(object sender, KeyEventArgs e)
        {
            //Если нажата клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                //Если выделена хотя бы одна ячейка
                if (dgList.SelectedCells.Count > 0)
                {
                    //Выполняем тоже действие, что и по двойному клику мыши
                    dgList_CellDoubleClick(this, new DataGridViewCellEventArgs(dgList.SelectedCells[0].ColumnIndex, dgList.SelectedCells[0].RowIndex));

                }
            }
        }
        #endregion

        #endregion

        #region Манипуляции с кассовым центром

        #region Выделение нужных кассовых центров из БД
        private void SetCheckedListBox(DataSet dataSet, CheckedListBox checkedListBox)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {

                for (int i = 0; i < checkedListBox.Items.Count; i++)

                {

                    if (((DataRowView)checkedListBox.Items[i]).Row["id"].ToString() == row["id"].ToString())
                    {
                        checkedListBox.SetItemChecked(i, true);
                        break;
                    }

                }
            }
        }
        #endregion

        #region Убрать выделение с кассовых центров
        private void UncheckCheckBox(CheckedListBox checkedListBox)
        {
            foreach (int checkedIndex in checkedListBox.CheckedIndices)
            {
                checkedListBox.SetItemChecked(checkedIndex, false);
            }
        }
        #endregion

        #region Выбор ячейки кассового центра в гриде 
        private void dgCashCentre_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                

                //////15.01.2020


                if (enCashPointDataSetobsh != null)
                {

                    //Заполнение данными переменную точек инкассации
                    DataRow[] currentRows = ((DataTable)enCashPointDataSetobsh.Tables[0]).Select(" id_clienttocc in (" + String.Join(",", clienttocDataSet.Tables[0].AsEnumerable().
              Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"])).
              Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
              ToList<string>()).ToString() + ")");

                    enCashPointDataSet = dataBase.GetData("t_g_encashpoint", "id_clienttocc", "-1");
                    foreach (DataRow dr in currentRows)
                    {
                        object[] row = dr.ItemArray;
                        enCashPointDataSet.Tables[0].Rows.Add(row);
                    }

                    if (accountDataSetobsh != null)
                    {
                        DataRow[] currentRows1 = ((DataTable)accountDataSetobsh.Tables[0]).Select(" id_clienttocc in (" + String.Join(",",
                      clienttocDataSet.Tables[0].AsEnumerable().
                      Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"])).
                      Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                      ToList<string>()).ToString() + ")");

                        accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc", "-1");
                        foreach (DataRow dr in currentRows1)
                        {
                            object[] row = dr.ItemArray;
                            accountDataSet.Tables[0].Rows.Add(row);
                        }

                        /*
                        //Заполнение данными переменную точек инкассации
                        enCashPointDataSet = dataBase.GetData("t_g_encashpoint", "id_clienttocc",
                      clienttocDataSet.Tables[0].AsEnumerable().
                      Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"])).
                      Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                      ToList<string>());

                        //Заполнение данными переменную счетов
                        accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc",
                           clienttocDataSet.Tables[0].AsEnumerable().
                          Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[e.RowIndex]["id"])).
                          Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                          ToList<string>());
                        */
                    }

                }
                //////15.01.2020

                //Очистка грида счетов
                ClearDataGrid(dgAccounts);
                //Очистка грида точек инкассации
                ClearDataGrid(dgEnCashPoint);
                //Заполнение грида точек инкассации
                EncashPoint_View();
                //Заполнение грида счетов
                Account_View();
            }
        }
        #endregion

        #region Проверка выделенных кассовых центров
        private void clbCashCentre_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Элемент в гриде клиентов
            int rowIndex = dgList.CurrentCell.RowIndex;

            //////14.01.2020
            if (flizmtocc == 0)
            {
                if (flvub == 0)
                    clienttocDataSet = dataBase.GetData("t_g_clienttocc", "id_client", "-1");

                else

                    //Выбор всех кассовых центров для данного клиента
                    clienttocDataSet = dataBase.GetData("t_g_clienttocc", "id_client", dataSet.Tables[0].Rows[rowIndex]["id"].ToString());
            }
            //////14.01.2020


            //Если выбираем кассовый центр
            if (e.NewValue == CheckState.Checked)
            {

                //////15.01.2020
                long imin1 = 0;

                ////нахождение минимума

                
                    if (clienttocDataSet.Tables[0].Rows.Count > 0)
                {

                    for (int i1 = 0; i1 < clienttocDataSet.Tables[0].Rows.Count; i1++)
                    {
                        if ((Convert.ToInt64(clienttocDataSet.Tables[0].Rows[i1]["id"].ToString())<0)& (imin1>Convert.ToInt64(clienttocDataSet.Tables[0].Rows[i1]["id"].ToString())))
                        imin1 = Convert.ToInt64(clienttocDataSet.Tables[0].Rows[i1]["id"].ToString());
                    }
                
                }

                //////15.01.2020

                    //Создаем строку для кассового центра
                    DataRow dataRow = clienttocDataSet.Tables[0].NewRow();




                //////14.01.2020
                dataRow["id"] = imin1 - 1;
                if (flvub == 0)
                {
                    dataRow["id_client"] = -1;
                   
                }
                else
                    //////14.01.2020
                    dataRow["id_client"] = dataSet.Tables[0].Rows[rowIndex]["id"];
                dataRow["id_cashcentre"] = ((DataRowView)clbCashCentre.Items[e.Index]).Row["id"];
                dataRow["lastupdate"] = DateTime.Now;
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                //Добавляем в переменную данные
                clienttocDataSet.Tables[0].Rows.Add(dataRow);

                /////21.01.2020
                clienttocDataSet.Tables[0].AcceptChanges();
                /////21.01.2020

                //////14.01.2020
                flizmtocc = 1;
                //Обновляем данные в БД
                // dataBase.UpdateData(clienttocDataSet, "t_g_clienttocc");
                //Обновление данных из БД
                // clienttocDataSet = dataBase.GetData("t_g_clienttocc", "id_client", dataSet.Tables[0].Rows[rowIndex]["id"].ToString());
                //////14.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020

            }
            else if (e.NewValue == CheckState.Unchecked) //Если убираем выделение с кассового центра
            {

                /////15.01.2020
                Int64 idRemove = 0;
                if (flvub == 0)
                     idRemove = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(clbCashCentre.SelectedValue) &&
                     r.Field<Int64>("id_client") == -1)
                    .Select(r => r.Field<Int64>("id")).First();
                else
                

                //Находим идентификатор записи для удаления из БД
               // Int64

                        /////15.01.2020
                     
                  
                    
                        idRemove = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(clbCashCentre.SelectedValue) &&
                     r.Field<Int64>("id_client") == Convert.ToInt64(dataSet.Tables[0].Rows[rowIndex]["id"].ToString()))
                    .Select(r => r.Field<Int64>("id")).First();


                /////16.01.2020
           
                   for (int i2 = accountDataSetobsh.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
          
                {

                     if ((Convert.ToInt64(accountDataSetobsh.Tables[0].Rows[i2]["id_clienttocc"]) == idRemove) )
                    
                    {
                  
                        accountDataSetobsh.Tables[0].Rows[i2].Delete();


                    }

                }
                accountDataSetobsh.Tables[0].AcceptChanges();

                for (int i2 = enCashPointDataSetobsh.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if (Convert.ToInt64(enCashPointDataSetobsh.Tables[0].Rows[i2]["id_clienttocc"]) == idRemove)
                    {
                        enCashPointDataSetobsh.Tables[0].Rows[i2].Delete();
                        

                    }

                }
                enCashPointDataSetobsh.Tables[0].AcceptChanges();

                for (int i2 = accountDataSet.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if ((Convert.ToInt64(accountDataSet.Tables[0].Rows[i2]["id_clienttocc"]) == idRemove))
                    {
                        accountDataSet.Tables[0].Rows[i2].Delete();


                    }

                }

                accountDataSet.Tables[0].AcceptChanges();

                for (int i2 = enCashPointDataSet.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if (Convert.ToInt64(enCashPointDataSet.Tables[0].Rows[i2]["id_clienttocc"]) == idRemove)
                    {
                        enCashPointDataSet.Tables[0].Rows[i2].Delete();


                    }

                }

                enCashPointDataSet.Tables[0].AcceptChanges();
                ////20.01.2020
                /*
                for (int i2 = clienttocDataSet.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if (Convert.ToInt64(clienttocDataSet.Tables[0].Rows[i2]["id"]) == idRemove)
                    {
                        clienttocDataSet.Tables[0].Rows[i2].Delete();


                    }

                }
                */
                clienttocDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == idRemove).FirstOrDefault<DataRow>().Delete();

                clienttocDataSet.Tables[0].AcceptChanges();
                ////20.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020

                /*
                if (dataBase.GetData("t_g_encashpoint", "id_clienttocc", idRemove.ToString()).Tables[0].Rows.Count == 0 &&
                    dataBase.GetData("t_g_account", "id_clienttocc",idRemove.ToString()).Tables[0].Rows.Count == 0)
                {
                    //Удаляем данные из набора
                    clienttocDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") == idRemove).FirstOrDefault<DataRow>().Delete();
                    //Обновляем данные в БД
                    //clienttocDataSet.AcceptChanges();

                    //////14.01.2020
                    //dataBase.UpdateData(clienttocDataSet, "t_g_clienttocc");
                    //////14.01.2020

                    btnAdd.Enabled = true;
                    btnModify.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Удалите связанные точки инкассации и счета");
                    e.NewValue = CheckState.Checked;
                }
                */
                /////16.01.2020

            }

            //Обновляем данные из БД

            ////20.01.2020
            cashCentreDataSet = dataBase.GetData("t_g_cashcentre", "id",
                clienttocDataSet.Tables[0].AsEnumerable().Select(r => r.Field<Int64>("id_cashcentre").ToString()).ToList<string>());
            ////20.01.2020

            //Очищаем грид с кассовыми центрами
            ClearDataGrid(dgCashCentre);
            //Очищаем грид с точками инкассации
            ClearDataGrid(dgEnCashPoint);
            //Очищаем грид со счетами
            ClearDataGrid(dgAccounts);

            //Добавляем столбец наименования кассового центра
            dgCashCentre.Columns.Add("branch_name", "Кассовый центр");
            dgCashCentre.Columns["branch_name"].DataPropertyName = "branch_name";
            dgCashCentre.Columns["branch_name"].Visible = true;

            //Если данные по кассовым центрам есть
            if (cashCentreDataSet.Tables.Count > 0)
            {

                /*
                /////16.01.2020
                dataview1 = cashCentreDataSet.Tables[0].DefaultView;
                dataview1.Sort = "branch_name ASC, branch_name DESC";
                dgCashCentre.DataSource = dataview1;


                //  dgCashCentre.DataSource = cashCentreDataSet.Tables[0];
                /////16.01.2020
                */
                //Добавляем их на грид

                /////21.01.2020
                 dgCashCentre.DataSource = cashCentreDataSet.Tables[0];
                /*
                bindingSource1 = new BindingSource();
                bindingSource1.DataSource = cashCentreDataSet.Tables[0];
                dgCashCentre.DataSource = bindingSource1;
                */
                /////21.01.2020

                /////08.01.2020
                dgCashCentre.AutoResizeColumns();
                /////08.01.2020
            }


        }
        #endregion

        #endregion

        #region Манипуляции с точкой инкасации

        #region Выбор точки инкассации двойным кликом мыши
        private void dgEnCashPoint_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /////23.10.2019
            if (e.RowIndex > -1)
            {
                /////23.10.2019
                //Заполняем поле имени точки инкассации
                tbEncashPointName.Text = enCashPointDataSet.Tables[0].Rows[e.RowIndex]["name"].ToString();
                //Заполняем поле адресса точки инкассации
                tbPointAdrress.Text = enCashPointDataSet.Tables[0].Rows[e.RowIndex]["Address"].ToString();
                //17.09.2020
                tbINF1.Text= enCashPointDataSet.Tables[0].Rows[e.RowIndex]["INF1"].ToString();
                tbINF2.Text = enCashPointDataSet.Tables[0].Rows[e.RowIndex]["INF2"].ToString();


                /////23.10.2019
            }
                    /////23.10.2019

                }
                #endregion

                #region Выбор ячейки точки инкассации
                private void dgEnCashPoint_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex > 0)
            {

                ////15.01.2020

                if (accountDataSetobsh != null)
                {
                    string s2 = enCashPointDataSet.Tables[0].Rows[e.RowIndex]["id"].ToString();
                    DataRow[] currentRows1 = ((DataTable)accountDataSetobsh.Tables[0]).Select(" id_encashpoint in (" + s2.ToString() + ")");

                    accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc", "-1");
                    foreach (DataRow dr in currentRows1)
                    {
                        object[] row = dr.ItemArray;
                        accountDataSet.Tables[0].Rows.Add(row);
                    }

                    //Заполнение данными переменную со счетами
                    //    accountDataSet = dataBase.GetData("t_g_account", "id_encashpoint",
                    //    enCashPointDataSet.Tables[0].Rows[e.RowIndex]["id"].ToString());
                }
                    ////15.01.2020

                    //Заполнение данными переменную со валютой
                    currencyDataSet = dataBase.GetData("t_g_currency");

                //Очистка грида со счетами
                ClearDataGrid(dgAccounts);
                //Заполнение грида счетов
                Account_View();
            }
            
        }
        #endregion

        #region Кнопка добавления для точки инкассации
        private void btnAddEncashPoint_Click(object sender, EventArgs e)


             
        {

            /////23.10.2019

            if (dgList.CurrentCell == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgList.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgCashCentre.CurrentCell == null)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }

            if (dgCashCentre.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }

            /////23.10.2019

            //Индекс клиента
            int rowIndexClient = dgList.CurrentCell.RowIndex;
            //Индекс кассового центра
            int rowIndexCashCentre = dgCashCentre.CurrentCell.RowIndex;
            //Если название введено точки инкассации
            if (!String.IsNullOrEmpty(tbEncashPointName.Text))
            {

                //////15.01.2020
                long imin1 = 0;

                ////нахождение минимума

                if (enCashPointDataSetobsh != null)
                if (enCashPointDataSetobsh.Tables[0].Rows.Count > 0)
                {

                    for (int i1 = 0; i1 < enCashPointDataSetobsh.Tables[0].Rows.Count; i1++)
                    {
                        if ((Convert.ToInt64(enCashPointDataSetobsh.Tables[0].Rows[i1]["id"].ToString())<0) & (imin1> Convert.ToInt64(enCashPointDataSetobsh.Tables[0].Rows[i1]["id"].ToString())))
                        imin1 = Convert.ToInt64(enCashPointDataSetobsh.Tables[0].Rows[i1]["id"].ToString());
                    }

                }

                //////15.01.2020

                //Создаем строку
                DataRow dataRow = enCashPointDataSet.Tables[0].NewRow();

                ////15.01.2020
                
                dataRow["id"] = imin1 - 1;

                if (flvub == 0)
                    dataRow["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == -1)
                    .Select(r => r.Field<Int64>("id")).First();
                else
                ////15.01.2020

                //Выбираем идентификатор для связи с клиентом
                dataRow["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value.ToString()
                         //dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()
                         ))
                    .Select(r => r.Field<Int64>("id")).First();
                dataRow["creation"] = DateTime.Now;
                dataRow["lastupdate"] = DateTime.Now;
                dataRow["name"] = tbEncashPointName.Text;
                dataRow["address"] = tbPointAdrress.Text;
                //17.09.2020
                dataRow["INF1"] = tbINF1.Text.Trim();
                dataRow["INF2"] = tbINF2.Text.Trim();

                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                //Добавляем точку инкассации 

                //////16.01.2020
               // enCashPointDataSet.Tables[0].Rows.Add(dataRow);
                //////16.01.2020

                /////15.01.2020
                DataRow dataRow1 = enCashPointDataSetobsh.Tables[0].NewRow();

                

                dataRow1["id"] = imin1 - 1;

                if (flvub == 0)
                    dataRow1["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == -1)
                    .Select(r => r.Field<Int64>("id")).First();
                else
                   

                    //Выбираем идентификатор для связи с клиентом
                    dataRow1["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value
                             //dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()
                             ))
                        .Select(r => r.Field<Int64>("id")).First();
                dataRow1["creation"] = DateTime.Now;
                dataRow1["lastupdate"] = DateTime.Now;
                dataRow1["name"] = tbEncashPointName.Text;
                dataRow1["address"] = tbPointAdrress.Text;
                //17.09.2020
                dataRow1["INF1"] = tbINF1.Text.Trim();
                dataRow1["INF2"] = tbINF2.Text.Trim();

                dataRow1["last_update_user"] = CurrentUser.CurrentUserId;
                
                
                enCashPointDataSetobsh.Tables[0].Rows.Add(dataRow1);

                //////16.01.2020
                enCashPointDataSet.Tables[0].Rows.Add(dataRow);
                //////16.01.2020

                /////15.01.2020

                /////14.01.2020
                //dataBase.UpdateData(enCashPointDataSet, "t_g_encashpoint");
                /////14.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020


                ////23.10.2019
                // tbEncashPointName.Clear();
                // tbPointAdrress.Clear();

                dgEnCashPoint.CurrentCell = dgEnCashPoint["name", enCashPointDataSet.Tables[0].Rows.Count - 1];



                DgEnCashPoint_SelectionChanged(sender, e);
                dgEnCashPoint.Refresh();
                dgEnCashPoint.Update();
                ////23.10.2019

            }
            else
            {
                MessageBox.Show("Введите название точки инкассации");
            }
        }
        #endregion

        #region Кнопка изменения точки инкассации
        private void btnModifyPoint_Click(object sender, EventArgs e)
        {

            /////23.10.2019

            if (dgList.CurrentCell == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgList.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgCashCentre.CurrentCell == null)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }

            if (dgCashCentre.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }
            if (dgEnCashPoint.CurrentCell == null)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }

            if (dgEnCashPoint.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }
            /////23.10.2019

            //Индекс клиента
            int rowIndexClient = dgList.CurrentCell.RowIndex;
            //Индекс кассового центра
            int rowIndexCashCentre = dgCashCentre.CurrentCell.RowIndex;
            //Индекс точки инкассации
            int rowIndexEncashPoint = dgEnCashPoint.CurrentCell.RowIndex;
            //Если выбрана точка инкассации
            if (!String.IsNullOrEmpty(tbEncashPointName.Text))
            {

                //////16.01.2020
               
                for (int i2 = 0; i2 < enCashPointDataSetobsh.Tables[0].Rows.Count; i2++)
                {

                    if (enCashPointDataSetobsh.Tables[0].Rows[i2]["id"].ToString() == enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id"].ToString())
                    {
                        

                        enCashPointDataSetobsh.Tables[0].Rows[i2]["lastupdate"] = DateTime.Now;
                        enCashPointDataSetobsh.Tables[0].Rows[i2]["name"] = tbEncashPointName.Text;
                        enCashPointDataSetobsh.Tables[0].Rows[i2]["address"] = tbPointAdrress.Text;
                        //17.09.2020
                        enCashPointDataSetobsh.Tables[0].Rows[i2]["INF1"] = tbINF1.Text.Trim();
                        enCashPointDataSetobsh.Tables[0].Rows[i2]["INF2"] = tbINF2.Text.Trim();

                        enCashPointDataSetobsh.Tables[0].Rows[i2]["last_update_user"] = CurrentUser.CurrentUserId;

                        
                    }
                }
                /*
                ////15.01.2020
                if (flvub == 0)
                    enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id_clienttocc"] =
                                        clienttocDataSet.Tables[0].AsEnumerable().
                                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                                         r.Field<Int64>("id_client") == -1)
                                        .Select(r => r.Field<Int64>("id")).First();
                else
                    ////15.01.2020
                    
                    //Находим ячейку с точкой инкассации
                    enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id_clienttocc"] =
                    clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == Convert.ToInt64(dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()))
                    .Select(r => r.Field<Int64>("id")).First();
                //Заполняем поля
                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["creation"] = enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["creation"];
                */
                //////16.01.2020


                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["lastupdate"] = DateTime.Now;
                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["name"] = tbEncashPointName.Text;
                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["address"] = tbPointAdrress.Text;
                //17.09.2020
                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["INF1"] = tbINF1.Text.Trim();
                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["INF2"] = tbINF2.Text.Trim();

                enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["last_update_user"] = CurrentUser.CurrentUserId;


                //////16.01.2020
                /*
                //////15.01.2020

                if (flvub == 0)
                    enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["id_clienttocc"] =
                                        clienttocDataSet.Tables[0].AsEnumerable().
                                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                                         r.Field<Int64>("id_client") == -1)
                                        .Select(r => r.Field<Int64>("id")).First();
                else


                    //Находим ячейку с точкой инкассации
                    enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["id_clienttocc"] =
                    clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == Convert.ToInt64(dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()))
                    .Select(r => r.Field<Int64>("id")).First();
                //Заполняем поля
                enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["creation"] = enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["creation"];
                 enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["lastupdate"] = DateTime.Now;
                enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["name"] = tbEncashPointName.Text;
                enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["address"] = tbPointAdrress.Text;
                enCashPointDataSetobsh.Tables[0].Rows[rowIndexEncashPoint]["last_update_user"] = CurrentUser.CurrentUserId;
                 */


                //////16.01.2020

                //////15.01.2020

                /////14.01.2020
                // dataBase.UpdateData(enCashPointDataSet, "t_g_encashpoint");
                /////14.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020


                ////23.10.2019
                // tbEncashPointName.Clear();
                // tbPointAdrress.Clear();

                DgEnCashPoint_SelectionChanged(sender, e);
                dgEnCashPoint.Refresh();
                dgEnCashPoint.Update();

                ////23.10.2019

            }
            else
            {
                MessageBox.Show("Введите название точки инкассации");
            }
        }
        #endregion

        #region Кнопка удаления точки инкассации
        private void btnDeletePoint_Click(object sender, EventArgs e)
        {

            ////23.10.2019

            if (dgAccounts.Rows.Count > 0)
            {
                MessageBox.Show("Удалите счета для точки инкассации!");
                return;
            }

            ////23.10.2019


                if (dgEnCashPoint.SelectedCells.Count > 0)
            {
                //Индекс точки инкассации
                int rowIndex = dgEnCashPoint.CurrentCell.RowIndex;

                ////23.10.2019
                // dgEnCashPoint.ClearSelection();
                ////23.10.2019


                //////16.01.2020

                for (int i2 = accountDataSetobsh.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if ((accountDataSetobsh.Tables[0].Rows[i2]["id_clienttocc"].ToString() == enCashPointDataSet.Tables[0].Rows[rowIndex]["id_clienttocc"].ToString()) & (accountDataSetobsh.Tables[0].Rows[i2]["id_encashpoint"].ToString() == enCashPointDataSet.Tables[0].Rows[rowIndex]["id"].ToString()))
                    {
                        accountDataSetobsh.Tables[0].Rows[i2].Delete();
                       

                    }

                }

                //////20.01.2020
                accountDataSetobsh.Tables[0].AcceptChanges();
                //////20.01.2020

                for (int i2 = enCashPointDataSetobsh.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if (enCashPointDataSetobsh.Tables[0].Rows[i2]["id"].ToString() == enCashPointDataSet.Tables[0].Rows[rowIndex]["id"].ToString())
                    {
                        enCashPointDataSetobsh.Tables[0].Rows[i2].Delete();
                        break;

                    }

                }

                //////20.01.2020
                enCashPointDataSetobsh.Tables[0].AcceptChanges();
                //////20.01.2020
                
                //////16.01.2020


                enCashPointDataSet.Tables[0].Rows[rowIndex].Delete();

                //////20.01.2020
                enCashPointDataSet.Tables[0].AcceptChanges();
                //////20.01.2020

                /////
                //enCashPointDataSet.AcceptChanges();
                /////

                /////14.01.2020
                //dataBase.UpdateData(enCashPointDataSet, "t_g_encashpoint");
                /////14.01.2020


                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020


                ////23.10.2019

                if (dgEnCashPoint.Rows.Count == 0)
                {
                   tbEncashPointName.Clear();
                     tbPointAdrress.Clear();
                }



                tbAccountName.Clear();


                DgEnCashPoint_SelectionChanged(sender, e);
                dgEnCashPoint.Refresh();
                dgEnCashPoint.Update();

                ////23.10.2019

            }
            else
            {
                MessageBox.Show("Выберите точку инкассации");
            }
        }
        #endregion

        #region Обработка нажатия клавиш на кнопке добавления
        private void btnAddPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Если нажата кнопка Enter
            if (e.KeyChar == (char)Keys.Return)
            {
                //Выполняем обработчик нажатия кнопки мыши 
                btnAddPoint.PerformClick();
                //Помечаем событие обработанным
                e.Handled = true;
                //Устанавливаем фокус на номере счета
                tbAccountName.Focus();
            }
        }
        #endregion

        #region Подготовка и заполнение данными грид с точками инкассации
        private void EncashPoint_View()
        {

            /////23.10.2019
            //dgEnCashPoint.ClearSelection();
            dgEnCashPoint.AutoGenerateColumns = false;
            //dgEnCashPoint.ColumnHeadersHeight = 70;
            //dgEnCashPoint.RowHeight = 40;

            dgEnCashPoint.DataSource = null;
            dgEnCashPoint.Rows.Clear();
            dgEnCashPoint.Columns.Clear();
            //dgEnCashPoint.Size = Size.Width = 400;
            /////23.10.2019

            //Добавление столбца с наименование
            dgEnCashPoint.Columns.Add("name", "Точка инкассации");
            dgEnCashPoint.Columns["name"].DataPropertyName = "name";
            dgEnCashPoint.Columns["name"].Width = 200;
            dgEnCashPoint.Columns["name"].Visible = true;
            dgEnCashPoint.Columns["name"].SortMode= DataGridViewColumnSortMode.NotSortable;

            //Добавление столбца с адресом
            dgEnCashPoint.Columns.Add("address", "Адрес точки");
            dgEnCashPoint.Columns["address"].DataPropertyName = "address";
            dgEnCashPoint.Columns["address"].Width = 200;
            dgEnCashPoint.Columns["address"].Visible = true;
            dgEnCashPoint.Columns["address"].SortMode = DataGridViewColumnSortMode.NotSortable;
            

            //17.09.2020
            //Добавление столбца с INF1
            dgEnCashPoint.Columns.Add("INF1", "INF1");
            dgEnCashPoint.Columns["INF1"].DataPropertyName = "INF1";
            dgEnCashPoint.Columns["INF1"].Width = 100;
            dgEnCashPoint.Columns["INF1"].Visible = true;
            dgEnCashPoint.Columns["INF1"].SortMode = DataGridViewColumnSortMode.NotSortable;


            //Добавление столбца с INF2
            dgEnCashPoint.Columns.Add("INF2", "INF2");
            dgEnCashPoint.Columns["INF2"].DataPropertyName = "INF2";
            dgEnCashPoint.Columns["INF2"].Width = 100;
            dgEnCashPoint.Columns["INF2"].Visible = true;
            dgEnCashPoint.Columns["INF2"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgEnCashPoint.Columns.Add("id", "id");
            dgEnCashPoint.Columns["id"].DataPropertyName = "id";            
            dgEnCashPoint.Columns["id"].Visible = false;

            DataGridViewButtonColumn dgAD = new DataGridViewButtonColumn();
            dgAD.Visible = true;
            dgAD.Text = ". . .";
            dgAD.Name = "pechat";
            dgEnCashPoint.Columns.Add(dgAD);
            dgEnCashPoint.Columns["pechat"].HeaderText = "Пломба";


            /////15.01.2020
            if (enCashPointDataSet!=null)
            /////15.01.2020
            //Если данные есть заполняем грид
                if (enCashPointDataSet.Tables.Count > 0 && !String.IsNullOrEmpty(enCashPointDataSet.DataSetName))
                dgEnCashPoint.DataSource = enCashPointDataSet.Tables[0];

            /////08.01.2020
            //dgEnCashPoint.AutoResizeColumns();
            /////08.01.2020
            
            //Убираем выделение 
            dgEnCashPoint.ClearSelection();

        }
        #endregion

        #region Навигация по кнопкам Enter точек инкассации

        #region Обработчик нажатия клавиш в поле имени точки инкассации        
        private void tbEncashPointName_KeyUp(object sender, KeyEventArgs e)
        {
            //Если нажата клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                //Фокус на поле ввода адреса точки инкассации
                tbPointAdrress.Focus();
            }
        }
        #endregion

        #region Обработчик нажатия клавиш в поле адреса точки инкассации
        private void tbPointAdrress_KeyUp(object sender, KeyEventArgs e)
        {
            //Если нажата клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                //Фокус на кнопку добавления точки инкассации
                btnAddPoint.Focus();
            }
        }
        #endregion

        #endregion
        
        #region Обработчик на добавление точки инкассации
        private void dgEnCashPoint_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //Снять выделение с грида
            dgEnCashPoint.ClearSelection();
            //Выделить добавленную строку
            dgEnCashPoint.Rows[e.RowIndex].Selected = true;
            //Установить фокус на поле ввода номера счета
            tbAccountName.Focus();
        }
        #endregion

        #endregion

        #region Манипуляции со счетом

        #region Выбор счета двойным кликом мыши
        private void dgAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //Заполняем поле номера счета
                tbAccountName.Text = accountDataSet.Tables[0].Rows[e.RowIndex]["name"].ToString();
                //Выбор валюты счета
                cbAccountCurrency.SelectedValue = accountDataSet.Tables[0].Rows[e.RowIndex]["id_currency"];
            }
        }
        #endregion

        #region Кнопка добавления счета
        private void btnAddAccount_Click(object sender, EventArgs e)
        {

            /////23.10.2019

            if (dgList.CurrentCell == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgList.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgCashCentre.CurrentCell == null)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }

            if (dgCashCentre.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }
            if (dgEnCashPoint.CurrentCell == null)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }

            if (dgEnCashPoint.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }
            /////23.10.2019
            
            //Индекс клиента
            int rowIndexClient = dgList.CurrentCell.RowIndex;
            //Индекс кассового центра
            int rowIndexCashCentre = dgCashCentre.CurrentCell.RowIndex;
            //Индекс точки инкассации
            int rowIndexEncashPoint = dgEnCashPoint.CurrentCell.RowIndex;

            //Если номер счета введен
            if (!String.IsNullOrEmpty(tbAccountName.Text))
            {

                //////16.01.2020
                long imin1 = 0;

                ////нахождение минимума

                if (accountDataSetobsh != null)
                    if (accountDataSetobsh.Tables[0].Rows.Count > 0)
                    {

                        for (int i1 = 0; i1 < accountDataSetobsh.Tables[0].Rows.Count; i1++)
                        {
                            if ((Convert.ToInt64(accountDataSetobsh.Tables[0].Rows[i1]["id"].ToString()) < 0) & (imin1 > Convert.ToInt64(accountDataSetobsh.Tables[0].Rows[i1]["id"].ToString())))
                                imin1 = Convert.ToInt64(accountDataSetobsh.Tables[0].Rows[i1]["id"].ToString());
                        }

                    }

                //////16.01.2020


                //Добавляем новую строку
                DataRow dataRow = accountDataSet.Tables[0].NewRow();
                dataRow["id_encashpoint"] = enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id"];

                //////16.01.2020
                dataRow["id"] = imin1 - 1;
                //////16.01.2020

                ////15.01.2020
                if (flvub == 0)
                {

                    dataRow["id_client"] = -1;
                    //Выбираем идентификатор для связи с клиентом
                    dataRow["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == -1)
                        .Select(r => r.Field<Int64>("id")).First();

                }
                else
                {
                    ////15.01.2020
                    //22222
                    dataRow["id_client"] = dgList.CurrentRow.Cells["id"].Value;//dataSet.Tables[0].Rows[rowIndexClient]["id"];
                                                                               // MessageBox.Show("id_client=" + dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString());
                                                                               //Выбираем идентификатор для связи с клиентом
                    dataRow["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value.ToString()))//dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()))
                    .Select(r => r.Field<Int64>("id")).First();

                ////15.01.2020
                }
                ////15.01.2020

                dataRow["creation"] = DateTime.Now;
                dataRow["lastupdate"] = DateTime.Now;
                dataRow["name"] = tbAccountName.Text;
                dataRow["id_currency"] = cbAccountCurrency.SelectedValue;
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;

                //Добавляем строку в набор

                //////16.01.2020
                //accountDataSet.Tables[0].Rows.Add(dataRow);
                //////16.01.2020

                //////15.01.2020


                DataRow dataRow1 = accountDataSetobsh.Tables[0].NewRow();
                dataRow1["id_encashpoint"] = enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id"];

                //////16.01.2020
                dataRow1["id"] = imin1 - 1;
                //////16.01.2020
                
                if (flvub == 0)
                {

                    dataRow1["id_client"] = -1;
                    //Выбираем идентификатор для связи с клиентом
                    dataRow1["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == -1)
                        .Select(r => r.Field<Int64>("id")).First();

                }
                else
                {


                    dataRow1["id_client"] = dgList.CurrentRow.Cells["id"].Value;//dataSet.Tables[0].Rows[rowIndexClient]["id"];
                    //Выбираем идентификатор для связи с клиентом
                    dataRow1["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == Convert.ToInt64(dgList.CurrentRow.Cells["id"].Value.ToString()))//dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()))
                        .Select(r => r.Field<Int64>("id")).First();

                   
                }
               

                dataRow1["creation"] = DateTime.Now;
                dataRow1["lastupdate"] = DateTime.Now;
                dataRow1["name"] = tbAccountName.Text;
                dataRow1["id_currency"] = cbAccountCurrency.SelectedValue;
                dataRow1["last_update_user"] = CurrentUser.CurrentUserId;


                accountDataSetobsh.Tables[0].Rows.Add(dataRow1);

                //////16.01.2020
                accountDataSet.Tables[0].Rows.Add(dataRow);
                //////16.01.2020

                //////15.01.2020

                /////14.01.2020
                //dataBase.UpdateData(accountDataSet, "t_g_account");
                /////14.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020



                ////23.10.2019
                //  tbAccountName.Clear();

                if (accountDataSet.Tables[0].Rows.Count>1)
                dgAccounts.CurrentCell = dgAccounts["name", accountDataSet.Tables[0].Rows.Count - 1];



                DgAccounts_SelectionChanged(sender, e);
                dgAccounts.Refresh();
                dgAccounts.Update();
                ////23.10.2019



            }
            else
            {
                MessageBox.Show("Введите номер счета");
            }
        }
        #endregion

        #region Кнопка изменения счета
        private void btnModifyAccount_Click(object sender, EventArgs e)
        {

            /////23.10.2019

            if (dgList.CurrentCell == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgList.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgCashCentre.CurrentCell == null)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }

            if (dgCashCentre.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }
            if (dgEnCashPoint.CurrentCell == null)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }

            if (dgEnCashPoint.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }

            if (dgAccounts.CurrentCell == null)
            {
                MessageBox.Show("Выберите счёт!");
                return;
            }

            if (dgAccounts.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите счёт!");
                return;
            }
            /////23.10.2019

            //Индекс клииента
            int rowIndexClient = dgList.CurrentCell.RowIndex;
            //Индекс кассового центра
            int rowIndexCashCentre = dgCashCentre.CurrentCell.RowIndex;
            //Индекс точки инкассации
            int rowIndexEncashPoint = dgEnCashPoint.CurrentCell.RowIndex;
            //Индекс счета
            int rowIndexAccount = dgAccounts.CurrentCell.RowIndex;
            //Если счет выбран или изменен
            if (!String.IsNullOrEmpty(tbAccountName.Text))
            {


                //////16.01.2020
                

                for (int i2 = 0; i2 < accountDataSetobsh.Tables[0].Rows.Count; i2++)
                {

                    if (accountDataSetobsh.Tables[0].Rows[i2]["id"].ToString() == accountDataSet.Tables[0].Rows[rowIndexAccount]["id"].ToString())
                    {


                        accountDataSetobsh.Tables[0].Rows[i2]["creation"] = DateTime.Now;
                        accountDataSetobsh.Tables[0].Rows[i2]["lastupdate"] = DateTime.Now;
                        accountDataSetobsh.Tables[0].Rows[i2]["name"] = tbAccountName.Text;
                        accountDataSetobsh.Tables[0].Rows[i2]["id_currency"] = cbAccountCurrency.SelectedValue;
                        accountDataSetobsh.Tables[0].Rows[i2]["last_update_user"] = CurrentUser.CurrentUserId;




                    }
                }

                /*
                //Наполняем измененными данными
                accountDataSet.Tables[0].Rows[rowIndexAccount]["id_encashpoint"] = enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id"];

                ////15.01.2020
                if (flvub == 0)
                {

                    accountDataSet.Tables[0].Rows[rowIndexAccount]["id_client"] = -1;
                    //Выбираем идентификатор для связи с клиентом
                    accountDataSet.Tables[0].Rows[rowIndexAccount]["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == -1)
                        .Select(r => r.Field<Int64>("id")).First();

                }
                else
                {
                    ////15.01.2020

                    accountDataSet.Tables[0].Rows[rowIndexAccount]["id_client"] = dataSet.Tables[0].Rows[rowIndexClient]["id"];
                //Выбираем идентификатор для связи с клиентом
                accountDataSet.Tables[0].Rows[rowIndexAccount]["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                    Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                     r.Field<Int64>("id_client") == Convert.ToInt64(dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()))
                    .Select(r => r.Field<Int64>("id")).First();

                    ////15.01.2020
                }

                ////15.01.2020
                */
                //////16.01.2020

                accountDataSet.Tables[0].Rows[rowIndexAccount]["creation"] = DateTime.Now;
                accountDataSet.Tables[0].Rows[rowIndexAccount]["lastupdate"] = DateTime.Now;
                accountDataSet.Tables[0].Rows[rowIndexAccount]["name"] = tbAccountName.Text;
                accountDataSet.Tables[0].Rows[rowIndexAccount]["id_currency"] = cbAccountCurrency.SelectedValue;
                accountDataSet.Tables[0].Rows[rowIndexAccount]["last_update_user"] = CurrentUser.CurrentUserId;


                //////16.01.2020
                /*
                /////15.01.2020

                //Наполняем измененными данными
                accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["id_encashpoint"] = enCashPointDataSet.Tables[0].Rows[rowIndexEncashPoint]["id"];

               
                if (flvub == 0)
                {

                    accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["id_client"] = -1;
                    //Выбираем идентификатор для связи с клиентом
                    accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == -1)
                        .Select(r => r.Field<Int64>("id")).First();

                }
                else
                {


                    accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["id_client"] = dataSet.Tables[0].Rows[rowIndexClient]["id"];
                    //Выбираем идентификатор для связи с клиентом
                    accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["id_clienttocc"] = clienttocDataSet.Tables[0].AsEnumerable().
                        Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndexCashCentre]["id"].ToString()) &&
                         r.Field<Int64>("id_client") == Convert.ToInt64(dataSet.Tables[0].Rows[rowIndexClient]["id"].ToString()))
                        .Select(r => r.Field<Int64>("id")).First();

                    
                }



                accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["creation"] = DateTime.Now;
                accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["lastupdate"] = DateTime.Now;
                accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["name"] = tbAccountName.Text;
                accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["id_currency"] = cbAccountCurrency.SelectedValue;
                accountDataSetobsh.Tables[0].Rows[rowIndexAccount]["last_update_user"] = CurrentUser.CurrentUserId;


                /////15.01.2020
                */






                //////16.01.2020

                /////14.01.2020
                //dataBase.UpdateData(accountDataSet, "t_g_account");
                /////14.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020


                ////23.10.2019
                //  tbAccountName.Clear();





                DgAccounts_SelectionChanged(sender, e);
                dgAccounts.Refresh();
                dgAccounts.Update();
                ////23.10.2019
            }
            else
            {
                MessageBox.Show("Введите номер счета");
            }
        }
        #endregion

        #region Кнопка удаления счета
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            //Если счет выделен
            if (dgAccounts.SelectedCells.Count > 0)
            {
                //Индекс счета
                int rowIndex = dgAccounts.CurrentRow.Index;
                ////23.10.2019
                //MessageBox.Show(rowIndex.ToString());
                ////23.10.2019

                
                //////16.01.2020
                
                for (int i2 = accountDataSetobsh.Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                {

                    if (accountDataSetobsh.Tables[0].Rows[i2]["id"].ToString() == accountDataSet.Tables[0].Rows[rowIndex]["id"].ToString())
                    {
                       // MessageBox.Show("account=" + accountDataSet.Tables[0].Rows[rowIndex]["id"].ToString());
                        DataSet account = dataBase.GetData9("Select * from t_g_counting_content where id_account="+ accountDataSet.Tables[0].Rows[rowIndex]["id"].ToString());
                        if(account.Tables[0].Rows.Count>0)
                        {
                            MessageBox.Show("Невозможно удалить счет поскольку оно используется в пересчете!");
                            return;
                        }
                        accountDataSetobsh.Tables[0].Rows[i2].Delete();
                        break;

                    }

                }

                //////20.01.2020
                accountDataSetobsh.Tables[0].AcceptChanges();
                //////20.01.2020

                //////16.01.2020



                accountDataSet.Tables[0].Rows[rowIndex].Delete();

                //////20.01.2020
                accountDataSet.Tables[0].AcceptChanges();
                //////20.01.2020

                /////14.01.2020
                //dataBase.UpdateData(accountDataSet, "t_g_account");
                /////14.01.2020

                /////21.01.2020
                /*
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
                */
                /////21.01.2020


                ////23.10.2019
                //  tbAccountName.Clear();

                if (dgAccounts.Rows.Count == 0)
                    tbAccountName.Clear();



                DgAccounts_SelectionChanged(sender, e);
                dgAccounts.Refresh();
                dgAccounts.Update();
                ////23.10.2019
            }
            else
            {
                MessageBox.Show("Выберите счет для удаления");
            }
        }
        #endregion

        #region Подготовка и заполнение данными грид со счетами
        private void Account_View()
        {

            /////23.10.2019
            //dgAccounts.ClearSelection();
            dgAccounts.Columns.Clear();
            //dgAccounts.Rows.Clear();
            /////23.10.2019
            
            //Добавление столбца с номером счета
            dgAccounts.Columns.Add("name", "Номер счета");
            dgAccounts.Columns["name"].DataPropertyName = "name";
            dgAccounts.Columns["name"].Visible = true;
            dgAccounts.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //Создание столбца для отборажения валюты
            DataGridViewComboBoxColumn columnCurrency = new DataGridViewComboBoxColumn();
            columnCurrency.DataSource = currencyDataSet.Tables[0];
            columnCurrency.Name = "id_currency";
            columnCurrency.HeaderText = "Валюта";
            columnCurrency.ValueMember = "id";
            columnCurrency.DisplayMember = "curr_code";
            columnCurrency.DataPropertyName = "id_currency";
            columnCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

            //Добавление столбца с валютой
            dgAccounts.Columns.Add(columnCurrency);
            dgAccounts.Columns["id_currency"].Visible = true;
            dgAccounts.Columns["id_currency"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //////15.01.2020
            if (accountDataSet != null)
                //////15.01.2020

                /////23.10.2019
                if (accountDataSet.Tables.Count > 0)
                /////23.10.2019
                {
                    
                    dgAccounts.DataSource = accountDataSet.Tables[0];
                  
                }
            /////08.01.2020
            dgAccounts.AutoResizeColumns();
            /////08.01.2020

            dgAccounts.Columns["id_currency"].ReadOnly = false;

            //Убираем выделение с грида
            dgAccounts.ClearSelection();
        }
        #endregion

        #region Навигация клавишей Enter по счетам

        #region Обработчик клавиш в поле ввода номера счета
        private void tbAccountName_KeyUp(object sender, KeyEventArgs e)
        {
            //Если нажата клавиша Enter
            if (e.KeyCode == Keys.Enter && tbAccountName.Text != String.Empty)
            {
                //Пометить как выполненое
                e.Handled = true;
                //Установить фокус на выборе валюты счета
                cbAccountCurrency.Focus();
            }
        }
        #endregion

        #region Обработка клавиш на выборе валюты счета
        private void cbAccountCurrency_KeyUp(object sender, KeyEventArgs e)
        {
            //Если нажата клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                //Установить фокус на кнопку добавления счета
                btnAddAccount.Focus();
            }
        }
        #endregion

        #region Обработка клавиш на кнопке добавления счета
        private void btnAddAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Если нажата клавиша Enter
            if (e.KeyChar == (char)Keys.Return)
            {
                //Выполнить действие по нажатию кнопки
                btnAddAccount.PerformClick();
            }
        }
        #endregion

        #region Выделение вставленных счетов
        private void RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgAccounts.ClearSelection();
            dgAccounts.Rows[e.RowIndex].Selected = false;
        }





        #endregion

        #endregion

        #endregion

        /// 23.10.2019
       
        private void DgEnCashPoint_SelectionChanged(object sender, EventArgs e)
        {
            if (dgEnCashPoint.CurrentCell != null)
            {

                int rowIndex = dgEnCashPoint.CurrentCell.RowIndex;
                tbAccountName.Text = "";

                //Заполняем поле имени точки инкассации
                //   tbEncashPointName.Text = enCashPointDataSet.Tables[0].Rows[rowIndex]["name"].ToString();
                if (dgEnCashPoint[0, rowIndex].Value!=null)
                tbEncashPointName.Text = dgEnCashPoint[0, rowIndex].Value.ToString();
                //Заполняем поле адресса точки инкассации
                // tbPointAdrress.Text = enCashPointDataSet.Tables[0].Rows[rowIndex]["Address"].ToString();
                if (dgEnCashPoint.Columns.Count>1)
                if (dgEnCashPoint[1, rowIndex].Value != null)
                    tbPointAdrress.Text = dgEnCashPoint[1, rowIndex].Value.ToString();
                //17.09.2020
                if(dgEnCashPoint.Columns.Count > 1)
                if (dgEnCashPoint[2, rowIndex].Value != null)
                    tbINF1.Text = dgEnCashPoint[2, rowIndex].Value.ToString();
                else tbINF1.Text = "";
                if (dgEnCashPoint.Columns.Count > 1)
                    if (dgEnCashPoint[3, rowIndex].Value != null)
                    tbINF2.Text = dgEnCashPoint[3, rowIndex].Value.ToString();
                else tbINF2.Text = "";
                //////

                //Очистка грида счетов
                ClearDataGrid(dgAccounts);

                //////
                /*
                if (ds.Tables[0].Rows[i].RowState != DataRowState.Deleted &&
    Convert.ToString(ds.Tables[0].Rows[i][0].ToString()) == value)
                {
                    // blaaaaa
                }
                */
                /////



                // dgAccounts.DataSource = null;
                if (enCashPointDataSet.Tables[0].Rows[rowIndex].RowState != DataRowState.Deleted)
                {

                    ///16.01.2020
                    if (enCashPointDataSet.Tables[0].Rows[rowIndex].RowState != DataRowState.Detached)
                        //string s1 = enCashPointDataSet.Tables[0].Rows[rowIndex]["id"].ToString();
                        ///16.01.2020

                        ////15.01.2020

                        if (accountDataSetobsh != null)
                    {
                        string s2= (enCashPointDataSet.Tables.Count > 0 && enCashPointDataSet.Tables[0].Rows.Count > 0) ? enCashPointDataSet.Tables[0].Rows[rowIndex]["id"].ToString() : "0";
                           // MessageBox.Show(s2);
                            DataRow[] currentRows1 = ((DataTable)accountDataSetobsh.Tables[0]).Select(" id_encashpoint in (" + s2.ToString() + ")");

                        accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc", "-1");
                        foreach (DataRow dr in currentRows1)
                        {
                            object[] row = dr.ItemArray;
                            accountDataSet.Tables[0].Rows.Add(row);
                        }




                   //     accountDataSet = dataBase.GetData("t_g_account", "id_encashpoint",
               //   (enCashPointDataSet.Tables.Count > 0 && enCashPointDataSet.Tables[0].Rows.Count > 0) ? enCashPointDataSet.Tables[0].Rows[rowIndex]["id"].ToString() : "0");

                    }
                        ////15.01.2020

                    }

                    int i1 = accountDataSet.Tables[0].Rows.Count;

                if (i1==0)
                    tbAccountName.Text = "";

                //  dgAccounts.DataSource = accountDataSet;

                //  dgAccounts.Refresh();
                //  dgAccounts.Update();

                //Заполнение грида счетов
                Account_View();

                ///////

            }

        }

        private void DgCashCentre_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCashCentre.CurrentCell != null)
            {

                int rowIndex = dgCashCentre.CurrentCell.RowIndex;

                /////23.10.2019
                dgEnCashPoint.ClearSelection();
                dgAccounts.ClearSelection();

                ////05.01.2020
                // string s1=cashCentreDataSet.Tables[0].Rows[rowIndex]["id"].ToString();
                //  int i1 = Convert.ToInt32(cashCentreDataSet.Tables[0].Rows[rowIndex]["id"]);
                ////05.01.2020
                /////23.10.2019

                //////15.01.2020

                if (enCashPointDataSetobsh != null)
                {

                    //////17.01.2020
                    
                    if (clienttocDataSet.Tables[0].Rows.Count >0)
                    {
                        //////17.01.2020

                        /////21.01.2020
                        
                        
                        if (rowIndex<= cashCentreDataSet.Tables[0].Rows.Count-1)
                        {

                        /////21.01.2020

                            //Заполнение данными переменную точек инкассации

                            DataRow[] currentRows = ((DataTable)enCashPointDataSetobsh.Tables[0]).Select(" id_clienttocc in (" + String.Join(",", clienttocDataSet.Tables[0].AsEnumerable().
                         Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[rowIndex]["id"])).
                         Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                         ToList<string>()).ToString() + ")");
                               

                            

                            enCashPointDataSet = dataBase.GetData("t_g_encashpoint", "id_clienttocc", "-1");
                        foreach (DataRow dr in currentRows)
                        {
                            object[] row = dr.ItemArray;
                            enCashPointDataSet.Tables[0].Rows.Add(row);
                        }

                            if (accountDataSetobsh != null)
                            {
                                DataRow[] currentRows1 = ((DataTable)accountDataSetobsh.Tables[0]).Select(" id_clienttocc in (" + String.Join(",",
                              clienttocDataSet.Tables[0].AsEnumerable().
                             Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[rowIndex]["id"])).
                             Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                             ToList<string>()).ToString() + ")");

                                accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc", "-1");
                                foreach (DataRow dr in currentRows1)
                                {
                                    object[] row = dr.ItemArray;
                                    accountDataSet.Tables[0].Rows.Add(row);
                                }
                            }
                            /*
                            DataRow[] currentRows = dataTable.Select(expression);
                            DataTable table = new DataTable();
                            table = dataTable.Clone();
                            foreach (DataRow dr in currentRows)
                            {
                                object[] row = dr.ItemArray;
                                table.Rows.Add(row);
                            }
                            */
                            /*
                            //Заполнение данными переменную точек инкассации
                            enCashPointDataSet = dataBase.GetData("t_g_encashpoint", "id_clienttocc",
                          clienttocDataSet.Tables[0].AsEnumerable().
                          Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[rowIndex]["id"])).
                          Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                          ToList<string>());

                            //Заполнение данными переменную счетов
                            accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc",
                               clienttocDataSet.Tables[0].AsEnumerable().
                              Where(r => r.Field<Int64>("id_cashcentre") == Convert.ToInt64(cashCentreDataSet.Tables[0].Rows[rowIndex]["id"] == DBNull.Value ? 0 : cashCentreDataSet.Tables[0].Rows[rowIndex]["id"])).
                              Select(r => (r["id"] == DBNull.Value ? 0 : r["id"]).ToString()).
                              ToList<string>());
                            */

                            /////21.01.2020
                        }
                        /////21.01.2020

                    }

                }
                //////15.01.2020



                //Очистка грида счетов
                ClearDataGrid(dgAccounts);
                //Очистка грида точек инкассации
                ClearDataGrid(dgEnCashPoint);
                //Заполнение грида точек инкассации
                EncashPoint_View();
                //Заполнение грида счетов
                Account_View();
                

            }
        }

        private void DgAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgAccounts.CurrentCell != null)
            {

                int rowIndex = dgAccounts.CurrentCell.RowIndex;

                //Заполняем поле номера счета
                // tbAccountName.Text = accountDataSet.Tables[0].Rows[rowIndex]["name"].ToString();
                if (dgAccounts[0, rowIndex].Value != null)
                    tbAccountName.Text = dgAccounts[0, rowIndex].Value.ToString();
                //Выбор валюты счета

                //cbAccountCurrency.SelectedValue = accountDataSet.Tables[0].Rows[rowIndex]["id_currency"];
                if (dgAccounts.Columns.Count > 1)
                    if (dgAccounts[1, rowIndex].Value != null)
                        cbAccountCurrency.SelectedValue = dgAccounts[1, rowIndex].Value;
            }
        }
        /// 23.10.2019

        /////14.11.2019
        ///
       
        private void timer2_Tick(object sender, EventArgs e)
        {
           
            timer2.Enabled = false;

            tbName.Text = "";

            /////21.01.2020
            //btnAdd.Enabled = true;
            // btnModify.Enabled = true;
            /////21.01.2020

        }

        private void oschist1()
        {
            tbRKO_CODE.Text = "";
            tbRKO_DEP_CODE.Text = "";
            tbKO_CODE.Text = "";
            tbKO_INF1.Text = "";
            tbKO_INF2.Text = "";
            tbKO_INF3.Text = "";
            tbKO_INF4.Text = "";
            tbINF1.Text = "";
            tbINF2.Text = "";
            /////14.01.2020
            flvub = 0;
            flizm = 0;
            /////14.01.2020

            tbName.Text = "";
            tbID.Text = "";
            //Группа клиента
            cbClientGroup.SelectedValue = -1;
            //Город
            cbCity.SelectedValue = -1;
            //Адрес
            tbAddress.Text = "";
            //Группы
            tbGroup1.Text = "";
            tbGroup2.Text = "";
            tbGroup3.Text = "";
            tbGroup4.Text = "";
            //Индекс
            tbPostIndex.Text = "";

            tbEncashPointName.Clear();
            tbPointAdrress.Clear();
            tbAccountName.Clear();
            cbAccountCurrency.Text = "";

            /////
            dgCashCentre.ClearSelection();
            // if (dgCashCentre.Rows.Count>0) 
            dgCashCentre.DataSource = null;
            /////

            //Заполнение всех элементов 
            //   AllConntrolsView();
            //Фокус на точке инкасации
            //  tbEncashPointName.Focus();

            pictureBox1.Image = null;
            pictureBox2.Image = null;

            //Очищаем грид с кассовыми центрами
            ClearDataGrid(dgCashCentre);
            //Очищаем грид с точками инкассации
            ClearDataGrid(dgEnCashPoint);
            //Очищаем грид со счетами
            ClearDataGrid(dgAccounts);
            //MessageBox.Show("Clear!");
            tbEncashPointName.Text = "";
            tbPointAdrress.Text = "";
            tbINF1.Text = "";
            tbINF2.Text = "";
            tbAccountName.Text = "";
            cbAccountCurrency.SelectedIndex = -1;
            clbCashCentre.ClearSelected();
            //Делаем все центры неактивными
            ((ListBox)clbCashCentre).DataSource = null;
            //dataGridView1.DataSource = null;
            // textBox1.Text = "";

            ////14.01.2020
            clbCashCentre.ItemCheck -= clbCashCentre_ItemCheck;
            //Загружаем все кассовые центры из справочника

            /////11.02.2020
            //allCashCentresDataset = dataBase.GetData("t_g_cashcentre");
            allCashCentresDataset = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
            /////11.02.2020

            //Добавляем гриду с кассовыми центрами столбец с назавниями
            dgCashCentre.Columns.Add("branch_name", "Кассовый центр");
            dgCashCentre.Columns["branch_name"].DataPropertyName = "branch_name";
            dgCashCentre.Columns["branch_name"].Visible = true;

            //List<string> list = new List<string>();
            //list.Add("asd");
            //list.Add("cvb");

            //Добавляем все кассовые центры в компонент выбора кассовых центров
            ((ListBox)clbCashCentre).DataSource = allCashCentresDataset.Tables[0];            
            ((ListBox)clbCashCentre).DisplayMember = allCashCentresDataset.Tables[0].Columns["branch_name"].ColumnName;
            ((ListBox)clbCashCentre).ValueMember = allCashCentresDataset.Tables[0].Columns["id"].ColumnName;
            //Убираем выделение
            clbCashCentre.ClearSelected();
            clbCashCentre.ItemCheck += clbCashCentre_ItemCheck;
            // AllConntrolsView();

            //Заполнение данными переменную со валютой
            currencyDataSet = dataBase.GetData("t_g_currency");

            cbAccountCurrency.DisplayMember = "curr_code";
            cbAccountCurrency.ValueMember = "id";
            cbAccountCurrency.DataSource = currencyDataSet.Tables[0];
            ////14.01.2020

            /////15.01.2020

            enCashPointDataSet = dataBase.GetData("t_g_encashpoint", "id_clienttocc", "-1");
            accountDataSet = dataBase.GetData("t_g_account", "id_clienttocc", "-1");

            enCashPointDataSetobsh = dataBase.GetData("t_g_encashpoint", "id_clienttocc", "-1");
            accountDataSetobsh = dataBase.GetData("t_g_account", "id_clienttocc", "-1");

            /////15.01.2020

            ////20.01.2020
            pechat4 = null;
            ////20.01.2020

            ////21.01.2020
            flkn = 0;
            //btnAdd.Enabled = true;
            //btnModify.Enabled = false;
            ////21.01.2020

        }

        //private void button7_Click(object sender, EventArgs e)
        //{

        //    oschist1();

        //}

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    pictureBox1.Image = null;
        //    pictureBox2.Image = null;
        //    pokazimg();
        //}

        /////13.01.2020
        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {

                pechatpoisk();

            }
        }

        private void dgCashCentre_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {

           
        }

        private void dgCashCentre_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ////16.01.2020
        //    cashCentreDataSet.Tables[0].DefaultView.Sort = "branch_name Desc";
            ////16.01.2020
        }

       

        private void tbName_TextChanged_1(object sender, EventArgs e)
        {
            #region comment
            // if (flkn==1)
            // {

            //     btnAdd.Enabled = false;
            //     btnModify.Enabled = true;
            // }
            //else
            // {
            //     btnAdd.Enabled = true;
            //     btnModify.Enabled = false;

            // }
            #endregion

        }

        private void dgCashCentre_Sorted(object sender, EventArgs e)
        {

            /////21.01.2020

            DataTable dt = new DataTable();

            if (cashCentreDataSet != null)
            {
                if (y1 == 0)
                {
                    // dt = cashCentreDataSet.Tables[0].Select("", "branch_name ASC").CopyToDataTable();
                    dt = cashCentreDataSet.Tables[0].Select("", "branch_name DESC").CopyToDataTable();
                    cashCentreDataSet = new DataSet();
                    cashCentreDataSet.Tables.Add(dt);
                    y1 = 1;

                }
                else
                {
                    // dt = cashCentreDataSet.Tables[0].Select("", "branch_name DESC").CopyToDataTable();
                    dt = cashCentreDataSet.Tables[0].Select("", "branch_name ASC").CopyToDataTable();
                    cashCentreDataSet = new DataSet();
                    cashCentreDataSet.Tables.Add(dt);
                    y1 = 0;

                }

                DgCashCentre_SelectionChanged(sender, e);
            }
            /////21.01.2020

        }
        /////13.01.2020

        /////14.11.2019

       /////22.01.2020
       
        protected override void btnOchist_Click(object sender, EventArgs e)
        {
            oschist1();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if ((comboBox3.Text.Trim() != "") | (comboBox4.Text.Trim() != "") | (comboBox5.Text.Trim() != ""))
            {
                string s1 = "";
                if (comboBox3.Text.Trim() != "")
                    s1 = s1 + " and t1.id="+comboBox3.SelectedValue.ToString();
                if (comboBox5.Text.Trim() != "")
                    s1 = s1 + " and t1.BIN = '" + comboBox5.Text.ToString() + "' ";
                if (comboBox4.Text.Trim() != "")
                    s1 = s1 + "  and id=(select top 1 id_client from t_g_account t2 where t2.id = " + comboBox4.SelectedValue.ToString() + ") ";

                dataSet = dataBase.GetData9("select t1.* from t_g_client t1 where deleted = 0 " + s1.ToString() + " order by t1.name");
                dgList.DataSource = dataSet.Tables[0];

                //if (dgList.Rows.Count > 0)
                //    dgList.CurrentCell = dgList[0, 0];

                button10.BackColor = System.Drawing.Color.Yellow;
                #region comment
                //button10.Enabled = true;
                #endregion
            }
        }      
       

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region comment
            //if(comboBox4.SelectedIndex!=-1)
            //{
            //    comboBox3.Enabled = false;
            //    comboBox5.Enabled = false;
            //}
            //else
            //{

            //    comboBox3.Enabled = true;
            //    comboBox5.Enabled = true;
            //}
            #endregion
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataSet = dataBase.GetData9("select t1.* from t_g_client t1 where deleted = 0  order by t1.name");
            dgList.DataSource = dataSet.Tables[0];

            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;

            button10.BackColor = SystemColors.Control;
            button10.Enabled = false;
        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox3.Text.Trim() != "")
            {
                string text = comboBox3.Text;

                string selectString = "name like '%" + comboBox3.Text.Trim().ToString() + "%'";
                DataRow[] searchedRows = ((DataTable)clientsDataSet2.Tables[0]).Select(selectString);

                if (searchedRows.Count() > 0)
                    comboBox3.DataSource = ((DataTable)clientsDataSet2.Tables[0]).Select(selectString).CopyToDataTable();
                else
                    comboBox3.DataSource = clientsDataSet2.Tables[0];

                comboBox3.Text = text;
                comboBox3.SelectionStart = text.Length;

                
            }
            else
                comboBox3.DataSource = clientsDataSet2.Tables[0];
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) | (e.KeyCode == Keys.Back))
            {
                clientsDataSet2 = dataBase.GetData9("select * from t_g_client where deleted=0 order by name");
                comboBox3.DataSource = clientsDataSet2.Tables[0];
                comboBox3.SelectedIndex = -1;
            }
            //PrepareData();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void dgEnCashPoint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgList.CurrentCell == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgList.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (dgCashCentre.CurrentCell == null)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }

            if (dgCashCentre.CurrentCell.RowIndex == -1)
            {
                MessageBox.Show("Выберите кассовый центр!");
                return;
            }
            if (dgEnCashPoint.CurrentCell == null)
            {
                MessageBox.Show("Выберите точку инкассации!");
                return;
            }

            int id_EncashPoint = Convert.ToInt32(dgEnCashPoint.CurrentRow.Cells["id"].Value);

            if (id_EncashPoint == -1)
            {
                MessageBox.Show("Сперва зареистриуйте клиента а затем можете добавить пломбы!");
                return;
            }

            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) //PROC_BALANCE
            {
                PlombForm1 plombForm1 = new PlombForm1(id_EncashPoint,1);
                DialogResult dialogResult = plombForm1.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    
                  //  pechat4 = plombForm1.Pechat4;
                }

            }
        }

    }
}
