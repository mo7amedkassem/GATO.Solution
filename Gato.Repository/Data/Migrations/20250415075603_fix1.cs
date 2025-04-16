using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gato.Repository.Data.Migrations
{
    public partial class fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "likes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "likes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
