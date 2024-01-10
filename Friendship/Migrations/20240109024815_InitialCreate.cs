using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendship.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    BlockId = table.Column<string>(type: "text", nullable: false),
                    BlockingUserId = table.Column<string>(type: "text", nullable: false),
                    BlockedUserId = table.Column<string>(type: "text", nullable: false),
                    DateBlocked = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.BlockId);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    FriendRequestId = table.Column<string>(type: "text", nullable: false),
                    SenderUserId = table.Column<string>(type: "text", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValue: "PENDING"),
                    RequestDateSend = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.FriendRequestId);
                });

            migrationBuilder.CreateTable(
                name: "UsersFriendships",
                columns: table => new
                {
                    FriendshipId = table.Column<string>(type: "text", nullable: false),
                    UserId1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserId2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFriendships", x => x.FriendshipId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockedUserId",
                table: "Blocks",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockingUserId",
                table: "Blocks",
                column: "BlockingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverUserId",
                table: "FriendRequests",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderUserId",
                table: "FriendRequests",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriendships_UserId1",
                table: "UsersFriendships",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriendships_UserId2",
                table: "UsersFriendships",
                column: "UserId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "UsersFriendships");
        }
    }
}
