using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RegionDomain> CreateAsync(RegionDomain region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<RegionDomain?> DeleteAsync(Guid Id)
        {
            var existingRegion = await dbContext.Regions.FindAsync(Id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<RegionDomain>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<RegionDomain?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Regions.FindAsync(Id);
        }

        public async Task<RegionDomain?> UpdateAsync(Guid Id, RegionDomain region)
        {
            var existingRegion = await dbContext.Regions.FindAsync(Id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}