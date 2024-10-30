using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs; // Make sure to include this namespace

[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly CeliciousContext _context;

    public DishesController(CeliciousContext context)
    {
        _context = context;
    }

    // GET: api/dishes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes()
    {
        var dishes = await _context.Dishes
            .Include(d => d.DishCategory)
            .Include(d => d.Restaurant)
            .Select(d => new DishDto
            {
                DishId = d.DishId,
                DishName = d.DishName,
                Description = d.Description,
                Price = d.Price,
                LinkToShoppe = d.LinkToShoppe,
                Image = d.Image,
                DishCategoryId = d.DishCategoryId,
                DishCategoryName = d.DishCategory.Name, // Fetch category name
                RestaurantId = d.RestaurantId
            })
            .ToListAsync();

        return Ok(dishes);
    }

    // GET: api/dishes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DishDto>> GetDish(int id)
    {
        var dish = await _context.Dishes
            .Include(d => d.DishCategory)
            .Include(d => d.Restaurant)
            .Select(d => new DishDto
            {
                DishId = d.DishId,
                DishName = d.DishName,
                Description = d.Description,
                Price = d.Price,
                LinkToShoppe = d.LinkToShoppe,
                Image = d.Image,
                DishCategoryId = d.DishCategoryId,
                DishCategoryName = d.DishCategory.Name, // Fetch category name
                RestaurantId = d.RestaurantId
            })
            .FirstOrDefaultAsync(d => d.DishId == id);

        if (dish == null)
        {
            return NotFound();
        }

        return Ok(dish);
    }

    // POST: api/dishes
    [HttpPost]
    public async Task<ActionResult<DishDto>> PostDish(DishDto dishDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var dish = new Dish
        {
            DishName = dishDto.DishName,
            Description = dishDto.Description,
            Price = dishDto.Price,
            LinkToShoppe = dishDto.LinkToShoppe,
            Image = dishDto.Image,
            DishCategoryId = dishDto.DishCategoryId,
            RestaurantId = dishDto.RestaurantId
        };

        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();

        // Include DishCategoryName in the response
        dishDto.DishId = dish.DishId; // Set the ID after creation
        dishDto.DishCategoryName = (await _context.DishCategories.FindAsync(dish.DishCategoryId))?.Name;

        return CreatedAtAction(nameof(GetDish), new { id = dish.DishId }, dishDto);
    }

    // PUT: api/dishes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDish(int id, DishDto dishDto)
    {
        if (id != dishDto.DishId)
        {
            return BadRequest();
        }

        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        dish.DishName = dishDto.DishName;
        dish.Description = dishDto.Description;
        dish.Price = dishDto.Price;
        dish.LinkToShoppe = dishDto.LinkToShoppe;
        dish.Image = dishDto.Image;
        dish.DishCategoryId = dishDto.DishCategoryId;

        _context.Entry(dish).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DishExists(id))
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

    // DELETE: api/dishes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDish(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DishExists(int id)
    {
        return _context.Dishes.Any(e => e.DishId == id);
    }
}
