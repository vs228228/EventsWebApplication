using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsWebApplication.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationString",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationString",
                table: "Users");
        }
    }
}
