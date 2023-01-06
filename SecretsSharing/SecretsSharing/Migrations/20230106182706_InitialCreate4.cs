using Microsoft.EntityFrameworkCore.Migrations;

namespace SecretsSharing.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserText_Users_UserId",
                table: "UserText");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserText",
                table: "UserText");

            migrationBuilder.RenameTable(
                name: "UserText",
                newName: "Text");

            migrationBuilder.RenameIndex(
                name: "IX_UserText_UserId",
                table: "Text",
                newName: "IX_Text_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Text",
                table: "Text",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Text_Users_UserId",
                table: "Text",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Text_Users_UserId",
                table: "Text");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Text",
                table: "Text");

            migrationBuilder.RenameTable(
                name: "Text",
                newName: "UserText");

            migrationBuilder.RenameIndex(
                name: "IX_Text_UserId",
                table: "UserText",
                newName: "IX_UserText_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserText",
                table: "UserText",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserText_Users_UserId",
                table: "UserText",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
