using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionMVCAppDuffield.Data.Migrations
{
    public partial class ModifiedPermitToAddTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeWhenParkingPermitWasAssigned",
                table: "Permits",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParkingEmployeeWhoAssignedPermitID",
                table: "Permits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeWhenParkingPermitWasAssigned",
                table: "Permits");

            migrationBuilder.DropColumn(
                name: "ParkingEmployeeWhoAssignedPermitID",
                table: "Permits");
        }
    }
}
