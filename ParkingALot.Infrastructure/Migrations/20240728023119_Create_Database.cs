using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingALot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drivers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    total_points = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_drivers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parking_lot_owners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parking_lot_owners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    characteristics_brand = table.Column<string>(type: "text", nullable: false),
                    characteristics_model = table.Column<string>(type: "text", nullable: false),
                    characteristics_year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "fk_vehicles_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parking_lots",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parking_lot_owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    address_country = table.Column<string>(type: "text", nullable: false),
                    address_state = table.Column<string>(type: "text", nullable: false),
                    address_city = table.Column<string>(type: "text", nullable: false),
                    address_street = table.Column<string>(type: "text", nullable: false),
                    price_per_hour_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    price_per_hour_currency = table.Column<string>(type: "text", nullable: false),
                    open_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    close_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parking_lots", x => x.id);
                    table.ForeignKey(
                        name: "fk_parking_lots_parking_lot_owner_parking_lot_owner_id",
                        column: x => x.parking_lot_owner_id,
                        principalTable: "parking_lot_owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    parking_lot_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price_for_period_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    price_for_period_currency = table.Column<string>(type: "text", nullable: false),
                    services_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    services_price_currency = table.Column<string>(type: "text", nullable: false),
                    points_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    points_discount_currency = table.Column<string>(type: "text", nullable: false),
                    total_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_price_currency = table.Column<string>(type: "text", nullable: false),
                    range_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    range_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_one_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    completed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cancelled_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookings", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookings_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bookings_parking_lots_parking_lot_id",
                        column: x => x.parking_lot_id,
                        principalTable: "parking_lots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parking_lot_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    price_currency = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_services", x => x.id);
                    table.ForeignKey(
                        name: "fk_services_parking_lots_parking_lot_id",
                        column: x => x.parking_lot_id,
                        principalTable: "parking_lots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    booking_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    price_currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_items_bookings_booking_id",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_booking_items_service_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_booking_items_booking_id",
                table: "booking_items",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_items_service_id",
                table: "booking_items",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_driver_id",
                table: "bookings",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_parking_lot_id",
                table: "bookings",
                column: "parking_lot_id");

            migrationBuilder.CreateIndex(
                name: "ix_drivers_email",
                table: "drivers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_parking_lot_owners_email",
                table: "parking_lot_owners",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_parking_lots_parking_lot_owner_id",
                table: "parking_lots",
                column: "parking_lot_owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_services_parking_lot_id",
                table: "services",
                column: "parking_lot_id");

            migrationBuilder.CreateIndex(
                name: "ix_vehicles_driver_id",
                table: "vehicles",
                column: "driver_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_items");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "drivers");

            migrationBuilder.DropTable(
                name: "parking_lots");

            migrationBuilder.DropTable(
                name: "parking_lot_owners");
        }
    }
}
