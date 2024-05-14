using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HussieniSuperMarket.Data;
using HussieniSuperMarket.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HussieniSuperMarket.Dtos.OrderDtos;
using HussieniSuperMarket.Dtos.ProductDtos;

namespace HussieniSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public OrderController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.Include(c=>c.User).Include(c=>c.OrderProducts).ToListAsync();
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("GetTotalAmountOfOrders")]
        public decimal GetTotalAmountOfOrders()
        {
            decimal totalAmount = _context.Orders.Sum(order => order.TotalAmount);
            return totalAmount;
        }
        [HttpGet("GetNumberOfOrders")]
        public int GetNumberOfOrders()
        {
            int numberOfOrders = _context.Orders.Count();
            return numberOfOrders;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order OrderModel = _mapper.Map<Order>(createOrderDto);

            _context.Orders.Add(OrderModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = OrderModel.Id }, OrderModel);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // PATCH: api/Order/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateTotalAmount updateTotalAmount)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the total amount
            existingOrder.TotalAmount = updateTotalAmount.TotalAmount;

            // Update the order entity in the database
            _context.Entry(existingOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict);
                }
            }

            return NoContent();
        }



        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
