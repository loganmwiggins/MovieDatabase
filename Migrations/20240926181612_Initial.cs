using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace jm_sql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Runtime = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    ActorId = table.Column<int>(type: "integer", nullable: false),
                    CharacterName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Characters_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresGenreId = table.Column<int>(type: "integer", nullable: false),
                    MoviesMovieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresGenreId, x.MoviesMovieId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "ActorId", "Name" },
                values: new object[,]
                {
                    { 1, "Heath Ledger" },
                    { 2, "Julia Stiles" },
                    { 3, "Joseph Gordon-Levitt" },
                    { 4, "Leonardo DiCaprio" },
                    { 5, "Jonah Hill" },
                    { 6, "Margot Robbie" },
                    { 7, "Christian Bale" },
                    { 8, "Justin Theroux" },
                    { 9, "Josh Lucas" },
                    { 10, "Ben Stiller" },
                    { 11, "Robin Williamss" },
                    { 12, "Owen Wilson" },
                    { 13, "Robert Downey Jr." },
                    { 14, "Scarlett Johansson" },
                    { 15, "Chris Evans" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Comedy" },
                    { 4, "Drama" },
                    { 5, "Horror" },
                    { 6, "Science Fiction" },
                    { 7, "Fantasy" },
                    { 8, "Thriller" },
                    { 9, "Romance" },
                    { 10, "Mystery" },
                    { 11, "Documentary" },
                    { 12, "Animation" },
                    { 13, "Musical" },
                    { 14, "Crime" },
                    { 15, "War" },
                    { 16, "Western" },
                    { 17, "Historical" },
                    { 18, "Biographical" },
                    { 19, "Family" },
                    { 20, "Superhero" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Description", "Rating", "ReleaseDate", "Runtime", "Title" },
                values: new object[,]
                {
                    { 1, null, "PG-13", new DateOnly(1999, 3, 31), 97, "10 Things I Hate About You" },
                    { 2, null, "R", new DateOnly(2013, 12, 25), 180, "The Wolf of Wall Street" },
                    { 3, null, "R", new DateOnly(2000, 4, 14), 104, "American Psycho" },
                    { 4, null, "PG", new DateOnly(2006, 12, 17), 108, "Night at the Museum" },
                    { 5, null, "PG-13", new DateOnly(2019, 4, 26), 181, "Avengers: Endgame" }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "CharacterId", "ActorId", "CharacterName", "MovieId" },
                values: new object[,]
                {
                    { 1, 1, "Patrick Verona", 1 },
                    { 2, 2, "Kat Stratford", 1 },
                    { 3, 3, "Cameron James", 1 },
                    { 4, 4, "Jordan Belfort", 2 },
                    { 5, 5, "Donnie Azoff", 2 },
                    { 6, 6, "Naomi Lapaglia", 2 },
                    { 7, 7, "Patrick Bateman", 3 },
                    { 8, 8, "Timothy Bryce", 3 },
                    { 9, 9, "Craig McDermott", 3 },
                    { 10, 10, "Larry Daley", 4 },
                    { 11, 11, "Theodore Roosevelt", 4 },
                    { 12, 12, "Jebediah Smith", 4 },
                    { 13, 13, "Tony Stark / Iron Man", 5 },
                    { 14, 14, "Natasha Romanov / Black Widow", 5 },
                    { 15, 15, "Steve Rodgers / Captain America", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ActorId",
                table: "Characters",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_MovieId",
                table: "Characters",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesMovieId",
                table: "GenreMovie",
                column: "MoviesMovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
