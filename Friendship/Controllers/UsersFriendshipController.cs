using System;
using Friendship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Friendship.Controllers
{
    [Route("[controller]")]
    public class UsersFriendshipController: ControllerBase
	{
		private readonly FriendshipDbContext _context;

		public UsersFriendshipController(FriendshipDbContext context)
		{
			_context = context;
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
				return Ok(friendships);
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
	}
}

