using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haram.RemittanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class modifyRemittanceWithReceiverInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverFirstName",
                table: "RemittanceSystemRemittance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverLirstName",
                table: "RemittanceSystemRemittance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverPhone",
                table: "RemittanceSystemRemittance",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverFirstName",
                table: "RemittanceSystemRemittance");

            migrationBuilder.DropColumn(
                name: "ReceiverLirstName",
                table: "RemittanceSystemRemittance");

            migrationBuilder.DropColumn(
                name: "ReceiverPhone",
                table: "RemittanceSystemRemittance");
        }
    }
}
