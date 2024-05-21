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

        public async Task<WalkDomain> CreateAsync(WalkDomain walkDomain)
        {
            await dbContext.Walks.AddAsync(walkDomain);
            await dbContext.SaveChangesAsync();

            return walkDomain;
        }
    }
}
