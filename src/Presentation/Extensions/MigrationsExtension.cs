using Microsoft.EntityFrameworkCore;
using Presentation.DbContext;

namespace Presentation.Extensions
{
  public static class MigrationsExtension
  {
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
      using RecognizerContext recognizerContext = serviceScope.ServiceProvider.GetRequiredService<RecognizerContext>();
      recognizerContext.Database.Migrate();
    }
  }
}
