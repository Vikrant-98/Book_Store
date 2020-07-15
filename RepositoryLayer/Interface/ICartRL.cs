using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        Task<CartBookResponse> AddBookIntoCart(int userID, Cart cart);

        Task<List<CartBookResponse>> GetListOfBooksInCart(int userID);

        Task<bool> DeleteBookFromCart(int userID, int cartID);
    }
}
