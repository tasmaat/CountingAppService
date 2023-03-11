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

namespace CountingForms.DictionaryForms
{
    public partial class OpeningBagsForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = null;
        private DataSet dataSet = null;
        private DataSet dataSet1 = null;
        private DataSet dataSet2 = null;

        public OpeningBagsForm()
        {
            dataBase = new MSDataBase();
            dataBase.Connect();
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        private async void OpeningBagsForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            dgNotOpen.AutoGenerateColumns = false;
            dgNotOpen.RowHeadersWidth = 10;
            dgNotOpen.Columns.Add("name", "Наименование сумки");
            dgNotOpen.Columns["name"].Visible = true;
            dgNotOpen.Columns["name"].Width = 176;
            dgNotOpen.Columns["name"].DataPropertyName = "name";
            dgNotOpen.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dgNotOpen.Columns.Add("id", "id");
            dgNotOpen.Columns["id"].Visible = false;
            dgNotOpen.Columns["id"].DataPropertyName = "id";

            dgNotOpen.Columns.Add("owner_user", "owner_user");
            dgNotOpen.Columns["owner_user"].Visible = false;
            dgNotOpen.Columns["owner_user"].DataPropertyName = "owner_user";

            dgNotOpen.Columns.Add("id_counting", "id_counting");
            dgNotOpen.Columns["id_counting"].Visible = false;
            dgNotOpen.Columns["id_counting"].DataPropertyName = "id_counting";


            dgOpen.AutoGenerateColumns = false;
            dgOpen.RowHeadersWidth = 10;
            dgOpen.Columns.Add("name", "Наименование сумки");
            dgOpen.Columns["name"].Visible = true;
            dgOpen.Columns["name"].Width = 176;
            dgOpen.Columns["name"].DataPropertyName = "name";
            dgOpen.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgOpen.Columns.Add("id", "id");
            dgOpen.Columns["id"].Visible = false;
            dgOpen.Columns["id"].DataPropertyName = "id";
            sost();
        }
        
        private void sost()
        {
            dataSet1 = dataBase.GetData9("Select t1.*,c1.id as id_counting from t_g_bags t1 left join t_g_counting c1 on t1.id=c1.id_bag where t1.status=1 and c1.deleted=0 and t1.id_shift_current="+DataExchange.CurrentUser.CurrentUserShift);
            if (dataSet1 != null)
                if (dataSet1.Tables[0].Rows.Count > 0)
                    dgNotOpen.DataSource = dataSet1.Tables[0];
                else dgNotOpen.DataSource = null;

            dataSet2 = dataBase.GetData9("Select t1.*,c1.id as id_counting from t_g_bags t1 left join t_g_counting c1 on t1.id=c1.id_bag where t1.status=2 and c1.deleted=0 and t1.id_shift_current = "+DataExchange.CurrentUser.CurrentUserShift);
            if (dataSet2 != null)
                if (dataSet2.Tables[0].Rows.Count > 0)
                    dgOpen.DataSource = dataSet2.Tables[0];
                else dgOpen.DataSource = null;

        }

