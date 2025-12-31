using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMessageWall.Migrations
{
    /// <inheritdoc />
    public partial class AddReactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "Messages",
                newName: "Wow");

            migrationBuilder.AddColumn<int>(
                name: "Celebrate",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Clap",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Laugh",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Love",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Celebrate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Clap",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Laugh",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Love",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Wow",
                table: "Messages",
                newName: "Likes");
        }
    }
}
