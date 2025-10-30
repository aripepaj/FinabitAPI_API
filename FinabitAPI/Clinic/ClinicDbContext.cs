using Microsoft.EntityFrameworkCore;
using FinabitAPI.Hotel.HGuests;
using PatientEntity = FinabitAPI.Clinic.Patient.Patient;
using FinabitAPI.Clinic.Patient;
using FinabitAPI.Clinic.Medical;

namespace FinabitAPI.Clinic
{
    /// <summary>
    /// DbContext for Clinic module
    /// </summary>
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for Patient entities
        /// </summary>
        public DbSet<PatientEntity> Patients { get; set; }

        /// <summary>
        /// DbSet for dynamic field definitions
        /// </summary>
        public DbSet<PatientFieldDefinition> PatientFieldDefinitions { get; set; }

        /// <summary>
        /// DbSet for patient field values
        /// </summary>
        public DbSet<PatientFieldValue> PatientFieldValues { get; set; }

      // Unified Medical Modules
      public DbSet<MedicalRecord> MedicalRecords { get; set; }
      public DbSet<MedicalModuleType> ModuleTypes { get; set; }
      // Anamnesis (stickers/templates)
      public DbSet<AnamnesisType> AnamnesisTypes { get; set; }
      public DbSet<AnamnesisDefinition> AnamnesisDefinitions { get; set; }

        // Medical Field Configuration
        public DbSet<MedicalFieldDefinition> MedicalFieldDefinitions { get; set; }
        public DbSet<MedicalFieldValue> MedicalFieldValues { get; set; }

        /// <summary>
        /// Configure entity relationships and constraints
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Patient entity
            modelBuilder.Entity<PatientEntity>(entity =>
            {
                // Configure table name with clinic prefix
                entity.ToTable("tblCLPatients");

                // Configure primary key
                entity.HasKey(p => p.Id);

                // Note: No EF Core navigation to HGuest to avoid mapping conflicts
                // Foreign key constraint exists in database but managed manually

                // Configure properties
                entity.Property(p => p.MedicalRecordNumber)
                      .HasMaxLength(50);

                entity.Property(p => p.BloodType)
                      .HasMaxLength(10);

                entity.Property(p => p.Allergies)
                      .HasMaxLength(1000);

                entity.Property(p => p.EmergencyContactName)
                      .HasMaxLength(100);

                entity.Property(p => p.EmergencyContactPhone)
                      .HasMaxLength(50);

                entity.Property(p => p.Notes)
                      .HasMaxLength(2000);

                entity.Property(p => p.CreatedDate)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(p => p.UpdatedDate);
            });

