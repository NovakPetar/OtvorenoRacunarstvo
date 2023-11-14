using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Autoceste.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedsp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autoceste",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    duljina = table.Column<double>(type: "double precision", nullable: false),
                    oznaka = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    neformalninaziv = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    dionica = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("autoceste_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "naplatnepostaje",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    autocestaid = table.Column<int>(type: "integer", nullable: true),
                    naziv = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    geoduzina = table.Column<double>(type: "double precision", nullable: true),
                    geosirina = table.Column<double>(type: "double precision", nullable: true),
                    imaenc = table.Column<bool>(type: "boolean", nullable: true),
                    kontakt = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("naplatnepostaje_pkey", x => x.id);
                    table.ForeignKey(
                        name: "naplatnepostaje_autocestaid_fkey",
                        column: x => x.autocestaid,
                        principalTable: "autoceste",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_naplatnepostaje_autocestaid",
                table: "naplatnepostaje",
                column: "autocestaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "naplatnepostaje");

            migrationBuilder.DropTable(
                name: "autoceste");
        }
    }
}
