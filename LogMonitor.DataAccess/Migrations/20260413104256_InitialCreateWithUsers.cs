using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogMonitor.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "LogEntries");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LogEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_UserId",
                table: "LogEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApiKey",
                table: "Users",
                column: "ApiKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LogEntries_Users_UserId",
                table: "LogEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogEntries_Users_UserId",
                table: "LogEntries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_LogEntries_UserId",
                table: "LogEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LogEntries");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "LogEntries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
