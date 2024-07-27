using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingALot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_version_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "drivers",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "drivers");
        }
    }
}
