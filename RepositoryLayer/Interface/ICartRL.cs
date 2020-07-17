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
        Task<CartBookResponse> AddBookIntoCart(int userID, int BookID);

        Task<List<CartBookResponse>> GetListOfBooksInCart(int userID);

        Task<bool> DeleteBookFromCart(int userID, int cartID);

        Task<PlaceOrderResponce> BookPlaceOdrder(int userID, PlaceOrder Info);

        Task<PlaceOrderResponce> CancelPlaceOdrder(int userID, CalcelOrder Info);
    }
}
