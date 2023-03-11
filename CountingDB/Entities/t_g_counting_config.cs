namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting_config
    {
        public long id { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        [Required]
        [StringLength(250)]
        public string name { get; set; }

        public short type_deposit { get; set; }

        public bool date_deposit { get; set; }

        public bool date_collect { get; set; }

        public bool date_payment { get; set; }

        public bool date_packing { get; set; }

        public bool bag { get; set; }

        public bool wallet { get; set; }

        public bool currency { get; set; }

        public bool deposit1 { get; set; }

        public bool deposit2 { get; set; }

        public bool deposit3 { get; set; }

        public bool deposit4 { get; set; }

        public bool deposit5 { get; set; }

        public bool deposit6 { get; set; }

        public bool deposit7 { get; set; }

        public bool declared_note { get; set; }

        public bool declared_coin { get; set; }

        public bool declared_other { get; set; }

        public bool open_card { get; set; }

        public bool date_deposit_null { get; set; }

        public bool date_collect_null { get; set; }

        public bool date_payment_null { get; set; }

        public bool date_packing_null { get; set; }

        public bool bag_null { get; set; }

        public bool wallet_null { get; set; }

        public bool currency_null { get; set; }

        public bool deposit1_null { get; set; }

        public bool deposit2_null { get; set; }

        public bool deposit3_null { get; set; }

        public bool deposit4_null { get; set; }

        public bool deposit5_null { get; set; }

        public bool deposit6_null { get; set; }

        public bool deposit7_null { get; set; }

        public bool declared_note_null { get; set; }

        public bool declared_coin_null { get; set; }

        public bool declared_other_null { get; set; }

        public bool open_card_null { get; set; }
    }
}
