using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBooksRL
    {
        Task<BooksResponse> AddBooks(int adminId, Books data);

        Task<List<BooksResponse>> GetListOfBooks();

        Task<List<BooksResponse>> DeleteBooks(int BookId);

        Task<BooksResponse> UpdateBooks(int BooksId, Books data);

        Task<List<BooksResponse>> BookSearch(string bookSearch);

        Task<List<BooksResponse>> SortBooks(string sortingChoice, string sortingType);
    }
}
