using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingService.Migrations
{
    public partial class raa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightModel",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "OnewayTicketCost",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "RoundTripTicketCost",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "BusinessClassSeatTicketCost",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NonBusinessClassSeatTicketCost",
                table: "Flights",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessClassSeatTicketCost",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "NonBusinessClassSeatTicketCost",
                table: "Flights");

            migrationBuilder.AddColumn<string>(
                name: "FlightModel",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnewayTicketCost",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoundTripTicketCost",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
