using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_dodatkowe_s27062.Migrations
{
    /// <inheritdoc />
    public partial class AddforeignkeytoUczestnik_WydarzenietableforUczestnikId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Uczestnik_Wydarzenie_Uczestnik_Uczestnik_ID",
                table: "Uczestnik_Wydarzenie",
                column: "Uczestnik_ID",
                principalTable: "Uczestnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uczestnik_Wydarzenie_Uczestnik_Uczestnik_ID",
                table: "Uczestnik_Wydarzenie");
        }
    }
}
