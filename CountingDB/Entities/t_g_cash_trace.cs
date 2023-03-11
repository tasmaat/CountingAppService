namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cash_trace
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime creation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime lastupdate { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long last_user_update { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? seqnumber { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_user { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal count { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_denomination { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id_cashcentre { get; set; }

        public long? id_cond { get; set; }
    }
}
