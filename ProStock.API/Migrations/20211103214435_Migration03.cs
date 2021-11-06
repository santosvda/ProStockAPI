using Microsoft.EntityFrameworkCore.Migrations;

namespace ProStock.API.Migrations
{
    public partial class Migration03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Pessoas",
                maxLength: 17,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Pessoas",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Pessoas",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 17,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Pessoas",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 14,
                oldNullable: true);
        }
    }
}
