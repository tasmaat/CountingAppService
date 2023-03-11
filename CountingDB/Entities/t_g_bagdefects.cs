namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_bagdefects
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_bagdefects()
        {
            t_g_bagdefect_factor = new HashSet<t_g_bagdefect_factor>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(250)]
        public string Category { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_bagdefect_factor> t_g_bagdefect_factor { get; set; }
    }
}
