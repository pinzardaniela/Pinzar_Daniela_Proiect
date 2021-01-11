using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pinzar_Daniela_Proiect.Migrations
{
    public partial class orderdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataComenzii",
                table: "Comanda",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataComenzii",
                table: "Comanda");
        }
    }
}
