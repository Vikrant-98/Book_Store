﻿using CommonLayer.Request;
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
        public async Task<CartBookResponse> AddBookIntoCart(int userID, Cart cart)
        {
            try
            {
                CartBookResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("spBookIntoCart", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userID);
                    cmd.Parameters.AddWithValue("@BookId", cart.BookID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    responseData = BookResponseModel(dataReader);
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
                using (SqlCommand cmd = new SqlCommand("spGetBooksByUserId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    bookList = ListBookResponseModel(dataReader);
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
                using (SqlCommand cmd = new SqlCommand("spDeleteCartByUserId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userID);
                    cmd.Parameters.AddWithValue("@CartId", cartID);

                    conn.Open();
                    int count = await cmd.ExecuteNonQueryAsync();
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
        public static CartBookResponse BookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                CartBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new CartBookResponse
                    {
                        CartID = Convert.ToInt32(dataReader["CartId"]),
                        BookID = Convert.ToInt32(dataReader["BookId"]),
                        BookName = dataReader["BookName"].ToString(),
                        Author = dataReader["Authorname"].ToString(),
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
                        CartID = Convert.ToInt32(dataReader["CartId"]),
                        BookID = Convert.ToInt32(dataReader["BookId"]),
                        BookName = dataReader["BookName"].ToString(),
                        Author = dataReader["Authorname"].ToString(),
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