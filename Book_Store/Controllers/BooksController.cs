using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookBL _books;

        public BooksController(IBookBL data)
        {
            _books = data;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddBooks(Books Info)
        {
            try
            {
                int a = 1;
                var data = await _books.AddBooks(a,Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Books Details Entered Succesfully";
                    return this.Ok(new { status, Message, data });
                }
                else
                {
                    var status = false;
                    var Message = "Books Details Entered Failed";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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

        [HttpDelete]
        [Route("{BookId}")]
        public async Task<IActionResult> DeleteBooks(int BookId)
        {
            try
            {
                var data = await _books.DeleteBooks(BookId);
                if (data != null)
                {
                    var success = true;
                    var message = "Books Deleted Successfully";
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

        [Route("{BookId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBooks(int BookId, Books Info)
        {
            try
            {
                
                var data = await _books.UpdateBooks(BookId, Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Books Details Update Succesfully";
                    return this.Ok(new { status, Message, data });
                }
                else
                {
                    var status = false;
                    var Message = "Books Details Update Failed";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
        [Route("Sort")]
        public async Task<IActionResult> SortBooks(string sortingChoice, string sortingType)
        {
            try
            {
                var data = await _books.SortBooks(sortingChoice,sortingType);
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