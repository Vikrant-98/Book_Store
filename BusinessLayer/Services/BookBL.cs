using BusinessLayer.Interface;
using CommonLayer.Request;
using CommonLayer.Responce;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private IBooksRL _books;

        public BookBL(IBooksRL Data)
        {
            _books = Data;
        }

        public async Task<BooksResponse> AddBooks(int adminId, Books data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await _books.AddBooks(adminId,data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BooksResponse>> GetListOfBooks()
        {
            try
            {
                return await _books.GetListOfBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BooksResponse>> DeleteBooks(int BookId)
        {
            try
            {
                return await _books.DeleteBooks(BookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BooksResponse> UpdateBooks(int BooksId, Books data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await _books.UpdateBooks(BooksId, data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BooksResponse>> SearchBook(string bookSearch)
        {
            try
            {
                if (bookSearch == null)
                    return null;
                else
                    return await _books.BookSearch(bookSearch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<BooksResponse>> SortBooks(string sortingChoice, string sortingType)
        {
            try
            {
                return await _books.SortBooks(sortingChoice,sortingType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
