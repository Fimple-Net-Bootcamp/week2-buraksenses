using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.API.Migrations.WeatherAppAuthDb
{
    /// <inheritdoc />
    public partial class rolesupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A7D0EE93-2B1F-4BC5-9C58-C70279CF68E3",
                column: "NormalizedName",
                value: "WRITER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A7D0EE93-2B1F-4BC5-9C58-C70279CF68E3",
                column: "NormalizedName",
                value: "WRİTER");
        }
    }
}
