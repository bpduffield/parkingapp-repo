using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionMVCAppDuffield.Data.Migrations
{
    public partial class AddedLotStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotStatuses",
                columns: table => new
                {
                    LotStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfDay = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ParkingAmount = table.Column<double>(nullable: false),
                    LotID = table.Column<int>(nullable: false),
                    LotTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotStatuses", x => x.LotStatusID);
                    table.ForeignKey(
                        name: "FK_LotStatuses_Lots_LotID",
                        column: x => x.LotID,
                        principalTable: "Lots",
                        principalColumn: "LotID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotStatuses_LotTypes_LotTypeID",
                        column: x => x.LotTypeID,
                        principalTable: "LotTypes",
                        principalColumn: "LotTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotStatuses_LotID",
                table: "LotStatuses",
                column: "LotID");

            migrationBuilder.CreateIndex(
                name: "IX_LotStatuses_LotTypeID",
                table: "LotStatuses",
                column: "LotTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotStatuses");
        }
    }
}
