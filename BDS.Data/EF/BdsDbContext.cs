using BDS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BDS.Data.EF
{
    public class BdsDbContext : IdentityDbContext<User,Role,long>
    {
        public BdsDbContext(DbContextOptions<BdsDbContext> options) : base((options))
        {
            
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=BDS;User Id=postgres;Password=admin");
        //     base.OnConfiguring(optionsBuilder);
        // }
        
        
        public DbSet<Project> Project { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<RealEstate> RealEstate { get; set; }
        public DbSet<RealEstateType> RealEstateType { get; set; }
        public DbSet<News> News { get; set; }
        
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<RoleClaim> RoleClaim { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Recruitment> Recruitment { get; set; }
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
            builder.Entity<User>()
                .HasKey("Id");
            builder.Entity<Role>()
                .HasKey("Id");
            builder.Entity<IdentityUserClaim<long>>()
                .HasKey("Id");
            builder.Entity<IdentityUserRole<long>>()
                .HasKey(x => new {x.UserId, x.RoleId});
            builder.Entity<IdentityUserLogin<long>>()
                .HasNoKey();
            builder.Entity<IdentityRoleClaim<long>>()
                .HasKey("Id");
            builder.Entity<IdentityUserToken<long>>()
                .HasNoKey();
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
        
    }
}