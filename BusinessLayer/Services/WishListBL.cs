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
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL _wishList;

        public WishListBL(IWishListRL wishList)
        {
            _wishList = wishList;
        }

        public async Task<WishListResponse> CreateNewWishList(int userID, int BookId)
        {
            try
            {
                if (userID <= 0 || BookId <= 0)
                {
                    return null;
                }
                else
                {
                    return await _wishList.CreateNewWishList(userID, BookId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<WishListResponse>> GetListOfWishList(int userID)
        {
            try
            {
                if (userID <= 0 )
                {
                    return null;
                }
                else
                {
                    return await _wishList.GetListOfWishList(userID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBookFromWishList(int userID, int wishListID, WishList wishListBook)
        {
            try
            {
                if (userID <= 0 || wishListBook.BookID <= 0)
                {
                    return false;
                }
                else
                {
                    return await _wishList.DeleteBookFromWishList(userID, wishListID, wishListBook);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CartBookResponse> MoveToCart(int userID, int wishListID, WishList wishListBook)
        {
            try
            {
                if (userID <= 0 || wishListBook == null)
                {
                    return null;
                }
                else
                {
                    return await _wishList.MoveToCart(userID, wishListID, wishListBook);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
