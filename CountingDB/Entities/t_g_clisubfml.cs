namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_clisubfml
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_clisubfml()
        {
            t_g_client = new HashSet<t_g_client>();
        }

        public long id { get; set; }

        public DateTime creation { get; set; }

        [Required]
        [StringLength(250)]
        public string name { get; set; }

        public int last_update_user { get; set; }

        public DateTime lastupdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_client> t_g_client { get; set; }
    }
}
