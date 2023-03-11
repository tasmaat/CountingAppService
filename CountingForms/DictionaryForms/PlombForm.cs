using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class PlombForm : Form
    {
        private IPermisionsManager pm;
        private MSDataBaseAsync dataBaseAsync;
        private List<t_g_role_permisions> perm;

        DataSet dataSet = null;
        private BindingSource bindingSource;
        public PlombForm(string formName, string id)
        {
            InitializeComponent();

            pm = new PermisionsManager();
            dataBaseAsync = new MSDataBaseAsync();

            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            this.Text = formName;
            
            dgList.AutoGenerateColumns = false;
            
            dgList.Columns.Add("name_pechat", "Название");
            dgList.Columns["name_pechat"].DataPropertyName = "name_pechat";

            dataSet = dataBase.GetData9("Select * from t_g_pechatclien where id_client="+id);
            
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataSet.Tables[0];

            dgList.DataSource = bindingSource;

           // pokazimg();


        }

        private async void PlombForm_Load(object sender, EventArgs e)
        {
            perm = await dataBaseAsync.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgList_SelectionChanged(object sender, EventArgs e)
        {
            pokazimg();
        }

        private void pokazimg()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            if (dgList.CurrentCell != null)
            {
                if (dataSet != null)
                    if (dataSet.Tables[0] != null)
                        if (dgList.CurrentRow.Index < dataSet.Tables[0].Rows.Count)
                        {
                            if ((((DataRowView)bindingSource.Current)["img1"] != null) & (((DataRowView)bindingSource.Current)["img1"] != DBNull.Value))
                            {
                                MemoryStream memoryStream = new MemoryStream();

                                memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img1"], 0, ((byte[])((DataRowView)bindingSource.Current)["img1"]).Length);
                                pictureBox1.Image = Image.FromStream(memoryStream);

                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                                memoryStream.Dispose();

                            }
                            else
                                pictureBox1.Image = null;

                            if ((((DataRowView)bindingSource.Current)["img2"] != null) & (((DataRowView)bindingSource.Current)["img2"] != DBNull.Value))
                            {

                                MemoryStream memoryStream = new MemoryStream();

                                memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img2"], 0, ((byte[])((DataRowView)bindingSource.Current)["img2"]).Length);

                                pictureBox2.Image = Image.FromStream(memoryStream);

                                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                                memoryStream.Dispose();

                            }
                            else
                                pictureBox2.Image = null;
                        }
            }
        }

       
    }
}
