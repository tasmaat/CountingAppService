namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_parent_table
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }
    }
}
