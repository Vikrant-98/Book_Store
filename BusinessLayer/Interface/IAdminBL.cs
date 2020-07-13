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
        Task<RegistrationResponse> AdminRegistration(User data);

        Task<RegistrationResponse> AdminLogin(Login loginDetails);
    }
}
