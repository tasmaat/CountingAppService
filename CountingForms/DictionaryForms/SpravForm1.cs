﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms.Services;
using DataExchange;

namespace CountingForms.DictionaryForms
{
    public partial class SpravForm1  : DictionaryForm
    {
        public int tipsp;
        private DataSet d1 = null;
        private DataSet d2 = null;
        private DataSet d3 = null;
        private DataSet cityDataSet = null;
        private DataSet nameColumnDataSet = null;

        public SpravForm1()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public SpravForm1(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {

            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            //Инициализация компонентов управления
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dgList.Columns.Add("c1", "Кассовый центр");
            dgList.Columns["c1"].DataPropertyName = "c1";
           // dgList.Columns["c1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("name1", "Код");
            dgList.Columns["name1"].DataPropertyName = "name1";
           // dgList.Columns["name1"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dgList.Columns.Add("id_parent", "id_parent");
            dgList.Columns["id_parent"].DataPropertyName = "id_parent";
            dgList.Columns["id_parent"].Visible = false;


        }

        private async void SpravForm1_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            //Заполнение название полей
            nameColumnDataSet = dataBase.GetData("t_g_name_column");
            //INF1
            if (nameColumnDataSet.Tables[0].AsEnumerable().Any(x => x.Field<string>("name") == "Rez1"))
                lblRez1.Text = nameColumnDataSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("name") == "Rez1").Select(x => x.Field<string>("value")).FirstOrDefault<string>();
            else
                lblRez1.Text = "Rez1";



            cityDataSet = dataBase.GetData("t_g_city");

              //SpravForm1.t
                label3.Visible = false;
                textBox2.Visible = false;
                // dataSet = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=2");
                dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=2");
                d1 = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=1");
           
            //   else
           
           
            ///////14.02.2020

            comboBox1.DataSource = d1.Tables[0];
            comboBox1.DisplayMember = "branch_name";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;

            ////12.02.2020
            
            

               

            dgList.DataSource = dataSet.Tables[0];

            ////14.02.2020
            dgList.AutoResizeColumns();
            ////14.02.2020

            dgList.ClearSelection();


            tbName.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            tbCam.Text = "";
            tbRez1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            tbName.Focus();

            if(pm.EnabledPossibility(perm, btnAdd))
                btnAdd.Enabled = true;
            if (pm.EnabledPossibility(perm, btnModify))
                btnModify.Enabled = true;

        }

        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {

            if (dgList.CurrentCell != null)
            {
                int rowIndex = dgList.CurrentCell.RowIndex;
                

                if (dataSet.Tables[0].Rows[dgList.CurrentRow.Index].RowState != DataRowState.Deleted)
                {

                    ////14.02.2020

                    tbName.Text = dgList.CurrentRow.Cells["branch_name"].Value.ToString();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["branch_name"].ToString();

                    //                    tbName.Text = dgList.CurrentRow.Cells["user_name"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["user_name"].ToString().Trim();


                    if (dgList.CurrentRow.Cells["id_parent"].Value.ToString() != "")// */dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_parent"].ToString() != "")
                        comboBox1.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_parent"].Value);// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_parent"]);


                    //MessageBox.Show(dgList.CurrentRow.Cells["name1"].Value.ToString().Trim());
                    textBox1.Text = dgList.CurrentRow.Cells["name1"].Value.ToString().Trim();//*/dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["name1"].ToString();


                   
                }
            }

        }

        protected override void btnOchist_Click(object sender, EventArgs e)
        {
           
            dgList.ClearSelection();
            tbName.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;

            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

           
            tbRez1.Text = "";
            tbCam.Text = "";
        }

        protected override void btnDelete_Click(object sender, EventArgs e)
        {


            int rowIndex = dgList.CurrentCell.RowIndex;
            dataSet.Tables[0].Rows[rowIndex].Delete();
            dataBase.UpdateData(dataSet, "t_g_cashcentre");

            
                dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=2");
                        

            dgList.DataSource = dataSet.Tables[0];

            tbName.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;

           

        }


        protected override void btnAdd_Click(object sender, EventArgs e)
        {
           
            int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            if (tbName.Text != String.Empty && result == 0)
            {

                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;

                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;
                dataRow["time_zone"] = 6;
                dataRow["id_city"] = cityDataSet.Tables[0].Rows[0]["id"];

                /////14.02.2020
               
                
                {

                /////14.02.2020
                
                dataRow[gridFieldName] = tbName.Text;


                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                {
                    dataRow["id_parent"] = comboBox1.SelectedValue;
                }
                else
                {
                    
                        MessageBox.Show("Выберите кассовый центр!");
                    
                    return;
                }

                

                
                dataRow["name1"] = textBox1.Text;

                dataRow["id_city"] = cityDataSet.Tables[0].Rows[0]["id"];

                
                    dataRow["tipsp1"] = "2";

               

                //////14.02.2020
                }
                //////14.02.2020

                dataSet.Tables[0].Rows.Add(dataRow);
                // dataBase.UpdateData(dataSet);
                dataBase.UpdateData(dataSet, "t_g_cashcentre");

                //////14.02.2020

                
                    dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=2");

               
                


                dgList.DataSource = dataSet.Tables[0];

                //////14.02.2020

                

                dgList.ClearSelection();

                tbName.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedIndex = -1;

                tbName.Focus();

                /////14.02.2020
               
                /////14.02.2020

                //    dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];

            }

            
        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {

            

            int rowIndex = dgList.CurrentCell.RowIndex;

            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;

            /////14.02.2020
            
            {

                /////14.02.2020

                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                {
                    dataSet.Tables[0].Rows[rowIndex]["id_parent"] = comboBox1.SelectedValue;
                }
                else
                {                   
                    MessageBox.Show("Выберите кассовый центр!");                    
                    return;
                }

              

                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                    dataSet.Tables[0].Rows[rowIndex]["id_parent"] = comboBox1.SelectedValue;

                dataSet.Tables[0].Rows[rowIndex]["name1"] = textBox1.Text;

                

                /////14.02.2020
            }
                /////14.02.2020

            dataBase.UpdateData(dataSet, "t_g_cashcentre");

            dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=2");

            dgList.DataSource = dataSet.Tables[0];

            //////14.02.2020

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
    }
}
