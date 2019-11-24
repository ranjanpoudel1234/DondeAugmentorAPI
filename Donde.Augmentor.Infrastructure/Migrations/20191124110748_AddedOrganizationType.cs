using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class AddedOrganizationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_AugmentImages_AugmentImageId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Organizations_OrganizationId",
                table: "AugmentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Videos_VideoId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_AudioId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_AugmentImageId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_AvatarId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_OrganizationId",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_VideoId",
                table: "AugmentObjects");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationType",
                table: "Organizations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationType",
                table: "Organizations");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_AudioId",
                table: "AugmentObjects",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_AugmentImageId",
                table: "AugmentObjects",
                column: "AugmentImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_AvatarId",
                table: "AugmentObjects",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_OrganizationId",
                table: "AugmentObjects",
                column: "OrganizationId");

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
                name: "FK_AugmentObjects_AugmentImages_AugmentImageId",
                table: "AugmentObjects",
                column: "AugmentImageId",
                principalTable: "AugmentImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Organizations_OrganizationId",
                table: "AugmentObjects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
