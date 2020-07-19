using BusinessLayer.Interface;
using CommonLayer.Responce;
using CommonLayer.Services;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL _userRL;

        public UserBL(IUserRL Data)
        {
            _userRL = Data;
        }

        public async Task<UserRegistrationResponse> UserRegistration(User data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await _userRL.UserRegistration(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserRegistrationResponse> UserLogin(Login loginDetails)
        {
            try
            {
                if (loginDetails == null)
                    return null;
                else
                    return await _userRL.UserLogin(loginDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
