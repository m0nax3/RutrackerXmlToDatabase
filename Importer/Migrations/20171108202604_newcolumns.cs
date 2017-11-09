using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RutrackerImport.Migrations
{
    public partial class newcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Magnet",
                table: "Torrents");

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Torrents",
                type: "nvarchar(80)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackerId",
                table: "Torrents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "TrackerId",
                table: "Torrents");

            migrationBuilder.AddColumn<long>(
                name: "Magnet",
                table: "Torrents",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
