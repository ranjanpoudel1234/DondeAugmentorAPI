using Microsoft.EntityFrameworkCore.Migrations;

namespace Donde.Augmentor.Infrastructure.Migrations
{
    public partial class AddLogTableWithCustomSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               $@"CREATE TABLE Logs
                (
                    Id serial primary key,
                    Application character varying(100) NULL,
                    Logged text,
                    Level character varying(100) NULL,
                    Message character varying(8000) NULL,
                    Logger character varying(8000) NULL,
                    Callsite character varying(8000) NULL,
                    Exception character varying(8000) NULL
                )"
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP TABLE Logs");
        }
    }
}
