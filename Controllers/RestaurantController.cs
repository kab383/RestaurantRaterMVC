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
    }
}