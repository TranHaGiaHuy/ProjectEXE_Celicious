using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs;
using BusinessObjects.Models;
using BusinessObjects.DataContext;

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public RestaurantController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var restaurants = await _context.Restaurants
                .Include(r => r.RestaurantAddress)
                .Include(r => r.RestaurantCategory)
                .Select(r => new RestaurantDto
                {
                    RestaurantId = r.RestaurantId,
                    RestaurantName = r.RestaurantName,
                    Phone = r.Phone,
                    Description = r.Description,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    Logo = r.Logo,
                    Background = r.Background,
                    Status = r.Status,
                    RestaurantCategoryId = r.RestaurantCategoryId,
                    CategoryName = r.RestaurantCategory.Name,
                    Address = r.RestaurantAddress.HouseNumber + " " + r.RestaurantAddress.Street,
                    District = r.RestaurantAddress.District,
                    Province = r.RestaurantAddress.Province
                })
                .ToListAsync();

            return Ok(restaurants);
        }

        // GET: api/restaurant/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.RestaurantAddress)
                .Include(r => r.RestaurantCategory)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound("Restaurant not found.");
            }

            var restaurantDto = new RestaurantDto
            {
                RestaurantId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName,
                Phone = restaurant.Phone,
                Description = restaurant.Description,
                StartTime = restaurant.StartTime,
                EndTime = restaurant.EndTime,
                Logo = restaurant.Logo,
                Background = restaurant.Background,
                Status = restaurant.Status,
                RestaurantCategoryId = restaurant.RestaurantCategoryId,
                CategoryName = restaurant.RestaurantCategory.Name,
                Address = restaurant.RestaurantAddress.HouseNumber + " " + restaurant.RestaurantAddress.Street,
                District = restaurant.RestaurantAddress.District,
                Province = restaurant.RestaurantAddress.Province
            };

            return Ok(restaurantDto);
        }

        // POST: api/restaurant
        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                RestaurantName = restaurantDto.RestaurantName,
                Phone = restaurantDto.Phone,
                Description = restaurantDto.Description,
                StartTime = restaurantDto.StartTime,
                EndTime = restaurantDto.EndTime,
                Logo = restaurantDto.Logo,
                Background = restaurantDto.Background,
                Status = restaurantDto.Status,
                RestaurantCategoryId = restaurantDto.RestaurantCategoryId,
                RestaurantAddress = new RestaurantAddress
                {
                    HouseNumber = restaurantDto.Address,
                    District = restaurantDto.District,
                    Province = restaurantDto.Province
                }
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.RestaurantId }, restaurant);
        }

        // PUT: api/restaurant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDto restaurantDto)
        {
            if (id != restaurantDto.RestaurantId)
            {
                return BadRequest("Restaurant ID mismatch.");
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.RestaurantAddress)
                .Include(r => r.RestaurantCategory)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound("Restaurant not found.");
            }

            // Cập nhật thông tin nhà hàng
            restaurant.RestaurantName = restaurantDto.RestaurantName;
            restaurant.Phone = restaurantDto.Phone;
            restaurant.Description = restaurantDto.Description;
            restaurant.StartTime = restaurantDto.StartTime;
            restaurant.EndTime = restaurantDto.EndTime;
            restaurant.Logo = restaurantDto.Logo;
            restaurant.Background = restaurantDto.Background;
            restaurant.Status = restaurantDto.Status;
            restaurant.RestaurantCategoryId = restaurantDto.RestaurantCategoryId;

            // Cập nhật thông tin địa chỉ
            if (restaurant.RestaurantAddress != null)
            {
                restaurant.RestaurantAddress.HouseNumber = restaurantDto.Address;
                restaurant.RestaurantAddress.District = restaurantDto.District;
                restaurant.RestaurantAddress.Province = restaurantDto.Province;
            }
            else
            {
                restaurant.RestaurantAddress = new RestaurantAddress
                {
                    HouseNumber = restaurantDto.Address,
                    District = restaurantDto.District,
                    Province = restaurantDto.Province
                };
            }

            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/restaurant/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound("Restaurant not found.");
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
