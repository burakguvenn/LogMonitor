using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogMonitor.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedIpsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowedIPs",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedIPs",
                table: "Users");
        }
    }
}
