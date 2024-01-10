using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Friendship.Models.ModelConfigurations
{
	public class BlockConfiguration: IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.HasKey(b => b.BlockId);
            builder.Property(b => b.BlockingUserId).IsRequired();
            builder.Property(b => b.BlockedUserId).IsRequired();
            builder.Property(b => b.DateBlocked);

            //Indexes 
            builder.HasIndex(b => b.BlockingUserId);
            builder.HasIndex(b => b.BlockedUserId);

            //Timestamp 
            builder.Property(b => b.DateBlocked).HasColumnType("timestamp with time zone");

            //Default Values on Creation
            builder.Property(b => b.DateBlocked).HasDefaultValueSql("CURRENT_TIMESTAMP");
           
        }
	}
}