            // Configure PatientFieldDefinition entity
            modelBuilder.Entity<PatientFieldDefinition>(entity =>
            {
                entity.ToTable("tblCLPatientFieldDefinitions");

                entity.HasKey(f => f.Id);

                entity.Property(f => f.FieldKey)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(f => f.DisplayName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(f => f.FieldType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(f => f.HelpText)
                      .HasMaxLength(500);

                entity.Property(f => f.CssClass)
                      .HasMaxLength(100);

                entity.Property(f => f.CreatedDate)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                // Index for performance
                entity.HasIndex(f => new { f.DepartmentID, f.FieldKey })
                      .HasDatabaseName("IX_PatientFieldDef_Dept_Key");

                entity.HasIndex(f => new { f.DepartmentID, f.DisplayOrder })
                      .HasDatabaseName("IX_PatientFieldDef_Dept_Order");
            });

            // Configure PatientFieldValue entity
            modelBuilder.Entity<PatientFieldValue>(entity =>
            {
                entity.ToTable("tblCLPatientFieldValues");

                entity.HasKey(v => v.Id);

                entity.Property(v => v.FieldKey)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(v => v.CreatedDate)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                // Foreign key relationship
                entity.HasOne(v => v.FieldDefinition)
                      .WithMany()
                      .HasForeignKey(v => v.FieldDefinitionID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Indexes for performance
                entity.HasIndex(v => new { v.PatientID, v.FieldKey })
                      .HasDatabaseName("IX_PatientFieldValues_Patient_Key");

                entity.HasIndex(v => v.FieldDefinitionID)
                      .HasDatabaseName("IX_PatientFieldValues_Definition");
            });

            // Configure MedicalFieldDefinition entity
            modelBuilder.Entity<MedicalFieldDefinition>(entity =>
            {
                entity.ToTable("tblCLMedicalFieldDefinitions");
                entity.HasKey(f => f.Id);

                entity.Property(f => f.ModuleType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(f => f.FieldKey)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(f => f.DisplayName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(f => f.CreatedDate)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                // Indexes for performance
                entity.HasIndex(f => new { f.ModuleType, f.DepartmentID, f.FieldKey })
                      .HasDatabaseName("IX_MedicalFieldDef_Module_Dept_Key");
            });

            // Configure MedicalFieldValue entity
            modelBuilder.Entity<MedicalFieldValue>(entity =>
            {
                entity.ToTable("tblCLMedicalFieldValues");
                entity.HasKey(v => v.Id);

                entity.Property(v => v.ModuleType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(v => v.FieldKey)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(v => v.CreatedDate)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                // Foreign key relationship
                entity.HasOne(v => v.FieldDefinition)
                      .WithMany()
                      .HasForeignKey(v => v.FieldDefinitionID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Indexes
                entity.HasIndex(v => new { v.MedicalRecordID, v.ModuleType, v.FieldKey })
                      .HasDatabaseName("IX_MedicalFieldValues_Record_Module_Key");
            });

            // Configure Module Types lookup
            modelBuilder.Entity<MedicalModuleType>(entity =>
            {
                entity.ToTable("tblCLMedicalModuleTypes");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Description).HasMaxLength(200);

                // Seed default module types
                entity.HasData(
                    new MedicalModuleType { Id = 1, Name = MedicalModuleTypes.Ankesat, Description = "Surveys/Questionnaires" },
                    new MedicalModuleType { Id = 2, Name = MedicalModuleTypes.Ekzaminimet, Description = "Examinations" },
                    new MedicalModuleType { Id = 3, Name = MedicalModuleTypes.Diagnozat, Description = "Diagnoses" },
                    new MedicalModuleType { Id = 4, Name = MedicalModuleTypes.Terapite, Description = "Therapies" },
                    new MedicalModuleType { Id = 5, Name = MedicalModuleTypes.Analizat, Description = "Laboratory Tests" },
                    new MedicalModuleType { Id = 6, Name = MedicalModuleTypes.Keshillat, Description = "Consultations" },
                    new MedicalModuleType { Id = 7, Name = MedicalModuleTypes.Kontrollat, Description = "Follow-ups" },
                    new MedicalModuleType { Id = 8, Name = MedicalModuleTypes.Verejtjet, Description = "Observations" }
                );
            });

            // Configure unified medical records table
            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.ToTable("tblCLMedicalRecords");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Kodi).HasMaxLength(50);
                entity.Property(e => e.Titulli).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ModuleType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(50).HasDefaultValue("Active");
                entity.Property(e => e.Priority).HasMaxLength(20).HasDefaultValue("Normal");
                entity.Property(e => e.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

                // Relationship to lookup
                entity.HasOne(e => e.ModuleTypeRef)
                      .WithMany()
                      .HasForeignKey(e => e.ModuleTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => new { e.PatientID, e.ModuleType })
                      .HasDatabaseName("IX_tblCLMedicalRecords_Patient_Module");

                entity.HasIndex(e => new { e.Departamenti, e.ModuleType })
                      .HasDatabaseName("IX_tblCLMedicalRecords_Dept_Module");

                entity.HasIndex(e => e.ModuleTypeId)
                      .HasDatabaseName("IX_tblCLMedicalRecords_ModuleTypeId");
            });

                  // Configure Anamnesis Types
                  modelBuilder.Entity<AnamnesisType>(entity =>
                  {
                        entity.ToTable("tblCLAnamnesisTypes");
                        entity.HasKey(t => t.Id);
                        entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                        entity.Property(t => t.Description).HasMaxLength(500);

                        entity.HasIndex(t => t.Name).IsUnique();
                  });

                  // Configure Anamnesis Definitions
                  modelBuilder.Entity<AnamnesisDefinition>(entity =>
                  {
                        entity.ToTable("tblCLAnamnesisDefinitions");
                        entity.HasKey(d => d.Id);

                        entity.Property(d => d.Kodi).HasMaxLength(50);
                        entity.Property(d => d.Titulli).IsRequired().HasMaxLength(300);
                        entity.Property(d => d.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

                        entity.HasOne(d => d.Type)
                                .WithMany()
                                .HasForeignKey(d => d.TypeId)
                                .OnDelete(DeleteBehavior.Restrict);

                        entity.HasIndex(d => new { d.TypeId, d.DepartmentID, d.Titulli })
                                .HasDatabaseName("IX_AnamnesisDefinitions_Type_Dept_Title");
                  });
        }

        // Removed ConfigureMedicalEntity<T> as we now use a single MedicalRecord entity
    }
}

/*
Migration Commands:
dotnet ef migrations add Clinic_Init -c ClinicDbContext
dotnet ef database update -c ClinicDbContext
*/