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
        Task<PlaceOrderResponce> BookPlaceOdrder(int userID, PlaceOrder data);

        Task<bool> CancelPlaceOdrder(int userID, int orderID);

        Task<List<PlaceOrderResponce>> GetListOfBooks(int userID);

        Task<AddressResponce> Address(int userID, Address data);
    }
}
