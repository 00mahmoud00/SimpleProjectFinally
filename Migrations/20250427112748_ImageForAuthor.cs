using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ImageForAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Authors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Authors");
        }
    }
}
