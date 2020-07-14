using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        Task<BooksResponse> AddBooks(int adminId, Books data);

        Task<List<BooksResponse>> GetListOfBooks();

        Task<List<BooksResponse>> SearchBook(BookSearchRequest bookSearch);

        Task<List<BooksResponse>> SortBooks(string sortingChoice, string sortingType);
    }
}
