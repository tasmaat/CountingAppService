namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cause
    {
        public long id { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }
    }
}
