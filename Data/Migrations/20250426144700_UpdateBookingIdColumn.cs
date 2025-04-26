using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Booking_BookingId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Booking_TravelPackageId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BookingId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "paymentId",
                table: "Booking");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payment",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                schema: "dbo",
                table: "Payment",
                newName: "PriceCurrency");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                schema: "dbo",
                table: "Payment",
                newName: "PriceAmount");

            migrationBuilder.AlterColumn<string>(
                name: "StripePaymentIntentId",
                schema: "dbo",
                table: "Payment",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "Payment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethodId",
                schema: "dbo",
                table: "Payment",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TravelPackageId",
                table: "Booking",
                column: "TravelPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                schema: "dbo",
                table: "Payment",
                column: "BookingId");


            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Booking_BookingId",
                schema: "dbo",
                table: "Payment",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Booking_BookingId",
                schema: "dbo",
                table: "Payment");


            migrationBuilder.DropIndex(
                name: "IX_Booking_TravelPackageId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BookingId",
                schema: "dbo",
                table: "Payment");


            migrationBuilder.RenameTable(
                name: "Payment",
                schema: "dbo",
                newName: "Payment");

            migrationBuilder.RenameColumn(
                name: "PriceCurrency",
                table: "Payment",
                newName: "Price_Currency");

            migrationBuilder.RenameColumn(
                name: "PriceAmount",
                table: "Payment",
                newName: "Price_Amount");

            migrationBuilder.AddColumn<int>(
                name: "paymentId",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripePaymentIntentId",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethodId",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TravelPackageId",
                table: "Booking",
                column: "TravelPackageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                table: "Payment",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Booking_BookingId",
                table: "Payment",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");
        }
    }
}
