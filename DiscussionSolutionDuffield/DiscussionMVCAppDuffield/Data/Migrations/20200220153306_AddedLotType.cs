using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionMVCAppDuffield.Data.Migrations
{
    public partial class AddedLotType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotTypes",
                columns: table => new
                {
                    LotTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotTypes", x => x.LotTypeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotTypes");
        }
    }
}
