﻿using AutoMapper;
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


    }
}