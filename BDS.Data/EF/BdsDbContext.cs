using BDS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDS.Data.EF
{
    public class BdsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"postgres://postgres:admin@localhost:5432/BDS");
        }
        
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<ProjectMedia> ProjectMedia { get; set; }
        public DbSet<RealEstateMedia> RealEstateMedia { get; set; }
        public DbSet<NewsMedia> NewsMedia { get; set; }
        public DbSet<RecruitmentMedia> RecruitmentMedia { get; set; }
        
        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasKey("id");
            builder.Entity<Area>()
                .HasKey("id");
            builder.Entity<RealEstate>()
                .HasKey("id");
            builder.Entity<News>()
                .HasKey("id");
            builder.Entity<Recruitment>()
                .HasKey("id");
            builder.Entity<ProjectMedia>()
                .HasKey("id");
            builder.Entity<RealEstateMedia>()
                .HasKey("id");
            builder.Entity<NewsMedia>()
                .HasKey("id");
            builder.Entity<RecruitmentMedia>()
                .HasKey("id");
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
        
    }
}