using Microsoft.AspNetCore.Mvc;
using Shooping.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shooping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CheckoutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/checkout
        [HttpPost]
        public ActionResult<Order> Checkout(Order order)
        {
            try
            {
                // Ensure that the order has products
                if (order.Products == null || !order.Products.Any())
                {
                    return BadRequest("The order must contain at least one product.");
                }

             
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the order: {ex.Message}");
            }
        }
    }
}
