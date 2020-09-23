using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class AddFileIdAndExtensionOnMediaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Videos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LogoExtension",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LogoFileId",
                table: "Organizations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Avatars",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Avatars",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "AugmentImages",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "AugmentImages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Audios",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Audios",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "LogoExtension",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "LogoFileId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "AugmentImages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "AugmentImages");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Audios");
        }
    }
}
