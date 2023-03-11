namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_role
    {
        public long id { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        public bool is_active { get; set; } = true;

        public bool updated { get; set; } = false;
        public bool isNew { get; set; } = false;
    }
}
