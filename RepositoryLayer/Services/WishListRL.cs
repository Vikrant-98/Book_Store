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
        public async Task<WishListResponse> CreateNewWishList(int userID, int BookID)
        {
            try
            {
                WishListResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("spCreateNewWishList", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BookID", BookID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
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
                using (SqlCommand cmd = new SqlCommand("spGetListOfWishListByUserID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
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
        /// responce for WishList
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
                        WishListID = Convert.ToInt32(dataReader["WishListId"]),
                        UserID = Convert.ToInt32(dataReader["UserId"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["Authorname"].ToString(),
                        BookID = Convert.ToInt32(dataReader["BookId"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"])
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
                        WishListID = Convert.ToInt32(dataReader["WishListId"]),
                        UserID = Convert.ToInt32(dataReader["UserId"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["Authorname"].ToString(),
                        BookID = Convert.ToInt32(dataReader["BookId"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Price = Convert.ToInt32(dataReader["Price"])
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
