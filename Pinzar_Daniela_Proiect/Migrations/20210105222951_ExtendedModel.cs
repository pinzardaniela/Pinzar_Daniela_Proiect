using Microsoft.EntityFrameworkCore.Migrations;

namespace Pinzar_Daniela_Proiect.Migrations
{
    public partial class ExtendedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distribuitor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeDistribuitor = table.Column<string>(maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuitor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DistribuitorProdus",
                columns: table => new
                {
                    DistribuitorID = table.Column<int>(nullable: false),
                    ProdusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistribuitorProdus", x => new { x.ProdusID, x.DistribuitorID });
                    table.ForeignKey(
                        name: "FK_DistribuitorProdus_Distribuitor_DistribuitorID",
                        column: x => x.DistribuitorID,
                        principalTable: "Distribuitor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistribuitorProdus_Produs_ProdusID",
                        column: x => x.ProdusID,
                        principalTable: "Produs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistribuitorProdus_DistribuitorID",
                table: "DistribuitorProdus",
                column: "DistribuitorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistribuitorProdus");

            migrationBuilder.DropTable(
                name: "Distribuitor");
        }
    }
}
