using CommonLayer.Request;
using CommonLayer.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        Task<PlaceOrderResponce> BookPlaceOdrder(int userID, int CartId);

        Task<bool> CancelPlaceOdrder(int userID, int orderID);

        Task<List<PlaceOrderResponce>> GetListOfBooks(int userID);
    }
}
