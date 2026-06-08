using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFormatTypeToFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formats_Products_ProductId",
                table: "Formats");

            // Make the new column nullable first so we don't create FK violations
            // for existing rows. After data is fixed, you can make it non-nullable
            // in a later migration.
            migrationBuilder.AddColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Formats_FormatTypeId",
                table: "Formats",
                column: "FormatTypeId");

            // Add the foreign key without cascade delete to avoid multiple cascade
            // paths and to ensure the database doesn't try to cascade deletes
            // which can cause conflicts. Use ReferentialAction.NoAction or Restrict
            // depending on your SQL Server version/EF behavior.
            migrationBuilder.AddForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats",
                column: "FormatTypeId",
                principalTable: "FormatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Formats_Products_ProductId",
                table: "Formats",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats");

            migrationBuilder.DropForeignKey(
                name: "FK_Formats_Products_ProductId",
                table: "Formats");

            migrationBuilder.DropIndex(
                name: "IX_Formats_FormatTypeId",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "FormatTypeId",
                table: "Formats");

            migrationBuilder.AddForeignKey(
                name: "FK_Formats_Products_ProductId",
                table: "Formats",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
