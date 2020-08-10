using Book_Store.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Services;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;
using Xunit.Sdk;

namespace BookStoreTestCases
{
    public class UnitTest1
    {
        UsersController userController;
        BooksController bookController;
        CartController cartController;

        private readonly IUserBL _IUserBL;
        private readonly IBookBL _IBookStoreBL;
        private readonly ICartBL _ICartBL;

        private readonly IUserRL _IUserRL;
        private readonly IBooksRL _IBookStoreRL;
        private readonly ICartRL _ICartRL;

        public readonly IConfiguration configuration;

        public UnitTest1()
        {

            //IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.AddJsonFile("appsettings.json");
            //configuration = configurationBuilder.Build();

            //configuration.AddJsonFile("appsettings.json");
            //_configuration = configuration.Build();
            //_adminRepository = new AdminRepository(_configuration);
            //_adminBusiness = new AdminBusiness(_adminRepository);

            _IUserRL = new UserRL(configuration);
            _IUserBL = new UserBL(_IUserRL);
            userController = new UsersController(_IUserBL, configuration);

            _IBookStoreRL = new BooksRL(configuration);
            _IBookStoreBL = new BookBL(_IBookStoreRL);
            bookController = new BooksController(_IBookStoreBL);

            _ICartRL = new CartRL(configuration);
            _ICartBL = new CartBL(_ICartRL);
            cartController = new CartController(_ICartBL);

        }

        bool SuccessTrue = true;
        bool SuccessFalse = false;

        [Fact]
        public void AdminRegistration_ValidData_Return_OkResult()
        {
            var controller = new UsersController(_IUserBL, configuration);
            var adminData = new User
            {
                FirstName = "Abcd",
                LastName = "Efgh",
                EmailID = "abcd@gmail.com",
                Password = "Abcd1234",
            };

            var data = controller.UserRegister(adminData);

            Assert.IsType<OkObjectResult>(data);
        }
    }
}
