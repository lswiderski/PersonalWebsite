using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonalWebsite.Common.Enums;

namespace PersonalWebsite.Data.Migrations
{
    public partial class PostStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsTrashed",
                table: "Posts");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Posts",
                nullable: false,
                defaultValue: PostStatusType.DRAFT);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrashed",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }
    }
}
