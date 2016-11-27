using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Torrents",
                table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Size = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 1024, nullable: true),
                    ForumId = table.Column<int>(nullable: false),
                    ForumTitle = table.Column<string>(maxLength: 512, nullable: true),
                    Magnet = table.Column<string>(maxLength: 512, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_TorrentsID", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Torrents");
        }
    }
}