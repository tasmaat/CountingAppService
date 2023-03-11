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
    public partial class AddCardsForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        private MSDataBase dataBase = null;
        private DataSet dataSet, CardsdataSet = null;
        //private DataSet dataSet1 = null;
        //private DataSet dataSet2 = null;
        private Int64 colCards=0;

        Int64 bag_id, counting_id;

        public AddCardsForm()
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();
        }

        public AddCardsForm(Int64 id_bag, Int64 ib_counting, string name_bag)
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            dataBase = new MSDataBase();
            dataBase.Connect();
            
            bag_id = id_bag;
            counting_id = ib_counting;

            dataSet = dataBase.GetData9("Select * from [t_g_counting_content] where [id_bag] = " + id_bag);
            if (dataSet != null)
                if (dataSet.Tables[0].Rows.Count > 0)
                    colCards = dataSet.Tables[0].Rows.Count;
            if (dataSet.Tables[0].AsEnumerable().Any(x => x.Field<Int64>("id_currency") == 1002))
            //{
                colCards += 1;

               // label3.Text = (colCards).ToString().Trim();
           // }
            //else
                label3.Text = colCards.ToString().Trim();
            label3.ForeColor = Color.Red;
            label2.Text += name_bag.ToString().Trim();

            dgCards.AutoGenerateColumns = false;
            dgCards.RowHeadersWidth = 10;
            dgCards.Columns.Add("name", "Наименование карты");
            dgCards.Columns["name"].Visible = true;
            dgCards.Columns["name"].Width = 176;
            dgCards.Columns["name"].DataPropertyName = "name";
            dgCards.Columns["name"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dgCards.Columns.Add("id", "id");
            dgCards.Columns["id"].Visible = false;
            dgCards.Columns["id"].DataPropertyName = "id";
            sost();
        }

        private async void AddCardsForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
            // tbAddCard.Focus();
        }

        private void sost()
        {
            CardsdataSet = dataBase.GetData9("Select * from t_g_cards where id_bag = " + bag_id) ;
            if (CardsdataSet != null)
              //  if (CardsdataSet.Tables[0].Rows.Count > 0)
                    dgCards.DataSource = CardsdataSet.Tables[0];
            //else dgCards.
        }

        private void tbAddCard_KeyDown(object sender, KeyEventArgs e) //KeyEventArgs
        {
            if ((e.KeyCode == Keys.Escape) )//| (e.KeyCode == Keys.Enter))
            {

                tbAddCard.Text= tbAddCard.Text.Replace("+", "").Trim();
                tbAddCard.Text = tbAddCard.Text.Replace("-", "").Trim();
                if (tbAddCard.Text != String.Empty)
                {
                    Int64 i1 = 0;

                    if (!Int64.TryParse(tbAddCard.Text.ToString().Trim(), out i1))
                    {

                        MessageBox.Show("Номер карты должна быть целым числом!");
                        tbAddCard.Text = String.Empty;
                        return;

                    }

                    dataSet = dataBase.GetData9("select * from t_g_cards where name = '" + tbAddCard.Text.Trim() + "'");
                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        //DataRow row = cardsDataSet.Tables[0].NewRow();
                        //row["BeginCard"] = tbCard.Text;
                        //cardsDataSet.Tables[0].Rows.Add(row)

                        card_add();
                        sost();
                        tbAddCard.Text = String.Empty;

                    }
                    else MessageBox.Show(tbAddCard.Text + " - карта уже существует в базе обработок, введите другую карту!");
                }
            }
            if (e.KeyCode == Keys.Enter)
                if (tbAddCard.Text == String.Empty)
                    btnAdd_Click(sender,e);


        }
        private void card_add()
        {

            dataSet = dataBase.GetData9("INSERT INTO[dbo].[t_g_cards] ([name],[id_bag],[type],[creation],[lastupdate],[last_user_update],[id_counting],[id_user_create],[id_zona_create],[id_shift_create],[id_shift_current], [IDkassir], [FIOKASS]) values('" + tbAddCard.Text.Trim()+
                "','"+bag_id.ToString()+ "', '0' ,'" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','"+counting_id.ToString()+ "', " +
                "'" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserZona.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserShift.ToString() + 
                "','"+tbId.Text.Trim()+ "','"+tbName.Text.Trim()+"')");

            dataSet = dataBase.GetData9( "update t_g_bags set date_preparation=getdate() where id = '" + bag_id.ToString() + "' ");
           
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
           // if (dgCards.Rows.Count >= colCards)
            {
                if (tbAddCard.Text != String.Empty)
                {
                    dataSet = dataBase.GetData9("select * from t_g_cards where name = '" + tbAddCard.Text.Trim() + "'");
                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        //DataRow row = cardsDataSet.Tables[0].NewRow();
                        //row["BeginCard"] = tbCard.Text;
                        //cardsDataSet.Tables[0].Rows.Add(row)

                        card_add();
                        //sost();
                        //tbAddCard.Text = String.Empty;

                        this.DialogResult = DialogResult.OK;
                        Close();

                    }
                    else MessageBox.Show(tbAddCard.Text + " - карта уже существует в базе обработок, введите другую карту!");
                }
                else
                {
                    //card_add();
                    //sost();
                    //tbAddCard.Text = String.Empty;

                    this.DialogResult = DialogResult.OK;
                    Close();

                }
            }
           // else MessageBox.Show("Добавтье номер карты");
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            if (CardsdataSet != null)
                if (CardsdataSet.Tables[0].Rows.Count > 0)
                {
                    DataSet card = dataBase.GetData9("Select * from t_g_cards where fl_obr<>0 and id=" + CardsdataSet.Tables[0].Rows[dgCards.CurrentCell.RowIndex]["id"]);
                    DataSet card2 = dataBase.GetData9("Select * from t_g_counting_denom where id_card=" + CardsdataSet.Tables[0].Rows[dgCards.CurrentCell.RowIndex]["id"]);
                    if ((card.Tables[0].Rows.Count > 0) | (card2.Tables[0].Rows.Count > 0))
                    {
                        MessageBox.Show("Эту карту не возможно удалить, поскольку по этой карте был пересчет!");
                        return;
                    }

                    dataSet = dataBase.GetData9("DELETE FROM [dbo].[t_g_cards]      WHERE [id] = " + CardsdataSet.Tables[0].Rows[dgCards.CurrentCell.RowIndex]["id"] );
                    // dataSet1.Tables[0].Rows[dgSend1.CurrentCell.RowIndex]["id"]
                    sost();
                    //MessageBox.Show("deleted name =" + );
                }
               // else MessageBox.Show("CardsdataSet.Tables[0].Rows.Count > 0");
           // else MessageBox.Show("CardsdataSet != null");
                    
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
