using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataExchange;
using CountingDB;
using CountingForms.Services;
using CountingDB.Entities;

namespace CountingForms.DictionaryForms
{
    public partial class ConditionDictForm : DictionaryForm
    {

        public DataTable blokTable;

        public ConditionDictForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public ConditionDictForm(String formName, String gridFieldName, string strCaption, string tableName) : base(formName, gridFieldName, strCaption, tableName)
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
            /////21.10.2019
            blokTable = new DataTable("blok");
            DataColumn p1 = new DataColumn("id", Type.GetType("System.Boolean"));
            DataColumn p2 = new DataColumn("nameblok", Type.GetType("System.String"));
            blokTable.Columns.Add(p1);
            blokTable.Columns.Add(p2);
            blokTable.Rows.Add(new object[] { "False", "Видимый" });
            blokTable.Rows.Add(new object[] { "True", "Не видимый" });


            DataGridViewComboBoxColumn dg1 = new DataGridViewComboBoxColumn();
            dg1.Name = "visible";
            dg1.HeaderText = "Видимость";
            //dg1.FlatStyle = FlatStyle.Flat;
            dg1.DataPropertyName = "visible";
            dg1.DisplayMember = "nameblok";
            dg1.ValueMember = "id";
            dg1.ReadOnly = true;
            dg1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dg1.DataSource = blokTable;
            dgList.Columns.Add(dg1);
            dgList.Columns["visible"].SortMode = DataGridViewColumnSortMode.NotSortable;



            dg.Width = 420;
            dg.AutoGenerateColumns = false;
            dg.Columns.Add("short_evidence", "Признак");
            dg.Columns["short_evidence"].Visible = true;
            dg.Columns["short_evidence"].Width = 100;
            dg.Columns["short_evidence"].DataPropertyName = "short_evidence";

            dg.Columns.Add("evidence", "Описание");
            dg.Columns["evidence"].Visible = true;
            dg.Columns["evidence"].Width = 314;
            dg.Columns["evidence"].DataPropertyName = "evidence";

            dg.Columns.Add("id", "id");
            dg.Columns["id"].Visible = false;
            dg.Columns["id"].DataPropertyName = "id";

            dg.ReadOnly = true;
            dg.RowHeadersWidth = 4;

            /////21.10.2019

        }

        private async void ConditionDictForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        protected override void tbName_TextChanged(object sender, EventArgs e)
        {
            
            base.tbName_TextChanged(sender, e);
        }

