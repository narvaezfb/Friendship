using System;
using Microsoft.EntityFrameworkCore;
using Friendship.Models;
using Friendship.Models.ModelConfigurations;

namespace Friendship.Data
{
	public class FriendshipDbContext: DbContext
	{
        public DbSet<UsersFriendship> UsersFriendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Block> Blocks { get; set; }

        public FriendshipDbContext(DbContextOptions<FriendshipDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersFriendshipConfiguration());
            modelBuilder.ApplyConfiguration(new FriendRequestConfiguration());
            modelBuilder.ApplyConfiguration(new BlockConfiguration());
        }
    }
}

