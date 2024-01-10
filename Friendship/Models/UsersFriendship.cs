using System;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel.DataAnnotations;

namespace Friendship.Models
{
	public class UsersFriendship
	{
        public string FriendshipId { get; set; }
        
        [Required(ErrorMessage = "User ID 1 is required")]
        public string UserId1 { get; set; }
        
        [Required(ErrorMessage = "User ID 2 is required")]
        public string UserId2 { get; set; }
        
        public DateTime DateCreated { get; set; }


        public UsersFriendship(string userId1, string userId2)
        {
            FriendshipId = Guid.NewGuid().ToString();
            UserId1 = userId1;
            UserId2 = userId2;
        }
	}
}

