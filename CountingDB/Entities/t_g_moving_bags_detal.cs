namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_moving_bags_detal
    {
        public long id { get; set; }

        public long id_moving_bags { get; set; }

        public long id_bags { get; set; }

        public long status { get; set; }

        public virtual t_g_bags t_g_bags { get; set; }

        public virtual t_g_moving_bags t_g_moving_bags { get; set; }
    }
}
