using CommonLayer.Responce;
using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        Task<AdminRegistrationResponse> AdminRegistration(User data);

        Task<AdminRegistrationResponse> AdminLogin(Login data);
    }
}
