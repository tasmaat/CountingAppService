using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;

namespace CountingForms
{
    public partial class CountingForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = null;
        private DataSet currencyDataSet = null;
        private DataSet cardsDataSet = null;
        private DataSet countingDataSet = null;
        private DataSet countContentDataSet = null;
        private DataSet countDenomDataSet = null;
        private DataSet countDenomDataSet1 = null;
 
        private DataSet conditionDataSet = null;
        private DataSet denominationDataSet = null;
        private DataSet clientDataSet = null;
        private DataSet dataSet = null;
        private DataSet dataSet2 = null;
        private Int64 counting_id = -1;
        private Int64 client_id = -1;
        private Int64 id_bag;
        //private Int64 selectedCurrency = -1;
        private Int64 card_id = -1;
        private DataTable counterfietTable = null;
        private bool card = false;
        private int fl_prov = -1;
        ///06.11.2019 
        private DataSet spisval1 = null;
        ///06.11.2019 


        /////28.11.2019
        private DataSet cennDataSet = null;
        /////28.11.2019


        /////14.02.2020
        private string tval1;
        private string tsost1;
        private string ttip1;
        /////14.02.2020

        ////17.02.2020
        // private DataTable dsost1 = null;

        private int rasrval1 = 0;
        ////17.02.2020
        //21.07.2020
        private string coun1 = "0";

        #region Конструктор формы
        public CountingForm()
        {
            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase = new MSDataBase();
            dataBase.Connect();
            InitializeComponent();
            currencyDataSet = dataBase.GetData("t_g_currency");



            ////17.02.2020

            //conditionDataSet = dataBase.GetData("t_g_condition");
            //dsost1 = conditionDataSet.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == false).CopyToDataTable<DataRow>();
            conditionDataSet = dataBase.GetData9("select * from t_g_condition where visible=0");

            ////17.02.2020

            countDenomDataSet = dataBase.GetSchema("t_g_counting_denom");
            //countDenomDataSet1 = dataBase.GetSchema("t_g_cashtransfer_detalization");



            /////28.11.2019
            cennDataSet = dataBase.GetData9("select * from t_g_tipzenn where visible=0");
            /////28.11.2019

            Prepare_Components();




        }

        #endregion

