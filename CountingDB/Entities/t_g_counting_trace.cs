namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting_trace
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_client { get; set; }

        public long? id_bag { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime creation { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long last_user_update { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime lastupdate { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string name { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime date_deposit { get; set; }

        public DateTime? date_collect { get; set; }

        public DateTime? date_reception { get; set; }

        public DateTime? date_value { get; set; }

        public DateTime? datetime1 { get; set; }

        public DateTime? datetime2 { get; set; }

        public DateTime? datetime3 { get; set; }

        public DateTime? datetime4 { get; set; }

        [StringLength(50)]
        public string deposit1 { get; set; }

        [StringLength(50)]
        public string deposit2 { get; set; }

        [StringLength(50)]
        public string deposit3 { get; set; }

        [StringLength(50)]
        public string deposit4 { get; set; }

        [StringLength(50)]
        public string deposit5 { get; set; }

        [StringLength(50)]
        public string deposit6 { get; set; }

        [StringLength(50)]
        public string deposit7 { get; set; }

        public bool? deleted { get; set; }

        public byte? status { get; set; }

        public byte? source { get; set; }
    }
}
