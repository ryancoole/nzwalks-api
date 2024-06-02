using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel regionViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7139/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(regionViewModel), Encoding.UTF8, "application/json"),
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var client = httpClientFactory.CreateClient();

            var responseMessage = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7139/api/regions/{Id.ToString()}");

            if (responseMessage is not null)
            {
                return View(responseMessage);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO regionDto)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7139/api/regions/{regionDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8, "application/json"),
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }
    }
}
