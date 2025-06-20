using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_dodatkowe_s27062.Migrations
{
    /// <inheritdoc />
    public partial class AddPrelegent_Wydarzenietable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prelegent_Wydarzenie",
                columns: table => new
                {
                    Prelegent_ID = table.Column<int>(type: "int", nullable: false),
                    Wydarzenie_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prelegent_Wydarzenie", x => new { x.Prelegent_ID, x.Wydarzenie_ID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prelegent_Wydarzenie");
        }
    }
}
