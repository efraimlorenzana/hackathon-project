using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPontsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0fc314a-bb58-4ce3-b479-2fc760220185");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a914ca92-661b-48d4-bc28-43ca7d6c7f1e");

            migrationBuilder.CreateTable(
                name: "EarnedPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    DateEarned = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnedPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EarnedPoints_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a66dd0c2-2007-4282-8aa6-ac78bb4a99c9", "e4c5c6c0-d327-4e36-a4d8-fa29ffa43ac4", "Retailer", "RETAILER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15fcbcd2-4e69-465b-b4a6-52c25b2df285", "c4977d22-7e30-4883-b106-571b25710ee4", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_EarnedPoints_PersonId",
                table: "EarnedPoints",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EarnedPoints");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15fcbcd2-4e69-465b-b4a6-52c25b2df285");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a66dd0c2-2007-4282-8aa6-ac78bb4a99c9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0fc314a-bb58-4ce3-b479-2fc760220185", "e8bcaa1c-6d6b-4911-8245-2291445e7fa4", "Retailer", "RETAILER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a914ca92-661b-48d4-bc28-43ca7d6c7f1e", "6f561a64-d2c4-4fa5-84e3-d5d6beda5912", "Customer", "CUSTOMER" });
        }
    }
}
