namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_pechatclien
    {
        public long id { get; set; }

        public long? id_client { get; set; }

        [StringLength(50)]
        public string name_pechat { get; set; }

        [Column(TypeName = "image")]
        public byte[] img1 { get; set; }

        [Column(TypeName = "image")]
        public byte[] img2 { get; set; }

        public DateTime? creation { get; set; }

        public int? last_update_user { get; set; }

        public DateTime? lastupdate { get; set; }

        public long? id_encashpoint { get; set; }

        [Column(TypeName = "image")]
        public byte[] img3 { get; set; }

        [Column(TypeName = "image")]
        public byte[] img4 { get; set; }
    }
}
