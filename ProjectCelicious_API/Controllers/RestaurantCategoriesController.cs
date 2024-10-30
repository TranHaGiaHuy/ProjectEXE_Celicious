using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs;

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantCategoriesController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public RestaurantCategoriesController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantCategoryDto>>> GetRestaurantCategories()
        {
            var categories = await _context.RestaurantCategories
                .Select(rc => new RestaurantCategoryDto
                {
                    RestaurantCategoryId = rc.RestaurantCategoryId,
                    Name = rc.Name
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/RestaurantCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantCategoryDto>> GetRestaurantCategory(int id)
        {
            var restaurantCategory = await _context.RestaurantCategories
                .Select(rc => new RestaurantCategoryDto
                {
                    RestaurantCategoryId = rc.RestaurantCategoryId,
                    Name = rc.Name
                })
                .FirstOrDefaultAsync(rc => rc.RestaurantCategoryId == id);

            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return Ok(restaurantCategory);
        }

        // POST: api/RestaurantCategories
        [HttpPost]
        public async Task<ActionResult<RestaurantCategoryDto>> PostRestaurantCategory(RestaurantCategoryDto restaurantCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurantCategory = new RestaurantCategory
            {
                Name = restaurantCategoryDto.Name
            };

            _context.RestaurantCategories.Add(restaurantCategory);
            await _context.SaveChangesAsync();

            restaurantCategoryDto.RestaurantCategoryId = restaurantCategory.RestaurantCategoryId; // Set ID after creation

            return CreatedAtAction(nameof(GetRestaurantCategory), new { id = restaurantCategory.RestaurantCategoryId }, restaurantCategoryDto);
        }

        // PUT: api/RestaurantCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantCategory(int id, RestaurantCategoryDto restaurantCategoryDto)
        {
            if (id != restaurantCategoryDto.RestaurantCategoryId)
            {
                return BadRequest();
            }

            var restaurantCategory = await _context.RestaurantCategories.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            restaurantCategory.Name = restaurantCategoryDto.Name;

            _context.Entry(restaurantCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/RestaurantCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantCategory(int id)
        {
            var restaurantCategory = await _context.RestaurantCategories.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            _context.RestaurantCategories.Remove(restaurantCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantCategoryExists(int id)
        {
            return _context.RestaurantCategories.Any(e => e.RestaurantCategoryId == id);
        }
    }
}
