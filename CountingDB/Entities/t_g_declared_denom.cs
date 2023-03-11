namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_declared_denom
    {
        public long id { get; set; }

        public long id_bag { get; set; }

        public long id_denomination { get; set; }

        public int denomcount { get; set; }

        public long id_account { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_user_update { get; set; }

        public long id_currency { get; set; }

        public decimal declared_value { get; set; }

        public long id_counting { get; set; }

        public long? id_condition { get; set; }

        public virtual t_g_account t_g_account { get; set; }

        public virtual t_g_bags t_g_bags { get; set; }

        public virtual t_g_counting t_g_counting { get; set; }

        public virtual t_g_currency t_g_currency { get; set; }

        public virtual t_g_denomination t_g_denomination { get; set; }
    }
}
