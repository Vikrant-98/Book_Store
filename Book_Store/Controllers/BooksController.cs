﻿using System;
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
    public class BooksController : ControllerBase
    {

        private readonly IBookBL _booksBL;

        private static bool success;
        private static string message;

        public BooksController(IBookBL data)
        {
            _booksBL = data;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBooks(Books Info)
        {
            try
            {
                var user = HttpContext.User;
                
                int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                var data = await _booksBL.AddBooks(adminID, Info);
                if (data != null)
                   {
                       success = true;
                       message = "Book Details Added Successfully";
                       return Ok(new { success, message, data });
                   }
                   else
                   {
                       message = "No Data Provided";
                       return NotFound(new { success, message });
                   }                  
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [Route("{BookID}/Image")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddImage(int BookID, IFormFile Image)
        {
            try
            {
                var user = HttpContext.User;

                int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                var data = await _booksBL.AddImage(adminID, BookID, Image);
                if (data != null)
                {
                    success = true;
                    message = "Image Added Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Book Added";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListOfBooks(string Search,string OrderBy,string Type)
        {
            try
            {
                var data = await _booksBL.GetListOfBooks();
                if (Search != null)
                {
                    data = await _booksBL.SearchBook(Search);
                }
                else if (OrderBy != null)
                {
                    data = await _booksBL.SortBooks(OrderBy, Type);
                }
                if (data != null)
                {
                    var success = true;
                    var message = "List of Books Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    var message = "No Data Found";
                    var status = false;
                    return NotFound(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        /// <summary>
        /// Delete Books Record Using BookId
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{BookId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBooks(int BookId)
        {
            try
            {
                var user = HttpContext.User;
                
                int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                var data = await _booksBL.DeleteBooks(BookId);
                if (data == true)
                {
                   success = true;
                   message = "Book Deleted Successfully";
                   return Ok(new { success, message });
                }
                else
                {
                   success = false;
                   message = "Book not Deleted";
                  return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                success = false;
                return BadRequest(new { success, ex.Message });
            }
        }

        /// <summary>
        /// Update the Records of book using BookId and Information
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        [Route("Cart")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetListOfBooksInCart()
        {
            try
            {
                var user = HttpContext.User;
                int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                var data = await _booksBL.GetListOfBooksInCart(userID);

                if (data != null)
                {
                    success = true;
                    message = "Books In Cart fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Books Not Fetched";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Update the Records of book using BookId and Information
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        [Route("{BookId}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBooks(int BookId,[FromBody] UpdateBooks Info)
        {
            try
            {
                var user = HttpContext.User;
                int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                var data = await _booksBL.UpdateBooks(adminID,BookId, Info);

                if (data != null)
                {
                   success = true;
                   message = "Book Updated Successfully";
                   return Ok(new { success, message, data });
                }
                else
                {
                   message = "Book Not Updated";
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