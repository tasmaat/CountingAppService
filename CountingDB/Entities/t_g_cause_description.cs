namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cause_description
    {
        public long id { get; set; }

        public long? id_counting { get; set; }

        public long? id_multi_bag { get; set; }

        public long id_cause { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        public DateTime creation { get; set; }

        public long createuser { get; set; }
    }
}
