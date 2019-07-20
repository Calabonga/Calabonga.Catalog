using Microsoft.EntityFrameworkCore.Migrations;

namespace Calabonga.Catalog.Data.Migrations
{
    public partial class PublishedPropertAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Reviews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Categories");
        }
    }
}
