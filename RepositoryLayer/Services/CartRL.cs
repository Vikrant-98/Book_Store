using CommonLayer.Request;
using CommonLayer.Responce;
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
    public class CartRL : ICartRL
    {

        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public CartRL(IConfiguration configuration)
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

        /// <summary>
        /// Add Book into Cart
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Added Successfully return Response Data else null or Bad Request</returns>
        public async Task<CartBookResponse> AddBookIntoCart(int userID, Cart data)
        {
            try
            {

                CartBookResponse responseData = null;

                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = createDate;

                SQLConnection();
                using (SqlCommand command = new SqlCommand("spBookIntoCart", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@BookID", data.BookID);
                    command.Parameters.AddWithValue("@IsDelete", false);
                    command.Parameters.AddWithValue("@IsActive", true);
                    command.Parameters.AddWithValue("@CreateDate", createDate);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = BookResponseModel(dataReader);
                    conn.Close();
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Fetch List of Books from the Database
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <returns>If Data Fetched Successfull return Response Data else null or Exception</returns>
        public async Task<List<CartBookResponse>> GetListOfBooksInCart(int userID)
        {
            try
            {
                List<CartBookResponse> bookList = null;
                SQLConnection();
                bookList = new List<CartBookResponse>();
                using (SqlCommand command = new SqlCommand("spGetBooksByUserId", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    bookList = ListBookResponseModel(dataReader);
                    conn.Close();
                };
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Book From the Cart in the database
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="cartID">Cart-ID</param>
        /// <returns>If Data Deleted Successfull return true else false or Exception</returns>
        public async Task<bool> DeleteBookFromCart(int userID, int cartID)
        {
            try
            {
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spDeleteCartByUserId", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CartID", cartID);
                    conn.Open();
                    int count = await command.ExecuteNonQueryAsync();
                    if (count >= 0)
                    {
                        return true;
                    }
                    conn.Close();
                };
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// List of Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return List of Book Response Data</returns>
        private List<CartBookResponse> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<CartBookResponse> bookList = new List<CartBookResponse>();
                CartBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new CartBookResponse
                    {
                        UserID = Convert.ToInt32(dataReader["UserID"]),
                        CartID = Convert.ToInt32(dataReader["CartID"]),
                        BookID = Convert.ToInt32(dataReader["BookID"]),
                        BookName = dataReader["BookName"].ToString(),
                        Author = dataReader["AuthorName"].ToString(),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        IsDelete = Convert.ToBoolean(dataReader["IsDeleted"]),
                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                        Image = dataReader["Images"].ToString(),
                    };
                    bookList.Add(responseData);
                }
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return Book Response Data</returns>
        public static CartBookResponse BookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                CartBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new CartBookResponse
                    {
                        UserID = Convert.ToInt32(dataReader["UserID"]),
                        CartID = Convert.ToInt32(dataReader["CartID"]),
                        BookID = Convert.ToInt32(dataReader["BookID"]),
                        BookName = dataReader["BookName"].ToString(),
                        Author = dataReader["AuthorName"].ToString(),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        IsDelete = Convert.ToBoolean(dataReader["IsDeleted"]),
                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                        Image = dataReader["Images"].ToString(),
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
