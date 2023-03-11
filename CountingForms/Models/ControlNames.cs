using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingForms.Models
{
    public class ControlNames
    {
        public string controlName { get; set; }
        public string userFriendlyName { get; set; }

        public ControlNames(string controlName, string userFriendlyName)
        {
            this.controlName = controlName;
            this.userFriendlyName = userFriendlyName;
        }
    }
}
