﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL _cartBL;

        private static bool success;
        private static string message;

        public CartController(ICartBL cart)
        {
            _cartBL = cart;
        }

        /// <summary>
        /// Add Book into Cart
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddBookIntoCart(Cart info)
        {
            try
            {
                var user = HttpContext.User;
                
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _cartBL.AddBookIntoCart(userID,info);
                if (data != null)
                {
                    success = true;
                    message = "Added to Card Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Cart Added";
                    return NotFound(new { success, message });
                }
                    
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Shows All Books in Cart
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
                var data = await _cartBL.GetListOfBooksInCart(userID);
                if (data != null)
                {
                    success = true;
                    message = "List of Books Fetched Successfully";
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
        /// Delete Book From Cart
        /// </summary>
        /// <param name="cartID">CartID</param>
        /// <returns>If Data Deleted return Ok else Not Found or Bad Request</returns>
        [HttpDelete("{cartID}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteBookFromCart(int cartID)
        {
            try
            {
                var user = HttpContext.User;
                
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _cartBL.DeleteBookFromCart(userID, cartID);
                if (data)
                {
                    success = true;
                    message = "Book Removed from Cart Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                     message = "No Cart is present with this ID: " + cartID;
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