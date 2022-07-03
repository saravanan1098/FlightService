using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class airline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AitlineName",
                table: "Airlines");

            migrationBuilder.AddColumn<string>(
                name: "AirlineName",
                table: "Airlines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirlineName",
                table: "Airlines");

            migrationBuilder.AddColumn<string>(
                name: "AitlineName",
                table: "Airlines",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
