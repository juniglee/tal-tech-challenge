using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAL.TechTest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blockout",
                table: "Blockout");

            migrationBuilder.RenameTable(
                name: "Blockout",
                newName: "Blockouts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blockouts",
                table: "Blockouts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blockouts",
                table: "Blockouts");

            migrationBuilder.RenameTable(
                name: "Blockouts",
                newName: "Blockout");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blockout",
                table: "Blockout",
                column: "Id");
        }
    }
}
