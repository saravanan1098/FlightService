using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingService.Migrations
{
    public partial class rabbit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<int>(nullable: false),
                    AirlineName = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ContactAddress = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(nullable: false),
                    FlightModel = table.Column<string>(nullable: true),
                    FromPlace = table.Column<string>(nullable: true),
                    ToPlace = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    ScheduledDays = table.Column<string>(nullable: true),
                    BusinessClassSeats = table.Column<int>(nullable: false),
                    NonBusinessClassSeats = table.Column<int>(nullable: false),
                    MealType = table.Column<string>(nullable: true),
                    TypeofTrip = table.Column<string>(nullable: true),
                    OnewayTicketCost = table.Column<int>(nullable: false),
                    RoundTripTicketCost = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    AirlineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seatnumbers",
                columns: table => new
                {
                    SeatId = table.Column<int>(nullable: false),
                    SeatNumber = table.Column<string>(nullable: true),
                    Typeofseat = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    FlightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seatnumbers", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seatnumbers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatnumbers_FlightId",
                table: "Seatnumbers",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seatnumbers");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");
        }
    }
}
