using CommonLayer.Request;
using CommonLayer.Responce;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public WishListRL(IConfiguration configuration)
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
        /// Create New Wish List in the database
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="wishList">Wish List Data</param>
        /// <returns>If data added successfully return Response Data else null or Exception</returns>
        public async Task<WishListResponse> CreateNewWishList(int userID, WishList data)
        {
            try
            {
                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = createDate;

                WishListResponse responseData = null;
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spCreateNewWishList", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@BookID", data.BookID);
                    command.Parameters.AddWithValue("@IsMoved", false);
                    command.Parameters.AddWithValue("@IsDelete", false);
                    command.Parameters.AddWithValue("@CreateDate", createDate);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = WishListResponseModel(dataReader);
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
        /// <returns>If Data Fetched Successfully return Response Data else null or Exception</returns>
        public async Task<List<WishListResponse>> GetListOfWishList(int userID)
        {
            try
            {
                List<WishListResponse> responseData = null;

                SQLConnection();
                using (SqlCommand command = new SqlCommand("spGetListOfWishListByUserID", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = AllWishListResponseModel(dataReader);
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
        /// Add Move Book Details in the database
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="wishListID">WishListID</param>
        /// <param name="wishListBook">Wish List Book Data</param>
        /// <returns>If Data Found return Response Data else null or Exeption</returns>
        public async Task<CartBookResponse> MoveToCart(int userID, Wish_List data)
        {
            try
            {
                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = createDate;
                CartBookResponse responseData = null;
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spMoveBookToCart", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@WishListID", data.WishListID);
                    command.Parameters.AddWithValue("@CreateDate", createDate);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = CartRL.BookResponseModel(dataReader);
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
        /// Delete Book From the Wish List in the database
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="wishList">Wish List Data</param>
        /// <returns>If Data Deleted Successfull return true else false or Exception</returns>
        public async Task<bool> DeleteBookFromWishList(int userID, int wishListID)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("spDeleteBookFromWishList", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@WishListID", wishListID);

                    conn.Open();
                    int count = await cmd.ExecuteNonQueryAsync();
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
        /// Responce for WishList
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private WishListResponse WishListResponseModel(SqlDataReader dataReader)
        {
            try
            {
                WishListResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new WishListResponse
                    {
                        WishListID = Convert.ToInt32(dataReader["WishListID"]),
                        UserID = Convert.ToInt32(dataReader["UserID"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["AuthorName"].ToString(),
                        BookID = Convert.ToInt32(dataReader["BookID"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]),
                        IsMoved = Convert.ToBoolean(dataReader["IsMoved"]),
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
        /// <summary>
        /// List of Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return List of Book Response Data</returns>
        private List<WishListResponse> AllWishListResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<WishListResponse> bookList = new List<WishListResponse>();
                WishListResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new WishListResponse
                    {
                        WishListID = Convert.ToInt32(dataReader["WishListID"]),
                        UserID = Convert.ToInt32(dataReader["UserID"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["AuthorName"].ToString(),
                        BookID = Convert.ToInt32(dataReader["BookID"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]),
                        IsMoved = Convert.ToBoolean(dataReader["IsMoved"]),
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
    }
}
