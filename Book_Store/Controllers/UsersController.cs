using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Book_Store.MSMQ_Service;
using Book_Store.TokenGeneration;
using BusinessLayer.Interface;
using CommonLayer.Services;
using MessagrListner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL _books;
        MessageSender msmqSender = new MessageSender();
        Token token = new Token();
        public static string _user = "User";
        public UsersController(IUserBL data)
        {
            _books = data;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> UserRegister(User Info)
        {
            try
            {
                var data = await _books.UserRegistration(Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "User Details Entered Succesfully";
                    string msmqRecordInQueue = Convert.ToString(Info.FirstName) 
                    + Convert.ToString(Info.LastName) + "\n" + Message + "\n Email : " 
                    + Convert.ToString(Info.Password);
                    msmqSender.Message(msmqRecordInQueue);
                    MessageListner msg = new MessageListner();
                    

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
                return BadRequest(e.Message );
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin([FromBody] Login Info)
        {
            try
            {
                var Result = _books.UserLogin(Info);

                var jsontoken = token.GenerateToken(Info,_user);
                if (!Result.Equals(null))
                {
                    var status = "True";
                    var Message = "Login Successful";
                    return Ok(new { status, Message, Result });
                }
                else
                {
                    var status = "False";
                    var Message = "Invaid Username Or Password";
                    return BadRequest(new { status, Message, Result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}