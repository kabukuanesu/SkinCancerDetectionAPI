using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Models;

namespace SkinCancerDetectionAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSet properties for all your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Lesion> Lesions { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Biopsy> Biopsies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<WorkflowStage> WorkflowStages { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<ModelPrediction> ModelPredictions { get; set; }
        public DbSet<FinalDiagnosis> FinalDiagnoses { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }

        // Configure relationships and other database-specific settings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships here if needed

            // User -> Authentication (One-to-One)
            modelBuilder.Entity<Authentication>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // User -> Patient (One-to-Many)
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Patient -> Lesion (One-to-Many)
            modelBuilder.Entity<Lesion>()
                .HasOne(l => l.Patient)
                .WithMany()
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Patient -> Workflow (One-to-Many)
            modelBuilder.Entity<Workflow>()
                .HasOne(w => w.Patient)
                .WithMany()
                .HasForeignKey(w => w.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Lesion -> Biopsy (One-to-Many)
            modelBuilder.Entity<Biopsy>()
                .HasOne(b => b.Lesion)
                .WithMany()
                .HasForeignKey(b => b.LesionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Lesion -> Image (One-to-Many)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Lesion)
                .WithMany()
                .HasForeignKey(i => i.LesionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Workflow -> WorkflowStage (One-to-Many)
            modelBuilder.Entity<WorkflowStage>()
                .HasOne(ws => ws.Workflow)
                .WithMany()
                .HasForeignKey(ws => ws.WorkflowId)
                .OnDelete(DeleteBehavior.NoAction);

            // Biopsy -> Diagnosis (One-to-Many)
            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.Biopsy)
                .WithMany()
                .HasForeignKey(d => d.BiopsyId)
                .OnDelete(DeleteBehavior.NoAction);

            // Image -> ModelPrediction (One-to-Many)
            modelBuilder.Entity<ModelPrediction>()
                .HasOne(mp => mp.Image)
                .WithMany()
                .HasForeignKey(mp => mp.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

            // Diagnosis -> FinalDiagnosis (One-to-Many)
            modelBuilder.Entity<FinalDiagnosis>()
                .HasOne(fd => fd.Diagnosis)
                .WithMany()
                .HasForeignKey(fd => fd.DiagnosisId)
                .OnDelete(DeleteBehavior.NoAction);

            // ModelPrediction -> FinalDiagnosis (One-to-Many)
            modelBuilder.Entity<FinalDiagnosis>()
                .HasOne(fd => fd.ModelPrediction)
                .WithMany()
                .HasForeignKey(fd => fd.ModelPredictionId)
                .OnDelete(DeleteBehavior.NoAction);

            // FinalDiagnosis -> Treatment (One-to-Many)
            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.FinalDiagnosis)
                .WithMany()
                .HasForeignKey(t => t.FinalDiagnosisId)
                .OnDelete(DeleteBehavior.NoAction);

            // Treatment -> FollowUp (One-to-Many)
            modelBuilder.Entity<FollowUp>()
                .HasOne(fu => fu.Treatment)
                .WithMany()
                .HasForeignKey(fu => fu.TreatmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
