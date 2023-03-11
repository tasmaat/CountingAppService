namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_w_sessions
    {
        [Key]
        [StringLength(36)]
        public string id_session { get; set; }

        public int id_user { get; set; }

        public DateTime connected { get; set; }

        public DateTime last_active { get; set; }
    }
}
