using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCelicious_API.DTOs;

namespace ProjectCelicious_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantAddressesController : ControllerBase
    {
        private readonly CeliciousContext _context;

        public RestaurantAddressesController(CeliciousContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantAddressDto>>> GetRestaurantAddresses()
        {
            var addresses = await _context.RestaurantAddresses
                .Select(ra => new RestaurantAddressDto
                {
                    RestaurantAddressId = ra.RestaurantAddressId,
                    HouseNumber = ra.HouseNumber,
                    Street = ra.Street,
                    District = ra.District,
                    Province = ra.Province,
                    GoogleMapLink = ra.GoogleMapLink
                })
                .ToListAsync();

            return Ok(addresses);
        }

        // GET: api/RestaurantAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantAddressDto>> GetRestaurantAddress(int id)
        {
            var restaurantAddress = await _context.RestaurantAddresses
                .Select(ra => new RestaurantAddressDto
                {
                    RestaurantAddressId = ra.RestaurantAddressId,
                    HouseNumber = ra.HouseNumber,
                    Street = ra.Street,
                    District = ra.District,
                    Province = ra.Province,
                    GoogleMapLink = ra.GoogleMapLink
                })
                .FirstOrDefaultAsync(ra => ra.RestaurantAddressId == id);

            if (restaurantAddress == null)
            {
                return NotFound();
            }

            return Ok(restaurantAddress);
        }

        // POST: api/RestaurantAddresses
        [HttpPost]
        public async Task<ActionResult<RestaurantAddressDto>> PostRestaurantAddress(RestaurantAddressDto restaurantAddressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurantAddress = new RestaurantAddress
            {
                HouseNumber = restaurantAddressDto.HouseNumber,
                Street = restaurantAddressDto.Street,
                District = restaurantAddressDto.District,
                Province = restaurantAddressDto.Province,
                GoogleMapLink = restaurantAddressDto.GoogleMapLink
            };

            _context.RestaurantAddresses.Add(restaurantAddress);
            await _context.SaveChangesAsync();

            restaurantAddressDto.RestaurantAddressId = restaurantAddress.RestaurantAddressId; // Set ID after creation

            return CreatedAtAction(nameof(GetRestaurantAddress), new { id = restaurantAddress.RestaurantAddressId }, restaurantAddressDto);
        }

        // PUT: api/RestaurantAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantAddress(int id, RestaurantAddressDto restaurantAddressDto)
        {
            if (id != restaurantAddressDto.RestaurantAddressId)
            {
                return BadRequest();
            }

            var restaurantAddress = await _context.RestaurantAddresses.FindAsync(id);
            if (restaurantAddress == null)
            {
                return NotFound();
            }

            restaurantAddress.HouseNumber = restaurantAddressDto.HouseNumber;
            restaurantAddress.Street = restaurantAddressDto.Street;
            restaurantAddress.District = restaurantAddressDto.District;
            restaurantAddress.Province = restaurantAddressDto.Province;
            restaurantAddress.GoogleMapLink = restaurantAddressDto.GoogleMapLink;

            _context.Entry(restaurantAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantAddressExists(id))
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

        // DELETE: api/RestaurantAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantAddress(int id)
        {
            var restaurantAddress = await _context.RestaurantAddresses.FindAsync(id);
            if (restaurantAddress == null)
            {
                return NotFound();
            }

            _context.RestaurantAddresses.Remove(restaurantAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantAddressExists(int id)
        {
            return _context.RestaurantAddresses.Any(e => e.RestaurantAddressId == id);
        }
    }
}
