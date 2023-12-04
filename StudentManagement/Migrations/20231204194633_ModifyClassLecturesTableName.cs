using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class ModifyClassLecturesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_classLectures_LectureId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_classLectures_Classes_ClassId",
                table: "classLectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_classLectures",
                table: "classLectures");

            migrationBuilder.RenameTable(
                name: "classLectures",
                newName: "ClassLectures");

            migrationBuilder.RenameIndex(
                name: "IX_classLectures_ClassId",
                table: "ClassLectures",
                newName: "IX_ClassLectures_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassLectures",
                table: "ClassLectures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_ClassLectures_LectureId",
                table: "Attendances",
                column: "LectureId",
                principalTable: "ClassLectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Courses_CourseId",
                table: "Classes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassLectures_Classes_ClassId",
                table: "ClassLectures",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_ClassLectures_LectureId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Courses_CourseId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassLectures_Classes_ClassId",
                table: "ClassLectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassLectures",
                table: "ClassLectures");

            migrationBuilder.DropIndex(
                name: "IX_Classes_CourseId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes");

            migrationBuilder.RenameTable(
                name: "ClassLectures",
                newName: "classLectures");

            migrationBuilder.RenameIndex(
                name: "IX_ClassLectures_ClassId",
                table: "classLectures",
                newName: "IX_classLectures_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_classLectures",
                table: "classLectures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_classLectures_LectureId",
                table: "Attendances",
                column: "LectureId",
                principalTable: "classLectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classLectures_Classes_ClassId",
                table: "classLectures",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
