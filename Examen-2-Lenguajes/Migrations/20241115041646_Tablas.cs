using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_2_Lenguajes.Migrations
{
    /// <inheritdoc />
    public partial class Tablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tipo_movimiento",
                schema: "dbo",
                table: "Catalago_Cuentas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tipo_movimiento",
                schema: "dbo",
                table: "Catalago_Cuentas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
