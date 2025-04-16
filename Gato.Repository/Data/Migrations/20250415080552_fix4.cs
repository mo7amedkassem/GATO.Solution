using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gato.Repository.Data.Migrations
{
    public partial class fix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
