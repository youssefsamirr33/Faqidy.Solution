using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faqidy.Infrastructure.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissingChildrens_AspNetUsers_ReporterId",
                table: "MissingChildrens");

            migrationBuilder.AddForeignKey(
                name: "FK_MissingChildrens_AspNetUsers_ReporterId",
                table: "MissingChildrens",
                column: "ReporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissingChildrens_AspNetUsers_ReporterId",
                table: "MissingChildrens");

            migrationBuilder.AddForeignKey(
                name: "FK_MissingChildrens_AspNetUsers_ReporterId",
                table: "MissingChildrens",
                column: "ReporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
