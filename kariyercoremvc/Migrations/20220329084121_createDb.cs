using Microsoft.EntityFrameworkCore.Migrations;

namespace kariyercoremvc.Migrations
{
    public partial class createDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasvuruSahibis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Yas = table.Column<int>(type: "int", nullable: false),
                    ToplamTecrube = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasvuruSahibis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deneyims",
                columns: table => new
                {
                    DeneyimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BSId = table.Column<int>(type: "int", nullable: false),
                    FirmaAdlari = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalismaTanimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalismaYili = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deneyims", x => x.DeneyimId);
                    table.ForeignKey(
                        name: "FK_Deneyims_BasvuruSahibis_BSId",
                        column: x => x.BSId,
                        principalTable: "BasvuruSahibis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deneyims_BSId",
                table: "Deneyims",
                column: "BSId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deneyims");

            migrationBuilder.DropTable(
                name: "BasvuruSahibis");
        }
    }
}
