using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedItem_Books_BookId",
                table: "ReservedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedItem_User_UserId",
                table: "ReservedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservedItem",
                table: "ReservedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ReservedItem",
                newName: "ReservedItems");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_User_FinCode",
                table: "Users",
                newName: "IX_Users_FinCode");

            migrationBuilder.RenameIndex(
                name: "IX_ReservedItem_UserId_Status",
                table: "ReservedItems",
                newName: "IX_ReservedItems_UserId_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ReservedItem_BookId_StartDate_EndDate",
                table: "ReservedItems",
                newName: "IX_ReservedItems_BookId_StartDate_EndDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservedItems",
                table: "ReservedItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedItems_Books_BookId",
                table: "ReservedItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedItems_Users_UserId",
                table: "ReservedItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedItems_Books_BookId",
                table: "ReservedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedItems_Users_UserId",
                table: "ReservedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservedItems",
                table: "ReservedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "ReservedItems",
                newName: "ReservedItem");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FinCode",
                table: "User",
                newName: "IX_User_FinCode");

            migrationBuilder.RenameIndex(
                name: "IX_ReservedItems_UserId_Status",
                table: "ReservedItem",
                newName: "IX_ReservedItem_UserId_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ReservedItems_BookId_StartDate_EndDate",
                table: "ReservedItem",
                newName: "IX_ReservedItem_BookId_StartDate_EndDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservedItem",
                table: "ReservedItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedItem_Books_BookId",
                table: "ReservedItem",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedItem_User_UserId",
                table: "ReservedItem",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
