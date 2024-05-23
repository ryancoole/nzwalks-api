using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<WalkDomain>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<WalkDomain?> GetByIdAsync(Guid id);
        Task<WalkDomain> CreateAsync(WalkDomain walkDomain);
        Task<WalkDomain?> UpdateAsync(Guid id, WalkDomain walkDomain);
        Task<WalkDomain?> DeleteAsync(Guid id);
    }
}