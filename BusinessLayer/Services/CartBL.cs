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
    public class CartBL : ICartBL
    {

        private readonly ICartRL _cart;

        public CartBL(ICartRL cart)
        {
            _cart = cart;
        }

        public async Task<List<CartBookResponse>> GetListOfBooksInCart(int userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return null;
                }
                else
                {
                    return await _cart.GetListOfBooksInCart(userID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartBookResponse> AddBookIntoCart(int userID, int BookID)
        {
            try
            {
                if (userID <= 0 || BookID <= 0)
                {
                    return null;
                }
                else
                {
                    return await _cart.AddBookIntoCart(userID, BookID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBookFromCart(int userID, int cartID)
        {
            try
            {
                if (userID <= 0 || cartID <= 0)
                {
                    return false;
                }
                else
                {
                    return await _cart.DeleteBookFromCart(userID, cartID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
