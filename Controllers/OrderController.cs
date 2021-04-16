using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecentOrderAPI.Repositories;
using RecentOrderAPI.Services;
using RecentOrderAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecentOrderAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerService _customerService;


        public OrderController(IOrderRepository orderRepository, ICustomerService customerService)
        {
            _orderRepository = orderRepository ?? 
                throw new ArgumentNullException(nameof(orderRepository));
            _customerService = customerService ?? 
                throw new ArgumentNullException(nameof(customerService));
        }
        public ActionResult<string> Get()
        {
            return "Please post your request to get customer recent order";
        }

        [HttpPost]
        public async Task<IActionResult> GetMostRecentOrder(RecentOrderPostDto recentOrderPostDto)
        {
            try
            {
                var customerDetails = await _customerService.GetCustomerDetails(recentOrderPostDto.User);
                
                if(customerDetails == null)
                {
                    return NotFound("Customer not found");
                }
                if (customerDetails.CustomerId != recentOrderPostDto.CustomerId)
                {
                    var error = new { Error = "Customer details mismatch" };
                    return BadRequest(error);
                }

                var mostRecentOrder = await _orderRepository.GetCustomerMostRecentOrder(recentOrderPostDto.User, recentOrderPostDto.CustomerId);

                var recentOrderDetails = new RecentOrderGetDto();

                recentOrderDetails.Customer = new CustomerDto
                                          { FirstName = customerDetails.FirstName,
                                            LastName = customerDetails.LastName };


                //recentOrderDetails.Order = new OrderDto();
                if (mostRecentOrder != null)
                {
                    var orderItems = new List<OrderItemDto>();
                    foreach(var orderItem in mostRecentOrder.Orderitems)
                    {
                        var orderItemDto = new OrderItemDto
                        {
                            Product = mostRecentOrder.Containsgift.GetValueOrDefault(false) ? "Gift" : orderItem.Product.Productname,
                            Quantity = orderItem.Quantity,
                            PriceEach = mostRecentOrder.Containsgift.GetValueOrDefault(false) ? "" : orderItem.Price.ToString()
                        };

                        orderItems.Add(orderItemDto);
                    }
                    recentOrderDetails.Order = new OrderDto
                    {
                        OrderNumber = mostRecentOrder.Orderid,
                        OrderDate = mostRecentOrder.Orderdate.HasValue 
                                                ? mostRecentOrder.Orderdate.Value.ToString("dd-MMM-yyyy") : string.Empty,
                        DeliveryAddress = string.Join(","
                                        , customerDetails.HouseNumber, customerDetails.Street
                                        , customerDetails.Town, customerDetails.Postcode),
                        OrderItems = orderItems,
                        DeliveryExpected = mostRecentOrder.Deliveryexpected.HasValue
                                    ? mostRecentOrder.Deliveryexpected.Value.ToString("dd-MMM-yyyy") : string.Empty
                    };
                }
                return Ok(recentOrderDetails);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
