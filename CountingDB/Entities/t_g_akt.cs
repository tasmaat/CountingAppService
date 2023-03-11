namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_akt
    {
        public long id { get; set; }

        public int? dan1 { get; set; }

        public int? dan2 { get; set; }

        public int? dan3 { get; set; }

        public DateTime? creation { get; set; }

        public int? last_update_user { get; set; }

        public DateTime? lastupdate { get; set; }

        [StringLength(50)]
        public string numakt { get; set; }

        public int? vidakt { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }
    }
}
