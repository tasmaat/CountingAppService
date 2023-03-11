namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_bags
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_bags()
        {
            t_g_bags_opening = new HashSet<t_g_bags_opening>();
            t_g_moving_bags_detal = new HashSet<t_g_moving_bags_detal>();
            t_g_declared_denom = new HashSet<t_g_declared_denom>();
            t_g_counting_content = new HashSet<t_g_counting_content>();
            t_g_counting = new HashSet<t_g_counting>();
            t_w_bagdefect = new HashSet<t_w_bagdefect>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(10)]
        public string name { get; set; }

        [StringLength(10)]
        public string seal { get; set; }

        public DateTime creation { get; set; }

        public DateTime lastupdate { get; set; }

        public long last_user_update { get; set; }

        public DateTime? date_preparation { get; set; }

        public DateTime? date_reception { get; set; }

        public long? id_marshr { get; set; }

        public long? id_akt { get; set; }

        public int? status { get; set; }

        public long create_user { get; set; }

        public long? opening_user { get; set; }

        public DateTime? opening_date { get; set; }

        public long owner_user { get; set; }

        [StringLength(50)]
        public string cli_cashier { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_bags_opening> t_g_bags_opening { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_moving_bags_detal> t_g_moving_bags_detal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_declared_denom> t_g_declared_denom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting_content> t_g_counting_content { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting> t_g_counting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_w_bagdefect> t_w_bagdefect { get; set; }
    }
}
