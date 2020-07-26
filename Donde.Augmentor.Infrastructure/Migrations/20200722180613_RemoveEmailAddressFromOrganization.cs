using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class RemoveEmailAddressFromOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Organizations_OrganizationId",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Site",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Organizations");

            migrationBuilder.RenameTable(
                name: "Site",
                newName: "Sites");

            migrationBuilder.RenameIndex(
                name: "IX_Site_OrganizationId",
                table: "Sites",
                newName: "IX_Sites_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_Organizations_OrganizationId",
                table: "Sites",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sites_Organizations_OrganizationId",
                table: "Sites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.RenameTable(
                name: "Sites",
                newName: "Site");

            migrationBuilder.RenameIndex(
                name: "IX_Sites_OrganizationId",
                table: "Site",
                newName: "IX_Site_OrganizationId");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Site",
                table: "Site",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Organizations_OrganizationId",
                table: "Site",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
