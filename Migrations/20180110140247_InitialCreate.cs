using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DevkitApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Devkit",
                columns: table => new
                {
                    DevkitID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devkit", x => x.DevkitID);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    ToolID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Aquire = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    URLRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.ToolID);
                    table.ForeignKey(
                        name: "FK_Tools_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevkitTools",
                columns: table => new
                {
                    DevkitToolsID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DevkitID = table.Column<int>(nullable: false),
                    ToolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevkitTools", x => x.DevkitToolsID);
                    table.ForeignKey(
                        name: "FK_DevkitTools_Devkit_DevkitID",
                        column: x => x.DevkitID,
                        principalTable: "Devkit",
                        principalColumn: "DevkitID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevkitTools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "ToolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevkitTools_DevkitID",
                table: "DevkitTools",
                column: "DevkitID");

            migrationBuilder.CreateIndex(
                name: "IX_DevkitTools_ToolId",
                table: "DevkitTools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_CategoryID",
                table: "Tools",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevkitTools");

            migrationBuilder.DropTable(
                name: "Devkit");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
