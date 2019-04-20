using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class MakeAvatarIdNullableInAugmentObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects");

            migrationBuilder.AlterColumn<Guid>(
                name: "AvatarId",
                table: "AugmentObjects",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects");

            migrationBuilder.AlterColumn<Guid>(
                name: "AvatarId",
                table: "AugmentObjects",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentObjects_Avatars_AvatarId",
                table: "AugmentObjects",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
