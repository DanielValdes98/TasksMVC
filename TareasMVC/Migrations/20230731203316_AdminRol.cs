using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                VALUES ('dafdbb4b-d0eb-4c14-b261-51c045d2f22e', 'admin', 'ADMIN')
                ON DUPLICATE KEY UPDATE Id = Id;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = 'dafdbb4b-d0eb-4c14-b261-51c045d2f22e'");
        }
    }
}
