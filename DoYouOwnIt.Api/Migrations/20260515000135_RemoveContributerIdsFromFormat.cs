using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContributerIdsFromFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributerIds",
                table: "Formats");

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Formats");

            migrationBuilder.AddColumn<string>(
                name: "ContributerIds",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
