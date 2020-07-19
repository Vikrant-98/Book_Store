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
        private IBooksRL _booksRL;

        public BookBL(IBooksRL Data)
        {
            _booksRL = Data;
        }

        public async Task<BooksResponse> AddBooks(int adminId, Books data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await _booksRL.AddBooks(adminId,data);
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
                return await _booksRL.GetListOfBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBooks(int BookID)
        {
            try
            {
                if (BookID < 0)
                {
                    return false;
                }
                else
                {
                    return await _booksRL.DeleteBooks(BookID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BooksResponse> UpdateBooks(int BooksId, UpdateBooks data)
        {
            try
            {
                if (data == null)
                    return null;
                else
                    return await _booksRL.UpdateBooks(BooksId, data);
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
                    return await _booksRL.BookSearch(bookSearch);
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
                return await _booksRL.SortBooks(sortingChoice,sortingType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
