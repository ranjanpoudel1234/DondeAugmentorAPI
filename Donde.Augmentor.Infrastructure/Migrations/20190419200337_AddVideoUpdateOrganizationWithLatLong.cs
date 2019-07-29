using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class AddVideoUpdateOrganizationWithLatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
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

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "Avatars",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "AugmentObjects",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "AugmentObjects",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<Guid>(
                name: "AudioId",
                table: "AugmentObjects",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "VideoId",
                table: "AugmentObjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "AugmentImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "Audios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_VideoId",
                table: "AugmentObjects",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_OrganizationId",
                table: "Videos",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Videos_VideoId",
                table: "AugmentObjects",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Videos_VideoId",
                table: "AugmentObjects");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_VideoId",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "AugmentImages");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "Audios");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "AugmentObjects",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "AugmentObjects",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AudioId",
                table: "AugmentObjects",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
