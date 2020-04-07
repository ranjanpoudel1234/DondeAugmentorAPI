using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class AddMetricTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_Id",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_Id",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjects_Id",
                table: "AugmentObjects");

            migrationBuilder.DropIndex(
                name: "IX_AugmentObjectMedias_Id",
                table: "AugmentObjectMedias");

            migrationBuilder.DropIndex(
                name: "IX_AugmentImages_Id",
                table: "AugmentImages");

            migrationBuilder.DropIndex(
                name: "IX_Audios_Id",
                table: "Audios");

            migrationBuilder.CreateTable(
                name: "AugmentObjectMediaVisitMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AugmentObjectId = table.Column<Guid>(nullable: false),
                    AugmentObjectMediaId = table.Column<Guid>(nullable: false),
                    DeviceUniqueId = table.Column<string>(nullable: true),
                    DeviceName = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AugmentObjectMediaVisitMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AugmentObjectMediaVisitMetrics_AugmentObjects_AugmentObject~",
                        column: x => x.AugmentObjectId,
                        principalTable: "AugmentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AugmentObjectMediaVisitMetrics_AugmentObjectMedias_AugmentO~",
                        column: x => x.AugmentObjectMediaId,
                        principalTable: "AugmentObjectMedias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AugmentObjectVisitMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AugmentObjectId = table.Column<Guid>(nullable: false),
                    DeviceUniqueId = table.Column<string>(nullable: true),
                    DeviceName = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AugmentObjectVisitMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AugmentObjectVisitMetrics_AugmentObjects_AugmentObjectId",
                        column: x => x.AugmentObjectId,
                        principalTable: "AugmentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMediaVisitMetrics_AugmentObjectId",
                table: "AugmentObjectMediaVisitMetrics",
                column: "AugmentObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMediaVisitMetrics_AugmentObjectMediaId",
                table: "AugmentObjectMediaVisitMetrics",
                column: "AugmentObjectMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectVisitMetrics_AugmentObjectId",
                table: "AugmentObjectVisitMetrics",
                column: "AugmentObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AugmentObjectMediaVisitMetrics");

            migrationBuilder.DropTable(
                name: "AugmentObjectVisitMetrics");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Id",
                table: "Organizations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Id",
                table: "Avatars",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjects_Id",
                table: "AugmentObjects",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AugmentObjectMedias_Id",
                table: "AugmentObjectMedias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AugmentImages_Id",
                table: "AugmentImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audios_Id",
                table: "Audios",
                column: "Id",
                unique: true);
        }
    }
}
