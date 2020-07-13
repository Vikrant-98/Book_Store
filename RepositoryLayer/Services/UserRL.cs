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
    public class UserRL : IUserRL
    {

        //Configuration initialized
        private readonly IConfiguration Configuration;
        readonly Random random = new Random();
        //constructor 
        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public object User(User data)
        {
            try
            {
                string connect = Configuration.GetConnectionString("myconn");
                SqlConnection connection = new SqlConnection(connect);
                SqlCommand command = StoreProcedureConnection("spAddUserDetail", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);

                string userId = data.FirstName.ToLower() + ((random.Next() % 1000) + 100).ToString();

                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = DateTime.Now;

                command.Parameters.AddWithValue("@FirstName", data.FirstName);
                command.Parameters.AddWithValue("@LastName", data.LastName);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Email", data.Email);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@UserCategory", data.UserCategory);
                command.Parameters.AddWithValue("@Address", data.Address);
                command.Parameters.AddWithValue("@City", data.City);
                command.Parameters.AddWithValue("@PinCode", data.PinCode);
                command.Parameters.AddWithValue("@CreateDate", createDate);
                command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                connection.Open();
                int Response = command.ExecuteNonQuery();
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
        public object Login(Login data)
        {
            try
            {
                string connect = Configuration.GetConnectionString("myconn");
                SqlConnection connection = new SqlConnection(connect);
                SqlCommand command = StoreProcedureConnection("spUserLogin", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                command.Parameters.AddWithValue("@Email", data.Email);
                command.Parameters.AddWithValue("@Password", Password);
                SqlCommand userCommand = StoreProcedureConnection("spUserDetails", connection);
                userCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = data.Email;
                //userCommand.Parameters.AddWithValue("@Email", data.Email);
                connection.Open();
                SqlDataReader UserResponse = userCommand.ExecuteReader();
                var user = UserResponse["UserCategory"].ToString();
                int UsersResponse = userCommand.ExecuteNonQuery();
                int Response = command.ExecuteNonQuery();
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
