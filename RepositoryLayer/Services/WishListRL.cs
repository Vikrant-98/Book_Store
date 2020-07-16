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
                    while (dataReader.Read())
                    {
                        responseData = new WishListResponse
                        {
                            UserID = Convert.ToInt32(dataReader["UserId"]),
                            BookName = dataReader["BookName"].ToString(),
                            AuthorName = dataReader["Authorname"].ToString(),
                            BookID = Convert.ToInt32(dataReader["BookId"]),
                            Pages = Convert.ToInt32(dataReader["Pages"]),
                            Price = Convert.ToInt32(dataReader["Price"])
                        };
                    }
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
