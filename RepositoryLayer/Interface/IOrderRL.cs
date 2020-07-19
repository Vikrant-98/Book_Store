using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        Task<PlaceOrderResponce> BookPlaceOdrder(int userID, int CartId);

        Task<bool> CancelPlaceOdrder(int userID, int orderID);

        Task<List<PlaceOrderResponce>> GetListOfPlaceOrder(int userID);
    }
}
