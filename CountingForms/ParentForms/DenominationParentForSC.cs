using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataExchange;
using CountingDB;



namespace CountingForms.ParentForms
{
    public partial class DenominationParentForSC : Form
    {
        private DataSet denominationDataSet = null;
        private DataSet conditionDataSet = null;
        private DataSet currencyDataSet = null;        
        private DataSet countDenomDataSet = null;
        public DataSet CountDenomDataSet { get; set; }
       
        private long id_currency;
        private long id_account;
        private string tableName;
        private int a = 0; //для сомнительных номиналов
        private int b = 1;  // для фиксации введенных данных: 1 -без состояния, 2 - состоянем без сомн. 3 - сосотоянием с сомн. 
        public int b_get { get; set; }
        private bool c = false;    // для фиксации введенных данных 
        private int obshkol1;
        private double obshsum1;
        private int nalcol;
        private double nalsum;
        private int nalcol44;
        private double nalsum44;
        
        

        public DenominationParentForSC()
        {
            InitializeComponent();
            b = 1;

        }        

        public DenominationParentForSC(string tableName, string id_currency, string id_account, DataSet countDenomDataSet = null, int b = 1)
        {
            

            InitializeComponent();
            
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();
            this.tableName = tableName;
            denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
            
            this.b_get = b;
            b = b_get;
            
            if (countDenomDataSet != null)
            c = true;
            nalcol = 0;
            nalsum = 0;
            nalcol44 = 0;
            nalsum44 = 0;

           
            {
                conditionDataSet = dataBase.GetData("t_g_condition");

                currencyDataSet = dataBase.GetData("t_g_currency");

                this.id_account = Convert.ToInt64(id_account);
                this.id_currency = Convert.ToInt64(id_currency);


                dgDenomination.Columns.Add("Count1", "Ввод кол-ва");
                dgDenomination.Columns["Count1"].Visible = true;

                dgDenomination.Columns["Count1"].Width = 65;
                dgDenomination.Columns["Count1"].DataPropertyName = "Count1";


                dgDenomination.Columns.Add("Count", "Кол-во");
                dgDenomination.Columns["Count"].Visible = true;
                dgDenomination.Columns["Count"].Width = 65;
                dgDenomination.Columns["Count"].ReadOnly = true;
                dgDenomination.Columns["Count"].DataPropertyName = "Count";


                dgDenomination.Columns.Add("id_denom", "Номинал");
                dgDenomination.Columns["id_denom"].Visible = false;
                dgDenomination.Columns["id_denom"].ReadOnly = true;
                dgDenomination.Columns["id_denom"].DataPropertyName = "id";
                
                dgDenomination.Columns.Add("Denom", "Номинал");
                dgDenomination.Columns["Denom"].Visible = true;
                dgDenomination.Columns["Denom"].ReadOnly = true;
                dgDenomination.Columns["Denom"].Width = 80;
                dgDenomination.Columns["Denom"].DataPropertyName = "name";


                DataGridViewComboBoxColumn currencyColumn = new DataGridViewComboBoxColumn();
                currencyColumn.Name = "Currency";
                currencyColumn.HeaderText = "Валюта";
                currencyColumn.ValueMember = "id";
                currencyColumn.DisplayMember = "name";
                currencyColumn.DataPropertyName = "name";
                currencyColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                dgDenomination.Columns.Add(currencyColumn);
                dgDenomination.Columns["Currency"].ReadOnly = true;
                dgDenomination.Columns["Currency"].Visible = true;
                dgDenomination.Columns["Currency"].Width = 90;
                dgDenomination.Columns["Currency"].DataPropertyName = "id_currency";
                currencyColumn.DataSource = currencyDataSet.Tables[0];


                DataGridViewComboBoxColumn dgcondition = new DataGridViewComboBoxColumn();
                dgcondition.Name = "id_condition";
                dgcondition.HeaderText = "Состояние";
                dgcondition.FlatStyle = FlatStyle.Flat;
                dgcondition.DataPropertyName = "id_condition";
                dgcondition.DisplayMember = "name";
                dgcondition.ValueMember = "id";
                dgcondition.ReadOnly = true;
                dgcondition.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dgcondition.DataSource = conditionDataSet.Tables[0];
                dgDenomination.Columns.Add(dgcondition);
                dgDenomination.Columns["id_condition"].Visible = true;
                dgDenomination.Columns["id_condition"].Width = 90;

                dgDenomination.Columns.Add("Sum_value", "Сумма");
                dgDenomination.Columns["Sum_value"].Visible = true;
                dgDenomination.Columns["Sum_value"].ReadOnly = true;
                dgDenomination.Columns["Sum_value"].DataPropertyName = "Sum_value";

                dgDenomination.Columns.Add("Count22", "Количество");
                dgDenomination.Columns["Count22"].Visible = true;
                dgDenomination.Columns["Count22"].ReadOnly = true;
                dgDenomination.Columns["Count22"].DataPropertyName = "Count22";

                dgDenomination.Columns.Add("Sum_value1", "Сумма");
                dgDenomination.Columns["Sum_value1"].Visible = true;
                dgDenomination.Columns["Sum_value1"].ReadOnly = true;
                dgDenomination.Columns["Sum_value1"].DataPropertyName = "Sum_value1";

                dgDenomination.Columns.Add("Count33", "Количество");
                dgDenomination.Columns["Count33"].Visible = true;
                dgDenomination.Columns["Count33"].ReadOnly = true;
                dgDenomination.Columns["Count33"].DataPropertyName = "Count33";

                dgDenomination.Columns.Add("sum33", "Сумма");
                dgDenomination.Columns["sum33"].Visible = true;
                dgDenomination.Columns["sum33"].ReadOnly = true;
                dgDenomination.Columns["sum33"].DataPropertyName = "sum33";

                dgDenomination.Columns.Add("Count44", "Количество");
                dgDenomination.Columns["Count44"].Visible = true;
                dgDenomination.Columns["Count44"].ReadOnly = true;
                dgDenomination.Columns["Count44"].DataPropertyName = "Count44";

                dgDenomination.Columns.Add("sum44", "Сумма");
                dgDenomination.Columns["sum44"].Visible = true;
                dgDenomination.Columns["sum44"].ReadOnly = true;
                dgDenomination.Columns["sum44"].DataPropertyName = "sum44";

                dgDenomination.AutoGenerateColumns = false;

                dgDenomination.Columns["Count22"].DefaultCellStyle.Format = "### ### ### ###";
                dgDenomination.Columns["Sum_value1"].DefaultCellStyle.Format = "### ### ### ###";
                dgDenomination.Columns["Count33"].DefaultCellStyle.Format = "### ### ### ###";
                dgDenomination.Columns["sum33"].DefaultCellStyle.Format = "### ### ### ###";
                dgDenomination.Columns["Count44"].DefaultCellStyle.Format = "### ### ### ###";
                dgDenomination.Columns["sum44"].DefaultCellStyle.Format = "### ### ### ###";

                dgDenomination.AutoResizeColumns();

                dgDenomination.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                dgDenomination.ColumnHeadersHeight = this.dgDenomination.ColumnHeadersHeight * 3;

                dgDenomination.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



                dgDenomination.Paint += new PaintEventHandler(dgDenomination_Paint);



                dgDenomination.Scroll += new ScrollEventHandler(dgDenomination_Scroll);

                dgDenomination.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgDenomination_ColumnWidthChanged);
            }
                      




