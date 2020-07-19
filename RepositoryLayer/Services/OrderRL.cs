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
    public class OrderRL : IOrderRL
    {

        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public OrderRL(IConfiguration configuration)
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
        /// Place the Order
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Order Place Successfully return Response Data else null or Bad Request</returns>
        public async Task<PlaceOrderResponce> BookPlaceOdrder(int userID, int CartId)
        {
            try
            {
                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = createDate;

                PlaceOrderResponce responseData = null;
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spBookPlaceOrder", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CartID", CartId);
                    command.Parameters.AddWithValue("@IsPlaced", true);
                    command.Parameters.AddWithValue("@IsActive", true);
                    command.Parameters.AddWithValue("@CreateDate", createDate);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = OrderResponseModel(dataReader);
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get List of Order Place from the database
        /// </summary>
        /// <returns>If Data Fetched Successfully return Resonse Data else null or Exception</returns>
        public async Task<List<PlaceOrderResponce>> GetListOfPlaceOrder(int userID)
        {
            try
            {
                List<PlaceOrderResponce> bookList = null;
                SQLConnection();
                bookList = new List<PlaceOrderResponce>();
                using (SqlCommand command = new SqlCommand("spGetAllOrders", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    bookList = ListPlaceOrderResponceModel(dataReader);
                };
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Place the Order
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Order Place Successfully return Response Data else null or Bad Request</returns>
        public async Task<bool> CancelPlaceOdrder(int userID, int orderID)
        {
            try
            {
                DateTime modifiedDate = DateTime.Now;
                
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spCancelPlaceOrder", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", orderID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@IsActive", false); 
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    conn.Open();
                    int count = await command.ExecuteNonQueryAsync();
                    if (count >= 0)
                    {
                        return true;
                    }
                };

                return false;
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
        public static PlaceOrderResponce OrderResponseModel(SqlDataReader dataReader)
        {
            try
            {
                PlaceOrderResponce responseData = null;
                while (dataReader.Read())
                {
                    responseData = new PlaceOrderResponce
                    {
                        OrderId = Convert.ToInt32(dataReader["OrderId"]),
                        UserId = Convert.ToInt32(dataReader["UserId"]),
                        CartId = Convert.ToInt32(dataReader["CartId"]),
                        BookId = Convert.ToInt32(dataReader["BookId"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["Authorname"].ToString(),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        TotalPrice = Convert.ToInt32(dataReader["TotalPrice"]),
                        Quantity = Convert.ToInt32(dataReader["Quantity"]),
                        IsPlace = Convert.ToBoolean(dataReader["IsPlaced"]),
                        IsActive = Convert.ToBoolean(dataReader["IsActive"])
                    };
                }
                return responseData;
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
        private List<PlaceOrderResponce> ListPlaceOrderResponceModel(SqlDataReader dataReader)
        {
            try
            {
                List<PlaceOrderResponce> bookList = new List<PlaceOrderResponce>();
                PlaceOrderResponce responseData = null;
                while (dataReader.Read())
                {
                    responseData = new PlaceOrderResponce
                    {
                        OrderId = Convert.ToInt32(dataReader["OrderId"]),
                        UserId = Convert.ToInt32(dataReader["UserId"]),
                        CartId = Convert.ToInt32(dataReader["CartId"]),
                        BookId = Convert.ToInt32(dataReader["BookId"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["Authorname"].ToString(),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        TotalPrice = Convert.ToInt32(dataReader["TotalPrice"]),
                        Quantity = Convert.ToInt32(dataReader["Quantity"]),
                        IsPlace = Convert.ToBoolean(dataReader["IsPlaced"]),
                        IsActive = Convert.ToBoolean(dataReader["IsActive"])
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
    }
}
