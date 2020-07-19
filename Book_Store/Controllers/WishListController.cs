using BusinessLayer.Interface;
using CommonLayer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL _wishListBL;

        private static bool success;
        private static string message;

        public WishListController(IWishListBL wishList)
        {
            _wishListBL = wishList;
        }

        /// <summary>
        /// Add Book into WishList
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddBookIntoWishList(WishList info)
        {
            try
            {
                var user = HttpContext.User;

                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishListBL.CreateNewWishList(userID, info);
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
                var data = await _wishListBL.GetListOfWishList(userID);
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
        public async Task<IActionResult> DeleteBookFromCart(int wishListID)
        {
            try
            {
                var user = HttpContext.User;
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishListBL.DeleteBookFromWishList(userID, wishListID);
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
        public async Task<IActionResult> MoveToCart(int wishListID)
        {
            try
            {
                var user = HttpContext.User;
                
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _wishListBL.MoveToCart(userID, wishListID);
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