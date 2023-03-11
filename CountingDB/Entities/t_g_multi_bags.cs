namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_multi_bags
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        public long creation_user { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_update_user { get; set; }

        public long? id_client { get; set; }

        public long? id_encashpoint { get; set; }

        public long? id_marschrut { get; set; }

        public int count_bags1 { get; set; }

        public int count_bags2 { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        public int deleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int status { get; set; }

        [StringLength(10)]
        public string seal { get; set; }

        public int? fl_prov { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int status_count { get; set; }

        public int? fl_prov2 { get; set; }
    }
}
