﻿using CommonLayer.Responce;
using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        Task<RegistrationResponse> UserRegistration(User data);

        Task<RegistrationResponse> UserLogin(Login loginDetails);
       
    }
}