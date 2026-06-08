using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class ReAddTypeFormatTypeEditionAndReleaseDateToFormatEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormatRevisions_FormatTypes_FormatTypeId",
                table: "FormatRevisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats");

            migrationBuilder.DropIndex(
                name: "IX_FormatRevisions_FormatTypeId",
                table: "FormatRevisions");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "FormatRevisions");

            migrationBuilder.DropColumn(
                name: "FormatTypeId",
                table: "FormatRevisions");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "FormatRevisions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "FormatRevisions");

            migrationBuilder.AlterColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Formats",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats",
                column: "FormatTypeId",
                principalTable: "FormatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Formats");

            migrationBuilder.AlterColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "FormatRevisions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormatTypeId",
                table: "FormatRevisions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReleaseDate",
                table: "FormatRevisions",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "FormatRevisions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormatRevisions_FormatTypeId",
                table: "FormatRevisions",
                column: "FormatTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormatRevisions_FormatTypes_FormatTypeId",
                table: "FormatRevisions",
                column: "FormatTypeId",
                principalTable: "FormatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Formats_FormatTypes_FormatTypeId",
                table: "Formats",
                column: "FormatTypeId",
                principalTable: "FormatTypes",
                principalColumn: "Id");
        }
    }
}
