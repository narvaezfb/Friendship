using System;
using Friendship.Data;
using Microsoft.AspNetCore.Mvc;

namespace Friendship.Controllers
{
    [Route("[controller]")]
    public class BlockController : ControllerBase
    {
        private readonly FriendshipDbContext _context;

        public BlockController(FriendshipDbContext context)
        {
            _context = context;
        }

    }
}

