using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class UpdateIsActiveTypeAndAddFkRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AugmentImages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Audios");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Organizations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Avatars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AugmentObjects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AugmentImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Audios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_OrganizationId",
                table: "Avatars",
                column: "OrganizationId");

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
                name: "IX_AugmentImages_OrganizationId",
                table: "AugmentImages",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Audios_OrganizationId",
                table: "Audios",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audios_Organizations_OrganizationId",
                table: "Audios",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentImages_Organizations_OrganizationId",
                table: "AugmentImages",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Audios_AudioId",
                table: "AugmentObjects",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Organizations_OrganizationId",
                table: "AugmentObjects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Organizations_OrganizationId",
                table: "Avatars",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audios_Organizations_OrganizationId",
                table: "Audios");

            migrationBuilder.DropForeignKey(
                name: "FK_AugmentImages_Organizations_OrganizationId",
                table: "AugmentImages");

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
                name: "FK_Avatars_Organizations_OrganizationId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_OrganizationId",
                table: "Avatars");

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
                name: "IX_AugmentImages_OrganizationId",
                table: "AugmentImages");

            migrationBuilder.DropIndex(
                name: "IX_Audios_OrganizationId",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AugmentObjects");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AugmentImages");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Audios");

            migrationBuilder.AddColumn<DateTime>(
                name: "IsActive",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IsActive",
                table: "Organizations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IsActive",
                table: "Avatars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IsActive",
                table: "AugmentObjects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IsActive",
                table: "AugmentImages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IsActive",
                table: "Audios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
