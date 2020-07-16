using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        Task<WishListResponse> CreateNewWishList(int userID, int BookId);

        Task<List<WishListResponse>> GetListOfWishList(int userID);
    }
}
