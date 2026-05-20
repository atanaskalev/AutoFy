using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleFixedCosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FireExtinguisherPrice",
                table: "Vehicles",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InsurancePrice",
                table: "Vehicles",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TechnicalInspectionPrice",
                table: "Vehicles",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VignettePrice",
                table: "Vehicles",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FireExtinguisherPrice",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InsurancePrice",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TechnicalInspectionPrice",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VignettePrice",
                table: "Vehicles");
        }
    }
}
