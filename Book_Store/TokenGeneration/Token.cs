using CommonLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.TokenGeneration
{
    public class Token
    {

        private readonly IConfiguration _configuration;

        public Token(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        /// <summary>
        /// Generates Token for Login
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        
    }
}
