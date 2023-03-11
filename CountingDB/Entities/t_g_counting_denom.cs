namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting_denom
    {
        public long id { get; set; }

        public long id_counting { get; set; }

        public long id_card { get; set; }

        public long id_denomination { get; set; }

        public long? id_condition { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_user_update { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        public int? count { get; set; }

        public int? reject_count { get; set; }

        public decimal? fact_value { get; set; }

        public int source { get; set; }

        [StringLength(30)]
        public string serial_number { get; set; }

        [StringLength(30)]
        public string serial_number2 { get; set; }

        [StringLength(30)]
        public string description { get; set; }

        [StringLength(50)]
        public string description1 { get; set; }

        public int? flschet1 { get; set; }

        public virtual t_g_cards t_g_cards { get; set; }

        public virtual t_g_counting t_g_counting { get; set; }

        public virtual t_g_denomination t_g_denomination { get; set; }
    }
}
