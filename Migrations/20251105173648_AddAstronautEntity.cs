using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spaceTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddAstronautEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "astronauts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    status_json = table.Column<string>(type: "TEXT", nullable: true),
                    type_json = table.Column<string>(type: "TEXT", nullable: true),
                    nationality_json = table.Column<string>(type: "TEXT", nullable: true),
                    agency_json = table.Column<string>(type: "TEXT", nullable: true),
                    image_json = table.Column<string>(type: "TEXT", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    date_of_death = table.Column<DateTime>(type: "TEXT", nullable: true),
                    profile_image_thumbnail = table.Column<string>(type: "TEXT", nullable: true),
                    profile_image = table.Column<string>(type: "TEXT", nullable: true),
                    bio = table.Column<string>(type: "TEXT", nullable: true),
                    twitter = table.Column<string>(type: "TEXT", nullable: true),
                    instagram = table.Column<string>(type: "TEXT", nullable: true),
                    wiki = table.Column<string>(type: "TEXT", nullable: true),
                    first_flight = table.Column<DateTime>(type: "TEXT", nullable: true),
                    last_flight = table.Column<DateTime>(type: "TEXT", nullable: true),
                    flights_count = table.Column<int>(type: "INTEGER", nullable: false),
                    landings_count = table.Column<int>(type: "INTEGER", nullable: false),
                    spacewalks_count = table.Column<int>(type: "INTEGER", nullable: false),
                    time_in_space = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_astronauts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "astronauts");
        }
    }
}
