using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms_user_management");

            migrationBuilder.CreateTable(
                name: "role",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    profile_image_url = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    member_since_date = table.Column<DateOnly>(type: "date", nullable: false),
                    membership_ended_date = table.Column<DateOnly>(type: "date", nullable: true),
                    membership_ended_reason = table.Column<string>(type: "text", nullable: true),
                    external_id = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_role_history",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    function_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    function_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    user_foreign_key = table.Column<Guid>(type: "uuid", nullable: false),
                    role_foreign_key = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_history_role_role_id",
                        column: x => x.role_foreign_key,
                        principalSchema: "cms_user_management",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_history_user_user_id",
                        column: x => x.user_foreign_key,
                        principalSchema: "cms_user_management",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "cms_user_management",
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("763e0b4e-742a-44c4-8a08-0b45d2f34b32"), "moderator" },
                    { new Guid("ba78b12a-020b-434e-981d-aaee9b0f2bd6"), "technician" },
                    { new Guid("bd3fbc38-0a5e-49b4-8190-37b41308e00a"), "system-admin" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_firstname_lastname_nickname",
                schema: "cms_user_management",
                table: "user",
                columns: new[] { "firstname", "lastname", "nickname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_role_foreign_key",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "role_foreign_key");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_user_foreign_key",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "user_foreign_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_role_history",
                schema: "cms_user_management");

            migrationBuilder.DropTable(
                name: "role",
                schema: "cms_user_management");

            migrationBuilder.DropTable(
                name: "user",
                schema: "cms_user_management");
        }
    }
}
