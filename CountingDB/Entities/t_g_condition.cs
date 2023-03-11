namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_condition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_condition()
        {
            t_g_condition_factor = new HashSet<t_g_condition_factor>();
        }

        public long id { get; set; }

        public DateTime creation { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int last_update_user { get; set; }

        public DateTime lastupdate { get; set; }

        public bool? visible { get; set; }

        [StringLength(50)]
        public string code1 { get; set; }

        public int? priority { get; set; }

        public int? id_stat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_condition_factor> t_g_condition_factor { get; set; }
    }
}
