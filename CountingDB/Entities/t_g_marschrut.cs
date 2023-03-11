namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_marschrut
    {
        public long id { get; set; }

        [StringLength(150)]
        public string nummarsh { get; set; }

        [StringLength(250)]
        public string inkassator { get; set; }

        public long? id_kontrol { get; set; }

        public long? id_kassir { get; set; }

        public long? kol_porsum { get; set; }

        [StringLength(150)]
        public string num_porsum { get; set; }

        [StringLength(250)]
        public string komment { get; set; }

        [StringLength(50)]
        public string kol_oplsumok { get; set; }

        public DateTime? creation { get; set; }

        public int? last_update_user { get; set; }

        public DateTime? lastupdate { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }
    }
}
