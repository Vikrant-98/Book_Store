﻿using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        Task<WishListResponse> CreateNewWishList(int userID, WishList data);

        Task<List<WishListResponse>> GetListOfWishList(int userID);

        Task<bool> DeleteBookFromWishList(int userID, int wishListID);

        Task<CartBookResponse> MoveToCart(int userID, Wish_List data);
    }
}
