using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RutrackerImport.Migrations
{
    public partial class UrlColumnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Torrents");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Torrents",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Magnet",
                table: "Torrents",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ForumTitle",
                table: "Torrents",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Torrents",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Magnet",
                table: "Torrents",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ForumTitle",
                table: "Torrents",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Url",
                table: "Torrents",
                maxLength: 255,
                nullable: false,
                defaultValue: 0);
        }
    }
}
