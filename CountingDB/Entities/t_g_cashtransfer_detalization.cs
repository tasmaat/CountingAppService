namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cashtransfer_detalization
    {
        public long id { get; set; }

        public long? id_cashtransfer { get; set; }

        public long? id_denomination { get; set; }

        public long? id_condition { get; set; }

        public DateTime? creation { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        public long? count { get; set; }

        public decimal? fact_value { get; set; }

        public DateTime? lastupdate { get; set; }

        public long? lastuserupdate { get; set; }

        public long? id_card { get; set; }
    }
}
