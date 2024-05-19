using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<DifficultyDomain> Difficulties { get; set; }
        public DbSet<RegionDomain> Regions { get; set; }
        public DbSet<WalkDomain> Walks { get; set; }
    }
}
