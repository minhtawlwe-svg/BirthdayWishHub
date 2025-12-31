using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMessageWall.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaPath",
                table: "Messages",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaPath",
                table: "Messages");
        }
    }
}
