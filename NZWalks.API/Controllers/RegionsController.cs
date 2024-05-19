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

        // Get all regions
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

        // Get region by ID
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

        // Create new region
        // POST: https://localhost:7139/api/Regions
        [HttpPost]
        public IActionResult CreateNew([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to domain model
            var regionDomain = new RegionDomain
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use domain model to create region in database
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();

            // Map domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // 
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        // Update region
        // PUT: https://localhost:7139/api/Regions/D164CA1D-DEB7-4A77-86C5-FB9A7148C835
        [HttpPut]
        [Route("{Id:Guid}")]
        public IActionResult Update([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if region exists
            var regionDomain = dbContext.Regions.Find(Id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map DTO to domain model
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            // ID has been found, save update to database
            dbContext.SaveChanges();

            // Map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
