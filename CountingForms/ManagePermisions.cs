using CountingDB;
using CountingDB.Entities;
using CountingForms.Helpers;
using CountingForms.Interfaces;
using CountingForms.Models;
using CountingForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms
{
    public partial class ManagePermisions : Form
    {
        private IPermisionsManager pm;        
        private Form workingForm;
        private ToolTip formToolTip1 = null;
        private ToolTip formToolTip2 = null;
        MSDataBaseAsync dataBase = null;
        private List<t_g_role> role = null;
        private List<t_g_role_permisions> rolePermisions = null;
        private List<ControlNames> mi = null;

        public ManagePermisions()
        {
            InitializeComponent();
        }
        public ManagePermisions(Form f, ToolTip toolTip1, ToolTip toolTip2)
        {
            InitializeComponent();

            dataBase = new MSDataBaseAsync();
            pm = new PermisionsManager();
            mi = new List<ControlNames>();

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text += " for page " + f.Name;

            workingForm = f;
            formToolTip1 = toolTip1;
            formToolTip2 = toolTip2;
            formToolTip1.Active = false;
            formToolTip2.Active = true;           

            pm.ShowMenuItems(f.Controls, ref mi);
            pm.ShowControls(f.Controls, formToolTip2, ref mi);
        }         

        private async void ManagePermisions_Load(object sender, EventArgs e)
        {
            role = await dataBase.GetData<t_g_role>();
            rolePermisions = await dataBase.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + workingForm.Name + "'");            
            LBRoles.DataSource = role;            
            LBRoles.DisplayMember = "name";
            LBRoles.ValueMember = "id";

            FillDicValue();
            LBControl.DataSource = mi;
            LBControl.DisplayMember = "userFriendlyName";
            LBControl.ValueMember = "controlName";

            FillTreeView();            
        }

        private void ManagePermisions_FormClosing(object sender, FormClosingEventArgs e)
        {
            pm.ClearToolTips(workingForm, formToolTip2);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            UpdateRolePermissionList();
            int res = await dataBase.UpdateDB(rolePermisions);
            MessageBox.Show($"Обновленно {res} записей", "Обновление базы");
            rolePermisions = await dataBase.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + workingForm.Name + "'");
            FillTreeView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool containsRole = false;
            bool containsCon = false;
            foreach (t_g_role selRol in LBRoles.SelectedItems)
            {
                TreeNode NodeRole = null;
                Role sr = new Role(selRol.id, selRol.name);
                int ind;
                if (TreeViewHelper.TreeViewChildContainsText(tvRolePermisions, sr.name, out ind))
                {
                    containsRole = true;
                    NodeRole = tvRolePermisions.Nodes[ind];
                    NodeRole.Tag = sr;
                }
                else
                {
                    NodeRole = new TreeNode(sr.name);
                    NodeRole.Tag = sr;
                }

                foreach (ControlNames selCon in LBControl.SelectedItems)
                {                                        
                    ViewElement ve = new ViewElement(selCon.controlName, cbVis.Checked, cbEnab.Checked, cbSanction.Checked, cbSelfSanction.Checked, cbSanctioner.Checked);
                    TreeNode NodeCon = new TreeNode();
                    if (TreeViewHelper.NodeChildContainsText(NodeRole, selCon, out ind))
                    {
                        containsCon = true;
                        NodeCon = NodeRole.Nodes[ind];
                        NodeCon.Nodes.Clear();
                        ViewElement veOld = (ViewElement)NodeCon.Tag;
                        ve.id = veOld.id;
                        ve.updated = true;
                        NodeCon.Tag = ve;
                    }
                    else
                    {
                        NodeCon = new TreeNode(selCon.userFriendlyName);
                        NodeCon.Tag = ve;
                    }

                    TreeNode NodeConVis = new TreeNode("Visible - " + cbVis.Checked.ToString());
                    TreeNode NodeConEn = new TreeNode("Enabled - " + cbEnab.Checked.ToString());
                    if(cbSanctioner.Checked || cbSanction.Checked || cbSelfSanction.Checked)
                    {
                        TreeNode NodeSanction = new TreeNode("Санкция - " + cbSanction.Checked.ToString());
                        TreeNode NodeSelfSanction = new TreeNode("Самосанкция - " + cbSelfSanction.Checked.ToString());
                        TreeNode NodeSanctioner = new TreeNode("Санкционер - " + cbSanctioner.Checked.ToString());
                        NodeCon.Nodes.Add(NodeConVis);
                        NodeCon.Nodes.Add(NodeConEn);
                        NodeCon.Nodes.Add(NodeSanction);
                        NodeCon.Nodes.Add(NodeSelfSanction);
                        NodeCon.Nodes.Add(NodeSanctioner);
                    }
                    else
                    {
                        NodeCon.Nodes.Add(NodeConVis);
                        NodeCon.Nodes.Add(NodeConEn);
                    }                    
                    if (containsCon)
                        containsCon = false;
                    else
                        NodeRole.Nodes.Add(NodeCon);
                }
                if (containsRole)
                    containsRole = false;
                else
                    tvRolePermisions.Nodes.Add(NodeRole);
            }          
        }

        private void FillDicValue()
        {
            foreach (ControlNames item in mi)
            {
                foreach (t_g_role_permisions rp in rolePermisions)
                {
                    if (item.controlName == rp.controlName && rp.description != null)
                        item.userFriendlyName = rp.description;
                }
            }
            foreach (ControlNames item in mi)
            {
                if (String.IsNullOrEmpty(item.userFriendlyName))
                    item.userFriendlyName = item.controlName;
            }
        }

        private void FillTreeView()
        {
            int ind;
            bool containsRole = false;
            bool containsCon = false;            
            var rpg = rolePermisions.GroupBy(p => p.roleId).ToList();            
            foreach (var item in rpg)
            {
                foreach (var it in item)
                {
                    TreeNode NodeRole = null;
                    string roleName = role.Find(p => p.id == it.roleId).name;
                    Role sr = new Role((long)it.roleId, roleName);                    
                    if (TreeViewHelper.TreeViewChildContainsText(tvRolePermisions, sr.name, out ind))
                    {
                        containsRole = true;
                        NodeRole = tvRolePermisions.Nodes[ind];
                        NodeRole.Tag = sr;
                    }
                    else
                    {
                        NodeRole = new TreeNode(sr.name);
                        NodeRole.Tag = sr;
                    }
                    ControlNames cn;
                    if (String.IsNullOrEmpty(it.description))
                        cn = new ControlNames(it.controlName, it.controlName);  
                    else
                        cn = new ControlNames(it.controlName, it.description);
                    ViewElement ve = new ViewElement(it.id, it.controlName, (bool)it.visible, (bool)it.enabled, (bool)it.sanction, (bool)it.self_sanction, (bool)it.is_sanctioner);
                    TreeNode NodeCon = new TreeNode();                                        
                    if (TreeViewHelper.NodeChildContainsText(NodeRole, cn, out ind))
                    {
                        containsCon = true;
                        NodeCon = NodeRole.Nodes[ind];
                        NodeCon.Nodes.Clear();
                        NodeCon.Tag = ve;
                    }
                    else
                    {
                        NodeCon = new TreeNode(cn.userFriendlyName);
                        NodeCon.Tag = ve;
                    }

                    TreeNode NodeConVis = new TreeNode("Visible - " + it.visible.ToString());
                    TreeNode NodeConEn = new TreeNode("Enabled - " + it.enabled.ToString());
                    if(it.sanction || it.self_sanction || it.is_sanctioner)
                    {
                        TreeNode NodeSanction = new TreeNode("Санкция - " + it.sanction.ToString());
                        TreeNode NodeSelfSanction = new TreeNode("Самосанкция - " + it.self_sanction.ToString());
                        TreeNode NodeSanctioner = new TreeNode("Санкционер - " + it.is_sanctioner.ToString());
                        NodeCon.Nodes.Add(NodeConVis);
                        NodeCon.Nodes.Add(NodeConEn);
                        NodeCon.Nodes.Add(NodeSanction);
                        NodeCon.Nodes.Add(NodeSelfSanction);
                        NodeCon.Nodes.Add(NodeSanctioner);
                    }
                    else
                    {
                        NodeCon.Nodes.Add(NodeConVis);
                        NodeCon.Nodes.Add(NodeConEn);
                    }                  
                    if (containsCon)
                        containsCon = false;
                    else
                        NodeRole.Nodes.Add(NodeCon);
                    if (containsRole)
                        containsRole = false;
                    else
                        tvRolePermisions.Nodes.Add(NodeRole);
                }                     
            }
        }     
        
        private void UpdateRolePermissionList()
        {
            int ind = -1;
            var roleNodes = tvRolePermisions.Nodes;
            foreach (TreeNode roleNode in roleNodes)
            {
                Role role = (Role)roleNode.Tag;             
                long roleId = role.id;                
                foreach (TreeNode conNode in roleNode.Nodes)
                {
                    ViewElement ve = (ViewElement)conNode.Tag;
                    ind = rolePermisions.FindIndex(p => p.id == ve.id);
                    if(ind > -1)
                    {                        
                        rolePermisions[ind].enabled = (bool)ve.enabled;
                        rolePermisions[ind].visible = (bool)ve.visible;
                        rolePermisions[ind].sanction = (bool)ve.sanction;
                        rolePermisions[ind].self_sanction = (bool)ve.selfSanction;
                        rolePermisions[ind].is_sanctioner = (bool)ve.isSanctioner;
                        rolePermisions[ind].updated = ve.updated;
                        ind = 0;
                    }
                    else
                    {
                        t_g_role_permisions rp = new t_g_role_permisions();
                        rp.roleId = role.id;
                        rp.formName = workingForm.Name;
                        rp.controlName = ve.controlName;
                        rp.enabled = (bool)ve.enabled;
                        rp.visible = (bool)ve.visible;
                        rp.sanction = (bool)ve.sanction;
                        rp.self_sanction = (bool)ve.selfSanction;
                        rp.is_sanctioner = (bool)ve.isSanctioner;
                        rp.isNew = true;
                        rolePermisions.Add(rp);
                    }
                }
            }
        }

        private void LBControl_SelectedValueChanged(object sender, EventArgs e)
        {
            if(LBControl.SelectedIndex != -1)
            {
                if (LBControl.SelectedValue.ToString() == "btnAdd" ||
                    LBControl.SelectedValue.ToString() == "btnDelete" ||
                    LBControl.SelectedValue.ToString() == "btnModify")
                {
                    cbSanction.Visible = true;
                    cbSelfSanction.Visible = true;
                    cbSanctioner.Visible = true;
                }
                else
                {
                    cbSanction.Visible = false;
                    cbSelfSanction.Visible = false;
                    cbSanctioner.Visible = false;
                }
            }           
        }
    }      
}
