using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<DifficultyDomain> Difficulties { get; set; }
        public DbSet<RegionDomain> Regions { get; set; }
        public DbSet<WalkDomain> Walks { get; set; }
        public DbSet<ImageDomain> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed the data for Difficulties
            var difficulties = new List<DifficultyDomain>() 
            { 
                // Create guid in C# Interactive window with "Guid.NewGuid()"
                new DifficultyDomain()
                {
                    Id = Guid.Parse("17a88e61-deec-4f5f-8651-29255576572e"),
                    Name = "Easy"
                },
                new DifficultyDomain()
                {
                    Id = Guid.Parse("9c3cd398-8d4e-4e2d-9abd-36b9aa83872a"),
                    Name = "Medium"
                },
                new DifficultyDomain()
                {
                    Id = Guid.Parse("832a2b58-14c7-42e1-a6ed-6e52948bd06c"),
                    Name = "Hard"
                }
            };

            // Seed Difficulties to the database
            modelBuilder.Entity<DifficultyDomain>().HasData(difficulties);

            // Seed the data for Regions
            var regions = new List<RegionDomain>()
            { 
                // Create guid in C# Interactive window with "Guid.NewGuid()"
                new RegionDomain
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new RegionDomain
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new RegionDomain
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new RegionDomain
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new RegionDomain
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new RegionDomain
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };

            // Seed Regions to the database
            modelBuilder.Entity<RegionDomain>().HasData(regions);
        }
    }
}
