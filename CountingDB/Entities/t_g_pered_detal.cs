namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_pered_detal
    {
        public long id { get; set; }

        public long? id_pered { get; set; }

        public long? id_detal { get; set; }

        public int? count { get; set; }

        [Column(TypeName = "money")]
        public decimal? fact_value { get; set; }

        public long? id_cond { get; set; }
    }
}
