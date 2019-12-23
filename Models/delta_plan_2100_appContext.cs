using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DeltaPlan2100API.Models
{
    public partial class delta_plan_2100_appContext : DbContext
    {
        public delta_plan_2100_appContext()
        {
        }

        public delta_plan_2100_appContext(DbContextOptions<delta_plan_2100_appContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<TblComponentLevel1> TblComponentLevel1 { get; set; }
        public virtual DbSet<TblComponentLevel2> TblComponentLevel2 { get; set; }
        public virtual DbSet<TblComponentLevel3> TblComponentLevel3 { get; set; }
        public virtual DbSet<TblDeltaMetaData> TblDeltaMetaData { get; set; }
        public virtual DbSet<TblGraphData> TblGraphData { get; set; }
        public virtual DbSet<TblTabularData> TblTabularData { get; set; }
        public virtual DbSet<TblUserComments> TblUserComments { get; set; }
        public virtual DbSet<Upazilla> Upazilla { get; set; }
        //public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder.UseNpgsql("host=127.0.0.1;port=5432;database=delta_plan_2100_app;user id=postgres;password=cegis;");
                //_ = optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("district");

                entity.HasComment("Division > District");

                entity.Property(e => e.DistrictId).HasColumnName("district_id");

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("district_code")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.DistrictName)
                    .HasColumnName("district_name")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.DivisionId).HasColumnName("division_id");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("division");

                entity.HasComment("Division");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.DivisionCode)
                    .HasColumnName("division_code")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.DivisionName)
                    .HasColumnName("division_name")
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblComponentLevel1>(entity =>
            {
                entity.HasKey(e => e.ComponentLevel1Id)
                    .HasName("tbl_component_level_1_pkey");

                entity.ToTable("tbl_component_level_1");

                entity.Property(e => e.ComponentLevel1Id).HasColumnName("component_level_1_id");

                entity.Property(e => e.ComponentName)
                    .HasColumnName("component_name")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.DataVisualization)
                    .HasColumnName("data_visualization")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            });

            modelBuilder.Entity<TblComponentLevel2>(entity =>
            {
                entity.HasKey(e => e.ComponentLevel2Id);

                entity.ToTable("tbl_component_level_2");

                entity.Property(e => e.ComponentLevel2Id).HasColumnName("component_level_2_id");

                entity.Property(e => e.ComponentName)
                    .HasColumnName("component_name")
                    .HasMaxLength(250);

                entity.Property(e => e.DataVisualization)
                    .HasColumnName("data_visualization")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<TblComponentLevel3>(entity =>
            {
                entity.HasKey(e => e.ComponentLevel3Id);

                entity.ToTable("tbl_component_level_3");

                entity.Property(e => e.ComponentLevel3Id).HasColumnName("component_level_3_id");

                entity.Property(e => e.ComponentName)
                    .HasColumnName("component_name")
                    .HasMaxLength(250);

                entity.Property(e => e.DataVisualization)
                    .HasColumnName("data_visualization")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<TblDeltaMetaData>(entity =>
            {
                entity.HasKey(e => e.MetaDataId)
                    .HasName("tbl_delta_meta_data_pkey");

                entity.ToTable("tbl_delta_meta_data");

                entity.Property(e => e.MetaDataId).HasColumnName("meta_data_id");

                entity.Property(e => e.AccessAccessConstraints)
                    .HasColumnName("access_access_constraints")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessDataSourceLocation)
                    .HasColumnName("access_data_source_location")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessDataSourceName)
                    .HasColumnName("access_data_source_name")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessDistributionFileFormat)
                    .HasColumnName("access_distribution_file_format")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessMediaOfDistribution)
                    .HasColumnName("access_media_of_distribution")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessOrgAddress)
                    .HasColumnName("access_org_address")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessOrgEmailAddress)
                    .HasColumnName("access_org_email_address")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessOrganization)
                    .HasColumnName("access_organization")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.AccessUseConstraints)
                    .HasColumnName("access_use_constraints")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.GenAddInfoSourceDataset)
                    .HasColumnName("gen_add_info_source_dataset")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.GenCompleteness)
                    .HasColumnName("gen_completeness")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.GenDatasetLanguage)
                    .HasColumnName("gen_dataset_language")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.GenHistOfTheDataset)
                    .HasColumnName("gen_hist_of_the_dataset")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.GenProcessDescription)
                    .HasColumnName("gen_process_description")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.GenPurposeProduction)
                    .HasColumnName("gen_purpose_production")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.GenQuality)
                    .HasColumnName("gen_quality")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.GenTitle)
                    .HasColumnName("gen_title")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.GenTypeOfDataset)
                    .HasColumnName("gen_type_of_dataset")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.OvAbstract)
                    .HasColumnName("ov_abstract")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.OvTitle)
                    .HasColumnName("ov_title")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.ParentLevel).HasColumnName("parent_level");
            });

            modelBuilder.Entity<TblGraphData>(entity =>
            {
                entity.HasKey(e => e.GraphDataId)
                    .HasName("tbl_graph_data_pkey");

                entity.ToTable("tbl_graph_data");

                entity.Property(e => e.GraphDataId).HasColumnName("graph_data_id");

                entity.Property(e => e.DtMonth).HasColumnName("dt_month");

                entity.Property(e => e.DtYear).HasColumnName("dt_year");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.ParentLevel).HasColumnName("parent_level");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(500)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblTabularData>(entity =>
            {
                entity.HasKey(e => e.TabularDataId)
                    .HasName("tbl_tabular_data_pkey");

                entity.ToTable("tbl_tabular_data");

                entity.Property(e => e.TabularDataId).HasColumnName("tabular_data_id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParameterName)
                    .HasColumnName("parameter_name")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.ParameterValue)
                    .HasColumnName("parameter_value")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.ParentLevel).HasColumnName("parent_level");
            });

            modelBuilder.Entity<TblUserComments>(entity =>
            {
                entity.HasKey(e => e.CommentsId)
                    .HasName("tbl_user_comments_pkey");

                entity.ToTable("tbl_user_comments");

                entity.Property(e => e.CommentsId).HasColumnName("comments_id");

                entity.Property(e => e.UserComments)
                    .HasColumnName("user_comments")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.UserEmail)
                    .HasColumnName("user_email")
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Upazilla>(entity =>
            {
                entity.ToTable("upazilla");

                entity.HasComment("Division > District > Upazilla");

                entity.Property(e => e.UpazillaId).HasColumnName("upazilla_id");

                entity.Property(e => e.DistrictId).HasColumnName("district_id");

                entity.Property(e => e.UpazillaCode)
                    .HasColumnName("upazilla_code")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.UpazillaName)
                    .HasColumnName("upazilla_name")
                    .HasMaxLength(250)
                    .IsFixedLength();
            });

            modelBuilder.HasSequence("district_district_id_seq");

            modelBuilder.HasSequence("division_division_id_seq");

            modelBuilder.HasSequence("tbl_component_level_1_component_level_1_id_seq");

            modelBuilder.HasSequence("upazilla_upazilla_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
