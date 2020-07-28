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
    public class OrderBL : IOrderBL
    {

        private readonly IOrderRL _orderRL;

        public OrderBL(IOrderRL order)
        {
            _orderRL = order;
        }

        public async Task<List<PlaceOrderResponce>> GetListOfBooks(int userID)
        {
            try
            {
                return await _orderRL.GetListOfPlaceOrder(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlaceOrderResponce> BookPlaceOdrder(int userID, PlaceOrder data)
        {
            try
            {
                if (userID < 0 || data == null)
                {
                    return null;
                }
                else
                {
                    return await _orderRL.BookPlaceOdrder(userID, data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CancelPlaceOdrder(int userID, int orderID)
        {
            try
            {
                if (userID < 0 || orderID < 0)
                {
                    return false;
                }
                else
                {
                    return await _orderRL.CancelPlaceOdrder(userID, orderID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AddressResponce> Address(int userID, Address data)
        {
            try
            {
                if (userID < 0 || data == null)
                {
                    return null;
                }
                else
                {
                    return await _orderRL.Address(userID, data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
