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
        Task<PlaceOrderResponce> BookPlaceOdrder(int userID, PlaceOrder data);

        Task<bool> CancelPlaceOdrder(int userID, int orderID);

        Task<List<PlaceOrderResponce>> GetListOfPlaceOrder(int userID);

        Task<AddressResponce> Address(int userID, Address data);
    }
}
