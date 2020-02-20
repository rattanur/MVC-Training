using Microsoft.EntityFrameworkCore.Migrations;

namespace SCGEX_TRAINING.Migrations
{
    public partial class scgex2_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Machines_MachineId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductInfos_ProductInfoCode",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductInfoCode",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Machines_MachineId",
                table: "Products",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductInfos_ProductInfoCode",
                table: "Products",
                column: "ProductInfoCode",
                principalTable: "ProductInfos",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Machines_MachineId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductInfos_ProductInfoCode",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductInfoCode",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Machines_MachineId",
                table: "Products",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductInfos_ProductInfoCode",
                table: "Products",
                column: "ProductInfoCode",
                principalTable: "ProductInfos",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
