
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";    
    private static readonly List<GameDto> games = [
        new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new (2, "Final Fantasy VII Rebirth", "RPG", 69.99m, new DateOnly(2024, 2, 29)),
        new (3, "Astro Bot", "Platformer", 59.99M, new DateOnly(2024, 9, 6))
    ];


    public static void MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

   
        group.MapGet("/", () =>
        {
            return games;
        });


        group.MapGet("/{id}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game);
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {

            GameDto game = new (
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                games[index].Id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
        
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        });

    }
}