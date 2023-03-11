namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_shift_user
    {
        public long id { get; set; }

        public long id_user { get; set; }

        public long id_zona { get; set; }

        public long id_shift { get; set; }

        public long id_cashcenter { get; set; }

        public DateTime creation { get; set; }

        public long creation_user { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_update_user { get; set; }
    }
}
