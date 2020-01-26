using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class MadeAugmentObjectIdUniqueOnMediaTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AugmentObjectMedias_AugmentObjectId",
                table: "AugmentObjectMedias");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_AugmentObjectId",
                table: "AugmentObjectMedias",
                column: "AugmentObjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AugmentObjectMedias_AugmentObjectId",
                table: "AugmentObjectMedias");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_AugmentObjectId",
                table: "AugmentObjectMedias",
                column: "AugmentObjectId");
        }
    }
}
