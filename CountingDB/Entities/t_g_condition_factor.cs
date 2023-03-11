namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_condition_factor
    {
        public long id { get; set; }

        public long id_condition { get; set; }

        [StringLength(250)]
        public string short_evidence { get; set; }

        [StringLength(1000)]
        public string evidence { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }

        public virtual t_g_condition t_g_condition { get; set; }
    }
}
