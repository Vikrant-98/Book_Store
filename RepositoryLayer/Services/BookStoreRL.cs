using CommonLayer.Services;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookStoreRL : IBookStoreRL
    {

        //Configuration initialized
        private readonly IConfiguration Configuration;
        readonly Random random = new Random();
        //constructor 
        public BookStoreRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public object BooksDatails(User data)
        {
            try
            {
                string connect = Configuration.GetConnectionString("myconn");
                SqlConnection connection = new SqlConnection(connect);
                SqlCommand com = StoreProcedureConnection("spAddUserDetail", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);

                string userId = data.FirstName.ToLower() + ((random.Next() % 1000) + 100).ToString();

                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = DateTime.Now;

                com.Parameters.AddWithValue("@FirstName", data.FirstName);
                com.Parameters.AddWithValue("@LastName", data.LastName);
                com.Parameters.AddWithValue("@UserId", userId);
                com.Parameters.AddWithValue("@Email", data.Email);
                com.Parameters.AddWithValue("@Password", Password);
                com.Parameters.AddWithValue("@UserCategory", data.UserCategory);
                com.Parameters.AddWithValue("@Address", data.Address);
                com.Parameters.AddWithValue("@City", data.City);
                com.Parameters.AddWithValue("@PinCode", data.PinCode);
                com.Parameters.AddWithValue("@CreateDate", createDate);
                com.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                connection.Open();
                int Response = com.ExecuteNonQuery();
                connection.Close();
                if (Response != 0)
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
        public SqlCommand StoreProcedureConnection(string Procedurename, SqlConnection connection)
        {
            using (SqlCommand com = new SqlCommand(Procedurename, connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                return com;
            }
        }
    }

}
