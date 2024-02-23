using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalShelterApi.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "CatId", "Age", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 9, "A sassy old lady who loves to lay around and judge", "Esther" },
                    { 2, 2, "A young kitty, loves to play and chase toys", "Mittens" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "DogId", "Age", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 3, "A fluffy puppy, kid friendly and loves to play", "Rex" },
                    { 2, 5, "A big dog, loves to run and play, plays well with other puppies and kitties", "Buddy" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "CatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "CatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "DogId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "DogId",
                keyValue: 2);
        }
    }
}
