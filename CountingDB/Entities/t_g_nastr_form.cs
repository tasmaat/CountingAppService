namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_nastr_form
    {
        public long id { get; set; }

        public int? num_form { get; set; }

        public int? num_compon { get; set; }

        [StringLength(250)]
        public string name_compon { get; set; }

        public float? value_compon { get; set; }

        [StringLength(250)]
        public string dopol_pref { get; set; }

        [StringLength(50)]
        public string name_pole { get; set; }

        [StringLength(50)]
        public string name_grup { get; set; }
    }
}
