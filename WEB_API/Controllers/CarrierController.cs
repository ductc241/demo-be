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

        [HttpPut("update/{id}")]
        public IActionResult UpdateOne(int id, [FromBody] CarrierDto carrierUpdate)
        {
            try
            {
                if (carrierUpdate == null || !ModelState.IsValid) return BadRequest();

                var _carrierUpdate = _context.Shipping_Carrier.Where(s => s.Id == id).FirstOrDefault();

                if (_carrierUpdate == null) return NotFound("Not found shipment");

                _carrierUpdate.Name = carrierUpdate.Name;
                _carrierUpdate.Contact_Person = carrierUpdate.Contact_Person;
                _carrierUpdate.Phone_Number = carrierUpdate.Phone_Number;
                _carrierUpdate.Email = carrierUpdate.Email;
                _carrierUpdate.Note = carrierUpdate.Note;
                _carrierUpdate.Status = carrierUpdate.Status;

                _context.Shipping_Carrier.Update(_carrierUpdate);
                _context.SaveChanges();

                return Ok("Update Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
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
