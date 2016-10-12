using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonalWebsite.Data.Migrations
{
    public partial class Adventuresandimagetags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Map = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adventures_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => new { x.ImageId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ImageTags_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tags_Name",
                table: "Tags",
                column: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Posts_Name",
                table: "Posts",
                column: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_PostId",
                table: "Adventures",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_ImageId",
                table: "ImageTags",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_TagId",
                table: "ImageTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tags_Name",
                table: "Tags");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Posts_Name",
                table: "Posts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_Name",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Adventures");

            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: true);
        }
    }
}
