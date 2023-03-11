namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_client
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        [Required]
        [StringLength(350)]
        public string name { get; set; }

        public long id_subfml { get; set; }

        [Required]
        [StringLength(20)]
        public string BIN { get; set; }

        public long id_city { get; set; }

        [Required]
        [StringLength(300)]
        public string address { get; set; }

        public short? report_group_1 { get; set; }

        public short? report_group_2 { get; set; }

        public short? report_group_3 { get; set; }

        public short? report_group_4 { get; set; }

        [StringLength(10)]
        public string postindex { get; set; }

        public int last_update_user { get; set; }

        public DateTime lastupdate { get; set; }

        public bool deleted { get; set; }

        [StringLength(50)]
        public string RKO_CODE { get; set; }

        [StringLength(50)]
        public string RKO_DEP_CODE { get; set; }

        [StringLength(100)]
        public string KO_CODE { get; set; }

        [StringLength(100)]
        public string KO_INF1 { get; set; }

        [StringLength(100)]
        public string KO_INF2 { get; set; }

        [StringLength(100)]
        public string KO_INF3 { get; set; }

        [StringLength(100)]
        public string KO_INF4 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? KO_INF5 { get; set; }

        public virtual t_g_city t_g_city { get; set; }

        public virtual t_g_clisubfml t_g_clisubfml { get; set; }
    }
}
