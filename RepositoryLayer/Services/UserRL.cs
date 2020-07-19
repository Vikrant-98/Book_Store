using CommonLayer.Responce;
using CommonLayer.Services;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {

        //Configuration initialized
        private readonly IConfiguration _configuration;
        private SqlConnection conn;
        public static readonly string _user = "User";
        readonly Random random = new Random();
        //constructor 
        public UserRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sql Connection
        /// </summary>
        private void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("myconn");
            conn = new SqlConnection(sqlConnectionString);
        }


        public async Task<UserRegistrationResponse> UserRegistration(User data)
        {
            try
            {
                UserRegistrationResponse responseData = null;

                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                
                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = DateTime.Now;

                SQLConnection();
               
                using (SqlCommand command = new SqlCommand("spAddUserDetail", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", data.FirstName);
                    command.Parameters.AddWithValue("@LastName", data.LastName);
                    command.Parameters.AddWithValue("@EmailID", data.EmailID);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@UserCategory", _user);
                    command.Parameters.AddWithValue("@CreateDate", createDate);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = RegistrationResponseModel(dataReader);
                    
                    conn.Close();
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<UserRegistrationResponse> UserLogin(Login data)
        {
            try
            {
                UserRegistrationResponse responseData = null;

                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                SQLConnection();

                using (SqlCommand command = new SqlCommand("spUserLogin", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", data.EmailID);
                    command.Parameters.AddWithValue("@Password", Password);
                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = RegistrationResponseModel(dataReader);
                    conn.Close();
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private UserRegistrationResponse RegistrationResponseModel(SqlDataReader dataReader)
        {
            try
            {
                UserRegistrationResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new UserRegistrationResponse
                    {
                        UserID = Convert.ToInt32(dataReader["UserID"]),
                        FirstName = dataReader["FirstName"].ToString(),
                        LastName = dataReader["LastName"].ToString(),
                        EmailID = dataReader["EmailID"].ToString(),
                        UserCategory = dataReader["UserCategory"].ToString(),
                        CreatedDate = Convert.ToDateTime(dataReader["CreateDate"]),
                        ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"])
                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
