using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsTemplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUniqueIndexOnAcademicRoleCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAcademicRoles_UserId_AcademicRoleId",
                table: "UserAcademicRoles");

            migrationBuilder.DropIndex(
                name: "IX_AcademicRoleCourses_AcademicRoleId",
                table: "AcademicRoleCourses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAcademicRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRoleCourses_AcademicRoleId_CourseId",
                table: "AcademicRoleCourses",
                columns: new[] { "AcademicRoleId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AcademicRoleCourses_AcademicRoleId_CourseId",
                table: "AcademicRoleCourses");

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

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRoleCourses_AcademicRoleId",
                table: "AcademicRoleCourses",
                column: "AcademicRoleId");
        }
    }
}
