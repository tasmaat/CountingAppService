namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_bags_opening
    {
        public long id { get; set; }

        public long id_bags { get; set; }

        public long create_user { get; set; }

        public DateTime create_date { get; set; }

        public long owner_user { get; set; }

        public long opening_user { get; set; }

        public DateTime last_update { get; set; }

        public virtual t_g_bags t_g_bags { get; set; }

        public virtual t_g_user t_g_user { get; set; }
    }
}
