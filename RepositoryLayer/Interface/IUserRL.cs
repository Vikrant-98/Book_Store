using CommonLayer.Responce;
using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<UserRegistrationResponse> UserRegistration(User data);

        Task<UserRegistrationResponse> UserLogin(Login data);
    }
}
