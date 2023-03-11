using System;
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
    public partial class SpravForm2  : DictionaryForm
    {
        public int tipsp;
        private DataSet d1 = null;
        private DataSet d2 = null;
        private DataSet d3 = null;
        private DataSet cityDataSet = null;
        private DataSet nameColumnDataSet = null;

        public SpravForm2()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public SpravForm2(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {

            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            //Инициализация компонентов управления
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();


            dgList.Columns.Add("c1", "Зона");
            dgList.Columns["c1"].DataPropertyName = "c1";
          //  dgList.Columns["c1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("name1", "Имя компьютера");
            dgList.Columns["name1"].DataPropertyName = "name1";
           // dgList.Columns["name1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("name2", "Ip адрес");
            dgList.Columns["name2"].DataPropertyName = "name2";
            //dgList.Columns["name2"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("Camera", "Камера");
            dgList.Columns["Camera"].DataPropertyName = "Camera";
           // dgList.Columns["Camera"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgList.Columns.Add("Rez1", "Резервное поле");
            dgList.Columns["Rez1"].DataPropertyName = "Rez1";
            //dgList.Columns["Rez1"].SortMode = DataGridViewColumnSortMode.NotSortable;

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

           
            if(pm.VisiblePossibility(perm, lblCam))
                lblCam.Visible = true;
            if (pm.VisiblePossibility(perm, tbCam))
                tbCam.Visible = true;
            if (pm.VisiblePossibility(perm, lblRez1))
                lblRez1.Visible = true;
            if (pm.VisiblePossibility(perm, tbRez1))
                tbRez1.Visible = true;
                // label2.Text = "Пользователь";
                label2.Text = "Имя компьютера";
                label1.Text = "Зона";
            if (pm.VisiblePossibility(perm, label3))
                label3.Visible = true;
            if (pm.VisiblePossibility(perm, textBox2))
                textBox2.Visible = true;
                //dataSet = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=3");
                dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=3");
                d1 = dataBase.GetData9("SELECT *  FROM t_g_cashcentre where tipsp1=2");
            

           
            ///////14.02.2020

            comboBox1.DataSource = d1.Tables[0];
            comboBox1.DisplayMember = "branch_name";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;

           
           
            
            dgList.DataSource = dataSet.Tables[0];

            ////14.02.2020
            dgList.AutoResizeColumns();
            ////14.02.2020

            dgList.ClearSelection();


            // tbName.Text = "";

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

        //  protected override void dgList_KeyUp(object sender, KeyEventArgs e)
        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {

            if (dgList.CurrentCell != null)
            {
                int rowIndex = dgList.CurrentCell.RowIndex;
                // base.dgList_SelectionChanged(sender, e);


                //  Thread.Sleep(200);

                ////14.02.2020

                if (dataSet.Tables[0].Rows[dgList.CurrentRow.Index].RowState != DataRowState.Deleted)
                {

                    ////14.02.2020

                    tbName.Text = dgList.CurrentRow.Cells["branch_name"].Value.ToString();//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["branch_name"].ToString();

                    //                    tbName.Text = dgList.CurrentRow.Cells["user_name"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["user_name"].ToString().Trim();


                    if (dgList.CurrentRow.Cells["id_parent"].Value.ToString() != "")//dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_parent"].ToString() != "")
                        comboBox1.SelectedValue = Convert.ToInt32(dgList.CurrentRow.Cells["id_parent"].Value);// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_parent"]);


                   // MessageBox.Show(dgList.CurrentRow.Cells["name1"].Value.ToString().Trim());
                    textBox1.Text = dgList.CurrentRow.Cells["name1"].Value.ToString().Trim();//*/dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["name1"].ToString();

                                    //dgList.CurrentRow.Cells["user_name"].Value.ToString().Trim();// dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["user_name"].ToString().Trim();


                                        
                    textBox2.Text = dgList.CurrentRow.Cells["name2"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["name2"].ToString();
                    tbRez1.Text = dgList.CurrentRow.Cells["Rez1"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["Rez1"].ToString();
                    tbCam.Text = dgList.CurrentRow.Cells["Camera"].Value.ToString().Trim(); //dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["Camera"].ToString();


                }
                ////14.02.2020

                // Выбор города
                //  cbCity.SelectedValue = Convert.ToInt32(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["id_city"]);
                // Выбор часового пояса
                //  nUTC.Value = Convert.ToDecimal(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["time_zone"].ToString());
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

            /////14.02.2020
            if (tipsp == 3)
                tbName.Text = "зона-1";
            /////14.02.2020
            tbRez1.Text = "";
            tbCam.Text = "";
        }

        protected override void btnDelete_Click(object sender, EventArgs e)
        {


            int rowIndex = dgList.CurrentCell.RowIndex;
            dataSet.Tables[0].Rows[rowIndex].Delete();
            //dataBase.UpdateData(dataSet);
            dataBase.UpdateData(dataSet, "t_g_cashcentre");

            dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=3");

           

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
                
                dataRow[gridFieldName] = tbName.Text;


                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                {
                    dataRow["id_parent"] = comboBox1.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Выберите зону!");
                    return;
                }

                

                
                dataRow["name1"] = textBox1.Text;

                dataRow["id_city"] = cityDataSet.Tables[0].Rows[0]["id"];

               
                
                    dataRow["name2"] = textBox2.Text;
                    dataRow["tipsp1"] = "3";
                        dataRow["Camera"] = tbCam.Text.Trim();
                        dataRow["Rez1"] = tbRez1.Text.Trim();
                

                

                dataSet.Tables[0].Rows.Add(dataRow);
                
                dataBase.UpdateData(dataSet, "t_g_cashcentre");
                                
                dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=3");
                               

                dgList.DataSource = dataSet.Tables[0];

                

                dgList.ClearSelection();

                tbName.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedIndex = -1;

                tbName.Focus();

                
            }

        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {

            

            int rowIndex = dgList.CurrentCell.RowIndex;

            dataSet.Tables[0].Rows[rowIndex][gridFieldName] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;

            /////14.02.2020

            

                /////14.02.2020

                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                {
                    dataSet.Tables[0].Rows[rowIndex]["id_parent"] = comboBox1.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Выберите зону!");
                    return;
                }

              

                if ((comboBox1.SelectedIndex > -1) & (comboBox1.Text.ToString().Trim() != ""))
                    dataSet.Tables[0].Rows[rowIndex]["id_parent"] = comboBox1.SelectedValue;

                dataSet.Tables[0].Rows[rowIndex]["name1"] = textBox1.Text;

               
                    dataSet.Tables[0].Rows[rowIndex]["name2"] = textBox2.Text;
                    dataSet.Tables[0].Rows[rowIndex]["Rez1"] = tbRez1.Text.Trim();
                    dataSet.Tables[0].Rows[rowIndex]["Camera"] = tbCam.Text.Trim();

            dataBase.UpdateData(dataSet, "t_g_cashcentre");

            dataSet = dataBase.GetData9("SELECT [id],[creation],[branch_name],[id_parent],[lastupdate],[id_city],[time_zone],[last_update_user],[tipsp1],[name1],[name2],(select max(t2.branch_name) from t_g_cashcentre t2 where t2.id=t_g_cashcentre.id_parent) as c1,id_parent1, Rez1, Camera FROM t_g_cashcentre where tipsp1=3");

            dgList.DataSource = dataSet.Tables[0];

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
    }
}
