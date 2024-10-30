using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
  
        public class RestaurantDAO
        {
            private readonly CeliciousContext _context;

            public RestaurantDAO(CeliciousContext context)
            {
                _context = context;
            }

            // Get all restaurants with related data
            public async Task<List<Restaurant>> GetAllRestaurantsAsync()
            {
                return await _context.Restaurants
                    .Include(r => r.RestaurantAddress)
                    .Include(r => r.RestaurantCategory)
                    .Include(r => r.Dishes)
                    .Include(r => r.Review)
                    .Include(r => r.RestaurantImages)
                    .ToListAsync();
            }

            // Get a restaurant by ID with related data
            public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
            {
                return await _context.Restaurants
                    .Include(r => r.RestaurantAddress)
                    .Include(r => r.RestaurantCategory)
                    .Include(r => r.Dishes)
                    .Include(r => r.Review)
                    .Include(r => r.RestaurantImages)
                    .FirstOrDefaultAsync(r => r.RestaurantId == id);
            }

            // Add a new restaurant
            public async Task AddRestaurantAsync(Restaurant restaurant)
            {
                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();
            }

            // Update an existing restaurant
            public async Task UpdateRestaurantAsync(Restaurant restaurant)
            {
                _context.Entry(restaurant).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            // Delete a restaurant by ID
            public async Task DeleteRestaurantAsync(int id)
            {
                var restaurant = await _context.Restaurants.FindAsync(id);
                if (restaurant != null)
                {
                    _context.Restaurants.Remove(restaurant);
                    await _context.SaveChangesAsync();
                }
            }
        public async Task<List<Restaurant>> SearchAsync(string? name)
        {
            var query = _context.Restaurants.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.RestaurantName.Contains(name));
            }

            return await query
                .Include(r => r.RestaurantAddress)
                .Include(r => r.RestaurantCategory)
                .ToListAsync();
        }

    }
}

