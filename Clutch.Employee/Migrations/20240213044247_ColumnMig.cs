using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clutch_employee.Migrations
{
    public partial class ColumnMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Employees",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "PositionUniqueReferenceNumber",
                table: "Employees",
                newName: "positionUniqueReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "EmployeeStatus",
                table: "Employees",
                newName: "employeeStatus");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "employeeId");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Employees",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Employees",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "positionUniqueReferenceNumber",
                table: "Employees",
                newName: "PositionUniqueReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "employeeStatus",
                table: "Employees",
                newName: "EmployeeStatus");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Employees",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employees",
                newName: "Id");
        }
    }
}
