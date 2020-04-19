using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class RenameIsActiveToIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Videos",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Organizations",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Avatars",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AugmentObjects",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AugmentObjectMedias",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AugmentObjectLocations",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AugmentImages",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Audios",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Videos",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Organizations",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Avatars",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AugmentObjects",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AugmentObjectMedias",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AugmentObjectLocations",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AugmentImages",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Audios",
                newName: "IsActive");
        }
    }
}
