namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_moving_bags
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_moving_bags()
        {
            t_g_moving_bags_detal = new HashSet<t_g_moving_bags_detal>();
        }

        public long id { get; set; }

        public long user_send { get; set; }

        public DateTime createdate { get; set; }

        public long? user_received { get; set; }

        public long? zona_received { get; set; }

        public DateTime? last_update { get; set; }

        public long? lats_update_user { get; set; }

        public long? status { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_moving_bags_detal> t_g_moving_bags_detal { get; set; }
    }
}
