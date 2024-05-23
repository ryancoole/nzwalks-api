using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<WalkDomain>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            // dbContext collects a list of all the walks and uses Include() to get related data from other tables
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Check if query string wants the filter results
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                // If filtering by Name field (ignore casing)
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    // Filter by filterQuery
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Check if query string wants the sort results
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                // If sorting by Name field (ignore casing)
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    // If isAscending is true, ascend, otherwise descend.
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                // If sorting by LengthInKm field (ignore casing)
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    // If isAscending is true, ascend, otherwise descend.
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<WalkDomain?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<WalkDomain> CreateAsync(WalkDomain walkDomain)
        {
            await dbContext.Walks.AddAsync(walkDomain);
            await dbContext.SaveChangesAsync();

            return walkDomain;
        }

        public async Task<WalkDomain?> UpdateAsync(Guid Id, WalkDomain walkDomain)
        {
            var existingWalk = await dbContext.Walks.FindAsync(Id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walkDomain.Name;
            existingWalk.Description = walkDomain.Description;
            existingWalk.LengthInKm = walkDomain.LengthInKm;
            existingWalk.WalkImageUrl = walkDomain.WalkImageUrl;
            existingWalk.DifficultyId = walkDomain.DifficultyId;
            existingWalk.RegionId = walkDomain.RegionId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<WalkDomain?> DeleteAsync(Guid Id)
        {
            var existingWalk = await dbContext.Walks.FindAsync(Id);

            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}