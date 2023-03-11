using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingForms.Models
{
    public class Role
    {
        public long id { get; set; }
        public string name { get; set; }

        public Role(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
