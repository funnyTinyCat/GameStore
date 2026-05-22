using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    // if we want to execute models' changes automatically. 
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope(); 
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}