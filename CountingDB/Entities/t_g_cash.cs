namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cash
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        public long last_user_update { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? seqnumber { get; set; }

        public long id_user { get; set; }

        [Column(TypeName = "numeric")]
        public decimal count { get; set; }

        public long id_denomination { get; set; }

        public long id_cashcentre { get; set; }

        public long? id_cond { get; set; }
    }
}
