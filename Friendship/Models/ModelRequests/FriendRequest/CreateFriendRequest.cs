using System;
using System.ComponentModel.DataAnnotations;

namespace Friendship.Models.ModelRequests.FriendRequest
{
	public class CreateFriendRequest
	{
        [Required(ErrorMessage = "Request Sender ID is required")]
        public string SenderUserId { get; set; }

        [Required(ErrorMessage = "Request Receiver ID is required")]
        public string ReceiverUserId { get; set; }

	}
}

