using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCMS.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class asasasas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentBy",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentBy",
                table: "Comments",
                column: "CommentBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentBy",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentBy",
                table: "Comments",
                column: "CommentBy",
                unique: true);
        }
    }
}
