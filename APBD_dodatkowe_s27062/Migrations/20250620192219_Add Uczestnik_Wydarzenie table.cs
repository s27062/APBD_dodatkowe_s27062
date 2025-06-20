using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_dodatkowe_s27062.Migrations
{
    /// <inheritdoc />
    public partial class AddUczestnik_Wydarzenietable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uczestnik_Wydarzenie",
                columns: table => new
                {
                    Uczestnik_ID = table.Column<int>(type: "int", nullable: false),
                    Wydarzenie_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczestnik_Wydarzenie", x => new { x.Uczestnik_ID, x.Wydarzenie_ID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uczestnik_Wydarzenie");
        }
    }
}
