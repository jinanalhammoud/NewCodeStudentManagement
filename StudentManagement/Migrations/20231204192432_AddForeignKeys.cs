using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "classes",
                newName: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_classRegistrations_ClassId",
                table: "classRegistrations",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_classRegistrations_StudentId",
                table: "classRegistrations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_classLectures_ClassId",
                table: "classLectures",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LectureId",
                table: "Attendances",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_classLectures_LectureId",
                table: "Attendances",
                column: "LectureId",
                principalTable: "classLectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classLectures_classes_ClassId",
                table: "classLectures",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classRegistrations_Students_StudentId",
                table: "classRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classRegistrations_classes_ClassId",
                table: "classRegistrations",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_classLectures_LectureId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_classLectures_classes_ClassId",
                table: "classLectures");

            migrationBuilder.DropForeignKey(
                name: "FK_classRegistrations_Students_StudentId",
                table: "classRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_classRegistrations_classes_ClassId",
                table: "classRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_classRegistrations_ClassId",
                table: "classRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_classRegistrations_StudentId",
                table: "classRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_classLectures_ClassId",
                table: "classLectures");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_LectureId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "classes",
                newName: "Title");
        }
    }
}
