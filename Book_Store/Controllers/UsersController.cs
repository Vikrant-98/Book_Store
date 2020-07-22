using Book_Store.MSMQ_Service;
using BusinessLayer.Interface;
using CommonLayer.Responce;
using CommonLayer.Services;
using MessagrListner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL _userBL;
        MessageSender msmqSender = new MessageSender();
        
        public static string _user = "User";
        private IConfiguration _configuration;

        public UsersController(IUserBL data, IConfiguration configuration)
        {
            _userBL = data;
            _configuration = configuration;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> UserRegister(User Info)
        {
            try
            {
                var data = await _userBL.UserRegistration(Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "User Registered Succesfully";
                    string msmqRecordInQueue = Convert.ToString(Info.FirstName)+" " 
                    + Convert.ToString(Info.LastName) + "\n" + Message + "\n Email : " 
                    + Convert.ToString(Info.Password);
                    msmqSender.Message(msmqRecordInQueue);
                    MessageListner msg = new MessageListner();
                    

                    return this.Ok(new { status, Message, data });
                }
                else
                {
                    var status = false;
                    var Message = "User Registration Failed Try Again!!!";
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
        public async Task<IActionResult> UserLogin([FromBody] Login Info)
        {
            try
            {
                var Result = await _userBL.UserLogin(Info);
                
                if (!Result.Equals(null))
                {
                    var jsontoken = GenerateToken(Result);
                    var status = "True";
                    var Message = "Login Successful";
                    return Ok(new { status, Message, Result ,jsontoken });
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
        private string GenerateToken(UserRegistrationResponse Info)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Info.UserCategory.ToString()),
                    new Claim("EmailID", Info.EmailID.ToString()),
                    new Claim("UserID", Info.UserID.ToString())
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