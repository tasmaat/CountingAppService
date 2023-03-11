namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_user()
        {
            t_g_bags_opening = new HashSet<t_g_bags_opening>();
        }

        public long id { get; set; }

        public DateTime creation { get; set; }

        public long id_brach { get; set; }

        public int? id_role { get; set; }

        [Required]
        [StringLength(20)]
        public string code { get; set; }

        [Required]
        [StringLength(300)]
        public string user_name { get; set; }

        [Required]
        [StringLength(100)]
        public string reference { get; set; }

        public DateTime lastupdate { get; set; }

        [Required]
        [StringLength(256)]
        public string password { get; set; }

        public DateTime expire_date { get; set; }

        public int? id_language { get; set; }

        public int last_update_user { get; set; }

        public int? tip_pol { get; set; }

        public bool? deleted { get; set; }

        public short? rasrprper1 { get; set; }

        [StringLength(50)]
        public string person_number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_bags_opening> t_g_bags_opening { get; set; }
    }
}
