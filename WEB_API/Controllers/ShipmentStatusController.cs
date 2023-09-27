using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Dto;
using WEB_API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public ShipmentStatusController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var statusList = _context.Shipment_Status.AsNoTracking().ToList();
                return Ok(statusList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateOne([FromBody] ShipmentStatusDto statusCreate)
        {
            try
            {
                var existingStatus = _context.Shipment_Status.Where(ts => ts.Name == statusCreate.Name.ToUpper()).FirstOrDefault();

                if (existingStatus != null)
                    return BadRequest("Status already exists");

                Shipment_Status _statusCreate = new Shipment_Status()
                {
                    Name = statusCreate.Name.ToUpper(),
                    Status = statusCreate.Status,
                };


                _context.Shipment_Status.Add(_statusCreate);
                _context.SaveChanges();

                return Created("Create success", _statusCreate);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
