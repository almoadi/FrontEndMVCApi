using FrontEndMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FrontEndMVC.Controllers
{
    public class PassengerController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public PassengerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]

        public IActionResult Create(int id)
        {
            var model = new ModelView
            {
                Flight = new Flight { Id = id },
                Passenger = new Passenger ()
              
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ModelView model, int id)
        {
      
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model.Passenger);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"http://localhost:5135/api/Passengers/AddPassenger/{id}", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Flight");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Passenger passenger = new Passenger();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5135/api/Passengers/GetPassengerById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                passenger = JsonConvert.DeserializeObject<Passenger>(content);
            }

            return View(passenger);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Passenger passenger ,int id)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(passenger);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:5135/api/Passengers/UpdatePassenger/" + id, data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Flight");
            }

            return View(passenger);
        }

    
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://localhost:5135/api/Passengers/DeletePassenger/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Flight");
            }

            return RedirectToAction("Index", "Flight");
        }
    }
}
