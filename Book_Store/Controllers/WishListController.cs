using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL _wishList;

        private static bool success;
        private static string message;

        public WishListController(IWishListBL wishList)
        {
            _wishList = wishList;
        }

        /// <summary>
        /// Add Book into WishList
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("{BookID}")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddBookIntoWishList(int BookID)
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishList.CreateNewWishList(userID, BookID);
                if (data != null)
                {
                    success = true;
                    message = "Added to WishList Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No WishList Added";
                    return NotFound(new { success, message });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        /// <summary>
        /// Get Book from WishList
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetListOfWishList()
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishList.GetListOfWishList(userID);
                if (data != null)
                {
                    success = true;
                    message = "Get WishList Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No WishList ";
                    return NotFound(new { success, message });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Delete Book From Wish List
        /// </summary>
        /// <param name="wishListID">WishListID</param>
        /// <param name="wishListBook">Wish List Book Data</param>
        /// <returns>If Data Deleted return Ok else Not Found or Bad Request</returns>
        [HttpDelete("{wishListID}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteBookFromCart(int wishListID, WishList wishListBook)
        {
            try
            {
                var user = HttpContext.User;
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishList.DeleteBookFromWishList(userID, wishListID, wishListBook);
                if (data)
                {
                    success = true;
                    message = "Book Removed from Wish List Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "No Book is present with this ID: ";
                    return NotFound(new { success, message });
                }    
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        /// <summary>
        /// Move Book to Cart
        /// </summary>
        /// <param name="wishListID">WishListID</param>
        /// <param name="wishListBook">Wish List Book Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost("{wishListID}/Move")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MoveToCart(int wishListID, WishList wishListBook)
        {
            try
            {
                var user = HttpContext.User;
                
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishList.MoveToCart(userID, wishListID, wishListBook);
                if (data != null)
                {
                    success = true;
                    message = "Book Moved to Cart Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Book is Present";
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