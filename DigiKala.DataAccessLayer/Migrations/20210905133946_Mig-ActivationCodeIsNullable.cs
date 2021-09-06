using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiKala.DataAccessLayer.Migrations
{
    public partial class MigActivationCodeIsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "Users",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "Users",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6,
                oldNullable: true);
        }
    }
}
