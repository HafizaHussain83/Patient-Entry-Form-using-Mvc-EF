using Microsoft.EntityFrameworkCore;
using MVCStore.Models;

namespace MVCStore.Services
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions options): base(options)
        { 


        }
        //public DbSet<Product> products { get; set; }
        //public DbSet<Doctor> Doctors { get; set; }
        //public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<Product> products { get; set; } = default!;
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId);
                entity.ToTable("ALL_DOCTORS");
                entity.Property(d => d.DoctorId).HasColumnName("doctor_id");
                entity.Property(d => d.DepartmentId).HasColumnName("department_id");
                entity.Property(d => d.DoctorName).HasColumnName("doctor_name");
                entity.Property(d => d.DoctorType).HasColumnName("doctor_type");
            });

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Room)
            .WithMany(r => r.Products)
            .HasForeignKey(p => p.Room_id)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
    
    
}
