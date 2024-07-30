using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingALot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_OutboxMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_parking_lots_parking_lot_owner_parking_lot_owner_id",
                table: "parking_lots");

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    ocurred_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    error = table.Column<string>(type: "text", nullable: true),
                    process_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_parking_lots_parking_lot_owners_parking_lot_owner_id",
                table: "parking_lots",
                column: "parking_lot_owner_id",
                principalTable: "parking_lot_owners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_parking_lots_parking_lot_owners_parking_lot_owner_id",
                table: "parking_lots");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.AddForeignKey(
                name: "fk_parking_lots_parking_lot_owner_parking_lot_owner_id",
                table: "parking_lots",
                column: "parking_lot_owner_id",
                principalTable: "parking_lot_owners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
