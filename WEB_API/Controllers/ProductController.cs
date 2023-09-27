using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Dto;
using WEB_API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult SearchProducts([FromQuery] string? search)
        {
            try
            {
                var productList = _context.Products.AsQueryable();

                if (!String.IsNullOrEmpty(search))
                {
                    productList = productList.Where(p => p.Name.Contains(search));
                }

                return Ok(productList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOneById(int id)
        {
            try
            {
                var product = _context.Products.Where(s => s.Id == id).AsNoTracking().FirstOrDefault();

                if (product == null) return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost] 
        public IActionResult CreateOne([FromBody] Product_Create productCreate)
        {
            try
            {
                var existingProduct = _context.Products.Where(p => p.Name == productCreate.Name).FirstOrDefault();

                if (existingProduct != null)
                    return BadRequest("Product name already exists");

                Product _productCreate = new Product()
                {
                    Name = productCreate.Name,
                    Price = productCreate.Price,
                    Quantity = productCreate.Quantity,
                };

                _context.Products.Add(_productCreate);
                _context.SaveChanges();

                return Created("Create success", _productCreate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateOne(int id, [FromBody] Product_Create productUpdate)
        {
            try
            {
                if (productUpdate == null || !ModelState.IsValid) return BadRequest();

                var _productUpdate = _context.Products.Where(s => s.Id == id).FirstOrDefault();

                if (_productUpdate == null) return NotFound("Not found shipment");

                _productUpdate.Name = productUpdate.Name;
                _productUpdate.Price = productUpdate.Price;
                _productUpdate.Quantity= productUpdate.Quantity;

                _context.Products.Update(_productUpdate);
                _context.SaveChanges();

                return Ok("Update Success");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
