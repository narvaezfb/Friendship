using System;
using Friendship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Friendship.Models;
using Newtonsoft.Json.Linq;
using Friendship.Interfaces;

namespace Friendship.Controllers
{
    [Route("[controller]")]
    public class UsersFriendshipController: ControllerBase
	{
		private readonly FriendshipDbContext _context;
		private readonly IUserInformationService _userInformationService;

		public UsersFriendshipController(FriendshipDbContext context, IUserInformationService userInformationService)
		{
			_context = context;
			_userInformationService = userInformationService;
		}

		[HttpGet("Friendships/{UserId}")]
		public async Task<ActionResult> GetUserFriendships(string UserId)
		{
			try
			{
                var friendships = await _context.UsersFriendships.Where(uf => (uf.UserId1 == UserId || uf.UserId2 == UserId))
            											         .OrderBy(uf => uf.DateCreated)
            												     .ToListAsync();
				if(!friendships.Any() || friendships == null)
				{
					return NotFound("User Does not have any frienships");
				}

                // Initialize a list of anonymous objects
                var friendshipsList = new List<object>();

                // iterate through friend list and create new objects
                foreach (var friendship in friendships)
				{
					string friendId = GetFriendId(friendship, UserId);
					string friendName = await GetFriendName(friendId);

					var newFriendship = new
					{
						friendshipId = friendship.FriendshipId,
						friendId,
						friendName
					};

                    friendshipsList.Add(newFriendship);
                }

                return Ok(friendshipsList);
            }
            catch (DbUpdateException ex)
			{
				return StatusCode(500, $"Database error: {ex.Message}");
			}
			catch(Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}
		[HttpDelete("Friendship/{FriendshipId}")]
		public async Task<ActionResult> DeleteFriendship(string FriendshipId)
		{
			try
			{
				var friendship = await _context.UsersFriendships.FindAsync(FriendshipId);

				if(friendship == null)
				{
					return NotFound("Not friendship found with that ID");
				}

				_context.UsersFriendships.Remove(friendship);
				await _context.SaveChangesAsync();

				return Ok("Friendship Deleted successfully");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

		}

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetFriendId(UsersFriendship friendship, string UserId)
        {
            string friendId = null;

            if (friendship.UserId1 == UserId)
            {
                friendId = friendship.UserId2;
            }
            else
            {
                friendId = friendship.UserId1;
            }
            return friendId;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetFriendName(string friendId)
		{
			try
			{
                string? friendName = "Error";
                HttpResponseMessage response = await _userInformationService.RetrieveUserInformation(friendId);

                if (response.IsSuccessStatusCode)
                {
                    // Extract attributes from the response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var userObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                    // Extract additional attributes if needed
                    friendName = (userObject is JObject && ((JObject)userObject).ContainsKey("username")) ? ((JObject)userObject)["username"]?.ToString() : "error";
                }

                return friendName;
            }
			catch(Exception ex)
			{
				return "Error";
			}
        }
    }
}

