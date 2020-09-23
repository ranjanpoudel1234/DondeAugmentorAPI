using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class RenameCodeToShortNameAndDropNowUnusedColumnsOnOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Organizations");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Organizations",
                newName: "ShortName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Organizations",
                newName: "Code");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Organizations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Organizations",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
