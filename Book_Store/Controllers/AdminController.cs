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
using CommonLayer.Responce;
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
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL _books;
        MessageSender msmqSender = new MessageSender();
        public static string _admin = "Admin";
        private IConfiguration _configuration;
        
        public AdminController(IAdminBL data, IConfiguration configuration)
        {
            _books = data;
            _configuration = configuration;

        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> UserRegister(User Info)
        {
            try
            {
                var data = await _books.AdminRegistration(Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "User Details Entered Succesfully";
                    string msmqRecordInQueue = Convert.ToString(Info.FirstName)
                    + Convert.ToString(Info.LastName) 
                    + "\n" + Message + "\n Email : "
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
        public async Task<IActionResult> AdminLogin([FromBody] Login Info)
        {
            try
            {
                var Result = await _books.AdminLogin(Info);
                var jsontoken = GenerateToken(Result);
                if (!Result.Equals(null))
                {
                    var status = "True";
                    var Message = "Login Successful";
                    return Ok(new { status, Message, Result, jsontoken });
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
        /// Generates Token
        /// </summary>
        /// <param name="adminDetails">Admin Response Details</param>
        /// <param name="tokenType">Token Type</param>
        /// <returns>It return token else exception</returns>
        private string GenerateToken(AdminRegistrationResponse Info)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Info.UserCategory.ToString()),
                    new Claim("EmailID", Info.Email.ToString()),
                    new Claim("AdminID", Info.AdminId.ToString())
                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
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