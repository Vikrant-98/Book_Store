using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.MSMQ_Service;
using BusinessLayer.Interface;
using CommonLayer.Request;
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
        [Route("{CartId}/PlaceOrder")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> BookPlaceOdrder(int CartId)
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _orderBL.BookPlaceOdrder(userID, CartId);
                if (data != null)
                {
                    success = true;
                    message = "Place Order Successfully";
                    string msmqRecordInQueue = message + "\nInformation :"
                    + "\nUserID :"+ data.UserId
                    + "\nBookID :" + data.BookId
                    + "\nBook Name :" + data.BookName
                    + "\nAuthor Name :" + data.AuthorName
                    + "\nBooks Quantity :" + data.Quantity
                    + "\nTotal Cost :" + data.TotalPrice;
                    msmqSender.Message(msmqRecordInQueue);
                    MessageListner msg = new MessageListner();

                    return Ok(new { success, message, data });
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
        /// Add Book into Cart
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("{orderID}/CancelOrder")]
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CancelPlaceOdrder(int orderID)
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _orderBL.CancelPlaceOdrder(userID, orderID);
                if (data == true)
                {
                    success = true;
                    message = "Order Canceled Successfully";
                    return Ok(new { success, message});
                }
                else
                {
                    message = "Order Not Cancled";
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