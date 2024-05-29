using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<ImageDomain> Upload(ImageDomain imageDomain)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{imageDomain.FileName}{imageDomain.FileExtension}");

            // Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await imageDomain.File.CopyToAsync(stream);

            // https://localhost:7139/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/images/{imageDomain.FileName}{imageDomain.FileExtension}";

            imageDomain.FilePath = urlFilePath;

            // Add image to the images table
            await dbContext.Images.AddAsync(imageDomain);
            await dbContext.SaveChangesAsync();

            return imageDomain;
        }
    }
}
