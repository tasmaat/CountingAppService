namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_name_column
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        public int last_update_user { get; set; }

        public DateTime lastupdate { get; set; }

        [Required]
        [StringLength(350)]
        public string name { get; set; }

        [StringLength(350)]
        public string value { get; set; }

        public bool deleted { get; set; }
    }
}
