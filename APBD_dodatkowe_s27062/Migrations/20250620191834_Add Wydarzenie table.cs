using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_dodatkowe_s27062.Migrations
{
    /// <inheritdoc />
    public partial class AddWydarzenietable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wydarzenie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxUczestnikow = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wydarzenie", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wydarzenie");
        }
    }
}
