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
        private IUserRL Books;

        public UserBL(IUserRL Data)
        {
            Books = Data;
        }

        public async Task<RegistrationResponse> UserRegistration(User data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await Books.UserRegistration(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RegistrationResponse> UserLogin(Login loginDetails)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginDetails.Email) || string.IsNullOrWhiteSpace(loginDetails.Password))
                    return null;
                else
                    return await Books.UserLogin(loginDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
