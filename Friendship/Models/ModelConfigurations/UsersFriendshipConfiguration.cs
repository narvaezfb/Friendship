using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Friendship.Models.ModelConfigurations
{
	public class UsersFriendshipConfiguration: IEntityTypeConfiguration<UsersFriendship>
    {
        public void Configure(EntityTypeBuilder<UsersFriendship> builder)
        {
            builder.HasKey(uf => uf.FriendshipId);
            builder.Property(uf => uf.UserId1).IsRequired().HasMaxLength(255);
            builder.Property(uf => uf.UserId2).IsRequired().HasMaxLength(255);
            builder.Property(uf => uf.DateCreated);

            //Indexes 
            builder.HasIndex(uf => uf.UserId1);
            builder.HasIndex(uf => uf.UserId2);

            //Timestamp 
            builder.Property(uf => uf.DateCreated).HasColumnType("timestamp with time zone");
            builder.Property(uf => uf.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}

