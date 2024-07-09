using FrontEndMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FrontEndMVC.Controllers
{
    public class FlightController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FlightController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task <IActionResult> Index()
        {
            List<Flight> objects = new List<Flight>();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5135/api/Flights/GetFlights");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                objects = JsonConvert.DeserializeObject<List<Flight>>(content);
            }
            return View(objects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight Flight)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(Flight);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5135/api/Flights/AddFlight", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
