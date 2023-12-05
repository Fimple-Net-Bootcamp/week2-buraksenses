using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.API.Migrations
{
    /// <inheritdoc />
    public partial class objecttypechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CelestialObjects");

            migrationBuilder.AddColumn<string>(
                name: "ObjectType",
                table: "CelestialObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectType",
                table: "CelestialObjects");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CelestialObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
