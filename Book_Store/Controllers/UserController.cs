using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IBookStoreBL books;

        public UserController(IBookStoreBL data)
        {
            books = data;
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody]User Info)
        {
            try
            {
                var data = books.BooksDatails(Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "User Details Entered Succesfully";
                    return this.Ok(new { status, Message, data });
                }
                else
                {
                    var status = false;
                    var Message = "User Details Entered Failed";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
        }
}