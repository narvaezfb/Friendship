using System;
using Friendship.Data;
using Friendship.Models;
using Friendship.Models.ModelRequests.FriendRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Friendship.Controllers
{
    [Route("[controller]")]
    public class FriendRequestController : ControllerBase
    {
        private readonly FriendshipDbContext _context;

        public FriendRequestController(FriendshipDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateRequest([FromBody] CreateFriendRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Request Data");
                }

                if(await DoesFriendRequestExist(model.SenderUserId, model.ReceiverUserId))
                {
                    return BadRequest("Friend request already exists");
                }

                FriendRequest friendRequest = new FriendRequest(model.SenderUserId, model.ReceiverUserId);

                _context.FriendRequests.Add(friendRequest);
                await _context.SaveChangesAsync();

                return Ok(friendRequest);

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database exception: {ex.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
        }

        [HttpGet("GetReceivedFriendRequestsByUser/{UserId}")]
        public async Task<ActionResult> GetReceivedFriendRequestsByUser(string UserId)
        {
            try
            {
                var pendingFriendRequests = await _context.FriendRequests.Where(fr => (fr.ReceiverUserId == UserId &&
                                                                                       fr.Status == "PENDING"))
                                                                         .OrderBy(fr => fr.RequestDateSend)
                                                                         .ToListAsync();

                if(pendingFriendRequests == null || !pendingFriendRequests.Any())
                {
                    return NotFound("No friend requests were found for this user");
                }

                return Ok(pendingFriendRequests);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, $"Database exception: {ex.Message}");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error exception: {ex.Message}");
            }
        }

        [HttpPatch("AnswerFriendRequest")]
        public async Task<ActionResult> AnswerFriendRequest([FromBody] AnswerFriendRequest model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid Payload for Answering friend request");
                }

                var friendRequest = await _context.FriendRequests.FindAsync(model.FriendRequestId);

                if (friendRequest == null || friendRequest.Status != "PENDING")
                {
                    return NotFound("No friend request found with that ID or not in pending status");
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Update friend request status
                        if (model.Answer == "ACCEPT")
                        {
                            friendRequest.Status = "APPROVED";
                            await _context.SaveChangesAsync();

                            UsersFriendship usersFriendship = new(friendRequest.SenderUserId, friendRequest.ReceiverUserId);
                            _context.UsersFriendships.Add(usersFriendship);
                            await _context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            return Ok("Friend request accepted and friendship created successfully");
                        }
                        else if(model.Answer == "DECLINE")
                        {
                            friendRequest.Status = "REJECTED";
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();
                            return Ok("Friend request rejected successfully");
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return BadRequest("Invalid Answer");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and rollback the transaction if an error occurs
                        await transaction.RollbackAsync();
                        return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
                    }
                }
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, $"Database error occurred: {e.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> DoesFriendRequestExist(string senderId, string receiverId)
        {
            try
            {
                var existingFriendRequest = await _context.FriendRequests.FirstOrDefaultAsync(fr => (fr.SenderUserId == senderId &&
                                                                    fr.ReceiverUserId == receiverId));
                return existingFriendRequest != null;
            }
            catch(Exception )
            {
                throw; 
            }        
        }

    }
}

