using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Dto;
using WEB_API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            try
            {
                var orderList = _context.Orders.AsNoTracking().Select(order => new {
                    id =  order.Id,
                    customer_Id = order.Customer_Id,
                    customer_Name =  order.Customer_Name,
                    customer_Phone =  order.Customer_Phone,
                    order_Date =  order.Order_Date,
                    total_Amount =  order.Total_Amount,
                    shipping_Address =  order.Shipping_Address,
                    payment_Method =  order.Payment_Method,
                    shipment = order.Shipment == null ? null : new
                    {
                        id = order.Id,
                        status = order.Shipment.Shipment_Status.Name
                    }
                });

                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateOne([FromBody] Order_Create order)
        {
            try
            {
                Order _orderCreate = new Order()
                {
                    Customer_Name = order.Customer_Name,
                    Customer_Phone = order.Customer_Phone,
                    Shipping_Address = order.Shipping_Address,
                    Total_Amount = order.Total_Amount,
                    Order_Date = order.Order_Date,
                    Payment_Method = order.Payment_Method,
                    //Status = order.Status,
                };
                _context.Orders.Add(_orderCreate);
                _context.SaveChanges();

                foreach (Order_Product item in order.Products)
                {
                    Order_Item order_Item = new Order_Item()
                    {
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Amount = item.Amount,
                        Product_Id = item.Product_Id,
                        Order_Id = _orderCreate.Id
                    };

                    _context.Order_Items.Add(order_Item);
                }
                _context.SaveChanges();

                return Created("Create success", _orderCreate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
