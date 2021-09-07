using Microsoft.EntityFrameworkCore.Migrations;

namespace ProStock.API.Migrations
{
    public partial class Migration02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdReservada",
                table: "Estoques");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuarios",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);

            migrationBuilder.Sql("DROP TRIGGER IF EXISTS after_ProdutoVenda_insert");
            string createTrigger = @"
            CREATE TRIGGER `after_ProdutoVenda_insert` AFTER INSERT ON `produtosvendas`
            FOR EACH ROW BEGIN
                UPDATE estoques SET QtdAtual = QtdAtual - new.Quantidade WHERE ProdutoId = new.ProdutoId;
            END";
            migrationBuilder.Sql(createTrigger);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuarios",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QtdReservada",
                table: "Estoques",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("DROP TRIGGER IF EXISTS after_ProdutoVenda_insert");
        }
    }
}
