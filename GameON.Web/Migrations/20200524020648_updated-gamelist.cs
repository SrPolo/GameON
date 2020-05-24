using Microsoft.EntityFrameworkCore.Migrations;

namespace GameON.Web.Migrations
{
    public partial class updatedgamelist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GameLists_GameListId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGames_GameLists_GameListEntityId",
                table: "VideoGames");

            migrationBuilder.DropIndex(
                name: "IX_VideoGames_GameListEntityId",
                table: "VideoGames");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GameListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GameListEntityId",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "GameListId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GameLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoGameId",
                table: "GameLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameLists_UserId",
                table: "GameLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameLists_VideoGameId",
                table: "GameLists",
                column: "VideoGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameLists_AspNetUsers_UserId",
                table: "GameLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameLists_VideoGames_VideoGameId",
                table: "GameLists",
                column: "VideoGameId",
                principalTable: "VideoGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameLists_AspNetUsers_UserId",
                table: "GameLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GameLists_VideoGames_VideoGameId",
                table: "GameLists");

            migrationBuilder.DropIndex(
                name: "IX_GameLists_UserId",
                table: "GameLists");

            migrationBuilder.DropIndex(
                name: "IX_GameLists_VideoGameId",
                table: "GameLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameLists");

            migrationBuilder.DropColumn(
                name: "VideoGameId",
                table: "GameLists");

            migrationBuilder.AddColumn<int>(
                name: "GameListEntityId",
                table: "VideoGames",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameListId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_GameListEntityId",
                table: "VideoGames",
                column: "GameListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameListId",
                table: "AspNetUsers",
                column: "GameListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GameLists_GameListId",
                table: "AspNetUsers",
                column: "GameListId",
                principalTable: "GameLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGames_GameLists_GameListEntityId",
                table: "VideoGames",
                column: "GameListEntityId",
                principalTable: "GameLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
