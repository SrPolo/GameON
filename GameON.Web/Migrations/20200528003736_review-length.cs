using Microsoft.EntityFrameworkCore.Migrations;

namespace GameON.Web.Migrations
{
    public partial class reviewlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Reviews",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Reviews",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1500);
        }
    }
}
