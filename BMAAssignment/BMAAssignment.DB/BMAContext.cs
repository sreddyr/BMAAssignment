
namespace BMAAssignment.DB
{
    using Microsoft.EntityFrameworkCore;

    public class BMAContext : DbContext
    {
        public BMAContext()
        {

        }

        public BMAContext(DbContextOptions<BMAContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=corezendev.database.windows.net;Database=corezendev;User Id=devadmin;Password=P@ssw0rd@123;");
        }


        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContactInfo> ContactInfoes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.Info)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Appointments)
                .WithOne(e => e.Company)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Company)
                 .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContactInfo>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInfo>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ContactInfo>()
                .Property(e => e.WorkNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.SurName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Appointments)
                .WithOne(e => e.Employee)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ContactInfoes)
                .WithOne(e => e.Employee)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Employee)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .Property(e => e.Role1)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
