using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWarsSampleApp.Persistence.Migrations
{
    public partial class addIsActiveFlagToCharactersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Episodes",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Characters",
                nullable: true,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Characters");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Episodes",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: true);
        }
    }
}
