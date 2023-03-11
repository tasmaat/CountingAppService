using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingForms.Models
{
    public class Sanction
    {
        public bool sanction { get; set; }
        public bool selfSanction { get; set; }
        public bool isSanctioner { get; set; }

        public Sanction(bool sanction, bool selfSanction, bool isSanctioner)
        {
            this.sanction = sanction;
            this.selfSanction = selfSanction;
            this.isSanctioner = isSanctioner;
        }
    }
}
