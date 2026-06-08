using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class FormatRevisionsEditSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditSummary",
                table: "FormatRevisions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RevisionNumber",
                table: "FormatRevisions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditSummary",
                table: "FormatRevisions");

            migrationBuilder.DropColumn(
                name: "RevisionNumber",
                table: "FormatRevisions");
        }
    }
}
