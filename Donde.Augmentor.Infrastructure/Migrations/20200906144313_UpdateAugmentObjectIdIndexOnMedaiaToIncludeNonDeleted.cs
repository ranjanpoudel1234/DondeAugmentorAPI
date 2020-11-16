using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class UpdateAugmentObjectIdIndexOnMedaiaToIncludeNonDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
               name: "IX_AugmentObjectMedias_AugmentObjectId",
               table: "AugmentObjectMedias");

            migrationBuilder.Sql(@"CREATE UNIQUE INDEX IX_AugmentObjectMedias_AugmentObjectId_NonDeleted ON public.""AugmentObjectMedias"" (""AugmentObjectId"") WHERE ""IsDeleted"" = false");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "ix_augmentobjectmedias_augmentobjectid_nondeleted",
            table: "AugmentObjectMedias");

            migrationBuilder.CreateIndex(
                 name: "IX_AugmentObjectMedias_AugmentObjectId",
                 table: "AugmentObjectMedias",
                 column: "AugmentObjectId");
        }
    }
}
