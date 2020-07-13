using BusinessLayer.Interface;
using CommonLayer.Services;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL Books;

        public UserBL(IUserRL Data)
        {
            Books = Data;
        }

        public object User(User data)
        {
            try
            {
                var Result = Books.User(data);
                if (!Result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public object Login(Login data)
        {
            try
            {
                var Result = Books.Login(data);

                if (!Result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
