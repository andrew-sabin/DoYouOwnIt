using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoYouOwnIt.Api.Migrations
{
    /// <inheritdoc />
    public partial class ReMakeFormatTypeIdNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ensure there's a default Category and FormatType, update any existing Formats
            // that have NULL for FormatTypeId to point to the default, then alter the column
            // to be non-nullable.
            migrationBuilder.Sql(@"BEGIN TRANSACTION;
DECLARE @catId INT;
IF EXISTS (SELECT 1 FROM Categories WHERE Name = 'Unknown' OR Slug = 'unknown')
    SELECT @catId = Id FROM Categories WHERE Name = 'Unknown' OR Slug = 'unknown';
ELSE
BEGIN
    INSERT INTO Categories (Name, Slug, CreatedDate, ModifiedDate)
    VALUES ('Unknown', 'unknown', SYSUTCDATETIME(), SYSUTCDATETIME());
    SET @catId = SCOPE_IDENTITY();
END

DECLARE @ftId INT;
IF EXISTS (SELECT 1 FROM FormatTypes WHERE Name = 'Unknown' AND CategoryId = @catId)
    SELECT @ftId = Id FROM FormatTypes WHERE Name = 'Unknown' AND CategoryId = @catId;
ELSE
BEGIN
    INSERT INTO FormatTypes (Name, Description, CategoryId, CreatedDate, ModifiedDate)
    VALUES ('Unknown', 'Auto-created default FormatType', @catId, SYSUTCDATETIME(), SYSUTCDATETIME());
    SET @ftId = SCOPE_IDENTITY();
END

UPDATE Formats SET FormatTypeId = @ftId WHERE FormatTypeId IS NULL;

COMMIT TRANSACTION;
");

            // Alter the column to be non-nullable now that all rows have a valid value
            migrationBuilder.AlterColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Make the column nullable again
            migrationBuilder.AlterColumn<int>(
                name: "FormatTypeId",
                table: "Formats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