            if (b == 1)
            {
                //MessageBox.Show("WEEFWF "+b.ToString());
                dgDenomination.Columns["id_condition"].Visible = false;
                denominationDataSet = dataBase.GetData9("PROC_BALANCE_ALL2 " + id_account + ", " + id_currency);
                //denominationDataSet = dataBase.GetData9("PROC_BALANCE2 " + id_account + ", " + id_currency);

                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
            else if (b == 2)
            {
               // MessageBox.Show("WEEFWF " + b.ToString());
                a = 0;
                dgDenomination.Columns["id_condition"].Visible = true;
                denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
                checkBox1.Checked = true;
                checkBox2.Checked = false;
            }
            else if (b == 3)
            {
                //MessageBox.Show("WEEFWF " + b.ToString());
                a = 20007;
                dgDenomination.Columns["id_condition"].Visible = true;
                denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
                checkBox1.Checked = true;
                checkBox2.Checked = true;
            }

            foreach (DataRow denomRow in denominationDataSet.Tables[0].Rows)
            {
                nalcol += Convert.ToInt32(denomRow["Count22"]);
                nalsum += Convert.ToDouble(denomRow["Sum_value1"]);

                nalcol44 += Convert.ToInt32(denomRow["Count44"]);
                nalsum44 += Convert.ToDouble(denomRow["sum44"]);
            }


                SelectInfo(//"0", tableName,  id_account, id_currency, 
                denominationDataSet, countDenomDataSet);            
            
            

        }        

