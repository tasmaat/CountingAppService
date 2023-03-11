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
    public partial class DenominationParent : Form
    {
        private DataSet denominationDataSet = null;
        private DataSet conditionDataSet = null;
        private DataSet currencyDataSet = null;
        private DataSet accountDenomDataSet = null;
        public DataSet CountDenomDataSet { get; set; }

        private long id_currency;
        private long id_account;

        private int obshkol1;
        private double obshsum1;

        public DenominationParent()
        {
            InitializeComponent();
        }

        public DenominationParent(string tableName, string id_currency, string id_account, DataSet countDenomDataSet = null)
        {
            InitializeComponent();
            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            denominationDataSet = dataBase.GetData("t_g_denomination", "id_currency", id_currency);

            conditionDataSet = dataBase.GetData("t_g_condition");

            currencyDataSet = dataBase.GetData("t_g_currency");

            this.id_account = Convert.ToInt64(id_account);
            this.id_currency = Convert.ToInt64(id_currency);

            //accountDenomDataSet = dataBase.GetData(tableName, "id_account", id_account.);

            dgDenomination.AutoGenerateColumns = false;

            ////25.10.2019
            dgDenomination.Columns.Add("Count1", "Ввод кол-ва");
            dgDenomination.Columns["Count1"].Visible = true;

           // dgDenomination.Columns["Count1"].ReadOnly = true;


            dgDenomination.Columns["Count1"].Width = 65;
            dgDenomination.Columns["Count1"].DataPropertyName = "Count1";
            ////25.10.2019

            dgDenomination.Columns.Add("Count", "Кол-во");
            dgDenomination.Columns["Count"].Visible = true;
            dgDenomination.Columns["Count"].Width = 65;
            ////25.10.2019
            dgDenomination.Columns["Count"].ReadOnly = true;
            ////25.10.2019
            dgDenomination.Columns["Count"].DataPropertyName = "Count";

            dgDenomination.Columns.Add("id_denom", "Номинал");
            dgDenomination.Columns["id_denom"].Visible = false;
            dgDenomination.Columns["id_denom"].ReadOnly = true;
            dgDenomination.Columns["id_denom"].DataPropertyName = "id";

            dgDenomination.Columns.Add("Denom", "Номинал");
            dgDenomination.Columns["Denom"].Visible = true;
            dgDenomination.Columns["Denom"].ReadOnly = true;

            ////25.10.2019
            dgDenomination.Columns["Denom"].Width = 80;
            ////25.10.2019

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

            ////25.10.2019
            dgDenomination.Columns["Currency"].Width = 90;
            ////25.10.2019

            dgDenomination.Columns["Currency"].DataPropertyName = "id_currency";
            currencyColumn.DataSource = currencyDataSet.Tables[0];
            
            DataGridViewComboBoxColumn conditionColumn = new DataGridViewComboBoxColumn();
            conditionColumn.Name = "Condition";
            conditionColumn.HeaderText = "Состояние";
            conditionColumn.ValueMember = "id";

           

            conditionColumn.DisplayMember = "name";
            conditionColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dgDenomination.Columns.Add(conditionColumn);

            ////25.10.2019
            dgDenomination.Columns["Condition"].Width = 90;
            ////25.10.2019

            dgDenomination.Columns["Condition"].Visible = true;
            
            conditionColumn.DataSource = conditionDataSet.Tables[0];
            
            dgDenomination.Columns.Add("Sum_value", "Сумма");
            dgDenomination.Columns["Sum_value"].Visible = true;
            dgDenomination.Columns["Sum_value"].ReadOnly = true;
            dgDenomination.Columns["Sum_value"].DataPropertyName = "Sum_value";

            


            dgDenomination.DataSource = denominationDataSet.Tables[0];


            //////19.02.2020
            //denominationDataSet.Tables[0].Columns.Add("Count");
            // denominationDataSet.Tables[0].Columns.Add("Sum_value");

            denominationDataSet.Tables[0].Columns.Add("Count", typeof(Int16));
            denominationDataSet.Tables[0].Columns.Add("Sum_value", typeof(double));

            //   denominationDataSet.Tables[0].Columns["Sum_value"].DataType = typeof(decimal);
            //////19.02.2020

            ////17.02.2020
            obshkol1 = 0;
            obshsum1 = 0;
            denominationDataSet.Tables[0].Columns.Add("Count1");
            ////17.02.2020

            if (countDenomDataSet == null || countDenomDataSet.Tables[0].Rows.Count == 0)
            {
                CountDenomDataSet = dataBase.GetSchema("t_g_declared_denom");
            }
            else
            {
                
                CountDenomDataSet = countDenomDataSet;
               
                
                foreach(DataRow denomRow in denominationDataSet.Tables[0].Rows)
                {
                    var tmp = denomRow["id"];

                    /////30.10.2019
                    string selectString = "id_account = '" + id_account.ToString() + "' and id_denomination='"+ denomRow["id"].ToString()+"'";
                    DataRow[] searchedRows = ((DataTable)CountDenomDataSet.Tables[0]).Select(selectString);
                    if (searchedRows.Count() > 0)
                    {

                        denomRow["Count"] = searchedRows[0].Field<Int32>("denomcount");

                        ////17.02.2020
                        //denomRow["Sum_value"] = searchedRows[0].Field<Decimal>("declared_value");
                        denomRow["Sum_value"] = Convert.ToDouble(searchedRows[0].Field<Decimal>("declared_value"));
                        ////17.02.2020

                        ////17.02.2020
                        obshkol1 = obshkol1+ Convert.ToInt32(denomRow["Count"]);
                        obshsum1 = obshsum1+ Convert.ToDouble(denomRow["Sum_value"]);
                        ////17.02.2020

                    }
                    // string s2 = searchedRows[0].Field<string>("curr_code");
                    //  DataRow cntDenomDataRow = CountDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])).First<DataRow>();
                    //  if (cntDenomDataRow["id_account"] == id_account)
                    // {
                    /////30.10.2019

                    /*
                    denomRow["Count"] = CountDenomDataSet.Tables[0].AsEnumerable().
                            Where(x => x.Field<Int64?>("id_denomination") == Convert.ToInt64(denomRow["id"])).Count() > 0 ? CountDenomDataSet.Tables[0].AsEnumerable().
                            Where(x => x.Field<Int64?>("id_denomination") == Convert.ToInt64(denomRow["id"])).Select(x => x.Field<Int32>("denomcount")).First<Int32>().ToString() : null;


                        denomRow["Sum_value"] = CountDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])).Count() > 0 ? CountDenomDataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id_denomination") == Convert.ToInt64(denomRow["id"])).Select(x => x.Field<Decimal>("declared_value")).First<Decimal>().ToString() : null;
                    */


                    /////30.10.2019
                }

                


                dgDenomination.DataSource = denominationDataSet.Tables[0];
            }

            /////17.02.2020
            denominationDataSet.Tables[0].Rows.Add();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Count"] = obshkol1.ToString();
            denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count - 1]["Sum_value"] = obshsum1.ToString();
            // dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Count1"].Value = "0";
             denominationDataSet.Tables[0].Rows[denominationDataSet.Tables[0].Rows.Count-1]["Count1"]= "ИТОГО:";
            // dgDenomination.DataSource = denominationDataSet.Tables[0];
            //dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Count1"].Value = "0";
            /////17.02.2020

            //////19.02.2020
            // dgDenomination.Columns["Count"].ValueType = typeof(string);
            // dgDenomination.Columns["Sum_value"].ValueType = typeof(string);

           

            dgDenomination.Columns["Count"].DefaultCellStyle.Format = "### ### ### ###";
            dgDenomination.Columns["Sum_value"].DefaultCellStyle.Format = "### ### ### ###";
            //dgDenomination.Columns["Count"].ValueType = typeof(string);
           // dgDenomination.Columns["Sum_value"].ValueType = typeof(double);
            //////19.02.2020

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgDenomination_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {

                /////17.02.2020
                if (e.RowIndex < dgDenomination.Rows.Count - 1)
                {
                    if (dgDenomination.Rows[e.RowIndex].Cells["Count1"].Value.ToString().Trim() != "")
                    {
                        /////17.02.2020

                        ///25.10.2019
                        // dgDenomination.Rows[e.RowIndex].Cells["Sum_value"].Value = Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Count"].Value) * Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Denom"].Value);
                        int i2, i3 = 0;

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



                        int i1 = 0;

                        if (dgDenomination.Rows[e.RowIndex].Cells["Count"].Value == DBNull.Value)
                            i1 = 0;
                        else
                            i1 = Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Count"].Value);

                        //////30.12.2019

                        if ((i1 + i3) < 0)
                        {

                            MessageBox.Show("Количество должно быть положительным числом!");
                            return;

                        }

                        //////30.12.2019

                        dgDenomination.Rows[e.RowIndex].Cells["Count"].Value = i1 + i3;
                        dgDenomination.Rows[e.RowIndex].Cells["Sum_value"].Value = Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Count"].Value) * Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Denom"].Value);
                        ///25.10.2019

                        /////17.02.2020

                        /*
                        obshkol1 = 0;
                        obshsum1 = 0;


                        for (int i5 = 0; i5 < dgDenomination.Rows.Count - 1; i5++)
                        {

                            if (dgDenomination.Rows[i5].Cells["Count"].Value.ToString() != "")
                            {
                                obshkol1 = obshkol1 + Convert.ToInt32(dgDenomination.Rows[i5].Cells["Count"].Value);
                                obshsum1 = obshsum1 + Convert.ToDouble(dgDenomination.Rows[i5].Cells["Sum_value"].Value);
                            }




                        }
                        */
                        obshkol1=obshkol1 + i3;
                        obshsum1 = obshsum1 + (i3 * Convert.ToInt32(dgDenomination.Rows[e.RowIndex].Cells["Denom"].Value));
                        dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Count"].Value = obshkol1;
                        dgDenomination.Rows[dgDenomination.Rows.Count - 1].Cells["Sum_value"].Value = obshsum1;



                        ////17.02.2020

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
                /////17.02.2020
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            

            foreach (DataRow denomRow in denominationDataSet.Tables[0].Rows)
            {
                

                if (denomRow["Count"] != null && denomRow["Count"] != DBNull.Value)
                {
                    var temmp = denomRow["Count"].GetType();

                    /////17.02.2020
                    if (denomRow["id"].ToString() != "")
                    {
                    /////17.02.2020

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
                            CountDenomDataSet.Tables[0].Rows.Add(cntDenomDataRow);
                        }

                    /////17.02.2020
                    }
                    /////17.02.2020

                }

            }

            Close();
        }

        ///25.10.2019
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
        ///25.10.2019
    }
}
