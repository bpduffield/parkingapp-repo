using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionMVCAppDuffield.Data.Migrations
{
    public partial class AddedLot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    LotID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotNumber = table.Column<string>(nullable: false),
                    LocationName = table.Column<string>(nullable: false),
                    LotAddress = table.Column<string>(nullable: false),
                    MaxCapacity = table.Column<int>(nullable: false),
                    CurrentOccupancy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.LotID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lots");
        }
    }
}
