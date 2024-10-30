using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs; // Ensure this namespace is included

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishCategoriesController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public DishCategoriesController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/dishcategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishCategoryDto>>> GetDishCategories()
        {
            var categories = await _context.DishCategories
                .Select(dc => new DishCategoryDto
                {
                    DishCategoryId = dc.DishCategoryId,
                    Name = dc.Name
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/dishcategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DishCategoryDto>> GetDishCategory(int id)
        {
            var dishCategory = await _context.DishCategories
                .Select(dc => new DishCategoryDto
                {
                    DishCategoryId = dc.DishCategoryId,
                    Name = dc.Name
                })
                .FirstOrDefaultAsync(dc => dc.DishCategoryId == id);

            if (dishCategory == null)
            {
                return NotFound();
            }

            return Ok(dishCategory);
        }

        // POST: api/dishcategories
        [HttpPost]
        public async Task<ActionResult<DishCategoryDto>> PostDishCategory(DishCategoryDto dishCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dishCategory = new DishCategory
            {
                Name = dishCategoryDto.Name
            };

            _context.DishCategories.Add(dishCategory);
            await _context.SaveChangesAsync();

            dishCategoryDto.DishCategoryId = dishCategory.DishCategoryId; // Set ID after creation

            return CreatedAtAction(nameof(GetDishCategory), new { id = dishCategory.DishCategoryId }, dishCategoryDto);
        }

        // PUT: api/dishcategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishCategory(int id, DishCategoryDto dishCategoryDto)
        {
            if (id != dishCategoryDto.DishCategoryId)
            {
                return BadRequest();
            }

            var dishCategory = await _context.DishCategories.FindAsync(id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            dishCategory.Name = dishCategoryDto.Name; // Update properties as needed

            _context.Entry(dishCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishCategoryExists(id))
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

        // DELETE: api/dishcategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishCategory(int id)
        {
            var dishCategory = await _context.DishCategories.FindAsync(id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            _context.DishCategories.Remove(dishCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishCategoryExists(int id)
        {
            return _context.DishCategories.Any(e => e.DishCategoryId == id);
        }
    }
}
