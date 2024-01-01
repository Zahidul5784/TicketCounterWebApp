using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketCounterWebApp.Migrations
{
    /// <inheritdoc />
    public partial class imageUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Invoices");
        }
    }
}
