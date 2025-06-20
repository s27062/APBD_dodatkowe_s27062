using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_dodatkowe_s27062.Migrations
{
    /// <inheritdoc />
    public partial class AddforeignkeytoPrelegent_WydarzenietableforWydarzenieId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prelegent_Wydarzenie_Wydarzenie_ID",
                table: "Prelegent_Wydarzenie",
                column: "Wydarzenie_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prelegent_Wydarzenie_Wydarzenie_Wydarzenie_ID",
                table: "Prelegent_Wydarzenie",
                column: "Wydarzenie_ID",
                principalTable: "Wydarzenie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prelegent_Wydarzenie_Wydarzenie_Wydarzenie_ID",
                table: "Prelegent_Wydarzenie");

            migrationBuilder.DropIndex(
                name: "IX_Prelegent_Wydarzenie_Wydarzenie_ID",
                table: "Prelegent_Wydarzenie");
        }
    }
}
