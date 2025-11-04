using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsTemplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAcademicRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicRoleId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAcademicRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAcademicRoles_AcademicRoles_AcademicRoleId",
                        column: x => x.AcademicRoleId,
                        principalTable: "AcademicRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAcademicRoles_AcademicRoleId",
                table: "UserAcademicRoles",
                column: "AcademicRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAcademicRoles");

            migrationBuilder.DropTable(
                name: "AcademicRoles");
        }
    }
}
