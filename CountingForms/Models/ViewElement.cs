using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingForms.Models
{
    public class ViewElement
    {
        public long? id { get; set; } = null;
        public string controlName { get; set; }
        public bool? visible { get; set; } = null;
        public bool? enabled { get; set; } = null;
        public bool? sanction { get; set; } = null;
        public bool? selfSanction { get; set; } = null;
        public bool? isSanctioner { get; set; } = null;
        public bool updated { get; set; } = false;

        public ViewElement(string controlName, bool visible, bool enabled)
        {            
            this.controlName = controlName;
            this.visible = visible;
            this.enabled = enabled;
        }

        public ViewElement(string controlName, bool visible, bool enabled, bool sanction, bool selfSanction, bool isSanctioner)
        {
            this.controlName = controlName;
            this.visible = visible;
            this.enabled = enabled;
            this.sanction = sanction;
            this.selfSanction = selfSanction;
            this.isSanctioner = isSanctioner;
        }
        public ViewElement(long id, string controlName, bool visible, bool enabled)
        {
            this.id = id;
            this.controlName = controlName;
            this.visible = visible;
            this.enabled = enabled;
        }

        public ViewElement(long id, string controlName, bool visible, bool enabled, bool sanction, bool selfSanction, bool isSanctioner)
        {
            this.id = id;
            this.controlName = controlName;
            this.visible = visible;
            this.enabled = enabled;
            this.sanction = sanction;
            this.selfSanction = selfSanction;
            this.isSanctioner = isSanctioner;
        }
    }
}