        private async void CountingForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        ///01.11.2019
        // private void poiskkart()
        private void poiskkart(int i1)//, object sender, KeyEventArgs e)//EventArgs
        {
            tbCard.Text = tbCard.Text.Replace("+", "").Trim();
            tbCard.Text = tbCard.Text.Replace("-", "").Trim();
            counterfietTable = null;
            denominationDataSet = null;
            cardsDataSet = dataBase.GetData("t_g_cards", "name", tbCard.Text);
            if (cardsDataSet.Tables[0].Rows.Count > 0)
            {
                // MessageBox.Show(cardsDataSet.Tables[0].Rows[0]["id_bag"].ToString());
                if (cardsDataSet.Tables[0].Rows[0]["id_bag"] == null | cardsDataSet.Tables[0].Rows[0]["id_bag"].ToString().Trim() == "")
                {
                    MessageBox.Show("Эта карта не зарегистрировано в системе, сделайте сопоставление");
                    MessageBox.Show("Эта карта не зарегистрировано в системе, сделайте сопоставление");
                    tbCard.Text = String.Empty;
                    return;
                }
                id_bag = Convert.ToInt64(cardsDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_bag")).First<Int64>());

                ///19.02.2020
                if(pm.VisiblePossibility(perm, rblCenn))
                    rblCenn.Visible = true;
                ///19.02.2020

                /////17.02.2020

                rblCurrency.SelectedIndexChanged -= rblCurrency_SelectedIndexChanged;
                rblCondition.SelectedIndexChanged -= rblCondition_SelectedIndexChanged;
                rasrval1 = 1;
                /////17.02.2020

                counting_id = cardsDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).First<Int64>();
                //MessageBox.Show(counting_id.ToString());
                countingDataSet = dataBase.GetData("t_g_counting", "id", counting_id.ToString());
                countContentDataSet = dataBase.GetData("t_g_counting_content", "id_counting", counting_id.ToString());
                // MessageBox.Show(countingDataSet.Tables[0].Rows.Count.ToString());
                client_id = countingDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_client")).First<Int64>();
                clientDataSet = dataBase.GetData("t_g_client", "id", client_id.ToString());
                card_id = cardsDataSet.Tables[0].Rows[cardsDataSet.Tables[0].Rows.Count - 1].Field<Int64>("Id");

                //////18.02.2020
                //countDenomDataSet = dataBase.GetData("t_g_counting_denom", "id_card", card_id.ToString());
                countDenomDataSet = dataBase.GetData9("select * from t_g_counting_denom t1 where (counterfeit!=1 or counterfeit is null) and t1.id_card=" + card_id.ToString() + " and t1.last_user_update=(case when exists(select 1 from t_g_user t2  where t2.id=" + DataExchange.CurrentUser.CurrentUserId + " and t2.rasrprper1=1) then t1.last_user_update  else  " + DataExchange.CurrentUser.CurrentUserId + "  end)");
                //////18.02.2020
                //counterfietTable
                

                //Заполнение кнопок выбора валют



                ///06.11.2019
                if (i1 == 2)
                {
                    rblCurrency.DataSource = (from curr in currencyDataSet.Tables[0].AsEnumerable()
                                              join count in countContentDataSet.Tables[0].AsEnumerable() on curr.Field<Int64>("id") equals count.Field<Int64>("id_currency")
                                              select new
                                              {
                                                  id = curr.Field<Int64>("id"),
                                                  //name = curr.Field<String>("name"),
                                                  curr_code = curr.Field<String>("curr_code")
                                              }
                                                                 ).Distinct().ToList();
                }
                else
                {
                    spisval1 = dataBase.GetData9("select distinct t1.id id, t1.curr_code curr_code from t_g_currency t1, t_g_counting_content t2 where t1.id=t2.id_currency and id_counting='" + counting_id.ToString() + "' and   exists(select 1 from t_g_counting_denom t3, t_g_denomination t4 where t3.id_denomination=t4.id and t3.id_card='" + card_id.ToString() + "' and t4.id_currency=t1.id)");
                    if (spisval1.Tables[0].Rows.Count > 0)
                    {
                        rblCurrency.DataSource = (from curr in spisval1.Tables[0].AsEnumerable()

                                                  select new
                                                  {
                                                      id = curr.Field<Int64>("id"),
                                                      //name = curr.Field<String>("name"),
                                                      curr_code = curr.Field<String>("curr_code")
                                                  }
                                                   ).Distinct().ToList();
                    }
                    else
                    {
                        /////10.12.2019
                        /*
                        rblCurrency.DataSource = (from curr in currencyDataSet.Tables[0].AsEnumerable()
                                                  join count in countContentDataSet.Tables[0].AsEnumerable() on curr.Field<Int64>("id") equals count.Field<Int64>("id_currency")

                                                  /////06.12.2019

                                                  where (count.Field<String>("count")== null)


                                                  ////06.12.2019

                                                  select new
                                                  {
                                                      id = curr.Field<Int64>("id"),
                                                      //name = curr.Field<String>("name"),
                                                      curr_code = curr.Field<String>("curr_code")

                                                    

                                                  }
                                              ).Distinct().ToList();
                        */


                        spisval1 = dataBase.GetData9("select distinct t1.id id, t1.curr_code curr_code from t_g_currency t1, t_g_counting_content t2 where t1.id=t2.id_currency and id_counting='" + counting_id.ToString() + "' and   not exists(select 1 from t_g_counting_denom t3, t_g_denomination t4 where t3.id_denomination=t4.id and t4.id_currency=t1.id and t3.id_counting='" + counting_id.ToString() + "')");



                        if (spisval1.Tables[0].Rows.Count > 0)
                        {
                            rblCurrency.DataSource = (from curr in spisval1.Tables[0].AsEnumerable()

                                                      select new
                                                      {
                                                          id = curr.Field<Int64>("id"),
                                                          //name = curr.Field<String>("name"),
                                                          curr_code = curr.Field<String>("curr_code")
                                                      }
                                                       ).Distinct().ToList();

                        }
                        else
                        {

                            rblCurrency.DataSource = (from curr in currencyDataSet.Tables[0].AsEnumerable()
                                                      join count in countContentDataSet.Tables[0].AsEnumerable()
                                                      on curr.Field<Int64>("id") equals count.Field<Int64>("id_currency")
                                                      select new
                                                      {
                                                          id = curr.Field<Int64>("id"),
                                                          //name = curr.Field<String>("name"),
                                                          curr_code = curr.Field<String>("curr_code")
                                                      }
                                                                                             ).Distinct().ToList();

                        }

                        /////10.12.2019

                    }
                }

                /////17.02.2020
                rasrval1 = 2;

                if (tval1 != null)
                {
                    DataRow[] h22 = ((DataTable)currencyDataSet.Tables[0]).Select("id=" + tval1.ToString());
                    if (h22.Count() > 0)
                    {

                        //rblCurrency.SelectedValueChanged()

                        // rblCurrency. = tval1;

                        for (int i8 = 0; i8 < rblCurrency.Items.Count; i8++)
                        {

                            rblCurrency.SelectedIndex = i8;
                            if (rblCurrency.SelectedValue.ToString() == tval1.ToString())
                                break;
                            //  rblCurrency.SelectedValue = tval1;

                            // rblCurrency.SelectedIndex = 1;

                        }


                    }
                    else
                        rblCurrency.SelectedValue = 0;

                    // rblCurrency.SelectedIndexChanged += rblCurrency_SelectedIndexChanged;
                    rblCurrency.SelectedIndexChanged += rblCurrency_SelectedIndexChanged;
                    vubval1();




                }
                else
                {
                    // rblCurrency.SelectedValue = 0;

                    rblCurrency.SelectedIndexChanged += rblCurrency_SelectedIndexChanged;
                }









                /////17.02.2020

                /*
                 rblCurrency.DataSource = (from curr in currencyDataSet.Tables[0].AsEnumerable()
                                           join count in countContentDataSet.Tables[0].AsEnumerable() on curr.Field<Int64>("id") equals count.Field<Int64>("id_currency")
                                           select new
                                           {
                                               id = curr.Field<Int64>("id"),
                                               //name = curr.Field<String>("name"),
                                               curr_code = curr.Field<String>("curr_code")
                                           }
                                           ).Distinct().ToList();
                 */
                ///06.11.2019 


                if (conditionDataSet.Tables[0].Rows[conditionDataSet.Tables[0].Rows.Count - 1]["name"].ToString() != "Все")
                {
                    DataRow conditionAllRow = conditionDataSet.Tables[0].NewRow();
                    conditionAllRow["id"] = 0;
                    conditionAllRow["name"] = "Все";
                    conditionAllRow["visible"] = false;
                    //conditionAllRow["curr_code"] = "Все";
                    conditionDataSet.Tables[0].Rows.Add(conditionAllRow);

                }

                /////17.02.2020
                //rblCondition.DataSource = conditionDataSet.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == false).CopyToDataTable<DataRow>();
                rblCondition.DataSource = conditionDataSet.Tables[0];
                /////17.02.2020

                /////14.02.2020


                // rblCondition.SelectedValue = 0;

                if (tsost1 != null)
                {
                    DataRow[] h33 = ((DataTable)conditionDataSet.Tables[0]).Select("id=" + tsost1.ToString());
                    if (h33.Count() > 0)
                        rblCondition.SelectedValue = tsost1.ToString();
                    else
                        rblCondition.SelectedValue = 0;

                    vubsost1();

                    /////17.02.2020

                    //  rblCurrency.SelectedIndexChanged -= rblCurrency_SelectedIndexChanged;
                    rblCondition.SelectedIndexChanged += rblCondition_SelectedIndexChanged;

                    /////17.02.2020

                }
                else
                {
                    rblCondition.SelectedValue = 0;
                    rblCondition.SelectedIndexChanged += rblCondition_SelectedIndexChanged;

                }

                /////14.02.2020

                //var prop = rblCurrency.SelectedValue.GetType().GetProperty("id").GetValue(rblCurrency.SelectedValue);
                denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", rblCurrency.SelectedValue.ToString());

                //////28.11.2019
                if (cennDataSet.Tables[0].Rows[cennDataSet.Tables[0].Rows.Count - 1]["name_zenn"].ToString() != "Все")
                {
                    DataRow conditionAllRow = cennDataSet.Tables[0].NewRow();
                    conditionAllRow["id"] = 0;
                    conditionAllRow["name_zenn"] = "Все";
                    conditionAllRow["visible"] = false;
                    cennDataSet.Tables[0].Rows.Add(conditionAllRow);

                }
                rblCenn.DataSource = cennDataSet.Tables[0];




                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           where cond.Field<bool>("visible") == false && cond.Field<Int64>("id") != 0
                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name"),

                                           }

                    ).OrderBy(x => x.id_condition).ToList();//denominationDataSet.Tables[0];
                */


                /////18.02.2020
                // dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='"+ rblCurrency.SelectedValue.ToString() + "' and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0] ;

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id " +
                    "and t11.id_currency=t1.id_currency " +
                    //"and " +
                    //"t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + " " +
                    " and 1=(case when exists(select 1 from t_g_user t20  where " +
                    // "t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and" +
                    " t20.rasrprper1 = 1) then 0 else 1 end)) " +
                    "order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                //////28.11.2019

                /////14.02.2020


                //rblCenn.SelectedValue = 0;

                DataRow[] h44 = ((DataTable)cennDataSet.Tables[0]).Select("id=" + ttip1.ToString());
                if (h44.Count() > 0)
                    rblCenn.SelectedValue = ttip1.ToString();
                else
                    rblCenn.SelectedValue = 0;

                vubcenn1();
                /////14.02.2020


                LoadToGrid();
                lblCountNum.Text = countingDataSet.Tables[0].Rows[0].Field<string>("name").ToString();
                lblClientName.Text = clientDataSet.Tables[0].Rows[0].Field<string>("name").ToString();
                dgDenomCount.Select();
                ////14.02.2020
                if (dgDenomCount.Rows.Count > 0)
                    ///14.02.2020
                    dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
                card = true;

