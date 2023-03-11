namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting_denom_trace
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_counting { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_card { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_denomination { get; set; }

        public long? id_condition { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime creation { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime lastupdate { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long last_user_update { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        public int? count { get; set; }

        public int? reject_count { get; set; }

        public decimal? fact_value { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int source { get; set; }

        [StringLength(30)]
        public string serial_number { get; set; }

        [StringLength(30)]
        public string serial_number2 { get; set; }

        [StringLength(30)]
        public string description { get; set; }

        public int? flschet1 { get; set; }
    }
}
