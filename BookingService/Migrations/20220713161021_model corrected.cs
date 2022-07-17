using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingService.Migrations
{
    public partial class modelcorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "BookingName",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDateTime",
                table: "Airlines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Airlines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDateTime",
                table: "Airlines");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Airlines");

            migrationBuilder.AddColumn<string>(
                name: "PassengerId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
