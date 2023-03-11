namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_account()
        {
            t_g_declared_denom = new HashSet<t_g_declared_denom>();
            t_g_counting_content = new HashSet<t_g_counting_content>();
        }

        public long id { get; set; }

        public long id_clienttocc { get; set; }

        public long id_encashpoint { get; set; }

        public long id_client { get; set; }

        public DateTime creation { get; set; }

        [Required]
        [StringLength(250)]
        public string name { get; set; }

        public long id_currency { get; set; }

        public int last_update_user { get; set; }

        public DateTime lastupdate { get; set; }

        public virtual t_g_clienttocc t_g_clienttocc { get; set; }

        public virtual t_g_currency t_g_currency { get; set; }

        public virtual t_g_encashpoint t_g_encashpoint { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_declared_denom> t_g_declared_denom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting_content> t_g_counting_content { get; set; }
    }
}
