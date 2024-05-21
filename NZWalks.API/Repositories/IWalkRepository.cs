using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<WalkDomain> CreateAsync(WalkDomain walkDomain);
    }
}
