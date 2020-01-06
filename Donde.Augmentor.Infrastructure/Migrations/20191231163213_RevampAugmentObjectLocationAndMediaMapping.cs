using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class RevampAugmentObjectLocationAndMediaMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Videos_VideoId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_AudioId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_AvatarId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_VideoId",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "AudioId",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "AugmentObjects");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Organizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AugmentObjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AugmentObjectLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AugmentObjectId = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AugmentObjectLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AugmentObjectLocations_AugmentObjects_AugmentObjectId",
                        column: x => x.AugmentObjectId,
                        principalTable: "AugmentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AugmentObjectMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AvatarId = table.Column<Guid>(nullable: true),
                    AudioId = table.Column<Guid>(nullable: true),
                    VideoId = table.Column<Guid>(nullable: true),
                    MediaType = table.Column<int>(nullable: false),
                    AugmentObjectId = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AugmentObjectMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AugmentObjectMedias_Audios_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AugmentObjectMedias_AugmentObjects_AugmentObjectId",
                        column: x => x.AugmentObjectId,
                        principalTable: "AugmentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AugmentObjectMedias_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AugmentObjectMedias_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectLocations_AugmentObjectId",
                table: "AugmentObjectLocations",
                column: "AugmentObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_AudioId",
                table: "AugmentObjectMedias",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_AugmentObjectId",
                table: "AugmentObjectMedias",
                column: "AugmentObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_AvatarId",
                table: "AugmentObjectMedias",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_Id",
                table: "AugmentObjectMedias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_VideoId",
                table: "AugmentObjectMedias",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AugmentObjectLocations");

            migrationBuilder.DropTable(
                name: "AugmentObjectMedias");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AugmentObjects");

            migrationBuilder.AddColumn<Guid>(
                name: "AudioId",
                table: "AugmentObjects",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "AugmentObjects",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AugmentObjects",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AugmentObjects",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VideoId",
                table: "AugmentObjects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_AudioId",
                table: "AugmentObjects",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_AvatarId",
                table: "AugmentObjects",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_VideoId",
                table: "AugmentObjects",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects",
                column: "AvatarId",
                principalTable: "Avatars",
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
    }
}
