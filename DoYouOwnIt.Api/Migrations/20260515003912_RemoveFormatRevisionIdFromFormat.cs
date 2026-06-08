using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFormatRevisionIdFromFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormatRevisionId",
                table: "Formats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormatRevisionId",
                table: "Formats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
