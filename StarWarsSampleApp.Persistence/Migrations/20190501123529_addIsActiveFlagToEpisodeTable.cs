using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWarsSampleApp.Persistence.Migrations
{
    public partial class addIsActiveFlagToEpisodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Episodes",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Episodes");
        }
    }
}
