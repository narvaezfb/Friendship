using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Friendship.Models
{
	public class Block
	{   
        public string BlockId { get; set; }

        [Required(ErrorMessage ="Blocking User Id is required")]
        public string BlockingUserId { get; set; }
        
        [Required(ErrorMessage ="Blocked User Id is required")]
        public string BlockedUserId { get; set; }
        
        public DateTime DateBlocked { get; set; }

        public Block(string blockingUserId, string blockedUserId)
		{
            BlockId = Guid.NewGuid().ToString();
            BlockingUserId = blockingUserId;
            BlockedUserId = blockedUserId;
		}
	}
}

