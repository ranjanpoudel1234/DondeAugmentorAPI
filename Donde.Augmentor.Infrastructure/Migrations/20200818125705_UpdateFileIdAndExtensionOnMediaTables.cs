using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class UpdateFileIdAndExtensionOnMediaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update public.""AugmentImages""
                                    set ""FileId"" = uuid(split_part(split_part(""Url"", '/', 2), '.', 1)),
                                        ""Extension"" = Concat('.', split_part(split_part(""Url"", '/', 2), '.', 2))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
