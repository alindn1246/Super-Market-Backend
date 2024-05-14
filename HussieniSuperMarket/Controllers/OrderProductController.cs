using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HussieniSuperMarket.Data;
using HussieniSuperMarket.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HussieniSuperMarket.Dtos.OrderProductDtos;
using HussieniSuperMarket.Dtos.OrderDtos;

namespace HussieniSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public OrderProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        // GET: api/OrderProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct(int id)
        {
            var orderProduct = await _context.OrderProducts.Include(c=>c.Product).FirstOrDefaultAsync(op => op.Id == id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return orderProduct;
        }

        [HttpGet("GetByOrderId/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProductsByOrderId(int orderId)
        {
            var orderProducts = await _context.OrderProducts
                                                .Where(op => op.OrderId == orderId)
                                                .Include(op => op.Product)
                                                .ToListAsync();

            if (orderProducts == null || orderProducts.Count == 0)
            {
                return NotFound();
            }

            return orderProducts;
        }

        [HttpGet("TopProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetTopProductsSold()
        {
            var topProducts = await _context.OrderProducts
                                            .GroupBy(op => op.ProductId)
                                            .Select(g => new { ProductId = g.Key, TotalSales = g.Count() })
                                            .OrderByDescending(x => x.TotalSales)
                                            .Take(10)
                                            .ToListAsync();

            // Retrieve the actual Product entities for the top products
            var products = await _context.Products.Where(p => topProducts.Select(tp => tp.ProductId).Contains(p.Id)).ToListAsync();

            return Ok(products);
        }

        // POST: api/OrderProduct
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct([FromBody] CreateOrderProductDto createOrderProductDto)
        {
            OrderProduct OrderProductModel = _mapper.Map<OrderProduct>(createOrderProductDto);
            _context.OrderProducts.Add(OrderProductModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderProduct), new { id = OrderProductModel.Id }, OrderProductModel);
        }

        [HttpGet("GetTop10Products")]
        public List<ProductSalesDto> GetTop10Products()
        {
            var topProducts = _context.OrderProducts
                .GroupBy(op => op.ProductId)
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Key,
                    Product= g.First().Product,
                    TotalSalesAmount = g.Sum(op => op.Quantity * op.Price)
                })
                .OrderByDescending(ps => ps.TotalSalesAmount)
                .Take(10)
                .ToList();

            return topProducts;
        }
    

    // PUT: api/OrderProduct/5
    [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct(int id, OrderProduct orderProduct)
        {
            if (id != orderProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
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

        // DELETE: api/OrderProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(int id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProductExists(int id)
        {
            return _context.OrderProducts.Any(e => e.Id == id);
        }
    }
}
