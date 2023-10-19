using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CarWorkshopRatingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CreatedById",
                table: "Ratings",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatedById",
                table: "Ratings",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatedById",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CreatedById",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Ratings");
        }
    }
}
