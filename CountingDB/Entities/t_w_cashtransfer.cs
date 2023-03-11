namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_w_cashtransfer
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        public long? id_user_send { get; set; }

        public long? id_zone_send { get; set; }

        public long? id_pk_send { get; set; }

        public long id_user_receive { get; set; }

        public long? id_zone_receive { get; set; }

        public long? id_pk_reseive { get; set; }

        public int? id_status { get; set; }

        public bool? adjustment_sendorreseive { get; set; }

        public DateTime lastupdate { get; set; }

        [StringLength(20)]
        public string number_container { get; set; }

        public long? id_subtable { get; set; }

        public long? lastuserupdate { get; set; }

        public long? id_user_change { get; set; }

        public long? id_zone_change { get; set; }

        public long? id_pk_change { get; set; }

        public DateTime? changedate { get; set; }

        public long? changeuserdate { get; set; }

        public long? createuser { get; set; }

        public DateTime? receivedate { get; set; }

        public long? receiveuserdate { get; set; }

        public int? source { get; set; }

        public long? id_counting { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }
    }
}
