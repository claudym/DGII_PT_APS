using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DGIIAPP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModelstoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComprobanteFiscal",
                columns: table => new
                {
                    NCF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RncCedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Itbis18 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobanteFiscal", x => x.NCF);
                });

            migrationBuilder.CreateTable(
                name: "Contribuyente",
                columns: table => new
                {
                    RncCedula = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribuyente", x => x.RncCedula);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprobanteFiscal");

            migrationBuilder.DropTable(
                name: "Contribuyente");
        }
    }
}
