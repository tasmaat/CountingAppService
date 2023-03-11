namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_denomination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_denomination()
        {
            t_g_counting_denom = new HashSet<t_g_counting_denom>();
            t_g_declared_denom = new HashSet<t_g_declared_denom>();
        }

        public long id { get; set; }

        public DateTime creation { get; set; }

        public long id_currency { get; set; }

        public DateTime lastupdate { get; set; }

        [Required]
        [StringLength(60)]
        public string name { get; set; }

        [Column(TypeName = "money")]
        public decimal value { get; set; }

        public DateTime? valid_date { get; set; }

        public int last_update_user { get; set; }

        public long? id_tipzen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting_denom> t_g_counting_denom { get; set; }

        public virtual t_g_currency t_g_currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_declared_denom> t_g_declared_denom { get; set; }
    }
}
