using CommonLayer.Request;
using CommonLayer.Responce;
using CommonLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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
                DateTime modifiedDate = createDate;

                SQLConnection();

                using (SqlCommand command = new SqlCommand("spAddBooksDetail", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookName", data.BookName);
                    command.Parameters.AddWithValue("@AdminID", adminId);
                    command.Parameters.AddWithValue("@AuthorName", data.AuthorName);
                    command.Parameters.AddWithValue("@Price", data.Price);
                    command.Parameters.AddWithValue("@Pages", data.Pages);
                    command.Parameters.AddWithValue("@Description", data.Description);
                    command.Parameters.AddWithValue("@BooksAvailable", data.Available);
                    command.Parameters.AddWithValue("@IsDelete", false);
                    command.Parameters.AddWithValue("@CreateDate", createDate);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

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
        /// Add Image
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="BookID"></param>>
        /// <param name="Image"></param>
        /// <returns></returns>
        public async Task<BooksResponse> AddImage(int adminId,int BookID,IFormFile Image)
        {
            try
            {
                BooksResponse responseData = null;

                DateTime modifiedDate = DateTime.Now;

                Account account = new Account(
                    _configuration["CloudinarySettings:CloudName"],
                    _configuration["CloudinarySettings:ApiKey"],
                    _configuration["CloudinarySettings:ApiSecret"]);
                var path = Image.OpenReadStream();
                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Image.FileName,path)
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                SQLConnection();

                using (SqlCommand command = new SqlCommand("spUpdateBookById", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AdminID", adminId);
                    command.Parameters.AddWithValue("@BookID", BookID);
                    command.Parameters.AddWithValue("@Image", uploadResult.Url.ToString());
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

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
        public async Task<bool> DeleteBooks(int BookId)
        {
            try
            {
                
                SQLConnection();
                
                using (SqlCommand command = new SqlCommand("spDeleteBookById", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", BookId);
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
        /// Add Books Details
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BooksResponse> UpdateBooks(int BooksId, UpdateBooks data)
        {
            try
            {
                BooksResponse responseData = null;

                DateTime modifiedDate = DateTime.Now;

                SQLConnection();

                using (SqlCommand command = new SqlCommand("spUpdateBookById", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", BooksId);
                    command.Parameters.AddWithValue("@BookName", data.BookName);
                    command.Parameters.AddWithValue("@AuthorName", data.AuthorName);
                    command.Parameters.AddWithValue("@Price", data.Price);
                    command.Parameters.AddWithValue("@Pages", data.Pages);
                    command.Parameters.AddWithValue("@Description", data.Description);
                    command.Parameters.AddWithValue("@BooksAvailable", data.Available);
                    command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

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
                        BookId = Convert.ToInt32(dataReader["BookID"]),
                        AdminId = Convert.ToInt32(dataReader["AdminID"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["AuthorName"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Available = Convert.ToInt32(dataReader["BooksAvailable"]),
                        IsDeleted = Convert.ToBoolean(dataReader["IsDelete"]),
                        Image = dataReader["Images"].ToString()

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
                        BookId = Convert.ToInt32(dataReader["BookID"]),
                        AdminId = Convert.ToInt32(dataReader["AdminID"]),
                        BookName = dataReader["BookName"].ToString(),
                        AuthorName = dataReader["AuthorName"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        Pages = Convert.ToInt32(dataReader["Pages"]),
                        Available = Convert.ToInt32(dataReader["BooksAvailable"]),
                        IsDeleted = Convert.ToBoolean(dataReader["IsDelete"]),
                        Image = dataReader["Images"].ToString()
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
