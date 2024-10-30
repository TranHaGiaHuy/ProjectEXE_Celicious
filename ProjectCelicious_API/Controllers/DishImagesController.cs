using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs;

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishImagesController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public DishImagesController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/dishimages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishImageDto>>> GetDishImages()
        {
            var dishImages = await _context.DishImages
                .Select(di => new DishImageDto
                {
                    DishImageID = di.DishImageID,
                    ImagePath = di.ImagePath,
                    DishId = di.DishId
                })
                .ToListAsync();

            return Ok(dishImages);
        }

        // GET: api/dishimages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DishImageDto>> GetDishImage(int id)
        {
            var dishImage = await _context.DishImages
                .Where(di => di.DishImageID == id)
                .Select(di => new DishImageDto
                {
                    DishImageID = di.DishImageID,
                    ImagePath = di.ImagePath,
                    DishId = di.DishId
                })
                .FirstOrDefaultAsync();

            if (dishImage == null)
            {
                return NotFound();
            }

            return Ok(dishImage);
        }

        // POST: api/dishimages
        [HttpPost]
        public async Task<ActionResult<DishImageDto>> PostDishImage(DishImageDto dishImageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dishImage = new DishImage
            {
                ImagePath = dishImageDto.ImagePath,
                DishId = dishImageDto.DishId
            };

            _context.DishImages.Add(dishImage);
            await _context.SaveChangesAsync();

            dishImageDto.DishImageID = dishImage.DishImageID; // Set the ID after creation

            return CreatedAtAction(nameof(GetDishImage), new { id = dishImage.DishImageID }, dishImageDto);
        }

        // PUT: api/dishimages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishImage(int id, DishImageDto dishImageDto)
        {
            if (id != dishImageDto.DishImageID)
            {
                return BadRequest();
            }

            var dishImage = await _context.DishImages.FindAsync(id);
            if (dishImage == null)
            {
                return NotFound();
            }

            dishImage.ImagePath = dishImageDto.ImagePath;
            dishImage.DishId = dishImageDto.DishId;

            _context.Entry(dishImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishImageExists(id))
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

        // DELETE: api/dishimages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishImage(int id)
        {
            var dishImage = await _context.DishImages.FindAsync(id);
            if (dishImage == null)
            {
                return NotFound();
            }

            _context.DishImages.Remove(dishImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishImageExists(int id)
        {
            return _context.DishImages.Any(e => e.DishImageID == id);
        }
    }
}
