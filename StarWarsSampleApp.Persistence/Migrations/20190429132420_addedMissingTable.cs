using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWarsSampleApp.Persistence.Migrations
{
    public partial class addedMissingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEpisode_Characters_CharacterId",
                table: "CharacterEpisode");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEpisode_Episodes_EpisodeId",
                table: "CharacterEpisode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterEpisode",
                table: "CharacterEpisode");

            migrationBuilder.RenameTable(
                name: "CharacterEpisode",
                newName: "CharacterEpisodes");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterEpisode_EpisodeId",
                table: "CharacterEpisodes",
                newName: "IX_CharacterEpisodes_EpisodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterEpisodes",
                table: "CharacterEpisodes",
                columns: new[] { "CharacterId", "EpisodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEpisodes_Characters_CharacterId",
                table: "CharacterEpisodes",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEpisodes_Episodes_EpisodeId",
                table: "CharacterEpisodes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEpisodes_Characters_CharacterId",
                table: "CharacterEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEpisodes_Episodes_EpisodeId",
                table: "CharacterEpisodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterEpisodes",
                table: "CharacterEpisodes");

            migrationBuilder.RenameTable(
                name: "CharacterEpisodes",
                newName: "CharacterEpisode");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterEpisodes_EpisodeId",
                table: "CharacterEpisode",
                newName: "IX_CharacterEpisode_EpisodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterEpisode",
                table: "CharacterEpisode",
                columns: new[] { "CharacterId", "EpisodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEpisode_Characters_CharacterId",
                table: "CharacterEpisode",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEpisode_Episodes_EpisodeId",
                table: "CharacterEpisode",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
