using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace store.Migrations
{
    public partial class Paymentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "Paiements");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Paiements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Paiements");

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "Paiements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
