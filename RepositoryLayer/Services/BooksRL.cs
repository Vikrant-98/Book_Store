using CommonLayer.Request;
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
    public class BooksRL : IBooksRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;
        //constructor 
        public BooksRL(IConfiguration configuration)
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
        /// Add Books Details
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BooksResponse> AddBooks(int adminId,Books data)
        {
            try
            {
                BooksResponse responseData = null;

                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = DateTime.Now;

                SQLConnection();

                using (SqlCommand command = new SqlCommand("spAddBooksDetail", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookName", data.BookName);
                    command.Parameters.AddWithValue("@AdminId", adminId);
                    command.Parameters.AddWithValue("@AuthorName", data.AuthorName);
                    command.Parameters.AddWithValue("@Price", data.Price);
                    command.Parameters.AddWithValue("@Pages", data.Pages);
                    command.Parameters.AddWithValue("@Description", data.Description);
                    command.Parameters.AddWithValue("@Available", data.Available);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = BooksResponseModel(dataReader);
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
        /// Get List of Books from the database
        /// </summary>
        /// <returns>If Data Fetched Successfully return Resonse Data else null or Exception</returns>
        public async Task<List<BooksResponse>> GetListOfBooks()
        {
            try
            {
                List<BooksResponse> bookList = null;
                SQLConnection();
                bookList = new List<BooksResponse>();
                using (SqlCommand command = new SqlCommand("spListOfBooks", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
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
        /// Delete Books from the database
        /// </summary>
        /// <returns>Data get deleted or Exception get thrown</returns>
        public async Task<List<BooksResponse>> DeleteBooks(int BookId)
        {
            try
            {
                List<BooksResponse> deleteBook = null;
                SQLConnection();
                deleteBook = new List<BooksResponse>();
                using (SqlCommand command = new SqlCommand("spDeleteBookById", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", BookId);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    deleteBook = ListBookResponseModel(dataReader);
                };
                return deleteBook;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Books Details
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BooksResponse> UpdateBooks(int BooksId, Books data)
        {
            try
            {
                BooksResponse responseData = null;

                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = DateTime.Now;

                SQLConnection();

                using (SqlCommand command = new SqlCommand("spUpdateBookById", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", BooksId);
                    command.Parameters.AddWithValue("@BookName", data.BookName);
                    command.Parameters.AddWithValue("@AuthorName", data.AuthorName);
                    command.Parameters.AddWithValue("@Price", data.Price);
                    command.Parameters.AddWithValue("@Pages", data.Pages);
                    command.Parameters.AddWithValue("@Description", data.Description);
                    command.Parameters.AddWithValue("@Available", data.Available);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    responseData = BooksResponseModel(dataReader);
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
        /// Search book by Name in the database
        /// </summary>
        /// <param name="bookSearch">Book Search Data</param>
        /// <returns>If Data Fetched return Response Data else null or Exception</returns>
        public async Task<List<BooksResponse>> BookSearch(string bookSearch)
        {
            try
            {
                List<BooksResponse> bookList = new List<BooksResponse>();
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spSearchBookByName", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@search", bookSearch);

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
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
        /// Ascending/Decending sorting
        /// </summary>
        /// <param name="sortingChoice"></param>
        /// <param name="sortingType"></param>
        /// <returns>Return sorted books Ascending/Decending</returns>
        public async Task<List<BooksResponse>> SortBooks(string sortingChoice, string sortingType)
        {
            try
            {
                List<BooksResponse> bookList = new List<BooksResponse>();
                SQLConnection();
                using (SqlCommand command = new SqlCommand("spSortedBooksDetails", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SortCol", CategoryChoice(sortingChoice));
                    command.Parameters.AddWithValue("@SortDir", CategoryTypeChoice(sortingType));

                    conn.Open();
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    bookList = ListBookResponseModel(dataReader);
                }
                return bookList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Category Choice
        /// </summary>
        /// <param name="sortingChoice"></param>
        /// <returns>Return Category Choice</returns>
        public int CategoryChoice(string sortingChoice)
        {
            switch (sortingChoice)
            {
                case "BookName" :
                    return 1;
                case "Price":
                    return 3;
                case "Pages":
                    return 4;
                default:
                    return 1;
            }
        }
        /// <summary>
        /// Category Type Choice
        /// </summary>
        /// <param name="sortingType"></param>
        /// <returns>Return Category Type Choice</returns>
        public string CategoryTypeChoice(string sortingType)
        {
            switch (sortingType)
            {
                case "Ascending":
                    return "asc";
                case "Decending":
                    return "desc";
                default:
                    return "asc";
            }
        }

        /// <summary>
        /// List of Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return Book Response Data</returns>
        private BooksResponse BooksResponseModel(SqlDataReader dataReader)
        {
            try
            {
                BooksResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new BooksResponse
                    {
                        BookId = Convert.ToInt32(dataReader["BooksId"]),
                        AdminId = Convert.ToInt32(dataReader["AdminId"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["Authorname"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Available = Convert.ToInt32(dataReader["Available"])
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
        private List<BooksResponse> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<BooksResponse> bookList = new List<BooksResponse>();
                BooksResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new BooksResponse
                    {
                        BookId = Convert.ToInt32(dataReader["BooksId"]),
                        AdminId = Convert.ToInt32(dataReader["AdminId"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["Authorname"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Available = Convert.ToInt32(dataReader["Available"])
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
