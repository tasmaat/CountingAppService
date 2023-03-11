namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting_content
    {
        public long id { get; set; }

        public long id_account { get; set; }

        public long id_counting { get; set; }

        public long? id_bag { get; set; }

        public decimal declared_value { get; set; }

        public decimal? fact_value { get; set; }

        public long id_currency { get; set; }

        public DateTime? creation { get; set; }

        public DateTime? lastupdate { get; set; }

        public long? last_user_update { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int st { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }

        public virtual t_g_account t_g_account { get; set; }

        public virtual t_g_bags t_g_bags { get; set; }

        public virtual t_g_counting t_g_counting { get; set; }
    }
}
