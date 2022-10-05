using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Restaurant;

namespace RestaurantRaterMVC.Controllers
{
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantService _service;
        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<RestaurantListItem> restaurants = await _service.GetAllRestaurantsAsync();
            return View(restaurants);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.CreateRestaurantAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            RestaurantDetail restaurant = await _service.GetRestaurantByIdAsync(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));

            RestaurantEdit restaurantEdit = new RestaurantEdit()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location
            };

            return View(restaurantEdit);
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RestaurantEdit model)
        {
            if (!ModelState.IsValid) return View(ModelState);

            bool hasUpdated = await _service.UpdateRestaurantAsync(model);

            if (!hasUpdated) return View(model);
            return RedirectToAction(nameof(Details), new { id = model.Id});
        }

        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            RestaurantDetail restaurant = await _service.GetRestaurantByIdAsync(id);
            if (restaurant == null) return RedirectToAction(nameof(Index));

            return View(restaurant);
        }

    }
}