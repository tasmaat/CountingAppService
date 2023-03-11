namespace CountingDB.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_g_counting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_g_counting()
        {
            t_g_counting_content = new HashSet<t_g_counting_content>();
            t_g_declared_denom = new HashSet<t_g_declared_denom>();
            t_g_counting_denom = new HashSet<t_g_counting_denom>();
        }

        public long id { get; set; }

        public long id_client { get; set; }

        public long? id_bag { get; set; }

        public DateTime creation { get; set; }

        public long last_user_update { get; set; }

        public DateTime lastupdate { get; set; }

        [StringLength(50)]
        public string workstation { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public DateTime date_deposit { get; set; }

        public DateTime? date_collect { get; set; }

        public DateTime? date_reception { get; set; }

        public DateTime? date_value { get; set; }

        public DateTime? datetime1 { get; set; }

        public DateTime? datetime2 { get; set; }

        public DateTime? datetime3 { get; set; }

        public DateTime? datetime4 { get; set; }

        [StringLength(50)]
        public string deposit1 { get; set; }

        [StringLength(50)]
        public string deposit2 { get; set; }

        [StringLength(50)]
        public string deposit3 { get; set; }

        [StringLength(50)]
        public string deposit4 { get; set; }

        [StringLength(50)]
        public string deposit5 { get; set; }

        [StringLength(50)]
        public string deposit6 { get; set; }

        [StringLength(50)]
        public string deposit7 { get; set; }

        public bool? deleted { get; set; }

        public byte? status { get; set; }

        public byte? source { get; set; }

        public DateTime? datenachizm { get; set; }

        [StringLength(50)]
        public string qr_bin { get; set; }

        public DateTime? qr_data { get; set; }

        [StringLength(50)]
        public string qr_nummech { get; set; }

        [StringLength(50)]
        public string qr_numplom { get; set; }

        [StringLength(250)]
        public string qr_otprav { get; set; }

        [StringLength(50)]
        public string qr_kbe { get; set; }

        [StringLength(250)]
        public string qr_poluch { get; set; }

        [StringLength(150)]
        public string qr_kontr { get; set; }

        [StringLength(150)]
        public string qr_kass { get; set; }

        [StringLength(150)]
        public string qr_vidoper { get; set; }

        [StringLength(50)]
        public string qr_knp { get; set; }

        [StringLength(50)]
        public string qr_numgr { get; set; }

        [StringLength(50)]
        public string qr_poslsh { get; set; }

        [StringLength(50)]
        public string qr_poslsum { get; set; }

        [StringLength(50)]
        public string qr_poslval { get; set; }

        public int? fl_prov { get; set; }

        public long id_multi_bag { get; set; }

        public long? validate_user { get; set; }

        public DateTime? validate_date { get; set; }

        public long? id_user_create { get; set; }

        public long? id_zona_create { get; set; }

        public long? id_shift_create { get; set; }

        public long? id_shift_current { get; set; }

        public long? discr_det_user { get; set; }

        public DateTime? discr_det_date { get; set; }

        public virtual t_g_bags t_g_bags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting_content> t_g_counting_content { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_declared_denom> t_g_declared_denom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_g_counting_denom> t_g_counting_denom { get; set; }
    }
}
