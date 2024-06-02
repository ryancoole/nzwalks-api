using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDTO> responseBody = new List<RegionDTO>();

            try
            {
                // Get all Regions from API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7139/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

               responseBody.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                return View(responseBody);
            }
            catch (Exception ex)
            {
                // Log exception
                return View(ex.Message);
            }            
        }
    }
}
