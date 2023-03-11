using CountingDB;
using CountingDB.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class Roles : Form
    {
        private MSDataBaseAsync dataBase = null;
        private List<t_g_role> roles = null;
        private List<t_g_role> delRoles = null;
        private BindingSource bindingSource;
        private Dictionary<string, long> copyRole = null;        
        private string btnClicked;

        public Roles()
        {
            InitializeComponent();
            dataBase = new MSDataBaseAsync();
            bindingSource = new BindingSource();
            copyRole = new Dictionary<string, long>();
            delRoles = new List<t_g_role>();
            btnClicked = "";
        }

        private async void Roles_Load(object sender, EventArgs e)
        {
            roles = await dataBase.GetDataWhere<t_g_role>("WHERE is_active = 1");
            bindingSource.DataSource = roles;
            LBRoles.DataSource = bindingSource;
            LBRoles.DisplayMember = "name";
            LBRoles.ValueMember = "id";            
            LBRoles.ClearSelected();
            errorProvider1.ContainerControl = this;
            tbRoleName.Text = "";                
            this.ActiveControl = tbRoleName;
        }

        private void LBRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LBRoles.SelectedValue != null)
                tbRoleName.Text = ((t_g_role)LBRoles.SelectedItem).name;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(IsNotEmpty() && IsUniqueRoleName())
            {
                roles.Add(new t_g_role() { name = tbRoleName.Text, isNew = true });
                bindingSource.ResetBindings(true);
                LBRoles.ClearSelected();
            }            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty() && IsUniqueRoleName() && IsSelectedRole())
            {
                ((t_g_role)LBRoles.SelectedItem).name = tbRoleName.Text;
                ((t_g_role)LBRoles.SelectedItem).updated = true;
                bindingSource.ResetBindings(true);
                LBRoles.ClearSelected();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty() && IsUniqueRoleName() && IsSelectedRole())
            {
                roles.Add(new t_g_role() { name = tbRoleName.Text, isNew = true });
                copyRole.Add(tbRoleName.Text, ((t_g_role)LBRoles.SelectedItem).id);
                bindingSource.ResetBindings(true);
                LBRoles.ClearSelected();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(IsSelectedRole())
            {
                t_g_role del = (t_g_role)LBRoles.SelectedItem;
                del.is_active = false;
                del.updated = true;
                delRoles.Add(del);
                roles.Remove(del);
                bindingSource.ResetBindings(true);
                LBRoles.ClearSelected();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button current = (Button)sender;
            btnClicked = current.Name;
        }

        private bool IsSelectedRole()
        {
            if(LBRoles.SelectedItems.Count==0)
            {
                if (btnClicked == "btnCopy")
                {
                    errorProvider1.SetError(LBRoles, "Выберите роль для клонирования");
                    return false;
                }
                else if(btnClicked == "btnDelete")
                {
                    errorProvider1.SetError(LBRoles, "Выберите роль для удаления");
                    return false;
                }
                else if (btnClicked == "btnModify")
                {
                    errorProvider1.SetError(LBRoles, "Выберите роль для изменения");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(LBRoles, "");
                    return false;
                }
            }
            else
            {
                errorProvider1.SetError(LBRoles, "");
                return true;
            }
        }

        private bool IsNotEmpty()
        {            
            if (tbRoleName.Text.Trim() == "")
            {
                if (btnClicked == "btnAdd" || btnClicked == "btnCopy")
                {
                    errorProvider1.SetError(tbRoleName, "Укажите имя новой роли");
                    return false;
                }                    
                else if (btnClicked == "btnModify")
                {
                    errorProvider1.SetError(tbRoleName, "Укажите новое имя роли");
                    return false;
                }                    
                else
                {
                    errorProvider1.SetError(tbRoleName, "");
                    return true;
                }                
            }
            else
            {
                errorProvider1.SetError(tbRoleName, "");
                return true;
            }                        
        }

        private bool IsUniqueRoleName()
        {            
            foreach (t_g_role item in roles)
            {
                if (tbRoleName.Text == item.name)
                {
                    errorProvider1.SetError(tbRoleName, "Название роли должно быть уникальным");
                    return false;
                }                    
            }
            errorProvider1.SetError(tbRoleName, "");
            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            foreach (t_g_role item in delRoles)
            {
                roles.Add(item);
            }
            delRoles = new List<t_g_role>();
            int count = await dataBase.UpdateDB<t_g_role>(roles);
            roles = await dataBase.GetDataWhere<t_g_role>("WHERE is_active = 1");
            bindingSource.DataSource = roles;
            if (copyRole.Count>0)
            {
                List<t_g_role_permisions> rpfrom = new List<t_g_role_permisions>();
                List<t_g_role_permisions> rpto = new List<t_g_role_permisions>();
                foreach (KeyValuePair<string, long> item in copyRole)
                {
                    rpfrom = await dataBase.GetDataWhere<t_g_role_permisions>($"WHERE roleId = {item.Value}");
                    long idTo = roles.Find(p => p.name == item.Key).id;
                    rpto.AddRange(rpfrom.ToArray());
                    foreach (t_g_role_permisions rp in rpto)
                    {
                        rp.roleId = idTo;
                        rp.isNew = true;
                    }
                    int c = await dataBase.UpdateDB<t_g_role_permisions>(rpto);
                }
                copyRole = new Dictionary<string, long>();
            }
            MessageBox.Show($"Обновленно {count} записей", "Обновление базы");
        }
    }
}
