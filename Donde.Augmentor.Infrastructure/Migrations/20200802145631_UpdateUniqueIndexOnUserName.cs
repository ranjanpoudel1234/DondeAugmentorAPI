using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class UpdateUniqueIndexOnUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
               name: "UserNameIndex",
               table: "AspnetUsers");

            migrationBuilder.Sql(@"CREATE UNIQUE INDEX IX_UserName_NonDeleted ON public.""AspNetUsers"" (""UserName"") WHERE ""IsDeleted"" = false");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "ix_username_nondeleted",
            table: "AspNetUsers");

            migrationBuilder.CreateIndex(
              name: "UserNameIndex",
              table: "AspNetUsers",
              columns: new[] { "UserName" },
              unique: true);
        }
    }
}
