using Friendship;
using Friendship.Controllers;
using Friendship.Data;
using Friendship.Models.ModelRequests.FriendRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

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

            _dbContext = new FriendshipDbContext(options); // Initialize with a mocked context or any necessary setup
            _controller = new FriendRequestController(_dbContext);
        }

        [TestMethod]
        public async Task CreateFriendRequestwithValidPayload()
        {
            var controller = new FriendRequestController(_dbContext);
            var request = new CreateFriendRequest
            {
                SenderUserId = "a44cfaea-0766-486f-83bf-5b9cf706f9ee",
                ReceiverUserId = "1850d4ec-d44f-4665-9ee5-a6d92ae4af41"
            };

            var result = await _controller.CreateRequest(request) as ActionResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task AcceptFriendRequest()
        {
            // Arrange
            var request = new AnswerFriendRequest
            {
                FriendRequestId = "", // Provide a valid FriendRequestId from your test data
                Answer = "ACCEPT" // Provide "ACCEPT" or "DECLINE" based on your scenario
            };

            // Act
            var result = await _controller.AcceptFriendRequest(request) as ActionResult; // Call the endpoint

            // Assert
            Assert.IsNotNull(result); // Check if the result is not null
            Assert.IsInstanceOfType(result, typeof(OkObjectResult)); // Check for expected status code
            // Add more assertions based on the expected behavior of the endpoint
        }
    }
}

