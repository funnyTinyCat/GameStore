
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres"); 

    // GET /genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext.Genres
                .Select(g => new GenreDto(g.Id, g.Name))
                .AsNoTracking() 
                .ToListAsync();
        });
    }
}