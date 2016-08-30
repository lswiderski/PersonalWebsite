using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Data.Migrations
{
    public partial class manytomanyPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Categories_CategoryId",
                table: "PostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Posts_PostId",
                table: "PostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTag",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategory",
                columns: new[] { "PostId", "CategoryId" });

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategories_Categories_CategoryId",
                table: "PostCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategories_Posts_PostId",
                table: "PostCategory",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                newName: "IX_PostTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_PostId",
                table: "PostTag",
                newName: "IX_PostTags_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategory_PostId",
                table: "PostCategory",
                newName: "IX_PostCategories_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategory_CategoryId",
                table: "PostCategory",
                newName: "IX_PostCategories_CategoryId");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostTags");

            migrationBuilder.RenameTable(
                name: "PostCategory",
                newName: "PostCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategories_Categories_CategoryId",
                table: "PostCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategories_Posts_PostId",
                table: "PostCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTags",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategories",
                columns: new[] { "PostId", "CategoryId" });

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Posts",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Categories_CategoryId",
                table: "PostCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Posts_PostId",
                table: "PostCategories",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                newName: "IX_PostTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                newName: "IX_PostTag_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategories_PostId",
                table: "PostCategories",
                newName: "IX_PostCategory_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategories_CategoryId",
                table: "PostCategories",
                newName: "IX_PostCategory_CategoryId");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostCategories",
                newName: "PostCategory");
        }
    }
}
