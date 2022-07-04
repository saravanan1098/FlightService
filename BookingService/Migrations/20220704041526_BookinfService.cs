using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingService.Migrations
{
    public partial class BookinfService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerId = table.Column<string>(nullable: true),
                    MailId = table.Column<string>(nullable: true),
                    PNR = table.Column<string>(nullable: true),
                    BookingDateTime = table.Column<DateTime>(nullable: false),
                    FlightNumber = table.Column<int>(nullable: false),
                    NumberofSeats = table.Column<int>(nullable: false),
                    TotalCost = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerName = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    MealType = table.Column<string>(nullable: true),
                    SeatNumber = table.Column<int>(nullable: false),
                    BookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passengers_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_BookingId",
                table: "Passengers",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
