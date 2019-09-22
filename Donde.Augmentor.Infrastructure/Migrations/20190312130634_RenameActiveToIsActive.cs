using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class RenameActiveToIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Organizations",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Avatars",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "AugmentObjects",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "AugmentImages",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Audios",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Organizations",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Avatars",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AugmentObjects",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AugmentImages",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Audios",
                newName: "Active");
        }
    }
}
