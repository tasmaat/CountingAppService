namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting_content_trace
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_account { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_counting { get; set; }

        public long? id_bag { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal declared_value { get; set; }

        public decimal? fact_value { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_currency { get; set; }

        public DateTime? creation { get; set; }

        public DateTime? lastupdate { get; set; }

        public long? last_user_update { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }
    }
}
