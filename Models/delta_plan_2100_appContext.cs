using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Layer> Layer { get; set; }
        public virtual DbSet<MapInvestmentProject> MapInvestmentProject { get; set; }
        public virtual DbSet<TblComponentLevel1> TblComponentLevel1 { get; set; }
        public virtual DbSet<TblComponentLevel2> TblComponentLevel2 { get; set; }
        public virtual DbSet<TblComponentLevel3> TblComponentLevel3 { get; set; }
        public virtual DbSet<TblDeltaMetaData> TblDeltaMetaData { get; set; }
        public virtual DbSet<TblGraphData> TblGraphData { get; set; }
        public virtual DbSet<TblIndicatorFyData> TblIndicatorFyData { get; set; }
        public virtual DbSet<TblTabularData> TblTabularData { get; set; }
        public virtual DbSet<TblUserComments> TblUserComments { get; set; }
        public virtual DbSet<Topology> Topology { get; set; }
        public virtual DbSet<Upazilla> Upazilla { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("server=127.0.0.1;Port=5432;database=delta_plan_2100_app;User ID=postgres;password=cegis;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis")
                .HasPostgresExtension("postgis_topology");

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("district");

                entity.HasComment("Division > District");

                entity.Property(e => e.DistrictId).HasColumnName("district_id");

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("district_code")
                    .HasMaxLength(20);

                entity.Property(e => e.DistrictName)
                    .HasColumnName("district_name")
                    .HasMaxLength(250);

                entity.Property(e => e.DivisionId).HasColumnName("division_id");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("division");

                entity.HasComment("Division");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.DivisionCode)
                    .HasColumnName("division_code")
                    .HasMaxLength(20);

                entity.Property(e => e.DivisionName)
                    .HasColumnName("division_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Layer>(entity =>
            {
                entity.HasKey(e => new { e.TopologyId, e.LayerId })
                    .HasName("layer_pkey");

                entity.ToTable("layer", "topology");

                entity.HasIndex(e => new { e.SchemaName, e.TableName, e.FeatureColumn })
                    .HasName("layer_schema_name_table_name_feature_column_key")
                    .IsUnique();

                entity.Property(e => e.TopologyId).HasColumnName("topology_id");

                entity.Property(e => e.LayerId).HasColumnName("layer_id");

                entity.Property(e => e.ChildId).HasColumnName("child_id");

                entity.Property(e => e.FeatureColumn)
                    .IsRequired()
                    .HasColumnName("feature_column")
                    .HasColumnType("character varying");

                entity.Property(e => e.FeatureType).HasColumnName("feature_type");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.SchemaName)
                    .IsRequired()
                    .HasColumnName("schema_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnName("table_name")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Topology)
                    .WithMany(p => p.Layer)
                    .HasForeignKey(d => d.TopologyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("layer_topology_id_fkey");
            });

            modelBuilder.Entity<MapInvestmentProject>(entity =>
            {
                entity.HasKey(e => e.ShapeId)
                    .HasName("projectvalidwgs3587_pkey");

                entity.ToTable("map_investment_project");

                entity.Property(e => e.ShapeId)
                    .HasColumnName("shape_id")
                    .HasDefaultValueSql("nextval('projectvalidwgs3587_gid_seq'::regclass)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.Property(e => e.Objectid).HasColumnName("objectid");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(50);

                entity.Property(e => e.ShapeArea)
                    .HasColumnName("shape_area")
                    .HasColumnType("numeric");

                entity.Property(e => e.ShapeLeng)
                    .HasColumnName("shape_leng")
                    .HasColumnType("numeric");
            });

            modelBuilder.Entity<TblComponentLevel1>(entity =>
            {
                entity.HasKey(e => e.ComponentLevel1Id)
                    .HasName("tbl_component_level_1_pkey");

                entity.ToTable("tbl_component_level_1");

                entity.HasComment("Thematic group list");

                entity.Property(e => e.ComponentLevel1Id).HasColumnName("component_level_1_id");

                entity.Property(e => e.ComponentName)
                    .HasColumnName("component_name")
                    .HasMaxLength(250);

                entity.Property(e => e.DataVisualization)
                    .HasColumnName("data_visualization")
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            });

            modelBuilder.Entity<TblComponentLevel2>(entity =>
            {
                entity.HasKey(e => e.ComponentLevel2Id);

                entity.ToTable("tbl_component_level_2");

                entity.HasComment("Thematic type");

                entity.Property(e => e.ComponentLevel2Id).HasColumnName("component_level_2_id");

                entity.Property(e => e.ComponentName)
                    .HasColumnName("component_name")
                    .HasMaxLength(250);

                entity.Property(e => e.DataVisualization)
                    .HasColumnName("data_visualization")
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<TblComponentLevel3>(entity =>
            {
                entity.HasKey(e => e.ComponentLevel3Id);

                entity.ToTable("tbl_component_level_3");

                entity.HasComment("Thematic layers");

                entity.Property(e => e.ComponentLevel3Id).HasColumnName("component_level_3_id");

                entity.Property(e => e.ComponentName)
                    .HasColumnName("component_name")
                    .HasMaxLength(250);

                entity.Property(e => e.DataVisualization)
                    .HasColumnName("data_visualization")
                    .HasMaxLength(50);

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
                    .HasMaxLength(250);

                entity.Property(e => e.AccessDataSourceLocation)
                    .HasColumnName("access_data_source_location")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessDataSourceName)
                    .HasColumnName("access_data_source_name")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessDistributionFileFormat)
                    .HasColumnName("access_distribution_file_format")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessMediaOfDistribution)
                    .HasColumnName("access_media_of_distribution")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessOrgAddress)
                    .HasColumnName("access_org_address")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessOrgEmailAddress)
                    .HasColumnName("access_org_email_address")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessOrganization)
                    .HasColumnName("access_organization")
                    .HasMaxLength(250);

                entity.Property(e => e.AccessUseConstraints)
                    .HasColumnName("access_use_constraints")
                    .HasMaxLength(250);

                entity.Property(e => e.GenAddInfoSourceDataset)
                    .HasColumnName("gen_add_info_source_dataset")
                    .HasMaxLength(500);

                entity.Property(e => e.GenCompleteness)
                    .HasColumnName("gen_completeness")
                    .HasMaxLength(500);

                entity.Property(e => e.GenDatasetLanguage)
                    .HasColumnName("gen_dataset_language")
                    .HasMaxLength(50);

                entity.Property(e => e.GenHistOfTheDataset)
                    .HasColumnName("gen_hist_of_the_dataset")
                    .HasMaxLength(500);

                entity.Property(e => e.GenProcessDescription)
                    .HasColumnName("gen_process_description")
                    .HasMaxLength(500);

                entity.Property(e => e.GenPurposeProduction)
                    .HasColumnName("gen_purpose_production")
                    .HasMaxLength(500);

                entity.Property(e => e.GenQuality)
                    .HasColumnName("gen_quality")
                    .HasMaxLength(500);

                entity.Property(e => e.GenTitle)
                    .HasColumnName("gen_title")
                    .HasMaxLength(250);

                entity.Property(e => e.GenTypeOfDataset)
                    .HasColumnName("gen_type_of_dataset")
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.OvAbstract)
                    .HasColumnName("ov_abstract")
                    .HasMaxLength(500);

                entity.Property(e => e.OvTitle)
                    .HasColumnName("ov_title")
                    .HasMaxLength(250);

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

                entity.Property(e => e.DtValue)
                    .HasColumnName("dt_value")
                    .HasColumnType("numeric");

                entity.Property(e => e.DtYear).HasColumnName("dt_year");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.ParentLevel).HasColumnName("parent_level");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TblIndicatorFyData>(entity =>
            {
                entity.HasKey(e => e.IndicatorAutoId)
                    .HasName("tbl_indicator_fy_data_pkey");

                entity.ToTable("tbl_indicator_fy_data");

                entity.HasComment("Indicators BAU Policy and BDP Policy Data");

                entity.Property(e => e.IndicatorAutoId).HasColumnName("indicator_auto_id");

                entity.Property(e => e.FiscalYear).HasColumnName("fiscal_year");

                entity.Property(e => e.FyValue)
                    .HasColumnName("fy_value")
                    .HasColumnType("numeric(8,2)");

                entity.Property(e => e.FyValueUnit)
                    .HasColumnName("fy_value_unit")
                    .HasMaxLength(100);

                entity.Property(e => e.IndicatorName)
                    .HasColumnName("indicator_name")
                    .HasMaxLength(150);

                entity.Property(e => e.IndicatorType)
                    .HasColumnName("indicator_type")
                    .HasComment("1 = BAU; 2 = BDP;");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.ParentLevel).HasColumnName("parent_level");

                entity.Property(e => e.VisualOrder).HasColumnName("visual_order");
            });

            modelBuilder.Entity<TblTabularData>(entity =>
            {
                entity.HasKey(e => e.TabularDataId)
                    .HasName("tbl_tabular_data_pkey");

                entity.ToTable("tbl_tabular_data");

                entity.Property(e => e.TabularDataId).HasColumnName("tabular_data_id");

                entity.Property(e => e.Contents)
                    .HasColumnName("contents")
                    .HasMaxLength(5000);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

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
                    .HasMaxLength(500);

                entity.Property(e => e.UserEmailAddress)
                    .HasColumnName("user_email_address")
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserPhone)
                    .HasColumnName("user_phone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Topology>(entity =>
            {
                entity.ToTable("topology", "topology");

                entity.HasIndex(e => e.Name)
                    .HasName("topology_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('topology_id_seq'::regclass)");

                entity.Property(e => e.Hasz).HasColumnName("hasz");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Precision).HasColumnName("precision");

                entity.Property(e => e.Srid).HasColumnName("srid");
            });

            modelBuilder.Entity<Upazilla>(entity =>
            {
                entity.ToTable("upazilla");

                entity.HasComment("Division > District > Upazilla");

                entity.Property(e => e.UpazillaId).HasColumnName("upazilla_id");

                entity.Property(e => e.DistrictId).HasColumnName("district_id");

                entity.Property(e => e.UpazillaCode)
                    .HasColumnName("upazilla_code")
                    .HasMaxLength(20);

                entity.Property(e => e.UpazillaName)
                    .HasColumnName("upazilla_name")
                    .HasMaxLength(250);
            });

            modelBuilder.HasSequence("district_district_id_seq");

            modelBuilder.HasSequence("division_division_id_seq");

            modelBuilder.HasSequence("tbl_component_level_1_component_level_1_id_seq");

            modelBuilder.HasSequence("tbl_tabular_data_tabular_data_id_seq");

            modelBuilder.HasSequence("upazilla_upazilla_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
