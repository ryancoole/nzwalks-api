using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:7139/api/walks
    [Route("api/walks")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository) 
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Get all walks
        // GET: https://localhost:7139/api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain Models
            var walksDomain = await walkRepository.GetAllAsync();

            // Map domain models to DTOs and return the DTOs
            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }

        // Get walk by ID
        // GET: https://localhost:7139/api/walks/3CE74BD8-9C65-4817-0EF1-08DC79D5F21C
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            // Get data from database - Domain Model
            //var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == Id);
            var walkDomain = await walkRepository.GetByIdAsync(Id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO and return the DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        // Create walk
        // POST: https://localhost:7139/api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to domain model
            var walkDomain = mapper.Map<WalkDomain>(addWalkRequestDto);

            // Use domain model to create walk in database
            walkDomain = await walkRepository.CreateAsync(walkDomain);

            // Map domain model back to DTO and return DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        // Update walk
        // PUT: https://localhost:7139/api/walks/3CE74BD8-9C65-4817-0EF1-08DC79D5F21C
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to domain model
            var walkDomain = mapper.Map<WalkDomain>(updateWalkRequestDto);

            // Check if walk exists
            walkDomain = await walkRepository.UpdateAsync(Id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO and return the DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }


        // Delete walk
        // DELETE: https://localhost:7139/api/walks/3CE74BD8-9C65-4817-0EF1-08DC79D5F21C
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            // Check if walk exists
            var walkDomain = await walkRepository.DeleteAsync(Id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO and return the DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
    }
}
