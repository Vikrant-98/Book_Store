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
    public class BooksController : ControllerBase
    {

        private readonly IBookBL _books;

        private static bool success;
        private static string message;

        public BooksController(IBookBL data)
        {
            _books = data;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBooks(Books Info)
        {
            try
            {
                var user = HttpContext.User;
                
                int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                var data = await _books.AddBooks(adminID, Info);
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

        [HttpGet]
        public async Task<IActionResult> GetListOfBooks()
        {
            try
            {
                var data = await _books.GetListOfBooks();
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
                var data = await _books.DeleteBooks(BookId);
                if (data != null)
                {
                   success = true;
                   message = "Book Deleted Successfully";
                   return Ok(new { success, message, data });
                }
                else
                {
                   message = "Book not Deleted";
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
        public async Task<IActionResult> UpdateBooks(int BookId, Books Info)
        {
            try
            {
                var user = HttpContext.User;
                int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                var data = await _books.UpdateBooks(BookId,Info);

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

        /// <summary>
        /// Search Book by Name
        /// </summary>
        /// <param name="bookSearch">Book Search Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> BookSearch(BookSearchRequest bookSearch)
        {
            try
            {
                var data = await _books.SearchBook(bookSearch);
                if (data != null && data.Count != 0)
                {
                    var success = true;
                    var message = "Book Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    var message = "Book Not Found";
                    var success = false;
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Shows Books in Ascending order
        /// </summary>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        [Route("{Choice}/Sort/{Type}")]
        public async Task<IActionResult> SortBooks(string Choice, string Type)
        {
            try
            {
                var data = await _books.SortBooks(Choice,Type);
                if (data != null)
                {
                    var success = true;
                    var message = "Sorted Books Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    var message = "Books Not Found";
                    var success = false;
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}