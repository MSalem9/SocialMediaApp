using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Migrations
{
    /// <inheritdoc />
    public partial class GroupAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GroupAdminId",
                table: "Groups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupAdminId",
                table: "Groups",
                column: "GroupAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_GroupAdminId",
                table: "Groups",
                column: "GroupAdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_GroupAdminId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupAdminId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupAdminId",
                table: "Groups");
        }
    }
}
