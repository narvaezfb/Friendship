using System;
using Friendship.Data;
using Microsoft.AspNetCore.Mvc;

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

	}
}

