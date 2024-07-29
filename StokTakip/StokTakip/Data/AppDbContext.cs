using Microsoft.EntityFrameworkCore;
using StokTakip.Models;

namespace StokTakip.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Cihaz> Cihazlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cihaz>()
                .HasOne(c => c.Kisi)
                .WithMany(k => k.Cihazlar)
                .HasForeignKey(c => c.KisiId);
        }

    }
}