                if(pm.EnabledPossibility(perm, button2))
                    button2.Enabled = true;
                if (pm.EnabledPossibility(perm, btnFalse))
                    btnFalse.Enabled = true;
                if (pm.EnabledPossibility(perm, btnValidate))
                    btnValidate.Enabled = true;
                if (pm.EnabledPossibility(perm, btnClear))
                    btnClear.Enabled = true;
            }
            else
            {
                //if()
                MessageBox.Show("Карточка не найдена");

                MessageBox.Show("Карточка не найдена");
                //tbCard.Text = String.Empty;
                card = false;

                //denominationDataSet.Tables[0].Clear();
                object sender1 = null;
                EventArgs e1 = null;
                btnClear_Click(sender1, e1);

                button2.Enabled = false;
                btnFalse.Enabled = false;
                btnValidate.Enabled = false;
                btnClear.Enabled = false;

            }

            Sum_Count();

            //MessageBox.Show(rblCurrency.SelectedValue.ToString());


        }
        ///01.11.2019
        string s1 = "";
        #region Обрабочик нажатия клавиши Esc для ввода КР
        private void tbCard_KeyDown(object sender, KeyEventArgs e)
        {
            //s1+= e.KeyValue.ToString();
            s1 += e.KeyCode.ToString();
            if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Escape)
            {
                poiskkart(1);
            }


        }

        #endregion


        #region Данные из набора в грид
        private void LoadToGrid()
        {
            foreach (DataGridViewRow denomCountRow in dgDenomCount.Rows)
            {
                if (countDenomDataSet.Tables[0].AsEnumerable().Any
                    (x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value)
                    && x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value))
                    )
                {

                    /////30.12.2019

                    /*
                    denomCountRow.Cells["Sum_value"].Value = countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                        x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                        x.Field<int>("source") == 0).Select(x => x.Field<Decimal>("fact_value")).FirstOrDefault<Decimal>()
                        + countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                        x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                        x.Field<int>("source") == 1).Select(x => x.Field<Decimal>("fact_value")).FirstOrDefault<Decimal>()
                        ;
                    */

                    denomCountRow.Cells["Sum_value"].Value = Convert.ToDouble(
                        countDenomDataSet.Tables[0].AsEnumerable().Where(
                       x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                       x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                       x.Field<int>("source") == 0).Select(x => x.Field<Decimal>("fact_value")).FirstOrDefault<Decimal>()
                       + countDenomDataSet.Tables[0].AsEnumerable().Where(
                       x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                       x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                       x.Field<int>("source") == 1).Select(x => x.Field<Decimal>("fact_value")).FirstOrDefault<Decimal>()
                       );

                    /////30.12.2019

                    denomCountRow.Cells["count_manual"].Value = countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                        x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                        x.Field<int>("source") == 1).Select(x => x.Field<Int32>("count")).FirstOrDefault();
                    // 1
                    denomCountRow.Cells["count_machine"].Value = countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                                            x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                                            x.Field<int>("source") == 0).Select(x => x.Field<Int32>("count")).FirstOrDefault();
                }
            }
        }
        #endregion


        #region Подготовка компонентов

        private void Prepare_Components()
        {
            dgDenomCount.DataSource = null;
            dgDenomCount.Rows.Clear();
            dgDenomCount.Columns.Clear();
            if (denominationDataSet == null)
                denominationDataSet = dataBase.GetSchema("t_g_denomination");

            dgDenomCount.AutoGenerateColumns = false;

            dgDenomCount.ColumnHeadersHeight = 10;
            dgDenomCount.RowHeadersWidth = 10;

            dgDenomCount.Columns.Add("Key-In", "Ввод");
            dgDenomCount.Columns["Key-In"].Visible = true;
            dgDenomCount.Columns["Key-In"].Width = 65;


            dgDenomCount.Columns.Add("id_denom", "Номинал");
            dgDenomCount.Columns["id_denom"].Visible = false;
            dgDenomCount.Columns["id_denom"].ReadOnly = true;
            dgDenomCount.Columns["id_denom"].DataPropertyName = "id_denom";


            dgDenomCount.Columns.Add("Denom", "Номинал");
            dgDenomCount.Columns["Denom"].Visible = true;
            //dgDenomCount.Columns["Denom"].Width = 65;
            dgDenomCount.Columns["Denom"].ReadOnly = true;
            dgDenomCount.Columns["Denom"].DataPropertyName = "name_denom";
            dgDenomCount.Columns["Denom"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgDenomCount.Columns.Add("id_condition", "Состояние");
            dgDenomCount.Columns["id_condition"].Visible = false;
            dgDenomCount.Columns["id_condition"].ReadOnly = true;
            dgDenomCount.Columns["id_condition"].DataPropertyName = "id_condition";//?????????


            dgDenomCount.Columns.Add("Condition", "Состояние");
            dgDenomCount.Columns["Condition"].Visible = true;
            //dgDenomCount.Columns["Condition"].Width = 65;
            dgDenomCount.Columns["Condition"].ReadOnly = true;
            dgDenomCount.Columns["Condition"].DataPropertyName = "name_condition";//?????????
            dgDenomCount.Columns["Condition"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //17.07.2020
            // dgDenomCount.Columns.Add("Count_manual", "Ручной ввод");
            dgDenomCount.Columns.Add("Count1", "Количество");
            dgDenomCount.Columns["Count1"].Visible = false;
            dgDenomCount.Columns["Count1"].Width = 65;
            dgDenomCount.Columns["Count1"].DataPropertyName = "Count1";
            dgDenomCount.Columns["Count1"].ReadOnly = true;


            dgDenomCount.Columns.Add("Sum1", "Сумма");
            dgDenomCount.Columns["Sum1"].Visible = false;
            dgDenomCount.Columns["Sum1"].Width = 65;
            dgDenomCount.Columns["Sum1"].ReadOnly = true;
            dgDenomCount.Columns["Sum1"].DataPropertyName = "Sum1";
            dgDenomCount.Columns["Sum1"].ReadOnly = true;


            // dgDenomCount.Columns.Add("Count_manual", "Ручной ввод");
            dgDenomCount.Columns.Add("Count_manual", "Ввод без пересчёта");
            dgDenomCount.Columns["Count_manual"].Visible = true;
            dgDenomCount.Columns["Count_manual"].Width = 65;
            dgDenomCount.Columns["Count_manual"].DataPropertyName = "Count_manual";
            dgDenomCount.Columns["Count_manual"].ReadOnly = true;

            dgDenomCount.Columns.Add("Count_machine", "Пересчет");
            dgDenomCount.Columns["Count_machine"].Visible = true;
            dgDenomCount.Columns["Count_machine"].Width = 65;
            dgDenomCount.Columns["Count_machine"].DataPropertyName = "Count_machine";
            dgDenomCount.Columns["Count_machine"].ReadOnly = true;

            dgDenomCount.Columns.Add("Sum_value", "Сумма");
            dgDenomCount.Columns["Sum_value"].Visible = true;
            dgDenomCount.Columns["Sum_value"].ReadOnly = true;
            dgDenomCount.Columns["Sum_value"].DataPropertyName = "Sum_value";
            dgDenomCount.Columns["Sum_value"].ReadOnly = true;



            /////28.11.2019

            dgDenomCount.Columns.Add("Cenn", "Тип");
            dgDenomCount.Columns["Cenn"].Visible = true;
            dgDenomCount.Columns["Cenn"].ReadOnly = true;
            dgDenomCount.Columns["Cenn"].DataPropertyName = "name_zenn";//?????????
            dgDenomCount.Columns["Cenn"].SortMode = DataGridViewColumnSortMode.NotSortable;

            /////28.11.2019

            //dgDenomCount.DataSource = denominationDataSet.Tables[0];

            if (!denominationDataSet.Tables[0].Columns.Contains("Count_manual"))
                denominationDataSet.Tables[0].Columns.Add("Count_manual");
            if (!denominationDataSet.Tables[0].Columns.Contains("Count_machine"))
                denominationDataSet.Tables[0].Columns.Add("Count_machine");
            if (!denominationDataSet.Tables[0].Columns.Contains("Sum_value"))
                denominationDataSet.Tables[0].Columns.Add("Sum_value");

            rblCurrency.DisplayMember = "curr_code";
            rblCurrency.ValueMember = "id";

            rblCondition.DisplayMember = "name";
            rblCondition.ValueMember = "id";

            /////28.11.2019
            rblCenn.DisplayMember = "name_zenn";
            rblCenn.ValueMember = "id";
            /////28.11.2019


            //////19.02.2020
            dgDenomCount.Columns["Sum_value"].DefaultCellStyle.Format = "### ### ### ### ### ### ### ###";
            dgDenomCount.Columns["Count_manual"].DefaultCellStyle.Format = "### ### ### ### ### ### ### ###";
            dgDenomCount.Columns["Count_machine"].DefaultCellStyle.Format = "### ### ### ### ### ### ### ###";
            dgDenomCount.Columns["Key-In"].DefaultCellStyle.Format = "### ### ### ### ### ### ### ###";

            //dgDenomCount.Columns["Denom"].DefaultCellStyle.Format = "### ### ### ###";
            //////19.02.2020

        }

        #endregion


        private void vubval1()
        {

            ///14.02.2020

            //if (rblCurrency.DataSource != null)
            //    tval1 = rblCurrency.SelectedValue.ToString();

            ///14.02.2020


            /////28.11.2019
            string sql1 = "";

            if (rblCenn.SelectedValue != null)
                if (rblCenn.SelectedValue.ToString() != "0")
                    sql1 = " and id_tipzen='" + rblCenn.SelectedValue.ToString() + "'";

            /////28.11.2019

            //////18.02.2020

            if (rblCondition.SelectedValue != null)
                if (rblCondition.SelectedValue.ToString() != "0")
                    sql1 = sql1 + " and t2.id = '" + rblCondition.SelectedValue.ToString() + "'";

            //////18.02.2020

            Prepare_Components();
            if (rblCurrency.DataSource != null)
            {




                denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", rblCurrency.SelectedValue.ToString());

                //////28.11.2019
                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           where cond.Field<Int64>("id") != 0
                                           ///01.11.2019
                                           && cond.Field<bool>("visible") == false
                                           ///01.11.2019



                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name"),
                                           }
                            ).OrderBy(x => x.id_condition).ToList();//denominationDataSet.Tables[0];
                */

                /////18.02.2020
                //  dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + " and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + " and 1=(case when exists(select 1 from t_g_user t20  where t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and t20.rasrprper1 = 1) then 0 else 1 end)) order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                //////28.11.2019

                ///17.02.2020
                //rblCondition.SelectedValue = 0;
                ///17.02.2020

                LoadToGrid();
                Sum_Count();
                dgDenomCount.Select();

                /////28.11.2019
                //dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
                /////28.11.2019
            }


        }

        #region Обработчик выбора валюты 
        private void rblCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {


            ///14.02.2020
            if (rasrval1 == 2)
            {
                if (rblCurrency.DataSource != null)
                    tval1 = rblCurrency.SelectedValue.ToString();
            }
            ///14.02.2020


            /////28.11.2019
            string sql1 = "";

            if (rblCenn.SelectedValue != null)
                if (rblCenn.SelectedValue.ToString() != "0")
                    sql1 = " and id_tipzen='" + rblCenn.SelectedValue.ToString() + "'";

            /////28.11.2019


            //////18.02.2020

            if (rblCondition.SelectedValue != null)
                if (rblCondition.SelectedValue.ToString() != "0")
                    sql1 = sql1 + " and t2.id = '" + rblCondition.SelectedValue.ToString() + "'";

            //////18.02.2020


            Prepare_Components();
            if (rblCurrency.DataSource != null)
            {




                denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", rblCurrency.SelectedValue.ToString());

                //////28.11.2019
                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           where cond.Field<Int64>("id") != 0
                                           ///01.11.2019
                                           && cond.Field<bool>("visible") == false
                                           ///01.11.2019



                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name"),
                                           }
                            ).OrderBy(x => x.id_condition).ToList();//denominationDataSet.Tables[0];
                */

                /////18.02.2020
                //   dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + " and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                    //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                    " and 1=(case when exists(select 1 from t_g_user t20  " +
                    "where " +
                    //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + 
                    //" and " +
                    " t20.rasrprper1 = 1) then 0 else 1 end)) " +
                    "order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                //////28.11.2019

                ///17.02.2020
                //  rblCondition.SelectedValue = 0;
                ///17.02.2020

                LoadToGrid();
                Sum_Count();
                dgDenomCount.Select();

                /////28.11.2019
                //dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
                /////28.11.2019
            }

        }
        #endregion


        #region Обработка ввода данных в поле грида
        private void dgDenomCount_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataSet = null;
            //dataSet = dataBase.GetData9("SELECT  t1.[id],[id_cashtransfer],t2.id_counting,t2.id_user_receive,[id_denomination],[id_condition],[workstation],[count],[fact_value] FROM [CountingDB].[dbo].[t_g_cashtransfer_detalization] t1 left join t_w_cashtransfer as t2 on t1.id_cashtransfer=t2.id where t2.source=1");
            bool keyin = false;

            long i2 = 0;
            if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value != null)
                if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                    if (Int64.TryParse(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString(), out i2))
                    {
                        // MessageBox.Show("1");

                    }
                    else
                    {
                        MessageBox.Show("Введите корректное число!");
                        dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;
                        return;
                    }
            if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value != null)
                if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                {
                    char c = dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString()[0];
                    if (c == '0')
                    {
                        MessageBox.Show("Проверьте номер вводимой КР");
                        dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;
                        return;
                    }

                }

            //if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value != null)
            //    if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
            //    {
            //        char c = dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString()[0];
            //        MessageBox.Show("c=" + c.ToString());
            //    }
            //dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value

            if (e.ColumnIndex == 0)
            {






                if (cbManualMachine.CheckState == CheckState.Checked)
                {
                    if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                        if (Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) + Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value) >= 0)
                        {
                            dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value = Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) + Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value);
                            keyin = true;
                        }
                }
                else
                {
                    if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                        if (Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) + Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value) >= 0)
                        {
                            dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value = Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) + Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value);
                            keyin = true;
                        }
                }
                dgDenomCount.Rows[e.RowIndex].Cells["Sum_value"].Value = (Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) + Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value)) * Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);

                //21.07.2020 ввод данных для омбарной книги

                if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty && keyin)
                {
                    dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value = Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value) + Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value);
                    dgDenomCount.Rows[e.RowIndex].Cells["Sum1"].Value = Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value) * Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);

                    //надо исправить ошибку 21,09,2020
                    /*
                    coun1 =
                    dataSet.Tables[0].AsEnumerable().Where
                    (
                    x => x.Field<Int64>("id_user_receive") == Convert.ToInt64(DataExchange.CurrentUser.CurrentUserId)
                    && x.Field<Int64>("id_counting") == Convert.ToInt64(counting_id)
                    && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value)
                    && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)
                    ).Select
                    (
                    x => x.Field<Int64>("count")
                    ).FirstOrDefault<Int64>().ToString();
                    */
                    // MessageBox.Show("1111  Count = " + coun1);

                }

                if (keyin)
                {
                    if (
                        countDenomDataSet.Tables[0].AsEnumerable().Any
                           (
                            x => x.Field<int>("source") == Convert.ToInt32(cbManualMachine.Checked)
                            && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value)
                            && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)
                           )
                        )
                    {
                        DataRow countDenomRow = countDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<int>("source") == Convert.ToInt32(cbManualMachine.Checked)
                        && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value)
                        && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)).First<DataRow>();

                        countDenomRow["lastupdate"] = DateTime.Now;
                        countDenomRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                        countDenomRow["count"] = cbManualMachine.CheckState == CheckState.Checked ? dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value : dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value;

                        countDenomRow["fact_value"] = cbManualMachine.CheckState == CheckState.Checked ? Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) * Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value) : Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) * Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);


                        long jk = 0;
                        if ((countDenomRow["flschet1"] != null) & (countDenomRow["flschet1"].ToString().Trim() != ""))
                            jk = Convert.ToInt64(countDenomRow["flschet1"].ToString());
                        countDenomRow["flschet1"] = jk + 1;

                        //////12.12.2019

                    }
                    else
                    {
                        DataRow countDenomRow = countDenomDataSet.Tables[0].NewRow();
                        countDenomRow["id_counting"] = counting_id;
                        countDenomRow["id_card"] = card_id;
                        countDenomRow["id_denomination"] = dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value;
                        countDenomRow["id_condition"] = dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value;
                        countDenomRow["creation"] = DateTime.Now;
                        countDenomRow["lastupdate"] = DateTime.Now;
                        countDenomRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                        countDenomRow["count"] = cbManualMachine.CheckState == CheckState.Checked ? dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value : dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value;
                        //countDenomRow["reject_count"]
                        countDenomRow["fact_value"] = cbManualMachine.CheckState == CheckState.Checked ? Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) * Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value) : Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) * Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);
                        //countDenomRow["status"] = 1;//пересчет завершен
                        countDenomRow["source"] = cbManualMachine.CheckState == CheckState.Checked ? 1 : 0; // 1 - пересчет с машины, 0 - вручную

                        /*
                        //////12.12.2019

                        int jk = 0;
                        if ((countDenomRow["flschet1"] != null) & (countDenomRow["flschet1"].ToString().Trim() != ""))
                            jk = Convert.ToInt32(countDenomRow["flschet1"].ToString());
                        countDenomRow["flschet1"] = jk + 1;

                        //////12.12.2019
                        */

                        countDenomDataSet.Tables[0].Rows.Add(countDenomRow);

                    }
                }

                //21.07.2020 
                if (keyin)
                {
                    /*if (
                        countDenomDataSet1.Tables[0].AsEnumerable().Any(
                            x => x.Field<Int64>("lastuserupdate") == Convert.ToInt64(DataExchange.CurrentUser.CurrentUserId)
                            && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value)
                            && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)
                            )
                        )
                    {

                        DataRow countDenomRow = countDenomDataSet1.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("lastuserupdate") == Convert.ToInt64(DataExchange.CurrentUser.CurrentUserId)
                        && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value)
                        && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)).First<DataRow>();

                        countDenomRow["lastupdate"] = DateTime.Now;
                        countDenomRow["lastuserupdate"] = DataExchange.CurrentUser.CurrentUserId;
                       // countDenomRow["count"] = Convert.ToInt32(coun1) +   надо исправить ошибку
                            Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value);
                       // countDenomRow["fact_value"] = (Convert.ToInt32(coun1) + Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value)) * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);

                        //countDenomDataSet.Tables[0].Rows.Add(countDenomRow);

                    }
                    else
                    {
                        DataRow countDenomRow = countDenomDataSet1.Tables[0].NewRow();

                        countDenomRow["id_denomination"] = dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value;
                        countDenomRow["id_condition"] = dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value;
                        countDenomRow["creation"] = DateTime.Now;
                        countDenomRow["lastupdate"] = DateTime.Now;
                        countDenomRow["lastuserupdate"] = DataExchange.CurrentUser.CurrentUserId;
                        countDenomRow["count"] = Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value);
                        countDenomRow["fact_value"] = Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count1"].Value) * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);



                        countDenomDataSet1.Tables[0].Rows.Add(countDenomRow);

                    }*/
                }


                Sum_Count();
                // MessageBox.Show(e.ToString());
                //if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value != null)
                //if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                //{
                //    char c = dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString()[0];
                //    if(c=='0')
                //    MessageBox.Show("Проверьте номер вводимой КР");
                //}
                dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;
            }

        }

        #endregion


        #region Закрытие формы

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


        #region Очистка формы

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Visible = false;
            fl_prov = -1;
            tbCard.Text = String.Empty;
            lblClientName.Text = String.Empty;
            lblCountNum.Text = String.Empty;
            dgDenomCount.DataSource = null;
            dgDenomCount.Rows.Clear();
            dgDenomCount.Columns.Clear();
            Prepare_Components();
            Sum_Count();
            tbCard.Select();
            rblCondition.DataSource = null;
            rblCondition.Items.Clear();
            rblCurrency.DataSource = null;
            rblCurrency.Items.Clear();

            /////28.11.2019
            ///19.02.2020
            rblCenn.Visible = false;
            //rblCenn.DataSource = null;
            //rblCenn.Items.Clear();
            ///19.02.2020
            /////28.11.2019



            countDenomDataSet.Tables[0].Rows.Clear();
            tbCard.Select();
        }

        #endregion


        #region Элемент выбора источника ввода

        private void cbManualMachine_Click(object sender, EventArgs e)
        {
            dgDenomCount.Select();
            if (dgDenomCount.Rows.Count > 0)
                dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
        }

        #endregion

        /////14.02.2020

        private void vubsost1()
        {



            ///14.02.2020
           // if (rblCondition.SelectedValue != null)
            //    tsost1 = rblCondition.SelectedValue.ToString();
            ///14.02.2020

            /////28.11.2019
            string sql1 = "";
            if (rblCenn.SelectedValue != null)
                if (rblCenn.SelectedValue.ToString() != "0")
                    sql1 = " and id_tipzen='" + rblCenn.SelectedValue.ToString() + "'";

            /////28.11.2019

            if (rblCondition.DataSource != null && rblCondition.SelectedValue.ToString() != "0")
            {

                /////28.11.2019

                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           
                                           where cond.Field<Int64>("id") == Convert.ToInt64(rblCondition.SelectedValue) && cond.Field<bool>("Visible") == false
                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name")
                                           }
                        ).OrderBy(x => x.id_condition).ToList();
                */

                /////18.02.2020
                //  dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + " and 1=(case when exists(select 1 from t_g_user t20  where t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and t20.rasrprper1 = 1) then 0 else 1 end)) order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                /////28.11.2019
            }
            else
            {
                /////28.11.2019
                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           
                                           where cond.Field<Int64>("id") != 0 && cond.Field<bool>("Visible") == false
                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name")
                                           }
                        ).OrderBy(x => x.id_condition).ToList();
                */

                /////18.02.2020
                //  dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + " and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + " and 1=(case when exists(select 1 from t_g_user t20  where t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and t20.rasrprper1 = 1) then 0 else 1 end)) order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                /////28.11.2019

            }
            LoadToGrid();
            dgDenomCount.Select();
            /////28.11.2019
            //  dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
            /////28.11.2019
            Sum_Count();




        }

        /////14.02.2020


        #region Обработчик выбора состояния банкнот

        private void rblCondition_SelectedIndexChanged(object sender, EventArgs e)
        {


            ///14.02.2020
            if (rblCondition.SelectedValue != null)
                tsost1 = rblCondition.SelectedValue.ToString();
            ///14.02.2020

            /////28.11.2019
            string sql1 = "";
            if (rblCenn.SelectedValue != null)
                if (rblCenn.SelectedValue.ToString() != "0")
                    sql1 = " and id_tipzen='" + rblCenn.SelectedValue.ToString() + "'";

            /////28.11.2019

            if (rblCondition.DataSource != null && rblCondition.SelectedValue.ToString() != "0")
            {

                /////28.11.2019

                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           
                                           where cond.Field<Int64>("id") == Convert.ToInt64(rblCondition.SelectedValue) && cond.Field<bool>("Visible") == false
                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name")
                                           }
                        ).OrderBy(x => x.id_condition).ToList();
                */

                /////18.02.2020
                // dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'"+ sql1+"  and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                    //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                    " and 1=(case when exists(select 1 from t_g_user t20  " +
                    "where " +
                    //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and" +
                    " t20.rasrprper1 = 1) then 0 else 1 end)) " +
                    "order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                /////28.11.2019
            }
            else
            {
                /////28.11.2019
                /*
                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           
                                           where cond.Field<Int64>("id") != 0 && cond.Field<bool>("Visible") == false
                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name")
                                           }
                        ).OrderBy(x => x.id_condition).ToList();
                */

                /////18.02.2020
                //  dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'"+ sql1+" and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                    //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                    " and 1=(case when exists(select 1 from t_g_user t20  " +
                    " where " +
                    //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and " +
                    " t20.rasrprper1 = 1) then 0 else 1 end)) " +
                    "order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

                /////28.11.2019

            }
            LoadToGrid();
            dgDenomCount.Select();
            /////28.11.2019
            //  dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
            /////28.11.2019
            Sum_Count();

        }

        #endregion


        #region Расчет сумм для купюр
        private void Sum_Count()
        {
            //////19.02.2020

            lblCountManual.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt64(x.Cells["Count_Manual"].Value == DBNull.Value ? 0 : x.Cells["Count_Manual"].Value)).ToString("### ### ### ### ### ### ### ###");
            lblCountMachine.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt64(x.Cells["Count_Machine"].Value == DBNull.Value ? 0 : x.Cells["Count_Machine"].Value)).ToString("### ### ### ### ### ### ### ###");
            lblSumValue.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt64(x.Cells["Sum_Value"].Value == DBNull.Value ? 0 : x.Cells["Sum_value"].Value)).ToString("### ### ### ### ### ### ### ###");

            // dgDenomCount.Columns["Denom"].DefaultCellStyle.Format = "### ### ### ###";


            //dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;

            //lblCountManual.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells["Count_Manual"].Value == DBNull.Value ? 0 : x.Cells["Count_Manual"].Value)).ToString();
            // lblCountMachine.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells["Count_Machine"].Value == DBNull.Value ? 0 : x.Cells["Count_Machine"].Value)).ToString();
            // lblSumValue.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells["Sum_Value"].Value == DBNull.Value ? 0 : x.Cells["Sum_value"].Value)).ToString();
            //////19.02.2020

        }

        #endregion


        #region Запись в БД
        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (counterfietTable != null)
            {
                int i9 = dataBase.UpdateDataCounterfiet(counterfietTable);
                //foreach(DataRow row in counterfietTable.Rows)
                //{

                //}
                //countDenomDataSet.Tables[0].Merge(counterfietTable);
                counterfietTable = null;
            }

            ///////01.11.2019
            List<string> zapros1 = new List<string>();
            // dataBase.InitTransaction();
            // dataBase.UpdateData(countDenomDataSet, "t_g_counting_denom");
            ///////01.11.2019

            long user_id = DataExchange.CurrentUser.CurrentUserId;
            long idCashCentre = DataExchange.CurrentUser.CurrentUserCentre;



            int i1 = dataBase.UpdateData8(countDenomDataSet, "t_g_counting_denom", zapros1);

            poiskkart(1);


            string s2 = "update t_g_cards set fl_obr=1, lastupdate=getdate(), last_user_update = " + DataExchange.CurrentUser.CurrentUserId + " where id='" + card_id.ToString() + "'";

            int i2 = dataBase.Zapros1(s2, "");

            //////29.11.2019

            /////03.12.2019

            DataSet dzak1 = dataBase.GetData9("select Count(*) as n1 from t_g_cards t1 where t1.id_bag in(SELECT max(id_bag)  FROM[CountingDB].[dbo].[t_g_cards] where id = '" + card_id.ToString() + "')  and (t1.fl_obr < 1 or t1.fl_obr > 1)");

            if (dzak1.Tables[0].Rows.Count > 0)
                if (dzak1.Tables[0].Rows[0]["n1"].ToString() == "0")
                {
                    /////30.12.2019
                    //    DataSet dsv1 = dataBase.GetData9("select case when (select sum(t1.denomcount * t2.value) from t_g_declared_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) where t1.id_counting in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "'))=(select sum(t1.count * t2.value) from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) where t1.id_counting in  (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "') ) then 2 else 1 end as n1");

                    //DataSet dsv1 = dataBase.GetData9("select case when (select sum(t1.declared_value) from t_g_counting_content t1  where t1.id_counting in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "'))= (select sum(t1.count * t2.value) from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) where t1.id_counting in  (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "') ) then case when (select sum(t1.denomcount * t2.value) from t_g_declared_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) where t1.id_counting in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "'))=(select sum(t1.count * t2.value) from t_g_counting_denom t1 inner join t_g_denomination t2 on (t1.id_denomination = t2.id) where t1.id_counting in  (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "') ) then 2 else 4 end else 1 end as n1");

                    DataSet dsv1 = dataBase.GetData9("select iif( fact_value= declared_value, 2 , 1) as n1 from t_g_counting_content t1 left join t_g_cards t3 on t1.id_bag=t3.id_bag where t3.id=" + card_id.ToString());
                    /////30.12.2019


                    if (dsv1.Tables[0].Rows.Count > 0)
                    {

                        ////06.01.2020
                        // string s3 = "update t_g_counting set fl_prov='"+ dsv1.Tables[0].Rows[0]["n1"] + "' where id in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "')";

                        foreach (DataRow row in dsv1.Tables[0].Rows)
                        {
                            fl_prov = Convert.ToInt32(row["n1"]);
                            if (fl_prov == 0)
                                break;
                            if (fl_prov == 1)
                                break;


                        }
                        string s3 = "update t_g_counting set lastupdate=getdate(),last_user_update=" + DataExchange.CurrentUser.CurrentUserId + ", fl_prov='" + fl_prov.ToString() + "' where id in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "')";
                        ////06.01.2020   

                        int i3 = dataBase.Zapros1(s3, "");

                    }
                    Console.WriteLine("code=" + rblCurrency.SelectedItem.ToString());
                    string scode = rblCurrency.SelectedItem.ToString();

                    //if (scode.Contains("KZT"))
                    //    Console.WriteLine("KZT есть в строке");
                    //Console.WriteLine("code=" + rblCurrency.SelectedItem.ToString().Substring(25, 3));
                    //if (scode.Contains("UZS"))
                    //{
                    //    Console.WriteLine("UZS есть в строке");

                    //    Console.WriteLine("code=" + rblCurrency.SelectedItem.ToString().Substring(24, 3));
                    //}

                    if (scode.Contains("UZS") | scode.Contains("KZT"))
                    //if (rblCurrency.SelectedValue.ToString().Trim() == "1002")
                    {
                        Console.WriteLine("есть в строке=" + scode.Substring(25, 4));
                        if (card)
                            if (dsv1 != null)
                                if (dsv1.Tables[0] != null)
                                    if (dsv1.Tables[0].Rows.Count > 0)

                                        if (fl_prov == 1)
                                        {


                                            dataSet = dataBase.GetData9("select t3.name name_bag,  t1.* from t_g_counting_content t1 " +
                                                                        " left join t_g_cards t2 on t1.id_bag=t2.id_bag " +
                                                                        " left join t_g_bags t3 on t1.id_bag = t3.id " +
                                                                        " where t2.name='" + tbCard.Text.Trim() + "' " +
                                                                        " and id_currency=" + rblCurrency.SelectedValue.ToString().Trim());

                                            DataSet dataSetCurrency = dataBase.GetData9("SELECT [curr_code] FROM [CountingDB].[dbo].[t_g_currency] where id="+ rblCurrency.SelectedValue.ToString().Trim());
                                            Int64 razsum = (Convert.ToInt64(dataSet.Tables[0].Rows[0]["fact_value"]) - Convert.ToInt64(dataSet.Tables[0].Rows[0]["declared_value"]));

                                            Int64 declared_value = Convert.ToInt64(dataSet.Tables[0].Rows[0]["declared_value"]);

                                            decimal fact = Convert.ToInt64(dataSet.Tables[0].Rows[0]["fact_value"]);
                                            string curren = dataSetCurrency.Tables[0].Rows[0][0].ToString();
                                            if(pm.VisiblePossibility(perm, listBox1))
                                                listBox1.Visible = true;
                                            listBox1.Items.Clear();
                                            listBox1.Items.Add("Имеется расхождения,");
                                            listBox1.Items.Add("Валюта " + curren);// rblCurrency.SelectedValue.ToString()) ;
                                            listBox1.Items.Add("Заявленная сумма - " + declared_value.ToString());
                                            listBox1.Items.Add("Посчитано - " + Math.Truncate(fact).ToString());
                                            if (razsum < 0)
                                                listBox1.Items.Add("Недостача " + razsum.ToString());
                                            else
                                                listBox1.Items.Add("Излишек - " + razsum.ToString());
                                            DialogResult result = MessageBox.Show(
                                                "Имеется расхождения, желаете выйти?"
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

                                            else
                                            {
                                                dataSet = dataBase.GetData9("select * from t_g_counting where discr_det_user is null and id in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "')");
                                                if (dataSet.Tables[0].Rows.Count > 0)
                                                    dataSet = dataBase.GetData9("update t_g_counting set discr_det_date=getdate(), discr_det_user=" + DataExchange.CurrentUser.CurrentUserId + " where id in (SELECT TOP (1) id_counting  FROM t_g_cards as  t1 where id='" + card_id.ToString() + "')");

                                            }
                                        }
                    }
                    else
                    {
                        //MessageBox.Show("fl_prov="+fl_prov.ToString());

                        dataSet = dataBase.GetData9("select t3.name name_bag,  t1.* from t_g_counting_content t1 " +
                            " left join t_g_cards t2 on t1.id_bag=t2.id_bag " +
                            " left join t_g_bags t3 on t1.id_bag = t3.id " +
                            " where t2.name='" + tbCard.Text.Trim() + "' " +
                            " and id_currency=" + rblCurrency.SelectedValue.ToString().Trim());
                        string razsum = (Convert.ToInt64(dataSet.Tables[0].Rows[0]["fact_value"]) - Convert.ToInt64(dataSet.Tables[0].Rows[0]["declared_value"])).ToString().Trim();
                        string st = dataSet.Tables[0].Rows[0]["st"].ToString().Trim();
                        string name_bag = dataSet.Tables[0].Rows[0]["name_bag"].ToString().Trim();
                        string declared_value = Convert.ToInt64(dataSet.Tables[0].Rows[0]["declared_value"]).ToString().Trim();
                        string id_counting = dataSet.Tables[0].Rows[0]["id_counting"].ToString().Trim();
                        string fact = dataSet.Tables[0].Rows[0]["fact_value"].ToString();
                        // MessageBox.Show(st);
                        if (st == "2")
                        {
                            string id_acc = dataSet.Tables[0].Rows[0]["id_account"].ToString().Trim();
                            dataSet = dataBase.GetData9("select t3.name from t_g_client t1   left join t_g_account t2 on t1.id=t2.id_client   left join t_g_clisubfml t3 on t1.id_subfml=t3.id   where t2.id=" + id_acc);
                            string subfml = dataSet.Tables[0].Rows[0]["name"].ToString().Trim();
                            dataSet = dataBase.GetData9("select sum(t1.fact_value) fact_value, t3.user_name from t_g_counting_denom t1 left join t_g_cards t2 on t1.id_card=t2.id left join t_g_user t3 on t1.last_user_update=t3.id where t2.name='" + tbCard.Text.Trim() + "'  and t3.id_role=1 group by t3.user_name");
                            string CCM1 = "";
                            if (dataSet.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in dataSet.Tables[0].Rows)
                                    CCM1 += "\n\t" + row["user_name"].ToString().Trim() + " на сумму - " + Convert.ToInt64(row["fact_value"]).ToString().Trim();

                            }
                            string multi_bag = "";
                            dataSet = dataBase.GetData9("SELECT t2.name, t1.* FROM t_g_multi_bags_content t1  left join t_g_multi_bags t2 on t1.id_multi_bag=t2.id left join t_g_counting t3 on t1.id_multi_bag=t3.id_multi_bag where t3.id=" + id_counting +
                                " and t1.id_currency= " + rblCurrency.SelectedValue.ToString());
                            if (dataSet.Tables[0].Rows.Count > 0)
                                multi_bag = "\nНомер мульти сумки:  " + dataSet.Tables[0].Rows[0]["name"].ToString().Trim() + ", \nЗаявленная сумма мульти сумки:  " + Convert.ToInt64(dataSet.Tables[0].Rows[0]["declared_value1"]).ToString().Trim();
                            if(pm.VisiblePossibility(perm, listBox1))
                                listBox1.Visible = true;
                            listBox1.Items.Clear();
                            listBox1.Items.Add("Имеется расхождения по этой валюте");
                            listBox1.Items.Add("Заявленная сумма - " + declared_value);
                            listBox1.Items.Add("Посчитано - " + fact);
                            listBox1.Items.Add("Расхождение - " + razsum);




                            DialogResult result = MessageBox.Show(
                                                "Имеется расхождения по этой валюте, желаете выйти? " +
                                                "\nТип клиента:  " + subfml +
                                                "\nC машины пришло: " + CCM1 +
                                                "\nНомер сумки:  " + name_bag +
                                                "\nЗаявленная сумма сумки:  " + declared_value +
                                                "\nРасхождение на сумму: " + razsum +
                                                 multi_bag


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

                        }
                        else
                        if (fl_prov == 1)
                        {
                            DialogResult result1 = MessageBox.Show(
                                           "Имеется расхождения, желаете выйти?"
                                           ,
                                           "Сообщение",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Information,
                                           MessageBoxDefaultButton.Button1
                                           //,
                                           //MessageBoxOptions.DefaultDesktopOnly
                                           );

                            if (result1 == DialogResult.No)

                                return;
                        }

                    }



                }


            btnClear_Click(sender, e);
            btnFalse.Enabled = false;
            button2.Enabled = false;

            /////03.12.2019


            //  countDenomDataSet.AcceptChanges();
            // dataBase.TransactionCommit();

            /////03.12.2019
            /*
            if (i1==1)
                MessageBox.Show("Операция выполнена!");
            else
                MessageBox.Show("Ошибка операции!");
            */
            /////03.12.2019

            ///////01.11.2019

            /*
            //Если есть данные по данному номиналу
            */
        }
        #endregion

        #region Кнопка учета сомнительных номиналов
        private void btnFalse_Click(object sender, EventArgs e)
        {
            ParentForms.Counterfeit counterfeit = new ParentForms.Counterfeit(denominationDataSet, counting_id, card_id);
            counterfeit.ShowDialog();
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Visible = false;
            ///////06.11.2019
            //poiskkart();

            poiskkart(1);
            //btnClear_Click(sender, e);
            ///////06.11.2019
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ///////06.11.2019
            poiskkart(2);
            //btnClear_Click(sender, e);
            ///////06.11.2019
        }

        /////14.02.2020
        private void vubcenn1()
        {
            ///14.02.2020
            if (rblCenn.SelectedValue != null)
                ttip1 = rblCenn.SelectedValue.ToString();
            ///14.02.2020

            string sql1 = "";
            if (rblCenn.SelectedValue != null)
                if (rblCenn.SelectedValue.ToString() != "0")
                    sql1 = " and id_tipzen='" + rblCenn.SelectedValue.ToString() + "'";



            if (rblCondition.DataSource != null && rblCondition.SelectedValue.ToString() != "0")
            {


                /////18.02.2020
                //  dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                    //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                    " and 1=(case when exists(select 1 from t_g_user t20  " +
                    "where " +
                    //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + 
                    //" and " +
                    "t20.rasrprper1 = 1) then 0 else 1 end)) " +
                    "order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020


            }
            else
            {

                if (rblCurrency.SelectedValue != null)

                    /////18.02.2020
                    //     dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + " and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                    dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                        //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                        " and 1=(case when exists(select 1 from t_g_user t20  " +
                        "where " +
                        //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and " +
                        "t20.rasrprper1 = 1) then 0 else 1 end)) " +
                        "order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

            }
            LoadToGrid();
            dgDenomCount.Select();

            Sum_Count();


        }
        /////14.02.2020

        /////28.11.2019
        private void RblCenn_SelectedIndexChanged(object sender, EventArgs e)
        {

            ///14.02.2020
            if (rblCenn.SelectedValue != null)
                ttip1 = rblCenn.SelectedValue.ToString();
            ///14.02.2020

            string sql1 = "";
            if (rblCenn.SelectedValue != null)
                if (rblCenn.SelectedValue.ToString() != "0")
                    sql1 = " and id_tipzen='" + rblCenn.SelectedValue.ToString() + "'";



            if (rblCondition.DataSource != null && rblCondition.SelectedValue.ToString() != "0")
            {


                /////18.02.2020
                //  dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "' and t2.id='" + rblCondition.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                    //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                    " and 1=(case when exists(select 1 from t_g_user t20  " +
                    "where " +
                    //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + 
                    //" and " +
                    " t20.rasrprper1 = 1) then 0 else 1 end)) order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

            }
            else
            {

                if (rblCurrency.SelectedValue != null)

                    /////18.02.2020
                    // dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + " and t2.id is not null and t2.visible <> 1 order by t2.id").Tables[0];

                    dgDenomCount.DataSource = dataBase.GetData9("select t1.id as id_denom,t1.name as name_denom, t2.id as  id_condition, t2.name as name_condition,id_tipzen,name_zenn from  t_g_condition as t2,t_g_denomination as  t1 left join t_g_tipzenn as t3 on (t1.id_tipzen=t3.id) where t1.id_currency='" + rblCurrency.SelectedValue.ToString() + "'" + sql1 + "  and t2.id is not null and t2.visible <> 1 and not exists(select t10.id_denomination,t10.id_condition,t11.id_currency from t_g_counting_denom t10 inner join t_g_denomination t11 on(t10.id_denomination=t11.id) where t10.id_card =" + card_id.ToString() + " and t10.id_denomination=t1.id and t10.id_condition=t2.id and t11.id_currency=t1.id_currency " +
                        //"and t10.last_user_update <> " + DataExchange.CurrentUser.CurrentUserId + 
                        " and 1=(case when exists(select 1 from t_g_user t20  where " +
                        //"t20.id =  " + DataExchange.CurrentUser.CurrentUserId + " and " +
                        " t20.rasrprper1 = 1) then 0 else 1 end)) order by name_condition desc, id_denom").Tables[0];

                /////18.02.2020

            }
            LoadToGrid();
            dgDenomCount.Select();

            Sum_Count();

        }

        private void CountingForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void tbCard_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void tbCard_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(s1);
        }

        private void dgDenomCount_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.KeyCode == Keys.Escape)
            // MessageBox.Show("11111111");
        }
        /////28.11.2019
    }
}
