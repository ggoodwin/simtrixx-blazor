using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class stripe3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId",
                table: "StripeOrder");

            migrationBuilder.DropTable(
                name: "StripeCheckout");

            migrationBuilder.DropIndex(
                name: "IX_StripeOrder_StripeCheckoutId",
                table: "StripeOrder");

            migrationBuilder.DropColumn(
                name: "StripeCheckoutId",
                table: "StripeOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StripeCheckoutId",
                table: "StripeOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StripeCheckout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimtrixxUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StripeCheckoutId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeCheckout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeCheckout_Users_SimtrixxUserId",
                        column: x => x.SimtrixxUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_StripeCheckoutId",
                table: "StripeOrder",
                column: "StripeCheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCheckout_SimtrixxUserId",
                table: "StripeCheckout",
                column: "SimtrixxUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId",
                table: "StripeOrder",
                column: "StripeCheckoutId",
                principalTable: "StripeCheckout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
