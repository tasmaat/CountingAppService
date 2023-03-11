namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_w_transaction
    {
        public long id { get; set; }

        public long id_user { get; set; }

        [Required]
        [StringLength(50)]
        public string table_name { get; set; }

        public short type_operation { get; set; }

        public DateTime lastupdate { get; set; }

        public long id_key { get; set; }
    }
}
