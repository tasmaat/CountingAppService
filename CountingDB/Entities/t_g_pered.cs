namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_pered
    {
        public long id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public long? id_detal { get; set; }

        public int? count { get; set; }

        [Column(TypeName = "money")]
        public decimal? fact_value { get; set; }

        public long? user_otkogo { get; set; }

        public long? user_komu { get; set; }

        public int? status { get; set; }

        public DateTime? createdate { get; set; }

        public DateTime? izmenstatdate { get; set; }

        public int? tipper { get; set; }

        public int? vidsost { get; set; }

        [StringLength(150)]
        public string comment { get; set; }

        public bool? zone1 { get; set; }
    }
}
