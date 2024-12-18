using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    public partial class AgregarRelacionesUsuarioTarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Tareas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuario_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuario_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tareas");
        }
    }
}
