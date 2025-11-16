using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsTemplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicRoleCourseRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAcademicRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserAcademicRoles_UserId_AcademicRoleId",
                table: "UserAcademicRoles",
                columns: new[] { "UserId", "AcademicRoleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAcademicRoles_UserId_AcademicRoleId",
                table: "UserAcademicRoles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAcademicRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
