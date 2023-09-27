using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WEB_API.Dto;
using WEB_API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API.Controllers
{
    [Route("api/shipment")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly DataContext _context;

        public ShipmentController(DataContext context) {
            _context = context;
        }

        public class Result
        {
            public Result()
            {
                Status = true;
            }
            public bool Status { get; set; }
            public string Message { get; set; }

            public object Object { get; set; }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //var shipmentList = _context.Shipments.AsNoTracking().Select(shipment => new
            //{
            //    id = shipment.Id,
            //    infor = new
            //    {
            //        shipment.Customer_Name,
            //        shipment.Customer_Phone,
            //        shipment.Customer_Address,
            //        shipment.Shipping_Fee,
            //        shipment.Note,
            //        shipment.Estimated_Arrival_Date,
            //        shipment.Estimated_Delivery_Date,
            //        shipment.Actual_Arrival_Date,
            //        shipment.Actual_Delivery_Date
            //    },
            //    detail = new
            //    {
            //        shipment.Id,
            //        shipment.Shipment_Detail.Packaging_Type,
            //        shipment.Shipment_Detail.Quantity,
            //        shipment.Shipment_Detail.Weight,
            //        shipment.Shipment_Detail.Barcode,
            //    },
            //    status = shipment.Shipment_Status.Name,
            //    tracking = shipment.Trackings.Select(tracking => new
            //    {
            //        tracking.Id,
            //        tracking.From_Location,
            //        tracking.To_Location,
            //        tracking.Note,
            //    })
            //});

            var shipmentList = _context.Shipments.Include(s => s.Shipment_Status).ToList();

            return Ok(shipmentList);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneById(int id)
        {
            try
            {
                var shipment = _context.Shipments.Where(s => s.Id == id).AsNoTracking().FirstOrDefault();

                if (shipment == null) return NotFound();

                return Ok(shipment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                var shipmentList = _context.Shipments.AsQueryable();

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

        [HttpPost]
        public IActionResult CreateOneWithSP([FromBody] ShipmentDto shipmentCreate)
        {
            try
            {
                var sql = "EXEC SP_CreateShipment "
                    + "@customer_name, "
                    + "@customer_phone, "
                    + "@customer_address, "
                    + "@fee, "
                    + "@note, "
                    + "@estimated_delivery_date, "
                    + "@actual_delivery_date, "
                    + "@estimated_arrival_date, "
                    + "@actual_arrival_date, "
                    + "@order_id, "
                    + "@shipment_status_id";

                var _customer_name = new SqlParameter("@customer_name", SqlDbType.NVarChar, 50);
                _customer_name.Value = shipmentCreate.Customer_Name;

                var _customer_phone = new SqlParameter("@customer_phone", SqlDbType.NVarChar, 50);
                _customer_phone.Value = shipmentCreate.Customer_Phone;

                var _customer_address = new SqlParameter("@customer_address", SqlDbType.NVarChar, 255);
                _customer_address.Value = shipmentCreate.Customer_Address;

                var _fee = new SqlParameter("@fee", SqlDbType.Decimal);
                _fee.Value = shipmentCreate.Shipping_Fee;

                var _note = new SqlParameter("@note", SqlDbType.NVarChar, 255);
                _note.Value = shipmentCreate.Note;

                var _estimated_delivery_date = new SqlParameter("@estimated_delivery_date", SqlDbType.DateTime2);
                _estimated_delivery_date.Value = shipmentCreate.Estimated_Delivery_Date;

                var _actual_delivery_date = new SqlParameter("@actual_delivery_date", SqlDbType.DateTime2);
                _actual_delivery_date.Value = shipmentCreate.Actual_Delivery_Date;

                var _estimated_arrival_date = new SqlParameter("@estimated_arrival_date", SqlDbType.DateTime2);
                _estimated_arrival_date.Value = shipmentCreate.Estimated_Arrival_Date;

                var _actual_arrival_date = new SqlParameter("@actual_arrival_date", SqlDbType.DateTime2);
                _actual_arrival_date.Value = shipmentCreate.Actual_Arrival_Date;

                var _order_id = new SqlParameter("@order_id", SqlDbType.Int);
                _order_id.Value = shipmentCreate.Order_Id;

                var _shipment_status_id = new SqlParameter("@shipment_status_id", SqlDbType.Int);
                _shipment_status_id.Value = 2;

                // ExecuteSqlRaw trả về số bảng bị tác động
                //var new_shipment = _context.Database.ExecuteSqlRaw(sql,
                //    _customer_name, _customer_phone, _customer_address, _fee, _note,
                //    _estimated_delivery_date,
                //    _actual_delivery_date,
                //    _estimated_arrival_date,
                //    _actual_arrival_date,
                //    _order_id,
                //    _shipment_status_id
                //);

                var new_shipment = _context.Shipments.FromSqlRaw(sql,
                   _customer_name, _customer_phone, _customer_address, _fee, _note,
                   _estimated_delivery_date,
                   _actual_delivery_date,
                   _estimated_arrival_date,
                   _actual_arrival_date,
                   _order_id,
                   _shipment_status_id
               );

                return Created("create success", new_shipment);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("update/sp/{id}")]
        public IActionResult UpdateShipmentWithSP(int id, [FromBody] ShipmentDto shipmenUpdate)
        {
            try
            {
                var sql = "EXEC UpdateShipment "
                    + "@id, "
                    + "@customer_name, "
                    + "@customer_phone, "
                    + "@customer_address, "
                    + "@fee, "
                    + "@note, "
                    + "@estimated_delivery_date, "
                    + "@actual_delivery_date, "
                    + "@estimated_arrival_date, "
                    + "@actual_arrival_date, "
                    + "@order_id, "
                    + "@shipment_status_id";

                var _id = new SqlParameter("@id", SqlDbType.Int);
                _id.Value = id;

                var _customer_name = new SqlParameter("@customer_name", SqlDbType.NVarChar, 50);
                _customer_name.Value = shipmenUpdate.Customer_Name;

                var _customer_phone = new SqlParameter("@customer_phone", SqlDbType.NVarChar, 50);
                _customer_phone.Value = shipmenUpdate.Customer_Phone;

                var _customer_address = new SqlParameter("@customer_address", SqlDbType.NVarChar, 255);
                _customer_address.Value = shipmenUpdate.Customer_Address;

                var _fee = new SqlParameter("@fee", SqlDbType.Decimal);
                _fee.Value = shipmenUpdate.Shipping_Fee;

                var _note = new SqlParameter("@note", SqlDbType.NVarChar, 255);
                _note.Value = shipmenUpdate.Note;

                var _estimated_delivery_date = new SqlParameter("@estimated_delivery_date", SqlDbType.DateTime2);
                _estimated_delivery_date.Value = shipmenUpdate.Estimated_Delivery_Date;

                var _actual_delivery_date = new SqlParameter("@actual_delivery_date", SqlDbType.DateTime2);
                _actual_delivery_date.Value = shipmenUpdate.Actual_Delivery_Date;

                var _estimated_arrival_date = new SqlParameter("@estimated_arrival_date", SqlDbType.DateTime2);
                _estimated_arrival_date.Value = shipmenUpdate.Estimated_Arrival_Date;

                var _actual_arrival_date = new SqlParameter("@actual_arrival_date", SqlDbType.DateTime2);
                _actual_arrival_date.Value = shipmenUpdate.Actual_Arrival_Date;

                var _order_id = new SqlParameter("@order_id", SqlDbType.Int);
                _order_id.Value = 1;

                var _shipment_status_id = new SqlParameter("@shipment_status_id", SqlDbType.Int);
                _shipment_status_id.Value = shipmenUpdate.Shipment_Status_Id;

               var result = _context.Database.ExecuteSqlRaw(sql, 
                    _id,
                    _customer_name, _customer_phone, _customer_address, _fee, _note,
                    _estimated_delivery_date,
                    _actual_delivery_date,
                    _estimated_arrival_date,
                    _actual_arrival_date,
                    _order_id,
                    _shipment_status_id
                );

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateShipment(int id, [FromBody] ShipmentDto shipmentUpdate)
        {
            try
            {
                if (shipmentUpdate == null || !ModelState.IsValid) return BadRequest();

                var _shipmentUpdate = _context.Shipments.Where(s =>  s.Id == id).FirstOrDefault();

                if (_shipmentUpdate == null) return NotFound("Not found shipment");

                _shipmentUpdate.Customer_Name = shipmentUpdate.Customer_Name;
                _shipmentUpdate.Customer_Address = shipmentUpdate.Customer_Address;
                _shipmentUpdate.Customer_Phone = shipmentUpdate.Customer_Phone;
                _shipmentUpdate.Shipping_Fee = shipmentUpdate.Shipping_Fee;
                _shipmentUpdate.Note = shipmentUpdate.Note;
                _shipmentUpdate.Actual_Delivery_Date = shipmentUpdate.Actual_Delivery_Date; 
                _shipmentUpdate.Actual_Arrival_Date = shipmentUpdate.Actual_Arrival_Date;

                _context.Shipments.Update(_shipmentUpdate);
                _context.SaveChanges();

                return Ok("Update Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("update/shipment_detail/{id}")]
        public IActionResult UpdateShipmentDetail(int id, [FromBody] ShipmentDetailDto shipmentDetailUpdate)
        {
            try
            {
                if (shipmentDetailUpdate == null || !ModelState.IsValid) return BadRequest();

                var exitingShipmentDetail = _context.Shipment_Details.Where(sd =>  sd.Id == id).FirstOrDefault();

                if (exitingShipmentDetail == null) return NotFound("Not found shipment detail");

                Shipment_Detail _shipmentDetailUpdate = new Shipment_Detail()
                {
                    Shipping_Method = shipmentDetailUpdate.Shipping_Method,
                    Driver_Name = shipmentDetailUpdate.Driver_Name,
                    Driver_Phone = shipmentDetailUpdate.Driver_Phone,
                    Quantity = shipmentDetailUpdate.Quantity,
                    Weight = shipmentDetailUpdate.Weight,
                    Packaging_Type = shipmentDetailUpdate.Packaging_Type,
                    Barcode = shipmentDetailUpdate.Barcode,
                };

                _context.Shipment_Details.Update(_shipmentDetailUpdate);
                _context.SaveChanges();

                return Ok("Update Success");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("delete/sp/{id}")]
        public IActionResult DeleteOneWithSP(int id)
        {
            try
            {
                var sql = "EXEC DeleteShipment @id";

                var _id = new SqlParameter("@id", SqlDbType.Int);
                _id.Value = id;

                var result = _context.Database.ExecuteSqlRaw(sql, _id);

                return Ok(result);
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
                var existingShipment = _context.Shipments.Find(id);

                if (existingShipment == null)
                {
                    return NotFound("Not found shipment");
                }

                _context.Shipments.Remove(existingShipment);
                _context.SaveChanges();

                return Ok("Delete Success");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
