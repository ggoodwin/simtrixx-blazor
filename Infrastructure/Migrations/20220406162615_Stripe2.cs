using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Stripe2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeCheckout_StripeCheckout_IdStripeCheckoutId",
                table: "StripeCheckout");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeCustomer_StripeCustomer_IdStripeCustomerId",
                table: "StripeCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId1",
                table: "StripeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeCustomer_StripeCustomerId1",
                table: "StripeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeOrder_IdStripeOrderId",
                table: "StripeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeSubscription_StripeSubscriptionId1",
                table: "StripeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeSubscription_StripeSubscription_IdStripeSubscriptionId",
                table: "StripeSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeSubscription",
                table: "StripeSubscription");

            migrationBuilder.DropIndex(
                name: "IX_StripeSubscription_IdStripeSubscriptionId",
                table: "StripeSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeOrder",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_IdStripeOrderId",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeCheckoutId1",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeCustomerId1",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeSubscriptionId1",
                table: "StripeOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeCustomer",
                table: "StripeCustomer");

            migrationBuilder.DropIndex(
                name: "IX_StripeCustomer_IdStripeCustomerId",
                table: "StripeCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeCheckout",
                table: "StripeCheckout");

            migrationBuilder.DropIndex(
                name: "IX_StripeCheckout_IdStripeCheckoutId",
                table: "StripeCheckout");

            migrationBuilder.DropColumn(
                name: "IdStripeSubscriptionId",
                table: "StripeSubscription");

            migrationBuilder.DropColumn(
                name: "IdStripeOrderId",
                table: "StripeOrder");

            migrationBuilder.DropColumn(
                name: "StripeCheckoutId1",
                table: "StripeOrder");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId1",
                table: "StripeOrder");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId1",
                table: "StripeOrder");

            migrationBuilder.DropColumn(
                name: "IdStripeCustomerId",
                table: "StripeCustomer");

            migrationBuilder.DropColumn(
                name: "IdStripeCheckoutId",
                table: "StripeCheckout");

            migrationBuilder.AlterColumn<string>(
                name: "StripeSubscriptionId",
                table: "StripeSubscription",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StripeSubscription",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "StripeOrderId",
                table: "StripeOrder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StripeOrder",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "StripeCustomerId",
                table: "StripeCustomer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StripeCustomer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "StripeCheckoutId",
                table: "StripeCheckout",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StripeCheckout",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeSubscription",
                table: "StripeSubscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeOrder",
                table: "StripeOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeCustomer",
                table: "StripeCustomer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeCheckout",
                table: "StripeCheckout",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeCheckoutId",
                table: "StripeOrder",
                column: "StripeCheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeCustomerId",
                table: "StripeOrder",
                column: "StripeCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeSubscriptionId",
                table: "StripeOrder",
                column: "StripeSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId",
                table: "StripeOrder",
                column: "StripeCheckoutId",
                principalTable: "StripeCheckout",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeCustomer_StripeCustomerId",
                table: "StripeOrder",
                column: "StripeCustomerId",
                principalTable: "StripeCustomer",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeSubscription_StripeSubscriptionId",
                table: "StripeOrder",
                column: "StripeSubscriptionId",
                principalTable: "StripeSubscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId",
                table: "StripeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeCustomer_StripeCustomerId",
                table: "StripeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeSubscription_StripeSubscriptionId",
                table: "StripeOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeSubscription",
                table: "StripeSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeOrder",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeCheckoutId",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeCustomerId",
                table: "StripeOrder");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeSubscriptionId",
                table: "StripeOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeCustomer",
                table: "StripeCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StripeCheckout",
                table: "StripeCheckout");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StripeSubscription");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StripeOrder");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StripeCustomer");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StripeCheckout");

            migrationBuilder.AlterColumn<string>(
                name: "StripeSubscriptionId",
                table: "StripeSubscription",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdStripeSubscriptionId",
                table: "StripeSubscription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeOrderId",
                table: "StripeOrder",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdStripeOrderId",
                table: "StripeOrder",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeCheckoutId1",
                table: "StripeOrder",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId1",
                table: "StripeOrder",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionId1",
                table: "StripeOrder",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeCustomerId",
                table: "StripeCustomer",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdStripeCustomerId",
                table: "StripeCustomer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeCheckoutId",
                table: "StripeCheckout",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdStripeCheckoutId",
                table: "StripeCheckout",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeSubscription",
                table: "StripeSubscription",
                column: "StripeSubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeOrder",
                table: "StripeOrder",
                column: "StripeOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeCustomer",
                table: "StripeCustomer",
                column: "StripeCustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StripeCheckout",
                table: "StripeCheckout",
                column: "StripeCheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeSubscription_IdStripeSubscriptionId",
                table: "StripeSubscription",
                column: "IdStripeSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_IdStripeOrderId",
                table: "StripeOrder",
                column: "IdStripeOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeCheckoutId1",
                table: "StripeOrder",
                column: "StripeCheckoutId1");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeCustomerId1",
                table: "StripeOrder",
                column: "StripeCustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeSubscriptionId1",
                table: "StripeOrder",
                column: "StripeSubscriptionId1");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCustomer_IdStripeCustomerId",
                table: "StripeCustomer",
                column: "IdStripeCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCheckout_IdStripeCheckoutId",
                table: "StripeCheckout",
                column: "IdStripeCheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeCheckout_StripeCheckout_IdStripeCheckoutId",
                table: "StripeCheckout",
                column: "IdStripeCheckoutId",
                principalTable: "StripeCheckout",
                principalColumn: "StripeCheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeCustomer_StripeCustomer_IdStripeCustomerId",
                table: "StripeCustomer",
                column: "IdStripeCustomerId",
                principalTable: "StripeCustomer",
                principalColumn: "StripeCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId1",
                table: "StripeOrder",
                column: "StripeCheckoutId1",
                principalTable: "StripeCheckout",
                principalColumn: "StripeCheckoutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeCustomer_StripeCustomerId1",
                table: "StripeOrder",
                column: "StripeCustomerId1",
                principalTable: "StripeCustomer",
                principalColumn: "StripeCustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeOrder_IdStripeOrderId",
                table: "StripeOrder",
                column: "IdStripeOrderId",
                principalTable: "StripeOrder",
                principalColumn: "StripeOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeSubscription_StripeSubscriptionId1",
                table: "StripeOrder",
                column: "StripeSubscriptionId1",
                principalTable: "StripeSubscription",
                principalColumn: "StripeSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeSubscription_StripeSubscription_IdStripeSubscriptionId",
                table: "StripeSubscription",
                column: "IdStripeSubscriptionId",
                principalTable: "StripeSubscription",
                principalColumn: "StripeSubscriptionId");
        }
    }
}
