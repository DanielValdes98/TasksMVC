using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class TareasUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacionId",
                table: "Tareas",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioCreacionId",
                table: "Tareas",
                column: "UsuarioCreacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_AspNetUsers_UsuarioCreacionId",
                table: "Tareas",
                column: "UsuarioCreacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_AspNetUsers_UsuarioCreacionId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_UsuarioCreacionId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacionId",
                table: "Tareas");
        }
    }
}
