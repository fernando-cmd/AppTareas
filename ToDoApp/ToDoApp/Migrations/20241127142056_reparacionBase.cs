using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    public partial class reparacionBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuario_UsuarioId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Tareas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuario_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuario_UsuarioId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Tareas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuario_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
