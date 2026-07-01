using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FalconERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrinterSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoPrintReceipt",
                table: "SystemSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaperWidth",
                table: "SystemSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PrinterName",
                table: "SystemSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoPrintReceipt",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "PaperWidth",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "PrinterName",
                table: "SystemSettings");
        }
    }
}
