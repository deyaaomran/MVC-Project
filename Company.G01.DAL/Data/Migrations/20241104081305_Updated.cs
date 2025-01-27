using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.G01.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_WorkForId",
                table: "employee");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "employee",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "Departmentid",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaralryForId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => x.id);
                    table.ForeignKey(
                        name: "FK_attendance_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryRange = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "salary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary", x => x.id);
                    table.ForeignKey(
                        name: "FK_salary_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "employeeProject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoursWorked = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeProject", x => x.id);
                    table.ForeignKey(
                        name: "FK_employeeProject_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_employeeProject_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_Departmentid",
                table: "employee",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_employee_PositionId",
                table: "employee",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_SaralryForId",
                table: "employee",
                column: "SaralryForId");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_EmployeeId",
                table: "attendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeProject_EmployeeId",
                table: "employeeProject",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeProject_ProjectId",
                table: "employeeProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_salary_EmployeeId",
                table: "salary",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_Departmentid",
                table: "employee",
                column: "Departmentid",
                principalTable: "department",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_WorkForId",
                table: "employee",
                column: "WorkForId",
                principalTable: "department",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_position_PositionId",
                table: "employee",
                column: "PositionId",
                principalTable: "position",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_salary_SaralryForId",
                table: "employee",
                column: "SaralryForId",
                principalTable: "salary",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_Departmentid",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_WorkForId",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_position_PositionId",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_salary_SaralryForId",
                table: "employee");

            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "employeeProject");

            migrationBuilder.DropTable(
                name: "position");

            migrationBuilder.DropTable(
                name: "salary");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropIndex(
                name: "IX_employee_Departmentid",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_PositionId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_SaralryForId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "Departmentid",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "SaralryForId",
                table: "employee");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "employee",
                newName: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_WorkForId",
                table: "employee",
                column: "WorkForId",
                principalTable: "department",
                principalColumn: "id");
        }
    }
}
