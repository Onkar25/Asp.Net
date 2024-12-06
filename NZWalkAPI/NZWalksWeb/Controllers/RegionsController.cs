using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalksWeb.Models;
using NZWalksWeb.Models.DTO.Region;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalksWeb.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClient;

        public RegionsController(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var response = new List<RegionDTO>();
            try
            {
                var client = httpClient.CreateClient();

                var httpResponse = await client.GetAsync("https://localhost:7281/api/regions");

                httpResponse.EnsureSuccessStatusCode();

                response.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                //ViewBag.Response = stringResponse;
            }
            catch (Exception ex)
            {

            }

            return View(response);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClient.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
                RequestUri = new Uri("https://localhost:7281/api/regions")
            };

            var httpResponse = await client.SendAsync(httpRequestMessage);
            httpResponse.EnsureSuccessStatusCode();

            var response = httpResponse.Content.ReadFromJsonAsync<RegionDTO>();
            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClient.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7281/api/regions/{id.ToString()}");
            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO region)
        {
            var client = httpClient.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                Content = new StringContent(JsonSerializer.Serialize(region), Encoding.UTF8, "application/json"),
                RequestUri = new Uri($"https://localhost:7281/api/regions/{region.Id}")
            };

            var httpResponse = await client.SendAsync(httpRequestMessage);
            httpResponse.EnsureSuccessStatusCode();

            var response = httpResponse.Content.ReadFromJsonAsync<RegionDTO>();
            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO region)
        {
            try
            {
                var client = httpClient.CreateClient();

                var httpResponse = await client.DeleteAsync($"https://localhost:7281/api/regions/{region.Id}");
                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");

            }
            catch (Exception)
            {

            }
            return View("Edit");
        }
    }
}

