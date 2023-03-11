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

namespace CountingForms
{
    public partial class RejectForm : Form
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
        private DataSet conditionDataSet = null;

        private DataSet counterfeitDataSet = null;
        private DataSet factorNotesDataSet = null;
      

        private DataSet denominationDataSet = null;
        private DataSet clientDataSet = null;
        private Int64 counting_id = -1;
        private Int64 client_id = -1;
        private Int64 selectedCurrency = -1;
        private Int64 card_id = -1;
        private DataTable counterfietTable = null;

        #region Конструктор формы
        public RejectForm()
        {
            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase = new MSDataBase();
            dataBase.Connect();
            InitializeComponent();
            currencyDataSet = dataBase.GetData("t_g_currency");
            conditionDataSet = dataBase.GetData("t_g_condition");
            countDenomDataSet = dataBase.GetSchema("t_g_counting_denom");
            Prepare_Components();
            



        }

        #endregion

        private async void RejectForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        #region Обрабочик нажатия клавиши Esc для ввода КР
        private void tbCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && tbCard.Text != String.Empty)
            {
                e.SuppressKeyPress = true;
                cardsDataSet = dataBase.GetData("t_g_cards", "name", tbCard.Text);
                if (cardsDataSet.Tables[0].Rows.Count > 0)
                {
                    
                    counting_id = cardsDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_counting")).First<Int64>();
                    
                    countingDataSet = dataBase.GetData("t_g_counting", "id", counting_id.ToString());
                    countContentDataSet = dataBase.GetData("t_g_counting_content", "id_counting", counting_id.ToString());
                    client_id = countingDataSet.Tables[0].AsEnumerable().Select(x => x.Field<Int64>("id_client")).First<Int64>();
                    clientDataSet = dataBase.GetData("t_g_client", "id", client_id.ToString());
                    card_id = cardsDataSet.Tables[0].Rows[cardsDataSet.Tables[0].Rows.Count - 1].Field<Int64>("Id");
                    countDenomDataSet = dataBase.GetData("t_g_counting_denom", "id_card", card_id.ToString());
                    //Заполнение кнопок выбора валют
                    rblCurrency.DataSource = (from curr in currencyDataSet.Tables[0].AsEnumerable()
                                              join count in countContentDataSet.Tables[0].AsEnumerable() on curr.Field<Int64>("id")                 equals count.Field<Int64>("id_currency")
                                              select new
                                              {
                                                  id = curr.Field<Int64>("id"),
                                                  //name = curr.Field<String>("name"),
                                                  curr_code = curr.Field<String>("curr_code")
                                              }
                                              ).Distinct().ToList();

                    if (conditionDataSet.Tables[0].Rows[conditionDataSet.Tables[0].Rows.Count - 1]["name"].ToString() != "Все")
                    {
                        DataRow conditionAllRow = conditionDataSet.Tables[0].NewRow();
                        conditionAllRow["id"] = 0;
                        conditionAllRow["name"] = "Все";
                        conditionAllRow["visible"] = false;
                        //conditionAllRow["curr_code"] = "Все";
                        conditionDataSet.Tables[0].Rows.Add(conditionAllRow);

                    }
                    rblCondition.DataSource = conditionDataSet.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == false).CopyToDataTable<DataRow>();
                    rblCondition.SelectedValue = 0;
                    //var prop = rblCurrency.SelectedValue.GetType().GetProperty("id").GetValue(rblCurrency.SelectedValue);
                    denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", rblCurrency.SelectedValue.ToString());


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
                    LoadToGrid();
                    lblCountNum.Text = countingDataSet.Tables[0].Rows[0].Field<string>("name").ToString();
                    lblClientName.Text = clientDataSet.Tables[0].Rows[0].Field<string>("name").ToString();
                    dgDenomCount.Select();
                    dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
                }
                else
                {
                    MessageBox.Show("Карточка не найдена");
                }

                Sum_Count();
            }
        }

        #endregion


        #region Данные из набора в грид
        private void LoadToGrid()
        {
            foreach (DataGridViewRow denomCountRow in dgDenomCount.Rows)
            {
                if (countDenomDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) && x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value)))
                {
                    denomCountRow.Cells["Sum_value"].Value = countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                        x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                        x.Field<int>("source") == 0).Select(x => x.Field<Decimal>("fact_value")).FirstOrDefault<Decimal>()
                        + countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                        x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                        x.Field<int>("source") == 1).Select(x => x.Field<Decimal>("fact_value")).FirstOrDefault<Decimal>()
                        ;
                    denomCountRow.Cells["count_manual"].Value = countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                        x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                        x.Field<int>("source") == 1).Select(x => x.Field<int>("count")).FirstOrDefault();

                    denomCountRow.Cells["count_machine"].Value = countDenomDataSet.Tables[0].AsEnumerable().Where(
                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomCountRow.Cells["id_denom"].Value) &&
                                            x.Field<Int64>("id_condition") == Convert.ToInt64(denomCountRow.Cells["id_condition"].Value) &&
                                            x.Field<int>("source") == 0).Select(x => x.Field<int>("count")).FirstOrDefault();
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

            dgDenomCount.Columns.Add("Key-In", "Ввод");
            dgDenomCount.Columns["Key-In"].Visible = true;
            dgDenomCount.Columns["Key-In"].Width = 65;


            dgDenomCount.Columns.Add("id_denom", "Номинал");
            dgDenomCount.Columns["id_denom"].Visible = false;
            dgDenomCount.Columns["id_denom"].ReadOnly = true;
            dgDenomCount.Columns["id_denom"].DataPropertyName = "id_denom";

            dgDenomCount.Columns.Add("Denom", "Номинал");
            dgDenomCount.Columns["Denom"].Visible = true;
            dgDenomCount.Columns["Denom"].ReadOnly = true;
            dgDenomCount.Columns["Denom"].DataPropertyName = "name_denom";

            dgDenomCount.Columns.Add("id_condition", "Состояние");
            dgDenomCount.Columns["id_condition"].Visible = false;
            dgDenomCount.Columns["id_condition"].ReadOnly = true;
            dgDenomCount.Columns["id_condition"].DataPropertyName = "id_condition";//?????????

            dgDenomCount.Columns.Add("Condition", "Состояние");
            dgDenomCount.Columns["Condition"].Visible = true;
            dgDenomCount.Columns["Condition"].ReadOnly = true;
            dgDenomCount.Columns["Condition"].DataPropertyName = "name_condition";//?????????
  
            dgDenomCount.Columns.Add("Count_manual", "Ручной ввод");
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



           

        }

        #endregion

        
        #region Обработчик выбора валюты 
        private void rblCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            Prepare_Components();
            if (rblCurrency.DataSource != null)
            {
                denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", rblCurrency.SelectedValue.ToString());


                dgDenomCount.DataSource = (from denom in denominationDataSet.Tables[0].AsEnumerable()
                                           from cond in conditionDataSet.Tables[0].AsEnumerable()
                                           where cond.Field<Int64>("id") != 0
                                           select new
                                           {
                                               id_denom = denom.Field<Int64>("id"),
                                               name_denom = denom.Field<string>("name"),
                                               id_condition = cond.Field<Int64>("id"),
                                               name_condition = cond.Field<string>("name"),
                                           }
                            ).OrderBy(x => x.id_condition).ToList();//denominationDataSet.Tables[0];
                rblCondition.SelectedValue = 0;
                LoadToGrid();
                Sum_Count();
                dgDenomCount.Select();
                dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
            }
            
        }
        #endregion


        #region Обработка ввода данных в поле грида
        private void dgDenomCount_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (cbManualMachine.CheckState == CheckState.Checked)
                {
                    if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                        dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value = Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) + Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value);
                    
                }
                else
                {
                    if (dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value.ToString() != String.Empty)
                        dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value = Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) + Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value);
                    
                }
                dgDenomCount.Rows[e.RowIndex].Cells["Sum_value"].Value = (Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) + Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value))  * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);



                if (countDenomDataSet.Tables[0].AsEnumerable().Any(x => x.Field<int>("source") == Convert.ToInt32(cbManualMachine.Checked) && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value) && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)))
                {
                    DataRow countDenomRow = countDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<int>("source") == Convert.ToInt32(cbManualMachine.Checked) && x.Field<Int64>("id_denomination") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value) && x.Field<Int64>("id_condition") == Convert.ToInt64(dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value)).First<DataRow>();
                    //countDenomRow["id_counting"] = counting_id;
                    //countDenomRow["id_card"] = card_id;
                    //countDenomRow["id_denomination"] = dgDenomCount.Rows[e.RowIndex].Cells["id_denom"].Value;
                    //countDenomRow["id_condition"] = dgDenomCount.Rows[e.RowIndex].Cells["id_condition"].Value;
                    //countDenomRow["creation"] = DateTime.Now;
                    countDenomRow["lastupdate"] = DateTime.Now;
                    countDenomRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                    countDenomRow["count"] = cbManualMachine.CheckState == CheckState.Checked ? dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value : dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value;
                    //countDenomRow["reject_count"]
                    countDenomRow["fact_value"] = cbManualMachine.CheckState == CheckState.Checked ? Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value) : Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);
                    //countDenomRow["status"] = cbManualMachine.CheckState == CheckState.Checked ? 1 : 0;
                    //countDenomDataSet.Tables[0].Rows.Add(countDenomRow);
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
                    countDenomRow["fact_value"] = cbManualMachine.CheckState == CheckState.Checked ? Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_manual"].Value) * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value) : Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Count_machine"].Value) * Convert.ToInt32(dgDenomCount.Rows[e.RowIndex].Cells["Denom"].Value);
                    countDenomRow["status"] = 1;//пересчет завершен
                    countDenomRow["source"] = cbManualMachine.CheckState == CheckState.Checked ? 1 : 0; // 1 - пересчет с машины, 0 - вручную
                    countDenomDataSet.Tables[0].Rows.Add(countDenomRow);

                }
                Sum_Count();
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
            countDenomDataSet.Tables[0].Rows.Clear();
            tbCard.Select();
        }

        #endregion


        #region Элемент выбора источника ввода

        private void cbManualMachine_Click(object sender, EventArgs e)
        {
            dgDenomCount.Select();
            if(dgDenomCount.Rows.Count > 0)
                 dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
        }

        #endregion


        #region Обработчик выбора состояния банкнот

        private void rblCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCondition.DataSource != null && rblCondition.SelectedValue.ToString() != "0")
            {

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
            }
            else
            {
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

            }
            LoadToGrid();
            dgDenomCount.Select();
            dgDenomCount.CurrentCell = dgDenomCount.Rows[0].Cells["Key-In"];
            Sum_Count();
            
        }

        #endregion


        #region Расчет сумм для купюр
        private void Sum_Count()
        {
            //dgDenomCount.Rows[e.RowIndex].Cells["Key-In"].Value = String.Empty;
            lblCountManual.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells["Count_Manual"].Value == DBNull.Value ? 0 : x.Cells["Count_Manual"].Value)).ToString();
            lblCountMachine.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells["Count_Machine"].Value == DBNull.Value ? 0 : x.Cells["Count_Machine"].Value)).ToString();
            lblSumValue.Text = dgDenomCount.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells["Sum_Value"].Value == DBNull.Value ? 0 : x.Cells["Sum_value"].Value)).ToString();
        }

        #endregion


        #region Запись в БД
        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (counterfietTable != null)
            {
                countDenomDataSet.Tables[0].Merge(counterfietTable);
                counterfietTable = null;
            }
            dataBase.UpdateData(countDenomDataSet, "t_g_counting_denom");
            /*
            long user_id = DataExchange.CurrentUser.CurrentUserId;

            DataTable cash = dataBase.GetCash(user_id, idDenom, idCashCentre);

            //Если есть данные по данному номиналу
            if (cash.Rows.Count > 0)
            {
                //Считывыние LINK 
                long cash_id = Convert.ToInt64(cash.Rows[0]["id"]);
                //Считывание номера изменений
                int seqNumber = Convert.ToInt32(cash.Rows[0]["SEQNUMBER"]);
                //Расчет еоличества номиналов
                long countCash = Convert.ToInt64(cash.Rows[0]["COUNT"]) + Convert.ToInt64(counter["Number"]);
                //Дата и время последних изменений
                DateTime cashLastUpdate = Convert.ToDateTime(cash.Rows[0]["LASTUPDATE"]);
                //Обновление данных номинала по пользователю
                dataBase.UpdateCashTransaction(cash_id, user_id, cashLastUpdate, countCash);
            }
            else //Если номинала по пользователю в БД 
            {
                //Формирование LINK для номинала по пользователю
                //string cashLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                //seqInc++;
                //Добавление данных номинала по пользователю
                dataBase.InsertCashTransaction(user_id, idDenom, idCashCentre, Convert.ToInt32(counter["Number"]));
            }*/
        }
        #endregion

        #region Кнопка учета сомнительных номиналов
        private void btnFalse_Click(object sender, EventArgs e)
        {
            Int64 condition_id = conditionDataSet.Tables[0].AsEnumerable().Where(x => x.Field<bool>("visible") == true).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
            if (counterfietTable == null)
            {
                counterfietTable = countDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_condition") == condition_id).Any() ? countDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_condition") == condition_id).CopyToDataTable() : countDenomDataSet.Tables[0].Clone();
            }
            ParentForms.Counterfeit counterfeit = new ParentForms.Counterfeit(denominationDataSet,  counting_id, card_id);
            counterfeit.ShowDialog();

            

        }
        #endregion

      
    }
}
