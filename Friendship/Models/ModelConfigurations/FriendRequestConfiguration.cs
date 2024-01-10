using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Friendship.Models.ModelConfigurations
{
	public class FriendRequestConfiguration: IEntityTypeConfiguration<FriendRequest>
    {
		public void Configure(EntityTypeBuilder<FriendRequest> builder)
		{
            builder.HasKey(fr => fr.FriendRequestId);
            builder.Property(fr => fr.SenderUserId).IsRequired();
            builder.Property(fr => fr.ReceiverUserId).IsRequired();
            builder.Property(fr => fr.Status);
            builder.Property(fr => fr.RequestDateSend);

            //Indexes 
            builder.HasIndex(fr => fr.SenderUserId);
            builder.HasIndex(fr => fr.ReceiverUserId);

            //Timestamp 
            builder.Property(fr => fr.RequestDateSend).HasColumnType("timestamp with time zone");

            //Default Values on Creation
            builder.Property(fr => fr.RequestDateSend).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(fr => fr.Status).HasDefaultValue("PENDING");
        }
    
	}
}

