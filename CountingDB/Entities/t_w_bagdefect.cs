namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_w_bagdefect
    {
        public long id { get; set; }

        [Required]
        [StringLength(250)]
        public string num_defect { get; set; }

        public long id_bag { get; set; }

        public long id_bagdefectfactor { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }

        public virtual t_g_bagdefect_factor t_g_bagdefect_factor { get; set; }

        public virtual t_g_bags t_g_bags { get; set; }
    }
}
