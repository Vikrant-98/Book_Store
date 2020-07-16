using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
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

    }

}