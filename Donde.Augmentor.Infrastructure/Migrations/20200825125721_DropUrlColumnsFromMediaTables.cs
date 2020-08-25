using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class DropUrlColumnsFromMediaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "AugmentImages");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Audios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Avatars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "AugmentImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Audios",
                nullable: true);
        }
    }
}
