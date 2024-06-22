using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_membership_history_membership_membership_id",
                schema: "cms_user_management",
                table: "user_membership_history");

            migrationBuilder.DropForeignKey(
                name: "fk_user_membership_history_user_user_id",
                schema: "cms_user_management",
                table: "user_membership_history");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_history_role_role_id",
                schema: "cms_user_management",
                table: "user_role_history");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_history_user_user_id",
                schema: "cms_user_management",
                table: "user_role_history");

            migrationBuilder.AddForeignKey(
                name: "fk_user_membership_history_membership_membership_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "membership_id",
                principalSchema: "cms_user_management",
                principalTable: "membership",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_membership_history_user_user_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "user_id",
                principalSchema: "cms_user_management",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_history_role_role_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "role_id",
                principalSchema: "cms_user_management",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_history_user_user_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "user_id",
                principalSchema: "cms_user_management",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_membership_history_membership_membership_id",
                schema: "cms_user_management",
                table: "user_membership_history");

            migrationBuilder.DropForeignKey(
                name: "fk_user_membership_history_user_user_id",
                schema: "cms_user_management",
                table: "user_membership_history");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_history_role_role_id",
                schema: "cms_user_management",
                table: "user_role_history");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_history_user_user_id",
                schema: "cms_user_management",
                table: "user_role_history");

            migrationBuilder.AddForeignKey(
                name: "fk_user_membership_history_membership_membership_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "membership_id",
                principalSchema: "cms_user_management",
                principalTable: "membership",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_membership_history_user_user_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "user_id",
                principalSchema: "cms_user_management",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_history_role_role_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "role_id",
                principalSchema: "cms_user_management",
                principalTable: "role",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_history_user_user_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "user_id",
                principalSchema: "cms_user_management",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
