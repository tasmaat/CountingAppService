using CountingDB;
using CountingDB.Entities;
using CountingForms.Interfaces;
using CountingForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.Services
{
    public class PermisionsManager : IPermisionsManager
    {
        MSDataBaseAsync dataBase = new MSDataBaseAsync();
        private Dictionary<string, string> oldMenuToolTips = new Dictionary<string, string>();
        public void ClearToolTips(Form form, ToolTip tt)
        {
            tt.RemoveAll();
            foreach (var control in form.Controls)
            {
                if (control is MenuStrip)
                {
                    MenuStrip menuStrip = control as MenuStrip;
                    foreach (ToolStripMenuItem item in menuStrip.Items)
                    {
                        if (item.DropDownItems.Count > 0)
                        {
                            foreach (ToolStripMenuItem it in item.DropDownItems)
                            {
                                it.ToolTipText = oldMenuToolTips[item.Name];
                            }
                        }
                        if (oldMenuToolTips.ContainsKey(item.Name))
                            item.ToolTipText = oldMenuToolTips[item.Name];
                    }
                }
            }
            oldMenuToolTips.Clear();
        }

        public void ShowControls(Control.ControlCollection controls, ToolTip tt, ref List<ControlNames> ctrls)
        {            
            foreach (Control control in controls)
            {
                if (control.Controls.Count > 0)
                {
                    ShowControls(control.Controls, tt, ref ctrls);
                }             
                if (control is Button || 
                    control is TextBox || 
                    control is RadioButton || 
                    control is CheckBox ||
                    control is Label ||
                    control is DataGridView)
                {
                    tt.SetToolTip(control, control.Name);
                    ctrls.Add(new ControlNames(control.Name,""));
                }
            }
        }

        public void ShowMenuItems(Control.ControlCollection controls, ref List<ControlNames> mi)
        {
            foreach (Control control in controls)
            {              
                if (control is MenuStrip)
                {
                    MenuStrip menuStrip = control as MenuStrip;
                    ShowToolStipItems(menuStrip.Items, ref mi);
                }               
            }
        }

        private void ShowToolStipItems(ToolStripItemCollection items, ref List<ControlNames> mi)
        {
            foreach (ToolStripMenuItem menuItem in items)
            {
                oldMenuToolTips.Add(menuItem.Name, menuItem.ToolTipText);
                menuItem.ToolTipText = menuItem.Name;

                if (menuItem.DropDownItems.Count > 0)
                {
                    ShowToolStipItems(menuItem.DropDownItems, ref mi);
                }
                mi.Add(new ControlNames(menuItem.Name,""));
            }
        }

        public void ChangeControlByRole(Control.ControlCollection controls, List<t_g_role_permisions> perm)
        {            
            if(perm.Count>0)
            {
                foreach (Control control in controls)
                {
                    if (control.Controls.Count > 0)
                    {
                        ChangeControlByRole(control.Controls, perm);
                    }
                    if (control is MenuStrip)
                    {
                        MenuStrip menuStrip = control as MenuStrip;
                        ChangeMenuItems(menuStrip.Items, perm);                       
                    }

                    foreach (t_g_role_permisions rp in perm)
                    {                        
                        if(control.Name == rp.controlName)
                        {
                            if(control is Button)
                            {
                                if(control.Name == "btnAdd" || control.Name == "btnModify" || control.Name == "btnDelete")
                                {
                                    Sanction snc = new Sanction(rp.sanction, rp.self_sanction, rp.is_sanctioner);
                                    control.Tag = snc;
                                }
                            }
                            control.Visible = rp.visible;
                            control.Enabled = rp.enabled;
                        }
                    }
                }
            }
        }

        private void ChangeMenuItems(ToolStripItemCollection items, List<t_g_role_permisions> perm)
        {
            foreach (ToolStripMenuItem menuItem in items)
            {
                foreach (t_g_role_permisions rp in perm)
                {
                    if(menuItem.Name == rp.controlName)
                    {
                        menuItem.Enabled = rp.enabled;
                        menuItem.Visible = rp.visible;
                    }
                }                

                if (menuItem.DropDownItems.Count > 0)
                {
                    ChangeMenuItems(menuItem.DropDownItems, perm);
                }                
            }
        }

        public bool EnabledPossibility(List<t_g_role_permisions> rp, Control cn)
        {
            if (rp != null)
            {
                if (rp.FirstOrDefault(p => p.controlName == cn.Name) != null)
                {
                    if (rp.FirstOrDefault(p => p.controlName == cn.Name).enabled)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return true;
        }

        public bool VisiblePossibility(List<t_g_role_permisions> rp, Control cn)
        {
            if (rp != null)
            {
                if (rp.FirstOrDefault(p => p.controlName == cn.Name) != null)
                {
                    if (rp.FirstOrDefault(p => p.controlName == cn.Name).visible)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return true;
        }
    }
}
