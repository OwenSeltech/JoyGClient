using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyGClient.Data.Migrations
{
    public partial class Preferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationsId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preferences_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Preferences_RestaurantClassifications_ClassificationsId",
                        column: x => x.ClassificationsId,
                        principalTable: "RestaurantClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_AppUserId",
                table: "Preferences",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_ClassificationsId",
                table: "Preferences",
                column: "ClassificationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preferences");
        }
    }
}
