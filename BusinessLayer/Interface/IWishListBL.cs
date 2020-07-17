using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        Task<WishListResponse> CreateNewWishList(int userID, int BookId);

        Task<List<WishListResponse>> GetListOfWishList(int userID);

        Task<bool> DeleteBookFromWishList(int userID, int wishListID, WishList wishListBook);

        Task<CartBookResponse> MoveToCart(int userID, int wishListID, WishList wishListBook);

    }
}
