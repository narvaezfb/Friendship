using System;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
using System.Xml;
using System.ComponentModel.DataAnnotations;

namespace Friendship.Models
{
    public class FriendRequest
	{       
        public string FriendRequestId { get; set; }
        
        [Required(ErrorMessage = "Request Sender ID is required")]
        public string SenderUserId { get; set; }
       
        [Required(ErrorMessage = "Request Receiver ID is required")]
        public string ReceiverUserId { get; set; }
        
        public string? Status { get; set; }

        public DateTime RequestDateSend { get; set; }

        public FriendRequest(string senderUserId, string receiverUserId)
		{
            FriendRequestId = Guid.NewGuid().ToString();
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
		}
	}
}

