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

            migrationBuilder.Sql(@"update public.""Audios""
                                    set ""FileId"" = uuid(split_part(split_part(""Url"", '/', 2), '.', 1)),
                                        ""Extension"" = Concat('.', split_part(split_part(""Url"", '/', 2), '.', 2))");

            migrationBuilder.Sql(@"update public.""Videos""
                                    set ""FileId"" = uuid(split_part(split_part(""Url"", '/', 2), '.', 1)),
                                        ""Extension"" = Concat('.', split_part(split_part(""Url"", '/', 2), '.', 2))");     

            migrationBuilder.Sql(@"update public.""Organizations""
                                    set ""LogoFileId"" = uuid(split_part(split_part(""LogoUrl"", '/', 2), '.', 1)),
                                        ""LogoExtension"" = Concat('.', split_part(split_part(""LogoUrl"", '/', 2), '.', 2))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
