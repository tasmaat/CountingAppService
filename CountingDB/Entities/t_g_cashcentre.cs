namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_cashcentre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_cashcentre()
        {
            t_g_cashcentre1 = new HashSet<t_g_cashcentre>();
            t_g_clienttocc = new HashSet<t_g_clienttocc>();
        }

        public long id { get; set; }

        public DateTime creation { get; set; }

        [Required]
        [StringLength(300)]
        public string branch_name { get; set; }

        public long? id_parent { get; set; }

        public DateTime lastupdate { get; set; }

        public long id_city { get; set; }

        public byte time_zone { get; set; }

        public int last_update_user { get; set; }

        public short? tipsp1 { get; set; }

        [StringLength(50)]
        public string name1 { get; set; }

        [StringLength(50)]
        public string name2 { get; set; }

        public long? id_parent1 { get; set; }

        [StringLength(50)]
        public string Rez1 { get; set; }

        [StringLength(50)]
        public string Camera { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_cashcentre> t_g_cashcentre1 { get; set; }

        public virtual t_g_cashcentre t_g_cashcentre2 { get; set; }

        public virtual t_g_city t_g_city { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_clienttocc> t_g_clienttocc { get; set; }
    }
}
