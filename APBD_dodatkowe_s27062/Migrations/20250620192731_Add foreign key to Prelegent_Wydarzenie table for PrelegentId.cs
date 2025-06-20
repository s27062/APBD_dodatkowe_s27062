using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_dodatkowe_s27062.Migrations
{
    /// <inheritdoc />
    public partial class AddforeignkeytoPrelegent_WydarzenietableforPrelegentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Prelegent_Wydarzenie_Prelegent_Prelegent_ID",
                table: "Prelegent_Wydarzenie",
                column: "Prelegent_ID",
                principalTable: "Prelegent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prelegent_Wydarzenie_Prelegent_Prelegent_ID",
                table: "Prelegent_Wydarzenie");
        }
    }
}
