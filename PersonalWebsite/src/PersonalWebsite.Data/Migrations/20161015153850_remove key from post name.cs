using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Data.Migrations
{
    public partial class removekeyfrompostname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Posts_Name",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Name",
                table: "Posts",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Name",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Posts_Name",
                table: "Posts",
                column: "Name");
        }
    }
}
