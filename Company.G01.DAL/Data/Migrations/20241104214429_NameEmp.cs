using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.G01.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class NameEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "employee");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "employee",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "employee",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
