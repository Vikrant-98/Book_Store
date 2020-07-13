using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        object User(User data);

        object Login(Login data);
       
    }
}
