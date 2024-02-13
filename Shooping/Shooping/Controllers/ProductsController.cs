using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shooping.models;
using Shooping.Models;

namespace Shooping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("buy")]
        public IActionResult BuyProduct(CartItem cartItem)
        {



      
        
          
            _dbContext.CartItems.Add(cartItem);
            _dbContext.SaveChanges();



            return Ok(); // Purchase successful
        }
        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
   

        // POST: api/product
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return product;
        }
    }


}
