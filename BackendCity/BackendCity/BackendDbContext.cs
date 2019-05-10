using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendCity
{
    public class BackendDbContext : DbContext
    {
        public static string ccnx = "Server=127.0.0.1;Database=dbcity;User=root;Password=admin;";

        public BackendDbContext(DbContextOptions o) : base(o)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(table =>
            {
                table.ToTable("User");
                table.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                table.HasKey(e => e.Id);
                table.Property(e => e.Date);
                table.Property(e => e.IP);
                table.Property(e => e.Chiffre);
                table.Property(e => e.Text);
            });
        }
    }
}