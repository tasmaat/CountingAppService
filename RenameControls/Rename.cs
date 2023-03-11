using CountingDB;
using CountingDB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenameControls
{
    public partial class Rename : Form
    {
        MSDataBaseAsync dataBase = new MSDataBaseAsync();        
        private List<t_g_role_permisions> rolePermisions = null;
        public Rename()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;            
        }

        private async void Rename_Load(object sender, EventArgs e)
        {            
            rolePermisions = await dataBase.GetData<t_g_role_permisions>();
            var rpg = rolePermisions.GroupBy(p => p.controlName);
            foreach (IGrouping<string,t_g_role_permisions> item in rpg)
            {
                //lvControls.Items.Add(item.Key);               

                foreach (t_g_role_permisions it in item)
                {
                    lvControls.Items.Add(it.controlName).SubItems.Add(it.description);
                    break;
                }
            }
            lvControls.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(lvControls.SelectedItems.Count > 0)
            {
                lvControls.SelectedItems[0].SubItems[1].Text = textBox1.Text;
                lvControls.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }           
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvControls.Items)
            {
                foreach (t_g_role_permisions rp in rolePermisions)
                {
                    if (rp.controlName == item.Text)
                    {
                        if(item.SubItems.Count > 1 && item.SubItems[1].Text!="")
                        {
                            rp.description = item.SubItems[1].Text;
                            rp.updated = true;
                        }                            
                    }                        
                }
            }
            int c = await dataBase.UpdateDB(rolePermisions);
            MessageBox.Show($"Обновленно {c} записей", "Обновление базы");
        }
    }
}
