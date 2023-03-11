using CountingForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.Helpers
{
    public class TreeViewHelper
    {
        public static bool NodeChildContainsText(TreeNode parentNode, ControlNames cn, out int ind)
        {
            ind = -1;
            if(parentNode.Nodes.Count > 0)
            {
                for (int i = 0; i < parentNode.Nodes.Count; i++)
                {
                    if (parentNode.Nodes[i].Text == cn.controlName || parentNode.Nodes[i].Text == cn.userFriendlyName)
                    {
                        ind = i;
                        return true;
                    }                        
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool TreeViewChildContainsText(TreeView parent, string text, out int ind)
        {
            ind = -1;
            if (parent.Nodes.Count > 0)
            {
                for (int i = 0; i < parent.Nodes.Count; i++)
                {
                    if (parent.Nodes[i].Text == text)
                    {
                        ind = i;
                        return true;
                    }                        
                }                
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
