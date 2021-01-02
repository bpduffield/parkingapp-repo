using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionMVCAppDuffield.Data.Migrations
{
    public partial class AddedPermit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VisitorOrganization",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermitID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(nullable: true),
                    DepartmentAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    PermitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitAmount = table.Column<double>(nullable: false),
                    PStartDate = table.Column<DateTime>(nullable: false),
                    PEndDate = table.Column<DateTime>(nullable: false),
                    WVUEmployeeID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.PermitID);
                    table.ForeignKey(
                        name: "FK_Permits_AspNetUsers_WVUEmployeeID",
                        column: x => x.WVUEmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentID",
                table: "AspNetUsers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PermitID",
                table: "AspNetUsers",
                column: "PermitID");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_WVUEmployeeID",
                table: "Permits",
                column: "WVUEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentID",
                table: "AspNetUsers",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Permits_PermitID",
                table: "AspNetUsers",
                column: "PermitID",
                principalTable: "Permits",
                principalColumn: "PermitID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Permits_PermitID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PermitID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VisitorOrganization",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PermitID",
                table: "AspNetUsers");
        }
    }
}
