using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Data.Migrations
{
    public partial class imagespostsfileschanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeaderImageId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedOn",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedOn",
                table: "Files",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_HeaderImageId",
                table: "Posts",
                column: "HeaderImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ThumbnailId",
                table: "Images",
                column: "ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Files_ThumbnailId",
                table: "Images",
                column: "ThumbnailId",
                principalTable: "Files",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_HeaderImageId",
                table: "Posts",
                column: "HeaderImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Files_ThumbnailId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Images_HeaderImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_HeaderImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Images_ThumbnailId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "HeaderImageId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PublishedOn",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UploadedOn",
                table: "Files");
        }
    }
}
