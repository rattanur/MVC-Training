using Microsoft.EntityFrameworkCore.Migrations;

namespace SCGEX_TRAINING.Migrations
{
    public partial class update_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Slot",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Slot");
        }
    }
}
