using CountingDB.Entities;
using CountingForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.Interfaces
{
    public interface IPermisionsManager
    {
        void ShowControls(Control.ControlCollection controls, ToolTip tt, ref List<ControlNames> ctrls);
        void ShowMenuItems(Control.ControlCollection controls, ref List<ControlNames> mi);
        void ClearToolTips(Form form, ToolTip tt);
        void ChangeControlByRole(Control.ControlCollection controls, List<t_g_role_permisions> perm);
        bool EnabledPossibility(List<t_g_role_permisions> rp, Control cn);
        bool VisiblePossibility(List<t_g_role_permisions> rp, Control cn);
    }
}
