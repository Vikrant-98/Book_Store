using CommonLayer.Responce;
using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        Task<AdminRegistrationResponse> AdminRegistration(User data);

        Task<AdminRegistrationResponse> AdminLogin(Login loginDetails);
    }
}
