using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs;

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantImagesController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public RestaurantImagesController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/restaurantimages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantImageDto>>> GetRestaurantImages()
        {
            var images = await _context.RestaurantImages
                .Include(ri => ri.Restaurant)
                .Select(ri => new RestaurantImageDto
                {
                    ResImageID = ri.ResImageID,
                    RestaurantId = ri.RestaurantId,
                    ImagePath = ri.ImagePath
                })
                .ToListAsync();

            return Ok(images);
        }

        // GET: api/restaurantimages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantImageDto>> GetRestaurantImage(int id)
        {
            var restaurantImage = await _context.RestaurantImages
                .Include(ri => ri.Restaurant)
                .Select(ri => new RestaurantImageDto
                {
                    ResImageID = ri.ResImageID,
                    RestaurantId = ri.RestaurantId,
                    ImagePath = ri.ImagePath
                })
                .FirstOrDefaultAsync(ri => ri.ResImageID == id);

            if (restaurantImage == null)
            {
                return NotFound();
            }

            return Ok(restaurantImage);
        }

        // POST: api/restaurantimages
        [HttpPost]
        public async Task<ActionResult<RestaurantImageDto>> PostRestaurantImage(RestaurantImageDto restaurantImageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurantImage = new RestaurantImage
            {
                RestaurantId = restaurantImageDto.RestaurantId,
                ImagePath = restaurantImageDto.ImagePath
            };

            _context.RestaurantImages.Add(restaurantImage);
            await _context.SaveChangesAsync();

            restaurantImageDto.ResImageID = restaurantImage.ResImageID; // Set ID after creation

            return CreatedAtAction(nameof(GetRestaurantImage), new { id = restaurantImage.ResImageID }, restaurantImageDto);
        }

        // PUT: api/restaurantimages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantImage(int id, RestaurantImageDto restaurantImageDto)
        {
            if (id != restaurantImageDto.ResImageID)
            {
                return BadRequest();
            }

            var restaurantImage = await _context.RestaurantImages.FindAsync(id);
            if (restaurantImage == null)
            {
                return NotFound();
            }

            restaurantImage.RestaurantId = restaurantImageDto.RestaurantId;
            restaurantImage.ImagePath = restaurantImageDto.ImagePath;

            _context.Entry(restaurantImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantImageExists(id))
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

        // DELETE: api/restaurantimages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantImage(int id)
        {
            var restaurantImage = await _context.RestaurantImages.FindAsync(id);
            if (restaurantImage == null)
            {
                return NotFound();
            }

            _context.RestaurantImages.Remove(restaurantImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantImageExists(int id)
        {
            return _context.RestaurantImages.Any(e => e.ResImageID == id);
        }
    }
}
