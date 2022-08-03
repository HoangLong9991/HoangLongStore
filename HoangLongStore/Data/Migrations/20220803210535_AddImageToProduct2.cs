using Microsoft.EntityFrameworkCore.Migrations;

namespace HoangLongStore.Data.Migrations
{
    public partial class AddImageToProduct2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageProduct",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProduct",
                table: "Products");
        }
    }
}
