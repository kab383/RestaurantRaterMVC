using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRestaurantAsync(RestaurantCreate model)
        {
            RestaurantEntity restaurant = new RestaurantEntity()
            {
                Name = model.Name,
                Location = model.Location,
            };

            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<List<RestaurantListItem>> GetAllRestaurantsAsync()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score,
            }).ToListAsync();
            
            return restaurants;
        }

        public async Task<RestaurantDetail> GetRestaurantByIdAsync(int id)
        {
            RestaurantEntity restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null) return null;

            RestaurantDetail restaurantDetail = new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Score = restaurant.Score,
                AverageFoodScore = restaurant.AverageFoodScore,
                AverageCleanlinessScore = restaurant.AverageCleanlinessScore,
                AverageAtmosphereScore = restaurant.AverageAtmosphereScore
            };
            return restaurantDetail;
        }

        public async Task<bool> UpdateRestaurantAsync(RestaurantEdit model)
        {
            RestaurantEntity restaurant = await _context.Restaurants.FindAsync(model.Id);
            if (restaurant == null) return false;

            restaurant.Name = model.Name;
            restaurant.Location = model.Location;

            return await _context.SaveChangesAsync() == 1;
        }

                public Task<bool> DeleteRestaurantAsync(int id)
        {
            throw new NotImplementedException();
        }
}
