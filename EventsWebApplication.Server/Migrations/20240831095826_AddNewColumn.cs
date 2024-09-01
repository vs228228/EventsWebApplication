using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsWebApplication.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPacticipants",
                table: "Events",
                newName: "MaxParticipants");

            migrationBuilder.AddColumn<int>(
                name: "CountOfParticipants",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfParticipants",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "Events",
                newName: "MaxPacticipants");
        }
    }
}
