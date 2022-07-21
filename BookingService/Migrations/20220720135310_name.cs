using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingService.Migrations
{
    public partial class name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerName",
                table: "Passengers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Passengers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Passengers");

            migrationBuilder.AddColumn<string>(
                name: "PassengerName",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
