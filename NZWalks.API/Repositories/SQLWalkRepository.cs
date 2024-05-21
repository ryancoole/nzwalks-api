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

        public async Task<List<WalkDomain>> GetAllAsync()
        {
            // dbContext collects a list of all the walks and uses Include() to get related data from other tables
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<WalkDomain> CreateAsync(WalkDomain walkDomain)
        {
            await dbContext.Walks.AddAsync(walkDomain);
            await dbContext.SaveChangesAsync();

            return walkDomain;
        }        
    }
}
