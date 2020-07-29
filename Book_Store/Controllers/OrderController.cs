using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.MSMQ_Service;
using BusinessLayer.Interface;
using CommonLayer.Request;
using CommonLayer.Responce;
using MessagrListner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderBL _orderBL;
        MessageSender msmqSender = new MessageSender();

        private static bool success;
        private static string message;

        public OrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        /// <summary>
        /// Shows All Orders Details
        /// </summary>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetListOgBooksInCart()
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _orderBL.GetListOfBooks(userID);
                if (data != null)
                {
                    success = true;
                    message = "List of Order Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Data Found";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Add Book into Cart
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("PlaceOrder")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> BookPlaceOdrder(PlaceOrder data)
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var orderData = await _orderBL.BookPlaceOdrder(userID, data);
                if (orderData != null)
                {
                    success = true;
                    message = "Place Order Successfully";

                    OrderSummary responcedata = new OrderSummary
                    {
                        OrderID = orderData.OrderId,
                        BookName = orderData.BookName,
                        AuthorName = orderData.AuthorName,
                        Price = orderData.TotalPrice,
                        TotalPrice = orderData.TotalPrice,
                        Quantity = orderData.Quantity
                    };

                    string msmqRecordInQueue = message + "\nInformation :"
                    + "\nOrderID :" + orderData.UserId
                    + "\nUserID :" + orderData.UserId
                    + "\nBook Name :" + orderData.BookName
                    + "\nAuthor Name :" + orderData.AuthorName
                    + "\nBooks Quantity :" + orderData.Quantity
                    + "\nTotal Cost :" + orderData.TotalPrice;
                    msmqSender.Message(msmqRecordInQueue);
                    MessageListner msg = new MessageListner();

                    return Ok(new { success, message, responcedata });
                }
                else
                {
                    message = "No Order Place";
                    return NotFound(new { success, message });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Add Address
        /// </summary>
        /// <param name="cart">Address Info</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("AddAddress")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddAddress(Address info)
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _orderBL.Address(userID, info);
                if (data != null)
                {
                    success = true;
                    message = "Address Added Successfully";
                    return Ok(new { success, message, data});
                }
                else
                {
                    success = false;
                    message = "No Address Added";
                    return NotFound(new { success, message });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Add Address
        /// </summary>
        /// <param name="cart">Address Info</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("Address")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAddress()
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _orderBL.GetAddress(userID);
                if (data != null)
                {
                    success = true;
                    message = "Address Get Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "fail to Get  Address Added";
                    return NotFound(new { success, message });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}