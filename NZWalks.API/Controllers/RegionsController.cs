using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:7139/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: https://localhost:7139/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            // Map domain models to DTOs
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        // GET: https://localhost:7139/api/Regions/D164CA1D-DEB7-4A77-86C5-FB9A7148C835
        [HttpGet]
        [Route("{Id:Guid}")]
        public IActionResult GetById([FromRoute] Guid Id)
        {
            // Get data from database - Domain Model
            //var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == Id);
            var regionDomain = dbContext.Regions.Find(Id);            

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return DTO
            return Ok(regionDto);
        }
    }
}
