using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Models;

namespace SkinCancerDetectionAPI.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> ApplicationUsers { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Authentication>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Lesion>()
                .HasOne(l => l.Patient)
                .WithMany()
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Workflow>()
                .HasOne(w => w.Patient)
                .WithMany()
                .HasForeignKey(w => w.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Biopsy>()
                .HasOne(b => b.Lesion)
                .WithMany()
                .HasForeignKey(b => b.LesionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Lesion)
                .WithMany()
                .HasForeignKey(i => i.LesionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkflowStage>()
                .HasOne(ws => ws.Workflow)
                .WithMany()
                .HasForeignKey(ws => ws.WorkflowId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.Biopsy)
                .WithMany()
                .HasForeignKey(d => d.BiopsyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ModelPrediction>()
                .HasOne(mp => mp.Image)
                .WithMany()
                .HasForeignKey(mp => mp.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FinalDiagnosis>()
                .HasOne(fd => fd.Diagnosis)
                .WithMany()
                .HasForeignKey(fd => fd.DiagnosisId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FinalDiagnosis>()
                .HasOne(fd => fd.ModelPrediction)
                .WithMany()
                .HasForeignKey(fd => fd.ModelPredictionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.FinalDiagnosis)
                .WithMany()
                .HasForeignKey(t => t.FinalDiagnosisId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FollowUp>()
                .HasOne(fu => fu.Treatment)
                .WithMany()
                .HasForeignKey(fu => fu.TreatmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
