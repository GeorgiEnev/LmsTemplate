using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsTemplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicRoleCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicRoleCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicRoleId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicRoleCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicRoleCourses_AcademicRoles_AcademicRoleId",
                        column: x => x.AcademicRoleId,
                        principalTable: "AcademicRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicRoleCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRoleCourses_AcademicRoleId",
                table: "AcademicRoleCourses",
                column: "AcademicRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRoleCourses_CourseId",
                table: "AcademicRoleCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicRoleCourses");
        }
    }
}
