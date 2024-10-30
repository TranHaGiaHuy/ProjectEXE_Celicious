using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs;

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public ReviewsController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _context.Reviews
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    RestaurantId = r.RestaurantId,
                    Description = r.Description,
                    Image = r.Image,
                    Rating = r.Rating,
                    CreateTime = r.CreateTime
                })
                .ToListAsync();

            return Ok(reviews);
        }

        // GET: api/reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _context.Reviews
                .Where(r => r.ReviewId == id)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    RestaurantId = r.RestaurantId,
                    Description = r.Description,
                    Image = r.Image,
                    Rating = r.Rating,
                    CreateTime = r.CreateTime
                })
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostReview(ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = new Review
            {
                UserId = reviewDto.UserId,
                RestaurantId = reviewDto.RestaurantId,
                Description = reviewDto.Description,
                Image = reviewDto.Image,
                Rating = reviewDto.Rating,
                CreateTime = DateTime.UtcNow // Set create time to current time
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            reviewDto.ReviewId = review.ReviewId; // Set the ID after creation

            return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, reviewDto);
        }

        // PUT: api/reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.ReviewId)
            {
                return BadRequest();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.Description = reviewDto.Description;
            review.Image = reviewDto.Image;
            review.Rating = reviewDto.Rating;
            review.CreateTime = reviewDto.CreateTime;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // DELETE: api/reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
