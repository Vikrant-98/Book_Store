using CommonLayer.Request;
using CommonLayer.Responce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        Task<BooksResponse> AddBooks(int adminId, Books data);

        Task<BooksResponse> AddImage(int adminId, int BookID, IFormFile Image);

        Task<List<BooksResponse>> GetListOfBooks();

        Task<bool> DeleteBooks(int BookId);

        Task<BooksResponse> UpdateBooks(int BooksId, UpdateBooks data);

        Task<List<BooksResponse>> SearchBook(string bookSearch);

        Task<List<BooksResponse>> SortBooks(string sortingChoice, string sortingType);
    }
}
