using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RenameLobbyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LobbyUsers_MessageLobby_MessageLobbyId",
                table: "LobbyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageLobby_MessageLobbyId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "MessageLobby");

            migrationBuilder.RenameColumn(
                name: "MessageLobbyId",
                table: "Messages",
                newName: "LobbyId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_MessageLobbyId",
                table: "Messages",
                newName: "IX_Messages_LobbyId");

            migrationBuilder.RenameColumn(
                name: "MessageLobbyId",
                table: "LobbyUsers",
                newName: "LobbyId");

            migrationBuilder.RenameIndex(
                name: "IX_LobbyUsers_MessageLobbyId",
                table: "LobbyUsers",
                newName: "IX_LobbyUsers_LobbyId");

            migrationBuilder.CreateTable(
                name: "Lobby",
                columns: table => new
                {
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidityPeriod = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lobby", x => x.LobbyId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LobbyUsers_Lobby_LobbyId",
                table: "LobbyUsers",
                column: "LobbyId",
                principalTable: "Lobby",
                principalColumn: "LobbyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Lobby_LobbyId",
                table: "Messages",
                column: "LobbyId",
                principalTable: "Lobby",
                principalColumn: "LobbyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LobbyUsers_Lobby_LobbyId",
                table: "LobbyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Lobby_LobbyId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Lobby");

            migrationBuilder.RenameColumn(
                name: "LobbyId",
                table: "Messages",
                newName: "MessageLobbyId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_LobbyId",
                table: "Messages",
                newName: "IX_Messages_MessageLobbyId");

            migrationBuilder.RenameColumn(
                name: "LobbyId",
                table: "LobbyUsers",
                newName: "MessageLobbyId");

            migrationBuilder.RenameIndex(
                name: "IX_LobbyUsers_LobbyId",
                table: "LobbyUsers",
                newName: "IX_LobbyUsers_MessageLobbyId");

            migrationBuilder.CreateTable(
                name: "MessageLobby",
                columns: table => new
                {
                    MessageLobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ValidityPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLobby", x => x.MessageLobbyId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LobbyUsers_MessageLobby_MessageLobbyId",
                table: "LobbyUsers",
                column: "MessageLobbyId",
                principalTable: "MessageLobby",
                principalColumn: "MessageLobbyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageLobby_MessageLobbyId",
                table: "Messages",
                column: "MessageLobbyId",
                principalTable: "MessageLobby",
                principalColumn: "MessageLobbyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
