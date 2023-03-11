namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_clienttocc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_clienttocc()
        {
            t_g_account = new HashSet<t_g_account>();
            t_g_encashpoint = new HashSet<t_g_encashpoint>();
        }

        public long id { get; set; }

        public long id_client { get; set; }

        public long id_cashcentre { get; set; }

        public DateTime lastupdate { get; set; }

        public int last_update_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_account> t_g_account { get; set; }

        public virtual t_g_cashcentre t_g_cashcentre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_encashpoint> t_g_encashpoint { get; set; }
    }
}
