using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyGClient.Data.Migrations
{
    public partial class Survey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    DateVisited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmbienceRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverallRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishesEnjoyed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishesId = table.Column<int>(type: "int", nullable: false),
                    FlavourRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentationRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WineRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishesEnjoyed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishesEnjoyed_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishesEnjoyed_Dishes_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishesEnjoyed_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishesEnjoyed_DishesId",
                table: "DishesEnjoyed",
                column: "DishesId");

            migrationBuilder.CreateIndex(
                name: "IX_DishesEnjoyed_SurveyId",
                table: "DishesEnjoyed",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_DishesEnjoyed_UserId",
                table: "DishesEnjoyed",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_RestaurantId",
                table: "Surveys",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishesEnjoyed");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