        private  void SelectInfo(DataSet denominationDataSet, DataSet countDenomDataSet = null)
        
        {
            if (checkBox1.Checked == false) b = 1;
            else if (checkBox1.Checked == true & checkBox2.Checked == false) b = 2;
            else if (checkBox2.Checked == true) b = 3;
            else MessageBox.Show("Появилясь какая та ошибка в условиях CheckBox -ов, в разделе SelectInfo!!! ");

            dgDenomination.DataSource = denominationDataSet.Tables[0]; 
            
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            if (denominationDataSet.Tables[0].Columns.Contains("Count") != true)            
            {
                denominationDataSet.Tables[0].Columns.Add("Count", typeof(Int16));
                denominationDataSet.Tables[0].Columns.Add("Sum_value", typeof(double));                
                denominationDataSet.Tables[0].Columns.Add("Count1");
            }
                obshkol1 = 0;
                obshsum1 = 0;
                
               

            if (countDenomDataSet == null || countDenomDataSet.Tables[0].Rows.Count == 0)
            {
                CountDenomDataSet = dataBase.GetSchema("t_g_declared_denom");                
            }
            else
            {
                CountDenomDataSet = countDenomDataSet;

                foreach (DataRow denomRow in denominationDataSet.Tables[0].Rows)
                {
                    

                    string selectString;
                    if (b == 1)
                    {                        
                        selectString = "id_account = '" + id_account.ToString() + "' and " +
                        " id_denomination='" + denomRow["id"].ToString() + "'";
                    }
                    else
                    {
                        selectString = "id_account = '" + id_account.ToString() + "' and " +
                        " id_denomination = '" + denomRow["id"].ToString() + "' and" +
                        " id_condition = '" + denomRow["id_condition"].ToString() + "'";// id_condition
                    }
                    DataRow[] searchedRows = CountDenomDataSet.Tables[0].Select(selectString);
                    
                    if (searchedRows.Count() > 0)
                    {
                        denomRow["Count"] = searchedRows[0].Field<Int32>("denomcount");
                        denomRow["Sum_value"] = Convert.ToDouble(searchedRows[0].Field<Decimal>("declared_value"));
                        obshkol1 = obshkol1 + Convert.ToInt32(denomRow["Count"]);
                        obshsum1 = obshsum1 + Convert.ToDouble(denomRow["Sum_value"]);
                    }                    

                }
                
                dgDenomination.DataSource = denominationDataSet.Tables[0];
            }

            denominationDataSet.Tables[0].Rows.Add();
            //denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count"] = obshkol1.ToString();
            //denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Sum_value"] = obshsum1.ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count22"] = nalcol.ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Sum_value1"] = nalsum.ToString();

            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count44"] = nalcol44.ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["sum44"] = nalsum44.ToString();


            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count1"] = "ИТОГО:";

            dgDenomination.Columns["Count"].DefaultCellStyle.Format = "### ### ### ###";
            dgDenomination.Columns["Sum_value"].DefaultCellStyle.Format = "### ### ### ###";
            dgDenomination.Columns["Count22"].DefaultCellStyle.Format = "### ### ### ###";
            dgDenomination.Columns["Sum_value1"].DefaultCellStyle.Format = "### ### ### ###";
            dgDenomination.Columns["Count44"].DefaultCellStyle.Format = "### ### ### ###";
            dgDenomination.Columns["sum44"].DefaultCellStyle.Format = "### ### ### ###";


            denominationDataSet.Tables[0].Rows.Add();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count"] = obshkol1.ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Sum_value"] = obshsum1.ToString();


            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count22"] = (nalcol-obshkol1).ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Sum_value1"] = (nalsum-obshsum1).ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count1"] = "остаток:";
            

        }

        private void dgDenomination_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex < dgDenomination.Rows.Count - 1)
                {
                    if (dgDenomination.Rows[e.RowIndex].Cells["Count1"].Value.ToString().Trim() != "")
                    {
                        int i2, i3;

                        if (dgDenomination.Rows[e.RowIndex].Cells["Count1"].Value == DBNull.Value)

                            i3 = 0;

                        else
                        {
                            if (Int32.TryParse(dgDenomination.Rows[e.RowIndex].Cells["Count1"].Value.ToString(), out i2) != true)
                            {
                                MessageBox.Show("Количество должно быть целым числом!");
                                return;
                            }
                            else
                                i3 = Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Count1"].Value);

                        }

                        int i1;// = 0;

                        if (dgDenomination.Rows[e.RowIndex].Cells["Count"].Value == DBNull.Value)
                            i1 = 0;
                        else
                            i1 = Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Count"].Value);                       

                        if ((i1 + i3) < 0)
                        {

                            MessageBox.Show("Количество должно быть положительным числом!");
                            return;

                        }

                        dgDenomination.Rows[e.RowIndex].Cells["Count"].Value = i1 + i3;
                        dgDenomination.Rows[e.RowIndex].Cells["Sum_value"].Value = Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Count"].Value) * Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Denom"].Value);
                        
                        obshkol1 = obshkol1 + i3;
                        obshsum1 = obshsum1 + (i3 * Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Denom"].Value));
                        dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Count"].Value = obshkol1;
                        dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Sum_value"].Value = obshsum1;

                        dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Count22"].Value = nalcol - obshkol1;
                        dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Sum_value1"].Value = nalsum - obshsum1;

                    }
                    else
                    {
                        ////17.02.2020
                        // obshkol1 = obshkol1 + Convert.ToInt32(denomRow["Count"]);
                        // obshsum1 = obshsum1 + Convert.ToDouble(denomRow["Sum_value"]);
                        ////17.02.2020
                        // dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Count1"].Value = "0";
                        // denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count"] = obshkol1.ToString();
                        // denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Sum_value"] = obshsum1.ToString();
                    }
                    dgDenomination.Rows[e.RowIndex].Cells["Count1"].Value = String.Empty;
                }                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            int d = 1;  // для проверки введенных данных на количество имеющихся
            if (checkBox1.Checked == false) b = 1;
            else if (checkBox1.Checked == true & checkBox2.Checked == false) b = 2;
            else if (checkBox2.Checked == true) b = 3;
            else MessageBox.Show("Появилясь какая та ошибка в условиях CheckBox -ов 1");

            //this.DialogResult = DialogResult.OK;            
            //b_get = b;            
            
            foreach (DataRow denomRow in denominationDataSet.Tables[0].Rows)
            {
                
                if (denomRow["Count"] != null && denomRow["Count"] != DBNull.Value)//нужно добавить условия чтоб было меньше либо равно к сумме наличности
                {
                    
                    if (denomRow["id"].ToString() != "")
                    {
                        if (Convert.ToInt32(denomRow["Count"]) > Convert.ToInt32(denomRow["Count22"]))
                        {
                            //MessageBox.Show(" Введенное число было больше имеющего, прошу введите корректное число !!!   " + denomRow["id"]);
                            d = 0;
                        }
                        else
                        {
                            if (b != 1) // сохраниение данных с состоянем
                            {
                                /////где то сде есть ошибка на дбнулл не иожет преоболзовать на инт64
                                if (CountDenomDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])
                                        & x.Field<Int64>("id_condition") == Convert.ToInt64(denomRow["id_condition"])))
                                {
                                    DataRow cntDenomDataRow = CountDenomDataSet.Tables[0].AsEnumerable().Where(
                                        x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])
                                        & x.Field<Int64>("id_condition") == Convert.ToInt64(denomRow["id_condition"])
                                        ).First<DataRow>();
                                    cntDenomDataRow["id_denomination"] = denomRow["id"];
                                    cntDenomDataRow["denomcount"] = denomRow["Count"];
                                    cntDenomDataRow["id_account"] = id_account;
                                    cntDenomDataRow["creation"] = cntDenomDataRow["creation"];
                                    cntDenomDataRow["lastupdate"] = DateTime.Now;
                                    cntDenomDataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                                    cntDenomDataRow["id_currency"] = id_currency;
                                    cntDenomDataRow["declared_value"] = denomRow["Sum_value"];
                                    cntDenomDataRow["id_condition"] = denomRow["id_condition"];


                                }
                                else
                                {
                                    DataRow cntDenomDataRow = CountDenomDataSet.Tables[0].NewRow();
                                    cntDenomDataRow["id_denomination"] = denomRow["id"];
                                    cntDenomDataRow["denomcount"] = denomRow["Count"];
                                    cntDenomDataRow["id_account"] = id_account;
                                    cntDenomDataRow["creation"] = DateTime.Now;
                                    cntDenomDataRow["lastupdate"] = DateTime.Now;
                                    cntDenomDataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                                    cntDenomDataRow["id_currency"] = id_currency;
                                    cntDenomDataRow["declared_value"] = denomRow["Sum_value"];
                                    cntDenomDataRow["id_condition"] = denomRow["id_condition"];
                                    CountDenomDataSet.Tables[0].Rows.Add(cntDenomDataRow);

                                }
                            }
                            else // сохраниение данных без состояния
                            {
                                if (CountDenomDataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])))
                                {
                                    DataRow cntDenomDataRow = CountDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])).First<DataRow>();
                                    cntDenomDataRow["id_denomination"] = denomRow["id"];
                                    cntDenomDataRow["denomcount"] = denomRow["Count"];
                                    cntDenomDataRow["id_account"] = id_account;
                                    cntDenomDataRow["creation"] = cntDenomDataRow["creation"];
                                    cntDenomDataRow["lastupdate"] = DateTime.Now;
                                    cntDenomDataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                                    cntDenomDataRow["id_currency"] = id_currency;
                                    cntDenomDataRow["declared_value"] = denomRow["Sum_value"];
                                    //cntDenomDataRow["id_bag"] = b;
                                }
                                else
                                {
                                    DataRow cntDenomDataRow = CountDenomDataSet.Tables[0].NewRow();
                                    cntDenomDataRow["id_denomination"] = denomRow["id"];
                                    cntDenomDataRow["denomcount"] = denomRow["Count"];
                                    cntDenomDataRow["id_account"] = id_account;
                                    cntDenomDataRow["creation"] = DateTime.Now;
                                    cntDenomDataRow["lastupdate"] = DateTime.Now;
                                    cntDenomDataRow["last_user_update"] = DataExchange.CurrentUser.CurrentUserId;
                                    cntDenomDataRow["id_currency"] = id_currency;
                                    cntDenomDataRow["declared_value"] = denomRow["Sum_value"];
                                    //cntDenomDataRow["id_bag"] = b;
                                    CountDenomDataSet.Tables[0].Rows.Add(cntDenomDataRow);
                                }
                            }
                           
                        }                      
                    }  
                }                

            }
            if (d == 1)
            {
                this.DialogResult = DialogResult.OK;
                b_get = b;
                Close();
            }
            else 
            {
                //MessageBox.Show("Введите заново"); 
                MessageBox.Show(" Введенное число было больше имеющего, прошу введите корректное число заново!!!" );
                CountDenomDataSet.Clear();
                c = false;    
            }
            //MessageBox.Show("Отправляю b_get - " + b_get.ToString());
        }
        
        private void DgDenomination_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            int intNumColumnDeleteAttribute = 0;
            /*
             if (dgAccountDeclared[4, e.RowIndex].Value != null)
             {

                 if (dgAccountDeclared[4, e.RowIndex].Value.ToString() != "")
                     dgAccountDeclared[0, e.RowIndex].Value = true;

             }
             else
             {
                 dgAccountDeclared[0, e.RowIndex].Value = false;
             }
             */
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //MSDataBase dataBase = new MSDataBase();
            //dataBase.Connect();

            //if (c == true)
            //{
            //   // MessageBox.Show(" C - TRUE !!!");
            //    DialogResult result = MessageBox.Show(
            //    "1 ---Вы уже ввели данные. Вы желаете очистить превидущий ввод?",
            //    "Сообщение",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Information,
            //    MessageBoxDefaultButton.Button1,
            //    MessageBoxOptions.DefaultDesktopOnly
            //    );

            //    if (result == DialogResult.Yes)
            //    {
            //        countDenomDataSet = null;
            //        c = false; 

            //        if (checkBox1.Checked == true)
            //        {
            //            a = 0;
            //            b = 2;
            //            checkBox2.Checked = false;
            //            checkBox1.Checked = true;
            //            dgDenomination.Columns["id_condition"].Visible = true;
            //            denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
            //        }
            //        else
            //        {
            //            b = 1;
            //            checkBox2.Checked = false;
            //            checkBox1.Checked = false;
            //            dgDenomination.Columns["id_condition"].Visible = false;
            //            denominationDataSet = dataBase.GetData9("PROC_BALANCE_ALL");
            //        }
            //        SelectInfo(denominationDataSet, countDenomDataSet);


            //    }

            //   // else MessageBox.Show(" NO !!!");
                   
            //}
            //else // MessageBox.Show(" C - FALSE !!!");
            //{
            //    if (checkBox1.Checked == true)
            //    {
            //        a = 0;
            //        b = 2;
            //        checkBox2.Checked = false;
            //        checkBox1.Checked = true;
            //        dgDenomination.Columns["id_condition"].Visible = true;
            //        denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
            //    }
            //    else
            //    {
            //        b = 1;
            //        checkBox2.Checked = false;
            //        checkBox1.Checked = false;
            //        dgDenomination.Columns["id_condition"].Visible = false;
            //        denominationDataSet = dataBase.GetData9("PROC_BALANCE_ALL");
                    
            //    }
            //    SelectInfo(denominationDataSet, countDenomDataSet);
            //}
            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //MSDataBase dataBase = new MSDataBase();
            //dataBase.Connect();

            //if (c == true)
            //{
            //    // MessageBox.Show(" C - TRUE !!!");
            //    DialogResult result = MessageBox.Show(
            //    "2 -- Вы уже ввели данные. Вы желаете очистить превидущий ввод?",
            //    "Сообщение",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Information,
            //    MessageBoxDefaultButton.Button1,
            //    MessageBoxOptions.DefaultDesktopOnly
            //    );

            //    if (result == DialogResult.Yes)
            //    {
            //        countDenomDataSet = null;
            //        c = false;

            //        if (checkBox2.Checked == true)
            //        {
            //            a = 20007;
            //            b = 3;
            //            checkBox1.Checked = true;
            //            checkBox2.Checked = true;
            //        }
            //        else
            //        {
            //            a = 0;
            //            b = 2;
            //            checkBox1.Checked = true;
            //            checkBox2.Checked = false;
            //        }
            //        dgDenomination.Columns["id_condition"].Visible = true;
            //        denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
            //        SelectInfo(denominationDataSet, countDenomDataSet);
            //    }
            //}
            //else
            //{
            //    if (checkBox2.Checked == true)
            //    {
            //        a = 20007;
            //        b = 3;
            //        checkBox1.Checked = true;
            //        checkBox2.Checked = true;
            //    }
            //    else
            //    {
            //        a = 0;
            //        b = 2;
            //        checkBox1.Checked = true;
            //        checkBox2.Checked = false;
            //    }
            //    dgDenomination.Columns["id_condition"].Visible = true;
            //    denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
            //    SelectInfo(denominationDataSet, countDenomDataSet);                
            //}
            
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Ура заработала!!!");


            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            if (c == true)
            {
                // MessageBox.Show(" C - TRUE !!!");
                DialogResult result = MessageBox.Show(
                "1 ---Вы уже ввели данные. Вы желаете очистить превидущий ввод?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

                if (result == DialogResult.Yes)
                {
                    countDenomDataSet = null;
                    c = false;

                    if (checkBox1.Checked == true)
                    {
                        a = 0;
                        b = 2;
                        checkBox2.Checked = false;
                        checkBox1.Checked = true;
                        dgDenomination.Columns["id_condition"].Visible = true;
                        denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
                    }
                    else
                    {
                        b = 1;
                        checkBox2.Checked = false;
                        checkBox1.Checked = false;
                        dgDenomination.Columns["id_condition"].Visible = false;
                        denominationDataSet = dataBase.GetData9("PROC_BALANCE_ALL2 " + id_account + ", " + id_currency);
                    }
                    SelectInfo(denominationDataSet, countDenomDataSet);


                }

                // else MessageBox.Show(" NO !!!");

            }
            else // MessageBox.Show(" C - FALSE !!!");
            {
                if (checkBox1.Checked == true)
                {
                    a = 0;
                    b = 2;
                    checkBox2.Checked = false;
                    checkBox1.Checked = true;
                    dgDenomination.Columns["id_condition"].Visible = true;
                    denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
                }
                else
                {
                    b = 1;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                    dgDenomination.Columns["id_condition"].Visible = false;
                    denominationDataSet = dataBase.GetData9("PROC_BALANCE_ALL2 " + id_account + ", " + id_currency );

                }
                SelectInfo(denominationDataSet, countDenomDataSet);
            }
        }

        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            if (c == true)
            {
                // MessageBox.Show(" C - TRUE !!!");
                DialogResult result = MessageBox.Show(
                "2 -- Вы уже ввели данные. Вы желаете очистить превидущий ввод?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

                if (result == DialogResult.Yes)
                {
                    countDenomDataSet = null;
                    c = false;

                    if (checkBox2.Checked == true)
                    {
                        a = 20007;
                        b = 3;
                        checkBox1.Checked = true;
                        checkBox2.Checked = true;
                    }
                    else
                    {
                        a = 0;
                        b = 2;
                        checkBox1.Checked = true;
                        checkBox2.Checked = false;
                    }
                    dgDenomination.Columns["id_condition"].Visible = true;
                    denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
                    SelectInfo(denominationDataSet, countDenomDataSet);
                }
            }
            else
            {
                if (checkBox2.Checked == true)
                {
                    a = 20007;
                    b = 3;
                    checkBox1.Checked = true;
                    checkBox2.Checked = true;
                }
                else
                {
                    a = 0;
                    b = 2;
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;
                }
                dgDenomination.Columns["id_condition"].Visible = true;
                denominationDataSet = dataBase.GetData9(tableName + " " + id_account + ", " + id_currency + ", " + a);
                SelectInfo(denominationDataSet, countDenomDataSet);
            }

        }


        //14.07.2020


        private void dgDenomination_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

            Rectangle rtHeader = this.dgDenomination.DisplayRectangle;

            rtHeader.Height = this.dgDenomination.ColumnHeadersHeight / 2;

            this.dgDenomination.Invalidate(rtHeader);

        }

        void dgDenomination_Scroll(object sender, ScrollEventArgs e)

        {

            Rectangle rtHeader = this.dgDenomination.DisplayRectangle;

            rtHeader.Height = this.dgDenomination.ColumnHeadersHeight / 2;

            this.dgDenomination.Invalidate(rtHeader);


        }

        void dgDenomination_Paint(object sender, PaintEventArgs e)
            
        {
            for (int j = 7; j < 12;
                j++
                )
            {

                if (j >= 7)
                {
                    Rectangle r1 = this.dgDenomination.GetCellDisplayRectangle(j, -1, true);

                    int w2 = this.dgDenomination.GetCellDisplayRectangle(j + 1, -1, true).Width;

                    r1.X += 1;

                    r1.Y += 1;


                    r1.Width = r1.Width + w2 - 2;

                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(this.dgDenomination.ColumnHeadersDefaultCellStyle.BackColor), r1);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;

                    format.LineAlignment = StringAlignment.Center;

                    string s1 = "";

                    if (j == 7)
                        s1 = "В наличии";
                    if (j == 9)
                        s1 = "Отправленно без потверждения";
                    if (j == 11)
                        s1 = "В наличии с учетом отправленного";


                    e.Graphics.DrawString(s1,

                        this.dgDenomination.ColumnHeadersDefaultCellStyle.Font,

                        new SolidBrush(this.dgDenomination.ColumnHeadersDefaultCellStyle.ForeColor),

                        r1,

                        format);

                    j += 1;


                }



            }



        }

        private void DenominationParentForSC_Load(object sender, EventArgs e)
        {

        }
    }
}
