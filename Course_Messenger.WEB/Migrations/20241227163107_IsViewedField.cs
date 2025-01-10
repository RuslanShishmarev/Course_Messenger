using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_Messenger.WEB.Migrations
{
    /// <inheritdoc />
    public partial class IsViewedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "Messages");
        }
    }
}
