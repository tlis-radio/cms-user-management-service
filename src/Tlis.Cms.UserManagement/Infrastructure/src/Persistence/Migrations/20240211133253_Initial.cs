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
                name: "membership",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membership", x => x.id);
                });

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
                    profile_image_id = table.Column<Guid>(type: "uuid", nullable: true),
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
                    membership_id = table.Column<Guid>(type: "uuid", nullable: false),
                    change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_membership_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_membership_history_membership_membership_id",
                        column: x => x.membership_id,
                        principalSchema: "cms_user_management",
                        principalTable: "membership",
                        principalColumn: "id");
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
                table: "membership",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { new Guid("80126b05-9dab-4709-aa6a-39baa5bafe79"), "Active" },
                    { new Guid("a7c0bea2-2812-40b6-9836-d4b5accae95a"), "Archive" },
                    { new Guid("cfaeecff-d26b-44f2-bfa1-c80ab79983a9"), "Postponed" }
                });

            migrationBuilder.InsertData(
                schema: "cms_user_management",
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("4971ba3e-5a40-42cf-b9d9-17c49d9da309"), "dramaturge-dj" },
                    { new Guid("8570d900-396f-4b78-bf69-5065e2fe8acf"), "marketing-pr" },
                    { new Guid("a9a9040c-fbbd-4aa6-b0dc-56de7265ee7f"), "system-admin" },
                    { new Guid("cbec6f46-a2e8-4fb3-a126-fe4e51e5ead2"), "technician" },
                    { new Guid("ed7cafb5-f2bf-4fbe-972c-18fa4f056b69"), "moderator" },
                    { new Guid("f5bdf1df-8406-44d6-b1a1-942f7bde7b23"), "web-developer" },
                    { new Guid("fab1118e-38b9-4164-b222-66378654fcf4"), "graphic" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_membership_id",
                schema: "cms_user_management",
                table: "membership",
                column: "id");

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
                name: "ix_user_membership_history_membership_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "membership_id");

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
                name: "membership",
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
