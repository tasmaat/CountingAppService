namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_garter
    {
        public long id { get; set; }

        public long id_zona { get; set; }

        [StringLength(10)]
        public string id_user { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_user_update { get; set; }

        [StringLength(10)]
        public string workstation { get; set; }
    }
}
