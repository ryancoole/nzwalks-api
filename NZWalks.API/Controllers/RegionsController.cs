using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:7139/api/regions
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Get all regions
        // GET: https://localhost:7139/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map domain models to DTOs and return the DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        // Get region by ID
        // GET: https://localhost:7139/api/regions/D164CA1D-DEB7-4A77-86C5-FB9A7148C835
        [HttpGet]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            // Get data from database - Domain Model
            //var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == Id);
            var regionDomain = await regionRepository.GetByIdAsync(Id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO and return the DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // Create region
        // POST: https://localhost:7139/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Checks if the AddRegionRequestDto validation is valid
            //if (ModelState.IsValid)
            //{

                // Map DTO to domain model
                var regionDomain = mapper.Map<RegionDomain>(addRegionRequestDto);

                // Use domain model to create region in database
                regionDomain = await regionRepository.CreateAsync(regionDomain);

                // Map domain model back to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomain);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

            //} 
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        // Update region
        // PUT: https://localhost:7139/api/regions/D164CA1D-DEB7-4A77-86C5-FB9A7148C835
        [HttpPut]
        [Route("{Id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map DTO to domain model
            var regionDomain = mapper.Map<RegionDomain>(updateRegionRequestDto);

            // Check if region exists
            regionDomain = await regionRepository.UpdateAsync(Id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO and return the DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // Delete region
        // DELETE: https://localhost:7139/api/regions/D164CA1D-DEB7-4A77-86C5-FB9A7148C835
        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            // Check if region exists
            var regionDomain = await regionRepository.DeleteAsync(Id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO and return the DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
    }
}