        private void Add1_Click(object sender, EventArgs e)
        {
            if(dataSet1!=null)
                if(dataSet1.Tables[0].Rows.Count>0)
                {
                    AddCardsForm addCardsForm = new AddCardsForm(Convert.ToInt64(dataSet1.Tables[0].Rows[dgNotOpen.CurrentCell.RowIndex]["id"]), Convert.ToInt64( dataSet1.Tables[0].Rows[dgNotOpen.CurrentCell.RowIndex]["id_counting"]) ,
                        dataSet1.Tables[0].Rows[dgNotOpen.CurrentCell.RowIndex]["name"].ToString());

                 
                    DialogResult dialogResult = addCardsForm.ShowDialog();

                    if(dialogResult == DialogResult.OK)
                    {
                        dataSet = dataBase.GetData9("update t_g_bags set status=2, [lastupdate]=getdate(), opening_date=GETDATE(), opening_user = " + DataExchange.CurrentUser.CurrentUserId+ ", last_user_update = " + DataExchange.CurrentUser.CurrentUserId + ", [owner_user] = " + DataExchange.CurrentUser.CurrentUserId + " where id = '" + dataSet1.Tables[0].Rows[dgNotOpen.CurrentCell.RowIndex]["id"]+"'");
                        dataSet = dataBase.GetData9("INSERT INTO [dbo].[t_g_bags_opening] ([id_bags],[create_user],[create_date],[owner_user],[opening_user],[last_update]) " +
                        "VALUES ('" + dataSet1.Tables[0].Rows[dgNotOpen.CurrentCell.RowIndex]["id"] + "' ,'" + DataExchange.CurrentUser.CurrentUserId+ "', getdate(), '"+ dataSet1.Tables[0].Rows[dgNotOpen.CurrentCell.RowIndex]["owner_user"] + "', '"+DataExchange.CurrentUser.CurrentUserId+"', getdate() )");
                        sost();

                    }
                    
                }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if(dataSet2!=null)
                if(dataSet2.Tables[0].Rows.Count>0)
                {
                    dataSet = dataBase.GetData9("SELECT *   FROM [CountingDB].[dbo].[t_g_bags_opening]   where id_bags= " + dataSet2.Tables[0].Rows[dgOpen.CurrentCell.RowIndex]["id"] + " order by id desc ");
                    if(dataSet!=null)
                        if(dataSet.Tables[0].Rows.Count>0)
                        {
                            Int64 owner_user = Convert.ToInt64(dataSet.Tables[0].AsEnumerable().Select(x=>x.Field<Int64>("owner_user")).First<Int64>());
                            //MessageBox.Show(owner_user.ToString());

                            dataSet = dataBase.GetData9("update t_g_bags set status=1, opening_date=null, [lastupdate]=getdate(), opening_user = null, last_user_update = " + DataExchange.CurrentUser.CurrentUserId + ", [owner_user] = '" + owner_user.ToString()+"' where id= "+ dataSet2.Tables[0].Rows[dgOpen.CurrentCell.RowIndex]["id"]);

                            sost();
                        }
                    
                }
        }

        private void tbOpening_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) | (e.KeyCode == Keys.Enter))
            {
                Add2_Click(sender, e);
            }
        }

        private void Add2_Click(object sender, EventArgs e)
        {
            if (tbOpening.Text != "")
            {
                if (dataSet1 != null)
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        string name = tbOpening.Text.Trim();
                        if (dataSet1.Tables[0].AsEnumerable().Any(
                            x => x.Field<string>("name").Trim() == name
                            ))
                        {
                            Int64 id_bag = Convert.ToInt64(dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>());
                            Int64 id_counting = Convert.ToInt64(dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Int64>("id_counting")).FirstOrDefault<Int64>());
                            //string name_bag = dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Int64>("id")).FirstOrDefault<Int64>()
                            Int64 owner_user = Convert.ToInt64(dataSet1.Tables[0].AsEnumerable().Where(x => x.Field<string>("name").Trim() == name).Select(x => x.Field<Int64>("owner_user")).FirstOrDefault<Int64>());

                            AddCardsForm addCardsForm = new AddCardsForm(id_bag, id_counting,name);

                            DialogResult dialogResult = addCardsForm.ShowDialog();

                            if (dialogResult == DialogResult.OK)
                            {
                                dataSet = dataBase.GetData9("update t_g_bags set status=2, opening_date=GETDATE(), opening_user = " + DataExchange.CurrentUser.CurrentUserId + ", last_user_update = " + DataExchange.CurrentUser.CurrentUserId + ", [owner_user] = " + DataExchange.CurrentUser.CurrentUserId + " where id = '" + id_bag.ToString() + "'");
                                dataSet = dataBase.GetData9("INSERT INTO [dbo].[t_g_bags_opening] ([id_bags],[create_user],[create_date],[owner_user],[opening_user],[last_update]) " +
                                "VALUES ('" + id_bag.ToString() + "' ,'" + DataExchange.CurrentUser.CurrentUserId + "', getdate(), '" + owner_user.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserId + "', getdate() )");
                                sost();

                            }

                        }
                        else MessageBox.Show(name+" - такой сумки нету в списке, введите другой номер сумки!");
                    }
                tbOpening.Text = string.Empty;
            }
        
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sost();
        }
    }
}
