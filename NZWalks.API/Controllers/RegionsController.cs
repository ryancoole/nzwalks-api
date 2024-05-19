using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }

        // GET: https://localhost:7139/api/Regions/D164CA1D-DEB7-4A77-86C5-FB9A7148C835
        [HttpGet]
        [Route("{Id:Guid}")]
        public IActionResult GetById([FromRoute]Guid Id)
        {
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == Id);
            var region = dbContext.Regions.Find(Id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }
    }
}
