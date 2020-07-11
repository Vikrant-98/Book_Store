using CommonLayer.Services;
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
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MQSNJSU;Initial Catalog=BookStore;Integrated Security=True");

        public object BooksDatails(User data)
        {
            try
            {
                SqlCommand com = StoreProcedureConnection("spAddUserDetail", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                com.Parameters.AddWithValue("FirstName", data.FirstName);
                com.Parameters.AddWithValue("LastName", data.LastName);
                com.Parameters.AddWithValue("UserId", data.UserId);
                com.Parameters.AddWithValue("Email", data.Email);
                com.Parameters.AddWithValue("Password", Password);
                com.Parameters.AddWithValue("Address", data.Address);
                com.Parameters.AddWithValue("City", data.City);
                com.Parameters.AddWithValue("PinCode", data.PinCode);
                com.Parameters.AddWithValue("CreateDate", data.CreateDate);
                com.Parameters.AddWithValue("ModifiedDate", data.ModifiedDate);
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
            finally
            {
                connection.Close();
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
