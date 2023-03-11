namespace CountingDB.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<t_g_account> t_g_account { get; set; }
        public virtual DbSet<t_g_akt> t_g_akt { get; set; }
        public virtual DbSet<t_g_bagdefect_factor> t_g_bagdefect_factor { get; set; }
        public virtual DbSet<t_g_bagdefects> t_g_bagdefects { get; set; }
        public virtual DbSet<t_g_bags> t_g_bags { get; set; }
        public virtual DbSet<t_g_bags_opening> t_g_bags_opening { get; set; }
        public virtual DbSet<t_g_cards> t_g_cards { get; set; }
        public virtual DbSet<t_g_cash> t_g_cash { get; set; }
        public virtual DbSet<t_g_cashcentre> t_g_cashcentre { get; set; }
        public virtual DbSet<t_g_cashtransfer_detalization> t_g_cashtransfer_detalization { get; set; }
        public virtual DbSet<t_g_cause> t_g_cause { get; set; }
        public virtual DbSet<t_g_cause_description> t_g_cause_description { get; set; }
        public virtual DbSet<t_g_city> t_g_city { get; set; }
        public virtual DbSet<t_g_client> t_g_client { get; set; }
        public virtual DbSet<t_g_clienttocc> t_g_clienttocc { get; set; }
        public virtual DbSet<t_g_clisubfml> t_g_clisubfml { get; set; }
        public virtual DbSet<t_g_condition> t_g_condition { get; set; }
        public virtual DbSet<t_g_condition_factor> t_g_condition_factor { get; set; }
        public virtual DbSet<t_g_counting> t_g_counting { get; set; }
        public virtual DbSet<t_g_counting_config> t_g_counting_config { get; set; }
        public virtual DbSet<t_g_counting_content> t_g_counting_content { get; set; }
        public virtual DbSet<t_g_counting_denom> t_g_counting_denom { get; set; }
        public virtual DbSet<t_g_currency> t_g_currency { get; set; }
        public virtual DbSet<t_g_declared_denom> t_g_declared_denom { get; set; }
        public virtual DbSet<t_g_denomination> t_g_denomination { get; set; }
        public virtual DbSet<t_g_dvdeneg> t_g_dvdeneg { get; set; }
        public virtual DbSet<t_g_dvdeneg_jrn> t_g_dvdeneg_jrn { get; set; }
        public virtual DbSet<t_g_encashpoint> t_g_encashpoint { get; set; }
        public virtual DbSet<t_g_garter> t_g_garter { get; set; }
        public virtual DbSet<t_g_marschrut> t_g_marschrut { get; set; }
        public virtual DbSet<t_g_moving_bags> t_g_moving_bags { get; set; }
        public virtual DbSet<t_g_moving_bags_detal> t_g_moving_bags_detal { get; set; }
        public virtual DbSet<t_g_multi_bags> t_g_multi_bags { get; set; }
        public virtual DbSet<t_g_multi_bags_content> t_g_multi_bags_content { get; set; }
        public virtual DbSet<t_g_name_column> t_g_name_column { get; set; }
        public virtual DbSet<t_g_pechatclien> t_g_pechatclien { get; set; }
        public virtual DbSet<t_g_pered> t_g_pered { get; set; }
        public virtual DbSet<t_g_pered_detal> t_g_pered_detal { get; set; }
        public virtual DbSet<t_g_pols_ip> t_g_pols_ip { get; set; }
        public virtual DbSet<t_g_role> t_g_role { get; set; }
        public virtual DbSet<t_g_role_permisions> t_g_role_permisions { get; set; }
        public virtual DbSet<t_g_rules> t_g_rules { get; set; }
        public virtual DbSet<t_g_shift> t_g_shift { get; set; }
        public virtual DbSet<t_g_shift_user> t_g_shift_user { get; set; }
        public virtual DbSet<t_g_shift_zona> t_g_shift_zona { get; set; }
        public virtual DbSet<t_g_status> t_g_status { get; set; }
        public virtual DbSet<t_g_tipzenn> t_g_tipzenn { get; set; }
        public virtual DbSet<t_g_user> t_g_user { get; set; }
        public virtual DbSet<t_parent_table> t_parent_table { get; set; }
        public virtual DbSet<t_w_bagdefect> t_w_bagdefect { get; set; }
        public virtual DbSet<t_w_cashtransfer> t_w_cashtransfer { get; set; }
        public virtual DbSet<t_w_sessions> t_w_sessions { get; set; }
        public virtual DbSet<t_w_transaction> t_w_transaction { get; set; }
        public virtual DbSet<t_g_cash_trace> t_g_cash_trace { get; set; }
        public virtual DbSet<t_g_cashtransfer_detalization_trace> t_g_cashtransfer_detalization_trace { get; set; }
        public virtual DbSet<t_g_counting_content_trace> t_g_counting_content_trace { get; set; }
        public virtual DbSet<t_g_counting_denom_trace> t_g_counting_denom_trace { get; set; }
        public virtual DbSet<t_g_counting_trace> t_g_counting_trace { get; set; }
        public virtual DbSet<t_g_nastr_form> t_g_nastr_form { get; set; }
        public virtual DbSet<t_g_sprav> t_g_sprav { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_g_account>()
                .HasMany(e => e.t_g_declared_denom)
                .WithRequired(e => e.t_g_account)
                .HasForeignKey(e => e.id_account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_account>()
                .HasMany(e => e.t_g_counting_content)
                .WithRequired(e => e.t_g_account)
                .HasForeignKey(e => e.id_account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_bagdefect_factor>()
                .HasMany(e => e.t_w_bagdefect)
                .WithRequired(e => e.t_g_bagdefect_factor)
                .HasForeignKey(e => e.id_bagdefectfactor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_bagdefects>()
                .HasMany(e => e.t_g_bagdefect_factor)
                .WithRequired(e => e.t_g_bagdefects)
                .HasForeignKey(e => e.id_bagdefect)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_bags>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<t_g_bags>()
                .Property(e => e.seal)
                .IsFixedLength();

            modelBuilder.Entity<t_g_bags>()
                .HasMany(e => e.t_g_bags_opening)
                .WithRequired(e => e.t_g_bags)
                .HasForeignKey(e => e.id_bags);

            modelBuilder.Entity<t_g_bags>()
                .HasMany(e => e.t_g_moving_bags_detal)
                .WithRequired(e => e.t_g_bags)
                .HasForeignKey(e => e.id_bags);

            modelBuilder.Entity<t_g_bags>()
                .HasMany(e => e.t_g_declared_denom)
                .WithRequired(e => e.t_g_bags)
                .HasForeignKey(e => e.id_bag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_bags>()
                .HasMany(e => e.t_g_counting_content)
                .WithOptional(e => e.t_g_bags)
                .HasForeignKey(e => e.id_bag);

            modelBuilder.Entity<t_g_bags>()
                .HasMany(e => e.t_g_counting)
                .WithOptional(e => e.t_g_bags)
                .HasForeignKey(e => e.id_bag);

            modelBuilder.Entity<t_g_bags>()
                .HasMany(e => e.t_w_bagdefect)
                .WithRequired(e => e.t_g_bags)
                .HasForeignKey(e => e.id_bag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_cards>()
                .HasMany(e => e.t_g_counting_denom)
                .WithRequired(e => e.t_g_cards)
                .HasForeignKey(e => e.id_card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_cash>()
                .Property(e => e.seqnumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<t_g_cash>()
                .Property(e => e.count)
                .HasPrecision(18, 0);

            modelBuilder.Entity<t_g_cashcentre>()
                .HasMany(e => e.t_g_cashcentre1)
                .WithOptional(e => e.t_g_cashcentre2)
                .HasForeignKey(e => e.id_parent);

            modelBuilder.Entity<t_g_cashcentre>()
                .HasMany(e => e.t_g_clienttocc)
                .WithRequired(e => e.t_g_cashcentre)
                .HasForeignKey(e => e.id_cashcentre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_cashtransfer_detalization>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_city>()
                .HasMany(e => e.t_g_cashcentre)
                .WithRequired(e => e.t_g_city)
                .HasForeignKey(e => e.id_city)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_city>()
                .HasMany(e => e.t_g_client)
                .WithRequired(e => e.t_g_city)
                .HasForeignKey(e => e.id_city)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_client>()
                .Property(e => e.BIN)
                .IsFixedLength();

            modelBuilder.Entity<t_g_client>()
                .Property(e => e.postindex)
                .IsFixedLength();

            modelBuilder.Entity<t_g_clienttocc>()
                .HasMany(e => e.t_g_account)
                .WithRequired(e => e.t_g_clienttocc)
                .HasForeignKey(e => e.id_clienttocc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_clienttocc>()
                .HasMany(e => e.t_g_encashpoint)
                .WithRequired(e => e.t_g_clienttocc)
                .HasForeignKey(e => e.id_clienttocc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_clisubfml>()
                .HasMany(e => e.t_g_client)
                .WithRequired(e => e.t_g_clisubfml)
                .HasForeignKey(e => e.id_subfml)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_condition>()
                .HasMany(e => e.t_g_condition_factor)
                .WithRequired(e => e.t_g_condition)
                .HasForeignKey(e => e.id_condition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_counting>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<t_g_counting>()
                .HasMany(e => e.t_g_counting_content)
                .WithRequired(e => e.t_g_counting)
                .HasForeignKey(e => e.id_counting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_counting>()
                .HasMany(e => e.t_g_declared_denom)
                .WithRequired(e => e.t_g_counting)
                .HasForeignKey(e => e.id_counting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_counting>()
                .HasMany(e => e.t_g_counting_denom)
                .WithRequired(e => e.t_g_counting)
                .HasForeignKey(e => e.id_counting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_counting_content>()
                .Property(e => e.declared_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_content>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_denom>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_denom>()
                .Property(e => e.serial_number)
                .IsFixedLength();

            modelBuilder.Entity<t_g_counting_denom>()
                .Property(e => e.serial_number2)
                .IsFixedLength();

            modelBuilder.Entity<t_g_counting_denom>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<t_g_currency>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<t_g_currency>()
                .Property(e => e.curr_code)
                .IsFixedLength();

            modelBuilder.Entity<t_g_currency>()
                .HasMany(e => e.t_g_account)
                .WithRequired(e => e.t_g_currency)
                .HasForeignKey(e => e.id_currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_currency>()
                .HasMany(e => e.t_g_declared_denom)
                .WithRequired(e => e.t_g_currency)
                .HasForeignKey(e => e.id_currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_currency>()
                .HasMany(e => e.t_g_denomination)
                .WithRequired(e => e.t_g_currency)
                .HasForeignKey(e => e.id_currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_declared_denom>()
                .Property(e => e.declared_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_denomination>()
                .Property(e => e.value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_denomination>()
                .HasMany(e => e.t_g_counting_denom)
                .WithRequired(e => e.t_g_denomination)
                .HasForeignKey(e => e.id_denomination)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_denomination>()
                .HasMany(e => e.t_g_declared_denom)
                .WithRequired(e => e.t_g_denomination)
                .HasForeignKey(e => e.id_denomination)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_dvdeneg>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_encashpoint>()
                .HasMany(e => e.t_g_account)
                .WithRequired(e => e.t_g_encashpoint)
                .HasForeignKey(e => e.id_encashpoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_g_garter>()
                .Property(e => e.id_user)
                .IsFixedLength();

            modelBuilder.Entity<t_g_garter>()
                .Property(e => e.workstation)
                .IsFixedLength();

            modelBuilder.Entity<t_g_moving_bags>()
                .HasMany(e => e.t_g_moving_bags_detal)
                .WithRequired(e => e.t_g_moving_bags)
                .HasForeignKey(e => e.id_moving_bags);

            modelBuilder.Entity<t_g_multi_bags>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<t_g_multi_bags>()
                .Property(e => e.seal)
                .IsFixedLength();

            modelBuilder.Entity<t_g_multi_bags_content>()
                .Property(e => e.declared_value1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_multi_bags_content>()
                .Property(e => e.declared_value2)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_multi_bags_content>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_pered>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_pered_detal>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);           

            modelBuilder.Entity<t_g_shift>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<t_g_status>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<t_g_user>()
                .Property(e => e.code)
                .IsFixedLength();

            modelBuilder.Entity<t_g_user>()
                .HasMany(e => e.t_g_bags_opening)
                .WithRequired(e => e.t_g_user)
                .HasForeignKey(e => e.opening_user);

            modelBuilder.Entity<t_w_sessions>()
                .Property(e => e.id_session)
                .IsFixedLength();

            modelBuilder.Entity<t_g_cash_trace>()
                .Property(e => e.seqnumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<t_g_cash_trace>()
                .Property(e => e.count)
                .HasPrecision(18, 0);

            modelBuilder.Entity<t_g_cashtransfer_detalization_trace>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_content_trace>()
                .Property(e => e.declared_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_content_trace>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_denom_trace>()
                .Property(e => e.fact_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<t_g_counting_denom_trace>()
                .Property(e => e.serial_number)
                .IsFixedLength();

            modelBuilder.Entity<t_g_counting_denom_trace>()
                .Property(e => e.serial_number2)
                .IsFixedLength();

            modelBuilder.Entity<t_g_counting_denom_trace>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<t_g_counting_trace>()
                .Property(e => e.name)
                .IsFixedLength();
        }
    }
}
