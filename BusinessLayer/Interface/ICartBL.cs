using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        Task<CartBookResponse> AddBookIntoCart(int userID, int BookID);

        Task<List<CartBookResponse>> GetListOfBooksInCart(int userID);

        Task<bool> DeleteBookFromCart(int userID, int cartID);
    }
}
