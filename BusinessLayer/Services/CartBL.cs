﻿using BusinessLayer.Interface;
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

        private readonly ICartRL _cartRL;

        public CartBL(ICartRL cart)
        {
            _cartRL = cart;
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
                    return await _cartRL.GetListOfBooksInCart(userID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartBookResponse> AddBookIntoCart(int userID, Cart data)
        {
            try
            {
                if (userID <= 0 || data == null)
                {
                    return null;
                }
                else
                {
                    return await _cartRL.AddBookIntoCart(userID, data);
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
                    return await _cartRL.DeleteBookFromCart(userID, cartID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
