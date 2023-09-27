using Microsoft.AspNetCore.Mvc;
using WEB_API.Dto;
using WEB_API.Model;

namespace WEB_API.Controllers
{
    [Route("api/tracking")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly DataContext _context;

        public TrackingController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTrackingByShipmentId([FromQuery] int? shipment_id)
        {
            try
            {
                if(shipment_id == null)
                    return BadRequest("parameter shipment_id is missing");

                var existingShipment = _context.Shipments.Where(s => s.Id == shipment_id).FirstOrDefault();

                if (existingShipment == null)
                    return NotFound("Not found shipment");

                var trackingHistory = _context.Trackings.Where(t => t.Shipment_Id == shipment_id).ToList();

                return Ok(trackingHistory);
            }
            catch(Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTracking([FromBody] TrackingDto tracking)
        {
            try
            {
                if (tracking == null || !ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingShipment = _context.Shipments.Where(s => s.Id == tracking.Shipment_Id).FirstOrDefault();

                if (existingShipment == null)
                    return NotFound("Not foud shipment by this ID");

                Tracking _tracking = new Tracking()
                {
                    From_Location = tracking.From_Location,
                    To_Location = tracking.To_Location,
                    Note = tracking.Note,
                    Shipment_Id = tracking.Shipment_Id,
                    Tracking_Status_Id = tracking.Tracking_Status_Id,
                };

                _context.Trackings.Add(_tracking);
                _context.SaveChanges();

                return Ok("Create Tracking Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateOne(int id, [FromBody] TrackingDto trackingUpdate)
        {
            try
            {
                var existingTracking = _context.Trackings.Where(t => t.Id == id).FirstOrDefault();

                if (existingTracking == null) 
                    return NotFound("Not fount tracking");

                existingTracking.From_Location = trackingUpdate.From_Location;
                existingTracking.To_Location = trackingUpdate.To_Location;
                existingTracking.Note = trackingUpdate.Note;
                existingTracking.Tracking_Status_Id = trackingUpdate.Tracking_Status_Id;

                _context.Trackings.Update(existingTracking);
                _context.SaveChanges();

                return Ok("Update success");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
