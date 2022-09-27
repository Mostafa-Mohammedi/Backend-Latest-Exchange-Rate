using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Update",
                columns: table => new
                {
                    id_update = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_update = table.Column<DateTime>(type: "datetime", nullable: true),
                    @base = table.Column<string>(name: "base", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    timestamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Update", x => x.id_update);
                });

            migrationBuilder.CreateTable(
                name: "Rates_Update",
                columns: table => new
                {
                    id_rate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    id_update = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates_Update", x => x.id_rate);
                    table.ForeignKey(
                        name: "FK_Rates_Update_Update",
                        column: x => x.id_update,
                        principalTable: "Update",
                        principalColumn: "id_update");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_Update_id_update",
                table: "Rates_Update",
                column: "id_update");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates_Update");

            migrationBuilder.DropTable(
                name: "Update");
        }
    }
}
