using BusinessLayer.Interface;
using CommonLayer.Services;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookStoreBL : IBookStoreBL
    {
        private IBookStoreRL Books;

        public BookStoreBL(IBookStoreRL Data)
        {
            Books = Data;
        }

        public object BooksDatails(User data)
        {
            try
            {
                var Result = Books.BooksDatails(data);
                if (!Result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
