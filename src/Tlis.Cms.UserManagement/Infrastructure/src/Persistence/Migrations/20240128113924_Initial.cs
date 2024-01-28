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
                    abouth = table.Column<string>(type: "text", nullable: false),
                    profile_image_url = table.Column<string>(type: "text", nullable: true),
                    prefer_nickname_over_name = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_membership_history",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_membership_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_membership_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms_user_management",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_role_history",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    function_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    function_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_history_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "cms_user_management",
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_role_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms_user_management",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                schema: "cms_user_management",
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("1e1a6556-7e50-4e0c-b4c2-13bcea319efb"), "moderator" },
                    { new Guid("b15deaa2-3de8-42b8-813a-1df2ea764f66"), "technician" },
                    { new Guid("ed9359a3-0106-4c61-b2fb-5cb32ed4a788"), "system-admin" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_role_id",
                schema: "cms_user_management",
                table: "role",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_firstname_lastname_nickname",
                schema: "cms_user_management",
                table: "user",
                columns: new[] { "firstname", "lastname", "nickname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                schema: "cms_user_management",
                table: "user",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_user_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_role_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_user_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_membership_history",
                schema: "cms_user_management");

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
