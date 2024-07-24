using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingALot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Service_BookingItem_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_items_service_id",
                table: "booking_items");

            migrationBuilder.CreateIndex(
                name: "ix_booking_items_service_id",
                table: "booking_items",
                column: "service_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_items_service_id",
                table: "booking_items");

            migrationBuilder.CreateIndex(
                name: "ix_booking_items_service_id",
                table: "booking_items",
                column: "service_id",
                unique: true);
        }
    }
}
