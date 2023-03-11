using CountingDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.ParentForms
{
    public partial class AktNewForm : Form
    {
        public DataSet BagdefectfactorDataSet = null;
        public String Num_defect = "";
        MSDataBase dataBase = new MSDataBase();
        private DataSet bagdefectfactorDataSet = null;
        private DataSet bagdefectsDataSet = null;
        private DataSet factorsDataSet = null;
        private List<int> factrorList = null;
        public AktNewForm(int id_bag, String num_defect = "", DataSet bagdefectfactorDataSet=null)
        {
            InitializeComponent();
            
            dataBase.Connect();
            Console.WriteLine("id_bag="+id_bag.ToString());
            Console.WriteLine("num_defect='"+ num_defect+"'");
            bagdefectsDataSet = dataBase.GetData("t_g_bagdefects");
            if (bagdefectfactorDataSet != null)
                this.bagdefectfactorDataSet = bagdefectfactorDataSet;
            else
            {
                this.bagdefectfactorDataSet = dataBase.GetData9("select *, checking=0 from t_g_bagdefect_factor");
            if(id_bag!=0)
                {
                   DataSet d1= dataBase.GetData9("select * from t_w_bagdefect where id_bag = "+id_bag);
                    if (d1 != null)
                        if (d1.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < d1.Tables[0].Rows.Count; i++)
                            {
                                this.bagdefectfactorDataSet.Tables[0].Rows[Convert.ToInt32(d1.Tables[0].Rows[i]["id_bagdefectfactor"]) - 1]["checking"] = 1;
                                //bagdefectfactorDataSet.Tables[0].Rows[Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value) - 1]["checking"] = 1;

                            }
                            if (num_defect != "")
                                textBox1.Text = num_defect;
                            else
                                textBox1.Text = d1.Tables[0].Rows[0]["num_defect"].ToString();
                            Console.WriteLine("textBox1.Text=" + textBox1.Text);
                        }

                    for (int i = 0; i < this.bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                        Console.WriteLine("id = " + this.bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString() + "checking[" + i + "]=" + this.bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString());

                }
            }
            //Num_defect = num_defect;
            //textBox1.Text = Num_defect;

            dgBagsDefects.AutoGenerateColumns = false;
            dgBagsDefects.Columns.Add("Category", "Category");
            dgBagsDefects.Columns["Category"].Visible = true;
            dgBagsDefects.Columns["Category"].Width = 300;
            dgBagsDefects.Columns["Category"].DataPropertyName = "Category";
            dgBagsDefects.ReadOnly = true;
            dgBagsDefects.DataSource = bagdefectsDataSet.Tables[0];
            dgBagsDefects.RowHeadersWidth = 20;

            dgBagDefectFactors.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn dgAccountBoolColumn = new DataGridViewCheckBoxColumn();
            dgAccountBoolColumn.Name = "state";
            dgAccountBoolColumn.HeaderText = "Исп.";
            dgBagDefectFactors.Columns.Add(dgAccountBoolColumn);
            dgBagDefectFactors.Columns["state"].Visible = true;
            dgBagDefectFactors.Columns["state"].Width = 50;

            dgBagDefectFactors.Columns.Add("id", "id");
            dgBagDefectFactors.Columns["id"].Visible = false;
            dgBagDefectFactors.Columns["id"].DataPropertyName = "id";

            dgBagDefectFactors.Columns.Add("id_bagdefect", "id_bagdefect");
            dgBagDefectFactors.Columns["id_bagdefect"].Visible = false;
            dgBagDefectFactors.Columns["id_bagdefect"].DataPropertyName = "id_bagdefect";

            dgBagDefectFactors.Columns.Add("Category", "Category");
            dgBagDefectFactors.Columns["Category"].Visible = true;
            dgBagDefectFactors.Columns["Category"].Width = 300;
            dgBagDefectFactors.Columns["Category"].DataPropertyName = "Category";
           // dgBagDefectFactors.ReadOnly = true;
           // dgBagDefectFactors.DataSource = this.bagdefectfactorDataSet.Tables[0];
            dgBagDefectFactors.RowHeadersWidth = 20;
    


        }
        //Очистка 
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dgBagDefectFactors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (Convert.ToBoolean(senderGrid.Rows[e.RowIndex].Cells["state"].EditedFormattedValue) == false)
            {
                //MessageBox.Show("id="+senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()+ ", id_bagdefect=" + senderGrid.Rows[e.RowIndex].Cells["id_bagdefect"].Value.ToString());

                senderGrid.Rows[e.RowIndex].Cells["state"].Value = true;
                //factrorList.Add(Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id_bagdefect"].Value));
                if (bagdefectfactorDataSet != null)
                    bagdefectfactorDataSet.Tables[0].Rows[Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value)-1]["checking"] = 1;

                for (int i = 0; i < bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                    Console.WriteLine("id = " + bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString()+"checking["+i+"]=" + bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString());

                Console.WriteLine();

            }
            else
            {
                senderGrid.Rows[e.RowIndex].Cells["state"].Value = false;
                if (bagdefectfactorDataSet != null)
                    bagdefectfactorDataSet.Tables[0].Rows[Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value)-1]["checking"] = 0;

                for (int i = 0; i < bagdefectfactorDataSet.Tables[0].Rows.Count; i++)
                    Console.WriteLine("id = " + bagdefectfactorDataSet.Tables[0].Rows[i]["id"].ToString() + "checking[" + i + "]=" + bagdefectfactorDataSet.Tables[0].Rows[i]["checking"].ToString());

                Console.WriteLine();
            }
        }

        private void dgBagsDefects_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            detal(sender, e);
        }

        private void detal(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bagdefectfactorDataSet != null)
            {
                string s2 = bagdefectsDataSet.Tables[0].Rows[e.RowIndex]["id"].ToString();

                DataRow[] currentRows1 = ((DataTable)bagdefectfactorDataSet.Tables[0]).Select(" id_bagdefect in (" + s2.ToString() + ")");


                //factorsDataSet = dataBase.GetData("t_g_bagdefect_factor", "id_bagdefect", "-1");
                factorsDataSet = dataBase.GetData9("select *,checking=0 from t_g_bagdefect_factor where id_bagdefect=-1");

                foreach (DataRow dr in currentRows1)
                {
                    object[] row = dr.ItemArray;
                    factorsDataSet.Tables[0].Rows.Add(row);

                }


                if (factorsDataSet != null)

                    if (factorsDataSet.Tables.Count > 0)
                    {
                        dgBagDefectFactors.DataSource = factorsDataSet.Tables[0];

                        for (int i = 0; i < factorsDataSet.Tables[0].Rows.Count; i++)
                            if (factorsDataSet.Tables[0].Rows[i]["checking"].ToString() == "1")
                                dgBagDefectFactors.Rows[i].Cells["state"].Value = true;
                            else
                                dgBagDefectFactors.Rows[i].Cells["state"].Value = false;

                    }

                dgBagDefectFactors.AutoResizeColumns();

                //Убираем выделение с грида
                dgBagDefectFactors.ClearSelection();




            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            BagdefectfactorDataSet= bagdefectfactorDataSet;
            Num_defect = textBox1.Text.Trim();
            this.Close();
        }
    }
}
