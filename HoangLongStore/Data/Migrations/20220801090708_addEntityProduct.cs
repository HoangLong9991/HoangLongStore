﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace HoangLongStore.Data.Migrations
{
    public partial class addEntityProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProduct = table.Column<string>(nullable: true),
                    QuantityProduct = table.Column<int>(nullable: false),
                    PriceProduct = table.Column<int>(nullable: false),
                    DescriptionProduct = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}