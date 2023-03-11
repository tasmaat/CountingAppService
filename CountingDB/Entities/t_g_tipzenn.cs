namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_tipzenn
    {
        public long id { get; set; }

        [StringLength(150)]
        public string name_zenn { get; set; }

        public DateTime? creation { get; set; }

        public int? last_update_user { get; set; }

        public DateTime? lastupdate { get; set; }

        public bool? visible { get; set; }
    }
}
