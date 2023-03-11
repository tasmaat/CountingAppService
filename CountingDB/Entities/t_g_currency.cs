namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_currency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_currency()
        {
            t_g_account = new HashSet<t_g_account>();
            t_g_declared_denom = new HashSet<t_g_declared_denom>();
            t_g_denomination = new HashSet<t_g_denomination>();
        }

        public long id { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [Required]
        [StringLength(4)]
        public string curr_code { get; set; }

        public double? rate { get; set; }

        public double? sellrate { get; set; }

        public bool locked { get; set; }

        public int last_update_user { get; set; }

        public int? population { get; set; }

        public int? sort { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_account> t_g_account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_declared_denom> t_g_declared_denom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_denomination> t_g_denomination { get; set; }
    }
}
