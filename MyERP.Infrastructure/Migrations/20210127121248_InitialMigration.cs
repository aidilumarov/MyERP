using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "Price" },
                values: new object[] { new Guid("2203c8e5-21a4-48e0-b17a-08b00d3e1702"), new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Local), 500m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "Price" },
                values: new object[] { new Guid("7a209cc2-88be-4b4d-bbd6-7106a3c6f8f5"), new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Local), 500m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "Price" },
                values: new object[] { new Guid("3a38da79-7340-4e18-ad9d-faea343aa20e"), new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Local), 500m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
