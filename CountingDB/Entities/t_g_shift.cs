namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_shift
    {
        public long id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        public DateTime startDateTime { get; set; }

        public DateTime? endDateTime { get; set; }

        public int status { get; set; }

        public DateTime creation { get; set; }

        public long creation_user { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_update_user { get; set; }

        public long id_cashcenter { get; set; }

        public long? id_user_close_shift { get; set; }
    }
}
