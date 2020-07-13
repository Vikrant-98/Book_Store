using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Book_Store.MSMQ_Service;
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
        private readonly IConfiguration _configuration;
        MessageSender msmqSender = new MessageSender();
        public UsersController(IUserBL _data)
        {
            _books = _data;
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser(User Info)
        {
            try
            {
                var data = _books.User(Info);
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
                return BadRequest(new { error = e.Message });
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login Info)
        {
            try
            {
                var Result = _books.Login(Info);
                //var jsontoken = GenerateToken(Info,Result);
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
        /// <summary>
        /// Generates Token for Login
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private string GenerateToken(Login Info,string UserCategory)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, UserCategory),
                    new Claim("Email", Info.Email),
                    new Claim("Password", Info.Password)
                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}