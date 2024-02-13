using Microsoft.AspNetCore.Mvc;
using Shooping.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Shooping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCart()
        {
            return await _context.CartItems.ToListAsync();
        }

        // GET: api/cart/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetAllCartItems()
        {
            return await _context.CartItems.ToListAsync();
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(CartItem item)
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/cart/remove/{id}
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var itemToRemove = await _context.CartItems.FindAsync(id);
            if (itemToRemove == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(itemToRemove);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/cart/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, CartItem updatedItem)
        {
            if (id != updatedItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
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

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.Id == id);
        }
    }
}
