using Microsoft.AspNetCore.Mvc;
using WEB_API.Dto;
using WEB_API.Model;

namespace WEB_API.Controllers
{
    [Route("api/tracking_status")]
    [ApiController]
    public class TrackingStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public TrackingStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var statusList = _context.Tracking_Status.ToList();
                return Ok(statusList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateOne([FromBody] TrackingStatusDto statusCreate)
        {
            try
            {
                var existingStatus = _context.Tracking_Status.Where(ts => ts.Name == statusCreate.Name.ToUpper()).FirstOrDefault();

                if (existingStatus != null) 
                    return BadRequest("Status already exists");

                Tracking_Status _statusCreate = new Tracking_Status()
                {
                    Name = statusCreate.Name.ToUpper(),
                    Status = statusCreate.Status,
                };

                _context.Tracking_Status.Add(_statusCreate);
                _context.SaveChanges();

                return Created("Create success", _statusCreate);

            }
            catch(Exception ex)
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
