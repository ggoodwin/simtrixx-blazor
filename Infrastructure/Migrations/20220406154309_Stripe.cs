using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Stripe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StripeCheckout",
                columns: table => new
                {
                    StripeCheckoutId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimtrixxUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStripeCheckoutId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeCheckout", x => x.StripeCheckoutId);
                    table.ForeignKey(
                        name: "FK_StripeCheckout_StripeCheckout_IdStripeCheckoutId",
                        column: x => x.IdStripeCheckoutId,
                        principalTable: "StripeCheckout",
                        principalColumn: "StripeCheckoutId");
                    table.ForeignKey(
                        name: "FK_StripeCheckout_Users_SimtrixxUserId",
                        column: x => x.SimtrixxUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StripeCustomer",
                columns: table => new
                {
                    StripeCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimtrixxUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStripeCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeCustomer", x => x.StripeCustomerId);
                    table.ForeignKey(
                        name: "FK_StripeCustomer_StripeCustomer_IdStripeCustomerId",
                        column: x => x.IdStripeCustomerId,
                        principalTable: "StripeCustomer",
                        principalColumn: "StripeCustomerId");
                    table.ForeignKey(
                        name: "FK_StripeCustomer_Users_SimtrixxUserId",
                        column: x => x.SimtrixxUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StripeSubscription",
                columns: table => new
                {
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimtrixxUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStripeSubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeSubscription", x => x.StripeSubscriptionId);
                    table.ForeignKey(
                        name: "FK_StripeSubscription_StripeSubscription_IdStripeSubscriptionId",
                        column: x => x.IdStripeSubscriptionId,
                        principalTable: "StripeSubscription",
                        principalColumn: "StripeSubscriptionId");
                    table.ForeignKey(
                        name: "FK_StripeSubscription_Users_SimtrixxUserId",
                        column: x => x.SimtrixxUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StripeOrder",
                columns: table => new
                {
                    StripeOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    StripeSubscriptionId = table.Column<int>(type: "int", nullable: false),
                    StripeSubscriptionId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StripeCheckoutId = table.Column<int>(type: "int", nullable: false),
                    StripeCheckoutId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StripeCustomerId = table.Column<int>(type: "int", nullable: false),
                    StripeCustomerId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimtrixxUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStripeOrderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeOrder", x => x.StripeOrderId);
                    table.ForeignKey(
                        name: "FK_StripeOrder_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StripeOrder_StripeCheckout_StripeCheckoutId1",
                        column: x => x.StripeCheckoutId1,
                        principalTable: "StripeCheckout",
                        principalColumn: "StripeCheckoutId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StripeOrder_StripeCustomer_StripeCustomerId1",
                        column: x => x.StripeCustomerId1,
                        principalTable: "StripeCustomer",
                        principalColumn: "StripeCustomerId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StripeOrder_StripeOrder_IdStripeOrderId",
                        column: x => x.IdStripeOrderId,
                        principalTable: "StripeOrder",
                        principalColumn: "StripeOrderId");
                    table.ForeignKey(
                        name: "FK_StripeOrder_StripeSubscription_StripeSubscriptionId1",
                        column: x => x.StripeSubscriptionId1,
                        principalTable: "StripeSubscription",
                        principalColumn: "StripeSubscriptionId");
                    table.ForeignKey(
                        name: "FK_StripeOrder_Users_SimtrixxUserId",
                        column: x => x.SimtrixxUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StripeCheckout_IdStripeCheckoutId",
                table: "StripeCheckout",
                column: "IdStripeCheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCheckout_SimtrixxUserId",
                table: "StripeCheckout",
                column: "SimtrixxUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCustomer_IdStripeCustomerId",
                table: "StripeCustomer",
                column: "IdStripeCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCustomer_SimtrixxUserId",
                table: "StripeCustomer",
                column: "SimtrixxUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_IdStripeOrderId",
                table: "StripeOrder",
                column: "IdStripeOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_LicenseId",
                table: "StripeOrder",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeOrder_SimtrixxUserId",
                table: "StripeOrder",
                column: "SimtrixxUserId");

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
                name: "IX_StripeSubscription_IdStripeSubscriptionId",
                table: "StripeSubscription",
                column: "IdStripeSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeSubscription_SimtrixxUserId",
                table: "StripeSubscription",
                column: "SimtrixxUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeOrder");

            migrationBuilder.DropTable(
                name: "StripeCheckout");

            migrationBuilder.DropTable(
                name: "StripeCustomer");

            migrationBuilder.DropTable(
                name: "StripeSubscription");
        }
    }
}
