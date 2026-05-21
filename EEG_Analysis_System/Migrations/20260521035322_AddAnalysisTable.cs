using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EEG_Analysis_System.Migrations
{
    /// <inheritdoc />
    public partial class AddAnalysisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "AnalysisResults",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        ResultType = table.Column<string>(type: "nvarchar(max)", nullable: false),
        Confidence = table.Column<double>(type: "float", nullable: false),
        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
        Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_AnalysisResults", x => x.Id);
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AnalysisResults");
        }
    }
}
