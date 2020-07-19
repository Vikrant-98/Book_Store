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
        private IAdminRL _adminRL;

        public AdminBL(IAdminRL Data)
        {
            _adminRL = Data;
        }

        public async Task<AdminRegistrationResponse> AdminRegistration(User data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await _adminRL.AdminRegistration(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AdminRegistrationResponse> AdminLogin(Login loginDetails)
        {
            try
            {
                if (loginDetails == null)
                    return null;
                else
                    return await _adminRL.AdminLogin(loginDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
