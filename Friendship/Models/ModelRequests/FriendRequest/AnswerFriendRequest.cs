using System;
using System.ComponentModel.DataAnnotations;

namespace Friendship.Models.ModelRequests.FriendRequest
{
	public class AnswerFriendRequest
	{
		[Required(ErrorMessage ="Friend request ID is required")]
		public string FriendRequestId { get; set; }

		[Required(ErrorMessage ="Status is required")]
		public string Answer { get; set; }
    }
}

