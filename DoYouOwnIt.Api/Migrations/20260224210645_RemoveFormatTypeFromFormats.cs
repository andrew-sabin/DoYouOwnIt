using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFormatTypeFromFormats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formats_Products_ProductId",
                table: "Formats");

            migrationBuilder.AddColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Formats_FormatTypeId",
                table: "Formats",
                column: "FormatTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats",
                column: "FormatTypeId",
                principalTable: "FormatTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Formats_Products_ProductId",
                table: "Formats",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
