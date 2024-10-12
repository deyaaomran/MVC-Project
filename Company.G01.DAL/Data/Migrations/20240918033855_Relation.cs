using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.G01.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkForId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_WorkForId",
                table: "employee",
                column: "WorkForId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_WorkForId",
                table: "employee",
                column: "WorkForId",
                principalTable: "department",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_WorkForId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_WorkForId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "WorkForId",
                table: "employee");
        }
    }
}
