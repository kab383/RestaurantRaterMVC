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

        public Task<RestaurantDetail> GetRestaurantByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRestaurantAsync(RestaurantEdit model)
        {
            throw new NotImplementedException();
        }

                public Task<bool> DeleteRestaurantAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}