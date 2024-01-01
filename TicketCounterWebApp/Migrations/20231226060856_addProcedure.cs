using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketCounterWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCode = @"create proc spDeleteInvoice
@InvoiceId int

as

delete from TicketInfos where InvoiceId = @InvoiceId
delete from Invoices where Id = @InvoiceId";

            migrationBuilder.Sql(sqlCode);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            {
                migrationBuilder.Sql("drop proc spDeleteInvoice");
            }

        }
    }
}
