using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFormatRevisions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AIAssistsWith",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "IsAIAssisted",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "IsInPrint",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Formats");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Formats");

            migrationBuilder.RenameColumn(
                name: "OwnershipLevel",
                table: "Formats",
                newName: "FormatRevisionId");

            migrationBuilder.AlterColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "FormatRevisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormatId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormatTypeId = table.Column<int>(type: "int", nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsAiAssisted = table.Column<bool>(type: "bit", nullable: false),
                    AIAssistsWith = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnershipLevel = table.Column<int>(type: "int", nullable: false),
                    IsInPrint = table.Column<bool>(type: "bit", nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributerIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousRevisionId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormatRevisions_FormatRevisions_PreviousRevisionId",
                        column: x => x.PreviousRevisionId,
                        principalTable: "FormatRevisions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormatRevisions_FormatTypes_FormatTypeId",
                        column: x => x.FormatTypeId,
                        principalTable: "FormatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormatRevisions_Formats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "Formats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormatRevisions_FormatId",
                table: "FormatRevisions",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_FormatRevisions_FormatTypeId",
                table: "FormatRevisions",
                column: "FormatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormatRevisions_PreviousRevisionId",
                table: "FormatRevisions",
                column: "PreviousRevisionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormatRevisions");

            migrationBuilder.RenameColumn(
                name: "FormatRevisionId",
                table: "Formats",
                newName: "OwnershipLevel");

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
                name: "AIAssistsWith",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "Formats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAIAssisted",
                table: "Formats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInPrint",
                table: "Formats",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }
    }
}
