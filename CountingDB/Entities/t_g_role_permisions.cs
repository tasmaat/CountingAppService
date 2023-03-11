namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_role_permisions
    {
        public long id { get; set; }
        public long roleId { get; set; }
        [Required]
        [StringLength(200)]
        public string formName { get; set; }
        [Required]
        [StringLength(200)]
        public string controlName { get; set; }        
        [StringLength(200)]
        public string description { get; set; }
        public bool visible { get; set; }
        public bool enabled { get; set; }
        public bool sanction { get; set; }
        public bool self_sanction { get; set; }
        public bool is_sanctioner { get; set; }
        public bool updated { get; set; } = false;
        public bool isNew { get; set; } = false;
    }
}
