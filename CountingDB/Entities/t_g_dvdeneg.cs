namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_dvdeneg
    {
        public long id { get; set; }

        public long? id_counting { get; set; }

        public long? id_denomination { get; set; }

        public long? id_condition { get; set; }

        public DateTime? creation { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        public int? count { get; set; }

        public decimal? fact_value { get; set; }

        public long? user1 { get; set; }

        public long? id_card { get; set; }

        public DateTime? lastupdate { get; set; }

        public int? source { get; set; }
    }
}
