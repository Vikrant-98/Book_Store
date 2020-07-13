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
    public class AdminBL : IAdminBL
    {
        private IAdminRL Books;

        public AdminBL(IAdminRL Data)
        {
            Books = Data;
        }

        public async Task<RegistrationResponse> AdminRegistration(User data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await Books.AdminRegistration(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<RegistrationResponse> AdminLogin(Login loginDetails)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginDetails.Email) || string.IsNullOrWhiteSpace(loginDetails.Password))
                    return null;
                else
                    return Books.AdminLogin(loginDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
