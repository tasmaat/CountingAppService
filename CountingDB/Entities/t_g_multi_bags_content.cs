namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_multi_bags_content
    {
        public long id { get; set; }

        public long id_multi_bag { get; set; }

        public long id_account { get; set; }

        public long id_currency { get; set; }

        public decimal? declared_value1 { get; set; }

        public decimal? declared_value2 { get; set; }

        public decimal fact_value { get; set; }

        public DateTime creation { get; set; }

        public long creation_user { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_update_user { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int status_d { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int status_f { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int status_f2 { get; set; }
    }
}
