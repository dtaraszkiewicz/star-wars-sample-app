using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWarsSampleApp.Persistence.Migrations
{
    public partial class changeCharacterNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
