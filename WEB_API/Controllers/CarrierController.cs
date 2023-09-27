using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Dto;
using WEB_API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API.Controllers
{
    [Route("api/carrier")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly DataContext _context;

        public CarrierController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var shipmentList = _context.Shipping_Carrier.AsNoTracking().ToList();
                return Ok(shipmentList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOne([FromBody] CarrierDto carrierCreate)
        {
            try
            {   
                if(carrierCreate == null || !ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingCarries = _context.Shipping_Carrier.Where(sc => sc.Name == carrierCreate.Name.ToUpper()).FirstOrDefault();

                if (existingCarries != null)
                    return BadRequest("Carrier already exists");

                Shipping_Carrier _carrierCreate = new Shipping_Carrier()
                {
                    Name = carrierCreate.Name.ToUpper(),
                    Contact_Person = carrierCreate.Contact_Person,
                    Phone_Number = carrierCreate.Phone_Number,
                    Email = carrierCreate.Email,
                    Note = carrierCreate.Note,
                    Status  = carrierCreate.Status,
                };

                _context.Shipping_Carrier.Add(_carrierCreate);
                _context.SaveChanges();

                return Created("Create success", _carrierCreate);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOne(int id)
        {
            try
            {
                var existingCarier = _context.Shipping_Carrier.Find(id);

                if (existingCarier == null)
                {
                    return NotFound("Not found shipment");
                }

                _context.Shipping_Carrier.Remove(existingCarier);
                _context.SaveChanges();

                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
