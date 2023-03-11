namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cards
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_cards()
        {
            t_g_counting_denom = new HashSet<t_g_counting_denom>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public long? id_bag { get; set; }

        public int? type { get; set; }

        public DateTime? creation { get; set; }

        public DateTime? lastupdate { get; set; }

        public long? last_user_update { get; set; }

        public long? id_counting { get; set; }

        public bool? fl_obr { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }

        [StringLength(20)]
        public string IDkassir { get; set; }

        [StringLength(50)]
        public string FIOKASS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting_denom> t_g_counting_denom { get; set; }
    }
}
