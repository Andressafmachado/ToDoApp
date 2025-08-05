using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyToDoApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_composite_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToDos_ParentToDoId_Priority_Auth0UserId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_Priority_Auth0UserId",
                table: "ToDos");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ParentToDoId_Priority_Auth0UserId",
                table: "ToDos",
                columns: new[] { "ParentToDoId", "Priority", "Auth0UserId" },
                unique: true,
                filter: "\"ParentToDoId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_Priority_Auth0UserId",
                table: "ToDos",
                columns: new[] { "Priority", "Auth0UserId" },
                unique: true,
                filter: "\"ParentToDoId\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToDos_ParentToDoId_Priority_Auth0UserId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_Priority_Auth0UserId",
                table: "ToDos");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ParentToDoId_Priority_Auth0UserId",
                table: "ToDos",
                columns: new[] { "ParentToDoId", "Priority", "Auth0UserId" },
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_Priority_Auth0UserId",
                table: "ToDos",
                columns: new[] { "Priority", "Auth0UserId" },
                unique: true,
                filter: "[ParentId] IS NULL");
        }
    }
}
