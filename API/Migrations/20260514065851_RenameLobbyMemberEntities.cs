using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RenameLobbyMemberEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LobbyUsers");

            migrationBuilder.CreateTable(
                name: "LobbyMember",
                columns: table => new
                {
                    LobbyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbyMember", x => x.LobbyMemberId);
                    table.ForeignKey(
                        name: "FK_LobbyMember_Lobby_LobbyId",
                        column: x => x.LobbyId,
                        principalTable: "Lobby",
                        principalColumn: "LobbyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LobbyMember_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LobbyMember_LobbyId",
                table: "LobbyMember",
                column: "LobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_LobbyMember_UserId",
                table: "LobbyMember",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LobbyMember");

            migrationBuilder.CreateTable(
                name: "LobbyUsers",
                columns: table => new
                {
                    LobbyUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbyUsers", x => x.LobbyUsersId);
                    table.ForeignKey(
                        name: "FK_LobbyUsers_Lobby_LobbyId",
                        column: x => x.LobbyId,
                        principalTable: "Lobby",
                        principalColumn: "LobbyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LobbyUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LobbyUsers_LobbyId",
                table: "LobbyUsers",
                column: "LobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_LobbyUsers_UserId",
                table: "LobbyUsers",
                column: "UserId");
        }
    }
}
