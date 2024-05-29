using NZWalks.API.Models.Domain;
using System.Net;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<ImageDomain> Upload(ImageDomain imageDomain);
    }
}
