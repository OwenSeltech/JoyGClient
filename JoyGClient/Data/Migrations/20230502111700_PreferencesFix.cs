using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyGClient.Data.Migrations
{
    public partial class PreferencesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_AspNetUsers_AppUserId",
                table: "Preferences");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Preferences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_AspNetUsers_AppUserId",
                table: "Preferences",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_AspNetUsers_AppUserId",
                table: "Preferences");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Preferences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_AspNetUsers_AppUserId",
                table: "Preferences",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
