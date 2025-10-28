using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spaceTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "space_programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    start_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    image_json = table.Column<string>(type: "TEXT", nullable: true),
                    agencies_json = table.Column<string>(type: "TEXT", nullable: true),
                    last_updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_space_programs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "space_programs");
        }
    }
}
