namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_pols_ip
    {
        public long id { get; set; }

        public long? id_pols { get; set; }

        [StringLength(50)]
        public string ippols { get; set; }

        public DateTime? datevh1 { get; set; }
    }
}
