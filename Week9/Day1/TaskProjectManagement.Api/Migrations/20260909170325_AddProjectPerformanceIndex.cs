using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskProjectManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectPerformanceIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId_CreatedAt",
                table: "Projects",
                columns: new[] { "OwnerId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerId_CreatedAt",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");
        }
    }
}
