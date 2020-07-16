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
    }
}
