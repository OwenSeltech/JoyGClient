using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyGClient.Data.Migrations
{
    public partial class FixClassifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RestaurantClassifications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RestaurantClassifications");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "RestaurantClassifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "RestaurantClassifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantClassifications_CreatedById",
                table: "RestaurantClassifications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantClassifications_UpdatedById",
                table: "RestaurantClassifications",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantClassifications_AspNetUsers_CreatedById",
                table: "RestaurantClassifications",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantClassifications_AspNetUsers_UpdatedById",
                table: "RestaurantClassifications",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantClassifications_AspNetUsers_CreatedById",
                table: "RestaurantClassifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantClassifications_AspNetUsers_UpdatedById",
                table: "RestaurantClassifications");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantClassifications_CreatedById",
                table: "RestaurantClassifications");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantClassifications_UpdatedById",
                table: "RestaurantClassifications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RestaurantClassifications");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "RestaurantClassifications");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RestaurantClassifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "RestaurantClassifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
