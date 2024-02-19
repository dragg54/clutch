using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clutch_position.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Positions",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "PositionStatus",
                table: "Positions",
                newName: "positionStatus");

            migrationBuilder.RenameColumn(
                name: "PositionName",
                table: "Positions",
                newName: "positionName");

            migrationBuilder.RenameColumn(
                name: "PositionDescription",
                table: "Positions",
                newName: "positionDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "salary",
                table: "Positions",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "positionStatus",
                table: "Positions",
                newName: "PositionStatus");

            migrationBuilder.RenameColumn(
                name: "positionName",
                table: "Positions",
                newName: "PositionName");

            migrationBuilder.RenameColumn(
                name: "positionDescription",
                table: "Positions",
                newName: "PositionDescription");
        }
    }
}
