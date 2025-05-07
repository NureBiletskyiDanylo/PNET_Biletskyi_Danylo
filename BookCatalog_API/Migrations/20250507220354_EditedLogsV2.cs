using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog_API.Migrations
{
    /// <inheritdoc />
    public partial class EditedLogsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCreateLogs_Authors_AuthorId",
                table: "BookCreateLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCreateLogs_Books_BookId",
                table: "BookCreateLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCreateLogs_Authors_AuthorId",
                table: "BookCreateLogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCreateLogs_Books_BookId",
                table: "BookCreateLogs",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCreateLogs_Authors_AuthorId",
                table: "BookCreateLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCreateLogs_Books_BookId",
                table: "BookCreateLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCreateLogs_Authors_AuthorId",
                table: "BookCreateLogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCreateLogs_Books_BookId",
                table: "BookCreateLogs",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
