namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_dvdeneg_jrn
    {
        public long id { get; set; }

        public long? id_counting { get; set; }

        public long? id_denomination { get; set; }

        public long? id_condition { get; set; }

        public DateTime? creation { get; set; }

        public long? user_otkogo { get; set; }

        public long? user_komy { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        public int? count { get; set; }

        public long? id_card { get; set; }
    }
}
