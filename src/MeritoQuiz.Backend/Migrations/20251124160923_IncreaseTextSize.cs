using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeritoQuiz.Backend.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseTextSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Questions",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(72)",
                oldMaxLength: 72);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Answers",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(72)",
                oldMaxLength: 72);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Questions",
                type: "character varying(72)",
                maxLength: 72,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Answers",
                type: "character varying(72)",
                maxLength: 72,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);
        }
    }
}
