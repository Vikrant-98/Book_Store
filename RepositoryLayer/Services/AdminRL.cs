using CommonLayer.Responce;
using CommonLayer.Services;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;
        public static readonly string _user = "Admin";
        readonly Random random = new Random();
        //constructor 
        public AdminRL(IConfiguration configuration)
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


        public async Task<RegistrationResponse> AdminRegistration(User data)
        {
            try
            {
                RegistrationResponse responseData = null;

                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);

                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = DateTime.Now;

                SQLConnection();

                using (SqlCommand command = new SqlCommand("spAddUserDetail", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", data.FirstName);
                    command.Parameters.AddWithValue("@LastName", data.LastName);
                    command.Parameters.AddWithValue("@Email", data.Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@UserCategory", _user);
                    command.Parameters.AddWithValue("@Address", data.Address);
                    command.Parameters.AddWithValue("@City", data.City);
                    command.Parameters.AddWithValue("@PinCode", data.PinCode);
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


        public async Task<RegistrationResponse> AdminLogin(Login data)
        {
            try
            {
                RegistrationResponse responseData = null;

                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                SQLConnection();

                using (SqlCommand command = new SqlCommand("spUserLogin", conn))
                {
                    command.Parameters.AddWithValue("@Email", data.Email);
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
        private RegistrationResponse RegistrationResponseModel(SqlDataReader dataReader)
        {
            try
            {
                RegistrationResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new RegistrationResponse
                    {
                        UserId = Convert.ToInt32(dataReader["UserId"]),
                        FirstName = dataReader["FirstName"].ToString(),
                        LastName = dataReader["LastName"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        UserCategory = dataReader[" UserCategory"].ToString(),
                        CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
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
