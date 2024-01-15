using Friendship;
using Friendship.Controllers;
using Friendship.Data;
using Friendship.Models.ModelRequests.FriendRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Newtonsoft.Json;

namespace FriendshipTests.FrienshipController
{
    [TestClass]
    public class FriendRequestControllerTests
    {
        private FriendshipDbContext _dbContext;
        private FriendRequestController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FriendshipDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            _dbContext = new FriendshipDbContext(options);
            _controller = new FriendRequestController(_dbContext);
        }

        //[TestMethod]
        //public async Task CreateFriendRequestwithValidPayload()
        //{
        //    var request = new CreateFriendRequest
        //    {
        //        SenderUserId = "a44cfaea-0766-486f-83bf-5b9cf706f9ee",
        //        ReceiverUserId = "1850d4ec-d44f-4665-9ee5-a6d92ae4af41"
        //    };

        //    var result = await _controller.CreateRequest(request) as ActionResult;

        //    Console.WriteLine("Result: " + JsonConvert.SerializeObject(result));

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //}

        [TestMethod]
        public async Task CheckRecords()
        {
            var hasRecords = false;
            var records = await _dbContext.FriendRequests.ToListAsync();

            if(records.Count >= 1)
            {
                hasRecords = true;
            }
            Assert.IsTrue(hasRecords);
        }

        //[TestMethod]
        //public async Task AcceptFriendRequestReturnsNotNull()
        //{
        //    // Arrange
        //    var request = new AnswerFriendRequest
        //    {
        //        FriendRequestId = "80c7c53a-5e62-4cc1-9311-995e08f52981",
        //        Answer = "ACCEPT"
        //    };

        //    //Act
        //    var result = await _controller.AnswerFriendRequest(request) as ActionResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public async Task AcceptFriendRequestReturnsOkObject()
        //{
        //    var request = new AnswerFriendRequest
        //    {
        //        FriendRequestId = "40000c0c-1d1c-4f3b-b84c-056a96597fbe",
        //        Answer = "ACCEPT"
        //    };

        //    var result = await _controller.AnswerFriendRequest(request) as ActionResult;

        //    // Print the actual type of result
        //    Console.WriteLine("Hello World" + result?.GetType().FullName);

        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        //}

        //[TestMethod]
        //public async Task AcceptFriendRequestReturnsCorrectOkObjectMessage()
        //{
        //    const string OkObjectMessage = "Friend request accepted and friendship created successfully";
        //    var request = new AnswerFriendRequest
        //    {
        //        FriendRequestId = "80c7c53a-5e62-4cc1-9311-995e08f52981",
        //        Answer = "ACCEPT"
        //    };

        //    var result = await _controller.AnswerFriendRequest(request) as ActionResult;
        //    var okResult = result as OkObjectResult;
        //    var actualValue = okResult.Value;

        //    Assert.AreEqual(OkObjectMessage, actualValue);

        //}
    }
}

