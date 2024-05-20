using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<RegionDomain>> GetAllAsync();
        Task<RegionDomain?> GetByIdAsync(Guid id);
        Task<RegionDomain> CreateAsync(RegionDomain region);
        Task<RegionDomain?> UpdateAsync(Guid id, RegionDomain region);
        Task<RegionDomain?> DeleteAsync(Guid id);
    }
}
