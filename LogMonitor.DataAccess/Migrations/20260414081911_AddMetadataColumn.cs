using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogMonitor.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMetadataColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "LogEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "LogEntries");
        }
    }
}
