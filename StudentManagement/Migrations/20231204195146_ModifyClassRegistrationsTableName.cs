using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class ModifyClassRegistrationsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classRegistrations_Classes_ClassId",
                table: "classRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_classRegistrations_Students_StudentId",
                table: "classRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_classRegistrations",
                table: "classRegistrations");

            migrationBuilder.RenameTable(
                name: "classRegistrations",
                newName: "ClassRegistrations");

            migrationBuilder.RenameIndex(
                name: "IX_classRegistrations_StudentId",
                table: "ClassRegistrations",
                newName: "IX_ClassRegistrations_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_classRegistrations_ClassId",
                table: "ClassRegistrations",
                newName: "IX_ClassRegistrations_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRegistrations",
                table: "ClassRegistrations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRegistrations_Classes_ClassId",
                table: "ClassRegistrations",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRegistrations_Students_StudentId",
                table: "ClassRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRegistrations_Classes_ClassId",
                table: "ClassRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRegistrations_Students_StudentId",
                table: "ClassRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRegistrations",
                table: "ClassRegistrations");

            migrationBuilder.RenameTable(
                name: "ClassRegistrations",
                newName: "classRegistrations");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRegistrations_StudentId",
                table: "classRegistrations",
                newName: "IX_classRegistrations_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRegistrations_ClassId",
                table: "classRegistrations",
                newName: "IX_classRegistrations_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_classRegistrations",
                table: "classRegistrations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_classRegistrations_Classes_ClassId",
                table: "classRegistrations",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classRegistrations_Students_StudentId",
                table: "classRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
