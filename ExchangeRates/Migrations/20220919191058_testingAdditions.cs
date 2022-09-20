using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRates.Migrations
{
    public partial class testingAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(8,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });
            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Date", "Value" },
                values: new object[,]
                {
                    { DateTime.ParseExact("2018-11-11", "yyyy-MM-dd", null), 25.111111 },
                    { DateTime.ParseExact("2018-11-10", "yyyy-MM-dd", null), 27.111111 },
                    { DateTime.ParseExact("2018-11-09", "yyyy-MM-dd", null), 25.111111 },
                    { DateTime.ParseExact("2018-11-08", "yyyy-MM-dd", null), 22.111111 },
                    { DateTime.ParseExact("2018-11-07", "yyyy-MM-dd", null), 18.111111 },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
