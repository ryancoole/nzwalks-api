using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:7139/api/students
    [Route("api/v{version:apiVersion}/students")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:7139/api/v1/students
        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetAllStudentsV1()
        {
            string[] studentNames = new string[] {"Hannah","Holly","Jon","Richard","Ryan"};

            return Ok(studentNames);
        }

        // GET: https://localhost:7139/api/v2/students
        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetAllStudentsV2()
        {
            string[] studentNames = new string[] { "Grant", "Hannah", "Holly", "Jon", "Richard", "Ryan", "Sukh" };

            return Ok(studentNames);
        }
    }
}
