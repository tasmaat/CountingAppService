namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_bagdefect_factor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_bagdefect_factor()
        {
            t_w_bagdefect = new HashSet<t_w_bagdefect>();
        }

        public long id { get; set; }

        public long id_bagdefect { get; set; }

        [Required]
        [StringLength(250)]
        public string Category { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }

        public virtual t_g_bagdefects t_g_bagdefects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_w_bagdefect> t_w_bagdefect { get; set; }
    }
}
