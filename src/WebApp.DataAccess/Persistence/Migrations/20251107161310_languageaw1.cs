using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApp.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class languageaw1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_LanguageId_LanguageId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "LanguageId");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_LanguageId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Subjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Subjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LanguageId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engId = table.Column<int>(type: "integer", nullable: false),
                    rusId = table.Column<int>(type: "integer", nullable: false),
                    uzId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageId", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LanguageId",
                table: "Subjects",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_LanguageId_LanguageId",
                table: "Subjects",
                column: "LanguageId",
                principalTable: "LanguageId",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
