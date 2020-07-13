using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        object User(User data);

        object Login(Login data);
    }
}
