using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Dto;
using WEB_API.Interfaces;

namespace WEB_API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public TestController(IShipmentRepository shipmentRepository, IMapper mapper) {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetALl()
        {
            try
            {
                var shipmentList = _mapper.Map<List<ShipmentDto>>(_shipmentRepository.GetShipments());
                return Ok(shipmentList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("filter")]
        public IActionResult FilterShipment(
            [FromQuery] string? name,
            [FromQuery] string? area,
            [FromQuery] int? shipment_status
        )
        {
            try
            {
                var shipmentList = _shipmentRepository.GetShipments().AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    shipmentList = shipmentList.Where(s => s.Customer_Name.Contains(name));
                };

                if (!string.IsNullOrEmpty(area))
                {
                    shipmentList = shipmentList.Where(s => s.Customer_Address.Contains(area));
                }

                if (shipment_status.HasValue)
                {
                    shipmentList = shipmentList.Where(s => s.Shipment_Status_Id == shipment_status);
                }

                var filterShipment = shipmentList.Select(shipment => new
                {
                    id = shipment.Id,
                    infor = new
                    {
                        shipment.Customer_Name,
                        shipment.Customer_Phone,
                        shipment.Customer_Address,
                        shipment.Estimated_Arrival_Date,
                        shipment.Estimated_Delivery_Date,
                        shipment.Actual_Arrival_Date,
                        shipment.Actual_Delivery_Date
                    },
                    detail = new
                    {
                        shipment.Id,
                        shipment.Shipment_Detail.Packaging_Type,
                        shipment.Shipment_Detail.Quantity,
                        shipment.Shipment_Detail.Weight,
                    },
                    status = shipment.Shipment_Status.Name
                });

                return Ok(filterShipment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