        /// 21.10.2019
        protected override void dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgList.CurrentCell != null)
            {
               
                int rowIndex = dgList.CurrentCell.RowIndex;
                base.dgList_SelectionChanged(sender, e);


                cbVisisble.Checked = Convert.ToBoolean(dgList.CurrentRow.Cells["visible"].Value);// base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["visible"]);
                detal();
            }

        }
        /// 21.10.2019

        protected override void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            /// 21.10.2019
            if (e.RowIndex > -1)
            {
                /// 21.10.2019
                
                /// 21.10.2019
                // tbName.Text = dgList[e.ColumnIndex, e.RowIndex].Value.ToString();
                /// 21.10.2019

                if (dataSet.Tables[0].Rows[e.RowIndex]["visible"] != DBNull.Value && Convert.ToBoolean(dataSet.Tables[0].Rows[e.RowIndex]["visible"]) == true)
            {
                cbVisisble.CheckState = CheckState.Checked;
            }
            else
            {
                cbVisisble.CheckState = CheckState.Unchecked;
            }
                /// 21.10.2019
            }
            /// 21.10.2019
        }

        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            int result = dgList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() == tbName.Text).Count();
            if (tbName.Text != String.Empty && result == 0)
            {
                DataRow dataRow = dataSet.Tables[0].NewRow();
                dataRow["creation"] = DateTime.Now;
                dataRow["name"] = tbName.Text;
                dataRow["Visible"] = cbVisisble.CheckState == CheckState.Checked; 
                dataRow["last_update_user"] = CurrentUser.CurrentUserId;
                dataRow["lastupdate"] = DateTime.Now;

                //dataSet.Tables.Add((DataTable) dgList.DataSource);
                dataSet.Tables[0].Rows.Add(dataRow);
                dataBase.UpdateData(dataSet);

                /// 21.10.2019
                dgList.CurrentCell = dgList[gridFieldName, dgList.RowCount - 1];
                base.dgList_SelectionChanged(sender, e);
                dgList.Refresh();
                dgList.Update();


                cbVisisble.Checked = Convert.ToBoolean(base.dataSet.Tables[0].Rows[dgList.CurrentRow.Index]["visible"]);


                /// 21.10.2019


            }
        }

        protected override void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            //dgList.CurrentCell.Value = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["creation"] = dataSet.Tables[0].Rows[rowIndex]["creation"];
            dataSet.Tables[0].Rows[rowIndex]["name"] = tbName.Text;
            dataSet.Tables[0].Rows[rowIndex]["Visible"] = cbVisisble.CheckState == CheckState.Checked;
            dataSet.Tables[0].Rows[rowIndex]["last_update_user"] = CurrentUser.CurrentUserId;
            dataSet.Tables[0].Rows[rowIndex]["lastupdate"] = DateTime.Now;

            dataBase.UpdateData(dataSet);

        }

        private void cbVisisble_CheckedChanged(object sender, EventArgs e)
        {
            int rowIndex = dgList.CurrentCell.RowIndex;
            if (Convert.ToBoolean(dataSet.Tables[0].Rows[rowIndex]["visible"]) != cbVisisble.Checked)
            {
                if(pm.EnabledPossibility(perm, btnModify))
                    btnModify.Enabled = true;
                if (pm.EnabledPossibility(perm, btnAdd))
                    btnAdd.Enabled = true;

            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;

            }
        }

        private void btnAddEvidence_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("Необходимо заполнить признак и описание");
                return;
            }
            string name = dgList.CurrentRow.Cells["name"].Value.ToString();
            long id = dataSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("name") == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
                        
            Console.WriteLine("name="+name);
            Console.WriteLine("id=" + id.ToString());
            
            if (dgList!=null)
                if(dgList.RowCount>0)
                    if(dgList.CurrentCell!=null)
                    {
                        string s1 = "insert into t_g_condition_factor (" +
                            "[id_condition], [short_evidence],     [evidence] ,        [creation], [lastupdate], [last_update_user])" +
                            " values (" +
                            id+",            '"+textBox1.Text+"','"+textBox2.Text+"', getdate(),  getdate(),   "+DataExchange.CurrentUser.CurrentUserId+")";
                        Console.WriteLine(s1);
                        dataBase.GetData9(s1);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        detal();
                    }
        }
        private void detal()
        {
            string name = dgList.CurrentRow.Cells["name"].Value.ToString();
            long id = dataSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("name") == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();

            Console.WriteLine("name=" + name);
            Console.WriteLine("id=" + id.ToString());

            if (dgList != null)
                if (dgList.RowCount > 0)
                    if (dgList.CurrentCell != null)
                    {
                        DataSet d1 = dataBase.GetData9("select * from t_g_condition_factor where id_condition="+id);
                        if(d1!=null)
                            if(d1.Tables[0]!=null)
                                if(d1.Tables[0].Rows.Count>0)
                                {
                                    //dgEvidence.DataSource = d1.Tables[0];
                                    dg.DataSource = d1.Tables[0];
                                }
                                else
                                {
                                    dg.DataSource = null;
                                }
                    }
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void btnModifyEvidence_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Необходимо заполнить признак и описание");
                return;
            }
            //string name = dgList.CurrentRow.Cells["name"].Value.ToString();
            //long id = dataSet.Tables[0].AsEnumerable().Where(x => x.Field<string>("name") == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>();
            //Console.WriteLine("RowIndex=" + dg.CurrentCell.RowIndex);
            //Console.WriteLine("name=" + dg.CurrentRow.Cells["short_evidence"].Value.ToString());
            //Console.WriteLine("id=" + dg.CurrentRow.Cells["id"].Value.ToString());
            string id = dg.CurrentRow.Cells["id"].Value.ToString();
            Console.WriteLine("id=" + id.ToString());

            if (dg != null)
                if (dg.RowCount > 0)
                    if (dg.CurrentCell != null)
                    {
                        string s1 = "UPDATE [dbo].[t_g_condition_factor]   SET " +
                            "[short_evidence] = '" + textBox1.Text +
                            "',[evidence] = '" + textBox2.Text +
                            "' where id =" + id;
                        Console.WriteLine(s1);
                        dataBase.GetData9(s1);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        detal();
                    }


        }

        private void dg_SelectionChanged(object sender, EventArgs e)
        {
           
            if (dg!=null)
                if(dg.Rows.Count>0)
                {
                    textBox1.Text = dg.CurrentRow.Cells["short_evidence"].Value.ToString();
                    textBox2.Text = dg.CurrentRow.Cells["evidence"].Value.ToString();
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
         
            
        }

        private void btnDeleteEvidence_Click(object sender, EventArgs e)
        {
            string id = dg.CurrentRow.Cells["id"].Value.ToString();
            Console.WriteLine("id=" + id.ToString());

            if (dg != null)
                if (dg.RowCount > 0)
                    if (dg.CurrentCell != null)
                    {
                        string s1 = "DELETE FROM [dbo].[t_g_condition_factor] " +                            
                            " where id =" + id;
                        Console.WriteLine(s1);
                        dataBase.GetData9(s1);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        detal();
                    }
        }

      
    }
}
